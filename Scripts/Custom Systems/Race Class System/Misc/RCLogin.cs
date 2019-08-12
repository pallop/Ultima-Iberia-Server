using System;
using System.Collections;
using Server.Gumps;
using Server.Network;
using Server.Spells;
using Server.Items;
using Server.Mobiles;

namespace Server.CMemory
{
    public class RCLogin
    {
        public static void Initialize()
        {
            EventSink.Login += new LoginEventHandler(EventSink_Login);
        }

        private static void EventSink_Login(LoginEventArgs args)
        {

            Mobile from = args.Mobile;

            /******************Edited for Dynamic Race/Class System v2.0*******************************/

            RCChest chest = null;
            RCCONTROL rc = null;
            PlayerMobile pm = (PlayerMobile)from;

            int rc_Exists = from.Backpack.GetAmount(typeof(RCCONTROL));

            if (rc_Exists != 0)
            {
                rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;
            }

            if (rc_Exists == 0 || !rc.Active)
            {

                foreach (Item i in World.Items.Values)
                {
                    if (i is RCChest)
                    {
                        chest = i as RCChest;
                        if (chest.Active)
                        {
                            if (pm.AccessLevel == AccessLevel.Player || (pm.AccessLevel != AccessLevel.Player && chest.Staff_Login))
                            {
                                pm.Location = new Point3D(chest.Start_X, chest.Start_Y, chest.Start_Z);
                                pm.Map = chest.Start_Map;
                            }
                        }
                    }
                }

            }

            /******************************************************************************************/

        }
    }
}