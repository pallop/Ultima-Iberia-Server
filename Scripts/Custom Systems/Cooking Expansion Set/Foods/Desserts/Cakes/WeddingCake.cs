using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
    public class WeddingCake : Food
    {
        [Constructable]
        public WeddingCake()
            : base(0x3BCC)
        {
            Name = "Wedding Cake";
            this.Weight = 10.0;
            this.FillFactor = 10;
        }

        public WeddingCake(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}