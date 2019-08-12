using System;
using Server.Network;

namespace Server.Items
{
    public class PastaNoodles : Item
    {
        [Constructable]
        public PastaNoodles()
            : base(0x1038)
        {
            Weight = 0.5;
            Stackable = true;
            Name = "Pasta Noodles";
            Hue = 0x100;
        }
        public PastaNoodles(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}