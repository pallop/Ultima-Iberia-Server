/************************************************************************************************/
/**********Echo Dynamic Race Class System V2.0, ©2005********************************************/
/************************************************************************************************/

using System;
using Server;
using Server.Items;
using Server.Targeting;
using System.Collections;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using System.IO;
using Server.RaceClass;
using Server.Prompts;
using Server.Commands;

namespace Server.RaceClass
{

    public class RCCommands
    {
        public static void Initialize()
        {
            CommandSystem.Register("RCBegin", AccessLevel.Administrator, new CommandEventHandler(OnInitRaceClass));
            CommandSystem.Register("AddRace", AccessLevel.Administrator, new CommandEventHandler(OnAddRace));
            CommandSystem.Register("AddClass", AccessLevel.Administrator, new CommandEventHandler(OnAddClass));
            CommandSystem.Register("AddBP", AccessLevel.Administrator, new CommandEventHandler(OnAddBP));
            CommandSystem.Register("RemoveRace", AccessLevel.Administrator, new CommandEventHandler(OnRemoveRace));
            CommandSystem.Register("RemoveClass", AccessLevel.Administrator, new CommandEventHandler(OnRemoveClass));
            CommandSystem.Register("RemoveBP", AccessLevel.Administrator, new CommandEventHandler(OnRemoveBP));
            CommandSystem.Register("RCRemove", AccessLevel.Administrator, new CommandEventHandler(OnRCRemove));
            CommandSystem.Register("RestrictClass", AccessLevel.Administrator, new CommandEventHandler(OnRestrictClass));
            CommandSystem.Register("RCclean", AccessLevel.Administrator, new CommandEventHandler(OnClean));
            CommandSystem.Register("RaceHues", AccessLevel.Administrator, new CommandEventHandler(OnHue));
            CommandSystem.Register("EditSS", AccessLevel.Administrator, new CommandEventHandler(OnShapeChange));
            CommandSystem.Register("LoadSB", AccessLevel.Administrator, new CommandEventHandler(OnLoadSpellbook));
            CommandSystem.Register("RestrictSB", AccessLevel.Administrator, new CommandEventHandler(OnRestrictSpellbook));
            CommandSystem.Register("SyncNames", AccessLevel.Administrator, new CommandEventHandler(OnSyncNames));

            CommandSystem.Register("SC", AccessLevel.Player, new CommandEventHandler(OnSC));
        }

        private static void OnInitRaceClass(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            bool RaceClassOn = false;
            foreach (Item items in World.Items.Values)
            {
                if (items is RCChest)
                {
                    RaceClassOn = true;
                }
            }
            if (RaceClassOn)
            {
                from.SendMessage(6, "Race Class System is Already Active");
            }
            else
            {
                RCChest chest = new RCChest();
                chest.Location = new Point3D(from.X + 1, from.Y + 1, from.Z);
                chest.Map = from.Map;
                chest.AddItem(new RaceControl());
                chest.AddItem(new ClassControl());

                Container r_bag = new Bag();
                r_bag.Name = "RACES";
                chest.AddItem(r_bag);

                Container c_bag = new Bag();
                c_bag.Name = "CLASSES";
                chest.AddItem(c_bag);

                Container b_bag = new Bag();
                b_bag.Name = "BONUS PACKS";
                c_bag.AddItem(b_bag);
                c_bag.AddItem(new BonusPackControl());
            }

        }

        private static void OnAddRace(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            RaceControl r_control = null;
            bool r_active = false;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }

            if (r_active)
            {
                RaceOrb r_orb = new RaceOrb();

                foreach (Item i in World.Items.Values)
                {
                    if (i is RaceControl)
                        r_control = i as RaceControl;
                }

                if (r_control != null)
                {
                    r_orb.RaceNumber = r_control.A_Races + 1;
                    r_control.A_Current = r_orb.RaceNumber;
                }

                from.SendGump(new AddRaceGump(from, r_orb));
            }
        }


        private static void OnAddClass(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            ClassControl c_control = null;
            bool r_active = false;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }

            if (r_active)
            {
                ClassOrb c_orb = new ClassOrb();

                foreach (Item i in World.Items.Values)
                {
                    if (i is ClassControl)
                        c_control = i as ClassControl;
                }

                if (c_control != null)
                {
                    c_orb.ClassNumber = c_control.A_Classes + 1;
                    c_control.C_Current = c_orb.ClassNumber;
                }

                from.SendGump(new AddClassGump(from, c_orb));
            }
        }

        private static void OnAddBP(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            BonusPackControl b_control = null;
            bool r_active = false;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }

            if (r_active)
            {
                BonusPackOrb b_orb = new BonusPackOrb();

                foreach (Item i in World.Items.Values)
                {
                    if (i is BonusPackControl)
                        b_control = i as BonusPackControl;
                }

                if (b_control != null)
                {
                    b_orb.BPNumber = b_control.A_BP + 1;
                    b_control.B_Current = b_orb.BPNumber;
                }

                from.SendGump(new BonusPackGump(from, b_orb));
            }
        }


        private static void OnRemoveRace(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            bool r_active = false;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }

            if (r_active)
            {

                from.SendGump(new RemoveRaceGump(from));
            }
        }

        private static void OnRemoveClass(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            bool r_active = false;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }

            if (r_active)
            {

                from.SendGump(new RemoveClassGump(from));
            }
        }

        private static void OnRemoveBP(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            bool r_active = false;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }

            if (r_active)
            {

                from.SendGump(new RemoveBPGump(from));
            }
        }

        private static void OnRCRemove(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            Bag d_bag = new Bag();

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    d_bag.AddItem(i);
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceControl)
                    d_bag.AddItem(i);
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceOrb)
                    d_bag.AddItem(i);
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassControl)
                    d_bag.AddItem(i);
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassOrb)
                    d_bag.AddItem(i);
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackControl)
                    d_bag.AddItem(i);
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is BonusPackOrb)
                    d_bag.AddItem(i);
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is RCCONTROL)
                    d_bag.AddItem(i);
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceStone)
                    d_bag.AddItem(i);
            }

            d_bag.Delete();

        }

        private static void OnRestrictClass(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            bool r_active = false;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }

            if (r_active)
            {

                from.SendGump(new RestrictClassGump(from));
            }
        }

        private static void OnClean(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            bool r_active = false;
            Bag c_bag = new Bag();
            ClassOrb c_orb = null;
            RaceOrb r_orb = null;
            BonusPackOrb b_orb = null;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }

            if (r_active)
            {

                foreach (Item i in World.Items.Values)
                {
                    if (i is ClassOrb)
                    {
                        c_orb = i as ClassOrb;
                        if (!c_orb.Activated)
                            c_bag.AddItem(c_orb);
                    }
                }

                foreach (Item i in World.Items.Values)
                {
                    if (i is RaceOrb)
                    {
                        r_orb = i as RaceOrb;
                        if (!r_orb.Activated)
                            c_bag.AddItem(r_orb);
                    }
                }

                foreach (Item i in World.Items.Values)
                {
                    if (i is BonusPackOrb)
                    {
                        b_orb = i as BonusPackOrb;
                        if (!b_orb.Activated)
                            c_bag.AddItem(b_orb);
                    }
                }

                c_bag.Delete();
            }
        }

        private static void OnHue(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            bool r_active = false;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }

            if (r_active)
            {

                from.SendGump(new HueEditGump(from));
            }
        }

        private static void OnShapeChange(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            bool r_active = false;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }

            if (r_active)
            {

                from.SendGump(new ShapeChangeGump(from));
            }
        }

        private static void OnSC(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            bool r_active = false;
            RCCONTROL rc = null;
            RaceOrb r_orb = null;
            int rc_Exists = from.Backpack.GetAmount(typeof(RCCONTROL));
            int a_ss = 0;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }

            if (rc_Exists != 0)
            {

                rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

                foreach (Item i in World.Items.Values)
                {
                    if (i is RaceOrb)
                    {
                        r_orb = i as RaceOrb;
                        if (rc.Race == r_orb.RaceName)
                            break;
                    }
                }

                if (r_active)
                {

                    for (int i = 0; i < 5; i++)
                    {
                        if (r_orb.BodyValue[i] != 0)
                            a_ss += 1;
                    }

                    if (a_ss == 0)
                        from.SendMessage(6, "Your Race Cannot Shapeshift");
                    if (a_ss == 1)
                    {

                        if (from.BodyMod != 0)
                        {
                            from.PlaySound(0x228);
                            from.BodyMod = 0;
                            from.HueMod = -1;
                            from.NameMod = null;
                        }

                        else
                        {

                            for (int i = 0; i < 5; i++)
                            {
                                if (r_orb.BodyValue[i] != 0)
                                {
                                    if (from.Mount != null)
                                        from.Mount.Rider = null;
                                    from.PlaySound(0x228);
                                    from.BodyMod = r_orb.BodyValue[i];
                                    from.HueMod = r_orb.SS_Hue[i];
                                    from.NameMod = r_orb.SS_Name[i];
                                }
                            }
                        }
                    }

                    if (a_ss > 1)
                    {

                        if (from.BodyMod != 0)
                        {
                            from.PlaySound(0x228);
                            from.BodyMod = 0;
                            from.HueMod = -1;
                            from.NameMod = null;
                        }
                        else
                            from.SendGump(new PlayerShapeChange(from, r_orb));
                    }
                }
            }
        }


        private static void OnLoadSpellbook(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            bool r_active = false;
            ClassControl c_control = null;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }


            if (r_active)
            {

                foreach (Item i in World.Items.Values)
                {
                    if (i is ClassControl)
                        c_control = i as ClassControl;
                }

                if (c_control.Loaded_Books == 20)
                    from.SendMessage(0x35, "Cannot load anymore Spellbooks");
                else
                    from.Target = new SpellbookTarget();
            }
        }


        private static void OnRestrictSpellbook(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            bool r_active = false;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }


            if (r_active)
            {

                from.SendGump(new ResSBGump(from));

            }
        }

        private static void OnSyncNames(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            bool r_active = false;
            ClassOrb c_orb = null;
            RCCONTROL rc = null;
            RaceOrb r_orb = null;
            BonusPackOrb b_orb = null;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }


            if (r_active)
            {

                foreach (Item i in World.Items.Values)
                {
                    if (i is ClassOrb)
                    {
                        c_orb = i as ClassOrb;
                        if (c_orb.ClassName != c_orb.BackUpName)
                        {
                            c_orb.Name = c_orb.ClassName + " Orb";
                            foreach (Item j in World.Items.Values)
                            {
                                if (j is Bag && j.Name == c_orb.BackUpName + " Class")
                                    j.Name = c_orb.ClassName + " Class";
                                if (j is Bag && j.Name == c_orb.BackUpName + " Items")
                                    j.Name = c_orb.ClassName + " Items";
                                if (j is Bag && j.Name == c_orb.BackUpName + " Equip")
                                    j.Name = c_orb.ClassName + " Equip";
                                if (j is Bag && j.Name == c_orb.BackUpName + " Restricted Armors")
                                    j.Name = c_orb.ClassName + " Restricted Armors";
                                if (j is Bag && j.Name == c_orb.BackUpName + " Restricted Weapons")
                                    j.Name = c_orb.ClassName + " Restricted Weapons";
                            }
                            foreach (Item k in World.Items.Values)
                            {
                                if (k is RCCONTROL)
                                {
                                    rc = k as RCCONTROL;
                                    if (rc.P_Class == c_orb.BackUpName)
                                    {
                                        rc.P_Class = c_orb.ClassName;
                                    }
                                }
                            }

                            c_orb.BackUpName = c_orb.ClassName;
                        }
                    }
                }

                foreach (Item i in World.Items.Values)
                {
                    if (i is RaceOrb)
                    {
                        r_orb = i as RaceOrb;
                        if (r_orb.RaceName != r_orb.BackUpName)
                        {
                            r_orb.Name = r_orb.RaceName + " Orb";
                            foreach (Item j in World.Items.Values)
                            {
                                if (j is Bag && j.Name == r_orb.BackUpName + " Race")
                                    j.Name = r_orb.RaceName + " Race";
                                if (j is Bag && j.Name == r_orb.BackUpName + " Items")
                                    j.Name = r_orb.RaceName + " Items";
                            }
                            foreach (Item k in World.Items.Values)
                            {
                                if (k is RCCONTROL)
                                {
                                    rc = k as RCCONTROL;
                                    if (rc.Race == r_orb.BackUpName)
                                    {
                                        rc.Race = r_orb.RaceName;
                                    }
                                }
                            }

                            r_orb.BackUpName = r_orb.RaceName;
                        }
                    }
                }

                foreach (Item i in World.Items.Values)
                {
                    if (i is BonusPackOrb)
                    {
                        b_orb = i as BonusPackOrb;
                        if (b_orb.BPName != b_orb.BackUpName)
                        {
                            b_orb.Name = b_orb.BPName + " Orb";
                            foreach (Item j in World.Items.Values)
                            {
                                if (j is Bag && j.Name == b_orb.BackUpName + " Bonus Pack")
                                    j.Name = b_orb.BPName + " Bonus Pack";
                                if (j is Bag && j.Name == b_orb.BackUpName + " Items")
                                    j.Name = b_orb.BPName + " Items";
                            }
                            foreach (Item k in World.Items.Values)
                            {
                                if (k is RCCONTROL)
                                {
                                    rc = k as RCCONTROL;
                                    if (rc.BonusPack == b_orb.BackUpName)
                                    {
                                        rc.BonusPack = b_orb.BPName;
                                    }
                                }
                            }

                            b_orb.BackUpName = b_orb.BPName;
                        }
                    }
                }

            }
        }

    }


    public class SpellbookTarget : Target
    {
        public SpellbookTarget()
            : base(-1, true, TargetFlags.None)
        {
        }

        public SpellbookTarget(Mobile from, Item targeted)
            : base(-1, true, TargetFlags.None)
        {
        }

        protected override void OnTarget(Mobile from, object targeted)
        {

            ClassControl c_control = null;
            bool isLoaded = false;

            foreach (Item i in World.Items.Values)
            {
                if (i is ClassControl)
                    c_control = i as ClassControl;
            }

            if (from.Name == null)
            {
                from.SendMessage("Your name is not valid fix it now");
                return;
            }

            if (targeted is Spellbook)
            {
                Spellbook m_book = (Spellbook)targeted;

                for (int i = 0; i < 20; i++)
                {
                    if (m_book.SpellbookType == c_control.LoadBook[i])
                    {
                        isLoaded = true;
                        break;
                    }
                }


                if (isLoaded)
                {
                    from.SendMessage(0x35, "That Type of Spellbook has already been loaded");
                }

                else
                {
                    c_control.LoadBook[c_control.Loaded_Books] = m_book.SpellbookType;
                    c_control.Loaded_Books += 1;
                    from.SendMessage(6, m_book.SpellbookType + " Spellbook Loaded");
                    m_book.Delete();
                }
            }
            else
            {
                from.SendMessage(0x35, "That is not a Spellbook");
            }
        }

    }
}