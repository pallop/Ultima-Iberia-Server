using System;
using Server.Network;

namespace Server.Items
{
    public class RawTurkeyLeg : Food
    {
        [Constructable]
        public RawTurkeyLeg() : this(1) { }

        [Constructable]
        public RawTurkeyLeg(int amount)
            : base(amount, 0x1607)
        {
            Name = "Raw Turkey Leg";
            Stackable = true;
            Amount = amount;
            Raw = true;
        }

        public RawTurkeyLeg(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}