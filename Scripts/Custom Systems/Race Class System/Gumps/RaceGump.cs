/************************************************************************************************/
/**********Echo Dynamic Race Class System V2.0, ©2005********************************************/
/************************************************************************************************/

using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.RaceClass;
using Server.Items;
using System.Collections;
using System.Reflection;
using Server.Targeting;

namespace Server.Gumps
{


    public class RaceGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public RaceGump(Mobile from)
            : base(20, 30)
        {
            m_From = from;
            RaceOrb r_orb = null;
            RaceControl r_control = null;
            int h = 60;

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceControl)
                    r_control = i as RaceControl;
            }

            AddPage(0);
            AddBackground(0, 0, 340, (r_control.A_Races * 25) + 80, 5054);

            AddImageTiled(10, 10, 320, 23, 0x52);
            AddImageTiled(11, 11, 318, 23, 0xBBC);

            AddLabel(100, 11, 0, "Race/Class System");

            AddLabel(30, 34, 0, "Choose your race.");

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceOrb)
                {
                    r_orb = i as RaceOrb;
                    if (r_orb.Activated)
                    {
                        AddButton(11, h, 0x15E3, 0x15E7, r_orb.RaceNumber, GumpButtonType.Reply, 1);
                        AddLabel(30, h - 1, 0, r_orb.RaceName);
                        h += 25;
                    }
                }
            }


        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            RaceOrb r_orb = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceOrb)
                {
                    r_orb = i as RaceOrb;
                    if (info.ButtonID == r_orb.RaceNumber)
                    {
                        rc.Race = r_orb.RaceName;
                        m_From.SendGump(new RaceDescGump(from, r_orb));
                    }
                }
            }

            if (info.ButtonID == 0)
            {
                //m_From.SendGump(new RaceGump(from));
            }

        }
    }

    public class RaceDescGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public RaceDescGump(Mobile from, RaceOrb orb)
            : base(20, 30)
        {
            m_From = from;

            AddPage(0);
            AddBackground(0, 0, 335, 230, 5054);

            AddImageTiled(10, 10, 315, 23, 0x52);
            AddImageTiled(11, 11, 313, 21, 0xBBC);

            AddLabel(85, 11, 0, orb.RaceName + " Race Description");
            AddHtml(40, 50, 250, 150, @String.Format(orb.Description), false, false);
            AddLabel(30, 174, 0, "To be a " + orb.RaceName + " Click here");
            AddButton(11, 175, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 1);
            AddLabel(15, 204, 0, "Right click to return to the Race Descriptions");


        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            RaceOrb r_orb = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            if (info.ButtonID == 1)
            {
                foreach (Item i in World.Items.Values)
                {
                    if (i is RaceOrb)
                    {
                        r_orb = i as RaceOrb;
                        if (r_orb.RaceName == rc.Race)
                        {
                            m_From.SendGump(new BodyHueGump(from, r_orb, r_orb.BodyHues[0]));
                        }
                    }
                }
            }

            if (info.ButtonID == 0)
            {
                m_From.SendGump(new RaceGump(from));
            }
        }
    }


    public class BodyHueGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public BodyHueGump(Mobile from, RaceOrb orb, int hue)
            : base(20, 30)
        {
            m_From = from;
            int h = 45;
            int b_v = 12;

            if (from.Female)
                b_v = 13;

            from.Hue = hue;

            AddPage(0);
            AddBackground(0, 0, 310, 360, 5054);

            AddImageTiled(10, 10, 290, 23, 0x52);
            AddImageTiled(11, 11, 288, 21, 0xBBC);

            AddLabel(95, 11, 0, orb.RaceName + " Skin Colors");

            for (int i = 0; i < 10; i++)
            {
                if (orb.BodyHues[i] != 0)
                {
                    AddLabel(30, h, orb.BodyHues[i] - 1, orb.RaceName);
                    AddButton(11, h + 1, 0x15E3, 0x15E7, i + 1, GumpButtonType.Reply, 1);
                    h += 25;
                }
            }

            AddImage(110, 10, b_v, hue - 1); //Body

            h += 10;
            AddLabel(30, h, 0, "Select");
            AddButton(11, h + 1, 0x15E3, 0x15E7, 15, GumpButtonType.Reply, 1);

            AddLabel(15, 330, 0, "Right click to return to the Race Descriptions");


        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            RaceOrb r_orb = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceOrb)
                {
                    r_orb = i as RaceOrb;
                    if (r_orb.RaceName == rc.Race)
                        break;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (info.ButtonID == i + 1)
                {
                    from.SendGump(new BodyHueGump(from, r_orb, r_orb.BodyHues[i]));

                }
            }

            if (info.ButtonID == 15)
                from.SendGump(new HairHueGump(from, r_orb, r_orb.HairHues[0]));

            if (info.ButtonID == 0)
            {
                m_From.SendGump(new RaceGump(from));
            }

        }
    }

    public class HairHueGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public HairHueGump(Mobile from, RaceOrb orb, int hue)
            : base(20, 30)
        {
            m_From = from;
            int h = 45;
            int b_v = 12;

            if (from.Female)
                b_v = 13;

            AddPage(0);
            AddBackground(0, 0, 310, 360, 5054);

            AddImageTiled(10, 10, 290, 23, 0x52);
            AddImageTiled(11, 11, 288, 21, 0xBBC);

            AddLabel(95, 11, 0, orb.RaceName + " Hair Colors");

            for (int i = 0; i < 10; i++)
            {
                if (orb.HairHues[i] != 0)
                {
                    AddLabel(30, h, orb.HairHues[i] - 1, orb.RaceName);
                    AddButton(11, h + 1, 0x15E3, 0x15E7, i + 1, GumpButtonType.Reply, 1);
                    h += 25;
                }
            }

            AddImage(110, 10, b_v, from.Hue - 1); //Body
            AddImage(110, 10, 50701, hue - 1); //Hair
            if (!from.Female)
                AddImage(110, 10, 50806, hue - 1); //Beard

            h += 10;
            AddLabel(30, h, 0, "Select");
            AddButton(11, h + 1, 0x15E3, 0x15E7, 15, GumpButtonType.Reply, 1);

            AddLabel(15, 330, 0, "Right click to return to the Race Descriptions");


        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            RaceOrb r_orb = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceOrb)
                {
                    r_orb = i as RaceOrb;
                    if (r_orb.RaceName == rc.Race)
                        break;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (info.ButtonID == i + 1)
                {
                    from.SendGump(new HairHueGump(from, r_orb, r_orb.HairHues[i]));

                }
            }

            if (info.ButtonID == 15)
                from.SendGump(new ClassGump(from));

            if (info.ButtonID == 0)
            {
                m_From.SendGump(new RaceGump(from));
            }

        }
    }

    public class ClassGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public ClassGump(Mobile from)
            : base(20, 30)
        {
            m_From = from;
            ClassOrb c_orb = null;
            ClassControl c_control = null;
            RaceOrb r_orb = null;
            int h = 60;
            bool isRestricted = false;
            int num = 0;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassControl)
                    c_control = i as ClassControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceOrb)
                {
                    r_orb = i as RaceOrb;
                    if (r_orb.RaceName == rc.Race)
                        break;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (r_orb.Restricted_C[i] != null)
                    num += 1;
            }

            AddPage(0);
            AddBackground(0, 0, 340, ((c_control.A_Classes - num) * 25) + 80, 5054);

            AddImageTiled(10, 10, 320, 23, 0x52);
            AddImageTiled(11, 11, 318, 23, 0xBBC);

            AddLabel(100, 11, 0, "Race/Class System");

            AddLabel(30, 34, 0, "Choose your class.");

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassOrb)
                {
                    c_orb = i as ClassOrb;
                    if (c_orb.Activated)
                    {

                        isRestricted = false;

                        for (int j = 0; j < 5; j++)
                        {
                            if (c_orb.ClassName == r_orb.Restricted_C[j])
                                isRestricted = true;
                        }

                        if (!isRestricted)
                        {
                            AddButton(11, h, 0x15E3, 0x15E7, c_orb.ClassNumber, GumpButtonType.Reply, 1);
                            AddLabel(30, h - 1, 0, c_orb.ClassName);
                            h += 25;
                        }
                    }
                }
            }


        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            ClassOrb c_orb = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassOrb)
                {
                    c_orb = i as ClassOrb;
                    if (info.ButtonID == c_orb.ClassNumber)
                    {
                        rc.P_Class = c_orb.ClassName;
                        m_From.SendGump(new ClassDescGump(from, c_orb));
                    }
                }
            }

            if (info.ButtonID == 0)
            {
                m_From.SendGump(new RaceGump(from));
            }

        }
    }


    public class ClassDescGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public ClassDescGump(Mobile from, ClassOrb orb)
            : base(20, 30)
        {
            m_From = from;
            int h = 135;

            AddPage(0);
            AddBackground(0, 0, 335, ((orb.Amount_Primary + orb.Amount_Restricted) * 25) + 350, 5054);

            AddImageTiled(10, 10, 315, 23, 0x52);
            AddImageTiled(11, 11, 313, 21, 0xBBC);

            AddLabel(85, 11, 0, orb.ClassName + " Class Description");
            AddHtml(40, 50, 250, 180, @String.Format(orb.Description), false, false);


            AddLabel(35, h, 0, "Primary Skills:");
            h += 25;
            for (int i = 0; i < orb.Amount_Primary; i++)
            {
                AddLabel(35, h, 0, orb.Primary_Skills[i] + " = " + orb.Primary_Values[i]);
                h += 25;
            }

            h += 10;
            AddLabel(35, h, 0, "Restricted Skills:");
            h += 25;

            for (int i = 0; i < orb.Amount_Restricted; i++)
            {
                AddLabel(35, h, 0, orb.Restricted_Skills[i]);
                h += 25;
            }

            h += 10;
            AddLabel(35, h, 0, "Secondary Skills: " + orb.Amount_Secondary);
            h += 25;
            AddLabel(35, h, 0, "Tertiary Skills: " + orb.Amount_Tertiary);
            h += 30;
            AddLabel(35, h, 0, "Str Cap: " + orb.Str_Cap + "  " + "Dex Cap: " + orb.Dex_Cap + "  " + "Int Cap: " + orb.Int_Cap);
            h += 30;

            AddLabel(30, h, 0, "To be a " + orb.ClassName + " Click here");
            AddButton(11, h + 1, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 1);
            AddLabel(15, h + 25, 0, "Right click to return to the Class List");


        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            this.Closable = false;
            this.Disposable =false;
            this.Dragable = true;
            this.Resizable = false;
            Mobile from = sender.Mobile;
            ClassOrb c_orb = null;
            BonusPackControl b_control = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackControl)
                    b_control = i as BonusPackControl;
            }

            if (info.ButtonID == 1)
            {
                foreach (Item i in World.Items.Values)
                {
                    if (i is ClassOrb)
                    {
                        c_orb = i as ClassOrb;
                        if (c_orb.ClassName == rc.P_Class)
                        {

                            for (int j = 0; j < c_orb.Amount_Primary; j++)
                            {
                                for (int k = 0; k < 54; k++)
                                {
                                    if (from.Skills[k].Name == c_orb.Primary_Skills[j])
                                    {
                                        from.Skills[k].Cap = c_orb.Primary_Values[j];
                                        m_From.SendMessage(6, c_orb.Primary_Skills[j] + " Cap has been set to " + c_orb.Primary_Values[j]);
                                    }
                                }
                            }

                            for (int l = 0; l < c_orb.Amount_Restricted; l++)
                            {
                                for (int m = 0; m < 54; m++)
                                {
                                    if (from.Skills[m].Name == c_orb.Restricted_Skills[l])
                                    {
                                        from.Skills[m].Cap = 0.0;
                                        m_From.SendMessage(6, c_orb.Restricted_Skills[l] + " Cap has been set to 0");
                                    }
                                }
                            }

                            if (b_control.A_BP == 0)
                            {
                                rc.Second = c_orb.Amount_Secondary;
                                m_From.SendGump(new SecondaryGump(from, c_orb));
                            }
                            else
                            {
                                rc.Second = c_orb.Amount_Secondary;
                                m_From.SendGump(new SetBonusPackGump(from));
                            }
                        }
                    }
                }
            }

            if (info.ButtonID == 0)
            {
                m_From.SendGump(new ClassGump(from));
            }
        }
    }


    public class SetBonusPackGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public SetBonusPackGump(Mobile from)
            : base(20, 30)
        {
            m_From = from;
            BonusPackOrb b_orb = null;
            BonusPackControl b_control = null;
            int h = 115;
            int bp_add = 0;
            string desc = "A Bonus Pack is a group of lesser used skills that may be chosen at the cost of Secondary Skill Points.";
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackControl)
                    b_control = i as BonusPackControl;
            }

            foreach (Item b in World.Items.Values)
            {
                if (b is BonusPackOrb)
                {
                    b_orb = b as BonusPackOrb;
                    if (rc.Second + 1 > b_orb.Sec_Skill_Cost)
                        bp_add += 1;
                }
            }

            AddPage(0);
            AddBackground(0, 0, 340, (bp_add * 25) + 170, 5054);

            AddImageTiled(10, 10, 320, 23, 0x52);
            AddImageTiled(11, 11, 318, 23, 0xBBC);

            AddLabel(100, 11, 0, "Race/Class System");

            AddHtml(40, 50, 250, 120, @desc, false, false);

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackOrb)
                {
                    b_orb = i as BonusPackOrb;
                    if (b_orb.Activated)
                    {
                        if (rc.Second + 1 > b_orb.Sec_Skill_Cost)
                        {
                            AddButton(11, h, 0x15E3, 0x15E7, b_orb.BPNumber, GumpButtonType.Reply, 1);
                            AddLabel(30, h - 1, 0, b_orb.BPName + " Bonus Pack");
                            h += 25;
                        }
                    }
                }
            }

            h += 10;
            AddButton(11, h, 0x15E3, 0x15E7, 50, GumpButtonType.Reply, 1);
            AddLabel(30, h - 1, 0, "Choose Secondary Skills Instead.");


        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            BonusPackOrb b_orb = null;
            ClassOrb c_orb = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackOrb)
                {
                    b_orb = i as BonusPackOrb;
                    if (info.ButtonID == b_orb.BPNumber)
                    {
                        rc.BonusPack = b_orb.BPName;
                        m_From.SendGump(new BPDescGump(from, b_orb));
                    }
                }
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassOrb)
                {
                    c_orb = i as ClassOrb;
                    if (rc.P_Class == c_orb.ClassName)
                        break;
                }
            }

            if (info.ButtonID == 50)
            {
                m_From.SendGump(new SecondaryGump(from, c_orb));
            }

            if (info.ButtonID == 0)
            {
                m_From.SendGump(new ClassGump(from));
            }

        }
    }

    public class BPDescGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public BPDescGump(Mobile from, BonusPackOrb orb)
            : base(20, 30)
        {
            m_From = from;
            int h = 45;

            AddPage(0);
            AddBackground(0, 0, 335, ((orb.AmountSkills) * 25) + 150, 5054);

            AddImageTiled(10, 10, 315, 23, 0x52);
            AddImageTiled(11, 11, 313, 21, 0xBBC);

            AddLabel(85, 11, 0, orb.BPName + " Bonus Pack Skills");

            for (int i = 0; i < orb.AmountSkills; i++)
            {
                AddLabel(35, h, 0, orb.BPSkills[i] + " = " + orb.BPValues[i]);
                h += 25;
            }

            h += 10;
            AddLabel(35, h, 0, "Secondary Skill Cost: " + orb.Sec_Skill_Cost);
            h += 30;

            AddLabel(30, h, 0, "To select the " + orb.BPName + " Bonus Pack, Click here");
            AddButton(11, h + 1, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 1);
            AddLabel(15, h + 25, 0, "Right click to return to the Bonus Pack List");


        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            ClassOrb c_orb = null;
            BonusPackOrb b_orb = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassOrb)
                {
                    c_orb = i as ClassOrb;
                    if (rc.P_Class == c_orb.ClassName)
                        break;
                }
            }

            if (info.ButtonID == 1)
            {
                foreach (Item i in World.Items.Values)
                {
                    if (i is BonusPackOrb)
                    {
                        b_orb = i as BonusPackOrb;


                        if (b_orb.BPName == rc.BonusPack)
                        {

                            for (int j = 0; j < b_orb.AmountSkills; j++)
                            {
                                for (int k = 0; k < 54; k++)
                                {
                                    if (from.Skills[k].Name == b_orb.BPSkills[j])
                                    {
                                        from.Skills[k].Cap = b_orb.BPValues[j];
                                        m_From.SendMessage(6, b_orb.BPSkills[j] + " Cap has been set to " + b_orb.BPValues[j]);
                                    }
                                }
                            }

                            rc.Second = rc.Second - b_orb.Sec_Skill_Cost;
                            if (rc.Second > 0)
                                m_From.SendGump(new SecondaryGump(from, c_orb));
                            else
                            {
                                rc.Third = c_orb.Amount_Tertiary;
                                m_From.SendGump(new TertiaryGump(from, c_orb));
                            }
                        }

                    }
                }
            }

            if (info.ButtonID == 0)
            {
                m_From.SendGump(new SetBonusPackGump(from));
            }
        }
    }

    public class SecondaryGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public SecondaryGump(Mobile from, ClassOrb orb)
            : base(20, 30)
        {
            m_From = from;
            int h = 45;
            int w = 30;
            int m_move = 0;
            bool isUsed = false;
            ClassControl c_control = null;
            BonusPackOrb b_orb = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassControl)
                    c_control = i as ClassControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackOrb)
                {
                    b_orb = i as BonusPackOrb;
                    if (rc.BonusPack == b_orb.BPName)
                        break;
                }
            }

            AddPage(0);
            AddBackground(0, 0, 610, 420, 5054);

            AddImageTiled(180, 10, 290, 23, 0x52);
            AddImageTiled(181, 11, 288, 21, 0xBBC);

            AddLabel(200, 11, 0, "Secondary Skills (" + c_control.Secondary_Cap + " Skill Cap) - " + rc.Second + " left");


            for (int i = 0; i < 54; i++)
            {

                isUsed = false;

                for (int j = 0; j < 10; j++)
                {
                    if (orb.Primary_Skills[j] == from.Skills[i].Name)
                        isUsed = true;
                }

                for (int k = 0; k < 10; k++)
                {
                    if (orb.Restricted_Skills[k] == from.Skills[i].Name)
                        isUsed = true;
                }

                if (b_orb != null)
                {
                    for (int l = 0; l < 5; l++)
                    {
                        if (b_orb.BPSkills[l] == from.Skills[i].Name)
                            isUsed = true;
                    }
                }

                for (int m = 0; m < 10; m++)
                {
                    if (rc.Secondary[m] == from.Skills[i].Name)
                        isUsed = true;
                }

                if (!isUsed)
                {
                    AddLabel(w, h, 0, from.Skills[i].Name);
                    AddButton(w - 19, h + 1, 0x15E3, 0x15E7, i + 1, GumpButtonType.Reply, 1);

                    h = h + 20;
                    m_move = m_move + 1;
                    if (m_move == 15 || m_move == 30 || m_move == 45)
                    {
                        w = w + 150;
                        h = 45;
                    }
                }

            }


            AddLabel(20, 390, 0, "Right click to return to the Class List");


        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            ClassControl c_control = null;
            ClassOrb c_orb = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassControl)
                    c_control = i as ClassControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassOrb)
                {
                    c_orb = i as ClassOrb;
                    if (rc.P_Class == c_orb.ClassName)
                        break;
                }
            }

            for (int i = 0; i < 54; i++)
            {
                if ((i + 1) == info.ButtonID)
                {

                    from.Skills[i].Cap = c_control.Secondary_Cap;
                    m_From.SendMessage(6, from.Skills[i].Name + " Cap has been set to " + c_control.Secondary_Cap);
                    rc.Secondary[c_orb.Amount_Secondary - rc.Second] = from.Skills[i].Name;
                    rc.Second -= 1;
                    if (rc.Second > 0)
                        m_From.SendGump(new SecondaryGump(from, c_orb));
                    else
                    {
                        rc.Third = c_orb.Amount_Tertiary;
                        m_From.SendGump(new TertiaryGump(from, c_orb));
                    }

                }
            }

            if (info.ButtonID == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    rc.Secondary[i] = null;
                }
                m_From.SendGump(new ClassGump(from));
            }

        }
    }

    public class TertiaryGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public TertiaryGump(Mobile from, ClassOrb orb)
            : base(20, 30)
        {
            m_From = from;
            int h = 45;
            int w = 30;
            int m_move = 0;
            bool isUsed = false;
            ClassControl c_control = null;
            BonusPackOrb b_orb = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassControl)
                    c_control = i as ClassControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackOrb)
                {
                    b_orb = i as BonusPackOrb;
                    if (rc.BonusPack == b_orb.BPName)
                        break;
                }
            }

            AddPage(0);
            AddBackground(0, 0, 610, 420, 5054);

            AddImageTiled(180, 10, 290, 23, 0x52);
            AddImageTiled(181, 11, 288, 21, 0xBBC);

            AddLabel(200, 11, 0, "Tertiary Skills (" + c_control.Tertiary_Cap + " Skill Cap) - " + rc.Third + " left");


            for (int i = 0; i < 54; i++)
            {

                isUsed = false;

                for (int j = 0; j < 10; j++)
                {
                    if (orb.Primary_Skills[j] == from.Skills[i].Name)
                        isUsed = true;
                }

                for (int k = 0; k < 10; k++)
                {
                    if (orb.Restricted_Skills[k] == from.Skills[i].Name)
                        isUsed = true;
                }

                if (b_orb != null)
                {
                    for (int l = 0; l < 5; l++)
                    {
                        if (b_orb.BPSkills[l] == from.Skills[i].Name)
                            isUsed = true;
                    }
                }

                for (int m = 0; m < 10; m++)
                {
                    if (rc.Secondary[m] == from.Skills[i].Name)
                        isUsed = true;
                }

                for (int n = 0; n < 10; n++)
                {
                    if (rc.Tertiary[n] == from.Skills[i].Name)
                        isUsed = true;
                }

                if (!isUsed)
                {
                    AddLabel(w, h, 0, from.Skills[i].Name);
                    AddButton(w - 19, h + 1, 0x15E3, 0x15E7, i + 1, GumpButtonType.Reply, 1);

                    h = h + 20;
                    m_move = m_move + 1;
                    if (m_move == 15 || m_move == 30 || m_move == 45)
                    {
                        w = w + 150;
                        h = 45;
                    }
                }

            }


            AddLabel(20, 390, 0, "Right click to return to the Class List");


        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            ClassControl c_control = null;
            ClassOrb c_orb = null;
            bool isUsed = false;
            BonusPackOrb b_orb = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassControl)
                    c_control = i as ClassControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassOrb)
                {
                    c_orb = i as ClassOrb;
                    if (rc.P_Class == c_orb.ClassName)
                        break;
                }
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackOrb)
                {
                    b_orb = i as BonusPackOrb;
                    if (rc.BonusPack == b_orb.BPName)
                        break;
                }
            }

            for (int i = 0; i < 54; i++)
            {
                if ((i + 1) == info.ButtonID)
                {

                    from.Skills[i].Cap = c_control.Tertiary_Cap;
                    m_From.SendMessage(6, from.Skills[i].Name + " Cap has been set to " + c_control.Tertiary_Cap);
                    rc.Tertiary[c_orb.Amount_Tertiary - rc.Third] = from.Skills[i].Name;
                    rc.Third -= 1;
                    if (rc.Third > 0)
                        m_From.SendGump(new TertiaryGump(from, c_orb));
                    else
                    {
                        for (int o = 0; o < 54; o++)
                        {

                            isUsed = false;

                            for (int j = 0; j < 10; j++)
                            {
                                if (c_orb.Primary_Skills[j] == from.Skills[o].Name)
                                    isUsed = true;
                            }

                            for (int k = 0; k < 10; k++)
                            {
                                if (c_orb.Restricted_Skills[k] == from.Skills[o].Name)
                                    isUsed = true;
                            }

                            if (b_orb != null)
                            {
                                for (int l = 0; l < 5; l++)
                                {
                                    if (b_orb.BPSkills[l] == from.Skills[o].Name)
                                        isUsed = true;
                                }
                            }

                            for (int m = 0; m < 10; m++)
                            {
                                if (rc.Secondary[m] == from.Skills[o].Name)
                                    isUsed = true;
                            }

                            for (int n = 0; n < 10; n++)
                            {
                                if (rc.Tertiary[n] == from.Skills[o].Name)
                                    isUsed = true;
                            }

                            if (!isUsed)
                            {
                                from.Skills[o].Cap = c_control.Default_Cap;
                            }

                        }
                        m_From.SendGump(new FinalGump(from, c_orb));
                    }

                }
            }

            if (info.ButtonID == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    rc.Tertiary[i] = null;
                    rc.Secondary[i] = null;
                }
                m_From.SendGump(new ClassGump(from));
            }

        }
    }

    public class FinalGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public FinalGump(Mobile from, ClassOrb orb)
            : base(20, 30)
        {
            m_From = from;
            int h = 194;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            AddPage(0);
            AddBackground(0, 0, 335, ((orb.Amount_Secondary + orb.Amount_Tertiary) * 25) + 280, 5054);

            AddImageTiled(10, 10, 315, 23, 0x52);
            AddImageTiled(11, 11, 313, 21, 0xBBC);

            AddLabel(85, 11, 0, "Summary of Character");
            AddLabel(30, 44, 0, "You have chosen the following:");
            AddLabel(30, 74, 197, "Race: " + rc.Race);
            AddLabel(30, 104, 197, "Class: " + rc.P_Class);
            if (rc.BonusPack != null)
                AddLabel(30, 134, 197, "Bonus Pack: " + rc.BonusPack);
            else
                h -= 65;

            AddLabel(30, h, 0, "Secondary Skills:");
            h += 30;
            for (int i = 0; i < 10; i++)
            {
                if (rc.Secondary[i] != null)
                {
                    AddLabel(30, h, 197, rc.Secondary[i]);
                    h += 25;
                }
            }

            h += 10;

            AddLabel(30, h, 0, "Tertiary Skills:");
            h += 30;
            for (int j = 0; j < 10; j++)
            {
                if (rc.Tertiary[j] != null)
                {
                    AddLabel(30, h, 197, rc.Tertiary[j]);
                    h += 25;
                }
            }

            h += 20;
            AddLabel(30, h, 0, "Finish");
            AddButton(11, h + 1, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 1); ;

        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            ClassControl c_control = null;
            RaceControl r_control = null;
            RaceOrb r_orb = null;
            ClassOrb c_orb = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;
            PlayerMobile pm = (PlayerMobile)from;
            RCChest chest = null;

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassControl)
                    c_control = i as ClassControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceControl)
                    r_control = i as RaceControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceOrb)
                {
                    r_orb = i as RaceOrb;
                    if (rc.Race == r_orb.RaceName)
                        break;
                }
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassOrb)
                {
                    c_orb = i as ClassOrb;
                    if (rc.P_Class == c_orb.ClassName)
                        break;
                }
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    chest = i as RCChest;
            }

            if (info.ButtonID == 1 || info.ButtonID == 0)
            {
                if (pm.Young == false && chest.Young_F == true)
                {
                    from.StatCap = c_control.Stat_Cap;
                    from.Skills.Cap = 52000;

                    if (from.Str > c_orb.Str_Cap)
                        from.Str = c_orb.Str_Cap;
                    if (from.Dex > c_orb.Dex_Cap)
                        from.Dex = c_orb.Dex_Cap;
                    if (from.Int > c_orb.Int_Cap)
                        from.Int = c_orb.Int_Cap;

                    for (int i = 0; i < 54; i++)
                    {
                        if (from.Skills[i].Value > from.Skills[i].Cap)
                        {
                            from.Skills[i].Base = from.Skills[i].Cap;
                        }
                    }

                    foreach (Item i in World.Items.Values)
                    {
                        if (i is Bag && i.Name == rc.P_Class + " Restricted Armors")
                        {
                            rc.A_Bag = i as Bag;
                            break;
                        }
                    }

                    foreach (Item i in World.Items.Values)
                    {
                        if (i is Bag && i.Name == rc.P_Class + " Restricted Weapons")
                        {
                            rc.W_Bag = i as Bag;
                            break;
                        }
                    }

                    if (r_control.Race_Title)
                        from.Title = "the " + rc.Race;
                    if (c_control.Class_Title)
                        from.Title = "the " + rc.P_Class;
                    if (r_control.Race_Title && c_control.Class_Title)
                        from.Title = "the " + rc.Race + " " + rc.P_Class;
                    rc.Active = true;
                    if (r_control.Set_Location)
                    {
                        from.Map = r_orb.Race_Map;
                        from.Location = new Point3D(r_orb.Race_X, r_orb.Race_Y, r_orb.Race_Z);
                    }
                }
                else
                {
                    from.StatCap = c_control.Stat_Cap;
                    from.Skills.Cap = 52000;
                    from.Str = c_orb.Set_Str;
                    from.Dex = c_orb.Set_Dex;
                    from.Int = c_orb.Set_Int;

                    if (from.Str > c_orb.Str_Cap)
                        from.Str = c_orb.Str_Cap;
                    if (from.Dex > c_orb.Dex_Cap)
                        from.Dex = c_orb.Dex_Cap;
                    if (from.Int > c_orb.Int_Cap)
                        from.Int = c_orb.Int_Cap;

                    for (int i = 0; i < 54; i++)
                    {
                        from.Skills[i].Base = 0;
                    }

                    rc.Third = c_control.Amount_Start_Skills;

                    foreach (Item i in World.Items.Values)
                    {
                        if (i is Bag && i.Name == rc.P_Class + " Restricted Armors")
                        {
                            rc.A_Bag = i as Bag;
                            break;
                        }
                    }

                    foreach (Item i in World.Items.Values)
                    {
                        if (i is Bag && i.Name == rc.P_Class + " Restricted Weapons")
                        {
                            rc.W_Bag = i as Bag;
                            break;
                        }
                    }

                    if (r_control.Race_Title)
                        from.Title = "the " + rc.Race;
                    if (c_control.Class_Title)
                        from.Title = "the " + rc.P_Class;
                    if (r_control.Race_Title && c_control.Class_Title)
                        from.Title = "the " + rc.Race + " " + rc.P_Class;

                    m_From.SendGump(new SkillPickGump(from));
                }
            }

        }
    }

    public class SkillPickGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public SkillPickGump(Mobile from) 
           : base(20, 30)
        {
              m_From = from;
            int h = 45;
            int w = 30;
            int m_move = 0;
            int bg = 0;
            ClassControl c_control = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassControl)
                    c_control = i as ClassControl;
            }

          for (int i = 0; i < 54; i++)
            {

                if ((from.Skills[i].Cap > c_control.Start_Skills_Value + 0) && (from.Skills[i].Value == 0))
                    bg += 1;
            }

            AddPage(0);
            AddBackground(0, 0, 310, (bg * 30) + 25, 5054);

            AddImageTiled(10, 10, 290, 23, 0x52);
            AddImageTiled(11, 11, 288, 21, 0xBBC);

            AddLabel(45, 11, 0, "Skill (" + c_control.Start_Skills_Value + " Skill per Choice) - " + rc.Third + " left");

            for (int i = 0; i < 54; i++)
            {

                if ((from.Skills[i].Cap > c_control.Start_Skills_Value ) && (from.Skills[i].Value == 0))
                {
                    AddLabel(w, h, 0, from.Skills[i].Name);
                    AddButton(w - 19, h + 1, 0x15E3, 0x15E7, i + 1, GumpButtonType.Reply, 1);

                    h = h + 20;
                    m_move = m_move + 1;
                    if (m_move == 15 || m_move == 30)
                    {
                        w = w + 150;
                        h = 45;
                    }
                }

            }



        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            ClassOrb c_orb = null;
            ClassControl c_control = null;
            RaceOrb r_orb = null;
            RaceControl r_control = null;
            BonusPackOrb b_orb = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassControl)
                    c_control = i as ClassControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceControl)
                    r_control = i as RaceControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceOrb)
                {
                    r_orb = i as RaceOrb;
                    if (rc.Race == r_orb.RaceName)
                        break;
                }
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassOrb)
                {
                    c_orb = i as ClassOrb;
                    if (rc.P_Class == c_orb.ClassName)
                        break;
                }
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackOrb)
                {
                    b_orb = i as BonusPackOrb;
                    if (rc.BonusPack == b_orb.BPName)
                        break;
                }
            }

            for (int i = 0; i < 54; i++)
            {

                if ((i + 1) == info.ButtonID)
                {

                    from.Skills[i].Base = c_control.Start_Skills_Value;
                    rc.Third = rc.Third - 1;
                    if (rc.Third > 0)
                    {
                        from.SendGump(new SkillPickGump(from));
                        break;
                    }
                    else
                    {

                        if (c_control.Equip_Players)
                        {


                            Container pouch = from.Backpack;
                            ArrayList finalitems = new ArrayList(pouch.Items);
                            ArrayList equipitems = new ArrayList(from.Items);
                            Container staff_bag = new Bag();

                            if (from.AccessLevel == AccessLevel.Player)
                            {
                                foreach (Item items in equipitems)
                                {
                                    if ((items.Layer != Layer.Bank) && (items.Layer != Layer.Backpack) && (items.Layer != Layer.Hair) && (items.Layer != Layer.Mount) && (items.Layer != Layer.FacialHair))
                                    {
                                        pouch.DropItem(items);
                                    }
                                }

                                finalitems = new ArrayList(pouch.Items);
                                foreach (Item item in finalitems)
                                {
                                  /*  {
                                        if (item != rc)
                                            item.Delete();
                                    }*/
                                }
                            }
                            else
                            {
                                foreach (Item items in equipitems)
                                {
                                   /* if ((items.Layer != Layer.Bank) && (items.Layer != Layer.Backpack) && (items.Layer != Layer.Hair) && (items.Layer != Layer.Mount) && (items.Layer != Layer.FacialHair))
                                    {
                                        pouch.DropItem(items);
                                    }*/
                                }

                                finalitems = new ArrayList(pouch.Items);

                                foreach (Item item in finalitems)
                                {/*
                                    {
                                        if (item != rc)
                                            staff_bag.AddItem(item);
                                    }*/
                                }
                                from.AddToBackpack(staff_bag);
                            }

                            Bag c_bag = null;
                            foreach (Item j in World.Items.Values)
                            {
                                if (j is Bag && j.Name == c_orb.ClassName + " Equip")
                                    c_bag = j as Bag;
                            }

                            ArrayList e_items = new ArrayList(c_bag.Items);

                            foreach (Item k in e_items)
                            {
                                if (k.Layer != Layer.Invalid)
                                {



                                    Type t = k.GetType();

                                    ConstructorInfo[] cinfo = t.GetConstructors();

                                    foreach (ConstructorInfo c in cinfo)
                                    {
                                        //if ( !c.IsDefined( typeof( ConstructableAttribute ), false ) ) continue;

                                        ParameterInfo[] paramInfo = c.GetParameters();

                                        if (paramInfo.Length == 0)
                                        {
                                            object[] objParams = new object[0];

                                            try
                                            {
                                                for (int x = 0; x < 1; x++)
                                                {
                                                    object o = c.Invoke(objParams);

                                                    if (o != null && o is Item)
                                                    {
                                                        Item newItem = (Item)o;


                                                        PropertyInfo[] props = k.GetType().GetProperties();

                                                        for (int y = 0; y < props.Length; y++)
                                                        {
                                                            try
                                                            {
                                                                if (props[y].CanRead && props[y].CanWrite)
                                                                {
                                                                    //Console.WriteLine( "Setting {0} = {1}", props[i].Name, props[i].GetValue( src, null ) );
                                                                    props[y].SetValue(newItem, props[y].GetValue(k, null), null);
                                                                }
                                                            }
                                                            catch
                                                            {
                                                                //Console.WriteLine( "Denied" );
                                                            }
                                                        }

                                                        newItem.Parent = null;
                                                        from.EquipItem(newItem);
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                                from.SendMessage("Error!");
                                                return;
                                            }
                                        }
                                    }

                                }
                            }

                            Bag c_bag2 = null;
                            foreach (Item j in World.Items.Values)
                            {
                                if (j is Bag && j.Name == c_orb.ClassName + " Items")
                                    c_bag2 = j as Bag;
                            }

                            ArrayList e_items2 = new ArrayList(c_bag2.Items);

                            foreach (Item k in e_items2)
                            {


                                Type t = k.GetType();

                                ConstructorInfo[] cinfo = t.GetConstructors();

                                foreach (ConstructorInfo c in cinfo)
                                {
                                    //if ( !c.IsDefined( typeof( ConstructableAttribute ), false ) ) continue;

                                    ParameterInfo[] paramInfo = c.GetParameters();

                                    if (paramInfo.Length == 0)
                                    {
                                        object[] objParams = new object[0];

                                        try
                                        {
                                            for (int x = 0; x < 1; x++)
                                            {
                                                object o = c.Invoke(objParams);

                                                if (o != null && o is Item)
                                                {
                                                    Item newItem = (Item)o;


                                                    PropertyInfo[] props = k.GetType().GetProperties();

                                                    for (int y = 0; y < props.Length; y++)
                                                    {
                                                        try
                                                        {
                                                            if (props[y].CanRead && props[y].CanWrite)
                                                            {
                                                                //Console.WriteLine( "Setting {0} = {1}", props[i].Name, props[i].GetValue( src, null ) );
                                                                props[y].SetValue(newItem, props[y].GetValue(k, null), null);
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            //Console.WriteLine( "Denied" );
                                                        }
                                                    }

                                                    newItem.Parent = null;
                                                    from.AddToBackpack(newItem);
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            from.SendMessage("Error!");
                                            return;
                                        }
                                    }
                                }

                            }

                            if (rc.BonusPack != null)
                            {
                                Bag c_bag3 = null;
                                foreach (Item j in World.Items.Values)
                                {
                                    if (j is Bag && j.Name == b_orb.BPName + " Items")
                                        c_bag3 = j as Bag;
                                }

                                ArrayList e_items3 = new ArrayList(c_bag3.Items);

                                foreach (Item k in e_items3)
                                {


                                    Type t = k.GetType();

                                    ConstructorInfo[] cinfo = t.GetConstructors();

                                    foreach (ConstructorInfo c in cinfo)
                                    {
                                        //if ( !c.IsDefined( typeof( ConstructableAttribute ), false ) ) continue;

                                        ParameterInfo[] paramInfo = c.GetParameters();

                                        if (paramInfo.Length == 0)
                                        {
                                            object[] objParams = new object[0];

                                            try
                                            {
                                                for (int x = 0; x < 1; x++)
                                                {
                                                    object o = c.Invoke(objParams);

                                                    if (o != null && o is Item)
                                                    {
                                                        Item newItem = (Item)o;


                                                        PropertyInfo[] props = k.GetType().GetProperties();

                                                        for (int y = 0; y < props.Length; y++)
                                                        {
                                                            try
                                                            {
                                                                if (props[y].CanRead && props[y].CanWrite)
                                                                {
                                                                    //Console.WriteLine( "Setting {0} = {1}", props[i].Name, props[i].GetValue( src, null ) );
                                                                    props[y].SetValue(newItem, props[y].GetValue(k, null), null);
                                                                }
                                                            }
                                                            catch
                                                            {
                                                                //Console.WriteLine( "Denied" );
                                                            }
                                                        }

                                                        newItem.Parent = null;
                                                        from.AddToBackpack(newItem);
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                                from.SendMessage("Error!");
                                                return;
                                            }
                                        }
                                    }


                                }


                            }



                            Bag c_bag4 = null;
                            foreach (Item j in World.Items.Values)
                            {
                                if (j is Bag && j.Name == r_orb.RaceName + " Items")
                                    c_bag4 = j as Bag;
                            }

                            ArrayList e_items4 = new ArrayList(c_bag4.Items);

                            foreach (Item k in e_items4)
                            {


                                Type t = k.GetType();

                                ConstructorInfo[] cinfo = t.GetConstructors();

                                foreach (ConstructorInfo c in cinfo)
                                {
                                    //if ( !c.IsDefined( typeof( ConstructableAttribute ), false ) ) continue;

                                    ParameterInfo[] paramInfo = c.GetParameters();

                                    if (paramInfo.Length == 0)
                                    {
                                        object[] objParams = new object[0];

                                        try
                                        {
                                            for (int x = 0; x < 1; x++)
                                            {
                                                object o = c.Invoke(objParams);

                                                if (o != null && o is Item)
                                                {
                                                    Item newItem = (Item)o;


                                                    PropertyInfo[] props = k.GetType().GetProperties();

                                                    for (int y = 0; y < props.Length; y++)
                                                    {
                                                        try
                                                        {
                                                            if (props[y].CanRead && props[y].CanWrite)
                                                            {
                                                                //Console.WriteLine( "Setting {0} = {1}", props[i].Name, props[i].GetValue( src, null ) );
                                                                props[y].SetValue(newItem, props[y].GetValue(k, null), null);
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            //Console.WriteLine( "Denied" );
                                                        }
                                                    }

                                                    newItem.Parent = null;
                                                    from.AddToBackpack(newItem);
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            from.SendMessage("Error!");
                                            return;
                                        }
                                    }
                                }


                            }



                        }

                        rc.Active = true;
                        if (r_control.Set_Location)
                        {
                            from.Map = r_orb.Race_Map;
                            from.Location = new Point3D(r_orb.Race_X, r_orb.Race_Y, r_orb.Race_Z);
                        }
                        break;
                    }
                }
            }

            if (info.ButtonID == 0)
            {
                m_From.SendGump(new SkillPickGump(from));
            }
        }

    }


}