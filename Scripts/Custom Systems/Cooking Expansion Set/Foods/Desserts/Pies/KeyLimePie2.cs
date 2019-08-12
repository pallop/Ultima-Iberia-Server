
using System;
using Server.Network;

namespace Server.Items
{
    public class KeyLimePie2 : Food
    {
        [Constructable]
        public KeyLimePie2() : this(1) { }

        [Constructable]
        public KeyLimePie2(int amount)
            : base(amount, 0x1042)
        {
            Weight = 1.0;
            FillFactor = 3;
            Name = "Key Lime Pie 2";
        }
        public KeyLimePie2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
}