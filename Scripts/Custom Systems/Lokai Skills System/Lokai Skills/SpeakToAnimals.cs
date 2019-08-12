/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections.Generic;
using Server.Targeting;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Multis;

namespace Server.LokaiSkillHandlers
{
	public class SpeakToAnimals
	{
		public static void Initialize()
		{
            LokaiSkillInfo.Table[(int)LokaiSkillName.SpeakToAnimals].Callback = new LokaiSkillUseCallback(OnUse);
		}

		public static TimeSpan OnUse( Mobile from )
		{
            if (from.NextSkillTime > Core.TickCount)
            {
                double time = (double)(from.NextSkillTime - Core.TickCount);
                from.SendMessage("You must wait another {0} seconds before using this lokaiSkill.", time.ToString("F1"));
                return TimeSpan.FromSeconds(time);
            }

            LokaiSkill lokaiSkill = (LokaiSkillUtilities.XMLGetSkills(from)).SpeakToAnimals;
            List<BaseCreature> animals = new List<BaseCreature>();

            int Max = from.FollowersMax - from.Followers;
            int Cur = 0;

            foreach (Mobile mob in from.GetMobilesInRange(10))
            {
                if (mob is BaseCreature)
                {
                    BaseCreature creature = mob as BaseCreature;
                    if (creature.AI == AIType.AI_Animal && AllowPackAnimal(creature, lokaiSkill) &&
                        !creature.Controlled && creature.Combatant != from)
                    {
                        if (Cur >= Max) break;
                        Cur++;
                        animals.Add(creature);
                    }
                }
            }
            if (animals.Count <= 0)
            {
                from.SendMessage("You are unable to find any animals nearby with which you can speak.");
                from.NextSkillTime = Core.TickCount + (int)TimeSpan.FromSeconds(4.0).TotalSeconds;
            }
            else
            {
                SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, lokaiSkill, 0.0, 100.0);
                if (rating >= SuccessRating.PartialSuccess)
                {
                    int count = 3;
                    switch (rating)
                    {
                        case SuccessRating.PartialSuccess: count++; break;
                        case SuccessRating.Success: count += 3; break;
                        case SuccessRating.CompleteSuccess: count += 6; break;
                        case SuccessRating.ExceptionalSuccess: count += 9; break;
                        case SuccessRating.TooEasy: count += 12; break;
                    }
                    from.PublicOverheadMessage(MessageType.Emote, 0x47, true, "** Begins to call to the animals. **");
                    from.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
                    new InternalTimer(from, count, count + 5, animals).Start();
                }
                else
                    from.SendMessage("You fail to speak to the surrounding animals.");
            }
			return TimeSpan.FromSeconds( 3.0 );
        }

        private static bool AllowPackAnimal(BaseCreature animal, LokaiSkill lokaiSkill)
        {
            double skil = lokaiSkill.Value;
            switch (animal.PackInstinct)
            {
                case PackInstinct.Arachnid: return (skil > 40.0 + (Utility.RandomDouble() * 10));
                case PackInstinct.Bear: return (skil > 10.0 + (Utility.RandomDouble() * 30));
                case PackInstinct.Bull: return (skil > 0.0 + (Utility.RandomDouble() * 20));
                case PackInstinct.Canine: return (skil > 0.0 + (Utility.RandomDouble() * 10));
                case PackInstinct.Daemon: return (skil > 70.0 + (Utility.RandomDouble() * 20));
                case PackInstinct.Equine: return (skil > 10.0 + (Utility.RandomDouble() * 40));
                case PackInstinct.Feline: return (skil > 0.0 + (Utility.RandomDouble() * 30));
                case PackInstinct.Ostard: return (skil > 20.0 + (Utility.RandomDouble() * 10));
                case PackInstinct.None: return true;
            }
            return false;
        }

        private class InternalTimer : Timer
        {
            private SlotItem item;
            private Mobile m_From;
            private List<BaseCreature> m_Animals;
            private int m_MaxCount, m_Count, m_Slots, m_CreatureMax, m_Controlled;

            public InternalTimer(Mobile from, int count, int creaturemax, List<BaseCreature> animals)
                : base(TimeSpan.FromSeconds(2.0), TimeSpan.FromSeconds(3.0), count)
            {
                Priority = TimerPriority.TwoFiftyMS;

                m_From = from;
                m_MaxCount = count;
                m_CreatureMax = creaturemax;
                m_Slots = from.FollowersMax;
                from.FollowersMax = Math.Max(from.FollowersMax, creaturemax);
                m_Animals = animals;

                item = new SlotItem(m_From, m_Slots);
                item.MoveToWorld(m_From.Location);
            }

            protected override void OnTick()
            {
                m_Count++;
                if (m_Count < m_MaxCount)
                {
                    switch (Utility.Random(6))
                    {
                        case 0: m_From.PublicOverheadMessage(MessageType.Emote, 0x47, true, "Hear me, animal friends!"); break;
                        case 1: m_From.PublicOverheadMessage(MessageType.Emote, 0x47, true, "Come to my aid."); break;
                        case 2: m_From.PublicOverheadMessage(MessageType.Emote, 0x47, true, "I am a friend to the animals."); break;
                        case 3: m_From.PublicOverheadMessage(MessageType.Emote, 0x47, true, "I call to you my friends."); break;
                        case 4: m_From.PublicOverheadMessage(MessageType.Emote, 0x47, true, "Hear me, my friends!"); break;
                        case 5: m_From.PublicOverheadMessage(MessageType.Emote, 0x47, true, "** whistles softly **"); break;
                    }

                    LokaiSkill lokaiSkill = LokaiSkillUtilities.XMLGetSkills(m_From).SpeakToAnimals;

                    int Max = m_From.FollowersMax - m_From.Followers;
                    int Cur = 0;

                    foreach (Mobile mob in m_From.GetMobilesInRange(10))
                    {
                        if (mob is BaseCreature)
                        {
                            BaseCreature creature = mob as BaseCreature;
                            if (m_Animals.Contains(creature) || creature.Controlled)
                            {
                                continue;
                            }
                            if (creature.AI == AIType.AI_Animal && AllowPackAnimal(creature, lokaiSkill) && creature.Combatant != m_From)
                            {
                                if (Cur >= Max) break;
                                Cur++;
                                m_Animals.Add(creature);
                            }
                        }
                    }

                    foreach (BaseCreature animal in m_Animals)
                    {
                        if (m_Controlled >= m_CreatureMax) break;
                        if (Utility.RandomBool() && !animal.Controlled)
                        {
                            m_Controlled++;
                            animal.Owners.Add(m_From);
                            animal.SetControlMaster(m_From);
                            animal.ControlOrder = OrderType.Guard;
                        }
                    }
                }
                else
                {
                    foreach (BaseCreature animal in m_Animals)
                    {
                        animal.Controlled = false;
                        animal.Owners.Remove(m_From);
                        animal.SetControlMaster(null);
                    }
                    m_From.FollowersMax = m_Slots;
                    m_From.SendMessage("The animals have stopped listening to you.");
                    item.Delete();
                }
                m_From.NextSkillTime = Core.TickCount + (int)TimeSpan.FromSeconds(3.0 * (m_MaxCount - m_Count)).TotalSeconds;
            }
        }

        private class SlotItem : Item
        {
            private Mobile m_Owner;
            private int m_Slots;
            [Constructable]
            public SlotItem(Mobile owner, int slots)
                : base(0xD17)
            {
                m_Owner = owner;
                m_Slots = slots;
                Visible = false;
            }
            public SlotItem(Serial serial) : base(serial) { }
            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer); writer.Write((int)0);
                writer.Write((Mobile)m_Owner);
                writer.Write((int)m_Slots);
            }
            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                int version = reader.ReadInt();
                m_Owner = reader.ReadMobile();
                m_Slots = reader.ReadInt();
                m_Owner.FollowersMax = m_Slots;
                this.Delete();
            }
        }
	}
}