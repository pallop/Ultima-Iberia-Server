
using System;
using Server.Network;

namespace Server.Items
{
    [TypeAlias("Server.Items.UncookedPizza")]
    public class UncookedCheesePizza : Food
    {
        public override int LabelNumber { get { return 1041341; } }

        [Constructable]
        public UncookedCheesePizza()
            : base(0x1083)
        {
        }

        public UncookedCheesePizza(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}