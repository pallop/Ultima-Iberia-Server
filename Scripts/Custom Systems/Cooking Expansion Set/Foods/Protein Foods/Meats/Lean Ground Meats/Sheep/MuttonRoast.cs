using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class MuttonRoast : Food, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			if ( !Movable )
				return;

			base.ScissorHelper( from, new MuttonRoastSlices(), 5 );

			from.SendMessage( "You slice the roast." );
		}

		[Constructable]
		public MuttonRoast() : this( 1 )
		{
		}

		[Constructable]
		public MuttonRoast( int amount ) : base( amount, 0x9C9 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
			Name = "mutton roast";
			Hue = 1831;
		}

		public MuttonRoast( Serial serial ) : base( serial )
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