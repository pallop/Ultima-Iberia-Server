
using System;
using Server.Network;

namespace Server.Items
{
    public class UnbakedFruitPie : Food
    {
        public override int LabelNumber { get { return 1041334; } }

        [Constructable]
        public UnbakedFruitPie()
            : base(0x1042)
        {
        }

        public UnbakedFruitPie(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}