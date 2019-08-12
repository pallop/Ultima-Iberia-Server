/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Engines.Build
{
	public enum BuildECA
	{
		ChanceMinusSixty,
		FiftyPercentChanceMinusTenPercent,
		ChanceMinusSixtyToFourtyFive
	}

	public abstract class BuildSystem
	{
		private int m_MinBuildEffect;
		private int m_MaxBuildEffect;
		private double m_Delay;
		private bool m_Resmelt;
		private bool m_Repair;
		private bool m_MarkOption;
		private bool m_CanEnhance;

		private BuildItemCol m_BuildItems;
		private BuildGroupCol m_BuildGroups;
		private BuildSubResCol m_BuildSubRes;
		private BuildSubResCol m_BuildSubRes2;

		public int MinBuildEffect { get { return m_MinBuildEffect; } }
		public int MaxBuildEffect { get { return m_MaxBuildEffect; } }
		public double Delay { get { return m_Delay; } }

		public BuildItemCol BuildItems{ get { return m_BuildItems; } }
		public BuildGroupCol BuildGroups{ get { return m_BuildGroups; } }
		public BuildSubResCol BuildSubRes{ get { return m_BuildSubRes; } }
		public BuildSubResCol BuildSubRes2{ get { return m_BuildSubRes2; } }
		
		public abstract LokaiSkillName MainLokaiSkill{ get; }

		public virtual int GumpTitleNumber{ get{ return 0; } }
		public virtual string GumpTitleString{ get{ return ""; } }

		public virtual BuildECA ECA{ get{ return BuildECA.ChanceMinusSixty; } }

		private Dictionary<Mobile, BuildContext> m_ContextTable = new Dictionary<Mobile, BuildContext>();

		public abstract double GetChanceAtMin( BuildItem item );

		public virtual bool RetainsColorFrom( BuildItem item, Type type )
		{
			return false;
		}

		public BuildContext GetContext( Mobile m )
		{
			if ( m == null )
				return null;

			if ( m.Deleted )
			{
				m_ContextTable.Remove( m );
				return null;
			}

			BuildContext c = null;
			m_ContextTable.TryGetValue( m, out c );

			if ( c == null )
				m_ContextTable[m] = c = new BuildContext();

			return c;
		}

		public void OnMade( Mobile m, BuildItem item )
		{
			BuildContext c = GetContext( m );

			if ( c != null )
				c.OnMade( item );
		}

		public bool Resmelt
		{
			get { return m_Resmelt; }
			set { m_Resmelt = value; }
		}

		public bool Repair
		{
			get{ return m_Repair; }
			set{ m_Repair = value; }
		}

		public bool MarkOption
		{
			get{ return m_MarkOption; }
			set{ m_MarkOption = value; }
		}

		public bool CanEnhance
		{
			get{ return m_CanEnhance; }
			set{ m_CanEnhance = value; }
		}

		public BuildSystem( int minBuildEffect, int maxBuildEffect, double delay )
		{
			m_MinBuildEffect = minBuildEffect;
			m_MaxBuildEffect = maxBuildEffect;
			m_Delay = delay;

			m_BuildItems = new BuildItemCol();
			m_BuildGroups = new BuildGroupCol();
			m_BuildSubRes = new BuildSubResCol();
			m_BuildSubRes2 = new BuildSubResCol();

			InitBuildList();
		}

		public virtual bool ConsumeOnFailure( Mobile from, Type resourceType, BuildItem buildItem )
		{
			return true;
		}

		public void CreateItem( Mobile from, Type type, Type typeRes, BaseBuildingTool tool, BuildItem realBuildItem )
		{	
			// Verify if the type is in the list of the buildable item
			BuildItem buildItem = m_BuildItems.SearchFor( type );
			if ( buildItem != null )
			{
				// The item is in the list, try to create it
				realBuildItem.Build( from, this, typeRes, tool );
				//buildItem.Build( from, this, typeRes, tool );
			}
		}


        public int AddBuild(Type typeItem, TextDefinition group, TextDefinition name, double minLokaiSkill, double maxLokaiSkill, Type typeRes, TextDefinition nameRes, int amount)
        {
            return AddBuild(typeItem, group, name, MainLokaiSkill, minLokaiSkill, maxLokaiSkill, typeRes, nameRes, amount, "", null);
        }

        public int AddBuild(Type typeItem, TextDefinition group, TextDefinition name, double minLokaiSkill, double maxLokaiSkill, Type typeRes, TextDefinition nameRes, int amount, TextDefinition message)
        {
            return AddBuild(typeItem, group, name, MainLokaiSkill, minLokaiSkill, maxLokaiSkill, typeRes, nameRes, amount, message, null);
        }

        public int AddBuild(Type typeItem, TextDefinition group, TextDefinition name, double minLokaiSkill, double maxLokaiSkill, Type typeRes, TextDefinition nameRes, int amount, TextDefinition message, object arg)
        {
            return AddBuild(typeItem, group, name, MainLokaiSkill, minLokaiSkill, maxLokaiSkill, typeRes, nameRes, amount, message, arg);
        }

        public int AddBuild(Type typeItem, TextDefinition group, TextDefinition name, LokaiSkillName lokaiSkillToMake, double minLokaiSkill, double maxLokaiSkill, Type typeRes, TextDefinition nameRes, int amount)
        {
            return AddBuild(typeItem, group, name, lokaiSkillToMake, minLokaiSkill, maxLokaiSkill, typeRes, nameRes, amount, "", null);
        }

        public int AddBuild(Type typeItem, TextDefinition group, TextDefinition name, LokaiSkillName lokaiSkillToMake, double minLokaiSkill, double maxLokaiSkill, Type typeRes, TextDefinition nameRes, int amount, TextDefinition message, object arg)
        {
            BuildItem buildItem = new BuildItem(typeItem, group, name, arg);
            buildItem.AddRes(typeRes, nameRes, amount, message);
            buildItem.AddLokaiSkill(lokaiSkillToMake, minLokaiSkill, maxLokaiSkill);

            DoGroup(group, buildItem);
            return m_BuildItems.Add(buildItem);
        }


        private void DoGroup(TextDefinition groupName, BuildItem buildItem)
        {
            int index = m_BuildGroups.SearchFor(groupName);

            if (index == -1)
            {
                BuildGroup buildGroup = new BuildGroup(groupName);
                buildGroup.AddBuildItem(buildItem);
                m_BuildGroups.Add(buildGroup);
            }
            else
            {
                m_BuildGroups.GetAt(index).AddBuildItem(buildItem);
            }
        }


		public void SetManaReq( int index, int mana )
		{
			BuildItem buildItem = m_BuildItems.GetAt( index );
			buildItem.Mana = mana;
		}

		public void SetStamReq( int index, int stam )
		{
			BuildItem buildItem = m_BuildItems.GetAt( index );
			buildItem.Stam = stam;
		}

		public void SetHitsReq( int index, int hits )
		{
			BuildItem buildItem = m_BuildItems.GetAt( index );
			buildItem.Hits = hits;
		}
		
		public void SetUseAllRes( int index, bool useAll )
		{
			BuildItem buildItem = m_BuildItems.GetAt( index );
			buildItem.UseAllRes = useAll;
		}

		public void SetNeedHeat( int index, bool needHeat )
		{
			BuildItem buildItem = m_BuildItems.GetAt( index );
			buildItem.NeedHeat = needHeat;
		}

		public void SetNeedOven( int index, bool needOven )
		{
			BuildItem buildItem = m_BuildItems.GetAt( index );
			buildItem.NeedOven = needOven;
		}

		public void SetNeedMill( int index, bool needMill )
		{
			BuildItem buildItem = m_BuildItems.GetAt( index );
			buildItem.NeedMill = needMill;
		}

		public void SetNeededExpansion( int index, Expansion expansion )
		{
			BuildItem buildItem = m_BuildItems.GetAt( index );
			buildItem.RequiredExpansion = expansion;
		}

		public void AddRes( int index, Type type, TextDefinition name, int amount )
		{
			AddRes( index, type, name, amount, "" );
		}

		public void AddRes( int index, Type type, TextDefinition name, int amount, TextDefinition message )
		{
			BuildItem buildItem = m_BuildItems.GetAt( index );
			buildItem.AddRes( type, name, amount, message );
		}

		public void AddLokaiSkill( int index, LokaiSkillName lokaiSkillToMake, double minLokaiSkill, double maxLokaiSkill )
		{
			BuildItem buildItem = m_BuildItems.GetAt(index);
			buildItem.AddLokaiSkill(lokaiSkillToMake, minLokaiSkill, maxLokaiSkill);
		}

		public void SetUseSubRes2( int index, bool val )
		{
			BuildItem buildItem = m_BuildItems.GetAt(index);
			buildItem.UseSubRes2 = val;
		}

		public void AddRecipe( int index, int id )
		{
			BuildItem buildItem = m_BuildItems.GetAt( index );
			buildItem.AddRecipe( id, this );
		}

		public void ForceNonExceptional( int index )
		{
			BuildItem buildItem = m_BuildItems.GetAt( index );
			buildItem.ForceNonExceptional = true;
		}


		public void SetSubRes( Type type, string name )
		{
			m_BuildSubRes.ResType = type;
			m_BuildSubRes.NameString = name;
			m_BuildSubRes.Init = true;
		}

		public void SetSubRes( Type type, int name )
		{
			m_BuildSubRes.ResType = type;
			m_BuildSubRes.NameNumber = name;
			m_BuildSubRes.Init = true;
		}

		public void AddSubRes( Type type, int name, double reqLokaiSkill, object message )
		{
			BuildSubRes buildSubRes = new BuildSubRes( type, name, reqLokaiSkill, message );
			m_BuildSubRes.Add( buildSubRes );
		}

		public void AddSubRes( Type type, int name, double reqLokaiSkill, int genericName, object message )
		{
			BuildSubRes buildSubRes = new BuildSubRes( type, name, reqLokaiSkill, genericName, message );
			m_BuildSubRes.Add( buildSubRes );
		}

		public void AddSubRes( Type type, string name, double reqLokaiSkill, object message )
		{
			BuildSubRes buildSubRes = new BuildSubRes( type, name, reqLokaiSkill, message );
			m_BuildSubRes.Add( buildSubRes );
		}


		public void SetSubRes2( Type type, string name )
		{
			m_BuildSubRes2.ResType = type;
			m_BuildSubRes2.NameString = name;
			m_BuildSubRes2.Init = true;
		}

		public void SetSubRes2( Type type, int name )
		{
			m_BuildSubRes2.ResType = type;
			m_BuildSubRes2.NameNumber = name;
			m_BuildSubRes2.Init = true;
		}

		public void AddSubRes2( Type type, int name, double reqLokaiSkill, object message )
		{
			BuildSubRes buildSubRes = new BuildSubRes( type, name, reqLokaiSkill, message );
			m_BuildSubRes2.Add( buildSubRes );
		}

		public void AddSubRes2( Type type, int name, double reqLokaiSkill, int genericName, object message )
		{
			BuildSubRes buildSubRes = new BuildSubRes( type, name, reqLokaiSkill, genericName, message );
			m_BuildSubRes2.Add( buildSubRes );
		}

		public void AddSubRes2( Type type, string name, double reqLokaiSkill, object message )
		{
			BuildSubRes buildSubRes = new BuildSubRes( type, name, reqLokaiSkill, message );
			m_BuildSubRes2.Add( buildSubRes );
		}

		public abstract void InitBuildList();

		public abstract void PlayBuildEffect( Mobile from );
		public abstract int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, BuildItem item );

		public abstract int CanBuild( Mobile from, BaseBuildingTool tool, Type itemType );
	}
}