using System;
using Server.Network;

namespace Server.Items
{
    public class RawDuckLeg : Food
    {
        [Constructable]
        public RawDuckLeg() : this(1) { }

        [Constructable]
        public RawDuckLeg(int amount)
            : base(amount, 0x1607)
        {
            Name = "Raw Duck Leg";
            Stackable = true;
            Amount = amount;
            Raw = true;
        }

        public RawDuckLeg(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}