using System;
using Server.Network;

namespace Server.Items
{
    public class Muffins : Food
    {
        [Constructable]
        public Muffins()
            : base(0x9eb)
        {
            this.Weight = 1.0;
            this.FillFactor = 4;
        }

        public Muffins(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}