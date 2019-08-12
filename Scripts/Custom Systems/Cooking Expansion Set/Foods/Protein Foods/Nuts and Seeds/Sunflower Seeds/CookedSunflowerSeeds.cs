using System;
using Server.Network;
namespace Server.Items
{
    public class CookedSunflowerSeeds : Food
    {
        [Constructable]
        public CookedSunflowerSeeds() : this(1) { }
        [Constructable]
        public CookedSunflowerSeeds(int amount)
            : base(amount, 0xF27)
        {
            this.Weight = 0.1;
            this.Stackable = true;
            this.Hue = 0x44F;
            this.FillFactor = 2;
            this.Name = "Sunflower Seeds";
        }
        public CookedSunflowerSeeds(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
}