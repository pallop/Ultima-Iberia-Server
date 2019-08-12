using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
    public class CookedSteak : Food
    {
        [Constructable]
        public CookedSteak() : this(1) { }

        [Constructable]
        public CookedSteak(int amount)
            : base(amount, 0x9F1)
        {
            Weight = 1.0;
            FillFactor = 5;
            Name = "Cooked Steak";
        }

        public CookedSteak(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}