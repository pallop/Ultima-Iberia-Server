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


    public class ResSBGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public ResSBGump(Mobile from)
            : base(20, 30)
        {
            m_From = from;
            ClassOrb c_orb = null;
            ClassControl c_control = null;
            int h = 60;

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassControl)
                    c_control = i as ClassControl;
            }

            AddPage(0);
            AddBackground(0, 0, 340, (c_control.A_Classes * 25) + 80, 5054);

            AddImageTiled(10, 10, 320, 23, 0x52);
            AddImageTiled(11, 11, 318, 23, 0xBBC);

            AddLabel(100, 11, 0, "Race/Class System");

            AddLabel(30, 34, 0, "Restrict Spellbook from which Class?");

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassOrb)
                {
                    c_orb = i as ClassOrb;
                    if (c_orb.Activated)
                    {
                        AddButton(11, h, 0x15E3, 0x15E7, c_orb.ClassNumber, GumpButtonType.Reply, 1);
                        AddLabel(30, h - 1, 0, c_orb.ClassName);
                        h += 25;
                    }
                }
            }


        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            ClassOrb c_orb = null;
            ClassControl c_control = null;

            foreach (Item h in World.Items.Values)
            {
                if (h is ClassControl)
                    c_control = h as ClassControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassOrb)
                {
                    c_orb = i as ClassOrb;
                    if (info.ButtonID == c_orb.ClassNumber)
                    {
                        c_control.C_Current = c_orb.ClassNumber;
                        from.SendGump(new ResSBGump2(from, c_orb));
                    }
                }
            }


            if (info.ButtonID == 0) { }

        }
    }

    public class ResSBGump2 : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public ResSBGump2(Mobile from, ClassOrb orb)
            : base(20, 30)
        {
            m_From = from;
            ClassControl c_control = null;
            int h = 60;
            int rise = 0;
            bool isRestricted = false;

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassControl)
                    c_control = i as ClassControl;
            }

            for (int i = 0; i < 20; i++)
            {
                if (c_control.LoadBook[i] != SpellbookType.Invalid)
                    rise += 1;
            }

            AddPage(0);
            AddBackground(0, 0, 340, (rise * 25) + 130, 5054);

            AddImageTiled(10, 10, 320, 23, 0x52);
            AddImageTiled(11, 11, 318, 23, 0xBBC);

            AddLabel(100, 11, 0, "Race/Class System");

            AddLabel(30, 34, 0, "Current Restricted Spellbooks:");

            for (int i = 0; i < 20; i++)
            {

                if (orb.Res_Books[i] != SpellbookType.Invalid)
                {
                    AddButton(11, h, 0x15E3, 0x15E7, i + 1, GumpButtonType.Reply, 1);
                    AddLabel(30, h - 1, 0, orb.Res_Books[i] + " Spellbook");
                    h += 25;
                }
            }

            h += 10;

            AddLabel(30, h, 0, "Current Non-Restricted Spellbooks:");
            h += 30;

            for (int i = 0; i < 20; i++)
            {

                isRestricted = false;

                for (int j = 0; j < 20; j++)
                {
                    if (orb.Res_Books[j] == c_control.LoadBook[i])
                    {
                        isRestricted = true;
                        break;
                    }
                }

                if (!isRestricted)
                {
                    AddButton(11, h, 0x15E3, 0x15E7, i + 22, GumpButtonType.Reply, 1);
                    AddLabel(30, h - 1, 0, c_control.LoadBook[i] + " Spellbook");
                    h += 25;
                }
            }

        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            ClassOrb c_orb = null;
            ClassControl c_control = null;

            foreach (Item h in World.Items.Values)
            {
                if (h is ClassControl)
                    c_control = h as ClassControl;
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

            for (int i = 0; i < 20; i++)
            {
                if (info.ButtonID == i + 1)
                {
                    c_orb.Res_Books[i] = SpellbookType.Invalid;
                    from.SendGump(new ResSBGump(from));
                }
            }

            for (int i = 0; i < 20; i++)
            {
                if (info.ButtonID == i + 22)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        if (c_orb.Res_Books[j] == SpellbookType.Invalid)
                        {
                            c_orb.Res_Books[j] = c_control.LoadBook[i];
                            break;
                        }
                    }
                    from.SendGump(new ResSBGump(from));
                }
            }

            if (info.ButtonID == 0)
            {
                from.SendGump(new ResSBGump(from));
            }

        }
    }
}