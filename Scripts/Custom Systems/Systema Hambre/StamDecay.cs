//////////////////////////////////////////////////////////////////////////////////////////
// Script written by GM Jubal for Ebonspire a player run Shard http://ebonspire.com
// Reduces stamina increase based on thirst without making any changes to the regenrates.cs
// file.  Changes to that file may cause problem upon world load with your mobile
// scripts.  This is a much safer way to add this functionality
////////////////////////////////////////////////////////////////////////////////////////////
using System;
using Server.Network;
using Server;

namespace Server.Misc
{
	// Create the timer that monitors the current state of thirst
	public class StamDecayTimer : Timer
	{
		public static void Initialize()
		{
			new StamDecayTimer().Start();
		}
		// Based on the same timespan used in RegenRates.cs
		public StamDecayTimer() : base( TimeSpan.FromSeconds( 7 ), TimeSpan.FromSeconds( 7 ) )
		{
			Priority = TimerPriority.OneSecond;
		}
		
		protected override void OnTick()
		{
			StamDecay();
		}
		// Check the NetState and call the decaying function
		public static void StamDecay()
		{
			foreach ( NetState state in NetState.Instances )
			{
				StamDecaying( state.Mobile );
			}
		}
		
		// Check thirst level if below the value set take away 1 point of stam
		public static void StamDecaying( Mobile m )
		{
			if ( m != null && m.Thirst < 5 && m.Stam > 3 )
			{
				switch (m.Thirst)
				{
					case 4: m.Stam -= 1; break;
					case 3: m.Stam -= 1; break;
					case 2: m.Stam -= 2; break;
					case 1: m.Stam -= 2; break;
					case 0:
					{
						m.Stam -= 3;
						m.SendMessage( "Estas agotado deberias beber algo!" );
						break;
					}
				}
			}
		}
	}
}
