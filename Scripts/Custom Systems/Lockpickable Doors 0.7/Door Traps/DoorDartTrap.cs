using System;
using Server;
using Server.Items;

namespace Server.Traps
{
	public class DoorDartTrap : BaseDoorTrap
	{
		public DoorDartTrap( Mobile owner, int ammo ) : base( owner, DoorTrapType.Dart, true, ammo ) { }

		public override void ExecuteTrap( Mobile from )
		{
			base.ExecuteTrap( from );

			from.Damage( Utility.RandomMinMax( 20, 30 ), this.Owner );

			Effects.SendMovingEffect( this.Door, from, 0xF42, 18, 1, false, false );
			from.PlaySound( 0x234 );

			Blood bl = new Blood();
			bl.ItemID = Utility.Random( 0x122A, 5 );
			bl.MoveToWorld( from.Location, from.Map );
		}
	}
}