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


    public class HueEditGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public HueEditGump(Mobile from)
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

            AddLabel(30, 34, 0, "Change Hues for which Race?");

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
            RaceControl r_control = null;

            foreach (Item h in World.Items.Values)
            {
                if (h is RaceControl)
                    r_control = h as RaceControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceOrb)
                {
                    r_orb = i as RaceOrb;
                    if (info.ButtonID == r_orb.RaceNumber)
                    {
                        r_control.R_Current = r_orb.RaceNumber;
                        from.SendGump(new HueEditGump2(from, r_orb, r_orb.BodyHues[0]));
                    }
                }
            }


            if (info.ButtonID == 0) { }

        }
    }


    public class HueEditGump2 : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public HueEditGump2(Mobile from, RaceOrb orb, int hue)
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

            AddLabel(95, 11, 0, orb.RaceName + " Skin Colors");

            for (int i = 0; i < 10; i++)
            {
                AddLabel(30, h, orb.BodyHues[i] - 1, "Hue " + (i + 1));
                AddImageTiled(90, h, 50, 23, 0x52);
                AddImageTiled(91, h - 1, 51, 23, 0xBBC);
                AddTextEntry(95, h, 45, 20, 1334, i + 11, orb.BodyHues[i] + "");
                AddButton(150, h + 1, 0x15E3, 0x15E7, i + 11, GumpButtonType.Reply, 1);
                h += 25;
            }

            AddImage(145, 10, b_v, hue - 1); //Body

            h += 10;
            AddLabel(30, h, 0, "Hair Hues");
            AddButton(11, h + 1, 0x15E3, 0x15E7, 25, GumpButtonType.Reply, 1);

            AddLabel(15, 330, 0, "Right click to return to the Race List");


        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            RaceOrb r_orb = null;
            RaceControl r_control = null;
            bool is_void = false;
            bool isInt = true;

            foreach (Item h in World.Items.Values)
            {
                if (h is RaceControl)
                    r_control = h as RaceControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceOrb)
                {
                    r_orb = i as RaceOrb;
                    if (r_orb.RaceNumber == r_control.R_Current)
                        break;
                }
            }


            for (int i = 0; i < 10; i++)
            {
                if (info.ButtonID == i + 11)
                {
                    TextRelay m_hue_body = info.GetTextEntry(i + 11);
                    string text_hue_body = (m_hue_body == null ? "" : m_hue_body.Text.Trim());

                    if (text_hue_body.Length == 0)
                    {
                        if (i == 0)
                        {
                            from.SendMessage(0x35, "Hue 1 cannot be blank");
                            if (!is_void)
                            {
                                m_From.SendGump(new HueEditGump2(from, r_orb, r_orb.BodyHues[0]));
                                is_void = true;
                            }
                            break;
                        }
                        else
                        {
                            m_From.SendGump(new HueEditGump2(from, r_orb, r_orb.BodyHues[0]));
                        }
                    }
                    else
                    {
                        isInt = true;
                        try
                        {
                            int ihue = Convert.ToInt32(text_hue_body);
                        }
                        catch
                        {
                            from.SendMessage(0x35, "Hues must be numbers!");
                            if (!is_void)
                            {
                                m_From.SendGump(new HueEditGump2(from, r_orb, r_orb.BodyHues[0]));
                                is_void = true;
                            }
                            isInt = false;
                        }
                        if (isInt)
                        {
                            int r_hue = Convert.ToInt32(text_hue_body);
                            if (i == 0 && r_hue == 0)
                            {
                                from.SendMessage(0x35, "Hue 1 cannot be 0");
                                if (!is_void)
                                {
                                    m_From.SendGump(new HueEditGump2(from, r_orb, r_orb.BodyHues[0]));
                                    is_void = true;
                                }
                            }
                            else
                            {

                                if (r_orb.BodyHues[i] == 0 && r_hue != 0)
                                    r_orb.AmountBodyHues += 1;

                                if (r_orb.BodyHues[i] != 0 && r_hue == 0)
                                    r_orb.AmountBodyHues -= 1;

                                r_orb.BodyHues[i] = r_hue;
                                from.SendGump(new HueEditGump2(from, r_orb, r_orb.BodyHues[i]));
                            }

                        }
                    }
                }
            }


            if (info.ButtonID == 25)
                from.SendGump(new HueEditGump3(from, r_orb, r_orb.HairHues[0]));

            if (info.ButtonID == 0)
            {
                m_From.SendGump(new HueEditGump(from));
            }

        }
    }

    public class HueEditGump3 : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public HueEditGump3(Mobile from, RaceOrb orb, int hue)
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
                AddLabel(30, h, orb.HairHues[i] - 1, "Hue " + (i + 1));
                AddImageTiled(90, h, 50, 23, 0x52);
                AddImageTiled(91, h - 1, 51, 23, 0xBBC);
                AddTextEntry(95, h, 45, 20, 1334, i + 11, orb.HairHues[i] + "");
                AddButton(150, h + 1, 0x15E3, 0x15E7, i + 11, GumpButtonType.Reply, 1);
                h += 25;
            }

            AddImage(145, 10, b_v, orb.BodyHues[0] - 1); //Body
            AddImage(145, 10, 50701, hue - 1); //Hair
            if (!from.Female)
                AddImage(145, 10, 50806, hue - 1); //Beard

            h += 10;
            AddLabel(30, h, 0, "Finish");
            AddButton(11, h + 1, 0x15E3, 0x15E7, 25, GumpButtonType.Reply, 1);

            AddLabel(15, 330, 0, "Right click to return to the Race List");


        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            RaceOrb r_orb = null;
            RaceControl r_control = null;
            bool is_void = false;
            bool isInt = true;

            foreach (Item h in World.Items.Values)
            {
                if (h is RaceControl)
                    r_control = h as RaceControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceOrb)
                {
                    r_orb = i as RaceOrb;
                    if (r_orb.RaceNumber == r_control.R_Current)
                        break;
                }
            }


            for (int i = 0; i < 10; i++)
            {
                if (info.ButtonID == i + 11)
                {
                    TextRelay m_hue_hair = info.GetTextEntry(i + 11);
                    string text_hue_hair = (m_hue_hair == null ? "" : m_hue_hair.Text.Trim());

                    if (text_hue_hair.Length == 0)
                    {
                        if (i == 0)
                        {
                            from.SendMessage(0x35, "Hue 1 cannot be blank");
                            if (!is_void)
                            {
                                m_From.SendGump(new HueEditGump3(from, r_orb, r_orb.HairHues[0]));
                                is_void = true;
                            }
                            break;
                        }
                        else
                        {
                            m_From.SendGump(new HueEditGump3(from, r_orb, r_orb.HairHues[0]));
                        }
                    }
                    else
                    {
                        isInt = true;
                        try
                        {
                            int ihue = Convert.ToInt32(text_hue_hair);
                        }
                        catch
                        {
                            from.SendMessage(0x35, "Hues must be numbers!");
                            if (!is_void)
                            {
                                m_From.SendGump(new HueEditGump3(from, r_orb, r_orb.HairHues[0]));
                                is_void = true;
                            }
                            isInt = false;
                        }
                        if (isInt)
                        {
                            int r_hue = Convert.ToInt32(text_hue_hair);
                            if (i == 0 && r_hue == 0)
                            {
                                from.SendMessage(0x35, "Hue 1 cannot be 0");
                                if (!is_void)
                                {
                                    m_From.SendGump(new HueEditGump3(from, r_orb, r_orb.HairHues[0]));
                                    is_void = true;
                                }
                            }
                            else
                            {

                                if (r_orb.HairHues[i] == 0 && r_hue != 0)
                                    r_orb.AmountHairHues += 1;

                                if (r_orb.HairHues[i] != 0 && r_hue == 0)
                                    r_orb.AmountHairHues -= 1;

                                r_orb.HairHues[i] = r_hue;
                                from.SendGump(new HueEditGump3(from, r_orb, r_orb.HairHues[i]));
                            }

                        }
                    }
                }
            }


            if (info.ButtonID == 25) { }

            if (info.ButtonID == 0)
            {
                m_From.SendGump(new HueEditGump(from));
            }

        }
    }

}