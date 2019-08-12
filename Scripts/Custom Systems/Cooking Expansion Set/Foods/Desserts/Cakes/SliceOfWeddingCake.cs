using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
    public class SliceOfWeddingCake : Food
    {
        [Constructable]
        public SliceOfWeddingCake() : this(1) { }

        [Constructable]
        public SliceOfWeddingCake(int amount)
            : base(amount, 0x3BCB)
        {
            Name = "Slice of Wedding Cake";
            this.Weight = 1.0;
            this.FillFactor = 1;
        }

        public SliceOfWeddingCake(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}