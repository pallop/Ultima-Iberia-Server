using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Spells;
using Server.Network;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName("guard corpse")]
    public class NewMageGuard : BaseGoodGuard
    {
        private DateTime m_NextCastTime;
        [Constructable]
        public NewMageGuard()
            : base(AIType.AI_Mage)
        {
            SetStr(40, 100);
            SetDex(26, 60);
            SetInt(26, 150);

            SetHits(60, 80);
            SetMana(150);

            SetDamage(5, 10);
          
            VirtualArmor = 16;

            if (Utility.RandomBool())
            {
                new Horse().Rider = this;
            }
                
            SetResistance(ResistanceType.Physical, 10, 20);
            SetResistance(ResistanceType.Fire, 30, 25);
            SetResistance(ResistanceType.Cold, 30, 20);
            SetResistance(ResistanceType.Poison, 30, 30);

            SetSkill(SkillName.MagicResist, 80.0, 120.0);
			SetSkill(SkillName.DetectHidden, 50.0, 100.0);
            SetSkill(SkillName.Tactics, 80.0, 120.0);
            SetSkill(SkillName.Wrestling, 80.0, 120.0);
            SetSkill(SkillName.EvalInt, 80.1, 120.0);
            SetSkill(SkillName.Magery, 80.1, 120.0);

            Fame = 1000;
            Karma = 10000;

            AddItem(new BagOfReagents(20));
            AddItem(new Cloak(0x2A));
            AddItem(new Spellbook((ulong)0x382A8C38));
            AddItem(new Robe(0x2A));
            AddItem(new Sandals(0x2A));
            AddItem(new WizardsHat(0x2A));
            AddSpellDefense( typeof(Spells.First.HealSpell) );

            for(int i=0;i<5;i++)
            {
                switch (Utility.Random(15))
                {
                    case 0: AddSpellAttack(typeof(Spells.First.MagicArrowSpell)); break;
                    case 1: AddSpellAttack(typeof(Spells.First.WeakenSpell)); break;
                    case 2: AddSpellAttack(typeof(Spells.Sixth.EnergyBoltSpell)); break;
                    case 3: AddSpellAttack(typeof(Spells.Fifth.MindBlastSpell)); break;
                    case 4: AddSpellAttack(typeof(Spells.Sixth.DispelSpell)); break;
                    case 5: AddSpellAttack(typeof(Spells.Third.FireballSpell)); break;
                    case 6: AddSpellAttack(typeof(Spells.Seventh.ManaVampireSpell)); break;
                    case 7: AddSpellAttack(typeof(Spells.Seventh.FlameStrikeSpell)); break;
                    case 8: AddSpellDefense(typeof(Spells.Fourth.GreaterHealSpell)); break;
                    case 9: AddSpellDefense(typeof(Spells.Fourth.GreaterHealSpell)); break;
                    case 10: AddSpellDefense(typeof(Spells.Second.CunningSpell)); break;
                    case 11: AddSpellDefense(typeof(Spells.Second.ProtectionSpell)); break;
                    case 12: AddSpellDefense(typeof(Spells.First.ReactiveArmorSpell)); break;
                    case 13: AddSpellDefense(typeof(Spells.Third.BlessSpell)); break;
                    case 14: AddSpellDefense(typeof(Spells.Fifth.MagicReflectSpell)); break;
                }
            }

        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.Average);
        }

	public override bool OnBeforeDeath()
	{
	    IMount mount = this.Mount;

            if ( mount != null )
	    mount.Rider = null;

	    if ( mount is Mobile )
	    ((Mobile)mount).Kill();

	    return base.OnBeforeDeath();
	}

        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public NewMageGuard(Serial serial)
            : base(serial)
        {
        }

        public override void OnGotMeleeAttack(Mobile d)
        {
            if (0.3 >= Utility.RandomDouble())
                this.Say(500131);

            if (d is BaseCreature)
            {
                if ((d as BaseCreature).IsDispellable)
                {
                    if (DateTime.Now >= m_NextCastTime)
                    {
                        this.Say("An Ort");
                        Effects.SendLocationParticles(EffectItem.Create(d.Location, d.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                        Effects.PlaySound(d, d.Map, 0x201);
                        m_NextCastTime = DateTime.Now + TimeSpan.FromSeconds(5.0);
                        if (this.Combatant == d)
                            this.Combatant = null;
                        d.Delete();
                    }
                }

                BaseCreature t = (BaseCreature)d;
                if (t.Controlled && !(t as BaseCreature).IsDispellable)
                {
                    if (0.5 >= Utility.RandomDouble())
                    {
                        t.Combatant = null;
                        t.Warmode = false;
                        t.Pacify(this, DateTime.Now + TimeSpan.FromSeconds(20.0));

                        if (this.Combatant == t)
                        {
                            this.Provoke(this, t.ControlMaster, true);
                        }
                    }
                }
            }
        }

        public override void OnDamagedBySpell(Mobile d)
        {
            if (0.3 >= Utility.RandomDouble())
                this.Say("Tus hechizos no te ayudaran.");

            if (d is BaseCreature)
            {
                if ((d as BaseCreature).IsDispellable)
                {
                    if (DateTime.Now >= m_NextCastTime)
                    {
                        this.Say("An Ort");
                        Effects.SendLocationParticles(EffectItem.Create(d.Location, d.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                        Effects.PlaySound(d, d.Map, 0x201);
                        m_NextCastTime = DateTime.Now + TimeSpan.FromSeconds(5.0);
                        if (this.Combatant == d)
                            this.Combatant = null;
                        d.Delete();
                    }
                }

                BaseCreature t = (BaseCreature)d;
                if (t.Controlled && !(t as BaseCreature).IsDispellable)
                {
                    if (0.5 >= Utility.RandomDouble())
                    {
                        t.Combatant = null;
                        t.Warmode = false;
                        t.Pacify(this, DateTime.Now + TimeSpan.FromSeconds(20.0));

                        if (this.Combatant == t)
                        {
                            this.Provoke(this, t.ControlMaster, true);
                        }
                    }
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
