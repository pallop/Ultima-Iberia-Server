/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server;
using Server.Items;

namespace Server.Engines.Build
{
	public abstract class CustomBuild
	{
		private Mobile m_From;
		private BuildItem m_BuildItem;
		private BuildSystem m_BuildSystem;
		private Type m_TypeRes;
		private BaseBuildingTool m_Tool;
		private int m_Quality;

		public Mobile From{ get{ return m_From; } }
		public BuildItem BuildItem{ get{ return m_BuildItem; } }
		public BuildSystem BuildSystem{ get{ return m_BuildSystem; } }
		public Type TypeRes{ get{ return m_TypeRes; } }
		public BaseBuildingTool Tool{ get{ return m_Tool; } }
		public int Quality{ get{ return m_Quality; } }

		public CustomBuild( Mobile from, BuildItem buildItem, BuildSystem buildSystem, Type typeRes, BaseBuildingTool tool, int quality )
		{
			m_From = from;
			m_BuildItem = buildItem;
			m_BuildSystem = buildSystem;
			m_TypeRes = typeRes;
			m_Tool = tool;
			m_Quality = quality;
		}

		public abstract void EndBuildAction();
		public abstract Item CompleteBuild( out int message );
	}
}