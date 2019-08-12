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
using Server.Engines.XmlSpawner2;

namespace Server
{
    [PropertyObject]
    public class LokaiSkill
    {
        private LokaiSkills m_Owner;
        private LokaiSkillInfo m_Info;
        private ushort m_Base;
        private ushort m_Cap;
        private LokaiSkillLock m_Lock;

        public override string ToString()
        {
            return String.Format("[{0}: {1}]", Name, Base);
        }

        public LokaiSkill(LokaiSkills owner, LokaiSkillInfo info, GenericReader reader)
        {
            m_Owner = owner;
            m_Info = info;

            int version = reader.ReadByte();

            switch (version)
            {
                case 0:
                    {
                        m_Base = reader.ReadUShort();
                        m_Cap = reader.ReadUShort();
                        m_Lock = (LokaiSkillLock)reader.ReadByte();

                        break;
                    }
                case 0xFF:
                    {
                        m_Base = 0;
                        m_Cap = 1000;
                        m_Lock = LokaiSkillLock.Up;

                        break;
                    }
                default:
                    {
                        if ((version & 0xC0) == 0x00)
                        {
                            if ((version & 0x1) != 0)
                                m_Base = reader.ReadUShort();

                            if ((version & 0x2) != 0)
                                m_Cap = reader.ReadUShort();
                            else
                                m_Cap = 1000;

                            if ((version & 0x4) != 0)
                                m_Lock = (LokaiSkillLock)reader.ReadByte();
                        }

                        break;
                    }
            }

            if (m_Lock < LokaiSkillLock.Up || m_Lock > LokaiSkillLock.Locked)
            {
                Console.WriteLine("Bad Lokai Skill lock -> {0}.{1}", owner.Owner, m_Lock);
                m_Lock = LokaiSkillLock.Up;
            }
        }

        public LokaiSkill(LokaiSkills owner, LokaiSkillInfo info, int baseValue, int cap, LokaiSkillLock LokaiSkillLock)
        {
            m_Owner = owner;
            m_Info = info;
            m_Base = (ushort)baseValue;
            m_Cap = (ushort)cap;
            m_Lock = LokaiSkillLock;
        }

        public void SetLockNoRelay(LokaiSkillLock LokaiSkillLock)
        {
            if (LokaiSkillLock < LokaiSkillLock.Up || LokaiSkillLock > LokaiSkillLock.Locked)
                return;

            m_Lock = LokaiSkillLock;
        }

        public void Serialize(GenericWriter writer)
        {
            if (m_Base == 0 && m_Cap == 1000 && m_Lock == LokaiSkillLock.Up)
            {
                writer.Write((byte)0xFF); // default
            }
            else
            {
                int flags = 0x0;

                if (m_Base != 0)
                    flags |= 0x1;

                if (m_Cap != 1000)
                    flags |= 0x2;

                if (m_Lock != LokaiSkillLock.Up)
                    flags |= 0x4;

                writer.Write((byte)flags); // version

                if (m_Base != 0)
                    writer.Write((short)m_Base);

                if (m_Cap != 1000)
                    writer.Write((short)m_Cap);

                if (m_Lock != LokaiSkillLock.Up)
                    writer.Write((byte)m_Lock);
            }
        }

        public LokaiSkills Owner
        {
            get
            {
                return m_Owner;
            }
        }

        public LokaiSkillName LokaiSkillName
        {
            get
            {
                return (LokaiSkillName)m_Info.LokaiSkillID;
            }
        }

        public int LokaiSkillID
        {
            get
            {
                return m_Info.LokaiSkillID;
            }
        }

        [CommandProperty(AccessLevel.Counselor)]
        public string Name
        {
            get
            {
                return m_Info.Name;
            }
        }

        public LokaiSkillInfo Info
        {
            get
            {
                return m_Info;
            }
        }

        [CommandProperty(AccessLevel.Counselor)]
        public LokaiSkillLock Lock
        {
            get
            {
                return m_Lock;
            }
        }

        public int BaseFixedPoint
        {
            get
            {
                return m_Base;
            }
            set
            {
                if (value < 0)
                    value = 0;
                else if (value >= 0x10000)
                    value = 0xFFFF;

                ushort sv = (ushort)value;

                int oldBase = m_Base;

                if (m_Base != sv)
                {
                    m_Owner.Total = (m_Owner.Total - m_Base) + sv;

                    m_Base = sv;

                    m_Owner.OnLokaiSkillChange(this, ((double)oldBase) / 10);
                }
            }
        }

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public double Base
        {
            get
            {
                return ((double)m_Base / 10.0);
            }
            set
            {
                BaseFixedPoint = (int)(value * 10.0);
            }
        }

        public int CapFixedPoint
        {
            get
            {
                return m_Cap;
            }
            set
            {
                if (value < 0)
                    value = 0;
                else if (value >= 0x10000)
                    value = 0xFFFF;

                ushort sv = (ushort)value;

                if (m_Cap != sv)
                {
                    m_Cap = sv;

                    m_Owner.OnLokaiSkillChange(this);
                }
            }
        }

        [CommandProperty(AccessLevel.Counselor, AccessLevel.GameMaster)]
        public double Cap
        {
            get
            {
                return ((double)m_Cap / 10.0);
            }
            set
            {
                CapFixedPoint = (int)(value * 10.0);
            }
        }

        private static bool m_UseStatMods;

        public static bool UseStatMods { get { return m_UseStatMods; } set { m_UseStatMods = value; } }

        public int Fixed
        {
            get { return (int)(Value * 10); }
        }

        [CommandProperty(AccessLevel.Counselor)]
        public double Value
        {
            get
            {
                double value = this.NonRacialValue;

                //double raceBonus = m_Owner.Owner.RacialSkillBonus;

                //if (raceBonus > value)
                //    value = raceBonus;

                return value;
            }
        }

        [CommandProperty(AccessLevel.Counselor)]
        public double NonRacialValue
        {
            get
            {
                double baseValue = Base;
                //double inv = 100.0 - baseValue;

                //if (inv < 0.0) inv = 0.0;

                //inv /= 100.0;

                //double statsOffset = ((m_UseStatMods ? m_Owner.Owner.Str : m_Owner.Owner.RawStr) * m_Info.StrScale) + ((m_UseStatMods ? m_Owner.Owner.Dex : m_Owner.Owner.RawDex) * m_Info.DexScale) + ((m_UseStatMods ? m_Owner.Owner.Int : m_Owner.Owner.RawInt) * m_Info.IntScale);
                //double statTotal = m_Info.StatTotal * inv;

                //statsOffset *= inv;

                //if (statsOffset > statTotal)
                //    statsOffset = statTotal;

                double value = baseValue /*+ statsOffset*/;

                double bonusObey = 0.0, bonusNotObey = 0.0;

                ArrayList a = XmlAttach.FindAttachments(m_Owner.Owner);

                if (a != null)
                {
                    foreach (XmlAttachment x in a)
                    {
                        if (x is LokaiSkillMod && !x.Deleted)
                        {
                            LokaiSkillMod mod = x as LokaiSkillMod;
                            if (mod.Skill == this.LokaiSkillName)
                            {
                                if (mod.Relative)
                                {
                                    if (mod.ObeyCap)
                                        bonusObey += mod.Value;
                                    else
                                        bonusNotObey += mod.Value;
                                }
                                else
                                {
                                    value += mod.Value;
                                }
                            }
                        }
                    }
                }

                value += bonusNotObey;

                if (value < Cap)
                {
                    value += bonusObey;

                    if (value > Cap)
                        value = Cap;
                }

                return value;
            }
        }

        public void Update()
        {
            m_Owner.OnLokaiSkillChange(this);
        }
    }
}
