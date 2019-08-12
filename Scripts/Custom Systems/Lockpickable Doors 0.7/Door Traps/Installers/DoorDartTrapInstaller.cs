using System;
using Server;

namespace Server.Traps
{
	public class DoorDartTrapInstaller : DoorTrapInstaller
	{
		[Constructable]
		public DoorDartTrapInstaller() : base( null, DoorTrapType.Dart, 8 ) { }

		public DoorDartTrapInstaller( Serial serial ) : base( serial ) { }

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