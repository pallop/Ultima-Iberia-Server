using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class BeefRoastSlices : Food
	{
		[Constructable]
		public BeefRoastSlices() : this( 1 )
		{
		}

		[Constructable]
		public BeefRoastSlices( int amount ) : base( amount, 0x1E1F )
		{
			Weight = 0.2;
			FillFactor = 1;
			Name = "beef roast slices";
			Hue = 1831;
            Stackable = true;
            Amount = amount;
		}

		public BeefRoastSlices( Serial serial ) : base( serial )
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