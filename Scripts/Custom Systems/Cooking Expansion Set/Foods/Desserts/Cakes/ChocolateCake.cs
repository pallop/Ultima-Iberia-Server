using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
    public class ChocolateCake : Food
    {
        [Constructable]
        public ChocolateCake() : this(1) { }
        [Constructable]
        public ChocolateCake(int amount)
            : base(amount, 0x9E9)
        {
            this.Weight = 2.0;
            this.FillFactor = 3;
            this.Hue = 0x45D;
            this.Name = "Chocolate Cake";
        }
        public ChocolateCake(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
}