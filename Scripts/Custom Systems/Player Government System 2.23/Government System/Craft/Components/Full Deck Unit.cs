using System;
using Server;

namespace Server.Items
{
	public class FullDeckUnit : Item
	{
		[Constructable]
		public FullDeckUnit() : base( 16011 )
		{
			Name = "full deck unit";
		}

		public FullDeckUnit( Serial serial ) : base( serial )
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