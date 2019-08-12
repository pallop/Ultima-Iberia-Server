/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Commands;

namespace Server.Engines.Build
{
	public class Recipe
	{
		private static Dictionary<int, Recipe> m_Recipes = new Dictionary<int, Recipe>();

		public static Dictionary<int, Recipe> Recipes { get { return m_Recipes; } }

		private static int m_LargestRecipeID;
		public  static int LargestRecipeID{ get{ return m_LargestRecipeID; } }

		private BuildSystem m_System;

		public BuildSystem BuildSystem
		{
			get { return m_System; }
			set { m_System = value; }
		}

		private BuildItem m_BuildItem;

		public BuildItem BuildItem
		{
			get { return m_BuildItem; }
			set { m_BuildItem = value; }
		}

		private int m_ID;

		public int ID
		{
			get { return m_ID; }
		}

		private TextDefinition m_TD;
		public TextDefinition TextDefinition
		{
			get
			{
				if( m_TD == null )
					m_TD = new TextDefinition( m_BuildItem.NameNumber, m_BuildItem.NameString );

				return m_TD;
			}
		}

		public Recipe( int id, BuildSystem system, BuildItem item )
		{
			m_ID = id;
			m_System = system;
			m_BuildItem = item;

			if( m_Recipes.ContainsKey( id ) )
				throw new Exception( "Attempting to create recipe with preexisting ID." );

			m_Recipes.Add( id, this );
			m_LargestRecipeID = Math.Max( id, m_LargestRecipeID );
		}
	}
}
