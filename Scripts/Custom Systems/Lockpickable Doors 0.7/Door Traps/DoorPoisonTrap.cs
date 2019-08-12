using System;
using Server;

namespace Server.Traps
{
	public class DoorPoisonTrap : BaseDoorTrap
	{
		public DoorPoisonTrap( Mobile owner, int charges ) : base( owner, DoorTrapType.Poison, true, charges ) { }

		public override void ExecuteTrap( Mobile from )
		{
			base.ExecuteTrap( from );

			Effects.SendLocationEffect( new Point3D( this.Door.X, this.Door.Y, this.Door.Z ), this.Door.Map, 0x11A6, 16, 3 );
			Effects.PlaySound( this.Door.Location, this.Door.Map, 0x231 );

			from.ApplyPoison( this.Owner, Poison.Lethal );

			try
			{
				foreach( Mobile m in this.Door.GetMobilesInRange( 2 ) )
				{
					if( from.Alive )
						from.ApplyPoison( this.Owner, Poison.Deadly );
				}
			}
			catch { }
		}
	}
}