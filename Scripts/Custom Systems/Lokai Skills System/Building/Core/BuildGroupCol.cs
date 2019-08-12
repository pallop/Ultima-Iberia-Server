/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;

namespace Server.Engines.Build
{
	public class BuildGroupCol : System.Collections.CollectionBase
	{
		public BuildGroupCol()
		{
		}

		public int Add( BuildGroup buildGroup )
		{
			return List.Add( buildGroup );
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

		public BuildGroup GetAt( int index )
		{
			return ( BuildGroup ) List[index];
		}

		public int SearchFor( TextDefinition groupName )
		{
			for ( int i = 0; i < List.Count; i++ )
			{
				BuildGroup buildGroup = (BuildGroup)List[i];

				int nameNumber = buildGroup.NameNumber;
				string nameString = buildGroup.NameString;

				if ( ( nameNumber != 0 && nameNumber == groupName.Number ) || ( nameString != null && nameString == groupName.String ) )
					return i;
			}

			return -1;
		}
	}
}