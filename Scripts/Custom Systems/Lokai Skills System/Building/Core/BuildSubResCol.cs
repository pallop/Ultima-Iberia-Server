/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;

namespace Server.Engines.Build
{
	public class BuildSubResCol : System.Collections.CollectionBase
	{
		private Type m_Type;
		private string m_NameString;
		private int m_NameNumber;
		private bool m_Init;

		public bool Init
		{
			get { return m_Init; }
			set { m_Init = value; }
		}
				
		public Type ResType
		{
			get { return m_Type; }
			set { m_Type = value; }
		}

		public string NameString
		{
			get { return m_NameString; }
			set { m_NameString = value; }
		}

		public int NameNumber
		{
			get { return m_NameNumber; }
			set { m_NameNumber = value; }
		}

		public BuildSubResCol()
		{
			m_Init = false;
		}

		public void Add( BuildSubRes buildSubRes )
		{
			List.Add( buildSubRes );
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

		public BuildSubRes GetAt( int index )
		{
			return ( BuildSubRes ) List[index];
		}

		public BuildSubRes SearchFor( Type type )
		{
			for ( int i = 0; i < List.Count; i++ )
			{
				BuildSubRes buildSubRes = ( BuildSubRes )List[i];
				if ( buildSubRes.ItemType == type )
				{
					return buildSubRes;
				}
			}
			return null;
		}
	}
}