using System; 
using Server; 
using Server.Misc; 
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Server.Accounting; 

namespace Server.Commands 
{ 
   public class NewPasswordCommand 
   { 
      	public static void Initialize() 
      	{ 
         	EventSink.Login += new LoginEventHandler( PW_Login ); 
         	CommandSystem.Register( "NewPass", AccessLevel.Player, new CommandEventHandler( ChangePassword_OnCommand ) ); 
         	CommandSystem.Register( "Password", AccessLevel.Player, new CommandEventHandler( ChangePassword_OnCommand ) ); 
         	CommandSystem.Register( "PW", AccessLevel.Player, new CommandEventHandler( ChangePassword_OnCommand ) ); 
         	CommandSystem.Register( "MWSet", AccessLevel.Player, new CommandEventHandler( SetMagicWord_OnCommand ) ); 
      	} 
       
      	private static void PW_Login( LoginEventArgs args ) 
      	{              
         	Mobile m = args.Mobile; 
         	bool value = MagicWord( m ); 
 
          	if ( m != null && !value ) 
	{ 
            	m.SendMessage( 0x35, "Your need set you magic word by typing MWSet <magicword>." ); 
		} 
	} 

      	[Usage( "Password <newpassword> <magicword>" )] 
      	[Description( "Changes the current account's password." )] 
      	[Aliases( "NewPass, PW, Rassword" )] 
      	public static void ChangePassword_OnCommand( CommandEventArgs e ) 
      	{ 
         	string MWord = ((Account)e.Mobile.Account).GetTag("mword"); 
         if ( e.Length == 2 ) 
	{ 
            	string NewAccountPass = e.GetString ( 0 ).Trim() ; 
            	string SecretMagicWord = e.GetString ( 1 ).Trim() ; 
            	if (MWord == SecretMagicWord) 
	{ 
	if ( NameVerification.Validate( NewAccountPass, 2, 16, true, true, true, 1, NameVerification.SpaceDashPeriodQuote ) ) 
	{ 
		Console.WriteLine( "{0} is changing password to '{1}'", e.Mobile, NewAccountPass );                   
		((Account)e.Mobile.Account).CryptPassword = Account.HashMD5( NewAccountPass );
		e.Mobile.SendMessage( 0x35, "Your password has been changed." ); 
	}
	else 
	{ 
		e.Mobile.SendMessage( 0x25, "Failed verification of the new password. Try again with a different password." ); 
		} 
	} 
	else 
	{ 
            	e.Mobile.SendMessage( 0x25, "Incorrect magic word. Try again." ); 
            	} 
	} 
	else 
	{ 
            	e.Mobile.SendMessage ( 0x35, "Format: Password <newaccountpass> <magicword>" ); 
            	e.Mobile.SendMessage ( 0x35, "Password should not only contain letters , it should also contain numbers and upper/lowercase. No spaces & quotes allowed." );          
         	} 
	} 
		[Usage( "MWSet <magicword>" )] 
      		[Description( "Set up magic word needed for changing account's password." )] 
      		public static void SetMagicWord_OnCommand( CommandEventArgs e ) 
      		{ 
         	if ( !MagicWord( e.Mobile) ) 
         	{ 
            	if ( e.Length == 1 ) 
            	{ 
               		string MWord = e.GetString ( 0 ).Trim() ; 
               	if ( NameVerification.Validate( MWord, 2, 16, true, true, true, 1, NameVerification.SpaceDashPeriodQuote ) ) 
               	{ 
                  	Console.WriteLine( "{0} is setting magic word to '{1}'", e.Mobile, MWord );  
                  	((Account)e.Mobile.Account).SetTag( "mword", MWord ); 
                  	e.Mobile.SendMessage( 0x35, "Your magic word has been set." ); 
               	} 
               	else 
               	{ 
                  	e.Mobile.SendMessage( 0x25, "Failed verification magic word. Try again with a different word." ); 
               		} 
		} 
            	else 
            	{ 
               		e.Mobile.SendMessage ( 0x35, "Format: MWSet <magicword>" ); 
               		e.Mobile.SendMessage ( 0x35, "Magic word must contain only letters of the alphabet. No spaces, quotes allowed." );          
            		} 
         	} 
         	else 
            		e.Mobile.SendMessage ( 0x35, "Magic word alredy set." ); 
      		} 

      		private static bool MagicWord( Mobile m ) 
      		{ 
         		Account acct=(Account)m.Account; 
         		string mword = Convert.ToString( acct.GetTag("mword") ); 
         		if ( mword == null ) 
            		return false; 
         		return true; 
		} 
	} 
} 