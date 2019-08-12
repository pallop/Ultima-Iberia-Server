using System;
using Server;

namespace Server.Items
{
	public class FullFloorUnit : Item
	{
		[Constructable]
		public FullFloorUnit() : base( 1225 )
		{
			Name = "full floor unit";
		}

		public FullFloorUnit( Serial serial ) : base( serial )
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