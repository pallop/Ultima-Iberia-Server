using System;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Mobiles;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
    public abstract class Food : Item
    {
        private Mobile m_Poisoner;
        private Poison m_Poison;
        private int m_FillFactor;
        private bool m_PlayerConstructed;
        #region Raw Foods
        private bool m_Raw;
        #endregion
        #region Rotten Food
        private bool m_Rotten;
        #endregion
        #region stat and skill boosts
        private string m_Skill; //Name of the skill
        private int m_IntBoost; //boost points
        private int m_DexBoost; //boost points
        private int m_StrBoost; //boost points
        private int m_SkillBoost; //boost points
        private double m_StatBoostTime;//Seconds Duration
        private double m_StatBoostDataTime;//To convert to minutes from seconds in 3 case
        private double m_SkillBoostTime;//Minutes Duration
        #endregion

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Poisoner
        {
            get
            {
                return this.m_Poisoner;
            }
            set
            {
                this.m_Poisoner = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PlayerConstructed
        {
            get
            {
                return this.m_PlayerConstructed;
            }
            set
            {
                this.m_PlayerConstructed = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Poison Poison
        {
            get
            {
                return this.m_Poison;
            }
            set
            {
                this.m_Poison = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int FillFactor
        {
            get
            {
                return this.m_FillFactor;
            }
            set
            {
                this.m_FillFactor = value;
            }
        }

        #region Raw Foods
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Raw
        {
            get { return m_Raw; }
            set { m_Raw = value; }
        }
        #endregion

        #region Rotten Food
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Rotten
        {
            get { return m_Rotten; }
            set { m_Rotten = value; }
        }
        #endregion

        #region stat and skill boost
        [CommandProperty(AccessLevel.GameMaster)]
        public string Skill
        {
            get
            {
                return m_Skill;
            }
            set
            {
                m_Skill = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SkillBoost
        {
            get
            {
                return m_SkillBoost;
            }
            set
            {
                m_SkillBoost = value;
                InvalidateProperties();
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public double SkillBoostTime
        {
            get { return m_SkillBoostTime; }
            set { m_SkillBoostTime = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int IntBoost
        {
            get
            {
                return m_IntBoost;
            }
            set
            {
                m_IntBoost = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int StrBoost
        {
            get
            {
                return m_StrBoost;
            }
            set
            {
                m_StrBoost = value;
                InvalidateProperties();
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int DexBoost
        {
            get
            {
                return m_DexBoost;
            }
            set
            {
                m_DexBoost = value;
                InvalidateProperties();
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public double StatBoostTime
        {
            get { return m_StatBoostTime; }
            set { m_StatBoostTime = value; }
        }
        #endregion

        public Food(int itemID)
            : this(1, itemID)
        {
        }

        public Food(int amount, int itemID)
            : base(itemID)
        {
            Stackable = true;
            Amount = amount;
            m_FillFactor = 1;
            #region Rotten Food
            Rotten = false;
            #endregion
        }

        public Food(Serial serial)
            : base(serial)
        {
        }
        /*// needs work still
        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Skill: " + Skill + ", Boost: " + SkillBoost);
        }
        */

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            if (from.Alive)
                list.Add(new ContextMenus.EatEntry(from, this));
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!this.Movable)
                return;

            if (from.InRange(this.GetWorldLocation(), 1))
            {
                this.Eat(from);
            }
        }

        public override bool StackWith(Mobile from, Item dropped, bool playSound)
        {
            if (dropped is Food && ((Food)dropped).PlayerConstructed == this.PlayerConstructed)
                return base.StackWith(from, dropped, playSound);
            else
                return false;
        }

        public virtual bool Eat(Mobile from)
        {
            // Fill the Mobile with FillFactor
            if (this.CheckHunger(from))
            {
                // Play a random "eat" sound
                from.PlaySound(Utility.Random(0x3A, 3));

                if (from.Body.IsHuman && !from.Mounted)
                    from.Animate(34, 5, 1, true, false, 0);

                if (this.m_Poison != null)
                    from.ApplyPoison(this.m_Poisoner, this.m_Poison);

                #region Raw Food
                if (this.m_Raw == true)
                {
                    switch (Utility.Random(7))
                    {
                        case 0: from.SendMessage("The food was raw and you don't feel so well!");  from.Poison = Poison.Regular; break;
                        case 1: from.SendMessage("The food was raw and you don't feel so well!");  from.Poison = Poison.Lesser; break;
                        case 2: from.SendMessage("The food was raw and you don't feel so well!");  from.Poison = Poison.Greater; break;
                        case 3: from.SendMessage("You were lucky you didn't get food poisoning"); break;
                        case 4: from.SendMessage("The food was raw and you don't feel so well!");  from.Poison = Poison.Deadly; break;
                        case 5: from.SendMessage("You were lucky you didn't get food poisoning"); break;
                        case 6: from.SendMessage("The food was raw and you don't feel so well!");  from.Poison = Poison.Regular; break;
                        default: break;
                    }
                }
                #endregion

                #region SicknessSYS_Start_5
                switch (Utility.Random(5000))
                {
                    case 0: Rotten = true; break;
                    case 1: Rotten = false; break;
                    default: break;
                }

                PlayerMobile pm = from as PlayerMobile;

                if (Rotten == true)
                {
                    switch (Utility.Random(12))
                    {
                        case 0: pm.SendMessage("The food was rotten and you don't feel so well!");  pm.Poison = Poison.Regular; break;
                        case 1: pm.SendMessage("The food was rotten and you don't feel so well!");  pm.Poison = Poison.Lesser; break;
                        case 2: pm.SendMessage("The food was rotten and you don't feel so well!");  pm.Hits -= 10; break;
                        case 3: pm.SendMessage("The food was rotten and you don't feel so well!");  pm.Mana -= 10; break;
                        case 4: pm.SendMessage("The food was rotten and you don't feel so well!");  pm.Poison = Poison.Greater; break;
                        case 5: pm.SendMessage("The food was rotten and you didn't get food poisoning"); break;
                        case 6: pm.SendMessage("The food was rotten and you don't feel so well!");  pm.Poison = Poison.Deadly; break;
                        case 7: pm.SendMessage("The food was rotten and you don't feel so well!"); pm.Hunger -= 10; break;
                        case 8: pm.SendMessage("The food was rotten and you didn't get food poisoning"); break;
                        case 9: pm.SendMessage("The food was rotten and you don't feel so well!");  pm.Poison = Poison.Regular; break;
                        case 10: pm.SendMessage("The food was rotten and you don't feel so well!"); pm.Hunger -= 10; break;
                        case 11: pm.SendMessage("The food was rotten and you don't feel so well!");  pm.Stam -= 10; break;
                        default: break;
                    }
                }
                #endregion

                #region stat and skill boost
                if (m_StatBoostTime < 1)
                {
                    m_StatBoostTime = m_FillFactor * 1;//1 minutes, same as fillfactor time Stats are based on seconds
                    m_StatBoostDataTime = (m_StatBoostTime / 1) * 60;//converting seconds to minutes for same timeframe as m_StatBoostTime
                }
                if (m_SkillBoostTime < 1)
                    m_SkillBoostTime = m_FillFactor * 1;//1 minutes, same as fillfactor timeSkill is based in Minutes in XmlSpawner

                if (m_Skill != null)
                {
                    if (XmlAttach.FindAttachment(from, typeof(XmlData), m_Skill) == null)
                    {
                        XmlAttach.AttachTo(from, new XmlData(m_Skill, m_Skill, m_SkillBoostTime));//Set Data to look for next time, XmlData is by minute
                        XmlAttach.AttachTo(from, new XmlSkill(m_Skill, m_Skill, m_SkillBoost, m_SkillBoostTime));//+ Skill, XmlSkill is by the MINUTE

                        from.FixedEffect(0x376A, 9, 32);
                        from.PlaySound(0x1F2);
                    }
                    else
                    {
                        from.SendMessage(521, "You are already under a {0} effect.", m_Skill);
                    }
                }

                if (IntBoost > 0)
                    if (XmlAttach.FindAttachment(from, typeof(XmlData), "Int") == null)
                    {
                        XmlAttach.AttachTo(from, new XmlData("Int", "Int", m_StatBoostTime));//Set Data to look for next time, XmlData is by minute
                        XmlAttach.AttachTo(from, new XmlInt(IntBoost, m_StatBoostDataTime)); //+ int, XmlInt is by the SECOND

                        from.FixedEffect(0x376A, 9, 32);
                        from.PlaySound(0x1F2);
                    }
                    else
                    {
                        from.SendMessage(521, "You are already under a Int effect.");
                    }
                if (DexBoost > 0)
                    if (XmlAttach.FindAttachment(from, typeof(XmlData), "Dex") == null)
                    {
                        XmlAttach.AttachTo(from, new XmlData("Dex", "Dex", m_StatBoostTime));//Set Data to look for next time, XmlData is by minute
                        XmlAttach.AttachTo(from, new XmlDex(DexBoost, m_StatBoostDataTime)); //+ dex, XmlDex is by the SECOND

                        from.FixedEffect(0x376A, 9, 32);
                        from.PlaySound(0x1F2);
                    }
                    else
                    {
                        from.SendMessage(521, "You are already under a Dex effect.");
                    }
                if (StrBoost > 0)
                    if (XmlAttach.FindAttachment(from, typeof(XmlData), "Str") == null)
                    {
                        XmlAttach.AttachTo(from, new XmlData("Str", "Str", m_StatBoostTime));//Set Data to look for next time, XmlData is by minute
                        XmlAttach.AttachTo(from, new XmlStr(StrBoost, m_StatBoostDataTime)); //+ str, XmlStr is by the SECOND

                        from.FixedEffect(0x376A, 9, 32);
                        from.PlaySound(0x1F2);
                    }
                    else
                    {
                        from.SendMessage(521, "You are already under a Str effect.");
                    }
                #endregion

                this.Consume();

                EventSink.InvokeOnConsume(new OnConsumeEventArgs(from, this));

                return true;
            }

            return false;
        }

        public virtual bool CheckHunger(Mobile from)
        {
            return FillHunger(from, this.m_FillFactor);
        }

        public static bool FillHunger(Mobile from, int fillFactor)
        {
            if (from.Hunger >= 20)
            {
                from.SendLocalizedMessage(500867); // You are simply too full to eat any more!
                return false;
            }

            int iHunger = from.Hunger + fillFactor;

            if (from.Stam < from.StamMax)
                from.Stam += Utility.Random(6, 3) + fillFactor / 5;

            if (iHunger >= 20)
            {
                from.Hunger = 20;
                from.SendLocalizedMessage(500872); // You manage to eat the food, but you are stuffed!
            }
            else
            {
                from.Hunger = iHunger;

                if (iHunger < 5)
                    from.SendLocalizedMessage(500868); // You eat the food, but are still extremely hungry.
                else if (iHunger < 10)
                    from.SendLocalizedMessage(500869); // You eat the food, and begin to feel more satiated.
                else if (iHunger < 15)
                    from.SendLocalizedMessage(500870); // After eating the food, you feel much less hungry.
                else
                    from.SendLocalizedMessage(500871); // You feel quite full after consuming the food.
            }

            return true;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)8); // version old number is 7

            // version 8
            writer.Write(m_IntBoost);
            writer.Write(m_DexBoost);
            writer.Write(m_StrBoost);
            writer.Write(m_StatBoostTime);
            writer.Write(m_Skill);
            writer.Write(m_SkillBoost); ;
            writer.Write(m_SkillBoostTime);

            //Version 7
            writer.Write(m_Rotten);

            //Version 6
            writer.Write(m_Raw);
            //Version 5
            writer.Write((bool)m_PlayerConstructed);

            //Version 4
            writer.Write(this.m_Poisoner);

            Poison.Serialize(this.m_Poison, writer);
            writer.Write(this.m_FillFactor);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        switch (reader.ReadInt())
                        {
                            case 0:
                                this.m_Poison = null;
                                break;
                            case 1:
                                this.m_Poison = Poison.Lesser;
                                break;
                            case 2:
                                this.m_Poison = Poison.Regular;
                                break;
                            case 3:
                                this.m_Poison = Poison.Greater;
                                break;
                            case 4:
                                this.m_Poison = Poison.Deadly;
                                break;
                        }

                        break;
                    }
                case 2:
                    {
                        this.m_Poison = Poison.Deserialize(reader);
                        break;
                    }
                case 3:
                    {
                        this.m_Poison = Poison.Deserialize(reader);
                        this.m_FillFactor = reader.ReadInt();
                        break;
                    }
                case 4:
                    {
                        this.m_Poisoner = reader.ReadMobile();
                        goto case 3;
                    }
                case 5:
                    {
                        this.m_PlayerConstructed = reader.ReadBool();
                        goto case 4;
                    }
                #region Raw
                case 6:
                    {
                        m_Raw = reader.ReadBool();
                        goto case 5;
                    }
                #endregion
                #region Rotten Food
                case 7:
                    {
                        m_Rotten = reader.ReadBool();
                        goto case 6;
                    }
                #endregion
                #region Stat and skill boost
                case 8:
                    {
                        m_IntBoost = reader.ReadInt();
                        m_DexBoost = reader.ReadInt();
                        m_StrBoost = reader.ReadInt();
                        m_StatBoostTime = reader.ReadDouble();
                        m_Skill = reader.ReadString();
                        m_SkillBoost = reader.ReadInt();
                        m_SkillBoostTime = reader.ReadDouble();
                        goto case 7;
                    }
                #endregion
            }
        }
    }
}