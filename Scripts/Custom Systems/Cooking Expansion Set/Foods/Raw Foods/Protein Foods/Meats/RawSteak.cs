
using System;
using Server.Network;

namespace Server.Items
{
    public class RawSteak : Food
    {
        [Constructable]
        public RawSteak()
            : this(1)
        {
        }

        [Constructable]
        public RawSteak(int amount)
            : base(amount, 0x9F1)
        {
            Name = "Raw Steak";
            Stackable = true;
            Amount = amount;
            Raw = true;
        }

        public RawSteak(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}