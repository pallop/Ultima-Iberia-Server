
using System;
using Server.Network;

namespace Server.Items
{
    public class HalibutFishSteak : Food
    {
        public override double DefaultWeight
        {
            get { return 0.1; }
        }

        [Constructable]
        public HalibutFishSteak() : this(1) { }

        [Constructable]
        public HalibutFishSteak(int amount)
            : base(amount, 0x97B)
        {
            this.FillFactor = 3;
            Name = "Halibut Fish Steak";
        }

        public HalibutFishSteak(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}