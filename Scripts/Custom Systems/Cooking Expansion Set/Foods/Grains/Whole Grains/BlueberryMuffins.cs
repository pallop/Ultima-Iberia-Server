using System;
using Server.Network;

namespace Server.Items
{
    public class BlueberryMuffins : Food
    {
        [Constructable]
        public BlueberryMuffins() : this(1) { }
        [Constructable]
        public BlueberryMuffins(int amount)
            : base(amount, 0x9EB)
        {
            Weight = 1.0;
            FillFactor = 3;
            Hue = 0x1FC;
            Name = "Blueberry Muffins";
        }
        public BlueberryMuffins(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
}