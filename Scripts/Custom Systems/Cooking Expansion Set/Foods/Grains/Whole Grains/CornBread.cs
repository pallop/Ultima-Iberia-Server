using System;
using Server.Network;

namespace Server.Items
{
    public class CornBread : Food
    {
        [Constructable]
        public CornBread() : this(1) { }
        [Constructable]
        public CornBread(int amount)
            : base(amount, 0x103C)
        {
            Weight = 1.0;
            FillFactor = 3;
            Name = "Corn Bread";
            Hue = 0x1C7;
        }
        public CornBread(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
}