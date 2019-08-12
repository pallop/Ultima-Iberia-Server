using System;
using Server;

namespace Server.Traps
{
	public class DoorArrowTrapInstaller : DoorTrapInstaller
	{
		[Constructable]
		public DoorArrowTrapInstaller() : base( null, DoorTrapType.Arrow, 8 ) { }

		public DoorArrowTrapInstaller( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
}