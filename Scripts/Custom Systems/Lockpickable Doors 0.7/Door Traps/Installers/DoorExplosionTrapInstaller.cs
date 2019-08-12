using System;
using Server;

namespace Server.Traps
{
	public class DoorExplosionTrapInstaller : DoorTrapInstaller
	{
		[Constructable]
		public DoorExplosionTrapInstaller() : base( null, DoorTrapType.Explosion, 1 ) { }

		public DoorExplosionTrapInstaller( Serial serial ) : base( serial ) { }

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