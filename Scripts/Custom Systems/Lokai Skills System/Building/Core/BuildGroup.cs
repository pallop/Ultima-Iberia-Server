/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;

namespace Server.Engines.Build
{
	public class BuildGroup
	{
		private BuildItemCol m_arBuildItem;

		private string m_NameString;
		private int m_NameNumber;

		public BuildGroup( TextDefinition groupName )
		{
			m_NameNumber = groupName;
			m_NameString = groupName;
			m_arBuildItem = new BuildItemCol();
		}

		public void AddBuildItem( BuildItem buildItem )
		{
			m_arBuildItem.Add( buildItem );
		}

		public BuildItemCol BuildItems
		{
			get { return m_arBuildItem; }
		}

		public string NameString
		{
			get { return m_NameString; }
		}

		public int NameNumber
		{
			get { return m_NameNumber; }
		}
	}
}