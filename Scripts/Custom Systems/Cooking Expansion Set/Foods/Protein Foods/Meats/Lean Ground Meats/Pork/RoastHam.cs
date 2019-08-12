using System;
using Server.Network;

namespace Server.Items
{
    [FlipableAttribute(0x3BC0, 0x3BBF)]
    public class RoastHam : Food
    {
        [Constructable]
		public RoastHam() : this( 1 )
		{
		}

        [Constructable]
        public RoastHam( int amount ) : base( amount, 0x3BC0)
        {
            Name = "Roast Ham";
            Weight = 10.0;
            FillFactor = 2;
            Amount = amount;
            Stackable = true;
        }

        public RoastHam(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}