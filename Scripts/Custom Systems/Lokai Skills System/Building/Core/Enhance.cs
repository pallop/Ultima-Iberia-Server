/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server;
using Server.Targeting;
using Server.Items;

namespace Server.Engines.Build
{
	public enum EnhanceResult
	{
		NotInBackpack,
		BadItem,
		BadResource,
		AlreadyEnhanced,
		Success,
		Failure,
		Broken,
		NoResources,
		NoLokaiSkill
	}

	public class Enhance
	{
		public static EnhanceResult Invoke( Mobile from, BuildSystem buildSystem, BaseBuildingTool tool, Item item, BuildResource resource, Type resType, ref object resMessage )
		{
			return EnhanceResult.BadItem;
		}

		public static void CheckResult( ref EnhanceResult res, int chance )
		{
			if ( res != EnhanceResult.Success )
				return; // we've already failed..

			int random = Utility.Random( 100 );

			if ( 10 > random )
				res = EnhanceResult.Failure;
			else if ( chance > random )
				res = EnhanceResult.Broken;
		}

		public static void BeginTarget( Mobile from, BuildSystem buildSystem, BaseBuildingTool tool )
		{
			BuildContext context = buildSystem.GetContext( from );

			if ( context == null )
				return;

			int lastRes = context.LastResourceIndex;
			BuildSubResCol subRes = buildSystem.BuildSubRes;

			if ( lastRes >= 0 && lastRes < subRes.Count )
			{
				BuildSubRes res = subRes.GetAt( lastRes );

				if ( LokaiSkillUtilities.XMLGetSkills(from)[buildSystem.MainLokaiSkill].Value < res.RequiredLokaiSkill )
				{
					from.SendGump( new BuildGump( from, buildSystem, tool, res.Message ) );
				}
				else
				{
					BuildResource resource = BuildResources.GetFromType( res.ItemType );

					if ( resource != BuildResource.None )
					{
						from.Target = new InternalTarget( buildSystem, tool, res.ItemType, resource );
						from.SendLocalizedMessage( 1061004 ); // Target an item to enhance with the properties of your selected material.
					}
					else
					{
						from.SendGump( new BuildGump( from, buildSystem, tool, 1061010 ) ); // You must select a special material in order to enhance an item with its properties.
					}
				}
			}
			else
			{
				from.SendGump( new BuildGump( from, buildSystem, tool, 1061010 ) ); // You must select a special material in order to enhance an item with its properties.
			}

		}

		private class InternalTarget : Target
		{
			private BuildSystem m_BuildSystem;
			private BaseBuildingTool m_Tool;
			private Type m_ResourceType;
			private BuildResource m_Resource;

			public InternalTarget( BuildSystem buildSystem, BaseBuildingTool tool, Type resourceType, BuildResource resource ) :  base ( 2, false, TargetFlags.None )
			{
				m_BuildSystem = buildSystem;
				m_Tool = tool;
				m_ResourceType = resourceType;
				m_Resource = resource;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{
					object message = null;
					EnhanceResult res = Enhance.Invoke( from, m_BuildSystem, m_Tool, (Item)targeted, m_Resource, m_ResourceType, ref message );

					switch ( res )
					{
						case EnhanceResult.NotInBackpack: message = 1061005; break; // The item must be in your backpack to enhance it.
						case EnhanceResult.AlreadyEnhanced: message = 1061012; break; // This item is already enhanced with the properties of a special material.
						case EnhanceResult.BadItem: message = 1061011; break; // You cannot enhance this type of item with the properties of the selected special material.
						case EnhanceResult.BadResource: message = 1061010; break; // You must select a special material in order to enhance an item with its properties.
						case EnhanceResult.Broken: message = 1061080; break; // You attempt to enhance the item, but fail catastrophically. The item is lost.
						case EnhanceResult.Failure: message = 1061082; break; // You attempt to enhance the item, but fail. Some material is lost in the process.
						case EnhanceResult.Success: message = 1061008; break; // You enhance the item with the properties of the special material.
						case EnhanceResult.NoLokaiSkill: message = 1044153; break; // You don't have the required lokaiSkills to attempt this item.
					}

					from.SendGump( new BuildGump( from, m_BuildSystem, m_Tool, message ) );
				}
			}
		}
	}
}