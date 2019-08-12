
using System;
using Server.Network;

namespace Server.Items
{
    public class Quiche : Food
    {
        public override int LabelNumber { get { return 1041345; } }

        [Constructable]
        public Quiche()
            : base(0x1041)
        {
            Weight = 1.0;
            FillFactor = 5;
        }

        public Quiche(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}