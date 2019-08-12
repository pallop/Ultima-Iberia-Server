using System;
using Server;

namespace Server.Items
{
	public class FullWallUnit : Item
	{
		[Constructable]
		public FullWallUnit() : base( 832 )
		{
			Name = "full wall unit";
		}

		public FullWallUnit( Serial serial ) : base( serial )
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