using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class BeefRoast : Food, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			if ( !Movable )
				return;

			base.ScissorHelper( from, new BeefRoastSlices(), 5 );

			from.SendMessage( "You slice the roast." );
		}

		[Constructable]
		public BeefRoast() : this( 1 )
		{
		}

		[Constructable]
		public BeefRoast( int amount ) : base( amount, 0x9C9 )
		{
			Weight = 1.0;
			FillFactor = 5;
			Name = "beef roast";
			Hue = 1831;
            Stackable = true;
            Amount = amount;
		}

		public BeefRoast( Serial serial ) : base( serial )
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