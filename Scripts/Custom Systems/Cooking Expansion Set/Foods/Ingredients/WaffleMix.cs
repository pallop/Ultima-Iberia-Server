using System;
using Server.Network;

namespace Server.Items
{
    public class WaffleMix : Item
    {
        [Constructable]
        public WaffleMix()
            : base(0x103F)
        {
            Weight = 0.5;
            Stackable = true;
            Name = "Waffle Mix";
            Hue = 0x227;
        }
        public WaffleMix(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}