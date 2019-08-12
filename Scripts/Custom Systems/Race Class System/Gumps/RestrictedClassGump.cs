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


    public class RestrictClassGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public RestrictClassGump(Mobile from)
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

            AddLabel(30, 34, 0, "Restricted Class Menu for which Race?");

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
                        from.SendGump(new RestrictClassGump2(from, r_orb));
                    }
                }
            }


            if (info.ButtonID == 0) { }

        }
    }


    public class RestrictClassGump2 : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public RestrictClassGump2(Mobile from, RaceOrb orb)
            : base(20, 30)
        {
            m_From = from;
            RaceControl r_control = null;
            ClassControl c_control = null;
            ClassOrb c_orb = null;
            int h = 60;
            bool isRestricted = false;

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceControl)
                    r_control = i as RaceControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassControl)
                    c_control = i as ClassControl;
            }

            AddPage(0);
            AddBackground(0, 0, 340, (c_control.A_Classes * 25) + 160, 5054);

            AddImageTiled(10, 10, 320, 23, 0x52);
            AddImageTiled(11, 11, 318, 23, 0xBBC);

            AddLabel(100, 11, 0, "Race/Class System");

            AddLabel(30, 34, 0, "Remove Restricted Classes:");

            for (int i = 0; i < 5; i++)
            {
                if (orb.Restricted_C[i] != null)
                {
                    AddButton(11, h, 0x15E3, 0x15E7, i + 100, GumpButtonType.Reply, 1);
                    AddLabel(30, h - 1, 0, orb.Restricted_C[i]);
                    h += 25;
                }
            }

            h += 30;

            AddLabel(30, h, 0, "Add Restricted Class:");

            h += 25;

            foreach (Item k in World.Items.Values)
            {
                if (k is ClassOrb)
                {
                    c_orb = k as ClassOrb;
                    if (c_orb.Activated)
                    {

                        isRestricted = false;

                        for (int j = 0; j < 5; j++)
                        {
                            if (c_orb.ClassName == orb.Restricted_C[j])
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
            RaceOrb r_orb = null;
            RaceControl r_control = null;
            ClassOrb c_orb = null;
            ClassControl c_control = null;

            foreach (Item h in World.Items.Values)
            {
                if (h is RaceControl)
                    r_control = h as RaceControl;
            }

            foreach (Item h in World.Items.Values)
            {
                if (h is ClassControl)
                    c_control = h as ClassControl;
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

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassOrb)
                {
                    c_orb = i as ClassOrb;
                    if (info.ButtonID == c_orb.ClassNumber)
                    {
                        if (r_orb.Res_Class_Num != 5)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                if (r_orb.Restricted_C[j] == null)
                                {
                                    r_orb.Restricted_C[j] = c_orb.ClassName;
                                    r_orb.Res_Class_Num += 1;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            m_From.SendMessage(0x35, "You have reached the Maximum Restricted Classes");
                        }
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (info.ButtonID == i + 100)
                {
                    r_orb.Restricted_C[i] = null;
                    r_orb.Res_Class_Num -= 1;
                }
            }


            if (info.ButtonID == 0)
            {
                from.SendGump(new RestrictClassGump(from));
            }

        }
    }
}