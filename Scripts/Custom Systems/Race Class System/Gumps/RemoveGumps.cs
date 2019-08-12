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


    public class RemoveRaceGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public RemoveRaceGump(Mobile from)
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

            AddLabel(30, 34, 0, "Remove Which Race?");

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
            RCCONTROL rc = null;
            RaceControl r_control = null;
            Bag d_bag = new Bag();

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
                        foreach (Item j in World.Items.Values)
                        {
                            if (j is RCCONTROL)
                            {
                                rc = j as RCCONTROL;
                                if (rc.Race == r_orb.RaceName)
                                    d_bag.AddItem(rc);
                            }
                        }
                        d_bag.Delete();
                        r_orb.Delete();
                        r_control.A_Races -= 1;
                        resetROrbNums();
                        break;
                    }
                }
            }

            if (info.ButtonID == 0) { }

        }

        public void resetROrbNums()
        {

            RaceOrb r_orb = null;
            int new_num = 1;

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceOrb)
                {
                    r_orb = i as RaceOrb;
                    r_orb.RaceNumber = new_num;
                    new_num += 1;
                }
            }
        }

    }



    public class RemoveClassGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public RemoveClassGump(Mobile from)
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

            AddLabel(30, 34, 0, "Remove Which Class?");

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
            RCCONTROL rc = null;
            ClassControl c_control = null;
            Bag d_bag = new Bag();

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
                        foreach (Item j in World.Items.Values)
                        {
                            if (j is RCCONTROL)
                            {
                                rc = j as RCCONTROL;
                                if (rc.P_Class == c_orb.ClassName)
                                    d_bag.AddItem(rc);
                            }
                        }
                        d_bag.Delete();
                        foreach (Item k in World.Items.Values)
                        {
                            if (k is Bag && k.Name == c_orb.ClassName + " Class")
                            {
                                k.Delete();
                                break;
                            }
                        }
                        c_control.A_Classes -= 1;
                        resetCOrbNums();
                        break;
                    }
                }
            }


            if (info.ButtonID == 0) { }

        }

        public void resetCOrbNums()
        {

            ClassOrb c_orb = null;
            int new_num = 1;

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassOrb)
                {
                    c_orb = i as ClassOrb;
                    c_orb.ClassNumber = new_num;
                    new_num += 1;
                }
            }
        }

    }


    public class RemoveBPGump : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public RemoveBPGump(Mobile from)
            : base(20, 30)
        {
            m_From = from;
            BonusPackOrb b_orb = null;
            BonusPackControl b_control = null;
            int h = 60;

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackControl)
                    b_control = i as BonusPackControl;
            }

            AddPage(0);
            AddBackground(0, 0, 340, (b_control.A_BP * 25) + 80, 5054);

            AddImageTiled(10, 10, 320, 23, 0x52);
            AddImageTiled(11, 11, 318, 23, 0xBBC);

            AddLabel(100, 11, 0, "Race/Class System");

            AddLabel(30, 34, 0, "Remove Which Bonus Pack?");

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackOrb)
                {
                    b_orb = i as BonusPackOrb;
                    if (b_orb.Activated)
                    {
                        AddButton(11, h, 0x15E3, 0x15E7, b_orb.BPNumber, GumpButtonType.Reply, 1);
                        AddLabel(30, h - 1, 0, b_orb.BPName + " Bonus Pack");
                        h += 25;
                    }
                }
            }


        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            BonusPackOrb b_orb = null;
            RCCONTROL rc = null;
            BonusPackControl b_control = null;
            Bag d_bag = new Bag();

            foreach (Item h in World.Items.Values)
            {
                if (h is BonusPackControl)
                    b_control = h as BonusPackControl;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackOrb)
                {
                    b_orb = i as BonusPackOrb;
                    if (info.ButtonID == b_orb.BPNumber)
                    {
                        foreach (Item j in World.Items.Values)
                        {
                            if (j is RCCONTROL)
                            {
                                rc = j as RCCONTROL;
                                if (rc.BonusPack == b_orb.BPName)
                                    d_bag.AddItem(rc);
                            }
                        }
                        d_bag.Delete();
                        foreach (Item k in World.Items.Values)
                        {
                            if (k is Bag && k.Name == b_orb.BPName + " Bonus Pack")
                            {
                                k.Delete();
                                break;
                            }
                        }
                        b_control.A_BP -= 1;
                        resetBOrbNums();
                        break;
                    }
                }
            }


            if (info.ButtonID == 0) { }

        }

        public void resetBOrbNums()
        {

            BonusPackOrb b_orb = null;
            int new_num = 1;

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackOrb)
                {
                    b_orb = i as BonusPackOrb;
                    b_orb.BPNumber = new_num;
                    new_num += 1;
                }
            }
        }

    }
}