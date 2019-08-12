
using System;
using Server.Network;

namespace Server.Items
{
    public class PizzaCrust : Food
    {
        [Constructable]
        public PizzaCrust()
            : base(0x1083)
        {
            this.Weight = 0.5;
            this.Name = "Pizza Crust";
            this.Hue = 0x3FF;
        }
        public PizzaCrust(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}