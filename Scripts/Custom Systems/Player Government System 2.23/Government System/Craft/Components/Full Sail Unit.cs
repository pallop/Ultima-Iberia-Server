using System;
using Server;

namespace Server.Items
{
	public class FullSailUnit : Item
	{
		[Constructable]
		public FullSailUnit() : base( 7972 )
		{
			Name = "full sail unit";
		}

		public FullSailUnit( Serial serial ) : base( serial )
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