using System;
using System.Media;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using System.Collections;
using System.Collections.Generic;

namespace Server.Commands
{
	public class contact
	{
		public static void Initialize()
		{
			CommandSystem.Register( "contact", AccessLevel.Player, new CommandEventHandler( Time_OnCommand ) );
		}

		[Usage( "Time" )]
		[Description( "Returns the server's local time." )]
		private static void Time_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendMessage( DateTime.UtcNow.ToString() );
			e.Mobile.SendGump( new gumpcontact( ) );
		}
	}
}