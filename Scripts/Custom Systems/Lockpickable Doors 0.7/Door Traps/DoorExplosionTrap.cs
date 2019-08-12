using System;
using Server;

namespace Server.Traps
{
	public class DoorExplosionTrap : BaseDoorTrap
	{
		public DoorExplosionTrap( Mobile owner ) : base( owner, DoorTrapType.Explosion, false, 5 ) { }

		public override void ExecuteTrap( Mobile from )
		{
			base.ExecuteTrap( from );

			from.Damage( Utility.RandomMinMax( 45, 60 ), this.Owner );

			Effects.PlaySound( from.Location, from.Map, 0x207 );
			Effects.SendLocationEffect( this.Door.Location, this.Door.Map, Utility.RandomList( 0x36B0, 0x36BD, 0x36CA ), 20 );
		}
	}
}