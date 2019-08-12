using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class PorkChop : Food
	{
		[Constructable]
		public PorkChop() : this( 1 )
		{
		}

		[Constructable]
		public PorkChop( int amount ) : base( amount, 0x9F2 )
		{
			Weight = 1.0;
			Stackable = true;
			Amount = amount;
			FillFactor = 2;
			Name = "pork chop";
		}

		public PorkChop( Serial serial ) : base( serial )
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