using System;
using Server;

namespace Server.Items
{
	public class ClownMaskSouth : Item
	{

		[Constructable]
		public ClownMaskSouth() : base( 19088 )
		{
			Name = "Clown's Mask";
			Weight = 3.0;
			Hue = 0;
			Movable = true;
		}

		public ClownMaskSouth( Serial serial ) : base( serial )
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