using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class BeefPorterhouse : Food
	{
		[Constructable]
		public BeefPorterhouse() : this( 1 )
		{
		}

		[Constructable]
		public BeefPorterhouse( int amount ) : base( amount, 0x9F2 )
		{
			Weight = 1.0;
			FillFactor = 5;
			Name = "a porterhouse steak";
            Stackable = true;
            Amount = amount;
		}

		public BeefPorterhouse( Serial serial ) : base( serial )
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