/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Items;

namespace Server.Engines.Build
{
	public class Repair
	{
		public Repair()
		{
		}

		public static void Do( Mobile from, BuildSystem buildSystem, BaseBuildingTool tool )
		{
			from.Target = new InternalTarget( buildSystem, tool );
			from.SendLocalizedMessage( 1044276 ); // Target an item to repair.
		}

		public static void Do( Mobile from, BuildSystem buildSystem, RepairDeed deed )
		{
			from.Target = new InternalTarget( buildSystem, deed );
			from.SendLocalizedMessage( 1044276 ); // Target an item to repair.
		}

		private class InternalTarget : Target
		{
			private BuildSystem m_BuildSystem;
			private BaseBuildingTool m_Tool;
			private RepairDeed m_Deed;

			public InternalTarget( BuildSystem buildSystem, BaseBuildingTool tool ) :  base ( 2, false, TargetFlags.None )
			{
				m_BuildSystem = buildSystem;
				m_Tool = tool;
			}

			public InternalTarget( BuildSystem buildSystem, RepairDeed deed ) : base( 2, false, TargetFlags.None )
			{
				m_BuildSystem = buildSystem;
				m_Deed = deed;
			}

			private int GetWeakenChance( Mobile mob, LokaiSkillName lokaiSkill, int curHits, int maxHits )
			{
				// 40% - (1% per hp lost) - (1% per 10 build lokaiSkill)
                return (40 + (maxHits - curHits)) - (int)((LokaiSkillUtilities.XMLGetSkills(mob)[lokaiSkill].Value) / 10);
			}

			private bool CheckWeaken( Mobile mob, LokaiSkillName lokaiSkill, int curHits, int maxHits )
			{
				return ( GetWeakenChance( mob, lokaiSkill, curHits, maxHits ) > Utility.Random( 100 ) );
			}

			private int GetRepairDifficulty( int curHits, int maxHits )
			{
				return (((maxHits - curHits) * 1250) / Math.Max( maxHits, 1 )) - 250;
			}

			private bool CheckRepairDifficulty( Mobile mob, LokaiSkillName lokaiSkill, int curHits, int maxHits )
			{
                return false;
			}

			private bool CheckDeed( Mobile from )
			{
				if( m_Deed != null )
				{
					return m_Deed.Check( from );
				}

				return true;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				int number;

				if( !CheckDeed( from ) )
					return;


				bool usingDeed = (m_Deed != null);
				bool toDelete = false;
                
                if ( targeted is Item )
				{
					number = (usingDeed)? 1061136 : 1044277; // That item cannot be repaired. // You cannot repair that item with this type of repair contract.
				}
				else
				{
					number = 500426; // You can't repair that.
				}

				if( !usingDeed )
				{
					BuildContext context = m_BuildSystem.GetContext( from );
					from.SendGump( new BuildGump( from, m_BuildSystem, m_Tool, number ) );
				}
				else if( toDelete )
				{
					m_Deed.Delete();
				}
			}
		}
	}
}