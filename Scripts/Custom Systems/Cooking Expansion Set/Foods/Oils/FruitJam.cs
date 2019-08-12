using System;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Items
{
    public class FruitJam : Item
    {
        [Constructable]
        public FruitJam()
            : base(0x1006)
        {
            Stackable = true;
            Name = "Mixed Fruit Jam";
            Hue = 0x18;
        }
        public FruitJam(Serial serial)
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