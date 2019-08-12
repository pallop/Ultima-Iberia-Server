using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Prompts;
using Server.Network;
using Server.Regions;
using Server.Mobiles;

namespace Server.Gumps
{
	public class RecruitTypeGump : Gump
	{
		private CityRecruitStone m_Stone;
                private ArmyController m_Control;
                
		public RecruitTypeGump( CityRecruitStone Stone, ArmyController Control ) : base( 50, 50 )
		{
			m_Stone = Stone;
                        m_Control= Control;
                        
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddBackground(30, 29, 289, 235, 5120);
			AddImageTiled(35, 58, 280, 10, 5121);
			AddHtml( 37, 34, 276, 19, @"<BASEFONT COLOR=WHITE><CENTER>Recruit Type</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddLabel(35, 65, 1149, @"Can chose 1 every 24 hours to follower limit" );
			AddLabel(35, 85, 1149, @"They start off using 3 control slots, they" );
			AddLabel(35, 105, 1149, @"take less as they get older (higher in skill)" );
			
			AddLabel(65, 130, 1149, @"Fighter");
                        AddImageTiledButton(45, 130, 11400, 11400, (int)Buttons.Fighter, GumpButtonType.Reply, 0, 11400, 0, 10, 10, 0);
			AddLabel(65, 155, 1149, @"Mage");
                        AddImageTiledButton(45, 155, 11400, 11400, (int)Buttons.Mage, GumpButtonType.Reply, 0, 11400, 0, 10, 10, 0);
			AddLabel(65, 180, 1149, @"Archer");
                        AddImageTiledButton(45, 185, 11400, 11400, (int)Buttons.Archer, GumpButtonType.Reply, 0, 11400, 0, 10, 10, 0);
			AddLabel(65, 205, 1149, @"Macer");
                        AddImageTiledButton(45, 205, 11400, 11400, (int)Buttons.Macer, GumpButtonType.Reply, 0, 11400, 0, 10, 10, 0);
			AddLabel(65, 230, 1149, @"Cannoneer");
                        AddImageTiledButton(45, 230, 11400, 11400, (int)Buttons.Cannoneer, GumpButtonType.Reply, 0, 11400, 0, 10, 10, 0);
		}
  
                public enum Buttons
                {
                   NONE,
                   Fighter,
                   Mage,
                   Archer,
                   Macer,
                   Cannoneer
                }
       
      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		switch ( info.ButtonID ) // Close
         		{
           
                           case (int)Buttons.Fighter:
                                  ArmyController army = from.Backpack.FindItemByType( typeof(ArmyController)  ) as ArmyController;
                                    if(m_Stone.Recruits == 0)
                                    {
                                      SoldierFighter soldier = new SoldierFighter();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 2, m_Stone.Y -1 , m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.RangeHome = 0;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 3, m_Stone.Y -1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 3, m_Stone.Y -1, m_Stone.Z + 10);
                                      army.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 1;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                    }
                                    if(m_Stone.Recruits == 1)
                                    {
                                      SoldierFighter soldier = new SoldierFighter();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 4, m_Stone.Y -1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 5, m_Stone.Y -1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 5, m_Stone.Y -1, m_Stone.Z + 10);
                                      army.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 2;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                    }
                                    if(m_Stone.Recruits == 2)
                                    {
                                      SoldierFighter soldier = new SoldierFighter();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 2, m_Stone.Y + 3, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 3, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 3, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 3;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                    if(m_Stone.Recruits == 3)
                                    {
                                      SoldierFighter soldier = new SoldierFighter();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 4, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 5, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 5, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 4;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 4)
                                    {
                                      SoldierFighter soldier = new SoldierFighter();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 6, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.IsBonded = true;
                                      soldier.Home = new Point3D( m_Stone.X - 5, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 5, m_Stone.Y + 1, m_Stone.Z + 10);
                                      army.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 5;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 5)
                                    {
                                      SoldierFighter soldier = new SoldierFighter();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 6, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.IsBonded = true;
                                      soldier.Home = new Point3D( m_Stone.X - 7, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 7, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 6;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 6)
                                    {
                                      SoldierFighter soldier = new SoldierFighter();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 8, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.IsBonded = true;
                                      soldier.Home = new Point3D( m_Stone.X - 7, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 7, m_Stone.Y + 1, m_Stone.Z + 10);
                                      army.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 7;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 7)
                                    {
                                      SoldierFighter soldier = new SoldierFighter();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 8, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.IsBonded = true;
                                      soldier.Home = new Point3D( m_Stone.X - 9, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 9, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 8;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 8)
                                    {
                                      SoldierFighter soldier = new SoldierFighter();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 10, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.IsBonded = true;
                                      soldier.Home = new Point3D( m_Stone.X - 9, m_Stone.Y + 2, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 9, m_Stone.Y + 2, m_Stone.Z + 10);
                                      army.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 9;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 9)
                                    {
                                      SoldierFighter soldier = new SoldierFighter();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 10, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.IsBonded = true;
                                      soldier.Home = new Point3D( m_Stone.X - 9, m_Stone.Y + 3, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 9, m_Stone.Y + 3, m_Stone.Z + 10);
                                      army.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 10;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     else
                                     {
                                      from.SendMessage( "You dont have anymore beds." );
                                      return;
                                     }
                           break;
                           case (int)Buttons.Mage:
                           ArmyController army1 = from.Backpack.FindItemByType( typeof(ArmyController)  ) as ArmyController;
                                    if(m_Stone.Recruits == 0)
                                    {
                                      SoldierMage soldier = new SoldierMage();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 2, m_Stone.Y -1 , m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.RangeHome = 0;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.IsBonded = true;
                                      soldier.Home = new Point3D( m_Stone.X - 3, m_Stone.Y -1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 3, m_Stone.Y -1, m_Stone.Z + 10);
                                      army1.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 1;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                    }
                                    if(m_Stone.Recruits == 1)
                                    {
                                      SoldierMage soldier = new SoldierMage();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 4, m_Stone.Y -1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.IsBonded = true;
                                      soldier.Home = new Point3D( m_Stone.X - 5, m_Stone.Y -1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 5, m_Stone.Y -1, m_Stone.Z + 10);
                                      army1.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 2;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                    }
                                    if(m_Stone.Recruits == 2)
                                    {
                                      SoldierMage soldier = new SoldierMage();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 2, m_Stone.Y + 3, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.IsBonded = true;
                                      soldier.Home = new Point3D( m_Stone.X - 3, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 3, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army1.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 3;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                    if(m_Stone.Recruits == 3)
                                    {
                                      SoldierMage soldier = new SoldierMage();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 4, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 5, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 5, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army1.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 4;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 4)
                                    {
                                      SoldierMage soldier = new SoldierMage();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 6, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 5, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 5, m_Stone.Y + 1, m_Stone.Z + 10);
                                      army1.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 5;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 5)
                                    {
                                      SoldierMage soldier = new SoldierMage();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 6, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 7, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 7, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army1.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 6;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 6)
                                    {
                                      SoldierMage soldier = new SoldierMage();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 8, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 7, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 7, m_Stone.Y + 1, m_Stone.Z + 10);
                                      army1.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 7;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 7)
                                    {
                                      SoldierMage soldier = new SoldierMage();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 8, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 9, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 9, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army1.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 8;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 8)
                                    {
                                      SoldierMage soldier = new SoldierMage();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 10, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 9, m_Stone.Y + 2, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 9, m_Stone.Y + 2, m_Stone.Z + 10);
                                      army1.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 9;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 9)
                                    {
                                      SoldierMage soldier = new SoldierMage();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 10, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 9, m_Stone.Y + 3, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 9, m_Stone.Y + 3, m_Stone.Z + 10);
                                      army1.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 10;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     else
                                     {
                                      from.SendMessage( "You dont have anymore beds." );
                                      return;
                                     }
                           break;
                           case (int)Buttons.Archer:
                           ArmyController army2 = from.Backpack.FindItemByType( typeof(ArmyController)  ) as ArmyController;
                                    if(m_Stone.Recruits == 0)
                                    {
                                      SoldierArcher soldier = new SoldierArcher();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 2, m_Stone.Y -1 , m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.RangeHome = 0;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.IsBonded = true;
                                      soldier.Home = new Point3D( m_Stone.X - 3, m_Stone.Y -1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 3, m_Stone.Y -1, m_Stone.Z + 10);
                                      army2.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 1;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                    }
                                    if(m_Stone.Recruits == 1)
                                    {
                                      SoldierArcher soldier = new SoldierArcher();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 4, m_Stone.Y -1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.IsBonded = true;
                                      soldier.Home = new Point3D( m_Stone.X - 5, m_Stone.Y -1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 5, m_Stone.Y -1, m_Stone.Z + 10);
                                      army2.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 2;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                    }
                                    if(m_Stone.Recruits == 2)
                                    {
                                      SoldierArcher soldier = new SoldierArcher();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 2, m_Stone.Y + 3, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.IsBonded = true;
                                      soldier.Home = new Point3D( m_Stone.X - 3, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 3, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army2.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 3;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                    if(m_Stone.Recruits == 3)
                                    {
                                      SoldierArcher soldier = new SoldierArcher();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 4, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.IsBonded = true;
                                      soldier.Home = new Point3D( m_Stone.X - 5, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 5, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army2.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 4;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 4)
                                    {
                                      SoldierArcher soldier = new SoldierArcher();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 6, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.IsBonded = true;
                                      soldier.Home = new Point3D( m_Stone.X - 5, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 5, m_Stone.Y + 1, m_Stone.Z + 10);
                                      army2.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 5;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 5)
                                    {
                                      SoldierArcher soldier = new SoldierArcher();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 6, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 7, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 7, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army2.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 6;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 6)
                                    {
                                      SoldierArcher soldier = new SoldierArcher();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 8, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 7, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 7, m_Stone.Y + 1, m_Stone.Z + 10);
                                      army2.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 7;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 7)
                                    {
                                      SoldierArcher soldier = new SoldierArcher();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 8, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 9, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 9, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army2.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 8;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 8)
                                    {
                                      SoldierArcher soldier = new SoldierArcher();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 10, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 9, m_Stone.Y + 2, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 9, m_Stone.Y + 2, m_Stone.Z + 10);
                                      army2.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 9;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 9)
                                    {
                                      SoldierArcher soldier = new SoldierArcher();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 10, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 9, m_Stone.Y + 3, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 9, m_Stone.Y + 3, m_Stone.Z + 10);
                                      army2.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 10;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     else
                                     {
                                      from.SendMessage( "You dont have anymore beds." );
                                      return;
                                     }
                           break;
                           case (int)Buttons.Macer:
                           ArmyController army3 = from.Backpack.FindItemByType( typeof(ArmyController)  ) as ArmyController;
                                    if(m_Stone.Recruits == 0)
                                    {
                                      SoldierMacer soldier = new SoldierMacer();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 2, m_Stone.Y -1 , m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.RangeHome = 0;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 3, m_Stone.Y -1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 3, m_Stone.Y -1, m_Stone.Z + 10);
                                      army3.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 1;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                    }
                                    if(m_Stone.Recruits == 1)
                                    {
                                      SoldierMacer soldier = new SoldierMacer();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 4, m_Stone.Y -1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 5, m_Stone.Y -1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 5, m_Stone.Y -1, m_Stone.Z + 10);
                                      army3.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 2;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                    }
                                    if(m_Stone.Recruits == 2)
                                    {
                                      SoldierMacer soldier = new SoldierMacer();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 2, m_Stone.Y + 3, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 3, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 3, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army3.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 3;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                    if(m_Stone.Recruits == 3)
                                    {
                                      SoldierMacer soldier = new SoldierMacer();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 4, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 5, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 5, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army3.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 4;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 4)
                                    {
                                      SoldierMacer soldier = new SoldierMacer();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 6, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 5, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 5, m_Stone.Y + 1, m_Stone.Z + 10);
                                      army3.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 5;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 5)
                                    {
                                      SoldierMacer soldier = new SoldierMacer();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 6, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 7, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 7, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army3.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 6;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 6)
                                    {
                                      SoldierMacer soldier = new SoldierMacer();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 8, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 7, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 7, m_Stone.Y + 1, m_Stone.Z + 10);
                                      army3.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 7;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 7)
                                    {
                                      SoldierMacer soldier = new SoldierMacer();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 8, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 9, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 9, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army3.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 8;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 8)
                                    {
                                      SoldierMacer soldier = new SoldierMacer();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 10, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 9, m_Stone.Y + 2, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 9, m_Stone.Y + 2, m_Stone.Z + 10);
                                      army3.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 9;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 9)
                                    {
                                      SoldierMacer soldier = new SoldierMacer();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 10, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 9, m_Stone.Y + 3, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 9, m_Stone.Y + 3, m_Stone.Z + 10);
                                      army3.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 10;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     else
                                     {
                                      from.SendMessage( "You dont have anymore beds." );
                                      return;
                                     }
                           break;
                           case (int)Buttons.Cannoneer:
                           ArmyController army4 = from.Backpack.FindItemByType( typeof(ArmyController)  ) as ArmyController;
                                    if(m_Stone.Recruits == 0)
                                    {
                                      TurnableCannonGuard soldier = new TurnableCannonGuard();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 2, m_Stone.Y -1 , m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.RangeHome = 0;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 3, m_Stone.Y -1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 3, m_Stone.Y -1, m_Stone.Z + 10);
                                      army4.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 1;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                    }
                                    if(m_Stone.Recruits == 1)
                                    {
                                      TurnableCannonGuard soldier = new TurnableCannonGuard();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 4, m_Stone.Y -1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 5, m_Stone.Y -1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 5, m_Stone.Y -1, m_Stone.Z + 10);
                                      army4.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 2;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                    }
                                    if(m_Stone.Recruits == 2)
                                    {
                                      TurnableCannonGuard soldier = new TurnableCannonGuard();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 2, m_Stone.Y + 3, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 3, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 3, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army4.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 3;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                    if(m_Stone.Recruits == 3)
                                    {
                                      TurnableCannonGuard soldier = new TurnableCannonGuard();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 4, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 5, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 5, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army4.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 4;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 4)
                                    {
                                      TurnableCannonGuard soldier = new TurnableCannonGuard();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 6, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 5, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 5, m_Stone.Y + 1, m_Stone.Z + 10);
                                      army4.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 5;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 5)
                                    {
                                      TurnableCannonGuard soldier = new TurnableCannonGuard();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 6, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 7, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 7, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army4.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 6;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 6)
                                    {
                                      TurnableCannonGuard soldier = new TurnableCannonGuard();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 8, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 7, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 7, m_Stone.Y + 1, m_Stone.Z + 10);
                                      army4.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 7;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 7)
                                    {
                                      TurnableCannonGuard soldier = new TurnableCannonGuard();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 8, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 9, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 9, m_Stone.Y + 4, m_Stone.Z + 10);
                                      army4.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 8;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 8)
                                    {
                                      TurnableCannonGuard soldier = new TurnableCannonGuard();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 10, m_Stone.Y + 1, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 9, m_Stone.Y + 2, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 9, m_Stone.Y + 2, m_Stone.Z + 10);
                                      army4.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 9;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     if(m_Stone.Recruits == 9)
                                    {
                                      TurnableCannonGuard soldier = new TurnableCannonGuard();
                                      soldier.HouseLocation = new Point3D( m_Stone.X - 10, m_Stone.Y + 4, m_Stone.Z + 10);
                                      soldier.ControlMaster = m_Stone.Leader;
                                      soldier.Controlled = true;
                                      soldier.IsBonded = true;
                                      soldier.HouseMap = m_Stone.Map;
                                      soldier.IsHired = true;
                                      soldier.Home = new Point3D( m_Stone.X - 9, m_Stone.Y + 3, m_Stone.Z + 10);
                                      soldier.Map = m_Stone.Map;
                                      soldier.Location = new Point3D( m_Stone.X - 9, m_Stone.Y + 3, m_Stone.Z + 10);
                                      army4.Recruit(from, (BaseCreature)soldier);
                                      m_Stone.Recruits = 10;
                                      m_Stone.InUse = true;
                                      new UserTimer( m_Stone ).Start();
                                      from.SendMessage( "You have recruited a soldier." );
                                      return;
                                     }
                                     else
                                     {
                                      from.SendMessage( "You dont have anymore beds." );
                                      return;
                                     }
                           break;
                           default:
                           return; //CLOSE
			}
		}
	}
 public class UserTimer : Timer
         {
          private CityRecruitStone m_stone;
          public UserTimer( CityRecruitStone stone ) : base( TimeSpan.FromMinutes( 2 ) )
          {
            Priority = TimerPriority.FiftyMS;
            m_stone = stone;
          }
          protected override void OnTick()
          {
            m_stone.InUse = false;
            Stop();
          }
         }
}
