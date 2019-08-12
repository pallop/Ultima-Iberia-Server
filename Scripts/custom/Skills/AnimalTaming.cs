using System;
using System.Collections;
using Server;
using Server.Factions;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Spellweaving;
using Server.Engines.XmlSpawner2;

namespace Server.SkillHandlers
{
    public class AnimalTaming
    {
        private static readonly Hashtable m_BeingTamed = new Hashtable();
        private static bool m_DisableMessage;
        public static bool DisableMessage
        {
            get
            {
                return m_DisableMessage;
            }
            set
            {
                m_DisableMessage = value;
            }
        }
        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.AnimalTaming].Callback = new SkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.RevealingAction();

            m.Target = new InternalTarget();
            m.RevealingAction();

            if (!m_DisableMessage)
                m.SendLocalizedMessage(502789); // Tame which animal?

            return TimeSpan.FromHours(6.0);
        }

        public static bool CheckMastery(Mobile tamer, BaseCreature creature)
        {
            BaseCreature familiar = (BaseCreature)Spells.Necromancy.SummonFamiliarSpell.Table[tamer];

            if (familiar != null && !familiar.Deleted && familiar is DarkWolfFamiliar)
            {
                if (creature is DireWolf || creature is GreyWolf || creature is TimberWolf || creature is WhiteWolf || creature is BakeKitsune)
                    return true;
            }

            return false;
        }

        public static bool MustBeSubdued(BaseCreature bc)
        {
            if (bc.Owners.Count > 0)
            {
                return false;
            }//Checks to see if the animal has been tamed before
            return bc.SubdueBeforeTame && (bc.Hits > (bc.HitsMax / 10));
        }

        public static void ScaleStats(BaseCreature bc, double scalar)
        {
            if (bc.RawStr > 0)
                bc.RawStr = (int)Math.Max(1, bc.RawStr * scalar);

            if (bc.RawDex > 0)
                bc.RawDex = (int)Math.Max(1, bc.RawDex * scalar);

            if (bc.RawInt > 0)
                bc.RawInt = (int)Math.Max(1, bc.RawInt * scalar);

            if (bc.HitsMaxSeed > 0)
            {
                bc.HitsMaxSeed = (int)Math.Max(1, bc.HitsMaxSeed * scalar);
                bc.Hits = bc.Hits;
            }

            if (bc.StamMaxSeed > 0)
            {
                bc.StamMaxSeed = (int)Math.Max(1, bc.StamMaxSeed * scalar);
                bc.Stam = bc.Stam;
            }
        }

        public static void ScaleSkills(BaseCreature bc, double scalar)
        {
            ScaleSkills(bc, scalar, scalar);
        }

        public static void ScaleSkills(BaseCreature bc, double scalar, double capScalar)
        {
            for (int i = 0; i < bc.Skills.Length; ++i)
            {
                bc.Skills[i].Base *= scalar;

                bc.Skills[i].Cap = Math.Max(100.0, bc.Skills[i].Cap * capScalar);

                if (bc.Skills[i].Base > bc.Skills[i].Cap)
                {
                    bc.Skills[i].Cap = bc.Skills[i].Base;
                }
            }
        }

        private class InternalTarget : Target
        {
            private bool m_SetSkillTime = true;
            public InternalTarget()
                : base(Core.AOS ? 3 : 2, false, TargetFlags.None)
            {
            }

            public virtual void ResetPacify(object obj)
            {
                if (obj is BaseCreature)
                {
                    ((BaseCreature)obj).BardPacified = true;
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                if (this.m_SetSkillTime)
                    from.NextSkillTime = Core.TickCount;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                from.RevealingAction();

                if (targeted is Mobile)
                {
                    if (targeted is BaseCreature)
                    {
                        BaseCreature creature = (BaseCreature)targeted;

                        if (!creature.Tamable)
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1049655, from.NetState); // That creature cannot be tamed.
                        }
                        else if (creature.Controlled)
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502804, from.NetState); // That animal looks tame already.
                        }
                        else if (from.Female && !creature.AllowFemaleTamer)
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1049653, from.NetState); // That creature can only be tamed by males.
                        }
                        else if (!from.Female && !creature.AllowMaleTamer)
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1049652, from.NetState); // That creature can only be tamed by females.
                        }
                        else if (creature is CuSidhe && from.Race != Race.Elf)
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502801, from.NetState); // You can't tame that!
                        }
                        else if (from.Followers + creature.ControlSlots > from.FollowersMax)
                        {
                            from.SendLocalizedMessage(1049611); // You have too many followers to tame that creature.
                        }
                        else if (creature.Owners.Count >= BaseCreature.MaxOwners && !creature.Owners.Contains(from))
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1005615, from.NetState); // This animal has had too many owners and is too upset for you to tame.
                        }
                        else if (MustBeSubdued(creature))
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1054025, from.NetState); // You must subdue this creature before you can tame it!
                        }
                        else if (CheckMastery(from, creature) || from.Skills[SkillName.AnimalTaming].Value >= creature.MinTameSkill)
                        {
                            FactionWarHorse warHorse = creature as FactionWarHorse;

                            if (warHorse != null)
                            {
                                Faction faction = Faction.Find(from);

                                if (faction == null || faction != warHorse.Faction)
                                {
                                    creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1042590, from.NetState); // You cannot tame this creature.
                                    return;
                                }
                            }

                            if (m_BeingTamed.Contains(targeted))
                            {
                                creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502802, from.NetState); // Someone else is already taming this.
                            }
                            else if (creature.CanAngerOnTame && 0.95 >= Utility.RandomDouble())
                            {
                                creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502805, from.NetState); // You seem to anger the beast!
                                creature.PlaySound(creature.GetAngerSound());
                                creature.Direction = creature.GetDirectionTo(from);

                                if (creature.BardPacified && Utility.RandomDouble() > .24)
                                {
                                    Timer.DelayCall(TimeSpan.FromSeconds(2.0), new TimerStateCallback(ResetPacify), creature);
                                }
                                else
                                {
                                    creature.BardEndTime = DateTime.Now;
                                }

                                creature.BardPacified = false;

                                if (creature.AIObject != null)
                                    creature.AIObject.DoMove(creature.Direction);

                                if (from is PlayerMobile && !(((PlayerMobile)from).HonorActive || TransformationSpellHelper.UnderTransformation(from, typeof(EtherealVoyageSpell))))
                                    creature.Combatant = from;
                            }
                            else
                            {
                                m_BeingTamed[targeted] = from;

                                from.LocalOverheadMessage(MessageType.Emote, 0x59, 1010597); // You start to tame the creature.
                                from.NonlocalOverheadMessage(MessageType.Emote, 0x59, 1010598); // *begins taming a creature.*

                                new InternalTimer(from, creature, Utility.Random(3, 2)).Start();

                                this.m_SetSkillTime = false;
                            }
                        }
                        else
                        {
                            creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502806, from.NetState); // You have no chance of taming this creature.
                        }
                    }
                    else
                    {
                        ((Mobile)targeted).PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502469, from.NetState); // That being cannot be tamed.
                    }
                }
                else
                {
                    from.SendLocalizedMessage(502801); // You can't tame that!
                }
            }

            private class InternalTimer : Timer
            {
                private readonly Mobile m_Tamer;
                private readonly BaseCreature m_Creature;
                private readonly int m_MaxCount;
                private readonly DateTime m_StartTime;
                private int m_Count;
                private bool m_Paralyzed;
                public InternalTimer(Mobile tamer, BaseCreature creature, int count)
                    : base(TimeSpan.FromSeconds(3.0), TimeSpan.FromSeconds(3.0), count)
                {
                    this.m_Tamer = tamer;
                    this.m_Creature = creature;
                    this.m_MaxCount = count;
                    this.m_Paralyzed = creature.Paralyzed;
                    this.m_StartTime = DateTime.Now;
                    this.Priority = TimerPriority.TwoFiftyMS;
                }

                protected override void OnTick()
                {
                    this.m_Count++;

                    DamageEntry de = this.m_Creature.FindMostRecentDamageEntry(false);
                    bool alreadyOwned = this.m_Creature.Owners.Contains(this.m_Tamer);

                    if (!this.m_Tamer.InRange(this.m_Creature, Core.AOS ? 7 : 6))
                    {
                        m_BeingTamed.Remove(this.m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        this.m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502795, this.m_Tamer.NetState); // You are too far away to continue taming.
                        this.Stop();
                    }
                    else if (!this.m_Tamer.CheckAlive())
                    {
                        m_BeingTamed.Remove(this.m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        this.m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502796, this.m_Tamer.NetState); // You are dead, and cannot continue taming.
                        this.Stop();
                    }
                    else if (!this.m_Tamer.CanSee(this.m_Creature) || !this.m_Tamer.InLOS(this.m_Creature) || !this.CanPath())
                    {
                        m_BeingTamed.Remove(this.m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        this.m_Tamer.SendLocalizedMessage(1049654); // You do not have a clear path to the animal you are taming, and must cease your attempt.
                        this.Stop();
                    }
                    else if (!this.m_Creature.Tamable)
                    {
                        m_BeingTamed.Remove(this.m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        this.m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1049655, this.m_Tamer.NetState); // That creature cannot be tamed.
                        this.Stop();
                    }
                    else if (this.m_Creature.Controlled)
                    {
                        m_BeingTamed.Remove(this.m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        this.m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502804, this.m_Tamer.NetState); // That animal looks tame already.
                        this.Stop();
                    }
                    else if (this.m_Creature.Owners.Count >= BaseCreature.MaxOwners && !this.m_Creature.Owners.Contains(this.m_Tamer))
                    {
                        m_BeingTamed.Remove(this.m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        this.m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1005615, this.m_Tamer.NetState); // This animal has had too many owners and is too upset for you to tame.
                        this.Stop();
                    }
                    else if (MustBeSubdued(this.m_Creature))
                    {
                        m_BeingTamed.Remove(this.m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        this.m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 1054025, this.m_Tamer.NetState); // You must subdue this creature before you can tame it!
                        this.Stop();
                    }
                    else if (de != null && de.LastDamage > this.m_StartTime)
                    {
                        m_BeingTamed.Remove(this.m_Creature);
                        m_Tamer.NextSkillTime = Core.TickCount;
                        this.m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502794, this.m_Tamer.NetState); // The animal is too angry to continue taming.
                        this.Stop();
                    }
                    else if (this.m_Count < this.m_MaxCount)
                    {
                        this.m_Tamer.RevealingAction();

                        switch ( Utility.Random(3) )
                        {
                            case 0:
                                this.m_Tamer.PublicOverheadMessage(MessageType.Regular, 0x3B2, Utility.Random(502790, 4));
                                break;
                            case 1:
                                this.m_Tamer.PublicOverheadMessage(MessageType.Regular, 0x3B2, Utility.Random(1005608, 6));
                                break;
                            case 2:
                                this.m_Tamer.PublicOverheadMessage(MessageType.Regular, 0x3B2, Utility.Random(1010593, 4));
                                break;
                        }

                        if (!alreadyOwned) // Passively check animal lore for gain
                            this.m_Tamer.CheckTargetSkill(SkillName.AnimalLore, this.m_Creature, 0.0, 120.0);

                        if (this.m_Creature.Paralyzed)
                            this.m_Paralyzed = true;
                    }
                    else
                    {
                        this.m_Tamer.RevealingAction();
                        m_Tamer.NextSkillTime = Core.TickCount;
                        m_BeingTamed.Remove(this.m_Creature);

                        if (this.m_Creature.Paralyzed)
                            this.m_Paralyzed = true;

                        if (!alreadyOwned) // Passively check animal lore for gain
                            this.m_Tamer.CheckTargetSkill(SkillName.AnimalLore, this.m_Creature, 0.0, 120.0);

                        double minSkill = this.m_Creature.MinTameSkill + (this.m_Creature.Owners.Count * 6.0);

                        if (minSkill > -24.9 && CheckMastery(this.m_Tamer, this.m_Creature))
                            minSkill = -24.9; // 50% at 0.0?

                        minSkill += 24.9;

                        minSkill += XmlMobFactions.GetScaledFaction(this.m_Tamer, this.m_Creature, -25, 25, -0.001);

                        if (CheckMastery(this.m_Tamer, this.m_Creature) || alreadyOwned || this.m_Tamer.CheckTargetSkill(SkillName.AnimalTaming, this.m_Creature, minSkill - 25.0, minSkill + 25.0))
                        {
                            if (this.m_Creature.Owners.Count == 0) // First tame
                            {
                                if (this.m_Creature is GreaterDragon)
                                {
                                    ScaleSkills(this.m_Creature, 0.72, 0.90); // 72% of original skills trainable to 90%
                                    this.m_Creature.Skills[SkillName.Magery].Base = this.m_Creature.Skills[SkillName.Magery].Cap; // Greater dragons have a 90% cap reduction and 90% skill reduction on magery
                                }
                                else if (this.m_Paralyzed)
                                    ScaleSkills(this.m_Creature, 0.86); // 86% of original skills if they were paralyzed during the taming
                                else
                                    ScaleSkills(this.m_Creature, 0.90); // 90% of original skills

                                if (this.m_Creature.StatLossAfterTame)
                                    ScaleStats(this.m_Creature, 0.50);
                            }

                            if (alreadyOwned)
                            {
                                this.m_Tamer.SendLocalizedMessage(502797); // That wasn't even challenging.
                            }
                            else
                            {
                                this.m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502799, this.m_Tamer.NetState); // It seems to accept you as master.
                                this.m_Creature.Owners.Add(this.m_Tamer);
                            }

                            this.m_Creature.SetControlMaster(this.m_Tamer);
                            this.m_Creature.IsBonded = false;
                        }
                        else
                        {
                            this.m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502798, this.m_Tamer.NetState); // You fail to tame the creature.
                        }
                    }
                }

                private bool CanPath()
                {
                    IPoint3D p = this.m_Tamer as IPoint3D;

                    if (p == null)
                        return false;

                    if (this.m_Creature.InRange(new Point3D(p), 1))
                        return true;

                    MovementPath path = new MovementPath(this.m_Creature, new Point3D(p));
                    return path.Success;
                }
            }
        }
    }
}