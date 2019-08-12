using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
    public class LokaiSkillBall : Item
    {
        private int m_Points;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Points { get { return m_Points; } set { m_Points = value; InvalidateProperties(); } }

        [Constructable]
        public LokaiSkillBall()
            : this(15, 50)
        {
        }

        [Constructable]
        public LokaiSkillBall(int pointsMin, int pointsMax)
            : this(Utility.Random(pointsMin, pointsMax - pointsMin))
        {
        }

        [Constructable]
        public LokaiSkillBall(int points)
            : base(0xE73)
        {
            LootType = LootType.Blessed;
            Movable = true;
            m_Points = points;
            Name = "a NewSkill ball";
            Hue = 2222;
        }

        public LokaiSkillBall(Serial serial)
            : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_Points > 0)
                list.Add("  {0} Points!", m_Points.ToString());
            else list.Add(" *Empty* ");
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write((int)m_Points);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_Points = reader.ReadInt();
        }


        public override void OnDoubleClick(Mobile from)
        {
            PlayerMobile m;

            if (m_Points < 1)
            {
                from.SendMessage("That is empty!");
            }
            else if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
            else if (from.HasGump(typeof(IncreaseLokaiSkillsGump)))
            {
                from.SendMessage("You may not have more than one Increase LokaiSkills Gump running.");
            }
            else
            {
                if (from is PlayerMobile)
                {
                    m = from as PlayerMobile;
                    m.SendGump(new IncreaseLokaiSkillsGump(m, this, true, 0));
                }
            }
        }
    }

    //******************************************
    //**   Here is where the Gump starts     ***
    //******************************************
    public class IncreaseLokaiSkillsGump : Gump
    {
        private LokaiSkillBall m_Ball;
        private int m_Points;
        private PlayerMobile m_Mobile;
        private int m_Page;
        private int m_StartPos;
        private static double[] m_StartValue;
        private LokaiSkill m_LokaiSkill;
        private LokaiSkills m_LokaiSkills;

        public IncreaseLokaiSkillsGump(PlayerMobile mobile, LokaiSkillBall ball, double[] values, int page)
            : this(mobile, ball, false, page)
        {
            m_StartValue = new double[30];
            for (int x = 0; x < 30; x++)
                m_StartValue[x] = values[x];
        }

        public IncreaseLokaiSkillsGump(PlayerMobile mobile, LokaiSkillBall ball, bool first, int page)
            : base(50, 50)
        {
            m_Page = page;
            m_Ball = ball;
            m_Points = m_Ball.Points;
            m_Mobile = mobile;

            m_LokaiSkills = LokaiSkillUtilities.XMLGetSkills(m_Mobile);

            if (first)
            {
                m_StartValue = new double[30];
                m_Page = 0;
                for (int x = 0; x < 30; x++)
                {
                    m_StartValue[x] = m_LokaiSkills[x].Base;
                }
            }

            m_Mobile.CloseGump(typeof(IncreaseLokaiSkillsGump));

            AddPage(0);

            AddBackground(0, 0, 476, 440, 0x13BE);

            AddLabel(10, 7, 2100, "Choose Skills");

            if (m_Page > 0)
            {
                AddButton(275, 7, 250, 251, 2, GumpButtonType.Reply, 0); // Prev Page
            }

            if (m_Page < 2)
            {
                AddButton(275, 395, 252, 253, 3, GumpButtonType.Reply, 0); // Next Page
            }

            AddLabel(160, 7, 2100, "Points Left: " + m_Points.ToString());

            //			We only need this "if" condition if we want to make them use it all up right now.
            //			if ( m_Points == 0 )
            //			{
            AddButton(305, 335, 0xFB7, 0xFB9, 1, GumpButtonType.Reply, 0); // OK button
            //			}

            AddImage(170, -10, 0x58A);

            m_StartPos = m_Page * 10;
            int z = 0;

            for (int i = m_StartPos; i < m_StartPos + 10; i++)
            {
                int y = 20 + (30 * (++z));

                if (LokaiSkillUtilities.ShowLokaiSkill(i))
                {
                    m_LokaiSkill = m_LokaiSkills[i];

                    AddLabel(10, y, 2124, m_LokaiSkill.Name.ToString());

                    AddLabel(170, y, 2100, (((double)m_LokaiSkill.BaseFixedPoint) / 10).ToString());

                    if (CanLowerLokaiSkill(m_LokaiSkill, i, 1))
                        AddButton(220, y, 0x1519, 0x1519, 1000 + i, GumpButtonType.Reply, 0); // Decrease

                    if (CanRaiseLokaiSkill(m_LokaiSkill, i, 1))
                        AddButton(240, y, 0x151A, 0x151A, 2000 + i, GumpButtonType.Reply, 0); // Increase

                    if (CanLowerLokaiSkill(m_LokaiSkill, i, 5))
                        AddButton(200, y - 2, 2229, 2229, 3000 + i, GumpButtonType.Reply, 0); // Decrease by 5

                    if (CanRaiseLokaiSkill(m_LokaiSkill, i, 5))
                        AddButton(256, y - 2, 2229, 2229, 4000 + i, GumpButtonType.Reply, 0); // Increase by 5
                }
            }
        }

        public bool CanLowerLokaiSkill(LokaiSkill LokaiSkill, int pos, int amount)
        {
            if (LokaiSkill.Base - amount >= m_StartValue[pos])
                return true;
            else if (m_Mobile.AccessLevel >= AccessLevel.GameMaster) // Why should we limit a GM? hehe
                return true;
            else return false;
        }

        public bool CanRaiseLokaiSkill(LokaiSkill LokaiSkill, int pos, int amount)
        {
            if ((m_Points >= amount) && ((LokaiSkill.Base + amount) <= m_LokaiSkills[pos].Cap))
                return true;
            else if (m_Mobile.AccessLevel >= AccessLevel.GameMaster) // Why should we limit a GM? hehe
                return true;
            else return false;
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            PlayerMobile from = state.Mobile as PlayerMobile;

            if (info.ButtonID == 1) // "Ok"
            {
                from.CloseGump(typeof(IncreaseLokaiSkillsGump));
            }
            else if (info.ButtonID >= 4000 && (CanRaiseLokaiSkill(m_LokaiSkills[info.ButtonID - 4000], 
                info.ButtonID - 4000, 5) || from.AccessLevel>=AccessLevel.GameMaster)) // Increase by 5
            {
                m_LokaiSkills[info.ButtonID - 4000].Base += 5.0;
                m_Ball.Points -= 5;
                if (m_Ball.Points <= 0) m_Ball.Consume();
                else from.SendGump(new IncreaseLokaiSkillsGump(from, m_Ball, m_StartValue, m_Page));
            }
            else if (info.ButtonID >= 3000 && (CanLowerLokaiSkill(m_LokaiSkills[info.ButtonID - 3000], 
                info.ButtonID - 3000, 5) || from.AccessLevel>=AccessLevel.GameMaster)) // Decrease by 5
            {
                m_LokaiSkills[info.ButtonID - 3000].Base -= 5.0;
                m_Ball.Points += 5;
                from.SendGump(new IncreaseLokaiSkillsGump(from, m_Ball, m_StartValue, m_Page));
            }
            else if (info.ButtonID >= 2000 && (CanRaiseLokaiSkill(m_LokaiSkills[info.ButtonID - 2000], 
                info.ButtonID - 2000, 1) || from.AccessLevel>=AccessLevel.GameMaster)) // Increase
            {
                m_LokaiSkills[info.ButtonID - 2000].Base += 1.0;
                m_Ball.Points -= 1;
                if (m_Ball.Points <= 0) m_Ball.Consume();
                else from.SendGump(new IncreaseLokaiSkillsGump(from, m_Ball, m_StartValue, m_Page));
            }
            else if (info.ButtonID >= 1000 && (CanLowerLokaiSkill(m_LokaiSkills[info.ButtonID - 1000],
                info.ButtonID - 1000, 1) || from.AccessLevel >= AccessLevel.GameMaster)) // Decrease
            {
                m_LokaiSkills[info.ButtonID - 1000].Base -= 1.0;
                m_Ball.Points += 1;
                from.SendGump(new IncreaseLokaiSkillsGump(from, m_Ball, m_StartValue, m_Page));
            }
            else if (info.ButtonID == 2) // Previous Page
            {
                --m_Page;
                try
                {
                    from.SendGump(new IncreaseLokaiSkillsGump(from, m_Ball, m_StartValue, m_Page));
                }
                catch (Exception e) { Console.WriteLine("Exception caught: {0}, caused by {1} using Lokai Skill Ball.", e.Message, from.Name); }
            }
            else if (info.ButtonID == 3) // Next Page
            {
                ++m_Page;
                try
                {
                    from.SendGump(new IncreaseLokaiSkillsGump(from, m_Ball, m_StartValue, m_Page));
                }
                catch (Exception e) { Console.WriteLine("Exception caught: {0}, caused by {1} using Lokai Skill Ball.", e.Message, from.Name); }
            }
            else
                from.CloseGump(typeof(IncreaseLokaiSkillsGump));
        }
    }
}