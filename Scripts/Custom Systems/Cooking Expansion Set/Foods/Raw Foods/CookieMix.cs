using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
    public class CookieMix : Food
    {
        [Constructable]
        public CookieMix()
            : base(0x103F)
        {
        }

        public CookieMix(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}