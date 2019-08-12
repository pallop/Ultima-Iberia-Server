using System;
using Server.Network;

namespace Server.Items
{
    public class GarlicBread : Food
    {
        [Constructable]
        public GarlicBread() : this(1) { }
        [Constructable]
        public GarlicBread(int amount)
            : base(amount, 0x98C)
        {
            Weight = 1.0;
            FillFactor = 3;
            Name = "Garlic Bread";
            Hue = 0x1C8;
        }
        public GarlicBread(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
}