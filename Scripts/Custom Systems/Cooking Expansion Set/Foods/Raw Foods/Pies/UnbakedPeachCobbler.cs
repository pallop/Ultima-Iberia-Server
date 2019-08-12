
using System;
using Server.Network;

namespace Server.Items
{
    public class UnbakedPeachCobbler : Food
    {
        public override int LabelNumber { get { return 1041335; } }

        [Constructable]
        public UnbakedPeachCobbler()
            : base(0x1042)
        {
        }

        public UnbakedPeachCobbler(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}