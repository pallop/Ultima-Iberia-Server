/************************************************************************************************/
/**********Echo Dynamic Race Class System V1.0, ©2005********************************************/
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

    public class RaceBag
    {
        public static void Initialize()
        {
            //CommandSystem.Register( "RaceBag", AccessLevel.Administrator, new CommandEventHandler( OnRaceBag ) );
            CommandSystem.Register("Update", AccessLevel.Administrator, new CommandEventHandler(OnUpdate));

        }

        [Usage("RaceBag [amount]")]
        private static void OnRaceBag(CommandEventArgs e)
        {

            int amount = 0;
            if (e.Length >= 1)
                amount = e.GetInt32(0);

            Mobile from = e.Mobile;
            bool r_active = false;
            RaceOrb r_orb = null;
            Bag m_bag = null;
            RaceControl r_control = null;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }

            foreach (Item i in World.Items.Values)
            {
                if (i is RaceControl)
                    r_control = i as RaceControl;
            }

            if (r_active)
            {


                foreach (Item i in World.Items.Values)
                {
                    if (i is RaceOrb)
                    {
                        r_orb = i as RaceOrb;
                        if (r_orb.RaceNumber == amount)
                        {
                            Bag r_bag = new Bag();
                            r_bag.Name = r_orb.RaceName + " Race";
                            Bag i_bag = new Bag();
                            i_bag.Name = r_orb.RaceName + " Items";
                            r_bag.AddItem(i_bag);
                            r_bag.AddItem(r_orb);
                            foreach (Item k in World.Items.Values)
                            {
                                if (k is Bag && k.Name == "RACES")
                                {
                                    m_bag = k as Bag;
                                    m_bag.AddItem(r_bag);
                                    break;
                                }
                            }
                            r_orb.Activated = true;
                            break;

                        }
                    }
                }
            }
        }

        private static void OnUpdate(CommandEventArgs e)
        {

            Mobile from = e.Mobile;
            bool r_active = false;
            ClassOrb c_orb = null;
            RaceOrb r_orb = null;
            BonusPackOrb b_orb = null;
            Bag c_bag = null;
            RCCONTROL rc = null;

            foreach (Item i in World.Items.Values)
            {
                if (i is RCChest)
                    r_active = true;
            }

            if (r_active)
            {

                ArrayList items = new ArrayList(World.Items.Values);

                foreach (Item h in items)
                {
                    if (h is ClassOrb)
                    {
                        c_orb = h as ClassOrb;
                        foreach (Item i in items)
                        {
                            if (i is Bag && i.Name == c_orb.ClassName + " Class")
                            {
                                c_bag = i as Bag;
                                Bag n_bag = new Bag();
                                n_bag.Name = c_orb.ClassName + " Restricted Armors";
                                Bag n_bag2 = new Bag();
                                n_bag2.Name = c_orb.ClassName + " Restricted Weapons";
                                c_bag.AddItem(n_bag);
                                c_bag.AddItem(n_bag2);
                                break;
                            }
                        }
                    }
                }

                foreach (Item h in World.Items.Values)
                {
                    if (h is RCCONTROL)
                    {
                        rc = h as RCCONTROL;

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

                        foreach (Item i in World.Items.Values)
                        {
                            if (i is ClassOrb)
                            {
                                c_orb = i as ClassOrb;
                                c_orb.BackUpName = c_orb.ClassName;
                            }
                        }

                        foreach (Item i in World.Items.Values)
                        {
                            if (i is RaceOrb)
                            {
                                r_orb = i as RaceOrb;
                                r_orb.BackUpName = r_orb.RaceName;
                            }
                        }

                        foreach (Item i in World.Items.Values)
                        {
                            if (i is BonusPackOrb)
                            {
                                b_orb = i as BonusPackOrb;
                                b_orb.BackUpName = b_orb.BPName;
                            }
                        }

                    }
                }


            }
        }




    }
}