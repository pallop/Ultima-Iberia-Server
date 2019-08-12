/************************************************************************************************/
/**********Echo Dynamic Race Class System V2.0, ©2005********************************************/
/************************************************************************************************/

using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.RaceClass;
using Server.Items;

namespace Server.Gumps
{
	public class AddRaceGump : Gump
	{
		private const int FieldsPerPage = 14;

		private Mobile m_From;
		private Mobile m_Mobile;

		public AddRaceGump ( Mobile from, RaceOrb orb ) : base ( 20, 30 )
		{
			m_From = from;
			int b_distance = 45;
			int b_button = 1;
			Map map = Map.Trammel;
			orb.AmountBodyHues = 0;
			orb.AmountHairHues = 0;

			AddPage ( 0 );
			AddBackground( 0, 0, 720, 400, 5054 );

			AddImageTiled( 220, 10, 320, 23, 0x52 );
			AddImageTiled( 221, 11, 318, 23, 0xBBC );

			AddLabel( 270, 11, 0,  "Race/Class System *Add Race*" );

			AddLabel( 30, 45, 0,  "Race Name: " );
			AddImageTiled( 115, 43, 150, 23, 0x52 );
			AddImageTiled( 116, 44, 151, 23, 0xBBC );
			AddTextEntry( 120, 43, 144, 20, 1334, 21, "" );
			
			AddLabel( 30, 75, 0,  "Race Description: " );
			AddImageTiled( 36, 105, 250, 120, 0x52 );
			AddImageTiled( 37, 106, 251, 120, 0xBBC );
			AddTextEntry( 43, 105, 240, 105, 1334, 22, "" );
			
			
			for (int i=0; i<10; i++){
				AddLabel( 367, b_distance, 0,  "Add Body Hue: " );
				AddImageTiled( 455, b_distance-2, 50, 23, 0x52 );
				AddImageTiled( 456, b_distance-1, 51, 23, 0xBBC );
				AddTextEntry( 460, b_distance-2, 45, 20, 1334, b_button, "" );
				b_distance += 25;
				b_button += 1;
			}
			
			b_distance = 45;
			
			for (int i=0; i<10; i++){
				AddLabel( 545, b_distance, 0,  "Add Hair Hue: " );
				AddImageTiled( 630, b_distance-2, 50, 23, 0x52 );
				AddImageTiled( 631, b_distance-1, 51, 23, 0xBBC );
				AddTextEntry( 635, b_distance-2, 45, 20, 1334, b_button, "" );
				b_distance += 25;
				b_button += 1;
			}
			
			AddLabel( 30, 240, 0,  "Start Location:" );
			
			AddLabel( 55, 260, 0, "Trammel" );
			AddRadio( 25, 260, 208, 209, (map == Map.Trammel ? true : false), 6 );

			AddLabel( 55, 285, 0, "Felucca" );
			AddRadio( 25, 285, 208, 209, (map == Map.Felucca ? true : false), 7 );

			AddLabel( 55, 310, 0, "Malas" );
			AddRadio( 25, 310, 208, 209, (map == Map.Malas ? true : false), 8 );

			AddLabel( 195, 260, 0, "Ilshenar" );
			AddRadio( 165, 260, 208, 209, (map == Map.Ilshenar ? true : false), 9 );

			AddLabel( 195, 285, 0, "Tokuno" );
			AddRadio( 165, 285, 208, 209, (map == Map.Tokuno ? true : false), 10 );			
			
			AddLabel( 30, 335, 0,  "X: " );
			AddImageTiled( 60, 333, 50, 23, 0x52 );
			AddImageTiled( 61, 334, 51, 23, 0xBBC );
			AddTextEntry( 65, 333, 45, 20, 1334, 23, "" );
			
			AddLabel( 130, 335, 0,  "Y: " );
			AddImageTiled( 160, 333, 50, 23, 0x52 );
			AddImageTiled( 161, 334, 51, 23, 0xBBC );
			AddTextEntry( 165, 333, 45, 20, 1334, 24, "" );
			
			AddLabel( 230, 335, 0,  "Z: " );
			AddImageTiled( 260, 333, 50, 23, 0x52 );
			AddImageTiled( 261, 334, 51, 23, 0xBBC );
			AddTextEntry( 265, 333, 45, 20, 1334, 25, "" );
			
			AddLabel( 30, 370, 0,  "Click Here to Add the Race" );
			AddButton( 11, 371, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 1 );
							
		}
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			RaceControl r_control = null;
			RaceOrb r_orb = null;
			RaceOrb t_orb = null;
			bool name_used = false;
			bool isInt = true;
			bool is_void = false;
			string c_name = null;
			string n_name = null;
			
			foreach (Item i in World.Items.Values) {
				if (i is RaceControl)
				r_control = i as RaceControl;
			}
			
			foreach (Item i in World.Items.Values) {
				if (i is RaceOrb){
				r_orb = i as RaceOrb;
				if (r_orb.RaceNumber == r_control.A_Current)
				break;}
			}
			
			Map map = null;
				for( int i = 0; i < info.Switches.Length; i++ )
				{
					int m = info.Switches[i];
					switch( m )
					{
						case 6:  map = Map.Trammel;  break;
						case 7:  map = Map.Felucca;  break;
						case 8:  map = Map.Malas;    break;
						case 9:  map = Map.Ilshenar; break;
						case 10: map = Map.Tokuno;   break;
					}
			}
			
			
			if (info.ButtonID == 0) {
			r_orb.Delete();}
			
			if (info.ButtonID == 1) {
				if (r_orb != null) {
					TextRelay m_name = info.GetTextEntry( 21 );
					string text_name = ( m_name == null ? "" : m_name.Text.Trim() );
				
				if ( text_name.Length == 0 )
				{
					m_From.SendMessage( 0x35, "You must enter a Race Name." );
					m_From.SendGump( new AddRaceGump( from, r_orb ) );
					is_void = true;
				}
				else
				{
					foreach (Item x in World.Items.Values) {
						if (x is RaceOrb) {
							t_orb = x as RaceOrb;
							if (text_name != null)
								n_name = text_name.ToLower();
							if (t_orb.RaceName != null)
								c_name = t_orb.RaceName.ToLower();
							if (c_name == n_name) {
								name_used = true;
							}
						}
					}
					if ( name_used ) {
					m_From.SendMessage( 0x35, "That Race Name is already used." );
					if (!is_void){
					m_From.SendGump( new AddRaceGump( from, r_orb ) );
					is_void = true;}
					}
					else {
					r_orb.RaceName = text_name;
					r_orb.BackUpName = text_name;
					}
				}
				
					TextRelay m_desc = info.GetTextEntry( 22 );
					string text_desc = ( m_desc == null ? "" : m_desc.Text.Trim() );
				
				if ( text_desc.Length == 0 )
				{
					m_From.SendMessage( 0x35, "You must enter a Race Description." );
					if (!is_void){
					m_From.SendGump( new AddRaceGump( from, r_orb ) );
					is_void = true;}
				}
				else
				{
					r_orb.Description = text_desc;
				}
				
					TextRelay m_x = info.GetTextEntry( 23 );
					string text_x = ( m_x == null ? "" : m_x.Text.Trim() );

				if ( text_x.Length == 0 )
				{
					m_From.SendMessage( 0x35, "You must enter a X Coordinate" );
					if (!is_void){
					m_From.SendGump( new AddRaceGump( from, r_orb ) );
					is_void = true;}
				}
				else
				{
					isInt = true;
					try
					{
					int ix = Convert.ToInt32( text_x );
					}
					catch
					{
						from.SendMessage(0x35, "Coordinates must be numbers!" );
						if (!is_void){
						m_From.SendGump( new AddRaceGump( from, r_orb ) );
						is_void = true;}
						isInt = false;
					}
					if (isInt){
					int r_x = Convert.ToInt32( text_x );
					r_orb.Race_X = r_x;}
				}
				
					TextRelay m_y = info.GetTextEntry( 24 );
					string text_y = ( m_y == null ? "" : m_y.Text.Trim() );

				if ( text_y.Length == 0 )
				{
					m_From.SendMessage( 0x35, "You must enter a Y Coordinate" );
					if (!is_void){
					m_From.SendGump( new AddRaceGump( from, r_orb ) );
					is_void = true;}
				}
				else
				{
					isInt = true;
					try
					{
					int iy = Convert.ToInt32( text_y );
					}
					catch
					{
						from.SendMessage(0x35, "Coordinates must be numbers!" );
						if (!is_void){
						m_From.SendGump( new AddRaceGump( from, r_orb ) );
						is_void = true;}
						isInt = false;
					}
					if (isInt){
					int r_y = Convert.ToInt32( text_y );
					r_orb.Race_Y = r_y;}
				}
				
					TextRelay m_z = info.GetTextEntry( 25 );
					string text_z = ( m_z == null ? "" : m_z.Text.Trim() );

				if ( text_z.Length == 0 )
				{
					m_From.SendMessage( 0x35, "You must enter a Z Coordinate" );
					if (!is_void){
					m_From.SendGump( new AddRaceGump( from, r_orb ) );
					is_void = true;}
				}
				else
				{
					isInt = true;
					try
					{
					int iz = Convert.ToInt32( text_x );
					}
					catch
					{
						from.SendMessage(0x35, "Coordinates must be numbers!" );
						if (!is_void){
						m_From.SendGump( new AddRaceGump( from, r_orb ) );
						is_void = true;}
						isInt = false;
					}
					if (isInt){
					int r_z = Convert.ToInt32( text_z );
					r_orb.Race_Z = r_z;}
				}
				
				for (int i=1; i<11; i++) {
						TextRelay m_hue_body = info.GetTextEntry( i );
						string text_hue_body = ( m_hue_body == null ? "" : m_hue_body.Text.Trim() );
				
						if ( text_hue_body.Length == 0 )
						{
						if ( r_orb.AmountBodyHues == 0 ){
						from.SendMessage(0x35, "You must enter at least one Body Hue" );
						if (!is_void){
						m_From.SendGump( new AddRaceGump( from, r_orb ) );
						is_void = true;}
						break;}
						}
						else
						{
							isInt = true;
							try
							{
								int ihue = Convert.ToInt32( text_hue_body );
							}
							catch
							{
								from.SendMessage(0x35, "Hues must be numbers!" );
								if (!is_void){
								m_From.SendGump( new AddRaceGump( from, r_orb ) );
								is_void = true;}
								isInt = false;
							}
							if (isInt){
							int r_hue = Convert.ToInt32( text_hue_body );
							r_orb.BodyHues[i-1] = r_hue;
							r_orb.AmountBodyHues += 1;}
							
						}					
				}
				
				for (int i=11; i<21; i++) {
						TextRelay m_hue_hair = info.GetTextEntry( i );
						string text_hue_hair = ( m_hue_hair == null ? "" : m_hue_hair.Text.Trim() );
				
						if ( text_hue_hair.Length == 0 )
						{
						if ( r_orb.AmountHairHues == 0 ){
						from.SendMessage(0x35, "You must enter at least one Hair Hue" );
						if (!is_void){
						m_From.SendGump( new AddRaceGump( from, r_orb ) );
						is_void = true;}
						break;}
						}
						else
						{
							isInt = true;
							try
							{
								int ihue2 = Convert.ToInt32( text_hue_hair );
							}
							catch
							{
								from.SendMessage(0x35, "Hues must be numbers!" );
								if (!is_void){
								m_From.SendGump( new AddRaceGump( from, r_orb ) );
								is_void = true;}
								isInt = false;
							}
							if (isInt){
							int r_hue2 = Convert.ToInt32( text_hue_hair );
							r_orb.HairHues[i-11] = r_hue2;
							r_orb.AmountHairHues += 1;}
						}					
				}
				
				if (!is_void){
				
				Bag r_bag = new Bag();
				Bag i_bag = new Bag();
				
				r_bag.Name = r_orb.RaceName + " Race";
				i_bag.Name = r_orb.RaceName + " Items";
				
				r_bag.AddItem (i_bag);
				r_bag.AddItem (r_orb);
				
				foreach (Item i in World.Items.Values){
					if (i is Bag && i.Name == "RACES")
						i.AddItem (r_bag);
				}
			
				r_orb.Race_Map = map;
				r_orb.Name = r_orb.RaceName + " Race Orb";
				r_control.A_Races += 1;
				r_orb.Activated = true;
				from.SendMessage(6, "Race has been Generated!" );
				}
				
				}
			}
		}
		
	}
}