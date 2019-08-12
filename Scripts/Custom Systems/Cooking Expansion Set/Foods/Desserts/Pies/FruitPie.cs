
using System;
using Server.Network;

namespace Server.Items
{
    public class FruitPie : Food
    {
        public override int LabelNumber { get { return 1041346; } }

        [Constructable]
        public FruitPie()
            : base(0x1041)
        {
            Weight = 1.0;
            FillFactor = 5;
        }

        public FruitPie(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}