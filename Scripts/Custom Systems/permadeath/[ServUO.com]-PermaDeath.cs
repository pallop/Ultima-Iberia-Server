using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Items;
 
namespace Server
{
	public static class PlayerDeathManager
	{
#if Framework_4_0
		private static bool Processing { get; set; }
		private static Timer DeathTicker { get; set; }

		public static bool DeleteOnCorpseCarved { get; set; }
		public static List<PlayerMobile> DeathRegister { get; private set; }
#else
		private static bool Processing;
		private static Timer DeathTicker;

		private static List<PlayerMobile> _DeathRegister;
		public static List<PlayerMobile> DeathRegister { get { return _DeathRegister; } set { _DeathRegister = value; } }

		private static bool _DeleteOnCorpseCarved;
		public static bool DeleteOnCorpseCarved { get { return _DeleteOnCorpseCarved; } set { _DeleteOnCorpseCarved = value; } }
#endif

		public static void Configure()
		{
			DeathRegister = new List<PlayerMobile>();
			DeleteOnCorpseCarved = true;
		}

		public static void Initialize()
		{
			DeathTicker = Timer.DelayCall(TimeSpan.Zero, TimeSpan.FromSeconds(1.0), DeathOnTick);
			EventSink.PlayerDeath += OnPlayerDeath;
		}

		private static void DeathOnTick()
		{
			if (Processing)
				return;

			Processing = true;

			foreach (PlayerMobile dead in DeathRegister.ToArray())
			{
				if (dead.Deleted || dead.Alive)
				{
					//if (dead.Corpse != null)
						//dead.Corpse.Delete();

					DeathRegister.Remove(dead);
					continue;
				}

				Corpse corpse = dead.Corpse as Corpse;

				if (corpse != null && !corpse.Deleted && corpse.Carved && DeleteOnCorpseCarved)
				{
					dead.SendMessage("Alguien talló tus restos en pedazos pequeños e irreconocibles. Estás muerto.");
					Timer.DelayCall(TimeSpan.FromSeconds(5), DeletePlayer, dead);
					DeathRegister.Remove(dead);
				}
			}

			Processing = false;
		}

		private static void OnPlayerDeath(PlayerDeathEventArgs e)
		{
			PlayerMobile dead = e.Mobile as PlayerMobile;

			if (dead == null || dead.Deleted || dead.Alive)
				return;

			if (!DeathRegister.Contains(dead))
				DeathRegister.Add(dead);

			dead.Frozen = false;

			if (dead.LastKiller == null || dead.LastKiller is BaseCreature)
			{
				dead.SendMessage("Has sido noqueado");
				Timer.DelayCall(TimeSpan.FromSeconds(60), DeleteCorpseAndResurrect, dead);

			}
			else
			{
				dead.SendMessage("Has sido noqueado");
				Timer.DelayCall(TimeSpan.FromSeconds(60), DeleteCorpseAndResurrect, dead); 
				// you use DeletePlayer instead DeleteCorpseAndResurrect for instant PVP Kill
			}

			

		}

		private static void DeletePlayer(Mobile dead)
		{
			if (dead == null || dead.Deleted)
				return;

			dead.Delete();
		}

		private static void DeleteCorpseAndResurrect(Mobile dead)
		{
			if (dead == null || dead.Deleted)
				return;

			/*if (dead.Corpse != null)
			{
				dead.Corpse.Delete();
				dead.SendMessage("Tu cadáver se ha descompuesto.");
			}*/

			if (!dead.Alive)
			{
				dead.Resurrect();
				dead.Frozen = false;
				dead.SendMessage("Estás de nuevo en pie.");
			}
		}
	}
}