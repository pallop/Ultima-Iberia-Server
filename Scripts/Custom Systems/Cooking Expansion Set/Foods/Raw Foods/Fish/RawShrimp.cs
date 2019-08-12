using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
    public class RawShrimp : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public RawShrimp() : this(1) { }

        [Constructable]
        public RawShrimp(int amount)
            : base(amount, 0x097A)
        {
            this.Stackable = true;
            this.Amount = amount;
            this.Name = "Raw Shrimp";
            this.Raw = true;
        }

        public RawShrimp(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}