using System;
using Server.Network;

namespace Server.Items
{
    public class BananaBread : Food
    {
        [Constructable]
        public BananaBread() : this(1) { }
        [Constructable]
        public BananaBread(int amount)
            : base(amount, 0x103B)
        {
            Weight = 1.0;
            FillFactor = 3;
            Name = "Banana Bread";
        }
        public BananaBread(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
}