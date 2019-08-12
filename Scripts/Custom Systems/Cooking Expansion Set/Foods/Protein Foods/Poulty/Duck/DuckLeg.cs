
using System;
using Server.Network;

namespace Server.Items
{
    public class DuckLeg : Food
    {
        [Constructable]
        public DuckLeg() : this(1) { }

        [Constructable]
        public DuckLeg(int amount)
            : base(amount, 0x1608)
        {
            this.Weight = 1.0;
            this.FillFactor = 4;
        }

        public DuckLeg(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}