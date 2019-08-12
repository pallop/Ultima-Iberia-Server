/***************************************************************************
 *   Based off the RunUO Skills system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;

namespace Server
{
    [PropertyObject]
    public class LokaiSkills
    {
        // These numbers should be 10 times the equivalent double values
        private const int STARTVALUE = 300;
        private const int LOKAISKILLCAP = 1000;
        private const int LOKAISKILLSCAP = 30000;

        private Mobile m_Owner;
        private LokaiSkill[] m_LokaiSkills;
        private int m_Total, m_Cap;
        private LokaiSkill m_Highest;
        private SuccessRating[] m_LastCheck;

        #region Lokai Skill Getters & Setters
        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill AnimalRiding { get { return this[LokaiSkillName.AnimalRiding]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Brewing { get { return this[LokaiSkillName.Brewing]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Commerce { get { return this[LokaiSkillName.Commerce]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Construction { get { return this[LokaiSkillName.Construction]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill CureDisease { get { return this[LokaiSkillName.CureDisease]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill BrickLaying { get { return this[LokaiSkillName.BrickLaying]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill DetectEvil { get { return this[LokaiSkillName.DetectEvil]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill TreeCarving { get { return this[LokaiSkillName.TreeCarving]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Skinning { get { return this[LokaiSkillName.Skinning]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Pilfering { get { return this[LokaiSkillName.Pilfering]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Butchering { get { return this[LokaiSkillName.Butchering]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Weaving { get { return this[LokaiSkillName.Weaving]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill StoneMasonry { get { return this[LokaiSkillName.StoneMasonry]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill TreePicking { get { return this[LokaiSkillName.TreePicking]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Herblore { get { return this[LokaiSkillName.Herblore]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Hypnotism { get { return this[LokaiSkillName.Hypnotism]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Cooperage { get { return this[LokaiSkillName.Cooperage]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Linguistics { get { return this[LokaiSkillName.Linguistics]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill PickPocket { get { return this[LokaiSkillName.PickPocket]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill PreyTracking { get { return this[LokaiSkillName.PreyTracking]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Sailing { get { return this[LokaiSkillName.Sailing]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Spinning { get { return this[LokaiSkillName.Spinning]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Framing { get { return this[LokaiSkillName.Framing]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill SpeakToAnimals { get { return this[LokaiSkillName.SpeakToAnimals]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Roofing { get { return this[LokaiSkillName.Roofing]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Teaching { get { return this[LokaiSkillName.Teaching]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill TreeDigging { get { return this[LokaiSkillName.TreeDigging]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Ventriloquism { get { return this[LokaiSkillName.Ventriloquism]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill Woodworking { get { return this[LokaiSkillName.Woodworking]; } set { } }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkill TreeSapping { get { return this[LokaiSkillName.TreeSapping]; } set { } }

        #endregion

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public int Cap
        {
            get { return m_Cap; }
            set { m_Cap = value; }
        }

        public int Total
        {
            get { return m_Total; }
            set { m_Total = value; }
        }

        public Mobile Owner
        {
            get { return m_Owner; }
        }

        public int Length
        {
            get { return m_LokaiSkills.Length; }
        }

        public SuccessRating LastCheck(int index)
        {
            return m_LastCheck[index];
        }

        public SuccessRating LastCheck(LokaiSkillName name)
        {
            return m_LastCheck[(int)name];
        }

        public SuccessRating[] LastLokaiSkillCheck
        {
            get { return m_LastCheck; }
            set { m_LastCheck = value; }
        }

        public LokaiSkill this[LokaiSkillName name]
        {
            get { return this[(int)name]; }
        }

        public LokaiSkill this[int LokaiSkillID]
        {
            get
            {
                if (LokaiSkillID < 0 || LokaiSkillID >= m_LokaiSkills.Length)
                    return null;

                LokaiSkill skil = m_LokaiSkills[LokaiSkillID];

                if (skil == null)
                    m_LokaiSkills[LokaiSkillID] = skil = new LokaiSkill(this, LokaiSkillInfo.Table[LokaiSkillID], 0, 1000, LokaiSkillLock.Up);

                return skil;
            }
        }

        public override string ToString()
        {
            return "...";
        }

        public static bool UseLokaiSkill(Mobile from, LokaiSkillName name)
        {
            return UseLokaiSkill(from, (int)name);
        }

        public static bool UseLokaiSkill(Mobile from, int LokaiSkillID)
        {
            if (!from.CheckAlive())
                return false;
            //else if (!from.Region.OnLokaiSkillUse(from, LokaiSkillID))
            //    return false;
            //else if (!from.AllowLokaiSkillUse((LokaiSkillName)LokaiSkillID))
            //    return false;

            if (LokaiSkillID >= 0 && LokaiSkillID < LokaiSkillInfo.Table.Length)
            {
                LokaiSkillInfo info = LokaiSkillInfo.Table[LokaiSkillID];

                if (info.ClickToUse)
                {
                    if (from.NextSkillTime <= Core.TickCount && from.Spell == null)
                    {
                        from.DisruptiveAction();

                        if (info.Callback != null)
                            from.NextSkillTime = Core.TickCount + (int)info.Callback(from).TotalSeconds;
                        else
                            from.NextSkillTime = Core.TickCount + (int)TimeSpan.FromSeconds(2.0).TotalSeconds;

                        return true;
                    }
                    else
                    {
                        from.SendSkillMessage();
                    }
                }
                else
                {
                    from.SendMessage("That skill cannot be used directly.");
                }
            }

            return false;
        }

        public LokaiSkill Highest
        {
            get
            {
                if (m_Highest == null)
                {
                    LokaiSkill highest = null;
                    int value = int.MinValue;

                    for (int i = 0; i < m_LokaiSkills.Length; ++i)
                    {
                        LokaiSkill skil = m_LokaiSkills[i];

                        if (skil != null && skil.BaseFixedPoint > value)
                        {
                            value = skil.BaseFixedPoint;
                            highest = skil;
                        }
                    }

                    if (highest == null && m_LokaiSkills.Length > 0)
                        highest = this[0];

                    m_Highest = highest;
                }

                return m_Highest;
            }
        }

        public void Serialize(GenericWriter writer)
        {
            m_Total = 0;

            writer.Write((int)0); // version

            writer.Write((int)m_Cap);
            writer.Write((int)m_LokaiSkills.Length);

            for (int i = 0; i < m_LokaiSkills.Length; ++i)
            {
                LokaiSkill skil = m_LokaiSkills[i];

                if (skil == null)
                {
                    writer.Write((byte)0xFF);
                }
                else
                {
                    skil.Serialize(writer);
                    m_Total += skil.BaseFixedPoint;
                }
            }
        }

        public LokaiSkills(Mobile owner)
        {
            m_Owner = owner;
            m_Cap = LOKAISKILLSCAP;

            LokaiSkillInfo[] info = LokaiSkillInfo.Table;

            m_LokaiSkills = new LokaiSkill[info.Length];

            for (int i = 0; i < info.Length; ++i)
                m_LokaiSkills[i] = new LokaiSkill(this, info[i], STARTVALUE, LOKAISKILLCAP, LokaiSkillLock.Up);

            m_LastCheck = new SuccessRating[LokaiSkillInfo.Table.Length];
        }

        public LokaiSkills(Mobile owner, GenericReader reader)
        {
            m_Owner = owner;

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Cap = reader.ReadInt();

                        LokaiSkillInfo[] info = LokaiSkillInfo.Table;

                        m_LokaiSkills = new LokaiSkill[info.Length];

                        int count = reader.ReadInt();

                        for (int i = 0; i < count; ++i)
                        {
                            if (i < info.Length)
                            {
                                LokaiSkill skil = new LokaiSkill(this, info[i], reader);

                                if (skil.BaseFixedPoint != 0 || skil.CapFixedPoint != LOKAISKILLCAP || skil.Lock != LokaiSkillLock.Up)
                                {
                                    m_LokaiSkills[i] = skil;
                                    m_Total += skil.BaseFixedPoint;
                                }
                            }
                            else
                            {
                                new LokaiSkill(this, null, reader);
                            }
                        }

                        break;
                    }
            }
            m_LastCheck = new SuccessRating[LokaiSkillInfo.Table.Length];
        }

        public void OnLokaiSkillChange(LokaiSkill lokaiSkill, double oldValue)
        {
            if (lokaiSkill == m_Highest)
                m_Highest = null;
            else if (m_Highest != null && lokaiSkill.BaseFixedPoint > m_Highest.BaseFixedPoint)
                m_Highest = lokaiSkill;

            double change = lokaiSkill.Value - oldValue;

            if (change != 0.0)
                m_Owner.SendMessage(string.Format("Your {0} skill has changed by {1}. It is now {2}.",
                        lokaiSkill.Name, change.ToString("F1"), lokaiSkill.Base.ToString("F1")));
            m_Owner.InvalidateProperties();
        }

        public void OnLokaiSkillChange(LokaiSkill lokaiSkill)
        {
            OnLokaiSkillChange(lokaiSkill, lokaiSkill.Value);
        }
    }
}