using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 7820, 7821 )]
	public class Trotters : Food
	{
		[Constructable]
		public Trotters() : this( 1 )
		{
		}

		[Constructable]
		public Trotters( int amount ) : base( amount, 7820 )
		{
			Name = "pig's feet";
			Stackable = true;
			Weight = 0.5;
			Hue = 1831;
			FillFactor = 1;
			Amount = amount;
		}

		public Trotters( Serial serial ) : base( serial )
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