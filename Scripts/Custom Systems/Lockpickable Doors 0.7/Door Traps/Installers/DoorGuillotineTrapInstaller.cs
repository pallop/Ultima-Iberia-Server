using System;
using Server;

namespace Server.Traps
{
	public class DoorGuillotineTrapInstaller : DoorTrapInstaller
	{
		[Constructable( AccessLevel.GameMaster )]
		public DoorGuillotineTrapInstaller() : base( null, DoorTrapType.Guillotine, 1 ) { }

		public DoorGuillotineTrapInstaller( Serial serial ) : base( serial ) { }

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