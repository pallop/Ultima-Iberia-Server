
using System;
using Server.Network;

namespace Server.Items
{
    public class UnbakedApplePie : Food
    {
        public override int LabelNumber { get { return 1041336; } }

        [Constructable]
        public UnbakedApplePie()
            : base(0x1042)
        {
        }

        public UnbakedApplePie(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}