
using System;
using Server.Network;

namespace Server.Items
{
    public class Venison : Food
    {
        [Constructable]
        public Venison() : this(1) { }

        [Constructable]
        public Venison(int amount)
            : base(amount, 0x9F2)
        {
            Weight = 1.0;
            Name = "Venison";
            FillFactor = 5;
            IntBoost = 2;
            StatBoostTime = 50;
        }

        public Venison(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}