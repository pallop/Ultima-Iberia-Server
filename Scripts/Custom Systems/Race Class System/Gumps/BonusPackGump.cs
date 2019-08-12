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

    public class BonusPackGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public BonusPackGump(Mobile from, BonusPackOrb orb)
            : base(20, 30)
        {
            m_From = from;

            AddPage(0);
            AddBackground(0, 0, 340, 150, 5054);

            AddImageTiled(10, 10, 320, 23, 0x52);
            AddImageTiled(11, 11, 318, 23, 0xBBC);

            AddLabel(60, 11, 0, "Race/Class System *Add Bonus Pack*");

            AddLabel(30, 45, 0, "Bonus Pack Name: ");
            AddImageTiled(145, 43, 150, 23, 0x52);
            AddImageTiled(146, 44, 151, 23, 0xBBC);
            AddTextEntry(150, 43, 144, 20, 1334, 1, "");

            AddLabel(30, 75, 0, "Secondary Skill Cost: ");
            AddImageTiled(165, 73, 50, 23, 0x52);
            AddImageTiled(166, 74, 51, 23, 0xBBC);
            AddTextEntry(170, 73, 44, 20, 1334, 2, "");

            AddLabel(30, 120, 0, "Continue");
            AddButton(11, 121, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 1);
        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            BonusPackControl b_control = null;
            BonusPackOrb b_orb = null;
            BonusPackOrb t_orb = null;
            bool name_used = false;
            bool is_void = false;
            bool isInt = true;
            string c_name = null;
            string n_name = null;

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackControl)
                    b_control = i as BonusPackControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackOrb)
                {
                    b_orb = i as BonusPackOrb;
                    if (b_orb.BPNumber == b_control.B_Current)
                        break;
                }
            }

            if (info.ButtonID == 0)
            {
                b_orb.Delete();
            }

            if (info.ButtonID == 1)
            {
                if (b_orb != null)
                {
                    TextRelay m_name = info.GetTextEntry(1);
                    string text_name = (m_name == null ? "" : m_name.Text.Trim());

                    if (text_name.Length == 0)
                    {
                        m_From.SendMessage(0x35, "You must enter a Bonus Pack Name.");
                        m_From.SendGump(new BonusPackGump(from, b_orb));
                        is_void = true;
                    }
                    else
                    {
                        foreach (Item x in World.Items.Values)
                        {
                            if (x is BonusPackOrb)
                            {
                                t_orb = x as BonusPackOrb;
                                if (text_name != null)
                                    n_name = text_name.ToLower();
                                if (t_orb.BPName != null)
                                    c_name = t_orb.BPName.ToLower();
                                if (c_name == n_name)
                                {
                                    name_used = true;
                                }
                            }
                        }
                        if (name_used)
                        {
                            m_From.SendMessage(0x35, "That Bonus Pack Name is already used.");
                            if (!is_void)
                            {
                                m_From.SendGump(new BonusPackGump(from, b_orb));
                                is_void = true;
                            }
                        }
                        else
                        {
                            b_orb.BPName = text_name;
                            b_orb.BackUpName = text_name;
                        }
                    }

                    TextRelay m_bp = info.GetTextEntry(2);
                    string text_bp = (m_bp == null ? "" : m_bp.Text.Trim());

                    if (text_bp.Length == 0)
                    {
                        m_From.SendMessage(0x35, "You must enter a Secondary Skill Value");
                        if (!is_void)
                        {
                            m_From.SendGump(new BonusPackGump(from, b_orb));
                            is_void = true;
                        }
                    }
                    else
                    {
                        isInt = true;
                        try
                        {
                            int ibp = Convert.ToInt32(text_bp);
                        }
                        catch
                        {
                            from.SendMessage(0x35, "Value must be a number!");
                            if (!is_void)
                            {
                                m_From.SendGump(new BonusPackGump(from, b_orb));
                                is_void = true;
                            }
                            isInt = false;
                        }
                        if (isInt)
                        {
                            int r_bp = Convert.ToInt32(text_bp);
                            if (r_bp > 9)
                            {
                                from.SendMessage(0x35, "Value must be under 10");
                                if (!is_void)
                                {
                                    m_From.SendGump(new BonusPackGump(from, b_orb));
                                    is_void = true;
                                }
                            }
                            else
                                if (r_bp < 1)
                                {
                                    from.SendMessage(0x35, "Value must be over 0");
                                    if (!is_void)
                                    {
                                        m_From.SendGump(new BonusPackGump(from, b_orb));
                                        is_void = true;
                                    }
                                }
                                else
                                    b_orb.Sec_Skill_Cost = r_bp;
                        }
                    }

                    if (!is_void)
                    {
                        m_From.SendGump(new BonusPickGump(from, b_orb));
                    }

                }
            }

        }
    }

    public class BonusPickGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public BonusPickGump(Mobile from, BonusPackOrb orb)
            : base(20, 30)
        {
            m_From = from;
            int h = 70;
            int w = 30;
            int m_move = 0;
            bool isChosen = false;

            AddPage(0);
            AddBackground(0, 0, 610, 450, 5054);

            AddImageTiled(165, 10, 290, 23, 0x52);
            AddImageTiled(166, 11, 288, 21, 0xBBC);

            AddLabel(215, 11, 0, "Bonus Pack Skills (Maximum of 5)");

            AddLabel(250, 45, 0, "Value: ");
            AddImageTiled(300, 43, 50, 23, 0x52);
            AddImageTiled(301, 44, 51, 23, 0xBBC);
            AddTextEntry(306, 45, 45, 20, 1334, 1, "");

            for (int i = 0; i < 54; i++)
            {

                isChosen = false;

                for (int j = 0; j < 5; j++)
                {
                    if (orb.BPSkills[j] == from.Skills[i].Name)
                        isChosen = true;
                }
                if (!isChosen)
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

            AddLabel(20, 420, 0, "Right click to return to Add Bonus Pack");


        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            BonusPackControl b_control = null;
            BonusPackOrb b_orb = null;
            bool isInt = true;

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackControl)
                    b_control = i as BonusPackControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackOrb)
                {
                    b_orb = i as BonusPackOrb;
                    if (b_orb.BPNumber == b_control.B_Current)
                        break;
                }
            }

            for (int i = 0; i < 54; i++)
            {
                if ((i + 1) == info.ButtonID)
                {

                    if (b_orb.AmountSkills != 5)
                    {
                        b_orb.BPSkills[b_orb.AmountSkills] = from.Skills[i].Name;

                        TextRelay m_bo = info.GetTextEntry(1);
                        string text_bo = (m_bo == null ? "" : m_bo.Text.Trim());

                        if (text_bo.Length == 0)
                        {
                            m_From.SendMessage(0x35, "You must enter a Value");
                            b_orb.BPSkills[b_orb.AmountSkills] = null;
                            m_From.SendGump(new BonusPickGump(from, b_orb));
                        }
                        else
                        {
                            isInt = true;
                            try
                            {
                                int ibp = Convert.ToInt32(text_bo);
                            }
                            catch
                            {
                                from.SendMessage(0x35, "Value must be a number!");
                                b_orb.BPSkills[b_orb.AmountSkills] = null;
                                m_From.SendGump(new BonusPickGump(from, b_orb));
                                isInt = false;
                            }
                            if (isInt)
                            {
                                int b_pri = Convert.ToInt32(text_bo);
                                if (b_pri > 120)
                                {
                                    m_From.SendMessage(0x35, "Value is above 120");
                                    b_orb.BPSkills[b_orb.AmountSkills] = null;
                                    m_From.SendGump(new BonusPickGump(from, b_orb));
                                }
                                else
                                    if (b_pri < 0)
                                    {
                                        m_From.SendMessage(0x35, "Value is below 0");
                                        b_orb.BPSkills[b_orb.AmountSkills] = null;
                                        m_From.SendGump(new BonusPickGump(from, b_orb));
                                    }
                                    else
                                    {
                                        b_orb.BPValues[b_orb.AmountSkills] = b_pri;
                                        b_orb.AmountSkills += 1;
                                        m_From.SendGump(new BonusPickGump(from, b_orb));
                                        m_From.SendMessage(6, b_orb.BPSkills[b_orb.AmountSkills - 1] + " added as a Bonus Pack Skill with a cap of " + b_orb.BPValues[b_orb.AmountSkills - 1]);
                                    }
                            }
                        }
                    }
                    else
                    {
                        m_From.SendMessage(0x35, "You have reached the Maximum Bonus Pack Skills");
                        m_From.SendGump(new BonusPickGump(from, b_orb));
                    }

                }
            }

            if (info.ButtonID == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    b_orb.BPSkills[i] = null;
                }
                m_From.SendGump(new BonusPackGump(from, b_orb));
            }

            if (info.ButtonID == 60)
            {
                if (b_orb.AmountSkills == 0)
                {
                    m_From.SendMessage(0x35, "You have not chosen any Skills");
                    m_From.SendGump(new BonusPickGump(from, b_orb));
                }
                else
                {
                    Bag b_bag = new Bag();
                    b_bag.Name = b_orb.BPName + " Bonus Pack";

                    Bag i_bag = new Bag();
                    i_bag.Name = b_orb.BPName + " Items";

                    b_bag.AddItem(i_bag);

                    b_orb.Name = b_orb.BPName + " Bonus Pack";
                    b_control.A_BP += 1;
                    b_orb.Activated = true;
                    b_bag.AddItem(b_orb);
                    foreach (Item i in World.Items.Values)
                    {
                        if (i is Bag && i.Name == "BONUS PACKS")
                            i.AddItem(b_bag);
                    }
                    m_From.SendMessage(6, "Bonus Pack Generated");
                }
            }
        }


    }
}