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
	public class Resmelt
	{
		public Resmelt()
		{
		}

		public static void Do( Mobile from, BuildSystem buildSystem, BaseBuildingTool tool )
		{
			int num = buildSystem.CanBuild( from, tool, null );

			if ( num > 0 )
			{
				from.SendGump( new BuildGump( from, buildSystem, tool, num ) );
			}
			else
			{
				from.Target = new InternalTarget( buildSystem, tool );
				from.SendLocalizedMessage( 1044273 ); // Target an item to recycle.
			}
		}

		private class InternalTarget : Target
		{
			private BuildSystem m_BuildSystem;
			private BaseBuildingTool m_Tool;

			public InternalTarget( BuildSystem buildSystem, BaseBuildingTool tool ) :  base ( 2, false, TargetFlags.None )
			{
				m_BuildSystem = buildSystem;
				m_Tool = tool;
			}

			private bool Resmelt( Mobile from, Item item, BuildResource resource )
			{
				try
				{
					if ( BuildResources.GetType( resource ) != BuildResourceType.BasePieces )
						return false;

					BuildResourceInfo info = BuildResources.GetInfo( resource );

					if ( info == null || info.ResourceTypes.Length == 0 )
						return false;

					BuildItem buildItem = m_BuildSystem.BuildItems.SearchFor( item.GetType() );

					if ( buildItem == null || buildItem.Ressources.Count == 0 )
						return false;

					BuildRes buildResource = buildItem.Ressources.GetAt( 0 );

					if ( buildResource.Amount < 2 )
						return false; // Not enough metal to resmelt

					Type resourceType = info.ResourceTypes[0];
					Item ingot = (Item)Activator.CreateInstance( resourceType );

					if ( item is DragonBardingDeed || (item is BaseArmor && ((BaseArmor)item).PlayerConstructed) || (item is BaseWeapon && ((BaseWeapon)item).PlayerConstructed) || (item is BaseClothing && ((BaseClothing)item).PlayerConstructed) )
						ingot.Amount = buildResource.Amount / 2;
					else
						ingot.Amount = 1;

					item.Delete();
					from.AddToBackpack( ingot );

					from.PlaySound( 0x2A );
					from.PlaySound( 0x240 );
					return true;
				}
				catch
				{
				}

				return false;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				int num = m_BuildSystem.CanBuild( from, m_Tool, null );

				if ( num > 0 )
				{
					from.SendGump( new BuildGump( from, m_BuildSystem, m_Tool, num ) );
				}
			}
		}
	}
}