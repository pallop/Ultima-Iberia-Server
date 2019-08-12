using System;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class EasterEggs : Food
    {
        public override int LabelNumber { get { return 1016105; } }

        [Constructable]
        public EasterEggs() : this(1) { }

        [Constructable]
        public EasterEggs(int amount)
            : base(amount, 0x9B5)
        {
            this.Stackable = true;
            this.Amount = amount;
            this.Weight = 0.5;
            this.Hue = 3 + (Utility.Random(20) * 5);
        }

        public EasterEggs(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}