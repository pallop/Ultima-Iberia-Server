//==============================================// 
// Created by Dupre               			// 
//==============================================// 
using System; 
using Server; 
using Server.Multis; 
using Server.Network; 
using Server.Mobiles; 

namespace Server.Items 
{ 
      public class SecureTent : BaseContainer
   { 
      private Mobile m_Player; 
      private TentWalls m_TentWalls; 
      private TentRoof m_TentRoof; 
      private TentTrim m_TentTrim; 
      private TentFloor m_TentFloor; 

      public override int DefaultGumpID{ get{ return 0x3C; } } 
      public override int DefaultDropSound{ get{ return 0x42; } } 

      public override Rectangle2D Bounds 
      { 
         get{ return new Rectangle2D( 16, 51, 168, 73 ); } 
      } 

      public SecureTent( Mobile player, TentWalls tentwalls, TentRoof tentroof,TentFloor tentfloor,TentTrim tenttrim ) : base( 0xE80 ) 
      { 
         m_Player = player; 
         m_TentRoof = tentroof; 
         m_TentWalls = tentwalls; 
         m_TentFloor = tentfloor; 
         m_TentTrim = tenttrim; 
         this.ItemID = 2482; 
         this.Visible = true; 
         this.Movable = false; 
         MaxItems = 25; 
      } 

      [CommandProperty( AccessLevel.GameMaster )] 
      public Mobile Player 
      { 
         get 
         { 
            return m_Player; 
         } 
         set 
         { 
            m_Player = value; 
            InvalidateProperties(); 
         } 
      } 

      public override int MaxWeight 
      { 
         get 
         { 
            return 0; 
         } 
      } 

      public SecureTent( Serial serial ) : base(serial) 
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
      } 

      private void Validate() 
      { 
         if ( m_Player != null && m_TentWalls != null) 
         { 
            Console.WriteLine( "Warning: Destroying SecureTent of {0}", m_Player.Name ); 
            Destroy(); 
         } 
      } 

      public override TimeSpan DecayTime 
      { 
         get 
         { 
            return TimeSpan.FromMinutes( 30.0 ); 
         } 
      } 

      public override void AddNameProperty( ObjectPropertyList list ) 
      { 
         if ( m_Player != null ) 
            list.Add( "A Secure Travel Bag" );
         else 
            base.AddNameProperty( list ); 
      } 

      public override void OnSingleClick( Mobile from ) 
      { 
         if ( m_Player != null ) 
         { 
            LabelTo( from, "A Secure Travel Bag");

            if ( CheckContentDisplay( from ) ) 
               LabelTo( from, "({0} items, {1} stones)", TotalItems, TotalWeight ); 
         } 
         else 
         { 
            base.OnSingleClick( from ); 
         } 
      } 

      public override bool IsAccessibleTo( Mobile m ) 
      { 
		if (( m==m_Player || m_Player == null || m_Player.Deleted || m_TentWalls == null || m_TentWalls.Deleted || m.AccessLevel >= AccessLevel.GameMaster))
            {return true;}
		else
		{return false;}

         return m == m_Player && base.IsAccessibleTo( m ); 
      } 
   } 
}
