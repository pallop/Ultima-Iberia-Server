/************************************************************************************************/
/**********Echo Dynamic Race Class System V2.0, ©2005********************************************/
/************************************************************************************************/


using System;
using Server.Mobiles;
using Server.Items;
using Server.Network;

namespace Server.Spells
{
    public class RestrictSpellBook
    {

        public static bool RestrictSB(Mobile from, Spellbook book)
        {


            ClassOrb c_orb = null;
            int rc_Exists = from.Backpack.GetAmount(typeof(RCCONTROL));
            RCCONTROL rc = null;
            bool isRestricted = false;

            if (rc_Exists != 0)
            {
                rc = from.Backpack.FindItemByType(typeof(RCCONTROL)) as RCCONTROL;

                foreach (Item i in World.Items.Values)
                {
                    if (i is ClassOrb)
                    {
                        c_orb = i as ClassOrb;
                        if (rc.P_Class == c_orb.ClassName)
                            break;
                    }
                }


                for (int i = 0; i < 20; i++)
                {
                    if (c_orb.Res_Books[i] != SpellbookType.Invalid)
                    {
                        if (c_orb.Res_Books[i] == book.SpellbookType)
                        {
                            from.SendMessage(6, "Your Class lacks the ability to use these Spells.");
                            isRestricted = true;
                            break;
                        }
                    }
                }

                if (isRestricted)
                    return true;
                else
                    return false;

            }

            return false;
        }
    }
}