using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class RawSpareRibs : Food
	{
		[Constructable]
		public RawSpareRibs() : this( 1 )
		{
		}

		[Constructable]
        public RawSpareRibs(int amount)
            : base(amount, 0x9F1)
		{
			Name = "raw spare ribs";
			Weight = 1.0;
            Stackable = true;
            Amount = amount;
            Raw = true;
		}

		public RawSpareRibs( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}