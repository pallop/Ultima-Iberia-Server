
using System;
using Server.Network;

namespace Server.Items
{
    public class ApplePie : Food
    {
        public override int LabelNumber { get { return 1041343; } }

        [Constructable]
        public ApplePie()
            : base(0x1041)
        {
            Weight = 1.0;
            FillFactor = 5;
        }

        public ApplePie(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}