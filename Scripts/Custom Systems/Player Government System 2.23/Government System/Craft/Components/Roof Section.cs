using System;
using Server;

namespace Server.Items
{
	public class RoofSection : Item
	{
		[Constructable]
		public RoofSection() : base( 6355 )
		{
			Name = "roof section";
		}

		public RoofSection( Serial serial ) : base( serial )
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