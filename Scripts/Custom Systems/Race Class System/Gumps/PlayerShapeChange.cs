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


    public class PlayerShapeChange : Gump
    {
        private const int FieldsPerPage = 14;

        private Mobile m_From;
        private Mobile m_Mobile;

        public PlayerShapeChange(Mobile from, RaceOrb orb)
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
            AddBackground(0, 0, 340, (rise * 25) + 80, 5054);

            AddImageTiled(10, 10, 320, 23, 0x52);
            AddImageTiled(11, 11, 318, 23, 0xBBC);

            AddLabel(100, 11, 0, "Race/Class System");

            AddLabel(30, 34, 0, "Shapeshift to which Form:");

            for (int i = 0; i < 5; i++)
            {
                if (orb.BodyValue[i] != 0)
                {
                    AddButton(11, h, 0x15E3, 0x15E7, i, GumpButtonType.Reply, 1);
                    AddLabel(30, h - 1, 0, orb.SS_Name[i]);
                    h += 25;
                }
            }

        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            RaceOrb r_orb = null;
            RaceControl r_control = null;
            RCCONTROL rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

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
                    if (rc.Race == r_orb.RaceName)
                        break;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (info.ButtonID == i)
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

            if (info.ButtonID == 0) { }

        }
    }

}