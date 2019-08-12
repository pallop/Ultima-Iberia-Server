using System;
using Server.Network;

namespace Server.Items
{
	public class Pistacio : Food
	{
		[Constructable]
		public Pistacio() : this( 1 )
		{
		}

		[Constructable]
		public Pistacio( int amount ) : base( amount, 0x1AA2 )
		{
			Weight = 0.1;
			FillFactor = 1;
			Hue = 0x47E;
			Name = "Pistacio";
		}

		public Pistacio( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}