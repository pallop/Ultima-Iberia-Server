using System;
using Server;

namespace Server.Items
{
	public class JesterMaskSouth : Item
	{

		[Constructable]
		public JesterMaskSouth() : base( 19367 )
		{
			Name = "Jester's Mask";
			Weight = 3.0;
			Hue = 1153;
			Movable = true;
		}

		public JesterMaskSouth( Serial serial ) : base( serial )
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