using System;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Items
{
    public class Vinegar : Item
    {
        [Constructable]
        public Vinegar()
            : base(0x99B)
        {
            Stackable = true;
            Name = "Vinegar";
            Hue = 0x0;
        }
        public Vinegar(Serial serial)
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