using System;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Spells.Bushido;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;
using Server.Spells.Second;
using Server.Spells.Spellweaving;
using Server.Targeting;
using daat99;

namespace Server.Spells
{
	public abstract class Spell : ISpell
	{
		private readonly Mobile m_Caster;
		private readonly Item m_Scroll;
		private readonly SpellInfo m_Info;
		private SpellState m_State;
		private DateTime m_StartCastTime;

		public SpellState State
		{
			get
			{
				return this.m_State;
			}
			set
			{
				this.m_State = value;
			}
		}
		public Mobile Caster
		{
			get
			{
				return this.m_Caster;
			}
		}
		public SpellInfo Info
		{
			get
			{
				return this.m_Info;
			}
		}
		public string Name
		{
			get
			{
				return this.m_Info.Name;
			}
		}
		public string Mantra
		{
			get
			{
				return this.m_Info.Mantra;
			}
		}
		public Type[] Reagents
		{
			get
			{
				return this.m_Info.Reagents;
			}
		}
		public Item Scroll
		{
			get
			{
				return this.m_Scroll;
			}
		}
		public DateTime StartCastTime
		{
			get
			{
				return this.m_StartCastTime;
			}
		}

		private static readonly TimeSpan NextSpellDelay = TimeSpan.FromSeconds(0.75);
		private static readonly TimeSpan AnimateDelay = TimeSpan.FromSeconds(1.5);

		public virtual SkillName CastSkill
		{
			get
			{
				return SkillName.Magery;
			}
		}
		public virtual SkillName DamageSkill
		{
			get
			{
				return SkillName.EvalInt;
			}
		}

		public virtual bool RevealOnCast
		{
			get
			{
				return true;
			}
		}
		public virtual bool ClearHandsOnCast
		{
			get
			{
				return true;
			}
		}
		public virtual bool ShowHandMovement
		{
			get
			{
				return true;
			}
		}

		public virtual bool DelayedDamage
		{
			get
			{
				return false;
			}
		}

		public virtual bool DelayedDamageStacking
		{
			get
			{
				return true;
			}
		}
		//In reality, it's ANY delayed Damage spell Post-AoS that can't stack, but, only 
		//Expo & Magic Arrow have enough delay and a short enough cast time to bring up 
		//the possibility of stacking 'em.  Note that a MA & an Explosion will stack, but
		//of course, two MA's won't.

		private static readonly Dictionary<Type, DelayedDamageContextWrapper> m_ContextTable = new Dictionary<Type, DelayedDamageContextWrapper>();

		private class DelayedDamageContextWrapper
		{
			private readonly Dictionary<Mobile, Timer> m_Contexts = new Dictionary<Mobile, Timer>();

			public void Add(Mobile m, Timer t)
			{
				Timer oldTimer;
				if (this.m_Contexts.TryGetValue(m, out oldTimer))
				{
					oldTimer.Stop();
					this.m_Contexts.Remove(m);
				}

				this.m_Contexts.Add(m, t);
			}

			public void Remove(Mobile m)
			{
				this.m_Contexts.Remove(m);
			}
		}

		public void StartDelayedDamageContext(Mobile m, Timer t)
		{
			if (this.DelayedDamageStacking)
				return; //Sanity

			DelayedDamageContextWrapper contexts;

			if (!m_ContextTable.TryGetValue(this.GetType(), out contexts))
			{
				contexts = new DelayedDamageContextWrapper();
				m_ContextTable.Add(this.GetType(), contexts);
			}

			contexts.Add(m, t);
		}

		public void RemoveDelayedDamageContext(Mobile m)
		{
			DelayedDamageContextWrapper contexts;

			if (!m_ContextTable.TryGetValue(this.GetType(), out contexts))
				return;

			contexts.Remove(m);
		}

		public void HarmfulSpell(Mobile m)
		{
			if (m is BaseCreature)
				((BaseCreature)m).OnHarmfulSpell(this.m_Caster);
		}

		public Spell(Mobile caster, Item scroll, SpellInfo info)
		{
			this.m_Caster = caster;
			this.m_Scroll = scroll;
			this.m_Info = info;
		}

		public virtual int GetNewAosDamage(int bonus, int dice, int sides, Mobile singleTarget)
		{
			if (singleTarget != null)
			{
				return this.GetNewAosDamage(bonus, dice, sides, (this.Caster.Player && singleTarget.Player), this.GetDamageScalar(singleTarget));
			}
			else
			{
				return this.GetNewAosDamage(bonus, dice, sides, false);
			}
		}

		public virtual int GetNewAosDamage(int bonus, int dice, int sides, bool playerVsPlayer)
		{
			return this.GetNewAosDamage(bonus, dice, sides, playerVsPlayer, 1.0);
		}

		public virtual int GetNewAosDamage(int bonus, int dice, int sides, bool playerVsPlayer, double scalar)
		{
			int damage = Utility.Dice(dice, sides, bonus) * 100;
			int damageBonus = 0;

			int inscribeSkill = this.GetInscribeFixed(this.m_Caster);
			int inscribeBonus = (inscribeSkill + (1000 * (inscribeSkill / 1000))) / 200;
			damageBonus += inscribeBonus;

			int intBonus = this.Caster.Int / 10;
			damageBonus += intBonus;

			int sdiBonus = AosAttributes.GetValue(this.m_Caster, AosAttribute.SpellDamage);
			
			#region Mondain's Legacy
			sdiBonus += ArcaneEmpowermentSpell.GetSpellBonus(this.m_Caster, playerVsPlayer);
			#endregion
			
			// PvP spell damage increase cap of 15% from an item’s magic property
			if (playerVsPlayer && sdiBonus > 15)
				sdiBonus = 15;

			damageBonus += sdiBonus;

			TransformContext context = TransformationSpellHelper.GetContext(this.Caster);

			if (context != null && context.Spell is ReaperFormSpell)
				damageBonus += ((ReaperFormSpell)context.Spell).SpellDamageBonus;

			damage = AOS.Scale(damage, 100 + damageBonus);

			int evalSkill = this.GetDamageFixed(this.m_Caster);
			int evalScale = 30 + ((9 * evalSkill) / 100);

			damage = AOS.Scale(damage, evalScale);

			damage = AOS.Scale(damage, (int)(scalar * 100));

			return damage / 100;
		}

		public virtual bool IsCasting
		{
			get
			{
				return this.m_State == SpellState.Casting;
			}
		}

		public virtual void OnCasterHurt()
		{
			//Confirm: Monsters and pets cannot be disturbed.
			if (!this.Caster.Player)
				return;

			if (this.IsCasting)
			{
				object o = ProtectionSpell.Registry[this.m_Caster];
				bool disturb = true;

				if (o != null && o is double)
				{
					if (((double)o) > Utility.RandomDouble() * 100.0)
						disturb = false;
				}

				#region Stygian Abyss
				int focus = SAAbsorptionAttributes.GetValue(this.Caster, SAAbsorptionAttribute.CastingFocus);

				if (focus > 0)
				{
					if (focus > 30)
						focus = 30;

					if (focus > Utility.Random(100))
					{
						disturb = false;
						this.Caster.SendLocalizedMessage(1113690); // You regain your focus and continue casting the spell.
					}
				}
				#endregion
				if (disturb)
					this.Disturb(DisturbType.Hurt, false, true);
			}
		}

		public virtual void OnCasterKilled()
		{
			this.Disturb(DisturbType.Kill);
		}

		public virtual void OnConnectionChanged()
		{
			this.FinishSequence();
		}

		public virtual bool OnCasterMoving(Direction d)
		{
			if (this.IsCasting && this.BlocksMovement)
			{
				this.m_Caster.SendLocalizedMessage(500111); // You are frozen and can not move.
				return false;
			}

			return true;
		}

		public virtual bool OnCasterEquiping(Item item)
		{
			if (this.IsCasting)
				this.Disturb(DisturbType.EquipRequest);

			return true;
		}

		public virtual bool OnCasterUsingObject(object o)
		{
			if (this.m_State == SpellState.Sequencing)
				this.Disturb(DisturbType.UseRequest);

			return true;
		}

		public virtual bool OnCastInTown(Region r)
		{
			return this.m_Info.AllowTown;
		}

		public virtual bool ConsumeReagents()
		{
			if (this.m_Scroll != null || !this.m_Caster.Player)
				return true;

			if (AosAttributes.GetValue(this.m_Caster, AosAttribute.LowerRegCost) > Utility.Random(100))
				return true;

			if (Engines.ConPVP.DuelContext.IsFreeConsume(this.m_Caster))
				return true;

			Container pack = this.m_Caster.Backpack;

			if (pack == null)
				return false;

			if (pack.ConsumeTotal(this.m_Info.Reagents, this.m_Info.Amounts) == -1)
				return true;

			//daat99 OWLTR start - use SpellCastersKey
			if (OWLTROptionsManager.IsEnabled(OWLTROptionsManager.OPTIONS_ENUM.USE_STORAGE_RESOURCES) && MasterStorageUtils.ConsumePlayersStorageItems(m_Caster as PlayerMobile, m_Info.Reagents, m_Info.Amounts))
				return true;
			//daat99 OWLTR end - use SpellCastersKey
			return false;
		}

		public virtual double GetInscribeSkill(Mobile m)
		{
			// There is no chance to gain
			// m.CheckSkill( SkillName.Inscribe, 0.0, 120.0 );
			return m.Skills[SkillName.Inscribe].Value;
		}

		public virtual int GetInscribeFixed(Mobile m)
		{
			// There is no chance to gain
			// m.CheckSkill( SkillName.Inscribe, 0.0, 120.0 );
			return m.Skills[SkillName.Inscribe].Fixed;
		}

		public virtual int GetDamageFixed(Mobile m)
		{
			//m.CheckSkill( DamageSkill, 0.0, m.Skills[DamageSkill].Cap );
			return m.Skills[this.DamageSkill].Fixed;
		}

		public virtual double GetDamageSkill(Mobile m)
		{
			//m.CheckSkill( DamageSkill, 0.0, m.Skills[DamageSkill].Cap );
			return m.Skills[this.DamageSkill].Value;
		}

		public virtual double GetResistSkill(Mobile m)
		{
			return m.Skills[SkillName.MagicResist].Value;
		}

		public virtual double GetDamageScalar(Mobile target)
		{
			double scalar = 1.0;

			if (!Core.AOS)	//EvalInt stuff for AoS is handled elsewhere
			{
				double casterEI = this.m_Caster.Skills[this.DamageSkill].Value;
				double targetRS = target.Skills[SkillName.MagicResist].Value;

				/*
				if( Core.AOS )
				targetRS = 0;
				*/

				//m_Caster.CheckSkill( DamageSkill, 0.0, 120.0 );

				if (casterEI > targetRS)
					scalar = (1.0 + ((casterEI - targetRS) / 500.0));
				else
					scalar = (1.0 + ((casterEI - targetRS) / 200.0));

				// magery damage bonus, -25% at 0 skill, +0% at 100 skill, +5% at 120 skill
				scalar += (this.m_Caster.Skills[this.CastSkill].Value - 100.0) / 400.0;

				if (!target.Player && !target.Body.IsHuman /*&& !Core.AOS*/)
					scalar *= 2.0; // Double magery damage to monsters/animals if not AOS
			}

			if (target is BaseCreature)
				((BaseCreature)target).AlterDamageScalarFrom(this.m_Caster, ref scalar);

			if (this.m_Caster is BaseCreature)
				((BaseCreature)this.m_Caster).AlterDamageScalarTo(target, ref scalar);

			if (Core.SE)
				scalar *= this.GetSlayerDamageScalar(target);

			target.Region.SpellDamageScalar(this.m_Caster, target, ref scalar);

			if (Evasion.CheckSpellEvasion(target))	//Only single target spells an be evaded
				scalar = 0;

			return scalar;
		}

		public virtual double GetSlayerDamageScalar(Mobile defender)
		{
			Spellbook atkBook = Spellbook.FindEquippedSpellbook(this.m_Caster);

			double scalar = 1.0;
			if (atkBook != null)
			{
				SlayerEntry atkSlayer = SlayerGroup.GetEntryByName(atkBook.Slayer);
				SlayerEntry atkSlayer2 = SlayerGroup.GetEntryByName(atkBook.Slayer2);

				if (atkSlayer != null && atkSlayer.Slays(defender) || atkSlayer2 != null && atkSlayer2.Slays(defender))
				{
					defender.FixedEffect(0x37B9, 10, 5);	//TODO: Confirm this displays on OSIs
					scalar = 2.0;
				}

				TransformContext context = TransformationSpellHelper.GetContext(defender);

				if ((atkBook.Slayer == SlayerName.Silver || atkBook.Slayer2 == SlayerName.Silver) && context != null && context.Type != typeof(HorrificBeastSpell))
					scalar += .25; // Every necromancer transformation other than horrific beast take an additional 25% damage

				if (scalar != 1.0)
					return scalar;
			}

			ISlayer defISlayer = Spellbook.FindEquippedSpellbook(defender);

			if (defISlayer == null)
				defISlayer = defender.Weapon as ISlayer;

			if (defISlayer != null)
			{
				SlayerEntry defSlayer = SlayerGroup.GetEntryByName(defISlayer.Slayer);
				SlayerEntry defSlayer2 = SlayerGroup.GetEntryByName(defISlayer.Slayer2);

				if (defSlayer != null && defSlayer.Group.OppositionSuperSlays(this.m_Caster) || defSlayer2 != null && defSlayer2.Group.OppositionSuperSlays(this.m_Caster))
					scalar = 2.0;
			}

			return scalar;
		}

		public virtual void DoFizzle()
		{
			this.m_Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, 502632); // The spell fizzles.

			if (this.m_Caster.Player)
			{
				if (Core.AOS)
					this.m_Caster.FixedParticles(0x3735, 1, 30, 9503, EffectLayer.Waist);
				else
					this.m_Caster.FixedEffect(0x3735, 6, 30);

				this.m_Caster.PlaySound(0x5C);
			}
		}

		private CastTimer m_CastTimer;
		private AnimTimer m_AnimTimer;

		public void Disturb(DisturbType type)
		{
			this.Disturb(type, true, false);
		}

		public virtual bool CheckDisturb(DisturbType type, bool firstCircle, bool resistable)
		{
			if (resistable && this.m_Scroll is BaseWand)
				return false;

			return true;
		}

		public void Disturb(DisturbType type, bool firstCircle, bool resistable)
		{
			if (!this.CheckDisturb(type, firstCircle, resistable))
				return;

			if (this.m_State == SpellState.Casting)
			{
				if (!firstCircle && !Core.AOS && this is MagerySpell && ((MagerySpell)this).Circle == SpellCircle.First)
					return;

				this.m_State = SpellState.None;
				this.m_Caster.Spell = null;

				this.OnDisturb(type, true);

				if (this.m_CastTimer != null)
					this.m_CastTimer.Stop();

				if (this.m_AnimTimer != null)
					this.m_AnimTimer.Stop();

				if (Core.AOS && this.m_Caster.Player && type == DisturbType.Hurt)
					this.DoHurtFizzle();

				m_Caster.NextSpellTime = Core.TickCount + (int)GetDisturbRecovery().TotalMilliseconds;
			}
			else if (this.m_State == SpellState.Sequencing)
			{
				if (!firstCircle && !Core.AOS && this is MagerySpell && ((MagerySpell)this).Circle == SpellCircle.First)
					return;

				this.m_State = SpellState.None;
				this.m_Caster.Spell = null;

				this.OnDisturb(type, false);

				Targeting.Target.Cancel(this.m_Caster);

				if (Core.AOS && this.m_Caster.Player && type == DisturbType.Hurt)
					this.DoHurtFizzle();
			}
		}

		public virtual void DoHurtFizzle()
		{
			this.m_Caster.FixedEffect(0x3735, 6, 30);
			this.m_Caster.PlaySound(0x5C);
		}

		public virtual void OnDisturb(DisturbType type, bool message)
		{
			if (message)
				this.m_Caster.SendLocalizedMessage(500641); // Your concentration is disturbed, thus ruining thy spell.
		}

		public virtual bool CheckCast()
		{
			return true;
		}

		public virtual void SayMantra()
		{
			if (this.m_Scroll is BaseWand)
				return;

			if (this.m_Info.Mantra != null && this.m_Info.Mantra.Length > 0 && this.m_Caster.Player)
				this.m_Caster.PublicOverheadMessage(MessageType.Spell, this.m_Caster.SpeechHue, true, this.m_Info.Mantra, false);
		}

		public virtual bool BlockedByHorrificBeast
		{
			get
			{
				return true;
			}
		}
		public virtual bool BlockedByAnimalForm
		{
			get
			{
				return true;
			}
		}
		public virtual bool BlocksMovement
		{
			get
			{
				return true;
			}
		}

		public virtual bool CheckNextSpellTime
		{
			get
			{
				return !(this.m_Scroll is BaseWand);
			}
		}

		public bool Cast()
		{
			this.m_StartCastTime = DateTime.UtcNow;

			if (Core.AOS && this.m_Caster.Spell is Spell && ((Spell)this.m_Caster.Spell).State == SpellState.Sequencing)
				((Spell)this.m_Caster.Spell).Disturb(DisturbType.NewCast);

			if (!this.m_Caster.CheckAlive())
			{
				return false;
			}
			/*else if (this.m_Caster is PlayerMobile && ((PlayerMobile)this.m_Caster).Peaced)
			{
				this.m_Caster.SendLocalizedMessage(1072060); // You cannot cast a spell while calmed.
			}
			*/
			else if (this.m_Scroll is BaseWand && this.m_Caster.Spell != null && this.m_Caster.Spell.IsCasting)
			{
				this.m_Caster.SendLocalizedMessage(502643); // You can not cast a spell while frozen.
			}
			else if (this.m_Caster.Spell != null && this.m_Caster.Spell.IsCasting)
			{
				this.m_Caster.SendLocalizedMessage(502642); // You are already casting a spell.
			}
			else if (this.BlockedByHorrificBeast && TransformationSpellHelper.UnderTransformation(this.m_Caster, typeof(HorrificBeastSpell)) || (this.BlockedByAnimalForm && AnimalForm.UnderTransformation(this.m_Caster)))
			{
				this.m_Caster.SendLocalizedMessage(1061091); // You cannot cast that spell in this form.
			}
			else if (!(this.m_Scroll is BaseWand) && (this.m_Caster.Paralyzed || this.m_Caster.Frozen))
			{
				this.m_Caster.SendLocalizedMessage(502643); // You can not cast a spell while frozen.
			}
			else if (m_Caster.Spell != null && m_Caster.Spell.IsCasting)
			{
				this.m_Caster.SendLocalizedMessage(502644); // You have not yet recovered from casting a spell.
			}
		/**	else if (this.m_Caster is PlayerMobile && ((PlayerMobile)this.m_Caster).PeacedUntil > DateTime.Now)
			{
				this.m_Caster.SendLocalizedMessage(1072060); // You cannot cast a spell while calmed.
			}*/

						#region Dueling
			else if (this.m_Caster is PlayerMobile && ((PlayerMobile)this.m_Caster).DuelContext != null && !((PlayerMobile)this.m_Caster).DuelContext.AllowSpellCast(this.m_Caster, this))
			{
			}
			#endregion
			else if (this.m_Caster.Mana >= this.ScaleMana(this.GetMana()))
			{
				#region Stygian Abyss
				if (this.m_Caster.Race == Race.Gargoyle && this.m_Caster.Flying)
				{
					StaticTile[] tiles = this.Caster.Map.Tiles.GetStaticTiles(this.Caster.X, this.Caster.Y, true);
					ItemData itemData;
					bool cancast = true;

					for (int i = 0; i < tiles.Length && cancast; ++i)
					{
						itemData = TileData.ItemTable[tiles[i].ID & TileData.MaxItemValue];
						cancast = !(itemData.Name == "hover over");
					}

					if (!cancast)
					{
						if (this.m_Caster.IsPlayer())
						{
							this.m_Caster.SendLocalizedMessage(1113750); // You may not cast spells while flying over such precarious terrain.
							return false;
						}
						else
							this.m_Caster.SendMessage("Your staff level allows you to cast while flying over precarious terrain.");
					}
				}
				#endregion
				
				if (this.m_Caster.Spell == null && this.m_Caster.CheckSpellCast(this) && this.CheckCast() && this.m_Caster.Region.OnBeginSpellCast(this.m_Caster, this))
				{
					this.m_State = SpellState.Casting;
					this.m_Caster.Spell = this;

					if (!(this.m_Scroll is BaseWand) && this.RevealOnCast)
						this.m_Caster.RevealingAction();

					this.SayMantra();

					TimeSpan castDelay = this.GetCastDelay();

					if (this.ShowHandMovement && (this.m_Caster.Body.IsHuman || (this.m_Caster.Player && this.m_Caster.Body.IsMonster)))
					{
						int count = (int)Math.Ceiling(castDelay.TotalSeconds / AnimateDelay.TotalSeconds);

						if (count != 0)
						{
							this.m_AnimTimer = new AnimTimer(this, count);
							this.m_AnimTimer.Start();
						}

						if (this.m_Info.LeftHandEffect > 0)
							this.Caster.FixedParticles(0, 10, 5, this.m_Info.LeftHandEffect, EffectLayer.LeftHand);

						if (this.m_Info.RightHandEffect > 0)
							this.Caster.FixedParticles(0, 10, 5, this.m_Info.RightHandEffect, EffectLayer.RightHand);
					}

					if (this.ClearHandsOnCast)
						this.m_Caster.ClearHands();

					if (Core.ML)
						WeaponAbility.ClearCurrentAbility(this.m_Caster);

					this.m_CastTimer = new CastTimer(this, castDelay);
					//this.m_CastTimer.Start();

					this.OnBeginCast();

					if (castDelay > TimeSpan.Zero)
					{
						this.m_CastTimer.Start();
					}
					else
					{
						this.m_CastTimer.Tick();
					}

					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				this.m_Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 502625); // Insufficient mana
			}

			return false;
		}

		public abstract void OnCast();

		public virtual void OnBeginCast()
		{
		}

		public virtual void GetCastSkills(out double min, out double max)
		{
			min = max = 0;	//Intended but not required for overriding.
		}

		public virtual bool CheckFizzle()
		{
			if (this.m_Scroll is BaseWand)
				return true;

			double minSkill, maxSkill;

			this.GetCastSkills(out minSkill, out maxSkill);

			if (this.DamageSkill != this.CastSkill)
				this.Caster.CheckSkill(this.DamageSkill, 0.0, this.Caster.Skills[this.DamageSkill].Cap);

			return this.Caster.CheckSkill(this.CastSkill, minSkill, maxSkill);
		}

		public abstract int GetMana();

		public virtual int ScaleMana(int mana)
		{
			double scalar = 1.0;

			if (!Necromancy.MindRotSpell.GetMindRotScalar(this.Caster, ref scalar))
				scalar = 1.0;

			// Lower Mana Cost = 40%
			int lmc = AosAttributes.GetValue(this.m_Caster, AosAttribute.LowerManaCost);
			if (lmc > 40)
				lmc = 40;

			scalar -= (double)lmc / 100;

			return (int)(mana * scalar);
		}

		public virtual TimeSpan GetDisturbRecovery()
		{
			if (Core.AOS)
				return TimeSpan.Zero;

			double delay = 1.0 - Math.Sqrt((DateTime.Now - this.m_StartCastTime).TotalSeconds / this.GetCastDelay().TotalSeconds);

			if (delay < 0.2)
				delay = 0.2;

			return TimeSpan.FromSeconds(delay);
		}

		public virtual int CastRecoveryBase
		{
			get
			{
				return 6;
			}
		}
		public virtual int CastRecoveryFastScalar
		{
			get
			{
				return 1;
			}
		}
		public virtual int CastRecoveryPerSecond
		{
			get
			{
				return 4;
			}
		}
		public virtual int CastRecoveryMinimum
		{
			get
			{
				return 0;
			}
		}

		public virtual TimeSpan GetCastRecovery()
		{
			if (!Core.AOS)
				return NextSpellDelay;

			int fcr = AosAttributes.GetValue(this.m_Caster, AosAttribute.CastRecovery);

			fcr -= ThunderstormSpell.GetCastRecoveryMalus(this.m_Caster);

			int fcrDelay = -(this.CastRecoveryFastScalar * fcr);

			int delay = this.CastRecoveryBase + fcrDelay;

			if (delay < this.CastRecoveryMinimum)
				delay = this.CastRecoveryMinimum;

			return TimeSpan.FromSeconds((double)delay / this.CastRecoveryPerSecond);
		}

		public abstract TimeSpan CastDelayBase { get; }

		public virtual double CastDelayFastScalar
		{
			get
			{
				return 1;
			}
		}
		public virtual double CastDelaySecondsPerTick
		{
			get
			{
				return 0.25;
			}
		}
		public virtual TimeSpan CastDelayMinimum
		{
			get
			{
				return TimeSpan.FromSeconds(0.25);
			}
		}

		//public virtual int CastDelayBase{ get{ return 3; } }
		//public virtual int CastDelayFastScalar{ get{ return 1; } }
		//public virtual int CastDelayPerSecond{ get{ return 4; } }
		//public virtual int CastDelayMinimum{ get{ return 1; } }

		public virtual TimeSpan GetCastDelay()
		{
			if (this.m_Scroll is BaseWand)
				return Core.ML ? this.CastDelayBase : TimeSpan.Zero; // TODO: Should FC apply to wands?

			// Faster casting cap of 2 (if not using the protection spell) 
			// Faster casting cap of 0 (if using the protection spell) 
			// Paladin spells are subject to a faster casting cap of 4 
			// Paladins with magery of 70.0 or above are subject to a faster casting cap of 2 
			int fcMax = 4;

			if (this.CastSkill == SkillName.Magery || this.CastSkill == SkillName.Necromancy || (this.CastSkill == SkillName.Chivalry && this.m_Caster.Skills[SkillName.Magery].Value >= 70.0))
				fcMax = 2;

			int fc = AosAttributes.GetValue(this.m_Caster, AosAttribute.CastSpeed);

			if (fc > fcMax)
				fc = fcMax;

			if (ProtectionSpell.Registry.Contains(this.m_Caster))
				fc -= 2;

			if (EssenceOfWindSpell.IsDebuffed(this.m_Caster))
				fc -= EssenceOfWindSpell.GetFCMalus(this.m_Caster);

			TimeSpan baseDelay = this.CastDelayBase;

			TimeSpan fcDelay = TimeSpan.FromSeconds(-(this.CastDelayFastScalar * fc * this.CastDelaySecondsPerTick));

			//int delay = CastDelayBase + circleDelay + fcDelay;
			TimeSpan delay = baseDelay + fcDelay;

			if (delay < this.CastDelayMinimum)
				delay = this.CastDelayMinimum;

			#region Mondain's Legacy
			if (DreadHorn.IsUnderInfluence(this.m_Caster))
				delay.Add(delay);
			#endregion

			//return TimeSpan.FromSeconds( (double)delay / CastDelayPerSecond );
			return delay;
		}

		public virtual void FinishSequence()
		{
			this.m_State = SpellState.None;

			if (this.m_Caster.Spell == this)
				this.m_Caster.Spell = null;
		}

		public virtual int ComputeKarmaAward()
		{
			return 0;
		}

		public virtual bool CheckSequence()
		{
			int mana = this.ScaleMana(this.GetMana());

			if (this.m_Caster.Deleted || !this.m_Caster.Alive || this.m_Caster.Spell != this || this.m_State != SpellState.Sequencing)
			{
				this.DoFizzle();
			}
			else if (this.m_Scroll != null && !(this.m_Scroll is Runebook) && (this.m_Scroll.Amount <= 0 || this.m_Scroll.Deleted || this.m_Scroll.RootParent != this.m_Caster || (this.m_Scroll is BaseWand && (((BaseWand)this.m_Scroll).Charges <= 0 || this.m_Scroll.Parent != this.m_Caster))))
			{
				this.DoFizzle();
			}
			else if (!this.ConsumeReagents())
			{
				this.m_Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 502630); // More reagents are needed for this spell.
			}
			else if (this.m_Caster.Mana < mana)
			{
				this.m_Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 502625); // Insufficient mana for this spell.
			}
			else if (Core.AOS && (this.m_Caster.Frozen || this.m_Caster.Paralyzed))
			{
				this.m_Caster.SendLocalizedMessage(502646); // You cannot cast a spell while frozen.
				this.DoFizzle();
			}
			else if (this.m_Caster is PlayerMobile && ((PlayerMobile)this.m_Caster).PeacedUntil > DateTime.UtcNow)
			{
				this.m_Caster.SendLocalizedMessage(1072060); // You cannot cast a spell while calmed.
				this.DoFizzle();
			}
			else if (this.CheckFizzle())
			{
				this.m_Caster.Mana -= mana;

				if (this.m_Scroll is SpellScroll)
					this.m_Scroll.Consume();
				#region SA
				else if (this.m_Scroll is SpellStone)
				{
					// The SpellScroll check above isn't removing the SpellStones for some reason.
					this.m_Scroll.Delete();
				}
				#endregion
				else if (this.m_Scroll is BaseWand)
					((BaseWand)this.m_Scroll).ConsumeCharge(this.m_Caster);

				if (this.m_Scroll is BaseWand)
				{
					bool m = this.m_Scroll.Movable;

					this.m_Scroll.Movable = false;

					if (this.ClearHandsOnCast)
						this.m_Caster.ClearHands();

					this.m_Scroll.Movable = m;
				}
				else
				{
					if (this.ClearHandsOnCast)
						this.m_Caster.ClearHands();
				}

				int karma = this.ComputeKarmaAward();

				if (karma != 0)
					Misc.Titles.AwardKarma(this.Caster, karma, true);

				if (TransformationSpellHelper.UnderTransformation(this.m_Caster, typeof(VampiricEmbraceSpell)))
				{
					bool garlic = false;

					for (int i = 0; !garlic && i < this.m_Info.Reagents.Length; ++i)
						garlic = (this.m_Info.Reagents[i] == Reagent.Garlic);

					if (garlic)
					{
						this.m_Caster.SendLocalizedMessage(1061651); // The garlic burns you!
						AOS.Damage(this.m_Caster, Utility.RandomMinMax(17, 23), 100, 0, 0, 0, 0);
					}
				}

				return true;
			}
			else
			{
				this.DoFizzle();
			}

			return false;
		}

		public bool CheckBSequence(Mobile target)
		{
			return this.CheckBSequence(target, false);
		}

		public bool CheckBSequence(Mobile target, bool allowDead)
		{
			if (!target.Alive && !allowDead)
			{
				this.m_Caster.SendLocalizedMessage(501857); // This spell won't work on that!
				return false;
			}
			else if (this.Caster.CanBeBeneficial(target, true, allowDead) && this.CheckSequence())
			{
				this.Caster.DoBeneficial(target);
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool CheckHSequence(Mobile target)
		{
			if (!target.Alive)
			{
				this.m_Caster.SendLocalizedMessage(501857); // This spell won't work on that!
				return false;
			}
			else if (this.Caster.CanBeHarmful(target) && this.CheckSequence())
			{
				this.Caster.DoHarmful(target);
				return true;
			}
			else
			{
				return false;
			}
		}

		private class AnimTimer : Timer
		{
			private readonly Spell m_Spell;

			public AnimTimer(Spell spell, int count)
				: base(TimeSpan.Zero, AnimateDelay, count)
			{
				this.m_Spell = spell;

				this.Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				if (this.m_Spell.State != SpellState.Casting || this.m_Spell.m_Caster.Spell != this.m_Spell)
				{
					this.Stop();
					return;
				}

				if (!this.m_Spell.Caster.Mounted && this.m_Spell.m_Info.Action >= 0)
				{
					if (this.m_Spell.Caster.Body.IsHuman)
						this.m_Spell.Caster.Animate(this.m_Spell.m_Info.Action, 7, 1, true, false, 0);
					else if (this.m_Spell.Caster.Player && this.m_Spell.Caster.Body.IsMonster)
						this.m_Spell.Caster.Animate(12, 7, 1, true, false, 0);
				}

				if (!this.Running)
					this.m_Spell.m_AnimTimer = null;
			}
		}

		private class CastTimer : Timer
		{
			private readonly Spell m_Spell;

			public CastTimer(Spell spell, TimeSpan castDelay)
				: base(castDelay)
			{
				this.m_Spell = spell;

				this.Priority = TimerPriority.TwentyFiveMS;
			}

			protected override void OnTick()
			{
				if (this.m_Spell == null || this.m_Spell.m_Caster == null)
				{
					return;
				}
				else if (this.m_Spell.m_State == SpellState.Casting && this.m_Spell.m_Caster.Spell == this.m_Spell)
				{
					this.m_Spell.m_State = SpellState.Sequencing;
					this.m_Spell.m_CastTimer = null;
					this.m_Spell.m_Caster.OnSpellCast(this.m_Spell);
					if (this.m_Spell.m_Caster.Region != null)
						this.m_Spell.m_Caster.Region.OnSpellCast(this.m_Spell.m_Caster, this.m_Spell);
					m_Spell.m_Caster.NextSpellTime = Core.TickCount + (int)m_Spell.GetCastRecovery().TotalMilliseconds;

					Target originalTarget = this.m_Spell.m_Caster.Target;

					this.m_Spell.OnCast();

					if (this.m_Spell.m_Caster.Player && this.m_Spell.m_Caster.Target != originalTarget && this.m_Spell.Caster.Target != null)
						this.m_Spell.m_Caster.Target.BeginTimeout(this.m_Spell.m_Caster, TimeSpan.FromSeconds(30.0));

					this.m_Spell.m_CastTimer = null;
				}
			}

			public void Tick()
			{
				this.OnTick();
			}
		}
	}
}