using System;

namespace Server
{
	[AttributeUsage( AttributeTargets.Class )]
	public class SleepingNameAttribute : Attribute
	{
		private string m_Name;

		public string Name
		{
			get{ return m_Name; }
		}

		public SleepingNameAttribute( string name )
		{
			m_Name = name;
		}
	}
}