/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;

namespace Server.Engines.Build
{
	public class BuildLokaiSkill
	{
		private LokaiSkillName m_LokaiSkillToMake;
		private double m_MinLokaiSkill;
		private double m_MaxLokaiSkill;

		public BuildLokaiSkill( LokaiSkillName lokaiSkillToMake, double minLokaiSkill, double maxLokaiSkill )
		{
			m_LokaiSkillToMake = lokaiSkillToMake;
			m_MinLokaiSkill = minLokaiSkill;
			m_MaxLokaiSkill = maxLokaiSkill;
		}

		public LokaiSkillName LokaiSkillToMake
		{
			get { return m_LokaiSkillToMake; }
		}

		public double MinLokaiSkill
		{
			get { return m_MinLokaiSkill; }
		}

		public double MaxLokaiSkill
		{
			get { return m_MaxLokaiSkill; }
		}
	}
}