using System;
using Server;

namespace Server.Items
{
	public class DaemonMaskSouth : Item
	{

		[Constructable]
		public DaemonMaskSouth() : base( 19090 )
		{
			Name = "Daemon's Mask";
			Weight = 3.0;
			Hue = 0;
			Movable = true;
		}

		public DaemonMaskSouth( Serial serial ) : base( serial )
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