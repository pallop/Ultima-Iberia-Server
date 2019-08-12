using System;
using Server;

namespace Server.Items
{
	public class SailSection : Item
	{
		[Constructable]
		public SailSection() : base( 6420 )
		{
			Name = "sail section";
			Hue = 51;
		}

		public SailSection( Serial serial ) : base( serial )
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