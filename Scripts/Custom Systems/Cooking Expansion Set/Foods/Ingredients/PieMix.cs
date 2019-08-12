using System;
using Server.Network;

namespace Server.Items
{
    public class PieMix : Item
    {
        [Constructable]
        public PieMix()
            : base(0x103F)
        {
            Weight = 0.5;
            Stackable = true;
            Name = "Pie Mix";
            Hue = 0x45A;
        }
        public PieMix(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}