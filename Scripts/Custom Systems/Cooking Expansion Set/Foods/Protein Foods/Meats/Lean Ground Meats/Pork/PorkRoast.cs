using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class PorkRoast : Food
	{
		[Constructable]
		public PorkRoast() : this( 1 )
		{
		}

		[Constructable]
		public PorkRoast( int amount ) : base( amount, 0x9C9 )
		{
			Amount = amount;
			Stackable = true;
			Weight = 1.0;
			FillFactor = 3;
			Hue = 1831;
			Name = "pork roast";
		}

		public PorkRoast( Serial serial ) : base( serial )
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