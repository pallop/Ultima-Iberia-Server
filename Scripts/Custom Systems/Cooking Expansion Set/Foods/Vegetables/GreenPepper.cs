using System;
using Server.Network;

namespace Server.Items
{
	public class GreenPepper : Food
	{
		[Constructable]
		public GreenPepper() : this( 1 )
		{
		}

		[Constructable]
		public GreenPepper( int amount ) : base( amount, 0xC75 )
		{
			FillFactor = 1;
			Hue = 0x1D3;
			Name = "Green Pepper";
		}

		public GreenPepper( Serial serial ) : base( serial )
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