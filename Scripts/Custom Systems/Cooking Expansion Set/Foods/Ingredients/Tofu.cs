using System;
using Server.Network;

namespace Server.Items
{
    public class Tofu : Item
    {
        [Constructable]
        public Tofu()
            : base(0x1044)
        {
            Weight = 0.5;
            Stackable = true;
            Name = "Tofu";
            Hue = 0x38F;
        }
        public Tofu(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}