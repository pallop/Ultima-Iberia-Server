using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class RawBaconSlab : Food, ICarvable
	{
		[Constructable]
		public RawBaconSlab() : this( 1 )
		{
		}

		[Constructable]
        public RawBaconSlab(int amount)
            : base(amount, 0x976)
		{
			Name = "raw slab of bacon";
			Stackable = true;
            Amount = amount;
            Weight = 1.0;
            Raw = true;
			//Hue = 41;
		}

		public void Carve( Mobile from, Item item )
		{
			if ( !Movable )
				return;

			base.ScissorHelper( from, new RawBacon(), 5 );
			from.SendMessage( "You cut the slab into slices." );
		}

		public RawBaconSlab( Serial serial ) : base( serial )
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