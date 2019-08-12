
using System;
using Server.Network;

namespace Server.Items
{
    public class Sausage : Food
    {
        [Constructable]
        public Sausage() : this(1) { }

        [Constructable]
        public Sausage(int amount)
            : base(amount, 0x9C0)
        {
            Weight = 1.0;
            FillFactor = 1;
        }

        public Sausage(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}