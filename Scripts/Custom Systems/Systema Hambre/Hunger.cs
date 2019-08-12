using System;
using Server;
using Server.Commands;
using Server.Items;
using Server.Network;
using Server.Targeting;
using CPA = Server.CommandPropertyAttribute;

namespace Server.Scripts.Commands
{
	public class MyHunger
	{
		public static void Initialize()
		{
			CommandSystem.Register ( "hambre", AccessLevel.Player, new CommandEventHandler ( MyHunger_OnCommand ) );
			CommandSystem.Register ( "myhunger", AccessLevel.Player, new CommandEventHandler ( MyHunger_OnCommand ) );
		}
		public static void MyHunger_OnCommand( CommandEventArgs e )
		{
			int h = e.Mobile.Hunger; // Variable to hold the hunger value of the player
			// these values are taken from Food.cs and relate directly to the message
			// you get when you eat.
			if (h <= 0 )
				e.Mobile.SendMessage( "Te mueres de hambre." );
			else if ( h <= 5 )
			       	e.Mobile.SendMessage( "Estas extremadamente hambriento." );
			else if ( h <= 10 )
				e.Mobile.SendMessage( "Estas muy hambriento." );
			else if ( h <= 15 )
				e.Mobile.SendMessage( "Tienes algo de hambre." );
			else if ( h <= 19 )
				e.Mobile.SendMessage( "No tienes casi hambre." );
			else if ( h > 19 )
				e.Mobile.SendMessage( "Estas casi lleno." );
			else
				e.Mobile.SendMessage( "Error: Please report this error: hunger not found." );

			int t = e.Mobile.Thirst; // Variable to hold the thirst value of the player
			// read the comments above to see where these values came from
			if ( t <= 0 )
				e.Mobile.SendMessage( "Estas cansado y necesitas beber mucho." );
			else if ( t <= 5 )
			       	e.Mobile.SendMessage( "Estas extremadamente sediento." );
			else if ( t <= 10 )
				e.Mobile.SendMessage( "Estas muy sediento." );
			else if ( t <= 15 )
				e.Mobile.SendMessage( "Estas algo sediento." );
			else if ( t <= 19 )
				e.Mobile.SendMessage( "No tienes mucha sed." );
			else if ( t > 19 )
				e.Mobile.SendMessage( "No tienes sed." );
			else
				e.Mobile.SendMessage( "Error: Please report this error: thirst not found." );
		}
	}
}
