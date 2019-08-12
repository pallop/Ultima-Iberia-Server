/************************************************************************************************/
/**********Echo Dynamic Race Class System V2.0, ©2005********************************************/
/************************************************************************************************/

using System;
using Server.Items;
using System.Text;
using Server.Network;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
    public class RaceStone : Item
    {
        [Constructable]
        public RaceStone()
            : base(0xED4)
        {
            Movable = false;
            Hue = 1154;
            Name = "Race/Class Setup Stone";
        }


        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add("This stone will setup your race and class");
        }

        public override void OnDoubleClick(Mobile from)
        {
            int m_Exists = from.Backpack.GetAmount(typeof(RCCONTROL));
            if (m_Exists == 0)
            {
                from.AddToBackpack(new RCCONTROL());
                from.SendGump(new RaceGump(from));
            }

            else
            {
                return;
            }

        }

        public RaceStone(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}