using System;
using Server.Network;

namespace Server.Items
{
    public class PumpkinMuffins : Food
    {
        [Constructable]
        public PumpkinMuffins() : this(1) { }
        [Constructable]
        public PumpkinMuffins(int amount)
            : base(amount, 0x9EB)
        {
            this.Weight = 1.0;
            this.FillFactor = 3;
            this.Hue = 0x1C0;
            this.Name = "Pumpkin Muffins";
        }
        public PumpkinMuffins(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
}