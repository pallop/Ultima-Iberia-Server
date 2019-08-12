using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 7822, 7823 )]
	public class PigHead : Food
	{
		[Constructable]
		public PigHead() : this( 1 )
		{
		}

		[Constructable]
		public PigHead( int amount ) : base( amount, 7822 )
		{
			Name = "pig's head";
			Stackable = true;
			Weight = 1.0;
			FillFactor = 2;
			Hue = 1831;
			Amount = amount;
		}

		public PigHead( Serial serial ) : base( serial )
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