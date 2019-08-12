
using System;
using Server.Network;

namespace Server.Items
{
    public class ChickenPie : Food
    {
        [Constructable]
        public ChickenPie() : this(1) { }

        [Constructable]
        public ChickenPie(int amount)
            : base(amount, 0x1042)
        {
            Weight = 1.0;
            FillFactor = 3;
            Name = "Chicken Pie";
        }
        public ChickenPie(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
}