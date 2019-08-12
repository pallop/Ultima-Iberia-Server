using System;
using Server.Network;

namespace Server.Items
{
	public class Almond : Food
	{
		[Constructable]
		public Almond() : this( 1 )
		{
		}

		[Constructable]
		public Almond( int amount ) : base( amount, 0x1AA2 )
		{
			Weight = 0.1;
			FillFactor = 1;
			Hue = 0x482;
			Name = "Almond";
		}

		public Almond( Serial serial ) : base( serial )
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