using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
    public class RawRedSnapperSteak : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public RawRedSnapperSteak() : this(1) { }

        [Constructable]
        public RawRedSnapperSteak(int amount)
            : base(amount, 0x097A)
        {
            this.Stackable = true;
            this.Amount = amount;
            this.Name = "Raw Red Snapper Steak";
            this.Raw = true;
        }

        public RawRedSnapperSteak(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}