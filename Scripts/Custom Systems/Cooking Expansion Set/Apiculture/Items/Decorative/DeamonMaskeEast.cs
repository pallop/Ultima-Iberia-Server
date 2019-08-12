using System;
using Server;

namespace Server.Items
{
	public class DaemonMaskEast : Item
	{

		[Constructable]
		public DaemonMaskEast() : base( 19091 )
		{
			Name = "Daemon's Mask";
			Weight = 3.0;
			Hue = 0;
			Movable = true;
		}

		public DaemonMaskEast( Serial serial ) : base( serial )
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