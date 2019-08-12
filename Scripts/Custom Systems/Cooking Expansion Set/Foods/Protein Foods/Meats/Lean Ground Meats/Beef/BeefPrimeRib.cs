using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class BeefPrimeRib : Food
	{
		[Constructable]
		public BeefPrimeRib() : this( 1 )
		{
		}

		[Constructable]
		public BeefPrimeRib( int amount ) : base( amount, 0x9F2 )
		{
			Weight = 1.0;
			FillFactor = 5;
			Name = "a prime rib steak";
            Stackable = true;
            Amount = amount;
		}

		public BeefPrimeRib( Serial serial ) : base( serial )
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