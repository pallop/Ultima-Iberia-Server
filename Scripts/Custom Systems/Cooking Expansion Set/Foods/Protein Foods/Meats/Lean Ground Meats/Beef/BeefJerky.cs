using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class BeefJerky : Food
	{
		[Constructable]
		public BeefJerky() : this( 1 )
		{
		}

		[Constructable]
		public BeefJerky( int amount ) : base( amount, 0x979 )
		{
			Weight = 0.2;
			FillFactor = 1;
			Name = "beef jerky";
            Stackable = true;
            Amount = amount;
		}

		public BeefJerky( Serial serial ) : base( serial )
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