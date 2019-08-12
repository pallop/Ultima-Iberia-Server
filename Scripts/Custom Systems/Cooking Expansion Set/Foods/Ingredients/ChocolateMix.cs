using System;
using Server.Network;

namespace Server.Items
{
    public class ChocolateMix : Item
    {
        [Constructable]
        public ChocolateMix()
            : base(0xE23)
        {
            Weight = 0.5;
            Stackable = true;
            Name = "Chocolate Mix";
            Hue = 0x414;
        }
        public ChocolateMix(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}