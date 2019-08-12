using System;
using Server;

namespace Server.Traps
{
	public class DoorPoisonTrapInstaller : DoorTrapInstaller
	{
		[Constructable]
		public DoorPoisonTrapInstaller() : base( null, DoorTrapType.Poison, 6 ) { }

		public DoorPoisonTrapInstaller( Serial serial ) : base( serial ) { }

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