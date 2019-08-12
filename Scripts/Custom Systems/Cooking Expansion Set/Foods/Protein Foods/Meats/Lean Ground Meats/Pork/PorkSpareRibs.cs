using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class PorkSpareRibs : Food
	{
		[Constructable]
		public PorkSpareRibs() : this( 1 )
		{
		}

		[Constructable]
		public PorkSpareRibs( int amount ) : base( amount, 0x9F2 )
		{
			Weight = 1.0;
			FillFactor = 3;
			Name = "spare ribs";
            Stackable = true;
            Amount = amount;
		}

		public PorkSpareRibs( Serial serial ) : base( serial )
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