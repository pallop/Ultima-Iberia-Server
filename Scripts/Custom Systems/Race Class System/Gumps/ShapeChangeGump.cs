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


    public class ShapeChangeGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public ShapeChangeGump(Mobile from)
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

            AddLabel(30, 34, 0, "Edit Shapeshift for which Race?");

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
                        from.SendGump(new ShapeChangeGump2(from, r_orb));
                    }
                }
            }


            if (info.ButtonID == 0) { }

        }
    }


    public class ShapeChangeGump2 : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public ShapeChangeGump2(Mobile from, RaceOrb orb)
            : base(20, 30)
        {
            m_From = from;
            int h = 60;
            int rise = 0;

            for (int i = 0; i < 5; i++)
            {
                if (orb.BodyValue[i] != 0)
                    rise += 1;
            }

            AddPage(0);
            AddBackground(0, 0, 340, (rise * 25) + 120, 5054);

            AddImageTiled(10, 10, 320, 23, 0x52);
            AddImageTiled(11, 11, 318, 23, 0xBBC);

            AddLabel(100, 11, 0, "Race/Class System");

            AddLabel(30, 34, 0, "Remove a Current Shapeshift");

            for (int i = 0; i < 5; i++)
            {
                if (orb.BodyValue[i] != 0)
                {
                    AddButton(11, h, 0x15E3, 0x15E7, i, GumpButtonType.Reply, 1);
                    AddLabel(30, h - 1, 0, orb.SS_Name[i] + " (Body: " + orb.BodyValue[i] + " Hue: " + orb.SS_Hue[i] + ")");
                    h += 25;
                }
            }

            h += 10;
            AddButton(11, h, 0x15E3, 0x15E7, 10, GumpButtonType.Reply, 1);
            AddLabel(30, h - 1, 0, "Add a new Shapeshift");

        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            RaceOrb r_orb = null;
            RaceControl r_control = null;
            int num = 0;

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

            for (int i = 0; i < 5; i++)
            {
                if (info.ButtonID == i)
                {
                    r_orb.BodyValue[i] = 0;
                    r_orb.SS_Hue[i] = 0;
                    r_orb.SS_Name[i] = null;
                }
            }

            if (info.ButtonID == 10)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (r_orb.BodyValue[i] != 0)
                        num += 1;
                }
                if (num == 5)
                    m_From.SendMessage(0x35, "Race already has 5 Shapeshift Forms, remove one to add.");
                else
                    from.SendGump(new AddShapeChangeGump(from, r_orb));
            }

            if (info.ButtonID == 0)
            {

                from.SendGump(new ShapeChangeGump(from));
            }

        }
    }

    public class AddShapeChangeGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public AddShapeChangeGump(Mobile from, RaceOrb orb)
            : base(20, 30)
        {
            m_From = from;

            AddPage(0);
            AddBackground(0, 0, 340, 180, 5054);

            AddImageTiled(10, 10, 320, 23, 0x52);
            AddImageTiled(11, 11, 318, 23, 0xBBC);

            AddLabel(100, 11, 0, "Race/Class System");

            AddLabel(30, 34, 0, "Add Shapeshift:");

            AddLabel(30, 65, 0, "Shapeshift Name: ");
            AddImageTiled(135, 63, 150, 23, 0x52);
            AddImageTiled(136, 64, 151, 23, 0xBBC);
            AddTextEntry(140, 63, 144, 20, 1334, 1, "");

            AddLabel(30, 95, 0, "Shapeshift Body: ");
            AddImageTiled(135, 93, 50, 23, 0x52);
            AddImageTiled(136, 94, 51, 23, 0xBBC);
            AddTextEntry(140, 93, 45, 20, 1334, 2, "0");

            AddLabel(30, 125, 0, "Shapeshift Hue: ");
            AddImageTiled(135, 123, 50, 23, 0x52);
            AddImageTiled(136, 124, 51, 23, 0xBBC);
            AddTextEntry(140, 123, 45, 20, 1334, 3, "0");

            AddButton(11, 155, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 1);
            AddLabel(30, 154, 0, "Done");


        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            RaceOrb r_orb = null;
            RaceControl r_control = null;
            bool is_void = false;
            bool isInt = true;
            int current_shift = 0;

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

            for (int i = 0; i < 5; i++)
            {
                if (r_orb.BodyValue[i] != 0)
                    current_shift += 1;
                else
                    break;
            }


            if (info.ButtonID == 0)
            {
                m_From.SendMessage(0x35, "No Shapeshift Form Added");
            }

            if (info.ButtonID == 1)
            {

                TextRelay m_name = info.GetTextEntry(1);
                string text_name = (m_name == null ? "" : m_name.Text.Trim());

                if (text_name.Length == 0)
                {
                    m_From.SendMessage(0x35, "You must enter a Shapeshift Name.");
                    m_From.SendGump(new AddShapeChangeGump(from, r_orb));
                    is_void = true;
                }
                else
                {
                    r_orb.SS_Name[current_shift] = text_name;
                }

                TextRelay m_bv = info.GetTextEntry(2);
                string text_bv = (m_bv == null ? "" : m_bv.Text.Trim());

                if (text_bv.Length == 0)
                {
                    m_From.SendMessage(0x35, "You must Enter a Body Value");
                    if (!is_void)
                    {
                        m_From.SendGump(new AddShapeChangeGump(from, r_orb));
                        is_void = true;
                    }
                }
                else
                {
                    isInt = true;
                    try
                    {
                        int ibv = Convert.ToInt32(text_bv);
                    }
                    catch
                    {
                        from.SendMessage(0x35, "Body Values must be numbers!");
                        if (!is_void)
                        {
                            m_From.SendGump(new AddShapeChangeGump(from, r_orb));
                            is_void = true;
                        }
                        isInt = false;
                    }
                    if (isInt)
                    {
                        int r_bv = Convert.ToInt32(text_bv);

                        if (r_bv == 0)
                        {
                            from.SendMessage(0x35, "Body Values cannot be 0");
                            if (!is_void)
                            {
                                m_From.SendGump(new AddShapeChangeGump(from, r_orb));
                                is_void = true;
                            }
                        }

                        else
                            r_orb.BodyValue[current_shift] = r_bv;
                    }
                }

                TextRelay m_hue = info.GetTextEntry(3);
                string text_hue = (m_hue == null ? "" : m_hue.Text.Trim());

                if (text_hue.Length == 0)
                {
                    m_From.SendMessage(0x35, "You must Enter a Hue Value");
                    if (!is_void)
                    {
                        m_From.SendGump(new AddShapeChangeGump(from, r_orb));
                        is_void = true;
                    }
                }
                else
                {
                    isInt = true;
                    try
                    {
                        int ihue = Convert.ToInt32(text_hue);
                    }
                    catch
                    {
                        from.SendMessage(0x35, "Hue Values must be numbers!");
                        if (!is_void)
                        {
                            m_From.SendGump(new AddShapeChangeGump(from, r_orb));
                            is_void = true;
                        }
                        isInt = false;
                    }
                    if (isInt)
                    {
                        int r_hue = Convert.ToInt32(text_hue);
                        r_orb.SS_Hue[current_shift] = r_hue;
                    }
                }

                if (!is_void)
                {
                    from.SendMessage(6, "Shapeshift has been added");
                }

            }

        }
    }

}