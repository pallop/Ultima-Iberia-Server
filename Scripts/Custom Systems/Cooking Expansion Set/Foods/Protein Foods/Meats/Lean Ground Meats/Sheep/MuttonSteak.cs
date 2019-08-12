using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class MuttonSteak : Food
	{
		[Constructable]
		public MuttonSteak() : this( 1 )
		{
		}

		[Constructable]
		public MuttonSteak( int amount ) : base( amount, 0x9F2 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
			Name = "mutton steak";
		}

		public MuttonSteak( Serial serial ) : base( serial )
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