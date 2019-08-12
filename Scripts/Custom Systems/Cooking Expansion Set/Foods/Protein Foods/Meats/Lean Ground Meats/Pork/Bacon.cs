using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class Bacon : Food
	{
		[Constructable]
		public Bacon() : this( 1 )
		{
		}

		[Constructable]
		public Bacon( int amount ) : base( amount, 0x979 )
		{
			Weight = 0.2;
			FillFactor = 1;
            Stackable = true;
            Amount = amount;
		}

		public Bacon( Serial serial ) : base( serial )
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