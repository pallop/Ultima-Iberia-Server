using System;
using Server;

namespace Server.Items
{
	public class WallSection : Item
	{
		[Constructable]
		public WallSection() : base( 6355 )
		{
			Name = "wall section";
		}

		public WallSection( Serial serial ) : base( serial )
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