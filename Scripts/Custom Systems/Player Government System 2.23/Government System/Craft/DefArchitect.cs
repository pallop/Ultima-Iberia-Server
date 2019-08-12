using System;
using Server.Items;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.Multis.Deeds;
using System.Collections;
using System.Collections.Generic;


namespace Server.Engines.Build
{
	public class DefArchitect : BuildSystem
	{
			public override LokaiSkillName MainLokaiSkill
		{
			get	{ return LokaiSkillName.Construction; }
		}


		public override string GumpTitleString
		{
			get { return "<BASEFONT COLOR=#FFFFFF><CENTER>Architecture</CENTER></BASEFONT>"; }
		}

		private static BuildSystem m_BuildSystem;

		public static BuildSystem BuildSystem
		{
			get
			{
				if ( m_BuildSystem == null )
					m_BuildSystem = new DefArchitect();

				return m_BuildSystem;
			}
		}

		public override double GetChanceAtMin( BuildItem item )
		{
			return 0.50; // 50%
		}

		private DefArchitect() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
		{
		}

		public override int CanBuild( Mobile from, BaseBuildingTool tool, Type typeItem )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
				
			return 0;
		}

		public override void PlayBuildEffect( Mobile from )
		{
			from.PlaySound( 0x23D );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, BuildItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{	
				return 1044154; // You create the item.
			}
		}

		public override void InitBuildList()
		{
			int index = -1;
			
			// Comp

			index = AddBuild( typeof( WallSection ), "Components", "wall section", 95.0, 115.0, typeof( Board ), "Board", 100 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 115.0 );
			AddRes( index, typeof( IronIngot ), "Iron Ingot", 50 );

			index = AddBuild( typeof( FloorSection ), "Components", "floor section", 95.0, 115.0, typeof( Board ), "Board", 100 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 115.0 );
			AddRes( index, typeof( IronIngot ), "Iron Ingot", 50 );

			index = AddBuild( typeof( RoofSection ), "Components", "roof section", 95.0, 115.0, typeof( Board ), "Board", 100 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 115.0 );
			AddRes( index, typeof( IronIngot ), "Iron Ingot", 50 );

			index = AddBuild( typeof( DeckSection ), "Components", "deck section", 95.0, 115.0, typeof( Board ), "Board", 100 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 115.0 );
			AddRes( index, typeof( IronIngot ), "Iron Ingot", 50 );

			index = AddBuild( typeof( SailSection ), "Components", "sail section", 95.0, 115.0, typeof( Cloth ), "Cloth", 100 );
			AddLokaiSkill( index, LokaiSkillName.Spinning, 95.0, 115.0 );

			index = AddBuild( typeof( FullWallUnit ), "Components", "full wall unit", 95.0, 115.0, typeof( WallSection ), "Wall Section", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 115.0 );

			index = AddBuild( typeof( FullFloorUnit ), "Components", "full floor unit", 95.0, 115.0, typeof( FloorSection ), "Floor Section", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 115.0 );

			index = AddBuild( typeof( FullRoofUnit ), "Components", "full roof unit", 95.0, 115.0, typeof( RoofSection ), "Roof Section", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 115.0 );

			index = AddBuild( typeof( FullDeckUnit ), "Components", "full deck unit", 95.0, 115.0, typeof( DeckSection ), "Deck Section", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 115.0 );

			index = AddBuild( typeof( FullSailUnit ), "Components", "full sail unit", 95.0, 115.0, typeof( SailSection ), "Sail Section", 10 );
			AddLokaiSkill( index, LokaiSkillName.Spinning, 95.0, 115.0 );

			index = AddBuild( typeof( SupportBeam ), "Components", "support beam", 95.0, 115.0, typeof( Board ), "Board", 75 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 115.0 );

			index = AddBuild( typeof( FoundationBlock ), "Components", "foundation block", 95.0, 115.0, typeof( Granite ), "Granite", 5 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 115.0 );

			// Residential Structure

			index = AddBuild( typeof( StonePlasterHouseDeed ), "Residential Structure", "stone and plaster house", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( FieldStoneHouseDeed ), "Residential Structure", "field stone house", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( SmallBrickHouseDeed ), "Residential Structure", "small brick house", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( WoodPlasterHouseDeed ), "Residential Structure", "wood and plaster house", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( ThatchedRoofCottageDeed ), "Residential Structure", "thatched roof cottage", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( StoneWorkshopDeed ), "Residential Structure", "small stone workshop", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( MarbleWorkshopDeed ), "Residential Structure", "small marble workshop", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( SmallTowerDeed ), "Residential Structure", "small tower", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( VillaDeed ), "Residential Structure", "two-story villa", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 12 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 12 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 12 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 12 );

			index = AddBuild( typeof( SandstonePatioDeed ), "Residential Structure", "sandstone house with patio", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 11 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 11 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 11 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 11 );

			index = AddBuild( typeof( LogCabinDeed ), "Residential Structure", "two-story log cabin", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 12 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 12 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 12 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 12 );

			index = AddBuild( typeof( BrickHouseDeed ), "Residential Structure", "brick house", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 15 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 15 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 15 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 15 );

			index = AddBuild( typeof( TwoStoryWoodPlasterHouseDeed ), "Residential Structure", "two-story wood and plaster house", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 15 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 15 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 15 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 15 );

			index = AddBuild( typeof( TwoStoryStonePlasterHouseDeed ), "Residential Structure", "two-story stone and plaster house", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 15 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 15 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 15 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 15 );

			index = AddBuild( typeof( LargePatioDeed ), "Residential Structure", "large house with patio", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 15 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 15 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 15 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 15 );

			index = AddBuild( typeof( LargeMarbleDeed ), "Residential Structure", "marble house with patio", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 15 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 15 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 15 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 15 );

			index = AddBuild( typeof( TowerDeed ), "Residential Structure", "Tower", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 20 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 20 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 20 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 20 );

			index = AddBuild( typeof( KeepDeed ), "Residential Structure", "small stone keep", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 25 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 25 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 25 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 25 );

			index = AddBuild( typeof( CastleDeed ), "Residential Structure", "castle", 90.0, 105.0, typeof( FullWallUnit ), "Full Wall Unit", 30 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 90.0, 105.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 30 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 30 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 30 );

			// Civic Structures

			index = AddBuild( typeof( FieldStoneCityHallDeed ), "Civic Structure", "field stone city hall", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 30 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( SandstoneCityHallDeed ), "Civic Structure", "sandstone city hall", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 30 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( MarbleCityHallDeed ), "Civic Structure", "marble city hall", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 30 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( NecroCityHallDeed ), "Civic Structure", "necro city hall", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 30 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( AsianCityHallDeed ), "Civic Structure", "asian city hall", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 30 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( StoneCityHallDeed ), "Civic Structure", "stone city hall", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 30 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( WoodCityHallDeed ), "Civic Structure", "wood city hall", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 30 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( PlasterCityHallDeed ), "Civic Structure", "plaster city hall", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 30 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( PlasterCityHealerDeed ), "Civic Structure", "plaster city healer", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( WoodCityHealerDeed ), "Civic Structure", "wood city healer", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( StoneCityHealerDeed ), "Civic Structure", "stone city healer", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( FieldstoneCityHealerDeed ), "Civic Structure", "fieldstone city healer", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( SandstoneCityHealerDeed ), "Civic Structure", "sandstone city healer", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( MarbleCityHealerDeed ), "Civic Structure", "marble city healer", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( NecroCityHealerDeed ), "Civic Structure", "necro city healer", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( AsianCityHealerDeed ), "Civic Structure", "asian city healer", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( WoodCityBankDeed ), "Civic Structure", "wood city bank", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 5 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 5 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 5 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 5 );

			index = AddBuild( typeof( StoneCityBankDeed ), "Civic Structure", "stone city bank", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 5 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 5 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 5 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 5 );

			index = AddBuild( typeof( PlasterCityBankDeed ), "Civic Structure", "plaster city bank", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 5 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 5 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 5 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 5 );

			index = AddBuild( typeof( FieldstoneCityBankDeed ), "Civic Structure", "fieldstone city bank", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 5 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 5 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 5 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 5 );

			index = AddBuild( typeof( SandstoneCityBankDeed ), "Civic Structure", "sandstone city bank", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 5 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 5 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 5 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 5 );

			index = AddBuild( typeof( NecroCityBankDeed ), "Civic Structure", "necro city bank", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 5 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 5 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 5 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 5 );

			index = AddBuild( typeof( MarbleCityBankDeed ), "Civic Structure", "marble city bank", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 5 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 5 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 5 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 5 );

			index = AddBuild( typeof( AsianCityBankDeed ), "Civic Structure", "asian city bank", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 5 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 5 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 5 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 5 );

			index = AddBuild( typeof( WoodCityStableDeed ), "Civic Structure", "wood city stable", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( StoneCityStableDeed ), "Civic Structure", "stone city stable", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( PlasterCityStableDeed ), "Civic Structure", "plaster city stable", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( FieldstoneCityStableDeed ), "Civic Structure", "fieldstone city stable", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( SandstoneCityStableDeed ), "Civic Structure", "sandstone city stable", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( NecroCityStableDeed ), "Civic Structure", "necro city stable", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( MarbleCityStableDeed ), "Civic Structure", "marble city stable", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( AsianCityStableDeed ), "Civic Structure", "asian city stable", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( WoodCityTavernDeed ), "Civic Structure", "wood city tavern", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 25 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( StoneCityTavernDeed ), "Civic Structure", "stone city tavern", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 25 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( PlasterCityTavernDeed ), "Civic Structure", "plaster city tavern", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 25 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( FieldstoneCityTavernDeed ), "Civic Structure", "fieldstone city tavern", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 25 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( SandstoneCityTavernDeed ), "Civic Structure", "sandstone city tavern", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 25 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( MarbleCityTavernDeed ), "Civic Structure", "marble city tavern", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 25 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( NecroCityTavernDeed ), "Civic Structure", "necro city tavern", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 25 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( AsianCityTavernDeed ), "Civic Structure", "asian city tavern", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 25 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			// Harvesters
			/*
			index = AddBuild( typeof( AsianCityMoongateDeed ), "Harvesters", "small mineral harvester", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 15 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 15 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 15 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 15 );

			index = AddBuild( typeof( AsianCityMoongateDeed ), "Harvesters", "small organic harvester", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 15 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 15 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 15 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 15 );

			index = AddBuild( typeof( AsianCityMoongateDeed ), "Harvesters", "medium mineral harvester", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 15 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 15 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 15 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 15 );

			index = AddBuild( typeof( AsianCityMoongateDeed ), "Harvesters", "medium organic harvester", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 15 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 15 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 15 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 15 );

			index = AddBuild( typeof( AsianCityMoongateDeed ), "Harvesters", "large mineral harvester", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 15 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 15 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 15 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 15 );

			index = AddBuild( typeof( AsianCityMoongateDeed ), "Harvesters", "large organic harvester", 95.0, 106.0, typeof( FullWallUnit ), "Full Wall Unit", 15 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 95.0, 106.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 15 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 15 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 15 );
			*/
			// Parks & Gardens

			index = AddBuild( typeof( SmallCityGardenDeed ), "Parks & Gardens", "small garden", 80.0, 100.0, typeof( FullWallUnit ), "Full Wall Unit", 5 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 80.0, 100.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 5 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 5 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 5 );

			index = AddBuild( typeof( MediumCityGardenDeed ), "Parks & Gardens", "medium garden", 80.0, 100.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 80.0, 100.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( LargeCityGardenDeed ), "Parks & Gardens", "large garden", 80.0, 100.0, typeof( FullWallUnit ), "Full Wall Unit", 15 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 80.0, 100.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 15 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 15 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 15 );

			index = AddBuild( typeof( SmallCityParkDeed ), "Parks & Gardens", "small park", 80.0, 100.0, typeof( FullWallUnit ), "Full Wall Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 80.0, 100.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );

			index = AddBuild( typeof( MediumCityParkDeed ), "Parks & Gardens", "medium park", 80.0, 100.0, typeof( FullWallUnit ), "Full Wall Unit", 15 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 80.0, 100.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 15 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 15 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 15 );

			index = AddBuild( typeof( LargeCityParkDeed ), "Parks & Gardens", "large park", 80.0, 100.0, typeof( FullWallUnit ), "Full Wall Unit", 20 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 80.0, 100.0 );
			AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 20 );
			AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 20 );
			AddRes( index, typeof( FoundationBlock ), "Foundation Block", 20 );

			// Shipwright

			index = AddBuild( typeof( SmallBoatDeed ), "Shipwright Structures", "small ship", 50.0, 80.0, typeof( FullDeckUnit ), "Full Deck Unit", 5 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 50.0, 80.0 );
			AddRes( index, typeof( FullSailUnit ), "Full Sail Unit", 5 );
			AddRes( index, typeof( SupportBeam ), "Support Beam", 2 );

			index = AddBuild( typeof( MediumBoatDeed ), "Shipwright Structures", "medium ship", 60.0, 90.0, typeof( FullDeckUnit ), "Full Deck Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 60.0, 90.0 );
			AddRes( index, typeof( FullSailUnit ), "Full Sail Unit", 10 );
			AddRes( index, typeof( SupportBeam ), "Support Beam", 4 );

			index = AddBuild( typeof( LargeBoatDeed ), "Shipwright Structures", "large ship", 70.0, 100.0, typeof( FullDeckUnit ), "Full Deck Unit", 15 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 70.0, 100.0 );
			AddRes( index, typeof( FullSailUnit ), "Full Sail Unit", 15 );
			AddRes( index, typeof( SupportBeam ), "Support Beam", 6 );

			index = AddBuild( typeof( SmallDragonBoatDeed ), "Shipwright Structures", "small dragon ship", 60.0, 90.0, typeof( FullDeckUnit ), "Full Deck Unit", 10 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 60.0, 90.0 );
			AddRes( index, typeof( FullSailUnit ), "Full Sail Unit", 10 );
			AddRes( index, typeof( SupportBeam ), "Support Beam", 4 );

			index = AddBuild( typeof( MediumDragonBoatDeed ), "Shipwright Structures", "medium dragon ship", 70.0, 100.0, typeof( FullDeckUnit ), "Full Deck Unit", 15 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 70.0, 100.0 );
			AddRes( index, typeof( FullSailUnit ), "Full Sail Unit", 15 );
			AddRes( index, typeof( SupportBeam ), "Support Beam", 6 );

			index = AddBuild( typeof( LargeDragonBoatDeed ), "Shipwright Structures", "large dragon ship", 80.0, 110.0, typeof( FullDeckUnit ), "Full Deck Unit", 20 );
			AddLokaiSkill( index, LokaiSkillName.StoneMasonry, 80.0, 110.0 );
			AddRes( index, typeof( FullSailUnit ), "Full Sail Unit", 20 );
			AddRes( index, typeof( SupportBeam ), "Support Beam", 8 );

			//ASEDIO
			 //Municion Asedio
            this.AddBuild(typeof(LightCannonball), "Municion Asedio", "Light Cannonball", 60.0, 120.0, typeof(Granite), 1044514, 3, 1044513);
           index = this.AddBuild(typeof(FieryCannonball), "Municion Asedio", "Fiery Cannonball", 60.0, 120.0, typeof(Granite), 1044514, 7, 1044513);
           //this.AddLokaiSkill(index, SkillName.Alchemy, 75.0, 80.0);
           this.AddRes(index, typeof(MandrakeRoot), 1044357, 30, 1044365);

             index = this.AddBuild(typeof(ExplodingCannonball), "Municion Asedio", "Exploding Cannonball", 60.0, 120.0, typeof(Granite), 1044514, 10, 1044513);
            //this.AddLokaiSkill(index, SkillName.Alchemy, 75.0, 80.0);
                this.AddRes(index, typeof(SulfurousAsh), 1044359, 30, 1044367);

            index = this.AddBuild(typeof(IronCannonball), "Municion Asedio", "Iron Cannonball", 60.0, 120.0, typeof(Granite), 1044514, 5, 1044513);
            this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes(index, typeof(IronIngot), 1044036, 30, 1044037);

            this.AddBuild(typeof(LightSiegeLog), "Municion Asedio", "Light SiegeLog", 99.2, 103.9, typeof(Log), 1044041, 50, 1044351);
			this.AddBuild(typeof(HeavySiegeLog), "Municion Asedio", "Heavy SiegeLog", 99.2, 103.9, typeof(Log), 1044041, 100, 1044351);
			index = this.AddBuild(typeof(IronSiegeLog), "Municion Asedio", "Iron SiegeLog", 99.2, 103.9, typeof(Log), 1044041, 100, 1044351);
			this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes(index, typeof(IronIngot), 1044036, 30, 1044037);


 			index = this.AddBuild(typeof(SiegeCatapultDeed), "Armas Asedio", "Cañon", 99.2, 103.9, typeof(IronIngot), 1044036, 8500, 1044037);
			//this.AddLokaiSkill(index, SkillName.Carpentry, 75.0, 80.0);
                this.AddRes(index,  typeof(Board), 1044041, 1500, 1044351);

index = this.AddBuild(typeof(SiegeCatapultDeed), "Armas Asedio", "Catapulta", 99.2, 103.9, typeof(Board), 1044041, 8500, 1044351);
this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes(index, typeof(IronIngot), 1044036, 700, 1044037);

index = this.AddBuild(typeof(SiegeRamDeed), "Armas Asedio", "Ariete", 99.2, 103.9, typeof(Board), 1044041, 8500, 1044351);
this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes(index, typeof(IronIngot), 1044036, 700, 1044037);

index = this.AddBuild(typeof(SiegeRepairTool), "Reparacion Edificios/Asedio", "Herramienta Reparacion", 99.2, 103.9, typeof(Log), 1044041, 100, 1044351);
this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes(index, typeof(IronIngot), 1044036, 30, 1044037);

//FIN ASEDIOS
//Murallas


      
index = this.AddBuild(typeof(EmpalizadaEscaleraNorteDeed), "Empalizada", "Empalizada Escalera Norte", 94.7, 115.0, typeof( FullWallUnit ), "Full Wall Unit", 9 );
               this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 3 );
index = this.AddBuild(typeof(EmpalizadaNorteDeed), "Empalizada", "Empalizada Norte", 94.7, 115.0, typeof( FullWallUnit ), "Full Wall Unit", 9 );
               this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 3 );
index = this.AddBuild(typeof(EmpalizadaEscaleraWestDeed), "Empalizada", "Empalizada Escalera Oeste", 94.7, 115.0, typeof( FullWallUnit ), "Full Wall Unit", 9 );
               this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 3 );
index = this.AddBuild(typeof(EmpalizadaWestDeed), "Empalizada", "Empalizada Oeste", 94.7, 115.0, typeof( FullWallUnit ), "Full Wall Unit", 9 );
               this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 3 );
index = this.AddBuild(typeof(EmpalizadaEscaleraEstDeed), "Empalizada", "Empalizada Escalera Este", 94.7, 115.0, typeof( FullWallUnit ), "Full Wall Unit", 9 );
               this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 3 );
index = this.AddBuild(typeof(EmpalizadaEstDeed), "Empalizada", "Empalizada Este", 94.7, 115.0, typeof( FullWallUnit ), "Full Wall Unit", 9 );
               this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 3 );
index = this.AddBuild(typeof(EmpalizadaEscaleraSurDeed), "Empalizada", "Empalizada Escalera Sur", 94.7, 115.0, typeof( FullWallUnit ), "Full Wall Unit", 9 );
               this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 3 );
//Puertas y torres empalizada
index = this.AddBuild(typeof(EmpalizadaPuertaNDeed), "Empalizada", "Empalizada Puerta Norte", 100.0, 115.0, typeof( FullWallUnit ), "Full Wall Unit", 13 );
               this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 3 );
index = this.AddBuild(typeof(EmpalizadaPuertaSurDeed), "Empalizada", "Empalizada Puerta Sur", 100.0, 115.0, typeof( FullWallUnit ), "Full Wall Unit", 13 );
               this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 3 );
index = this.AddBuild(typeof(EmpalizadaPuertaWestDeed), "Empalizada", "Empalizada Puerta Oeste", 100.0, 115.0, typeof( FullWallUnit ), "Full Wall Unit", 13 );
               this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
this.AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 3 );
index = this.AddBuild(typeof(EmpalizadaPuertaEstDeed), "Empalizada", "Empalizada Puerta Este", 100.0, 115.0, typeof( FullWallUnit ), "Full Wall Unit", 13 );
               this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 3 );
index = this.AddBuild(typeof(EmpalizadaTorreEsquineraDeed), "Empalizada", "Empalizada Torre Esquinera", 100.0, 115.0, typeof( FullWallUnit ), "Full Wall Unit", 16 );
               this.AddLokaiSkill(index, LokaiSkillName.StoneMasonry, 75.0, 80.0);
                this.AddRes( index, typeof( FullFloorUnit ), "Full Floor Unit", 10 );
			this.AddRes( index, typeof( FullRoofUnit ), "Full Roof Unit", 10 );
			this.AddRes( index, typeof( FoundationBlock ), "Foundation Block", 10 );
			// Civic Decore

			index = AddBuild( typeof( CityHedge ), "Civic Decore", "hedge", 80.0, 100.0, typeof( FertileDirt ), "Fertile Dirt", 5 );

			index = AddBuild( typeof( CityLampPost ), "Civic Decore", "lamp post", 80.0, 100.0, typeof( IronIngot ), "Iron Ingot", 100 );

			index = AddBuild( typeof( CityTrashCan ), "Civic Decore", "trash barrel", 80.0, 100.0, typeof( Board ), "Board", 100 );
		}
	}
}
