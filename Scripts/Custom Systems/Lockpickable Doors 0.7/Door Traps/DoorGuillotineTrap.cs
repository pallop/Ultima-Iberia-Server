using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Traps
{
	public class DoorGuillotineTrap : BaseDoorTrap
	{
		public DoorGuillotineTrap( Mobile owner ) : base( owner, DoorTrapType.Guillotine, false, 1 ) { }

		public override void ExecuteTrap( Mobile from )
		{
			base.ExecuteTrap( from );

			Effects.PlaySound( this.Door.GetWorldLocation(), this.Door.Map, 0x387 );
			this.Door.PublicOverheadMessage( Server.Network.MessageType.Yell, from.YellHue, true, "*a large guillotine blade falls from the top of the doorsill, severing your hand clean off!*" );

            //if( from is PlayerMobile )
            //    ((PlayerMobile)from).HasHandCutOff = true;

			new InternalTimer( from ).Start();
			new TimedItem( 300.0, 0x1CE5 ).MoveToWorld( from.Location, from.Map );
		}

		private class InternalTimer : Timer
		{
			private Mobile _from;

			public InternalTimer( Mobile from )
				: base( TimeSpan.FromSeconds( 0.25 ), TimeSpan.FromSeconds( 1.0 ), 20 )
			{
				_from = from;

				Priority = TimerPriority.TwentyFiveMS;
			}

			protected override void OnTick()
			{
				if( _from == null || _from.Deleted || !_from.Alive || _from.NetState == null )
				{
					this.Stop();
					return;
				}

				new TimedItem( 90.0, Utility.Random( 0x122A, 5 ) ).MoveToWorld( _from.Location, _from.Map );

				_from.Damage( Utility.RandomMinMax( 10, 25 ) );
				_from.PlaySound( 0x133 );
			}
		}
	}
}