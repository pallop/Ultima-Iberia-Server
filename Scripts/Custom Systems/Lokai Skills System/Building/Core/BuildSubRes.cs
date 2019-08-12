/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;

namespace Server.Engines.Build
{
	public class BuildSubRes
	{
		private Type m_Type;
		private double m_ReqLokaiSkill;
		private string m_NameString;
		private int m_NameNumber;
		private int m_GenericNameNumber;
		private object m_Message;

		public BuildSubRes( Type type, TextDefinition name, double reqLokaiSkill, object message ) : this( type, name, reqLokaiSkill, 0, message )
		{
		}

		public BuildSubRes( Type type, TextDefinition name, double reqLokaiSkill, int genericNameNumber, object message )
		{
			m_Type = type;
			m_NameNumber = name;
			m_NameString = name;
			m_ReqLokaiSkill = reqLokaiSkill;
			m_GenericNameNumber = genericNameNumber;
			m_Message = message;
		}

		public Type ItemType
		{
			get { return m_Type; }
		}

		public string NameString
		{
			get { return m_NameString; }
		}

		public int NameNumber
		{
			get { return m_NameNumber; }
		}

		public int GenericNameNumber
		{
			get { return m_GenericNameNumber; }
		}

		public object Message
		{
			get { return m_Message; }
		}

		public double RequiredLokaiSkill
		{
			get { return m_ReqLokaiSkill; }
		}
	}
}