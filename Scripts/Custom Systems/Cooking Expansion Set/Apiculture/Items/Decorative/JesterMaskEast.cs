using System;
using Server;

namespace Server.Items
{
	public class JesterMaskEast : Item
	{

		[Constructable]
		public JesterMaskEast() : base( 19368 )
		{
			Name = "Jester's Mask";
			Weight = 3.0;
			Hue = 1153;
			Movable = true;
		}

		public JesterMaskEast( Serial serial ) : base( serial )
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