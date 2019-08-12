using System;
using Server.Network;

namespace Server.Items
{
    public class GroundPork : Item
    {
        [Constructable]
        public GroundPork() : this(1) { }

        [Constructable]
        public GroundPork(int amount)
            : base(9908)
        {
            Weight = 0.5;
            Stackable = true;
            Name = "Ground Pork";
            Hue = 0x221;
            Amount = amount;
        }
        public GroundPork(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}