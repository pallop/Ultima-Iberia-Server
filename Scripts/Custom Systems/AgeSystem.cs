//   ___|========================|___
//   \  |  Written by Felladrin  |  /	This script was released on RunUO Community under the GPL licensing terms.
//    > |      February 2010     | < 
//   /__|========================|__\	[Age System] - Current version: 1.3 (April 7, 2013)

using System;
using Server.Items;
using Server.Prompts;
using Server.Mobiles;
using Server.Targeting;
using Server.Accounting;

namespace Server.Commands
{
	public class AgeCommands
	{
		//===== System Config =====//

		public static bool AutoRenewAgeEnabled = true; // Should the characters get older through time automatically?

		private static TimeSpan AutoRenewDelay = TimeSpan.FromDays( 92 ); // How many Earth Days are equivalent to One Year for characters?

		private static TimeSpan AutoRenewCheck = TimeSpan.FromMinutes( 30 ); // Check for new birthdays every 30 minutes.

		public static bool AgeStatModEnabled = true; // Character's stats (Str,Dex,Int) are affected by the age?
		
		public static double maxBonus = 15;  // What is the bonus when the characters are at their best condition?

		public static double topStrAge = 35; // At what age the characters have the best strength condition?

		public static double topDexAge = 20; // At what age the characters have the best dexterity condition?

		public static double topIntAge = 50; // At what age the characters have the best intelligence condition?
		
		//===== System Config =====//

		public static void Initialize() 
		{
			CommandSystem.Register( "VerifyAge", AccessLevel.Administrator, new CommandEventHandler( VerifyAge_OnCommand ) );
			CommandSystem.Register( "ClearAgeSystem", AccessLevel.Administrator, new CommandEventHandler( ClearAgeSystem_OnCommand ) );
			CommandSystem.Register( "SetAge", AccessLevel.Administrator, new CommandEventHandler( SetAge_OnCommand ) );
			CommandSystem.Register( "NewAge", AccessLevel.Administrator, new CommandEventHandler( NewAge_OnCommand ) );
			CommandSystem.Register( "Age", AccessLevel.Player, new CommandEventHandler( MyAge_OnCommand ) );
			
			if ( AutoRenewAgeEnabled )
			{
				new AutoRenewAgeTimer().Start();
			}

			if ( AgeStatModEnabled )
			{
				foreach ( Mobile pm in World.Mobiles.Values )
				{
					if ( pm is PlayerMobile )
					{
						ApplyAgeStatMod( pm );
					}
				}
			}
		}
		
		public static void ApplyAgeStatMod( Mobile from )
		{
			try
			{
				double age = double.Parse( ((Account)from.Account).GetTag( "Age of " + (from.RawName) ) );
			
				double strBonus, dexBonus, intBonus;
			
				if ( age < topStrAge )
					strBonus = age / topStrAge * maxBonus;
				else
					strBonus = (topStrAge / age * maxBonus) + (topStrAge / age * maxBonus - maxBonus);
				
				if ( age < topDexAge )
					dexBonus = age / topDexAge * maxBonus;
				else
					dexBonus = (topDexAge / age * maxBonus) + (topDexAge / age * maxBonus - maxBonus);
				
				if ( age < topIntAge )
					intBonus = age / topIntAge * maxBonus ;
				else
					intBonus = (topIntAge / age * maxBonus) + (topIntAge / age * maxBonus - maxBonus);
			
				from.AddStatMod( new StatMod( StatType.Str, "AgeModStr", (int)strBonus, TimeSpan.Zero ) );
				from.AddStatMod( new StatMod( StatType.Dex, "AgeModDex", (int)dexBonus, TimeSpan.Zero ) );
				from.AddStatMod( new StatMod( StatType.Int, "AgeModInt", (int)intBonus, TimeSpan.Zero ) );
			}
			catch
			{
				from.SendMessage( 33, "¡Su edad no se define como un número! ¡Informa a un miembro del staff con urgencia!" );
			}
		}
		
		[Usage( "VerifyAge" )]
		[Description( "Comprueba la edad de todos los personajes, envía una advertencia a aquellos que no han registrado su edad, y muestra estadísticas sobre la edad de la población." )]
		public static void VerifyAge_OnCommand( CommandEventArgs e )
		{
			int WithoutAge = 0, TotalCounted = 0, SumAges = 0, EldestCharAge = 0, YoungestCharAge = 100, Unreadable = 0;
			
			Console.WriteLine("--- Age System ---");
			Console.WriteLine("Los personajes que no han registrado su edad todavía:");
			
			foreach ( Mobile pm in World.Mobiles.Values )
			{
				if ( pm is PlayerMobile )
				{
					try
					{
						if ( ((Account)pm.Account).GetTag( "Edad de " + (pm.RawName) ) == null || ((Account)pm.Account).GetTag( "Edad de " + (pm.RawName) ) == "")
						{
							if ( pm.Backpack == null )
							{
								pm.SendMessage( 33, "[Aviso] ¡No tienes mochila! ¡Informa a un miembro del staff con urgencia!" );
								Console.WriteLine("- {0} (Account: {1}) [MISSING BACKPACK]", pm.Name, ((Account)pm.Account).Username);
							}
							else
							{
								Item AgeChangeDeed = pm.Backpack.FindItemByType( typeof( AgeChangeDeed ) );
					
								if ( AgeChangeDeed == null )
								{
									pm.SendMessage( Utility.RandomMinMax(2,600), "[Aviso] Se ha colocado un papel para cambiar su edad. Úselo para ajustar su edad." );
									pm.AddToBackpack( new AgeChangeDeed() );
								}
								else
								{
									pm.SendMessage( Utility.RandomMinMax(2,600), "[Aviso] Hay un papel para cambiar su edad. Úselo para ajustar su edad." );
								}
					
								Console.WriteLine("- {0} (Account: {1})", pm.Name, ((Account)pm.Account).Username);
							}
					
							WithoutAge++;
						}
						else
						{
							int age = int.Parse( ((Account)pm.Account).GetTag( "Edad de " + (pm.RawName) ) );
					
							if ( age > EldestCharAge )
								EldestCharAge = age;
					
							if ( age < YoungestCharAge )
								YoungestCharAge = age;

							SumAges = SumAges + age;

							TotalCounted++;
						}
					}
					catch
					{
						Unreadable++; //The unreadable accounts are ignored to avoid server crash.
					}
				}
			}			
			
			if ( SumAges != 0 )
				e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "[Age System]  La población actual de {0} personas tienen una edad promedio de {1}. La edad más alta registrada fue {2}, y la más baja, {3}.", (TotalCounted + WithoutAge), (SumAges / TotalCounted), EldestCharAge, YoungestCharAge);
			else
				e.Mobile.SendMessage( 33, "[Age System] No se registró la edad todavía." );
			
			if ( WithoutAge == 0 )
			{
				e.Mobile.SendMessage( 67, "[Age System] Todos los personajes han registrado correctamente sus edades." );
				Console.WriteLine( "Todos los personajes han registrado correctamente sus edades." );
			}
			else     
			{
				e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "[Age System] {0} caracteres no han registrado sus edades. Ellos recibieron una advertencia pidiendo a ajustarla con la Escritura Edad Cambio en sus mochilas. Compruebe la consola para ver sus nombres", WithoutAge );
				Console.WriteLine("Total: {0} personajes necesitan ajustar su edad.", WithoutAge);
				if ( Unreadable != 0 )
					Console.WriteLine("Aviso: {0} cuentas ilegibles detectadas.", Unreadable);
			}
			
			Console.WriteLine("--- Age System ---");
		}
		
		[Usage( "ClearAgeSystem" )]
		[Description( "Elimina todas las etiquetas y elementos de la edad del Sistema de su fragmento. Después se puede volver a activar el sistema o eliminar el guión de la carpeta ServUO y reinicie el servidor." )]
		public static void ClearAgeSystem_OnCommand( CommandEventArgs e )
		{
			foreach ( Mobile pm in World.Mobiles.Values )
			{
				if ( pm is PlayerMobile )
				{
					try
					{
						if ( ((Account)pm.Account).GetTag( "Edad de " + (pm.RawName) ) != null )
						{
							((Account)pm.Account).RemoveTag( "Edad de " + (pm.RawName) );
							
							if ( pm.NameMod != null )
								pm.NameMod = null;
							
							pm.RemoveStatMod( "AgeModStr" );
							pm.RemoveStatMod( "AgeModDex" );
							pm.RemoveStatMod( "AgeModInt" );
						}
					}
					catch
					{
					}
				}
			}
			
			System.Collections.ArrayList itemlist = new System.Collections.ArrayList();
			
			foreach ( Item item in World.Items.Values )
			{
				if ( item is AgeChangeDeed || item is RejuvenationPotion )
					itemlist.Add( item );
			}
			
			foreach (Item item in itemlist)
				item.Delete();			
			
			Misc.AutoSave.Save();
			
			e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "[Age System] All tags and items of the Age System have been removed from your shard." );
			e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "[Age System] Type [VerifyAge, if you want to reenable the system." );
			e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "[Age System] Remove the script from RunUO folder and restart the server, if you will never use it again." );
		}

		[Usage( "SetAge" )]
		[Description( "Set the age of a character to the specified value." )]
		public static void SetAge_OnCommand( CommandEventArgs e )
		{ 		
			string NewAgeValue = e.ArgString;
			
			if ( NewAgeValue != null && NewAgeValue.Length >= 1 && System.Text.RegularExpressions.Regex.IsMatch( NewAgeValue, @"^[0-9]+$" ) )
			{
				e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "Select the character you would like to give {0} years old.", NewAgeValue );
				e.Mobile.Target = new SetAgeTarget( NewAgeValue );
			}	
			else
				e.Mobile.SendMessage("Usage: [SetAge <PositiveNumber>");
		}
	
		private class SetAgeTarget : Target
		{
			string ReceivedAge;
		
			public SetAgeTarget( string NewAgeValue ) : base( -1, false, TargetFlags.None )
			{
				ReceivedAge = NewAgeValue;
			}

			protected override void OnTarget( Mobile from, object targeted ) 
			{
				if ( targeted is PlayerMobile )
				{
					((Mobile)targeted).SendMessage( Utility.RandomMinMax(2,600), "Su edad se cambió por {0}. Ahora está {1} años.", from.Name, ReceivedAge );					
					((Account)((Mobile)targeted).Account).SetTag( "Edad de " + (((Mobile)targeted).RawName), ReceivedAge );
					((Account)((Mobile)targeted).Account).SetTag( "LastRenewAge " + (((Mobile)targeted).RawName), (DateTime.Now).ToString() );
					from.SendMessage( Utility.RandomMinMax(2,600), "Edad de {0} ha cambiado exitosamente.", ((Mobile)targeted).Name );
					if ( AgeStatModEnabled )
					{
						ApplyAgeStatMod( ((Mobile)targeted) );
					}
				}
				else
					from.SendMessage( Utility.RandomMinMax(2,600), "Sólo puede cambiar la edad de los PJ's." );
			}		
		}
		
		[Usage( "NewAge" )]
		[Description( "Hace que todos los personajes se convierten en un año mayor." )]
		public static void NewAge_OnCommand( CommandEventArgs e )
		{
			foreach ( Mobile pm in World.Mobiles.Values )
			{
				if ( pm is PlayerMobile )
				{
					try
					{
						if ( ((Account)pm.Account).GetTag( "Edad de" + (pm.RawName) ) == null || ((Account)pm.Account).GetTag( "Edad de " + (pm.RawName) ) == "")
						{
							//Ignore them. To make a check on the server and adjust the characters who have not recorded their age yet use the comand [VerifyAge.
						}
						else
						{
							int age = int.Parse( ((Account)pm.Account).GetTag( "Edad de " + (pm.RawName) ) );
							((Account)pm.Account).SetTag( "Edad de" + (pm.RawName), (age + 1).ToString() );
							pm.SendMessage( Utility.RandomMinMax(2,600), "¡Enhorabuena! Ahora tienes {0} años!", (age + 1) );
							if ( AgeStatModEnabled )
							{
								ApplyAgeStatMod( pm );
							}
						}
					}
					catch
					{
					}
				}
			}
			
			e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "Done. All characters are now one year older. Use the command [VerifyAge to see the statistics." );
		}
		
		[Usage( "Age" )]
		[Description( "Say your age and let others know about it. (Toggle age being shown in your name)" )]
		public static void MyAge_OnCommand( CommandEventArgs e )
		{
			if ( ((Account)e.Mobile.Account).GetTag( "Age of " + (e.Mobile.RawName) ) == null || ((Account)e.Mobile.Account).GetTag( "Age of " + (e.Mobile.RawName) ) == "")
			{
				if ( e.Mobile.Backpack == null )
				{
					e.Mobile.SendMessage( 33, "[Warning] You have no backpack! Report it to a staff member urgently!" );
					Console.WriteLine("[Age System: Warning] The character '{0}' (Account: {1}) has no backpack.", e.Mobile.Name, ((Account)e.Mobile.Account).Username);
				}
				else
				{
					Item AgeChangeDeed = e.Mobile.Backpack.FindItemByType( typeof( AgeChangeDeed ) );
					
					if ( AgeChangeDeed == null )
					{
						e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "Aun no has elegido tu edad. Usa el papel de tu mochila destinado a ello." );
						e.Mobile.AddToBackpack( new AgeChangeDeed() );
					}
					else
					{
						e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "Aun no has elegido tu edad. Usa el papel de tu mochila destinado a ello." );
					}
				}
			}
			else
			{
				if ( e.Mobile.NameMod == null )
				{
					e.Mobile.Say( "Tengo {0} años.", ((Account)e.Mobile.Account).GetTag( "Age of " + (e.Mobile.RawName) ) );
					e.Mobile.NameMod = e.Mobile.Name + " [Age: " + ((Account)e.Mobile.Account).GetTag( "Age of " + (e.Mobile.RawName) ) + "]";
					e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "tu edad ha sido añadida al nombre." );
				}
				else
				{
					e.Mobile.Whisper( "*{0} años*", ((Account)e.Mobile.Account).GetTag( "Edad de " + (e.Mobile.RawName) ) );
					e.Mobile.NameMod = null;
					e.Mobile.SendMessage( Utility.RandomMinMax(2,600), "tu edad ha sido eliminada del nombre.." );
				}
			}
		}
		
		public class AutoRenewAgeTimer : Timer
		{
			public AutoRenewAgeTimer() : base( AutoRenewCheck, AutoRenewCheck )
			{
				Priority = TimerPriority.OneMinute;
			}

			protected override void OnTick()
			{
				foreach ( Mobile pm in World.Mobiles.Values )
				{
					if ( pm is PlayerMobile )
					{
						try
						{
							if ( ((Account)pm.Account).GetTag( "Age of " + (pm.RawName) ) == null || ((Account)pm.Account).GetTag( "Age of " + (pm.RawName) ) == "" || ((Account)pm.Account).GetTag( "LastRenewAge " + (pm.RawName) ) == null )
							{
								//Ignore them.
							}
							else
							{
								DateTime LastRenew = DateTime.Parse( ((Account)pm.Account).GetTag( "LastRenewAge " + (pm.RawName) ) );
								
								if ( DateTime.Now > (LastRenew + AutoRenewDelay)  )
								{
									int age = int.Parse( ((Account)pm.Account).GetTag( "Age of " + (pm.RawName) ) );
									((Account)pm.Account).SetTag( "Age of " + (pm.RawName), (age + 1).ToString() );
									((Account)pm.Account).SetTag( "LastRenewAge " + (pm.RawName), (DateTime.Now).ToString() );
									pm.SendMessage( Utility.RandomMinMax(2,600), "HOY ES TU CUMPLEAÑOS! Ahora tienes {0} años! FELICIDADES!", (age + 1) );
									if ( AgeStatModEnabled )
									{
										ApplyAgeStatMod( pm );
									}
								}	
							}
						}
						catch
						{
						}
					}
				}
			}
		}
	}
}

namespace Server.Items
{
	public class AgeChangeDeed : Item
	{ 
		[Constructable] 
		public AgeChangeDeed() : base( 0x14F0 )
		{ 
			Name = "Age Change Deed";
			Movable = false;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			list.Add( this.Name );
			list.Add( "Cuantos años tienes?" );
		}

		public override void OnDoubleClick( Mobile from ) 
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.SendMessage( Utility.RandomMinMax(2,600), "Escribe una edad para tu personaje. debe estar entre 18 y 40 años." );
				from.Prompt = new ChooseAge( from );
				this.Delete();
			}
			else
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
		}

		private class ChooseAge : Prompt
		{ 
			public ChooseAge( Mobile from )
			{
			}

			public override void OnResponse( Mobile from, string number ) 
			{
				if ( !(System.Text.RegularExpressions.Regex.IsMatch(number, @"^[0-9]+$")) )
				{
					from.SendMessage( Utility.RandomMinMax(2,600), "Debe ser un numero positivo!" );
					from.AddToBackpack( new AgeChangeDeed() );
					return;
				}
				else if ( int.Parse(number) < 18 )
				{
					from.SendMessage( Utility.RandomMinMax(2,600), "no puedes empezar con menos de 18 años!" );
					from.AddToBackpack( new AgeChangeDeed() );
					return;
				}
				else if ( int.Parse(number) > 40 )
				{
					from.SendMessage( Utility.RandomMinMax(2,600), "no puedes empezar con mas de 40 años!" );
					from.AddToBackpack( new AgeChangeDeed() );
					return;
				}
				else
				{
					((Account)from.Account).SetTag( "Age of " + (from.RawName), number );
					((Account)from.Account).SetTag( "LastRenewAge " + (from.RawName), (DateTime.Now).ToString() );
					from.SendMessage( Utility.RandomMinMax(2,600), "tu edad ha sido establecida. Ahora tienes {0} años.", number );
					if ( Server.Commands.AgeCommands.AgeStatModEnabled )
					{
						Server.Commands.AgeCommands.ApplyAgeStatMod( from );
					}
				}	
			}

			public override void OnCancel( Mobile from )
			{
				from.AddToBackpack( new AgeChangeDeed() );
			}
		}
		
		public AgeChangeDeed( Serial serial ) : base( serial )
		{ 
		} 

		public override void Serialize( GenericWriter writer )
		{ 
			base.Serialize( writer );
			writer.Write( (int) 0 ); 
		} 

		public override void Deserialize( GenericReader reader ) 
		{
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 
	}

	public class RejuvenationPotion : Item
	{
		[Constructable] 
		public RejuvenationPotion() : base( 0xF0D )
		{
			Name = "Rejuvenation Potion";
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			list.Add( this.Name );
			list.Add( "Reduces your age" );
		}
		
		public override void OnDoubleClick( Mobile from ) 
		{
			if ( ((Account)from.Account).GetTag( "Age of " + (from.RawName) ) == null || ((Account)from.Account).GetTag( "Age of " + (from.RawName) ) == "")
			{
				from.SendMessage( 33, "Before drinking this potion you need to know your age!" );
			}
			else
			{
				try
				{
					int age = int.Parse( ((Account)from.Account).GetTag( "Age of " + (from.RawName) ) );				
				
					if ( age < 23 )
					{
						from.SendMessage( Utility.RandomMinMax(2,600), "Thinking better, you decide against drinking this potion, because it could make you so young that you could loose the reason." );
					}
					else
					{
						((Account)from.Account).SetTag( "Age of " + (from.RawName), (age - Utility.RandomMinMax(1,5)).ToString() );
						this.Delete();
						from.SendMessage( Utility.RandomMinMax(2,600), "You drink the potion and feel more willing! Now you are {0} years old!", ((Account)from.Account).GetTag( "Age of " + (from.RawName) ) );
						if ( Server.Commands.AgeCommands.AgeStatModEnabled )
						{
							Server.Commands.AgeCommands.ApplyAgeStatMod( from );
						}
					}
				}
				catch
				{
					from.SendMessage( 33, "Your age is not defined as a number! Report it to a staff member urgently!" );
				}
			}
		}
		
		public RejuvenationPotion( Serial serial ) : base( serial )
		{ 
		} 

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
