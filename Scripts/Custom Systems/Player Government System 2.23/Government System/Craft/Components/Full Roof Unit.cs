using System;
using Server;

namespace Server.Items
{
	public class FullRoofUnit : Item
	{
		[Constructable]
		public FullRoofUnit() : base( 1486 )
		{
			Name = "full roof unit";
		}

		public FullRoofUnit( Serial serial ) : base( serial )
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