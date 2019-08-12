/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;

namespace Server.Engines.Build
{
	public class BuildResCol : System.Collections.CollectionBase
	{
		public BuildResCol()
		{
		}

		public void Add( BuildRes buildRes )
		{
			List.Add( buildRes );
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

		public BuildRes GetAt( int index )
		{
			return ( BuildRes ) List[index];
		}
	}
}