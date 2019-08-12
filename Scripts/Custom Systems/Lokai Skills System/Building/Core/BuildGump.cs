/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Server.Gumps;
using Server.Network;
using Server.Items;

namespace Server.Engines.Build
{
	public class BuildGump : Gump
	{
		private Mobile m_From;
		private BuildSystem m_BuildSystem;
		private BaseBuildingTool m_Tool;

		private BuildPage m_Page;

		private const int LabelHue = 0x480;
		private const int LabelColor = 0x7FFF;
		private const int FontColor = 0xFFFFFF;

		private enum BuildPage
		{
			None,
			PickResource,
			PickResource2
		}

		/*public BuildGump( Mobile from, BuildSystem buildSystem, BaseBuildingTool tool ): this( from, buildSystem, -1, -1, tool, null )
		{
		}*/

		public BuildGump( Mobile from, BuildSystem buildSystem, BaseBuildingTool tool, object notice ) : this( from, buildSystem, tool, notice, BuildPage.None )
		{
		}

		private BuildGump( Mobile from, BuildSystem buildSystem, BaseBuildingTool tool, object notice, BuildPage page ) : base( 40, 40 )
		{
			m_From = from;
			m_BuildSystem = buildSystem;
			m_Tool = tool;
			m_Page = page;

			BuildContext context = buildSystem.GetContext( from );

			from.CloseGump( typeof( BuildGump ) );
			from.CloseGump( typeof( BuildGumpItem ) );

			AddPage( 0 );

			AddBackground( 0, 0, 530, 437, 5054 );
			AddImageTiled( 10, 10, 510, 22, 2624 );
			AddImageTiled( 10, 292, 150, 45, 2624 );
			AddImageTiled( 165, 292, 355, 45, 2624 );
			AddImageTiled( 10, 342, 510, 85, 2624 );
			AddImageTiled( 10, 37, 200, 250, 2624 );
			AddImageTiled( 215, 37, 305, 250, 2624 );
			AddAlphaRegion( 10, 10, 510, 417 );

			if ( buildSystem.GumpTitleNumber > 0 )
				AddHtmlLocalized( 10, 12, 510, 20, buildSystem.GumpTitleNumber, LabelColor, false, false );
			else
				AddHtml( 10, 12, 510, 20, MakeTitle(buildSystem.GumpTitleString), false, false );

			AddHtmlLocalized( 10, 37, 200, 22, 1044010, LabelColor, false, false ); // <CENTER>CATEGORIES</CENTER>
			AddHtmlLocalized( 215, 37, 305, 22, 1044011, LabelColor, false, false ); // <CENTER>SELECTIONS</CENTER>
			AddHtmlLocalized( 10, 302, 150, 25, 1044012, LabelColor, false, false ); // <CENTER>NOTICES</CENTER>

			AddButton( 15, 402, 4017, 4019, 0, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 50, 405, 150, 18, 1011441, LabelColor, false, false ); // EXIT

			AddButton( 270, 402, 4005, 4007, GetButtonID( 6, 2 ), GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 305, 405, 150, 18, 1044013, LabelColor, false, false ); // MAKE LAST

			// Mark option
			if ( buildSystem.MarkOption )
			{
				AddButton( 270, 362, 4005, 4007, GetButtonID( 6, 6 ), GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 305, 365, 150, 18, 1044017 + (context == null ? 0 : (int)context.MarkOption), LabelColor, false, false ); // MARK ITEM
			}
			// ****************************************

			// Resmelt option
			if ( buildSystem.Resmelt )
			{
				AddButton( 15, 342, 4005, 4007, GetButtonID( 6, 1 ), GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 50, 345, 150, 18, 1044259, LabelColor, false, false ); // SMELT ITEM
			}
			// ****************************************

			// Repair option
			if ( buildSystem.Repair )
			{
				AddButton( 270, 342, 4005, 4007, GetButtonID( 6, 5 ), GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 305, 345, 150, 18, 1044260, LabelColor, false, false ); // REPAIR ITEM
			}
			// ****************************************

			// Enhance option
			if ( buildSystem.CanEnhance )
			{
				AddButton( 270, 382, 4005, 4007, GetButtonID( 6, 8 ), GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 305, 385, 150, 18, 1061001, LabelColor, false, false ); // ENHANCE ITEM
			}
			// ****************************************

			if ( notice is int && (int)notice > 0 )
				AddHtmlLocalized( 170, 295, 350, 40, (int)notice, LabelColor, false, false );
			else if ( notice is string )
				AddHtml( 170, 295, 350, 40, String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", FontColor, notice ), false, false );

			// If the system has more than one resource
			if ( buildSystem.BuildSubRes.Init )
			{
				string nameString = buildSystem.BuildSubRes.NameString;
				int nameNumber = buildSystem.BuildSubRes.NameNumber;

				int resIndex = ( context == null ? -1 : context.LastResourceIndex );

                Type resourceType = buildSystem.BuildSubRes.ResType;

				if ( resIndex > -1 )
				{
					BuildSubRes subResource = buildSystem.BuildSubRes.GetAt( resIndex );

					nameString = subResource.NameString;
					nameNumber = subResource.NameNumber;
                    resourceType = subResource.ItemType;
				}

                int resourceCount = 0;

                if ( from.Backpack != null )
                {
                    Item[] items = from.Backpack.FindItemsByType( resourceType, true );

                    for ( int i = 0; i < items.Length; ++i )
                        resourceCount += items[i].Amount;
                }

				AddButton( 15, 362, 4005, 4007, GetButtonID( 6, 0 ), GumpButtonType.Reply, 0 );

				if ( nameNumber > 0 )
					AddHtmlLocalized( 50, 365, 250, 18, nameNumber, resourceCount.ToString(), LabelColor, false, false );
				else
                    AddLabel( 50, 362, LabelHue, String.Format( "{0} ({1} Available)", nameString, resourceCount ) );
			}
			// ****************************************

			// For dragon scales
			if ( buildSystem.BuildSubRes2.Init )
			{
				string nameString = buildSystem.BuildSubRes2.NameString;
				int nameNumber = buildSystem.BuildSubRes2.NameNumber;

				int resIndex = ( context == null ? -1 : context.LastResourceIndex2 );

                Type resourceType = buildSystem.BuildSubRes.ResType;

				if ( resIndex > -1 )
				{
					BuildSubRes subResource = buildSystem.BuildSubRes2.GetAt( resIndex );

					nameString = subResource.NameString;
					nameNumber = subResource.NameNumber;
                    resourceType = subResource.ItemType;
				}

                int resourceCount = 0;

                if ( from.Backpack != null )
                {
                    Item[] items = from.Backpack.FindItemsByType( resourceType, true );

                    for ( int i = 0; i < items.Length; ++i )
                        resourceCount += items[i].Amount;
                }

				AddButton( 15, 382, 4005, 4007, GetButtonID( 6, 7 ), GumpButtonType.Reply, 0 );

				if ( nameNumber > 0 )
                    AddHtmlLocalized( 50, 385, 250, 18, nameNumber, resourceCount.ToString(), LabelColor, false, false );
				else
                    AddLabel( 50, 385, LabelHue, String.Format( "{0} ({1} Available)", nameString, resourceCount ) );
			}
			// ****************************************

			CreateGroupList();

			if ( page == BuildPage.PickResource )
				CreateResList( false );
			else if ( page == BuildPage.PickResource2 )
				CreateResList( true );
			else if ( context != null && context.LastGroupIndex > -1 )
				CreateItemList( context.LastGroupIndex );
		}

        public string MakeTitle(string title)
        {
            return String.Format("<BASEFONT COLOR=#{0:X6}><CENTER>{1}</CENTER></BASEFONT>", FontColor, title);
        }

		public void CreateResList( bool opt )
		{
			BuildSubResCol res = ( opt ? m_BuildSystem.BuildSubRes2 : m_BuildSystem.BuildSubRes );

			for ( int i = 0; i < res.Count; ++i )
			{
				int index = i % 10;

				BuildSubRes subResource = res.GetAt( i );

				if ( index == 0 )
				{
					if ( i > 0 )
						AddButton( 485, 260, 4005, 4007, 0, GumpButtonType.Page, (i / 10) + 1 );

					AddPage( (i / 10) + 1 );

					if ( i > 0 )
						AddButton( 455, 260, 4014, 4015, 0, GumpButtonType.Page, i / 10 );

					BuildContext context = m_BuildSystem.GetContext( m_From );

					AddButton( 220, 260, 4005, 4007, GetButtonID( 6, 4 ), GumpButtonType.Reply, 0 );
					AddHtmlLocalized( 255, 263, 200, 18, (context == null || !context.DoNotColor) ? 1061591 : 1061590, LabelColor, false, false );
				}

				AddButton( 220, 60 + (index * 20), 4005, 4007, GetButtonID( 5, i ), GumpButtonType.Reply, 0 );

				if ( subResource.NameNumber > 0 )
					AddHtmlLocalized( 255, 63 + (index * 20), 250, 18, subResource.NameNumber, LabelColor, false, false );
				else
					AddLabel( 255, 60 + (index * 20), LabelHue, subResource.NameString );
			}
		}

		public void CreateMakeLastList()
		{
			BuildContext context = m_BuildSystem.GetContext( m_From );

			if ( context == null )
				return;

			ArrayList items = context.Items;

			if ( items.Count > 0 )
			{
				for ( int i = 0; i < items.Count; ++i )
				{
					int index = i % 10;

					BuildItem buildItem = (BuildItem)items[i];

					if ( index == 0 )
					{
						if ( i > 0 )
						{
							AddButton( 370, 260, 4005, 4007, 0, GumpButtonType.Page, (i / 10) + 1 );
							AddHtmlLocalized( 405, 263, 100, 18, 1044045, LabelColor, false, false ); // NEXT PAGE
						}

						AddPage( (i / 10) + 1 );

						if ( i > 0 )
						{
							AddButton( 220, 260, 4014, 4015, 0, GumpButtonType.Page, i / 10 );
							AddHtmlLocalized( 255, 263, 100, 18, 1044044, LabelColor, false, false ); // PREV PAGE
						}
					}

					AddButton( 220, 60 + (index * 20), 4005, 4007, GetButtonID( 3, i ), GumpButtonType.Reply, 0 );

					if ( buildItem.NameNumber > 0 )
						AddHtmlLocalized( 255, 63 + (index * 20), 220, 18, buildItem.NameNumber, LabelColor, false, false );
					else
						AddLabel( 255, 60 + (index * 20), LabelHue, buildItem.NameString );

					AddButton( 480, 60 + (index * 20), 4011, 4012, GetButtonID( 4, i ), GumpButtonType.Reply, 0 );
				}
			}
			else
			{
				AddHtmlLocalized( 230, 62, 200, 22, 1044165, LabelColor, false, false ); // You haven't made anything yet.
			}
		}

		public void CreateItemList( int selectedGroup )
		{
			if ( selectedGroup == 501 ) // 501 : Last 10
			{
				CreateMakeLastList();
				return;
			}

			BuildGroupCol buildGroupCol = m_BuildSystem.BuildGroups;
			BuildGroup buildGroup = buildGroupCol.GetAt( selectedGroup );
			BuildItemCol buildItemCol = buildGroup.BuildItems;

			for ( int i = 0; i < buildItemCol.Count; ++i )
			{
				int index = i % 10;

				BuildItem buildItem = buildItemCol.GetAt( i );

				if ( index == 0 )
				{
					if ( i > 0 )
					{
						AddButton( 370, 260, 4005, 4007, 0, GumpButtonType.Page, (i / 10) + 1 );
						AddHtmlLocalized( 405, 263, 100, 18, 1044045, LabelColor, false, false ); // NEXT PAGE
					}

					AddPage( (i / 10) + 1 );

					if ( i > 0 )
					{
						AddButton( 220, 260, 4014, 4015, 0, GumpButtonType.Page, i / 10 );
						AddHtmlLocalized( 255, 263, 100, 18, 1044044, LabelColor, false, false ); // PREV PAGE
					}
				}

				AddButton( 220, 60 + (index * 20), 4005, 4007, GetButtonID( 1, i ), GumpButtonType.Reply, 0 );

				if ( buildItem.NameNumber > 0 )
					AddHtmlLocalized( 255, 63 + (index * 20), 220, 18, buildItem.NameNumber, LabelColor, false, false );
				else
					AddLabel( 255, 60 + (index * 20), LabelHue, buildItem.NameString );

				AddButton( 480, 60 + (index * 20), 4011, 4012, GetButtonID( 2, i ), GumpButtonType.Reply, 0 );
			}
		}

		public int CreateGroupList()
		{
			BuildGroupCol buildGroupCol = m_BuildSystem.BuildGroups;

			AddButton( 15, 60, 4005, 4007, GetButtonID( 6, 3 ), GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 50, 63, 150, 18, 1044014, LabelColor, false, false ); // LAST TEN

			for ( int i = 0; i < buildGroupCol.Count; i++ )
			{
				BuildGroup buildGroup = buildGroupCol.GetAt( i );

				AddButton( 15, 80 + (i * 20), 4005, 4007, GetButtonID( 0, i ), GumpButtonType.Reply, 0 );

				if ( buildGroup.NameNumber > 0 )
					AddHtmlLocalized( 50, 83 + (i * 20), 150, 18, buildGroup.NameNumber, LabelColor, false, false );
				else
					AddLabel( 50, 80 + (i * 20), LabelHue, buildGroup.NameString );
			}

			return buildGroupCol.Count;
		}

		public static int GetButtonID( int type, int index )
		{
			return 1 + type + (index * 7);
		}

		public void BuildItem( BuildItem item )
		{
			int num = m_BuildSystem.CanBuild( m_From, m_Tool, item.ItemType );

			if ( num > 0 )
			{
				m_From.SendGump( new BuildGump( m_From, m_BuildSystem, m_Tool, num ) );
			}
			else
			{
				Type type = null;

				BuildContext context = m_BuildSystem.GetContext( m_From );

				if ( context != null )
				{
					BuildSubResCol res = ( item.UseSubRes2 ? m_BuildSystem.BuildSubRes2 : m_BuildSystem.BuildSubRes );
					int resIndex = ( item.UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex );

					if ( resIndex >= 0 && resIndex < res.Count )
						type = res.GetAt( resIndex ).ItemType;
				}

				m_BuildSystem.CreateItem( m_From, item.ItemType, type, m_Tool, item );
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( info.ButtonID <= 0 )
				return; // Canceled

			int buttonID = info.ButtonID - 1;
			int type = buttonID % 7;
			int index = buttonID / 7;

			BuildSystem system = m_BuildSystem;
			BuildGroupCol groups = system.BuildGroups;
			BuildContext context = system.GetContext( m_From );

			switch ( type )
			{
				case 0: // Show group
				{
					if ( context == null )
						break;

					if ( index >= 0 && index < groups.Count )
					{
						context.LastGroupIndex = index;
						m_From.SendGump( new BuildGump( m_From, system, m_Tool, null ) );
					}

					break;
				}
				case 1: // Create item
				{
					if ( context == null )
						break;

					int groupIndex = context.LastGroupIndex;

					if ( groupIndex >= 0 && groupIndex < groups.Count )
					{
						BuildGroup group = groups.GetAt( groupIndex );

						if ( index >= 0 && index < group.BuildItems.Count )
							BuildItem( group.BuildItems.GetAt( index ) );
					}

					break;
				}
				case 2: // Item details
				{
					if ( context == null )
						break;

					int groupIndex = context.LastGroupIndex;

					if ( groupIndex >= 0 && groupIndex < groups.Count )
					{
						BuildGroup group = groups.GetAt( groupIndex );

						if ( index >= 0 && index < group.BuildItems.Count )
							m_From.SendGump( new BuildGumpItem( m_From, system, group.BuildItems.GetAt( index ), m_Tool ) );
					}

					break;
				}
				case 3: // Create item (last 10)
				{
					if ( context == null )
						break;

					ArrayList lastTen = context.Items;

					if ( index >= 0 && index < lastTen.Count )
						BuildItem( (BuildItem)lastTen[index] );

					break;
				}
				case 4: // Item details (last 10)
				{
					if ( context == null )
						break;

					ArrayList lastTen = context.Items;

					if ( index >= 0 && index < lastTen.Count )
						m_From.SendGump( new BuildGumpItem( m_From, system, (BuildItem)lastTen[index], m_Tool ) );

					break;
				}
				case 5: // Resource selected
				{
					if ( m_Page == BuildPage.PickResource && index >= 0 && index < system.BuildSubRes.Count )
					{
						int groupIndex = ( context == null ? -1 : context.LastGroupIndex );

						BuildSubRes res = system.BuildSubRes.GetAt( index );

						if ( LokaiSkillUtilities.XMLGetSkills(m_From)[system.MainLokaiSkill].Base < res.RequiredLokaiSkill )
						{
							m_From.SendGump( new BuildGump( m_From, system, m_Tool, res.Message ) );
						}
						else
						{
							if ( context != null )
								context.LastResourceIndex = index;

							m_From.SendGump( new BuildGump( m_From, system, m_Tool, null ) );
						}
					}
					else if ( m_Page == BuildPage.PickResource2 && index >= 0 && index < system.BuildSubRes2.Count )
					{
						int groupIndex = ( context == null ? -1 : context.LastGroupIndex );

						BuildSubRes res = system.BuildSubRes2.GetAt( index );

						if ( LokaiSkillUtilities.XMLGetSkills(m_From)[system.MainLokaiSkill].Base < res.RequiredLokaiSkill )
						{
							m_From.SendGump( new BuildGump( m_From, system, m_Tool, res.Message ) );
						}
						else
						{
							if ( context != null )
								context.LastResourceIndex2 = index;

							m_From.SendGump( new BuildGump( m_From, system, m_Tool, null ) );
						}
					}

					break;
				}
				case 6: // Misc. buttons
				{
					switch ( index )
					{
						case 0: // Resource selection
						{
							if ( system.BuildSubRes.Init )
								m_From.SendGump( new BuildGump( m_From, system, m_Tool, null, BuildPage.PickResource ) );

							break;
						}
						case 1: // Smelt item
						{
							if ( system.Resmelt )
								Resmelt.Do( m_From, system, m_Tool );

							break;
						}
						case 2: // Make last
						{
							if ( context == null )
								break;

							BuildItem item = context.LastMade;

							if ( item != null )
								BuildItem( item );
							else
								m_From.SendGump( new BuildGump( m_From, m_BuildSystem, m_Tool, 1044165, m_Page ) ); // You haven't made anything yet.

							break;
						}
						case 3: // Last 10
						{
							if ( context == null )
								break;

							context.LastGroupIndex = 501;
							m_From.SendGump( new BuildGump( m_From, system, m_Tool, null ) );

							break;
						}
						case 4: // Toggle use resource hue
						{
							if ( context == null )
								break;

							context.DoNotColor = !context.DoNotColor;

							m_From.SendGump( new BuildGump( m_From, m_BuildSystem, m_Tool, null, m_Page ) );

							break;
						}
						case 5: // Repair item
						{
							if ( system.Repair )
								Repair.Do( m_From, system, m_Tool );

							break;
						}
						case 6: // Toggle mark option
						{
							if ( context == null || !system.MarkOption )
								break;

							switch ( context.MarkOption )
							{
								case BuildMarkOption.MarkItem: context.MarkOption = BuildMarkOption.DoNotMark; break;
								case BuildMarkOption.DoNotMark: context.MarkOption = BuildMarkOption.PromptForMark; break;
								case BuildMarkOption.PromptForMark: context.MarkOption = BuildMarkOption.MarkItem; break;
							}

							m_From.SendGump( new BuildGump( m_From, m_BuildSystem, m_Tool, null, m_Page ) );

							break;
						}
						case 7: // Resource selection 2
						{
							if ( system.BuildSubRes2.Init )
								m_From.SendGump( new BuildGump( m_From, system, m_Tool, null, BuildPage.PickResource2 ) );

							break;
						}
						case 8: // Enhance item
						{
							if ( system.CanEnhance )
								Enhance.BeginTarget( m_From, system, m_Tool );

							break;
						}
					}

					break;
				}
			}
		}
	}
}