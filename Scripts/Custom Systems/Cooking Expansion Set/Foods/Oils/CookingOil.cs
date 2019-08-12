using System;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Items
{
    public class CookingOil : Item
    {
        [Constructable]
        public CookingOil()
            : base(0xE2B)
        {
            Stackable = true;
            Name = "Cooking Oil";
            Hue = 0x2D6;
        }
        public CookingOil(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}