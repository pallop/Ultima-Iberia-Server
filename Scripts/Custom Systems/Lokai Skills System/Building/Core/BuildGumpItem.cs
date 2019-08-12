/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Build
{
	public class BuildGumpItem : Gump
	{
		private Mobile m_From;
		private BuildSystem m_BuildSystem;
		private BuildItem m_BuildItem;
		private BaseBuildingTool m_Tool;

		private const int LabelHue = 0x480; // 0x384
		private const int RedLabelHue = 0x20;

		private const int LabelColor = 0x7FFF;
		private const int RedLabelColor = 0x6400;

		private const int GreyLabelColor = 0x3DEF;

		private int m_OtherCount;

		public BuildGumpItem( Mobile from, BuildSystem buildSystem, BuildItem buildItem, BaseBuildingTool tool ) : base( 40, 40 )
		{
			m_From = from;
			m_BuildSystem = buildSystem;
			m_BuildItem = buildItem;
			m_Tool = tool;

			from.CloseGump( typeof( BuildGump ) );
			from.CloseGump( typeof( BuildGumpItem ) );

			AddPage( 0 );
			AddBackground( 0, 0, 530, 417, 5054 );
			AddImageTiled( 10, 10, 510, 22, 2624 );
			AddImageTiled( 10, 37, 150, 148, 2624 );
			AddImageTiled( 165, 37, 355, 90, 2624 );
			AddImageTiled( 10, 190, 155, 22, 2624 );
			AddImageTiled( 10, 217, 150, 53, 2624 );
			AddImageTiled( 165, 132, 355, 80, 2624 );
			AddImageTiled( 10, 275, 155, 22, 2624 );
			AddImageTiled( 10, 302, 150, 53, 2624 );
			AddImageTiled( 165, 217, 355, 80, 2624 );
			AddImageTiled( 10, 360, 155, 22, 2624 );
			AddImageTiled( 165, 302, 355, 80, 2624 );
			AddImageTiled( 10, 387, 510, 22, 2624 );
			AddAlphaRegion( 10, 10, 510, 399 );

            AddHtmlLocalized(170, 40, 150, 20, 1044053, LabelColor, false, false); // ITEM 
            AddHtmlLocalized(10, 192, 150, 22, 1070722, "<CENTER>ABILITIIES</CENTER>", LabelColor, false, false); // ABILITIIES
            //AddHtmlLocalized(10, 192, 150, 22, 1060857, LabelColor, false, false); // Primary LokaiSkill
			AddHtmlLocalized( 10, 277, 150, 22, 1044055, LabelColor, false, false ); // <CENTER>MATERIALS</CENTER>
			AddHtmlLocalized( 10, 362, 150, 22, 1044056, LabelColor, false, false ); // <CENTER>OTHER</CENTER>

			if ( buildSystem.GumpTitleNumber > 0 )
				AddHtmlLocalized( 10, 12, 510, 20, buildSystem.GumpTitleNumber, LabelColor, false, false );
			else
				AddHtml( 10, 12, 510, 20, buildSystem.GumpTitleString, false, false );

			AddButton( 15, 387, 4014, 4016, 0, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 50, 390, 150, 18, 1044150, LabelColor, false, false ); // BACK

			bool needsRecipe = false;

			if( needsRecipe )
			{
				AddButton( 270, 387, 4005, 4007, 0, GumpButtonType.Page, 0 );
				AddHtmlLocalized( 305, 390, 150, 18, 1044151, GreyLabelColor, false, false ); // MAKE NOW
			}
			else
			{
				AddButton( 270, 387, 4005, 4007, 1, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 305, 390, 150, 18, 1044151, LabelColor, false, false ); // MAKE NOW
			}

			if ( buildItem.NameNumber > 0 )
				AddHtmlLocalized( 330, 40, 180, 18, buildItem.NameNumber, LabelColor, false, false );
			else
				AddLabel( 330, 40, LabelHue, buildItem.NameString );

			if ( buildItem.UseAllRes )
				AddHtmlLocalized( 170, 302 + (m_OtherCount++ * 20), 310, 18, 1048176, LabelColor, false, false ); // Makes as many as possible at once

			DrawItem();
			DrawLokaiSkill();
			DrawRessource();

			/*
			if( buildItem.RequiresSE )
				AddHtmlLocalized( 170, 302 + (m_OtherCount++ * 20), 310, 18, 1063363, LabelColor, false, false ); //* Requires the "Samurai Empire" expansion
			 * */

			if( buildItem.RequiredExpansion != Expansion.None )
			{
				bool supportsEx = (from.NetState != null && from.NetState.SupportsExpansion( buildItem.RequiredExpansion ));
				TextDefinition.AddHtmlText( this, 170, 302 + (m_OtherCount++ * 20), 310, 18, RequiredExpansionMessage( buildItem.RequiredExpansion ), false, false, supportsEx ? LabelColor : RedLabelColor, supportsEx ? LabelHue : RedLabelHue );
			}

			if( needsRecipe )
				AddHtmlLocalized( 170, 302 + (m_OtherCount++ * 20), 310, 18, 1073620, RedLabelColor, false, false ); // You have not learned this recipe.

		}

		private TextDefinition RequiredExpansionMessage( Expansion expansion )
		{
			switch( expansion )
			{
				case Expansion.SE:
					return 1063363; // * Requires the "Samurai Empire" expansion
				case Expansion.ML:
					return 1072651; // * Requires the "Mondain's Legacy" expansion
				default:
					return String.Format( "* Requires the \"{0}\" expansion", ExpansionInfo.GetInfo( expansion ).Name );
			}
		}

		private bool m_ShowExceptionalChance;

		public void DrawItem()
		{
			Type type = m_BuildItem.ItemType;

			AddItem( 20, 50, BuildItem.ItemIDOf( type ) );

			if ( m_BuildItem.IsMarkable( type ) )
			{
				AddHtmlLocalized( 170, 302 + (m_OtherCount++ * 20), 310, 18, 1044059, LabelColor, false, false ); // This item may hold its maker's mark
				m_ShowExceptionalChance = true;
			}
		}

		public void DrawLokaiSkill()
		{
			for ( int i = 0; i < m_BuildItem.LokaiSkills.Count; i++ )
			{
				BuildLokaiSkill lokaiSkill = m_BuildItem.LokaiSkills.GetAt( i );
				double minLokaiSkill = lokaiSkill.MinLokaiSkill, maxLokaiSkill = lokaiSkill.MaxLokaiSkill;

				if ( minLokaiSkill < 0 )
					minLokaiSkill = 0;

                AddHtmlLocalized(170, 132 + (i * 20), 200, 18, 1070722, lokaiSkill.LokaiSkillToMake.ToString(), LabelColor, false, false);
				AddLabel( 430, 132 + (i * 20), LabelHue, String.Format( "{0:F1}", minLokaiSkill ) );
			}

			BuildSubResCol res = ( m_BuildItem.UseSubRes2 ? m_BuildSystem.BuildSubRes2 : m_BuildSystem.BuildSubRes );
			int resIndex = -1;

			BuildContext context = m_BuildSystem.GetContext( m_From );

			if ( context != null )
				resIndex = ( m_BuildItem.UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex );

			bool allRequiredLokaiSkills = true;
			double chance = m_BuildItem.GetSuccessChance( m_From, resIndex > -1 ? res.GetAt( resIndex ).ItemType : null, m_BuildSystem, false, ref allRequiredLokaiSkills );
			double excepChance = m_BuildItem.GetExceptionalChance( m_BuildSystem, chance, m_From );

			if ( chance < 0.0 )
				chance = 0.0;
			else if ( chance > 1.0 )
				chance = 1.0;

			AddHtmlLocalized( 170, 80, 250, 18, 1044057, LabelColor, false, false ); // Success Chance:
			AddLabel( 430, 80, LabelHue, String.Format( "{0:F1}%", chance * 100 ) );

			if ( m_ShowExceptionalChance )
			{
				if( excepChance < 0.0 )
					excepChance = 0.0;
				else if( excepChance > 1.0 )
					excepChance = 1.0;

				AddHtmlLocalized( 170, 100, 250, 18, 1044058, 32767, false, false ); // Exceptional Chance:
				AddLabel( 430, 100, LabelHue, String.Format( "{0:F1}%", excepChance * 100 ) );
			}
		}

		private static Type typeofBlankScroll = typeof( BlankScroll );
		private static Type typeofSpellScroll = typeof( SpellScroll );

		public void DrawRessource()
		{
			bool retainedColor = false;

			BuildContext context = m_BuildSystem.GetContext( m_From );

			BuildSubResCol res = ( m_BuildItem.UseSubRes2 ? m_BuildSystem.BuildSubRes2 : m_BuildSystem.BuildSubRes );
			int resIndex = -1;

			if ( context != null )
				resIndex = ( m_BuildItem.UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex );

			bool cropScroll = ( m_BuildItem.Ressources.Count > 1 )
				&& m_BuildItem.Ressources.GetAt( m_BuildItem.Ressources.Count - 1 ).ItemType == typeofBlankScroll
				&& typeofSpellScroll.IsAssignableFrom( m_BuildItem.ItemType );

			for ( int i = 0; i < m_BuildItem.Ressources.Count - (cropScroll ? 1 : 0) && i < 4; i++ )
			{
				Type type;
				string nameString;
				int nameNumber;

				BuildRes buildResource = m_BuildItem.Ressources.GetAt( i );

				type = buildResource.ItemType;
				nameString = buildResource.NameString;
				nameNumber = buildResource.NameNumber;
				
				// Resource Mutation
				if ( type == res.ResType && resIndex > -1 )
				{
					BuildSubRes subResource = res.GetAt( resIndex );

					type = subResource.ItemType;

					nameString = subResource.NameString;
					nameNumber = subResource.GenericNameNumber;

					if ( nameNumber <= 0 )
						nameNumber = subResource.NameNumber;
				}
				// ******************

				if ( !retainedColor && m_BuildItem.RetainsColorFrom( m_BuildSystem, type ) )
				{
					retainedColor = true;
					AddHtmlLocalized( 170, 302 + (m_OtherCount++ * 20), 310, 18, 1044152, LabelColor, false, false ); // * The item retains the color of this material
					AddLabel( 500, 219 + (i * 20), LabelHue, "*" );
				}

				if ( nameNumber > 0 )
					AddHtmlLocalized( 170, 219 + (i * 20), 310, 18, nameNumber, LabelColor, false, false );
				else
					AddLabel( 170, 219 + (i * 20), LabelHue, nameString );

				AddLabel( 430, 219 + (i * 20), LabelHue, buildResource.Amount.ToString() );
			}

			if ( m_BuildItem.NameNumber == 1041267 ) // runebook
			{
				AddHtmlLocalized( 170, 219 + (m_BuildItem.Ressources.Count * 20), 310, 18, 1044447, LabelColor, false, false );
				AddLabel( 430, 219 + (m_BuildItem.Ressources.Count * 20), LabelHue, "1" );
			}

			if ( cropScroll )
				AddHtmlLocalized( 170, 302 + (m_OtherCount++ * 20), 360, 18, 1044379, LabelColor, false, false ); // Inscribing scrolls also requires a blank scroll and mana.
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			// Back Button
			if ( info.ButtonID == 0 )
			{
				BuildGump buildGump = new BuildGump( m_From, m_BuildSystem, m_Tool, null );
				m_From.SendGump( buildGump );
			}
			else // Make Button
			{
				int num = m_BuildSystem.CanBuild( m_From, m_Tool, m_BuildItem.ItemType );

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
						BuildSubResCol res = ( m_BuildItem.UseSubRes2 ? m_BuildSystem.BuildSubRes2 : m_BuildSystem.BuildSubRes );
						int resIndex = ( m_BuildItem.UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex );

						if ( resIndex > -1 )
							type = res.GetAt( resIndex ).ItemType;
					}

					m_BuildSystem.CreateItem( m_From, m_BuildItem.ItemType, type, m_Tool, m_BuildItem );
				}
			}
		}
	}
}