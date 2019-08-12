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
	public class ArmyHelpGump : Gump
	{
		//private CityRecruitStone m_Stone;
                //private ArmyController m_Control;
                
		public ArmyHelpGump( /*CityRecruitStone Stone, ArmyController Control*/ ) : base( 50, 50 )
		{
			//m_Stone = Stone;
                        //m_Control= Control;
                        
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddBackground(30, 29, 800, 635, 5120);
			AddImageTiled(35, 58, 800, 10, 5121);
			AddHtml( 95, 34, 276, 19, @"<BASEFONT COLOR=WHITE><CENTER>Soldier Voice Commands</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddLabel(35, 65, 1149, @"You can wake individual sleeping soldiers by saying thier name within 2 squares" );
			AddLabel(35, 85, 1149, @"Saying soldier standdown or soldiers standown will put all soldiers in range into thier default AI" );
			AddLabel(35, 105, 1149, @"Saying soldier attention or soldiers attention will put all soldiers in range into Army AI (controlled by controller)" );
			
			AddLabel(35, 130, 1149, @"All commands below here need the Soldiers name in front of the command");
			AddLabel(35, 155, 1149, @"Name = will simply respond with a comment based on thier motivation");
			AddLabel(35, 180, 1149, @"who's your master: who's your daddy: who's your leader: who's your commander = Will tell anyone who thier owner is.");
			AddLabel(35, 205, 1149, @"make camp: setup camp = If in valid area and they have a tent they will set it up (more features coming soon)");
			AddLabel(35, 230, 1149, @"return home = If they have recall charges will return to thier barracks and wait");
                        AddLabel(35, 255, 1149, @"hide = Will hide themself");
                        AddLabel(35, 280, 1149, @"show = Will reveal themself");
                        AddLabel(35, 305, 1149, @"dismount = will dismount whatever mount they are riding");
                        AddLabel(35, 330, 1149, @"remount horse: remount llama: remount ostard = For a charge will mount said beast");
                        AddLabel(35, 355, 1149, @"remount swamp dragon: remount barded swampdragon: remount ridgeback = For a charge & with enough fame will mount said beast");
                        AddLabel(35, 380, 1149, @"remount skeletal horse: remount fire steed: remount nightmare = For a charge & with enough fame/karma will mount said beast");
                        AddLabel(35, 405, 1149, @"remount kirin: remount unicorn = For a charge & with enough fame, if the correct sex will mount said beast");
                        AddLabel(35, 430, 1149, @"food: cook: dinner: = Will try and cook any and all food in thier backback (will gain skill in cooking)");
                        AddLabel(35, 455, 1149, @"smelt = Will try to smelt any ore in thier pack (will gain skill in mining and str)");
                        AddLabel(35, 480, 1149, @"heal me = Will try to heal any damage you have (will gain skill in healing)");
                        AddLabel(35, 505, 1149, @"disrobe = Will remove all equipped items (except weapon)");
                        AddLabel(35, 530, 1149, @"disarm = Will remove weapon to backpack");
                        AddLabel(35, 580, 1149, @"All commands below here need the Soldiers name and will simply give thier skill level");
                        AddLabel(35, 605, 1149, @"sword skill: healing skill: cooking skill: tactic skill: parry skill: mining skill: mage skill: meditation skill:");
                        AddLabel(35, 630, 1149, @"anatomy skill: focus skill: wrestling skill");
            }
  
	}
}


