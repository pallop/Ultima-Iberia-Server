using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
	public class DefTrapCrafting : CraftSystem
	{
		public override SkillName MainSkill
		{
			get{ return SkillName.Tinkering; }
		}

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>TRAPCRAFTING MENU</CENTER></basefont>"; } 
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefTrapCrafting();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefTrapCrafting() : base( 1, 1, 1.25 )// base( 1, 2, 1.7 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
            from.PlaySound( 0x241 ); 
		}

		private class InternalTimer : Timer
		{
			private Mobile m_From;

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( 0.7 ) )
			{
				m_From = from;
			}

			protected override void OnTick()
			{
				m_From.PlaySound( 0x1C6 );
			}
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 );

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043;
				else
					return 1044157;
			}
			else
			{
				from.PlaySound( 0x1c6 );

				if ( quality == 0 )
					return 502785;
				else if ( makersMark && quality == 2 )
					return 1044156;
				else if ( quality == 2 )
					return 1044155;
				else
					return 1044154;
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

            #region Components
            index = AddCraft(typeof( TrapFrame ), "Components", "Trap Frame", 20.0, 40.0, typeof( IronIngot ), "Iron Ingot", 2, "You need more iron ingots.");
            AddRes(index, typeof(Leather), "Leather", 2, "You need more leather");
            AddRes(index, typeof(Board), "Board", 1, "You need a board");

            index = AddCraft(typeof( TrapSpike ), "Components", "Trap Spike", 25.0, 45.0, typeof(Bolt), "Crossbow Bolt", 1, "You need a crossbow bolt.");
            AddRes(index, typeof( Springs ), "Springs", 1, "You need some springs");

            index = AddCraft(typeof( TrapCrystalTrigger ), "Components", "Trap Crystal", 60.0, 80.0, typeof( CrystalisedEnergy ), "Crystalised Energy", 1, "You need a piece of crystalised energy.");
            AddRes(index, typeof( DullCopperIngot ), "Dull Copper Ingot", 2, "You need more dull copper ingots");
            AddRes(index, typeof( Springs ), "Springs", 2, "You need more springs");

            index = AddCraft(typeof( TrapCrystalSensor ), "Components", "Crystal Sensor", 90.0, 110.0, typeof( GazerEye ), "Gazer Eye", 1, "You need a gazer eye.");
            AddRes(index, typeof( TrapCrystalTrigger ), "Crystal Trigger", 1, "You need a crystal trigger.");
            #endregion

            #region Explosive Traps
            index = AddCraft(typeof(ExplosiveLesserTrap), "Explosive Traps", "Lesser Explosive Trap", 35.0, 55.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(LesserExplosionPotion), "Lesser Explosion Potion", 1, "You need an explosion potion");
            AddRes(index, typeof(TrapSpike), "Trap Spike", 1, "You need a trap spike");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(ExplosiveRegularTrap), "Explosive Traps", "Explosive Trap", 50.0, 70.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(ExplosionPotion), "Explosion Potion", 1, "You need an explosion potion");
            AddRes(index, typeof(TrapSpike), "Trap Spike", 1, "You need a trap spike");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(ExplosiveGreaterTrap), "Explosive Traps", "Greater Explosive Trap", 65.0, 85.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(GreaterExplosionPotion), "Greater Explosion Potion", 1, "You need an explosion potion");
            AddRes(index, typeof(TrapSpike), "Trap Spike", 1, "You need a trap spike");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");
            #endregion

            #region Freezing Traps
            index = AddCraft(typeof(FreezingLesserTrap), "Freezing Traps", "Lesser Freezing Trap", 50.0, 70.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(DryIce), "Dry Ice", 1, "You need dry ice.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(FreezingRegularTrap), "Freezing Traps", "Freezing Trap", 65.0, 85.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(DryIce), "Dry Ice", 2, "You need more dry ice.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(FreezingGreaterTrap), "Freezing Traps", "Greater Freezing Trap", 90.0, 110.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(DryIce), "Dry Ice", 3, "You need more dry ice.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");
            #endregion

            #region Lightning Traps
            index = AddCraft(typeof(LightningLesserTrap), "Lightning Traps", "Lesser Lightning Trap", 45.0, 65.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(BottledLightning), "Bottled Lightning", 1, "You need bottled lightning.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(LightningRegularTrap), "Lightning Traps", "Lightning Trap", 60.0, 80.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(BottledLightning), "Bottled Lightning", 2, "You need more bottled lightning.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(LightningGreaterTrap), "Lightning Traps", "Greater Lightning Trap", 75.0, 95.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(BottledLightning), "Bottled Lightning", 3, "You need more bottled lightning.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");
            #endregion

            #region Paralysis Traps
            index = AddCraft(typeof(ParalysisLesserTrap), "Paralysis Traps", "Lesser Paralysis Trap", 40.0, 60.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(GiantSpiderVenom), "Giant Spider Venom Gland", 1, "You need a giant spider venom sac.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(ParalysisRegularTrap), "Paralysis Traps", "Paralysis Trap", 55.0, 75.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(GiantSpiderVenom), "Giant Spider Venom Gland", 2, "You need more giant spider venom sacs.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears.");

            index = AddCraft(typeof(ParalysisGreaterTrap), "Paralysis Traps", "Greater Paralysis Trap", 70.0, 90.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(GiantSpiderVenom), "Giant Spider Venom Gland", 3, "You need more giant spider venom sacs.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears.");
            #endregion

            #region Other Traps
            index = AddCraft(typeof(BladeSpiritTrap), "Other Devices", "Blade Spirit Trap", 35.0, 55.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(TrappedGhost), "Trapped Ghost", 1, "You need a trapped ghost.");
            AddRes(index, typeof(Hammer), "Hammers", 1, "You need some a hammer.");
            AddRes(index, typeof(CrescentBlade), "Crescent Blades", 4, "You need more crescent blades.");

            index = AddCraft(typeof(GhostTrap), "Other Devices", "Ghost Trap", 75.0, 95.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(Bottle), "Bottle", 1, "You need a bottle.");
            AddRes(index, typeof(TrapCrystalTrigger), "Crystal Trigger", 1, "You need a crystal trigger.");
            AddRes(index, typeof(Garlic), "Garlic", 4, "You need more garlic.");

            index = AddCraft(typeof(TrapDetector), "Other Devices", "Trap Detector", 55.0, 75.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(Springs), "Springs", 4, "You need some more springs");
            AddRes(index, typeof(Hammer), "Hammers", 4, "You need some more hammers");
            AddRes(index, typeof(Buckler), "Buckler", 1, "You need a buckler");

            index = AddCraft(typeof(TrapTest), "Other Devices", "Whoopie Cushion", 25.0, 45.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(Leather), "Leather", 2, "You need more leather");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");
            AddRes(index, typeof(Springs), "Springs", 1, "You need some springs");
            #endregion

            #region Poison Dart Traps
            index = AddCraft(typeof(PoisonLesserDartTrap), "Poison Dart Traps", "Lesser Poison Dart Trap", 25.0, 45.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(LesserPoisonPotion), "Lesser Poison Potion", 1, "You need a lesser poison potion");
            AddRes(index, typeof(TrapSpike), "Trap Spike", 1, "You need a trap spike");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(PoisonRegularDartTrap), "Poison Dart Traps", "Poison Dart Trap", 40.0, 60.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(PoisonPotion), "Poison Potion", 1, "You need a poison potion");
            AddRes(index, typeof(TrapSpike), "Trap Spike", 1, "You need a trap spike");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(PoisonGreaterDartTrap), "Poison Dart Traps", "Greater Poison Dart Trap", 55.0, 75.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(GreaterPoisonPotion), "Greater Poison Potion", 1, "You need a greater poison potion");
            AddRes(index, typeof(TrapSpike), "Trap Spike", 1, "You need a trap spike");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");

            index = AddCraft(typeof(PoisonDeadlyDartTrap), "Poison Dart Traps", "Deadly Poison Dart Trap", 70.0, 90.0, typeof(TrapFrame), "Trap Frame", 1, "You need a trap frame.");
            AddRes(index, typeof(DeadlyPoisonPotion), "Deadly Poison Potion", 1, "You need a deadly poison potion");
            AddRes(index, typeof(TrapSpike), "Trap Spike", 1, "You need a trap spike");
            AddRes(index, typeof(Gears), "Gears", 2, "You need more gears");
            #endregion
        }
	}
}