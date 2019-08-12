using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
    public class RawSalmonSteak : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public RawSalmonSteak() : this(1) { }

        [Constructable]
        public RawSalmonSteak(int amount)
            : base(amount, 0x097A)
        {
            this.Stackable = true;
            this.Amount = amount;
            this.Name = "Raw Salmon Steak";
            this.Raw = true;
        }

        public RawSalmonSteak(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}