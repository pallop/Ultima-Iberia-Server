using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class RawGoatRoast : Food
	{
		[Constructable]
		public RawGoatRoast() : this( 1 )
		{
		}

		[Constructable]
        public RawGoatRoast(int amount)
            : base(amount, 0x9C9)
		{
			Name = "raw goat roast";
			Stackable = true;
			Weight = 1.0;
			Hue = 1194;
			Amount = amount;
            Raw = true;
		}

		public RawGoatRoast( Serial serial ) : base( serial )
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