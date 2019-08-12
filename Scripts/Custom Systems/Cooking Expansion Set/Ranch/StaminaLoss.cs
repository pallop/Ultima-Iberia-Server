using System;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.Misc
{
	public class StamLoss
	{
		public static void Initialize()
		{
			EventSink.Movement += new MovementEventHandler( EventSink_Movement );
		}

		public static void EventSink_Movement( MovementEventArgs e )
		{
			Mobile from = e.Mobile;
			
			//if (from.Mounted || !from.Alive || from.AccessLevel >= AccessLevel.GameMaster) return;
			//else from.Stam -= GetStamLoss(from, (e.Direction & Direction.Running) != 0);
			
			if (from is BaseAnimal)
			{
				BaseAnimal ba = (BaseAnimal)from;
				ba.Stam -= GetStamLoss(from, (e.Direction & Direction.Running) != 0);
			}
			
			return;
		}

		public static int GetStamLoss( Mobile from, bool running )
		{
			int loss = 0;

			if ( running )
				loss = 1;

			return loss;
		}
	}
}