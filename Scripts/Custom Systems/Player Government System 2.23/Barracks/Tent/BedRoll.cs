//==============================================//
// Created by Dupre					//
// Thanks to:						//
// Zippy							//
// Ike							//
// Ignacio							//
//								//
// For putting up with a 'tard like me :)		//
//								//
//==============================================//
using System; 
using Server; 
using Server.Items; 
using Server.Mobiles;
using Server.Network;
using Server.Gumps;

namespace Server.Items 
{ 
   public class BedRoll1 : BaseAddon
   { 
      [Constructable] 
      public BedRoll1( TentWalls tentwalls,TentRoof tentroof,TentFloor tentfloor,TentTrim tenttrim, PlayerMobile player, SecureTent chest, TentVerifier tentverifier)
      { 
         Name = "A Sleeping Bag"; 
         m_Player = player; 
         m_TentRoof = tentroof; 
         m_TentWalls = tentwalls; 
         m_TentFloor = tentfloor; 
         m_TentTrim = tenttrim; 
	   m_Chest = chest;
     m_TentVerifier = tentverifier;
         this.ItemID = 2645; 
         this.Visible = true; 
	   Hue = 1072;
      } 
      private TentRoof m_TentRoof; 
      private TentWalls m_TentWalls; 
      private TentTrim m_TentTrim; 
      private TentFloor m_TentFloor; 
      public PlayerMobile m_Player;
      private SecureTent m_Chest; 
       private TentVerifier m_TentVerifier;

      /*public override void OnDoubleClick( Mobile from )
      { 
      if (m_Player==from)
	   {
	   from.SendGump (new TentDGump(this,from));
         } 
      else 
         { 
         from.SendMessage( "You don't appear to own this Tent." ); 
         } 
      }*/
      public override void OnDoubleClick( Mobile from )
      {
      if (m_Player==from)
 		{
        if ( m_Chest != null && m_Chest.Items.Count > 0 )
 		{
         from.SendMessage( "You must remove the items from the travel bag before packing up your tent." );
        }
     else
         {
		from.SendGump (new TentDGump(this,from));
		}
         }
      else
         {
         from.SendMessage( "You don't appear to own this Tent." );
         }
      }
      public override void OnDelete() 
      { 
      m_TentFloor.Delete(); 
      m_TentTrim.Delete(); 
      m_TentWalls.Delete(); 
      m_TentRoof.Delete(); 
      m_Chest.Delete(); 
      m_TentVerifier.Delete();
      } 


      public BedRoll1( Serial serial ) : base( serial )
      { 
      } 

      public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 
         writer.Write( (int) 0 ); // version 
         writer.Write( m_TentTrim ); 
         writer.Write( m_TentFloor ); 
         writer.Write( m_TentWalls ); 
         writer.Write( m_TentRoof ); 
         writer.Write( m_Player ); 
         writer.Write( m_Chest ); 
          writer.Write( m_TentVerifier );
      } 

      public override void Deserialize( GenericReader reader ) 
      { 
         base.Deserialize( reader ); 
         int version = reader.ReadInt(); 

         m_TentTrim = (TentTrim)reader.ReadItem(); 
         m_TentFloor = (TentFloor)reader.ReadItem(); 
         m_TentWalls = (TentWalls)reader.ReadItem(); 
         m_TentRoof = (TentRoof)reader.ReadItem(); 
         m_Player = (PlayerMobile)reader.ReadMobile(); 
         m_Chest = (SecureTent)reader.ReadItem(); 
          m_TentVerifier = (TentVerifier)reader.ReadItem();
      } 

   } 
}
