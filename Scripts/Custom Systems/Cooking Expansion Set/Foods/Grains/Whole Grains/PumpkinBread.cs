using System;
using Server.Network;

namespace Server.Items
{
    public class PumpkinBread : Food
    {
        [Constructable]
        public PumpkinBread() : this(1) { }
        [Constructable]
        public PumpkinBread(int amount)
            : base(amount, 0x103B)
        {
            Weight = 1.0;
            FillFactor = 3;
            Name = "Pumpkin Bread";
            Hue = 0x1C1;
        }
        public PumpkinBread(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
}