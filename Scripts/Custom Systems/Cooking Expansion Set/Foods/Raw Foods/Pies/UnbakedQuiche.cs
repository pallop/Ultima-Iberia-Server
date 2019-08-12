
using System;
using Server.Network;

namespace Server.Items
{
    public class UnbakedQuiche : Food
    {
        public override int LabelNumber { get { return 1041339; } }

        [Constructable]
        public UnbakedQuiche()
            : base(0x1042)
        {
        }

        public UnbakedQuiche(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}