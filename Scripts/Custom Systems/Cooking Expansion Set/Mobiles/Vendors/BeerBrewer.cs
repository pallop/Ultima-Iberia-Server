using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class BeerBrewer : BaseVendor
	{

      private static bool m_Talked; // flag to prevent spam 

      string[] kfcsay = new string[] // things to say while greating 
      { 
         "I'm brewing up some fun in a bottle", 
         "Would you like to learn how to brew fun in a bottle?",   
         "Have a nice day",
      };

      private List<SBInfo> m_SBInfos = new List<SBInfo>();
      protected override List<SBInfo> SBInfos { get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.TinkersGuild; } }

		[Constructable]
		public BeerBrewer() : base( "The Beer Brewer" )
		{
			SetSkill( SkillName.Parry, 85.0, 150.0 );
			SetSkill( SkillName.Swords, 60.0, 183.0 );
			SetSkill( SkillName.ArmsLore, 85.0, 150.0 );
			SetSkill( SkillName.Anatomy, 60.0, 183.0 );
			SetSkill( SkillName.Archery, 85.0, 150.0 );
			SetSkill( SkillName.Hiding, 60.0, 183.0 );

		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBBeerBrewer() );
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.Shirt() );
			AddItem( new Server.Items.LongPants() );
			AddItem( new Server.Items.Boots() );
		}

		
		public override void OnMovement( Mobile m, Point3D oldLocation ) 
               {                                                    
         if( m_Talked == false ) 
         { 
            if ( m.InRange( this, 4 ) ) 
            {                
               m_Talked = true; 
               SayRandom( kfcsay, this ); 
               this.Move( GetDirectionTo( m.Location ) ); 
                  // Start timer to prevent spam 
               SpamTimer t = new SpamTimer(); 
               t.Start(); 
            } 
         } 
      } 

      private class SpamTimer : Timer 
      { 
         public SpamTimer() : base( TimeSpan.FromSeconds( 8 ) ) 
         { 
            Priority = TimerPriority.OneSecond; 
         } 

         protected override void OnTick() 
         { 
            m_Talked = false; 
         } 
      } 

      private static void SayRandom( string[] say, Mobile m ) 
      { 
         m.Say( say[Utility.Random( say.Length )] ); 
      } 

		public BeerBrewer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}