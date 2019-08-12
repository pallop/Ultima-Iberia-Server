/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;

namespace Server.Engines.Build
{
	public class BuildLokaiSkillCol : System.Collections.CollectionBase
	{
		public BuildLokaiSkillCol()
		{
		}

		public void Add( BuildLokaiSkill buildLokaiSkill )
		{
			List.Add( buildLokaiSkill );
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

		public BuildLokaiSkill GetAt( int index )
		{
			return ( BuildLokaiSkill ) List[index];
		}
	}
}