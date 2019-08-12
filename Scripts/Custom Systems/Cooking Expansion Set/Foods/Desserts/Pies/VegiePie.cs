using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class VegiePie : Food
	{
		[Constructable]
		public VegiePie() : base( 0x1041 )
		{
            Name = "vegetable pie";
			Weight = 1.0;
			FillFactor = 5;
		}

		public VegiePie( Serial serial ) : base( serial )
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