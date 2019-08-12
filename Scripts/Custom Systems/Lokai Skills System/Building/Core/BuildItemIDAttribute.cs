/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server;

namespace Server.Engines.Build
{
	[AttributeUsage( AttributeTargets.Class )]
	public class BuildItemIDAttribute : Attribute
	{
		private int m_ItemID;

		public int ItemID{ get{ return m_ItemID; } }

		public BuildItemIDAttribute( int itemID )
		{
			m_ItemID = itemID;
		}
	}
}