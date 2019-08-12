using System; 
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Regions;

namespace Server.Items
{ 
	public class CityRecruitStone : Item
	{
		private DateTime m_Time;
  
                public bool m_InUse = false;
  
                public int m_Recruits = 0;
                public Mobile m_Leader;

		private CityManagementStone m_Stone;
                private ArmyController m_Control;

		private UserTimer m_Timer;

                [CommandProperty( AccessLevel.GameMaster )]
                public bool InUse
                {
                        get{ return m_InUse; }
                        set{ m_InUse = value; }
                }

                [CommandProperty( AccessLevel.GameMaster )]
                public int Recruits
                {
                       get{ return m_Recruits; }
                       set{ m_Recruits = value; }
                }

                [CommandProperty( AccessLevel.GameMaster )]
                public Mobile Leader
                {
                       get{ return m_Leader; }
                       set{ m_Leader = value; }
                }
  
		public CityManagementStone Stone
		{
			get{ return m_Stone; }
			set{ m_Stone = value; }
		}
  
                public ArmyController Control
		{
			get{ return m_Control; }
			set{ m_Control = value; }
		}
  
		public CityRecruitStone() : base( 3804 )
		{ 
			Movable = false; 
			Name = "city recruiting stone";
		} 

		public override void OnDoubleClick( Mobile from )
		{
                        if ( from.FollowersMax <= (from.Followers +3))
                        {
                          from.SendMessage( "You have too many followers to Recruit anyone else." );
                          return;
                        }
                        if (this.InUse == true)
                        {
                          from.SendMessage( "You must wait 24 hours bewteen hiring each recruit." );
                          return;
                        }
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
                             if (from == this.Leader )
                             {
                                ArmyController army = from.Backpack.FindItemByType( typeof(ArmyController)  ) as ArmyController;
                                if( army != null)
                                {
                                   from.SendGump( new RecruitTypeGump(this, m_Control) );
                                }
                                else
				{
					from.SendMessage( "Your Army Controller appears to be missing page a GM!!." );
				}
                             }
                             else
                             {
                              from.SendMessage( "You must be the Leader of these Barracks to access this stone." );
                             }
			}
			else
			{
				from.SendMessage( "You are too far away to access that." );
			}
		} 

		/*public void RestartTimer()
		{
			m_Time = DateTime.Now ;
			m_Timer = new CityRecruitTimer( m_Time, this );
			m_Timer.Start();
		}

		public void RestartTimer( TimeSpan delay )
		{
			
			m_Time = DateTime.Now + delay;
			m_Timer = new CityRecruitTimer( m_Time, this );
			m_Timer.Start();
		}*/
		
		public CityRecruitStone( Serial serial ) : base( serial )
		{
		} 

		public override void OnDelete()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			base.OnDelete();
		}

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
                        writer.Write( m_Control);
			writer.WriteDeltaTime( m_Time );
                        writer.Write( m_Leader );
                        writer.Write( (bool)m_InUse );
                        writer.Write( (int)m_Recruits );
			writer.Write( m_Stone);
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 

			switch ( version )
			{
				case 0:
				{
                                        m_Control = (ArmyController)reader.ReadItem();
					m_Time = reader.ReadDeltaTime();
                                        m_Leader = reader.ReadMobile();
                                        m_InUse = (bool)reader.ReadBool();
                                        m_Recruits = (int)reader.ReadInt();
					m_Stone = (CityManagementStone)reader.ReadItem();
                                        if ( m_InUse = true )
                                        new UserTimer( this ).Start();
					break;
				}
			}
		}
         
       }
       
} 

