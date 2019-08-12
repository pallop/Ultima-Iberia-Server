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


namespace Server.Gumps
{


    public class AddClassGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public AddClassGump(Mobile from, ClassOrb orb)
            : base(20, 30)
        {
            m_From = from;

            AddPage(0);
            AddBackground(0, 0, 340, 275, 5054);

            AddImageTiled(10, 10, 320, 23, 0x52);
            AddImageTiled(11, 11, 318, 23, 0xBBC);

            AddLabel(60, 11, 0, "Race/Class System *Add Class*");

            AddLabel(30, 45, 0, "Class Name: ");
            AddImageTiled(115, 43, 150, 23, 0x52);
            AddImageTiled(116, 44, 151, 23, 0xBBC);
            AddTextEntry(120, 43, 144, 20, 1334, 1, "");

            AddLabel(30, 75, 0, "Class Description: ");
            AddImageTiled(36, 105, 250, 70, 0x52);
            AddImageTiled(37, 106, 251, 70, 0xBBC);
            AddTextEntry(43, 105, 240, 60, 1334, 2, "");

            AddLabel(30, 185, 0, "Str: ");
            AddImageTiled(60, 183, 50, 23, 0x52);
            AddImageTiled(61, 184, 51, 23, 0xBBC);
            AddTextEntry(65, 183, 45, 20, 1334, 3, "");

            AddLabel(130, 185, 0, "Dex: ");
            AddImageTiled(160, 183, 50, 23, 0x52);
            AddImageTiled(161, 184, 51, 23, 0xBBC);
            AddTextEntry(165, 183, 45, 20, 1334, 4, "");

            AddLabel(230, 185, 0, "Int: ");
            AddImageTiled(260, 183, 50, 23, 0x52);
            AddImageTiled(261, 184, 51, 23, 0xBBC);
            AddTextEntry(265, 183, 45, 20, 1334, 5, "");

            AddLabel(30, 220, 0, "Cap: ");
            AddImageTiled(60, 218, 50, 23, 0x52);
            AddImageTiled(61, 219, 51, 23, 0xBBC);
            AddTextEntry(65, 218, 45, 20, 1334, 6, "");

            AddLabel(130, 220, 0, "Cap: ");
            AddImageTiled(160, 218, 50, 23, 0x52);
            AddImageTiled(161, 219, 51, 23, 0xBBC);
            AddTextEntry(165, 218, 45, 20, 1334, 7, "");

            AddLabel(230, 220, 0, "Cap: ");
            AddImageTiled(260, 218, 50, 23, 0x52);
            AddImageTiled(261, 219, 51, 23, 0xBBC);
            AddTextEntry(265, 218, 45, 20, 1334, 8, "");

            AddLabel(30, 250, 0, "Continue");
            AddButton(11, 251, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 1);
        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            ClassControl c_control = null;
            ClassOrb c_orb = null;
            ClassOrb t_orb = null;
            bool name_used = false;
            bool is_void = false;
            bool isInt = true;
            string c_name = null;
            string n_name = null;

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
                    if (c_orb.ClassNumber == c_control.C_Current)
                        break;
                }
            }

            if (info.ButtonID == 0)
            {
                c_orb.Delete();
            }

            if (info.ButtonID == 1)
            {
                if (c_orb != null)
                {
                    TextRelay m_name = info.GetTextEntry(1);
                    string text_name = (m_name == null ? "" : m_name.Text.Trim());

                    if (text_name.Length == 0)
                    {
                        m_From.SendMessage(0x35, "You must enter a Class Name.");
                        m_From.SendGump(new AddClassGump(from, c_orb));
                        is_void = true;
                    }
                    else
                    {
                        foreach (Item x in World.Items.Values)
                        {
                            if (x is ClassOrb)
                            {
                                t_orb = x as ClassOrb;
                                if (text_name != null)
                                    n_name = text_name.ToLower();
                                if (t_orb.ClassName != null)
                                    c_name = t_orb.ClassName.ToLower();
                                if (c_name == n_name)
                                {
                                    name_used = true;
                                }
                            }
                        }
                        if (name_used)
                        {
                            m_From.SendMessage(0x35, "That Class Name is already used.");
                            if (!is_void)
                            {
                                m_From.SendGump(new AddClassGump(from, c_orb));
                                is_void = true;
                            }
                        }
                        else
                        {
                            c_orb.ClassName = text_name;
                            c_orb.BackUpName = text_name;
                        }
                    }

                    TextRelay m_desc = info.GetTextEntry(2);
                    string text_desc = (m_desc == null ? "" : m_desc.Text.Trim());

                    if (text_desc.Length == 0)
                    {
                        m_From.SendMessage(0x35, "You must enter a Class Description.");
                        if (!is_void)
                        {
                            m_From.SendGump(new AddClassGump(from, c_orb));
                            is_void = true;
                        }
                    }
                    else
                    {
                        c_orb.Description = text_desc;
                    }

                    TextRelay m_str = info.GetTextEntry(3);
                    string text_str = (m_str == null ? "" : m_str.Text.Trim());

                    if (text_str.Length == 0)
                    {
                        m_From.SendMessage(0x35, "You must enter a Strength Value");
                        if (!is_void)
                        {
                            m_From.SendGump(new AddClassGump(from, c_orb));
                            is_void = true;
                        }
                    }
                    else
                    {
                        isInt = true;
                        try
                        {
                            int istr = Convert.ToInt32(text_str);
                        }
                        catch
                        {
                            from.SendMessage(0x35, "Strength Value must be a number!");
                            if (!is_void)
                            {
                                m_From.SendGump(new AddClassGump(from, c_orb));
                                is_void = true;
                            }
                            isInt = false;
                        }
                        if (isInt)
                        {
                            int r_str = Convert.ToInt32(text_str);
                            if (r_str > 100)
                            {
                                m_From.SendMessage(0x35, "Strength is above 100");
                                if (!is_void)
                                {
                                    m_From.SendGump(new AddClassGump(from, c_orb));
                                    is_void = true;
                                }
                            }
                            else
                                if (r_str < 1)
                                {
                                    m_From.SendMessage(0x35, "Strength is below 1");
                                    if (!is_void)
                                    {
                                        m_From.SendGump(new AddClassGump(from, c_orb));
                                        is_void = true;
                                    }
                                }
                                else
                                {
                                    c_orb.Set_Str = r_str;
                                    m_From.SendMessage(6, "Strength set to " + c_orb.Set_Str);
                                }
                        }
                    }

                    TextRelay m_dex = info.GetTextEntry(4);
                    string text_dex = (m_dex == null ? "" : m_dex.Text.Trim());

                    if (text_dex.Length == 0)
                    {
                        m_From.SendMessage(0x35, "You must enter a Dexterity Value");
                        if (!is_void)
                        {
                            m_From.SendGump(new AddClassGump(from, c_orb));
                            is_void = true;
                        }
                    }
                    else
                    {
                        isInt = true;
                        try
                        {
                            int idex = Convert.ToInt32(text_dex);
                        }
                        catch
                        {
                            from.SendMessage(0x35, "Dexterity Value must be a number!");
                            if (!is_void)
                            {
                                m_From.SendGump(new AddClassGump(from, c_orb));
                                is_void = true;
                            }
                            isInt = false;
                        }
                        if (isInt)
                        {
                            int r_dex = Convert.ToInt32(text_dex);
                            if (r_dex > 100)
                            {
                                m_From.SendMessage(0x35, "Dexterity is above 100");
                                if (!is_void)
                                {
                                    m_From.SendGump(new AddClassGump(from, c_orb));
                                    is_void = true;
                                }
                            }
                            else
                                if (r_dex < 1)
                                {
                                    m_From.SendMessage(0x35, "Dexterity is below 1");
                                    if (!is_void)
                                    {
                                        m_From.SendGump(new AddClassGump(from, c_orb));
                                        is_void = true;
                                    }
                                }
                                else
                                {
                                    c_orb.Set_Dex = r_dex;
                                    m_From.SendMessage(6, "Dexterity set to " + c_orb.Set_Dex);
                                }
                        }
                    }

                    TextRelay m_int = info.GetTextEntry(5);
                    string text_int = (m_int == null ? "" : m_int.Text.Trim());

                    if (text_int.Length == 0)
                    {
                        m_From.SendMessage(0x35, "You must enter an Intelligence Value");
                        if (!is_void)
                        {
                            m_From.SendGump(new AddClassGump(from, c_orb));
                            is_void = true;
                        }
                    }
                    else
                    {
                        isInt = true;
                        try
                        {
                            int iint = Convert.ToInt32(text_int);
                        }
                        catch
                        {
                            from.SendMessage(0x35, "Intelligence Value must be a number!");
                            if (!is_void)
                            {
                                m_From.SendGump(new AddClassGump(from, c_orb));
                                is_void = true;
                            }
                            isInt = false;
                        }
                        if (isInt)
                        {
                            int r_int = Convert.ToInt32(text_int);
                            if (r_int > 100)
                            {
                                m_From.SendMessage(0x35, "Intelligence is above 100");
                                if (!is_void)
                                {
                                    m_From.SendGump(new AddClassGump(from, c_orb));
                                    is_void = true;
                                }
                            }
                            else
                                if (r_int < 1)
                                {
                                    m_From.SendMessage(0x35, "Intelligence is below 1");
                                    if (!is_void)
                                    {
                                        m_From.SendGump(new AddClassGump(from, c_orb));
                                        is_void = true;
                                    }
                                }
                                else
                                {
                                    c_orb.Set_Int = r_int;
                                    m_From.SendMessage(6, "Intelligence set to " + c_orb.Set_Int);
                                }
                        }
                    }

                    TextRelay m_cstr = info.GetTextEntry(6);
                    string text_cstr = (m_cstr == null ? "" : m_cstr.Text.Trim());

                    if (text_cstr.Length == 0)
                    {
                        m_From.SendMessage(0x35, "You must enter a Strength Cap");
                        if (!is_void)
                        {
                            m_From.SendGump(new AddClassGump(from, c_orb));
                            is_void = true;
                        }
                    }
                    else
                    {
                        isInt = true;
                        try
                        {
                            int icstr = Convert.ToInt32(text_cstr);
                        }
                        catch
                        {
                            from.SendMessage(0x35, "Strength Cap must be a number!");
                            if (!is_void)
                            {
                                m_From.SendGump(new AddClassGump(from, c_orb));
                                is_void = true;
                            }
                            isInt = false;
                        }
                        if (isInt)
                        {
                            int r_cstr = Convert.ToInt32(text_cstr);
                            if (r_cstr > 200)
                            {
                                m_From.SendMessage(0x35, "Strength Cap is above 200");
                                if (!is_void)
                                {
                                    m_From.SendGump(new AddClassGump(from, c_orb));
                                    is_void = true;
                                }
                            }
                            else
                                if (r_cstr < 1)
                                {
                                    m_From.SendMessage(0x35, "Strength Cap is below 1");
                                    if (!is_void)
                                    {
                                        m_From.SendGump(new AddClassGump(from, c_orb));
                                        is_void = true;
                                    }
                                }
                                else
                                {
                                    c_orb.Str_Cap = r_cstr;
                                    m_From.SendMessage(6, "Strength Cap set to " + c_orb.Str_Cap);
                                }
                        }
                    }

                    TextRelay m_cdex = info.GetTextEntry(7);
                    string text_cdex = (m_cdex == null ? "" : m_cdex.Text.Trim());

                    if (text_cdex.Length == 0)
                    {
                        m_From.SendMessage(0x35, "You must enter a Dexterity Cap");
                        if (!is_void)
                        {
                            m_From.SendGump(new AddClassGump(from, c_orb));
                            is_void = true;
                        }
                    }
                    else
                    {
                        isInt = true;
                        try
                        {
                            int icdex = Convert.ToInt32(text_cdex);
                        }
                        catch
                        {
                            from.SendMessage(0x35, "Dexterity Cap must be a number!");
                            if (!is_void)
                            {
                                m_From.SendGump(new AddClassGump(from, c_orb));
                                is_void = true;
                            }
                            isInt = false;
                        }
                        if (isInt)
                        {
                            int r_cdex = Convert.ToInt32(text_cdex);
                            if (r_cdex > 200)
                            {
                                m_From.SendMessage(0x35, "Dexterity Cap is above 200");
                                if (!is_void)
                                {
                                    m_From.SendGump(new AddClassGump(from, c_orb));
                                    is_void = true;
                                }
                            }
                            else
                                if (r_cdex < 1)
                                {
                                    m_From.SendMessage(0x35, "Dexterity Cap is below 1");
                                    if (!is_void)
                                    {
                                        m_From.SendGump(new AddClassGump(from, c_orb));
                                        is_void = true;
                                    }
                                }
                                else
                                {
                                    c_orb.Dex_Cap = r_cdex;
                                    m_From.SendMessage(6, "Dexterity Cap set to " + c_orb.Dex_Cap);
                                }
                        }
                    }


                    TextRelay m_cint = info.GetTextEntry(8);
                    string text_cint = (m_cint == null ? "" : m_cint.Text.Trim());

                    if (text_cint.Length == 0)
                    {
                        m_From.SendMessage(0x35, "You must enter an Intelligence Cap");
                        if (!is_void)
                        {
                            m_From.SendGump(new AddClassGump(from, c_orb));
                            is_void = true;
                        }
                    }
                    else
                    {
                        isInt = true;
                        try
                        {
                            int icint = Convert.ToInt32(text_cint);
                        }
                        catch
                        {
                            from.SendMessage(0x35, "Intelligence Cap must be a number!");
                            if (!is_void)
                            {
                                m_From.SendGump(new AddClassGump(from, c_orb));
                                is_void = true;
                            }
                            isInt = false;
                        }
                        if (isInt)
                        {
                            int r_cint = Convert.ToInt32(text_cint);
                            if (r_cint > 200)
                            {
                                m_From.SendMessage(0x35, "Intelligence Cap is above 200");
                                if (!is_void)
                                {
                                    m_From.SendGump(new AddClassGump(from, c_orb));
                                    is_void = true;
                                }
                            }
                            else
                                if (r_cint < 1)
                                {
                                    m_From.SendMessage(0x35, "Intelligence Cap is below 1");
                                    if (!is_void)
                                    {
                                        m_From.SendGump(new AddClassGump(from, c_orb));
                                        is_void = true;
                                    }
                                }
                                else
                                {
                                    c_orb.Int_Cap = r_cint;
                                    m_From.SendMessage(6, "Intelligence Cap set to " + c_orb.Int_Cap);
                                }
                        }
                    }


                    if (!is_void)
                    {
                        c_orb.Amount_Primary = 0;
                        m_From.SendGump(new ClassGumpPrimary(from, c_orb));
                    }

                }
            }

        }
    }

    public class ClassGumpPrimary : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public ClassGumpPrimary(Mobile from, ClassOrb orb)
            : base(20, 30)
        {
            m_From = from;
            int h = 70;
            int w = 30;
            int m_move = 0;
            bool isPrimary = false;

            AddPage(0);
            AddBackground(0, 0, 610, 450, 5054);

            AddImageTiled(165, 10, 290, 23, 0x52);
            AddImageTiled(166, 11, 288, 21, 0xBBC);

            AddLabel(215, 11, 0, "Primary Skills (Maximum of 10)");

            AddLabel(250, 45, 0, "Value: ");
            AddImageTiled(300, 43, 50, 23, 0x52);
            AddImageTiled(301, 44, 51, 23, 0xBBC);
            AddTextEntry(306, 45, 45, 20, 1334, 1, "");

            for (int i = 0; i < 54; i++)
            {

                isPrimary = false;

                for (int j = 0; j < 10; j++)
                {
                    if (orb.Primary_Skills[j] == from.Skills[i].Name)
                        isPrimary = true;
                }
                if (!isPrimary)
                {
                    AddLabel(w, h, 0, from.Skills[i].Name);
                    AddButton(w - 19, h + 1, 0x15E3, 0x15E7, i + 1, GumpButtonType.Reply, 1);

                    h = h + 20;
                    m_move = m_move + 1;
                    if (m_move == 15 || m_move == 30 || m_move == 45)
                    {
                        w = w + 150;
                        h = 70;
                    }
                }

            }

            AddButton(11, 391, 0x15E3, 0x15E7, 60, GumpButtonType.Reply, 1);
            AddLabel(30, 390, 0, "Done");

            AddLabel(20, 420, 0, "Right click to return to Add Class");


        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            ClassControl c_control = null;
            ClassOrb c_orb = null;
            bool isInt = true;

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
                    if (c_orb.ClassNumber == c_control.C_Current)
                        break;
                }
            }

            for (int i = 0; i < 54; i++)
            {
                if ((i + 1) == info.ButtonID)
                {

                    if (c_orb.Amount_Primary != 10)
                    {
                        c_orb.Primary_Skills[c_orb.Amount_Primary] = from.Skills[i].Name;

                        TextRelay m_pri = info.GetTextEntry(1);
                        string text_pri = (m_pri == null ? "" : m_pri.Text.Trim());

                        if (text_pri.Length == 0)
                        {
                            m_From.SendMessage(0x35, "You must enter a Value");
                            c_orb.Primary_Skills[c_orb.Amount_Primary] = null;
                            m_From.SendGump(new ClassGumpPrimary(from, c_orb));
                        }
                        else
                        {
                            isInt = true;
                            try
                            {
                                int ipri = Convert.ToInt32(text_pri);
                            }
                            catch
                            {
                                from.SendMessage(0x35, "Value must be a number!");
                                c_orb.Primary_Skills[c_orb.Amount_Primary] = null;
                                m_From.SendGump(new ClassGumpPrimary(from, c_orb));
                                isInt = false;
                            }
                            if (isInt)
                            {
                                int r_pri = Convert.ToInt32(text_pri);
                                if (r_pri > 120)
                                {
                                    m_From.SendMessage(0x35, "Value is above 120");
                                    c_orb.Primary_Skills[c_orb.Amount_Primary] = null;
                                    m_From.SendGump(new ClassGumpPrimary(from, c_orb));
                                }
                                else
                                    if (r_pri < 0)
                                    {
                                        m_From.SendMessage(0x35, "Value is below 0");
                                        c_orb.Primary_Skills[c_orb.Amount_Primary] = null;
                                        m_From.SendGump(new ClassGumpPrimary(from, c_orb));
                                    }
                                    else
                                    {
                                        c_orb.Primary_Values[c_orb.Amount_Primary] = r_pri;
                                        c_orb.Amount_Primary += 1;
                                        m_From.SendGump(new ClassGumpPrimary(from, c_orb));
                                        m_From.SendMessage(6, c_orb.Primary_Skills[c_orb.Amount_Primary - 1] + " added as a Primary Skill with a cap of " + c_orb.Primary_Values[c_orb.Amount_Primary - 1]);
                                    }
                            }
                        }
                    }
                    else
                    {
                        m_From.SendMessage(0x35, "You have reached the Maximum Primary Skills");
                        m_From.SendGump(new ClassGumpPrimary(from, c_orb));
                    }

                }
            }

            if (info.ButtonID == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    c_orb.Primary_Skills[i] = null;
                }
                m_From.SendGump(new AddClassGump(from, c_orb));
            }

            if (info.ButtonID == 60)
            {
                if (c_orb.Amount_Primary == 0)
                {
                    m_From.SendMessage(0x35, "You have not chosen any Primary Skills");
                    m_From.SendGump(new ClassGumpPrimary(from, c_orb));
                }
                else
                    m_From.SendGump(new NumberSecondaryGump(from, c_orb));
            }
        }

    }

    public class NumberSecondaryGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public NumberSecondaryGump(Mobile from, ClassOrb orb)
            : base(20, 30)
        {
            m_From = from;

            AddPage(0);
            AddBackground(0, 0, 280, 120, 5054);

            AddImageTiled(10, 10, 260, 23, 0x52);
            AddImageTiled(11, 11, 258, 23, 0xBBC);

            AddLabel(50, 11, 0, "Secondary Skills Amount (0-10)");

            AddImageTiled(30, 45, 50, 23, 0x52);
            AddImageTiled(31, 46, 51, 23, 0xBBC);
            AddTextEntry(33, 45, 45, 19, 1334, 1, "");

            AddButton(25, 80, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 1);
            AddLabel(44, 79, 0, "Submit");
        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            ClassControl c_control = null;
            ClassOrb c_orb = null;
            bool isInt = true;

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
                    if (c_orb.ClassNumber == c_control.C_Current)
                        break;
                }
            }

            if (info.ButtonID == 0)
            {
                m_From.SendGump(new NumberSecondaryGump(from, c_orb));
            }

            if (info.ButtonID == 1)
            {

                TextRelay m_num = info.GetTextEntry(1);
                string text_num = (m_num == null ? "" : m_num.Text.Trim());

                if (text_num.Length == 0)
                {
                    m_From.SendMessage(0x35, "You must enter a Value");
                    m_From.SendGump(new NumberSecondaryGump(from, c_orb));
                }
                else
                {
                    isInt = true;
                    try
                    {
                        int i = Convert.ToInt32(text_num);
                    }
                    catch
                    {
                        from.SendMessage(0x35, "Value must be a number!");
                        m_From.SendGump(new NumberSecondaryGump(from, c_orb));
                        isInt = false;
                    }
                    if (isInt)
                    {
                        int r_num = Convert.ToInt32(text_num);
                        if (r_num > 10)
                        {
                            m_From.SendMessage(0x35, "Value is above 10");
                            m_From.SendGump(new NumberSecondaryGump(from, c_orb));
                        }
                        else
                            if (r_num < 0)
                            {
                                m_From.SendMessage(0x35, "Value is below 0");
                                m_From.SendGump(new NumberSecondaryGump(from, c_orb));
                            }
                            else
                            {
                                c_orb.Amount_Secondary = r_num;
                                m_From.SendGump(new NumberTertiaryGump(from, c_orb));
                                m_From.SendMessage(6, "Number of Secondaries set to " + c_orb.Amount_Secondary);
                            }
                    }
                }
            }

        }
    }


    public class NumberTertiaryGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public NumberTertiaryGump(Mobile from, ClassOrb orb)
            : base(20, 30)
        {
            m_From = from;

            AddPage(0);
            AddBackground(0, 0, 280, 120, 5054);

            AddImageTiled(10, 10, 260, 23, 0x52);
            AddImageTiled(11, 11, 258, 23, 0xBBC);

            AddLabel(50, 11, 0, "Tertiary Skills Amount (0-10)");

            AddImageTiled(30, 45, 50, 23, 0x52);
            AddImageTiled(31, 46, 51, 23, 0xBBC);
            AddTextEntry(33, 45, 45, 19, 1334, 1, "");

            AddButton(25, 80, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 1);
            AddLabel(44, 79, 0, "Submit");
        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            ClassControl c_control = null;
            ClassOrb c_orb = null;
            bool isInt = true;

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
                    if (c_orb.ClassNumber == c_control.C_Current)
                        break;
                }
            }

            if (info.ButtonID == 0)
            {
                m_From.SendGump(new NumberTertiaryGump(from, c_orb));
            }

            if (info.ButtonID == 1)
            {

                TextRelay m_num = info.GetTextEntry(1);
                string text_num = (m_num == null ? "" : m_num.Text.Trim());

                if (text_num.Length == 0)
                {
                    m_From.SendMessage(0x35, "You must enter a Value");
                    m_From.SendGump(new NumberTertiaryGump(from, c_orb));
                }
                else
                {
                    isInt = true;
                    try
                    {
                        int i = Convert.ToInt32(text_num);
                    }
                    catch
                    {
                        from.SendMessage(0x35, "Value must be a number!");
                        m_From.SendGump(new NumberTertiaryGump(from, c_orb));
                        isInt = false;
                    }
                    if (isInt)
                    {
                        int r_num = Convert.ToInt32(text_num);
                        if (r_num > 10)
                        {
                            m_From.SendMessage(0x35, "Value is above 10");
                            m_From.SendGump(new NumberTertiaryGump(from, c_orb));
                        }
                        else
                            if (r_num < 0)
                            {
                                m_From.SendMessage(0x35, "Value is below 0");
                                m_From.SendGump(new NumberTertiaryGump(from, c_orb));
                            }
                            else
                            {
                                c_orb.Amount_Tertiary = r_num;
                                m_From.SendGump(new RestrictedSkillsGump(from, c_orb));
                                m_From.SendMessage(6, "Number of Tertiaries set to " + c_orb.Amount_Tertiary);
                            }
                    }
                }
            }

        }
    }


    public class RestrictedSkillsGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public RestrictedSkillsGump(Mobile from, ClassOrb orb)
            : base(20, 30)
        {
            m_From = from;
            int h = 45;
            int w = 30;
            int m_move = 0;
            bool isRestricted = false;

            AddPage(0);
            AddBackground(0, 0, 610, 450, 5054);

            AddImageTiled(165, 10, 290, 23, 0x52);
            AddImageTiled(166, 11, 288, 21, 0xBBC);

            AddLabel(215, 11, 0, "Restricted Skills (Maximum of 10)");


            for (int i = 0; i < 54; i++)
            {

                isRestricted = false;

                for (int j = 0; j < 10; j++)
                {
                    if (orb.Restricted_Skills[j] == from.Skills[i].Name)
                        isRestricted = true;
                }

                for (int k = 0; k < 10; k++)
                {
                    if (orb.Primary_Skills[k] == from.Skills[i].Name)
                        isRestricted = true;
                }

                if (!isRestricted)
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

            AddButton(11, 391, 0x15E3, 0x15E7, 60, GumpButtonType.Reply, 1);
            AddLabel(30, 390, 0, "Done");

            AddLabel(20, 420, 0, "Right click to return to Add Class");


        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            ClassControl c_control = null;
            ClassOrb c_orb = null;

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
                    if (c_orb.ClassNumber == c_control.C_Current)
                        break;
                }
            }

            for (int i = 0; i < 54; i++)
            {
                if ((i + 1) == info.ButtonID)
                {

                    if (c_orb.Amount_Restricted != 10)
                    {

                        c_orb.Restricted_Skills[c_orb.Amount_Restricted] = from.Skills[i].Name;
                        c_orb.Amount_Restricted += 1;
                        m_From.SendGump(new RestrictedSkillsGump(from, c_orb));
                        m_From.SendMessage(6, from.Skills[i].Name + " has been Restricted");
                    }

                    else
                    {
                        m_From.SendMessage(0x35, "You have reached the Maximum Restricted Skills");
                        m_From.SendGump(new RestrictedSkillsGump(from, c_orb));
                    }

                }
            }

            if (info.ButtonID == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    c_orb.Restricted_Skills[i] = null;
                    c_orb.Primary_Skills[i] = null;
                }
                m_From.SendGump(new AddClassGump(from, c_orb));
            }

            if (info.ButtonID == 60)
            {

                Container class_bag = new Bag();
                class_bag.Name = c_orb.ClassName + " Class";

                Container equip = new Bag();
                equip.Name = c_orb.ClassName + " Equip";

                Container items = new Bag();
                items.Name = c_orb.ClassName + " Items";

                Container r_arm = new Bag();
                r_arm.Name = c_orb.ClassName + " Restricted Armors";

                Container r_wea = new Bag();
                r_wea.Name = c_orb.ClassName + " Restricted Weapons";

                class_bag.AddItem(equip);
                class_bag.AddItem(items);
                class_bag.AddItem(r_arm);
                class_bag.AddItem(r_wea);
                class_bag.AddItem(c_orb);

                c_orb.Name = c_orb.ClassName + " Orb";

                foreach (Item i in World.Items.Values)
                {
                    if (i is Bag && i.Name == "CLASSES")
                        i.AddItem(class_bag);
                }
                c_control.A_Classes += 1;
                c_orb.Activated = true;
                m_From.SendMessage(6, "Class Generated");

            }
        }

    }

}