/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server;
using Server.Gumps;
using Server.Items;

namespace Server.Engines.Build
{
	public class QueryMakersMarkGump : Gump
	{
		private int m_Quality;
		private Mobile m_From;
		private BuildItem m_BuildItem;
		private BuildSystem m_BuildSystem;
		private Type m_TypeRes;
		private BaseBuildingTool m_Tool;

		public QueryMakersMarkGump( int quality, Mobile from, BuildItem buildItem, BuildSystem buildSystem, Type typeRes, BaseBuildingTool tool ) : base( 100, 200 )
		{
			from.CloseGump( typeof( QueryMakersMarkGump ) );

			m_Quality = quality;
			m_From = from;
			m_BuildItem = buildItem;
			m_BuildSystem = buildSystem;
			m_TypeRes = typeRes;
			m_Tool = tool;

			AddPage( 0 );

			AddBackground( 0, 0, 220, 170, 5054 );
			AddBackground( 10, 10, 200, 150, 3000 );

			AddHtmlLocalized( 20, 20, 180, 80, 1018317, false, false ); // Do you wish to place your maker's mark on this item?

			AddHtmlLocalized( 55, 100, 140, 25, 1011011, false, false ); // CONTINUE
			AddButton( 20, 100, 4005, 4007, 1, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 55, 125, 140, 25, 1011012, false, false ); // CANCEL
			AddButton( 20, 125, 4005, 4007, 0, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			bool makersMark = ( info.ButtonID == 1 );

			if ( makersMark )
				m_From.SendLocalizedMessage( 501808 ); // You mark the item.
			else
				m_From.SendLocalizedMessage( 501809 ); // Cancelled mark.

			m_BuildItem.CompleteBuild( m_Quality, makersMark, m_From, m_BuildSystem, m_TypeRes, m_Tool, null );
		}
	}
}