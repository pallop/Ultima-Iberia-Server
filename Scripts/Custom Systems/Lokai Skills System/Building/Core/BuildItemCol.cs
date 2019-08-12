/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;

namespace Server.Engines.Build
{
	public class BuildItemCol : System.Collections.CollectionBase
	{
		public BuildItemCol()
		{
		}

		public int Add( BuildItem buildItem )
		{
			return List.Add( buildItem );
		}

		public void Remove( int index )
		{
			if ( index > Count - 1 || index < 0 )
			{
			}
			else
			{
				List.RemoveAt( index );
			}
		}

		public BuildItem GetAt( int index )
		{
			return ( BuildItem ) List[index];
		}

		public BuildItem SearchForSubclass( Type type )
		{
			for ( int i = 0; i < List.Count; i++ )
			{
				BuildItem buildItem = ( BuildItem )List[i];

				if ( buildItem.ItemType == type || type.IsSubclassOf( buildItem.ItemType ) )
					return buildItem;
			}

			return null;
		}

		public BuildItem SearchFor( Type type )
		{
			for ( int i = 0; i < List.Count; i++ )
			{
				BuildItem buildItem = ( BuildItem )List[i];
				if ( buildItem.ItemType == type )
				{
					return buildItem;
				}
			}
			return null;
		}
	}
}