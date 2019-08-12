using System;
using Server;

namespace Server.Items
{
	public class FloorSection : Item
	{
		[Constructable]
		public FloorSection() : base( 6355 )
		{
			Name = "floor section";
		}

		public FloorSection( Serial serial ) : base( serial )
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