/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Targeting;

namespace Server.Engines.Harvest
{
    public enum TreeResourceType
    {
        BarkSkin, FruitNut, LeafSpine, RootBranch, SapJuice
    }

    public enum TreeProductType
    {
        Crafting, Food, Medicine, Reagent, Spice
    }

    public class TreeHarvest : HarvestSystem
    {
        private static TreeHarvest m_System;

        public static TreeHarvest System
        {
            get
            {
                if (m_System == null)
                    m_System = new TreeHarvest();

                return m_System;
            }
        }

        public static void GetHarvestInfo(Mobile from, TreeProductItem product)
        {
            string use = "for unknown purposes";
            switch (product.ProductType)
            {
                case TreeProductType.Crafting: use = "for various types of crafting"; break;
                case TreeProductType.Food: use = "for food"; break;
                case TreeProductType.Medicine: use = "in the healing arts"; break;
                case TreeProductType.Reagent: use = "in the practice of magic"; break;
                case TreeProductType.Spice: use = "as a culinary spice"; break;
            }
            //TEMP: Show gump or something?
            from.SendMessage("This product is called {0}. It is used {1}.", product.Name, use);
        }

        private HarvestDefinition m_AppleTree, m_AshTree, m_BananaTree, m_BlackCherryTree, m_BlackOakTree, m_CassiaTree, 
            m_CedarTree, m_CherryTree, m_CoconutPalm, m_CypressTree, m_DatePalm, m_DogwoodTree, m_EucalyptusTree, 
            m_FigTree, m_HickoryTree, m_IronwoodTree, m_JuniperBush, m_MapleTree, m_OakTree, m_OhiiTree, 
            m_OliveTree, m_PeachTree, m_PearTree, m_PipeCactus, m_PlumTree, m_PoplarTree,
            m_SandalwoodTree, m_SpiderTree, m_TuscanyPineTree, m_WalnutTree, 
            m_WhiteBeechTree, m_WillowTree, m_YewTree, m_YuccaPlant;

        public HarvestDefinition AppleTree { get { return m_AppleTree; } }
        public HarvestDefinition AshTree { get { return m_AshTree; } }
        public HarvestDefinition BananaTree { get { return m_BananaTree; } }
        public HarvestDefinition BlackCherryTree { get { return m_BlackCherryTree; } }
        public HarvestDefinition BlackOakTree { get { return m_BlackOakTree; } }
        public HarvestDefinition CassiaTree { get { return m_CassiaTree; } }
        public HarvestDefinition CedarTree { get { return m_CedarTree; } }
        public HarvestDefinition CherryTree { get { return m_CherryTree; } }
        public HarvestDefinition CoconutPalm { get { return m_CoconutPalm; } }
        public HarvestDefinition CypressTree { get { return m_CypressTree; } }
        public HarvestDefinition DatePalm { get { return m_DatePalm; } }
        public HarvestDefinition DogwoodTree { get { return m_DogwoodTree; } }
        public HarvestDefinition EucalyptusTree { get { return m_EucalyptusTree; } }
        public HarvestDefinition FigTree { get { return m_FigTree; } }
        public HarvestDefinition HickoryTree { get { return m_HickoryTree; } }
        public HarvestDefinition IronwoodTree { get { return m_IronwoodTree; } }
        public HarvestDefinition JuniperBush { get { return m_JuniperBush; } }
        public HarvestDefinition MapleTree { get { return m_MapleTree; } }
        public HarvestDefinition OakTree { get { return m_OakTree; } }
        public HarvestDefinition OhiiTree { get { return m_OhiiTree; } }
        public HarvestDefinition OliveTree { get { return m_OliveTree; } }
        public HarvestDefinition PeachTree { get { return m_PeachTree; } }
        public HarvestDefinition PearTree { get { return m_PearTree; } }
        public HarvestDefinition PipeCactus { get { return m_PipeCactus; } }
        public HarvestDefinition PlumTree { get { return m_PlumTree; } }
        public HarvestDefinition PoplarTree { get { return m_PoplarTree; } }
        public HarvestDefinition SandalwoodTree { get { return m_SandalwoodTree; } }
        public HarvestDefinition SpiderTree { get { return m_SpiderTree; } }
        public HarvestDefinition TuscanyPineTree { get { return m_TuscanyPineTree; } }
        public HarvestDefinition WalnutTree { get { return m_WalnutTree; } }
        public HarvestDefinition WhiteBeechTree { get { return m_WhiteBeechTree; } }
        public HarvestDefinition WillowTree { get { return m_WillowTree; } }
        public HarvestDefinition YewTree { get { return m_YewTree; } }
        public HarvestDefinition YuccaPlant { get { return m_YuccaPlant; } }

        private TreeHarvest()
        {

            #region Harvesting Apple trees
            HarvestDefinition tree = m_AppleTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_AppleTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            HarvestResource[] res;
            HarvestVein[] veins;

            res = new HarvestResource[]
				{
					new HarvestResource( 30.0, 20.0, 120.0, TreeResource.RedApple, typeof( TreeResourceItem ) ),
					new HarvestResource( 50.0, 40.0, 120.0, TreeResource.AppleBark, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 90.0, 0.0, res[0], null ),
					new HarvestVein( 10.0, 0.5, res[1], res[0] )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Apple Tree

            #region Harvesting Ash trees
            tree = m_AshTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_AshTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 60.0, 50.0, 120.0, TreeResource.AshBark, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.BonusResources = new BonusHarvestResource[]
                {
                    new BonusHarvestResource(40.0, 50.0, "", typeof(AshLog))
                };

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Ash Tree

            #region Harvesting Banana trees
            tree = m_BananaTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_BananaTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 30.0, 20.0, 120.0, TreeResource.RipeBanana, typeof( TreeResourceItem ) ),
					new HarvestResource( 50.0, 40.0, 120.0, TreeResource.PalmHusks, typeof( TreeResourceItem ) ),
					new HarvestResource( 70.0, 60.0, 120.0, TreeResource.PalmTreeSap, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 70.0, 0.0, res[0], null ),
					new HarvestVein( 20.0, 0.5, res[1], res[0] ),
					new HarvestVein( 10.0, 0.5, res[2], res[0] )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Banana Tree

            #region Harvesting BlackCherry trees
            tree = m_BlackCherryTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Cooking;

            // Set the list of harvestable tiles
            tree.Tiles = m_BlackCherryTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 60.0, 50.0, 120.0, TreeResource.BlackCherry, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //BlackCherry Tree

            #region Harvesting BlackOak trees
            tree = m_BlackOakTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Cooking;

            // Set the list of harvestable tiles
            tree.Tiles = m_BlackOakTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 60.0, 50.0, 120.0, TreeResource.BlackOakBark, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //BlackOak Tree

            #region Harvesting Cassia trees
            tree = m_CassiaTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Cooking;

            // Set the list of harvestable tiles
            tree.Tiles = m_CassiaTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 60.0, 50.0, 120.0, TreeResource.CassiaBark, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Cassia Tree

            #region Harvesting Cedar trees
            tree = m_CedarTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_CedarTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 60.0, 50.0, 120.0, TreeResource.CedarBark, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Cedar Tree

            #region Harvesting Cherry trees
            tree = m_CherryTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_CherryTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 60.0, 50.0, 120.0, TreeResource.Cherry, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Cherry Tree

            #region Harvesting Coconut Palm trees
            tree = m_CoconutPalm = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_CoconutTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 30.0, 20.0, 120.0, TreeResource.WholeCoconut, typeof( TreeResourceItem ) ),
					new HarvestResource( 50.0, 40.0, 120.0, TreeResource.PalmHusks, typeof( TreeResourceItem ) ),
					new HarvestResource( 70.0, 60.0, 120.0, TreeResource.PalmTreeSap, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 70.0, 0.0, res[0], null ),
					new HarvestVein( 20.0, 0.5, res[1], res[0] ),
					new HarvestVein( 10.0, 0.5, res[2], res[0] )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Coconut Palm Tree

            #region Harvesting Cypress trees
            tree = m_CypressTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Cooking;

            // Set the list of harvestable tiles
            tree.Tiles = m_CypressTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 40.0, 30.0, 120.0, TreeResource.CypressLeaves, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Cypress Tree

            #region Harvesting Date Palm trees
            tree = m_DatePalm = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_DateTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 30.0, 20.0, 120.0, TreeResource.TropicalDates, typeof( TreeResourceItem ) ),
					new HarvestResource( 50.0, 40.0, 120.0, TreeResource.PalmHusks, typeof( TreeResourceItem ) ),
					new HarvestResource( 70.0, 60.0, 120.0, TreeResource.PalmTreeSap, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 70.0, 0.0, res[0], null ),
					new HarvestVein( 20.0, 0.5, res[1], res[0] ),
					new HarvestVein( 10.0, 0.5, res[2], res[0] )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Date Palm Tree

            #region Harvesting Dogwood trees
            tree = m_DogwoodTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_DogwoodTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 50.0, 40.0, 120.0, TreeResource.DogwoodBark, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Dogwood Tree

            #region Harvesting Eucalyptus trees
            tree = m_EucalyptusTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Cooking;

            // Set the list of harvestable tiles
            tree.Tiles = m_EucalyptusTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 40.0, 30.0, 120.0, TreeResource.EucalyptusLeaves, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Eucalyptus Tree

            #region Harvesting Fig trees
            tree = m_FigTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Cooking;

            // Set the list of harvestable tiles
            tree.Tiles = m_FigTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 60.0, 50.0, 120.0, TreeResource.FigFruit, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Fig Tree

            #region Harvesting Hickory trees
            tree = m_HickoryTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_HickoryTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 50.0, 40.0, 120.0, TreeResource.HickoryBark, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Hickory Tree

            #region Harvesting Ironwood trees
            tree = m_IronwoodTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_IronwoodTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 70.0, 60.0, 120.0, TreeResource.IronwoodBark, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Ironwood Tree

            #region Harvesting Juniper trees
            tree = m_JuniperBush = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Cooking;

            // Set the list of harvestable tiles
            tree.Tiles = m_JuniperTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 45.0, 35.0, 120.0, TreeResource.JuniperLeaves, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Juniper Bush

            #region Harvesting Maple trees
            tree = m_MapleTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Cooking;

            // Set the list of harvestable tiles
            tree.Tiles = m_MapleTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 40.0, 30.0, 120.0, TreeResource.MapleTreeSap, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Maple Tree

            #region Harvesting Oak trees
            tree = m_OakTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_OakTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 35.0, 25.0, 120.0, TreeResource.Acorn, typeof( TreeResourceItem ) ),
					new HarvestResource( 55.0, 45.0, 120.0, TreeResource.OakBark, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.BonusResources = new BonusHarvestResource[]
                {
                    new BonusHarvestResource(30.0, 40.0, "", typeof(OakLog))
                };

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Oak Tree

            #region Harvesting Ohii trees
            tree = m_OhiiTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_OhiiTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 55.0, 45.0, 120.0, TreeResource.OhiiRoot, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Ohii Tree

            #region Harvesting Olive trees
            tree = m_OliveTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Cooking;

            // Set the list of harvestable tiles
            tree.Tiles = m_OliveTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 50.0, 40.0, 120.0, TreeResource.BlackOlives, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.BonusResources = new BonusHarvestResource[] 
                { 
                    new BonusHarvestResource(60, 0.25, (int)TreeResource.GreenOlives, typeof( TreeResourceItem ) ) 
                };

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Olive Tree

            #region Harvesting Peach trees
            tree = m_PeachTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_PeachTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 30.0, 20.0, 120.0, TreeResource.HarvestPeach, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Peach Tree

            #region Harvesting Pear trees
            tree = m_PearTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_PearTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 30.0, 20.0, 120.0, TreeResource.GoldenPear, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Pear Tree

            #region Harvesting PipeCactus trees
            tree = m_PipeCactus = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_CactusTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 80.0, 70.0, 120.0, TreeResource.CactusSpine, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Pipe Cactus Tree

            #region Harvesting Plum trees
            tree = m_PlumTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_PlumTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 30.0, 20.0, 120.0, TreeResource.Plum, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Plum Tree

            #region Harvesting Poplar trees
            tree = m_PoplarTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Cooking;

            // Set the list of harvestable tiles
            tree.Tiles = m_PoplarTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 60.0, 50.0, 120.0, TreeResource.TreeSap, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Poplar Tree

            #region Harvesting Sandalwood trees
            tree = m_SandalwoodTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Cooking;

            // Set the list of harvestable tiles
            tree.Tiles = m_SandalwoodTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 40.0, 30.0, 120.0, TreeResource.SandalwoodSap, typeof( TreeResourceItem ) ),
					new HarvestResource( 60.0, 50.0, 120.0, TreeResource.SandalwoodRoot, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 70.0, 0.0, res[0], null ),
					new HarvestVein( 30.0, 0.0, res[1], res[0] )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Sandalwood Tree

            #region Harvesting Spider trees
            tree = m_SpiderTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_SpiderTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 30.0, 20.0, 120.0, TreeResource.SpiderTreeLeaves, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Spider Tree

            #region Harvesting TuscanyPine trees
            tree = m_TuscanyPineTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Cooking;

            // Set the list of harvestable tiles
            tree.Tiles = m_TuscanyPineTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 60.0, 50.0, 120.0, TreeResource.PineTreeSap, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //TuscanyPine Tree

            #region Harvesting Walnut trees
            tree = m_WalnutTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_WalnutTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 35.0, 25.0, 120.0, TreeResource.Walnut, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Walnut Tree

            #region Harvesting WhiteBeech trees
            tree = m_WhiteBeechTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_WhiteBeechTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 50.0, 40.0, 120.0, TreeResource.BeechBark, typeof( TreeResourceItem ) ),
					new HarvestResource( 40.0, 30.0, 120.0, TreeResource.Beechnut, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 60.0, 0.0, res[0], null ),
					new HarvestVein( 40.0, 0.0, res[1], res[0] )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //WhiteBeech Tree

            #region Harvesting Willow trees
            tree = m_WillowTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_WillowTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 30.0, 20.0, 120.0, TreeResource.WillowBark, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Willow Tree

            #region Harvesting Yew trees
            tree = m_YewTree = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Camping;

            // Set the list of harvestable tiles
            tree.Tiles = m_YewTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 60.0, 50.0, 120.0, TreeResource.YewBark, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

            tree.BonusResources = new BonusHarvestResource[]
                {
                    new BonusHarvestResource(40.0, 50.0, "", typeof(YewLog))
                };

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Yew Tree

            #region Harvesting Yucca trees
            tree = m_YuccaPlant = new HarvestDefinition();

            // Resource banks are every 2x3 tiles
            tree.BankWidth = 2;
            tree.BankHeight = 3;

            // Every bank holds from 10 to 24 resources
            tree.MinTotal = 10;
            tree.MaxTotal = 24;

            // A resource bank will respawn its content every 10 to 20 minutes
            tree.MinRespawn = TimeSpan.FromMinutes(10.0);
            tree.MaxRespawn = TimeSpan.FromMinutes(20.0);

            // Skill checking is done on the related Harvesting skill
            tree.Skill = SkillName.Cooking;

            // Set the list of harvestable tiles
            tree.Tiles = m_YuccaTreeTiles;

            // Players must be within 2 tiles to harvest
            tree.MaxRange = 2;

            // Resources per harvest action
            tree.ConsumedPerHarvest = 2;
            tree.ConsumedPerFeluccaHarvest = 2;

            // The harvest effect
            tree.EffectActions = new int[] { 11 };
            tree.EffectSounds = new int[] { 0x125, 0x126 };
            tree.EffectCounts = new int[] { 3 };
            tree.EffectDelay = TimeSpan.FromSeconds(1.0);
            tree.EffectSoundDelay = TimeSpan.FromSeconds(0.9);

            tree.NoResourcesMessage = "There is no resource here to harvest.";
            tree.DoubleHarvestMessage = "[TEST] Double harvest message.";
            tree.TimedOutOfRangeMessage = "You have moved too far away to continue harvesting.";
            tree.OutOfRangeMessage = 500446; // That is too far away.
            tree.FailMessage = "You fail to find any resources to harvest.";
            tree.PackFullMessage = "Your backpack can't hold any more resources!";
            tree.ToolBrokeMessage = 1044038; // You have worn out your tool!

            res = new HarvestResource[]
				{
					new HarvestResource( 75.0, 65.0, 120.0, TreeResource.YuccaRoot, typeof( TreeResourceItem ) ),
					new HarvestResource( 50.0, 40.0, 120.0, TreeResource.PalmHusks, typeof( TreeResourceItem ) ),
					new HarvestResource( 70.0, 60.0, 120.0, TreeResource.PalmTreeSap, typeof( TreeResourceItem ) )
				};

            veins = new HarvestVein[]
				{
					new HarvestVein( 70.0, 0.0, res[0], null ),
					new HarvestVein( 20.0, 0.5, res[1], res[0] ),
					new HarvestVein( 10.0, 0.5, res[2], res[0] )
				};

            tree.Resources = res;
            tree.Veins = veins;

            Definitions.Add(tree);
            #endregion //Yucca Plant
        }

        public override void OnBadHarvestTarget(Mobile from, Item tool, object toHarvest)
        {
            from.SendMessage("You cannot harvest anything there using {0}.", tool.Name);
        }

        public override void SendSuccessTo(Mobile from, Item item, HarvestResource resource)
        {
            from.SendMessage("You harvest {0} {1}", item.Amount.ToString(), item.DefaultName);
        }

        public override void FinishHarvesting(Mobile from, Item tool, HarvestDefinition def, object toHarvest, object locked)
        {
            from.EndAction(locked);

            if (!CheckHarvest(from, tool)) return;

            int tileID;
            Map map;
            Point3D loc;

            if (!GetHarvestDetails(from, tool, toHarvest, out tileID, out map, out loc))
            {
                OnBadHarvestTarget(from, tool, toHarvest);
                return;
            }
            else if (!def.Validate(tileID))
            {
                OnBadHarvestTarget(from, tool, toHarvest);
                return;
            }

            if (!CheckRange(from, tool, def, map, loc, true)) return;
            else if (!CheckResources(from, tool, def, map, loc, false)) return;
            else if (!CheckHarvest(from, tool, def, toHarvest)) return;

            if (SpecialHarvest(from, tool, def, map, loc)) return;

            HarvestBank bank = def.GetBank(map, loc.X, loc.Y);
            if (bank == null) return;

            HarvestVein vein = bank.Vein;
            if (vein == null) return;

            HarvestResource resource = null;
            TreeHarvestTool thtool = null;
            TreeResourceItem trsource = null;
            bool gave = false;
            LokaiSkillName lokaiSkill = LokaiSkillName.TreePicking;
            string toolType = "unknown";
            string resType = "resources";

            if (tool is TreeHarvestTool)
            {
                thtool = tool as TreeHarvestTool;
                switch (thtool.ResourceType)
                {
                    case TreeResourceType.BarkSkin: toolType = "carving"; lokaiSkill = LokaiSkillName.TreeCarving; break;
                    case TreeResourceType.FruitNut: toolType = "picking"; lokaiSkill = LokaiSkillName.TreePicking; break;
                    case TreeResourceType.LeafSpine: toolType = "pinching"; lokaiSkill = LokaiSkillName.TreePicking; break;
                    case TreeResourceType.RootBranch: toolType = "digging"; lokaiSkill = LokaiSkillName.TreeDigging; break;
                    case TreeResourceType.SapJuice: toolType = "sapping"; lokaiSkill = LokaiSkillName.TreeSapping; break;
                }
                bool found = false;
                for (int x = 0; x < def.Resources.Length && !found; x++)
                {
                    object[] obj = new object[] { def.Resources[x].SuccessMessage };
                    Item item = Activator.CreateInstance(def.Resources[x].Types[0], obj) as Item;
                    if (item is TreeResourceItem)
                    {
                        trsource = item as TreeResourceItem;
                        switch (trsource.ResourceType)
                        {
                            case TreeResourceType.BarkSkin: resType = "bark or skin"; break;
                            case TreeResourceType.FruitNut: resType = "fruits or nuts"; break;
                            case TreeResourceType.LeafSpine: resType = "leaves or spines"; break;
                            case TreeResourceType.RootBranch: resType = "roots or branches"; break;
                            case TreeResourceType.SapJuice: resType = "sap or juice"; break;
                        }
                        if (thtool.ResourceType == trsource.ResourceType)
                        {
                            found = true;
                            resource = def.Resources[x];
                        }
                        else
                            trsource.Delete();
                    }
                    else
                        item.Delete();
                }

                if (!found)
                {
                    from.SendMessage("You will not get any {0} with the {1} tool.", resType, toolType);
                    return;
                }
            }
            else
            {
                Console.WriteLine("How did someone try to harvest a tree without a Tree Harvest Tool?");
                return;
            }

            if (trsource != null)
            {
                LokaiSkills skills = LokaiSkillUtilities.XMLGetSkills(from);
                LokaiSkill skil = skills[lokaiSkill];
                SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, skil, 0.0, 100.0);

                if (rating >= SuccessRating.PartialSuccess)
                {
                    if (trsource.Stackable)
                    {
                        int amount = def.ConsumedPerHarvest;
                        int feluccaAmount = def.ConsumedPerFeluccaHarvest;

                        int racialAmount = (int)Math.Ceiling(amount * 1.1);
                        int feluccaRacialAmount = (int)Math.Ceiling(feluccaAmount * 1.1);

                        bool eligableForRacialBonus = (def.RaceBonus && from.Race == Race.Human);
                        bool inFelucca = (map == Map.Felucca);

                        if (eligableForRacialBonus && inFelucca && bank.Current >= feluccaRacialAmount)
                            trsource.Amount = feluccaRacialAmount;
                        else if (inFelucca && bank.Current >= feluccaAmount)
                            trsource.Amount = feluccaAmount;
                        else if (eligableForRacialBonus && bank.Current >= racialAmount)
                            trsource.Amount = racialAmount;
                        else
                            trsource.Amount = amount;
                    }

                    Container pack = from.Backpack;
                    if (pack == null)
                    {
                        from.SendMessage("Why don't you have a pack?");
                        trsource.Delete();
                        return;
                    }
                    Item tap = pack.FindItemByType(typeof(BarrelTap));

                    if (trsource.ResourceType == TreeResourceType.SapJuice && tap == null)
                    {
                        from.SendMessage("You need a barrel tap to sap this tree.");
                        trsource.Delete();
                        return;
                    }
                    if (trsource.ResourceType == TreeResourceType.SapJuice)
                        trsource.Amount = 1;

                    bank.Consume(trsource.Amount, from);

                    if (tool is TreeHarvestTool)
                    {
                        TreeHarvestTool toolWithUses = (TreeHarvestTool)tool;

                        toolWithUses.ShowUsesRemaining = true;

                        if (toolWithUses.UsesRemaining > 0)
                            --toolWithUses.UsesRemaining;

                        if (toolWithUses.UsesRemaining < 1)
                        {
                            tool.Delete();
                            def.SendMessageTo(from, def.ToolBrokeMessage);
                        }
                    }

                    if (trsource.ResourceType != TreeResourceType.SapJuice || pack.ConsumeTotal(typeof(EmptyJar), 1))
                    {
                        if (trsource.ResourceType == TreeResourceType.SapJuice && 0.12 >= Utility.RandomDouble())
                        {
                            from.SendMessage("Your tap broke in the process, and is now gone.");
                            tap.Delete();
                        }

                        if (Give(from, trsource, def.PlaceAtFeetIfFull))
                        {
                            SendSuccessTo(from, trsource, resource);
                            gave = true;
                        }
                        else
                        {
                            SendPackFullTo(from, trsource, def, resource);
                            trsource.Delete();
                            return;
                        }
                    }
                    else
                    {
                        from.SendMessage("You don't have an empty jar to hold the sap, so it was lost!");
                        trsource.Delete();
                        return;
                    }

                    BonusHarvestResource bonus = def.GetBonusResource();

                    if (bonus != null && bonus.Type != null && rating >= SuccessRating.CompleteSuccess)
                    {
                        Item bonusItem;
                        if (bonus.Type == typeof(TreeResource))
                        {
                            object[] obj = new object[] { (TreeResource)bonus.SuccessMessage.Number };
                            bonusItem = Activator.CreateInstance(bonus.Type, obj) as Item;
                        }
                        else
                        {
                            bonusItem = Activator.CreateInstance(bonus.Type) as Item;
                        }

                        if (Give(from, bonusItem, true))
                        {
                            from.SendMessage("You find a bonus resource.");
                            gave = true;
                        }
                        else
                        {
                            bonusItem.Delete();
                        }
                    }
                }
                else
                    trsource.Delete();
            }

            if (gave)
            {
                //do nothing
            }
            else if (trsource == null || trsource.Deleted)
                def.SendMessageTo(from, def.FailMessage);

            OnHarvestFinished(from, tool, def, vein, bank, resource, toHarvest);
        }

        public static TreeResource[] GetResources(int tileID)
        {
            switch (tileID)
            {
                case 0x4CA8:
                case 0x4CAA:
                case 0x4CAB: return new TreeResource[] { TreeResource.RipeBanana, TreeResource.PalmHusks, TreeResource.PalmTreeSap };
                case 0x4C95: return new TreeResource[] { TreeResource.WholeCoconut, TreeResource.PalmHusks, TreeResource.PalmTreeSap };
                case 0x4C96: return new TreeResource[] { TreeResource.TropicalDates, TreeResource.PalmHusks, TreeResource.PalmTreeSap };
                case 0x4CC8: return new TreeResource[] { TreeResource.JuniperLeaves };
                case 0x4CC9: return new TreeResource[] { TreeResource.SpiderTreeLeaves };
                case 0x4CCA: return new TreeResource[] { TreeResource.IronwoodBark };
                case 0x4CCB: return new TreeResource[] { TreeResource.HickoryBark };
                case 0x4CCC: return new TreeResource[] { TreeResource.DogwoodBark };
                case 0x4C9E: return new TreeResource[] { TreeResource.OhiiRoot };
                case 0x4CCD:
                case 0x4CCE:
                case 0x4CCF: return new TreeResource[] { TreeResource.AshBark };
                case 0x4CD0:
                case 0x4CD1:
                case 0x4CD2: return new TreeResource[] { TreeResource.EucalyptusLeaves };
                case 0x4CD3:
                case 0x4CD4:
                case 0x4CD5: return new TreeResource[] { TreeResource.BlackOlives, TreeResource.GreenOlives };
                case 0x4CD6:
                case 0x4CD7:
                case 0x4CD8:
                case 0x4CD9: return new TreeResource[] { TreeResource.CedarBark };
                case 0x5B7E: return new TreeResource[] { TreeResource.PineTreeSap };
                case 0x6471:
                case 0x6472:
                case 0x6473:
                case 0x6474:
                case 0x6475:
                case 0x6476:
                case 0x6477: return new TreeResource[] { TreeResource.Cherry };
                case 0x6478:
                case 0x6479:
                case 0x647A:
                case 0x647B:
                case 0x647C:
                case 0x647D:
                case 0x647E: return new TreeResource[] { TreeResource.MapleTreeSap };
                case 0x4CDA:
                case 0x4CDB:
                case 0x4CDC:
                case 0x4CDD:
                case 0x4CDE:
                case 0x4CDF: return new TreeResource[] { TreeResource.Acorn, TreeResource.OakBark };
                case 0x4CE0:
                case 0x4CE1:
                case 0x4CE2:
                case 0x4CE3:
                case 0x4CE4:
                case 0x4CE5: return new TreeResource[] { TreeResource.Walnut };
                case 0x4CE6:
                case 0x4CE7:
                case 0x4CE8: return new TreeResource[] { TreeResource.WillowBark };
                case 0x4CF8:
                case 0x4CF9:
                case 0x4CFA:
                case 0x4CFB:
                case 0x4CFC:
                case 0x4CFD:
                case 0x4CFE:
                case 0x4CFF:
                case 0x4D00:
                case 0x4D01:
                case 0x4D02:
                case 0x4D03: return new TreeResource[] { TreeResource.CypressLeaves };
                case 0x4D35: return new TreeResource[] { TreeResource.CactusSpine };
                case 0x4D37:
                case 0x4D38: return new TreeResource[] { TreeResource.YuccaRoot, TreeResource.PalmHusks, TreeResource.PalmTreeSap };
                case 0x4D41:
                case 0x4D42:
                case 0x4D43:
                case 0x4D44:
                case 0x4D45:
                case 0x4D46:
                case 0x4D47:
                case 0x4D48:
                case 0x4D49:
                case 0x4D4A:
                case 0x4D4B:
                case 0x4D4C:
                case 0x4D4D:
                case 0x4D4E:
                case 0x4D4F:
                case 0x4D50:
                case 0x4D51:
                case 0x4D52:
                case 0x4D53: return new TreeResource[] { TreeResource.BlackCherry };
                case 0x4D57:
                case 0x4D58:
                case 0x4D59:
                case 0x4D5A:
                case 0x4D5B:
                case 0x4D5C:
                case 0x4D5D:
                case 0x4D5E:
                case 0x4D5F:
                case 0x4D60:
                case 0x4D61:
                case 0x4D62:
                case 0x4D63:
                case 0x4D64:
                case 0x4D65:
                case 0x4D66:
                case 0x4D67:
                case 0x4D68:
                case 0x4D69: return new TreeResource[] { TreeResource.TreeSap };
                case 0x4D6A:
                case 0x4D6B:
                case 0x4D6C:
                case 0x4D6D:
                case 0x4D6E:
                case 0x4D6F:
                case 0x4D70:
                case 0x4D71:
                case 0x4D72:
                case 0x4D73:
                case 0x4D74:
                case 0x4D75:
                case 0x4D76:
                case 0x4D77:
                case 0x4D78:
                case 0x4D79:
                case 0x4D7A:
                case 0x4D7B:
                case 0x4D7C:
                case 0x4D7D:
                case 0x4D7E:
                case 0x4D7F: return new TreeResource[] { TreeResource.FigFruit };
                case 0x4D84:
                case 0x4D85:
                case 0x4D86:
                case 0x4D87:
                case 0x4D88:
                case 0x4D89:
                case 0x4D8A:
                case 0x4D8B:
                case 0x4D8C:
                case 0x4D8D:
                case 0x4D8E:
                case 0x4D8F:
                case 0x4D90: return new TreeResource[] { TreeResource.SandalwoodSap, TreeResource.SandalwoodRoot };
                case 0x4D94:
                case 0x4D95:
                case 0x4D96:
                case 0x4D97:
                case 0x4D98:
                case 0x4D99:
                case 0x4D9A:
                case 0x4D9B:
                case 0x7124: return new TreeResource[] { TreeResource.RedApple, TreeResource.AppleBark };
                case 0x4D9C:
                case 0x4D9D:
                case 0x4D9E:
                case 0x4D9F:
                case 0x4DA0:
                case 0x4DA1:
                case 0x4DA2:
                case 0x4DA3:
                case 0x7123: return new TreeResource[] { TreeResource.HarvestPeach };
                case 0x4DA4:
                case 0x4DA5:
                case 0x4DA6:
                case 0x4DA7:
                case 0x4DA8:
                case 0x4DA9:
                case 0x4DAA:
                case 0x4DAB: return new TreeResource[] { TreeResource.GoldenPear };
                case 0x52B5:
                case 0x52B6:
                case 0x52B7:
                case 0x52B8:
                case 0x52B9:
                case 0x52BA:
                case 0x52BB:
                case 0x52BC:
                case 0x52BD:
                case 0x52BE:
                case 0x52BF:
                case 0x52C0:
                case 0x52C1:
                case 0x52C2:
                case 0x52C3:
                case 0x52C4:
                case 0x52C5:
                case 0x52C6:
                case 0x52C7: return new TreeResource[] { TreeResource.YewBark };
                case 0x66ED:
                case 0x66EE:
                case 0x66EF:
                case 0x66F0:
                case 0x66F1:
                case 0x66F2:
                case 0x66F3:
                case 0x7122: return new TreeResource[] { TreeResource.Plum };
                case 0x709C:
                case 0x709D:
                case 0x709E:
                case 0x709F:
                case 0x70A0:
                case 0x70A1:
                case 0x70A2:
                case 0x70A3:
                case 0x70A4:
                case 0x70A5:
                case 0x70A6:
                case 0x70A7:
                case 0x70A8:
                case 0x70A9:
                case 0x70AA:
                case 0x70AB:
                case 0x70AC:
                case 0x70AD:
                case 0x70AE:
                case 0x70AF:
                case 0x70B0:
                case 0x70B1:
                case 0x70B2:
                case 0x70B3:
                case 0x70B4:
                case 0x70B5:
                case 0x70B6:
                case 0x70B7:
                case 0x70B8:
                case 0x70B9:
                case 0x70BA:
                case 0x70BB:
                case 0x70BC: return new TreeResource[] { TreeResource.BlackOakBark };
                case 0x70D4:
                case 0x70D5:
                case 0x70D6:
                case 0x70D7:
                case 0x70D8:
                case 0x70D9:
                case 0x70DA:
                case 0x70DB:
                case 0x70DC:
                case 0x70DD:
                case 0x70DE: return new TreeResource[] { TreeResource.Beechnut, TreeResource.BeechBark };
                case 0x70BD:
                case 0x70BE:
                case 0x70BF:
                case 0x70C0:
                case 0x70C1:
                case 0x70C2:
                case 0x70C3:
                case 0x70C4:
                case 0x70C5:
                case 0x70C6:
                case 0x70C7:
                case 0x70C8:
                case 0x70C9:
                case 0x70CA:
                case 0x70CB:
                case 0x70CC:
                case 0x70CD:
                case 0x70CE:
                case 0x70CF:
                case 0x70D0:
                case 0x70D1:
                case 0x70D2:
                case 0x70D3: return new TreeResource[] { TreeResource.CassiaBark };
            }
            return new TreeResource[0];
        }

        #region Harvest Tree Tiles
        private static int[] m_AppleTreeTiles = new int[]
			{
                0x4D94, 0x4D95, 0x4D96, 0x4D97, 0x4D98, 0x4D99, 0x4D9A, 0x4D9B, 0x7124, //Apple Tree
			};

        private static int[] m_AshTreeTiles = new int[]
			{
                0x4CCD, 0x4CCE, 0x4CCF, //Ash
			};

        private static int[] m_BananaTreeTiles = new int[]
			{
                0x4CA8, 0x4CAA, 0x4CAB, //Banana Tree
			};

        private static int[] m_BlackCherryTreeTiles = new int[]
			{
				0x4D41, 0x4D42, 0x4D43, 0x4D44, 0x4D45, 0x4D46, 
				0x4D47, 0x4D48, 0x4D49, 0x4D4A, 0x4D4B, 0x4D4C, 
				0x4D4D, 0x4D4E, 0x4D4F, 0x4D50, 0x4D51, 0x4D52, 0x4D53, //BlackCherry
			};

        private static int[] m_BlackOakTreeTiles = new int[]
			{
                0x709C, 0x709D, 0x709E, 0x709F, 0x70A0, 0x70A1, 0x70A2, 0x70A3, 
                0x70A4, 0x70A5, 0x70A6, 0x70A7, 0x70A8, 0x70A9, 0x70AA, 0x70AB,
                0x70AC, 0x70AD, 0x70AE, 0x70AF, 0x70B0, 0x70B1, 0x70B2, 0x70B3, 
                0x70B4, 0x70B5, 0x70B6, 0x70B7, 0x70B8, 0x70B9, 0x70BA, 0x70BB, 0x70BC, //BlackOak
			};

        private static int[] m_CactusTreeTiles = new int[]
			{
                0x4D35, //Pipe Cactus
			};

        private static int[] m_CassiaTreeTiles = new int[]
			{
                0x70BD, 0x70BE, 0x70BF, 0x70C0, 0x70C1, 0x70C2, 0x70C3, 
                0x70C4, 0x70C5, 0x70C6, 0x70C7, 0x70C8, 0x70C9, 0x70CA, 0x70CB,
                0x70CC, 0x70CD, 0x70CE, 0x70CF, 0x70D0, 0x70D1, 0x70D2, 0x70D3, //Cassia
			};

        private static int[] m_CedarTreeTiles = new int[]
			{
                0x4CD6, 0x4CD7, 0x4CD8, 0x4CD9, //Cedar
			};

        private static int[] m_CherryTreeTiles = new int[]
			{
				0x6471, 0x6472, 0x6473, 
                0x6474, 0x6475, 0x6476, 0x6477, //Cherry
			};

        private static int[] m_CoconutTreeTiles = new int[]
			{
                0x4C95, //Coconut Palm
			};

        private static int[] m_CypressTreeTiles = new int[]
			{
                0x4CF8, 0x4CF9, 0x4CFA, 0x4CFB, 0x4CFC, 0x4CFD, 
                0x4CFE, 0x4CFF, 0x4D00, 0x4D01, 0x4D02, 0x4D03, //Cypress
			};

        private static int[] m_DateTreeTiles = new int[]
			{
                0x4C96, //Date Palm
			};

        private static int[] m_DogwoodTreeTiles = new int[]
			{
                0x4CCC, //Dogwood
			};

        private static int[] m_EucalyptusTreeTiles = new int[]
			{
                0x4CD0, 0x4CD1, 0x4CD2, //Eucalyptus
			};

        private static int[] m_FigTreeTiles = new int[]
			{
                0x4D6A, 0x4D6B, 0x4D6C, 0x4D6D, 0x4D6E, 0x4D6F, 0x4D70, 
                0x4D71, 0x4D72, 0x4D73, 0x4D74, 0x4D75, 0x4D76, 0x4D77, 
                0x4D78, 0x4D79, 0x4D7A, 0x4D7B, 0x4D7C, 0x4D7D, 0x4D7E, 0x4D7F, //Fig
			};

        private static int[] m_HickoryTreeTiles = new int[]
			{
                0x4CCB, //Hickory
			};

        private static int[] m_IronwoodTreeTiles = new int[]
			{
				0x4CCA, //Ironwood
			};

        private static int[] m_JuniperTreeTiles = new int[]
			{
                0x4CC8, //Juniper Bush
			};

        private static int[] m_MapleTreeTiles = new int[]
			{
				0x6478, 0x6479, 0x647A, 
                0x647B, 0x647C, 0x647D, 0x647E, //Maple
			};

        private static int[] m_OhiiTreeTiles = new int[]
			{
                0x4C9E, //Ohii
			};

        private static int[] m_OliveTreeTiles = new int[]
			{
                0x4CD3, 0x4CD4, 0x4CD5, //Olive
			};

        private static int[] m_OakTreeTiles = new int[]
			{
				0x4CDA, 0x4CDB, 0x4CDC, 
                0x4CDD, 0x4CDE, 0x4CDF, //Oak
			};

        private static int[] m_PeachTreeTiles = new int[]
			{
                0x4D9C, 0x4D9D, 0x4D9E, 0x4D9F, 0x4DA0, 0x4DA1, 0x4DA2, 0x4DA3, 0x7123, //Peach Tree
			};

        private static int[] m_PearTreeTiles = new int[]
			{
                0x4DA4, 0x4DA5, 0x4DA6, 0x4DA7, 0x4DA8, 0x4DA9, 0x4DAA, 0x4DAB, //Pear Tree
			};

        private static int[] m_PlumTreeTiles = new int[]
			{
                0x66ED, 0x66EE, 0x66EF, 0x66F0, 0x66F1, 0x66F2, 0x66F3, 0x7122, //Plum
			};

        private static int[] m_PoplarTreeTiles = new int[]
			{
				0x4D57, 0x4D58, 0x4D59, 0x4D5A, 0x4D5B, 0x4D5C, 
				0x4D5D, 0x4D5E, 0x4D5F, 0x4D60, 0x4D61, 0x4D62, 
                0x4D63, 0x4D64, 0x4D65, 0x4D66, 0x4D67, 0x4D68, 0x4D69, //Poplar
			};

        private static int[] m_SandalwoodTreeTiles = new int[]
			{
                0x4D84, 0x4D85, 0x4D86, 0x4D87, 0x4D88, 0x4D89, 
				0x4D8A, 0x4D8B, 0x4D8C, 0x4D8D, 0x4D8E, 0x4D8F, 0x4D90, //Sandalwood
			};

        private static int[] m_SpiderTreeTiles = new int[]
			{
                0x4CC9, //Spider Tree
			};

        private static int[] m_TuscanyPineTreeTiles = new int[]
			{
                0x5B7E, //Tuscany Pine
			};

        private static int[] m_WalnutTreeTiles = new int[]
			{
                0x4CE0, 0x4CE1, 0x4CE2, 
                0x4CE3, 0x4CE4, 0x4CE5, //Walnut
			};

        private static int[] m_WhiteBeechTreeTiles = new int[]
			{
                0x70D4, 0x70D5, 0x70D6, 0x70D7, 0x70D8, 
                0x70D9, 0x70DA, 0x70DB, 0x70DC, 0x70DD, 0x70DE, //WhiteBeech
			};

        private static int[] m_WillowTreeTiles = new int[]
			{
                0x4CE6, 0x4CE7, 0x4CE8, //Willow
			};

        private static int[] m_YewTreeTiles = new int[]
			{
				0x52B5, 0x52B6, 0x52B7, 0x52B8, 0x52B9, 0x52BA, 
				0x52BB, 0x52BC, 0x52BD, 0x52BE, 0x52BF, 0x52C0, 
				0x52C1, 0x52C2, 0x52C3, 0x52C4, 0x52C5, 0x52C6, 0x52C7, //Yew
			};

        private static int[] m_YuccaTreeTiles = new int[]
			{
                0x4D37, 0x4D38, //Yucca
			};
        #endregion

        #region GraveTiles
        private static int[] m_GraveTiles = new int[]
			{
                //Graves, coffins, bones, body parts, etc, the dead stuff :)
                0x4ECA, 0x4ECB, 0x4ECC, 0x4ECD, 0x4ECE, 0x4ECF, 0x4ED0, 0x4ED1, 0x4ED2, 
                0x4ED4, 0x4ED5, 0x4ED6, 0x4ED7, 0x4ED8, 0x4ED9, 0x4EDA, 0x4EDB, 0x4EDC, 
                0x4EDD, 0x4EDE, 0x4ED3, 0x4EDF, 0x4EE0, 0x4EE1, 0x4EE2, 0x4EE8, 0x5165, 
                0x5166, 0x5167, 0x5168, 0x5169, 0x516A, 0x516B, 0x516C, 0x516D, 
                0x516E, 0x516F, 0x5170, 0x5171, 0x5172, 0x5173, 0x5174, 0x5175, 
                0x5176, 0x5177, 0x5178, 0x5179, 0x517A, 0x517B, 0x517C, 0x517D, 
                0x517E, 0x517F, 0x5180, 0x5181, 0x5182, 0x5183, 0x5184, 0x5C22, 
                0x5C23, 0x5C24, 0x5C25, 0x5C26, 0x5C27, 0x5C28, 0x5C29, 0x5C2A, 
                0x5C2B, 0x5C2C, 0x5C2D, 0x5C2E, 0x5C2F, 0x5C30, 0x5C31, 0x5C32, 
                0x5C33, 0x5C34, 0x5C35, 0x5C36, 0x5C37, 0x5C38, 0x5C39, 0x5C3A, 
                0x5C3B, 0x5C3C, 0x5C3D, 0x5C3E, 0x5C3F, 0x5C40, 0x5C41, 0x5C42, 
                0x5C43, 0x5C44, 0x5C45, 0x5C46, 0x5C47, 0x5C48, 0x5C49, 0x5C4A, 
                0x5C4B, 0x5C4C, 0x5C4D, 0x5C4E, 0x5C4F, 0x5C50, 0x5C51, 0x5C52, 
                0x5C53, 0x5C54, 0x5C55, 0x5C56, 0x5C57, 0x5C58, 0x5C59, 0x5C5A, 
                0x5C5B, 0x5C5C, 0x5C5D, 0x5C5E, 0x5C5F, 0x6FF9, 0x6FFA, 0x7048, 
                0x7049, 0x704A, 0x704B, 0x5C60, 0x5C61, 0x5C62, 0x5C63, 0x5C64, 
                0x5C65, 0x5C66, 0x5C67, 0x5C68, 0x5C69, 0x5C6A, 0x5C6B, 0x5C6C, 
                0x5C6D, 0x5C6E, 0x5C6F, 0x5C70, 0x5C71, 0x5C72, 0x5C73, 0x5C74, 
                0x5C75, 0x5C76, 0x5C77, 0x5C78, 0x5C79, 0x5C7A, 0x5C7B, 0x5C7C, 
                0x5C7D, 0x5C7E, 0x5C7F, 0x5C80, 0x5C81, 0x5C82, 0x5C83, 0x5C84, 
                0x5C85, 0x5C86, 0x5C87, 0x5C88, 0x5C89, 0x5C8A, 0x5C8B, 0x5C8C, 
                0x5C8D, 0x5C8E, 0x5C8F, 0x5C90, 0x5C91, 0x5C92, 0x5C93, 0x5C94, 
                0x5C95, 0x5C96, 0x5C97, 0x5C98, 0x5C99, 0x5C9A, 0x5C9B, 0x5C9C, 
                0x5C9D, 0x5C9E, 0x5C9F, 0x5CA0, 0x5CA1, 0x5CA2, 0x5CA3, 0x5CA4, 
                0x5CA5, 0x5CA6, 0x5CA7, 0x5CA8, 0x5CA9, 0x5CAA, 0x5CAB, 0x5CAC, 
                0x5CAD, 0x5CAE, 0x5CAF, 0x5CB0, 0x5CB1, 0x5CB2, 0x5CB3, 0x5CB4, 
                0x5CB5, 0x5CB6, 0x5CB7, 0x5CB8, 0x5CB9, 0x5CBA, 0x5CBB, 0x5CBC, 
                0x5CBD, 0x5CBE, 0x5CBF, 0x4F7E, 0x5B09, 0x5B0A, 0x5B0B, 0x5B0C, 
                0x5B0D, 0x5B0E, 0x5B0F, 0x5B10, 0x5B11, 0x5B12, 0x5B13, 0x5B14, 
                0x5B15, 0x5B16, 0x5B17, 0x5B18, 0x5B19, 0x5B1A, 0x5B1B, 0x5B1C, 
                0x5B1D, 0x5B1E, 0x5AD8, 0x5AD9, 0x5ADA, 0x5ADB, 0x5ADC, 0x5ADD, 
                0x5ADE, 0x5ADF, 0x5AE0, 0x5AE1, 0x5AE2, 0x5AE3, 0x5AE4, 0x5853, 
                0x5854, 0x5855, 0x5856, 0x5857, 0x5858, 0x5859, 0x585A, 0x61FC, 
                0x6203, 0x6204, 0x624E, 0x624F, 0x6250, 0x6251, 0x5CDD, 0x5CDE, 
                0x5CDF, 0x5CE0, 0x5CE1, 0x5CE2, 0x5CE3, 0x5CE4, 0x5CE5, 0x5CE6, 
                0x5CE7, 0x5CE8, 0x5CE9, 0x5CEA, 0x5CEB, 0x5CEC, 0x5CED, 0x5CEE, 
                0x5CEF, 0x5CF0, 0x5D9F, 0x5DA0, 0x5DA1, 0x5DA2, 0x5DA3, 0x5DA4, 
                0x5D4C, 0x5D4D, 0x5D4E, 0x5D4F, 0x5D50, 0x5D51, 0x5D52, 0x5D53, 
                0x5D54, 0x5D55, 0x5D56, 0x5D57, 0x5D58, 0x5D59, 0x5D5A, 0x5D5B, 
                0x5D5C, 0x5D5D, 0x5D5E, 0x5D5F, 0x5D60, 0x5D61, 0x5D62, 0x5D63, 
                0x5D64, 0x5D65, 0x5D66, 0x5D67, 0x5D68, 0x5D69, 0x5D6A, 0x5D6B, 
                0x5D6C, 0x5D6D, 0x5D6E, 0x5D6F, 0x5D70, 0x5D71, 0x5D72, 0x5D73, 
                0x5D74, 0x5D75, 0x5D76, 0x5D77, 0x5D78, 0x5D79, 0x5D7A, 0x5D7B, 
                0x5D7C, 0x5D7D, 0x5D7E, 0x5D7F, 0x5D80, 0x5D81, 0x5D82, 0x5D83, 
                0x5D84, 0x5D85, 0x5D86, 0x5D87, 0x5D88, 0x5D89, 0x5D8E, 0x5D8F, 
                0x5D90, 0x5D91, 0x5DAE, 0x5DAF, 0x5DB0, 0x5DB1, 0x5DB2
			};
        #endregion

        #region SearchTiles
        private static int[] m_SearchTiles = new int[]  // int lists of harvestable tiles.  
            //LootTiles are the index number of the tile i.e. the 0x0000 number plus 0x4000  
            //example: 0x9B0 has an index of 0x49B0  It is accesed this way by the harvest system core files.
			{
            //Valuables, Hiding spots, cave steps
                //Coins
                0x4EEA, 0x4EEB, 0x4EEC, 0x4EED, 0x4EEE, 0x4EEF, 0x4EF0, 0x4EF1, 
                0x4EF2, 
                //Cave Steps 
                0x479D, 0x479E, 0x479F, 0x47B0, 0x47B1, 0x47B2, 0x47B7, 0x47B8, 
                0x47B9, 
                //Gems 
                0x4F0F, 0x4F10, 0x4F11, 0x4F12, 0x4F13, 0x4F14, 0x4F15, 0x4F16, 
                0x4F17, 0x4F18, 0x4F19, 0x4F1A, 0x4F1B, 0x4F1C, 0x4F1D, 0x4F1E, 
                0x4F1F, 0x4F20, 0x4F21, 0x4F22, 0x4F23, 0x4F24, 0x4F25, 0x4F26, 
                0x4F27, 0x4F28, 0x4F29, 0x4F2A, 0x4F2B, 0x4F2C, 0x4F2D, 0x4F2E, 
                0x4F2F, 0x4F30, 0x51C0, 0x51C1, 0x51C2, 0x51C3, 0x51C4, 0x51C5, 
                //Treasure 
                0x5B3F, 0x5B40, 0x5B41, 0x5B42, 0x5B43, 0x5B44, 0x5B45, 0x5B46, 
                0x5B47, 0x5B48, 0x5B4A, 0x5B4B, 0x5B4C, 0x5B4D, 0x5B4E, 0x5B4F, 
                0x5B50, 0x5B51, 0x5B53, 0x5B54, 0x5B55, 0x5B56, 0x5B57, 0x5B58, 
                0x5B59, 0x5B5A, 0x5B5B, 0x5B5C, 0x5B5D, 0x5B5F, 0x5B60, 0x5B61, 
                0x5B62, 0x5B63, 0x5B64, 0x5B65, 0x5B66, 0x5B67, 0x5B68, 0x5B69, 
                0x5B6A, 
                //Wall Cracks 
                0x5B6B, 0x5B6C, 0x5B6D, 0x5B6E, 0x5B6F, 
                //Floor Cracks
                0x5B01, 0x5B02, 0x5B03, 0x5B04, 0x5B05, 0x5B06, 0x5B07, 0x5B08, 
                //Refuse 
                0x5B9F, 0x5BA0, 0x5BA1, 0x5BA2, 0x5BA3, 0x5BA4, 0x5BA5, 0x5BA6, 
                0x5BA7, 0x5BA8, 0x5BA9, 0x5BAA, 0x5BAB, 0x5BAC, 0x5BAD, 0x5BAE, 
                0x5BAF, 0x5BB0, 0x5BB1, 0x5BB2, 0x5BB4, 0x5BB5, 0x5BB6, 0x5BB7, 
                0x5BB8, 0x5BB9, 0x5BBA, 0x5BBB, 0x5BBC, 0x5BBD, 
                //Holes 
                0x5B71, 0x70E7, 0x70E8, 0x70E9, 0x70EA, 0x70EB, 0x70EC, 0x70ED, 
                0x70EE, 0x70EF, 0x70F0, 0x70F1, 0x70F2, 0x70F3, 0x70F4, 0x70F5, 
                0x70F6, 0x70F7, 0x70F8, 0x70F9, 0x70FA, 0x70FB, 0x70FC, 0x70FD, 
                0x70FE, 0x70FF, 0x7100, 0x7101, 0x7102, 0x7103, 0x7104, 0x7105, 
                0x7106, 0x7107, 0x7108, 0x7109, 0x710A
			};
        #endregion

        #region Search Tiles Separated
        private static int[] m_CoinTiles = new int[]
			{
                //Coins
                0x4EEA, 0x4EEB, 0x4EEC, 0x4EED, 0x4EEE, 0x4EEF, 0x4EF0, 0x4EF1, 
                0x4EF2, 
            };

        private static int[] m_CaveStepTiles = new int[]
			{
                //Cave Steps 
                0x479D, 0x479E, 0x479F, 0x47B0, 0x47B1, 0x47B2, 0x47B7, 0x47B8, 
                0x47B9, 
            };

        private static int[] m_GemTiles = new int[]
			{
                //Gems 
                0x4F0F, 0x4F10, 0x4F11, 0x4F12, 0x4F13, 0x4F14, 0x4F15, 0x4F16, 
                0x4F17, 0x4F18, 0x4F19, 0x4F1A, 0x4F1B, 0x4F1C, 0x4F1D, 0x4F1E, 
                0x4F1F, 0x4F20, 0x4F21, 0x4F22, 0x4F23, 0x4F24, 0x4F25, 0x4F26, 
                0x4F27, 0x4F28, 0x4F29, 0x4F2A, 0x4F2B, 0x4F2C, 0x4F2D, 0x4F2E, 
                0x4F2F, 0x4F30, 0x51C0, 0x51C1, 0x51C2, 0x51C3, 0x51C4, 0x51C5, 
            };

        private static int[] m_TreasureTiles = new int[]
			{
                //Treasure 
                0x5B3F, 0x5B40, 0x5B41, 0x5B42, 0x5B43, 0x5B44, 0x5B45, 0x5B46, 
                0x5B47, 0x5B48, 0x5B4A, 0x5B4B, 0x5B4C, 0x5B4D, 0x5B4E, 0x5B4F, 
                0x5B50, 0x5B51, 0x5B53, 0x5B54, 0x5B55, 0x5B56, 0x5B57, 0x5B58, 
                0x5B59, 0x5B5A, 0x5B5B, 0x5B5C, 0x5B5D, 0x5B5F, 0x5B60, 0x5B61, 
                0x5B62, 0x5B63, 0x5B64, 0x5B65, 0x5B66, 0x5B67, 0x5B68, 0x5B69, 
                0x5B6A, 
            };

        private static int[] m_WallCrackTiles = new int[]
			{
                //Wall Cracks 
                0x5B6B, 0x5B6C, 0x5B6D, 0x5B6E, 0x5B6F, 
            };

        private static int[] m_FloorCrackTiles = new int[]
			{
                //Floor Cracks
                0x5B01, 0x5B02, 0x5B03, 0x5B04, 0x5B05, 0x5B06, 0x5B07, 0x5B08, 
            };

        private static int[] m_RefuseTiles = new int[]
			{
                //Refuse 
                0x5B9F, 0x5BA0, 0x5BA1, 0x5BA2, 0x5BA3, 0x5BA4, 0x5BA5, 0x5BA6, 
                0x5BA7, 0x5BA8, 0x5BA9, 0x5BAA, 0x5BAB, 0x5BAC, 0x5BAD, 0x5BAE, 
                0x5BAF, 0x5BB0, 0x5BB1, 0x5BB2, 0x5BB4, 0x5BB5, 0x5BB6, 0x5BB7, 
                0x5BB8, 0x5BB9, 0x5BBA, 0x5BBB, 0x5BBC, 0x5BBD, 
            };

        private static int[] m_HoleTiles = new int[]
			{
                //Holes 
                0x5B71, 0x70E7, 0x70E8, 0x70E9, 0x70EA, 0x70EB, 0x70EC, 0x70ED, 
                0x70EE, 0x70EF, 0x70F0, 0x70F1, 0x70F2, 0x70F3, 0x70F4, 0x70F5, 
                0x70F6, 0x70F7, 0x70F8, 0x70F9, 0x70FA, 0x70FB, 0x70FC, 0x70FD, 
                0x70FE, 0x70FF, 0x7100, 0x7101, 0x7102, 0x7103, 0x7104, 0x7105, 
                0x7106, 0x7107, 0x7108, 0x7109, 0x710A
			};
        #endregion

        private static int[] m_PlowedTiles = new int[]
			{
                0x4009,  0x400A,  0x400B,  0x400C,  0x400D,  0x400E,  
				0x400F,  0x4010,  0x4011,  0x4012,  0x4013,  0x4014,  0x4015,
            };

        private static int[] m_CaveWallTiles = new int[]
			{
                0x6F62,  0x6F63,  0x6F64,  0x6F65,  0x6F66,  0x6F67,  0x6F68,  0x6F69,  0x6F6A,  0x6F6B,  
                0x6F6C,  0x6F6D,  0x6F6E,  0x6F6F,  0x6F70,  0x6F71,  0x6F72,  0x6F73,  0x6F74,  0x6F75, 
                0x6F76,  0x6F77,  0x6F78,  0x6F79,  0x6F7A,  0x6F7B,  0x6F7C,  0x6F7D,  0x6F7E,  0x6F7F,  
                0x6F80,  0x6F81,  0x6F82,  0x6F83,  0x6F84,  0x6F85,  0x6F86,  0x6F87,  0x6F88,  0x6F89, 
                0x6F8A,  0x6F8B,  0x6F8C,  0x6F8D,  0x6F8E,  0x6F8F,  0x6F90,  0x6F91,  0x6F92,  0x6F93, 
                0x6F94,  0x6F95,  0x6F96,  0x6F97,  0x6F98,  0x6F99,  0x6F9A,  0x6F9B,  0x6F9C,  0x6F9D,  
                0x6F9E,  0x6F9F,  0x6FA0,  0x6FA1,  0x6FA2,  0x6FA3,  0x6FA4,  0x6FA5,  0x6FA6,  0x6FA7, 
                0x6FA8,  0x6FA9,  0x6FAA,  0x6FAB,  0x6FAC,  0x6FAD,  0x6FAE,  0x6FAF,  0x6FB0,  0x6FB1,  
                0x6FB2,  0x6FB3,  0x6FB4,  0x6FB5, 
            };

        private static int[] m_SwampTiles = new int[]
			{
                0x49C4,  0x49C5,  0x49C6,  0x49C7,  0x49C8,  0x49C9,  0x49CA,  0x49CB,  0x49CC,  0x49CD, 
                0x49CE,  0x49CF,  0x49D0,  0x49D1,  0x49D2,  0x49D3,  0x49D4,  0x49D5,  0x49D6,  0x49D7,  
                0x49D8,  0x49D9,  0x49DA,  0x49DB,  0x49DC,  0x49DD,  0x49DE,  0x49DF,  0x49E0,  0x49E1, 
                0x49E2,  0x49E3,  0x49E4,  0x49E5,  0x49E6,  0x49E7,  0x49E8,  0x49E9,  0x49EA,  0x49EB, 

                0x7AF0,  0x7AF1,  0x7AF2,  0x7AF3,  0x7AF4,  0x7AF5,  0x7AF6,  0x7AF7,  0x7AF8, 
                0x7D65,  0x7DC0,  0x7DC1,  0x7DC2,  0x7DC3,  0x7DC4,  0x7DC5,  0x7DC6,  0x7DC7,  
                0x7DC8,  0x7DC9,  0x7DCA,  0x7DCB,  0x7DCC,  0x7DCD,  0x7DCE,  0x7DCF,  0x7DD0,  
                0x7DD1,  0x7DD2,  0x7DD3,  0x7DD4,  0x7DD5,  0x7DD6,  0x7DD7,  0x7DD8,  0x7DD9,  
                0x7DDA,  0x7DDB,  0x7DDC,  0x7DDD,  0x7DDE,  0x7DDF,  0x7DE0,  0x7DE1,  0x7DE2, 
                0x7DE3,  0x7DE4,  0x7DE5,  0x7DE6,  0x7DE7,  0x7DE8,  0x7DE9,  0x7DEA,  0x7DEF,  
                0x7DF0,  0x7DF1,  0x7EF0, 
            };

        private static int[] m_ForestTiles = new int[]
			{
                0x40C4,  0x40C5,  0x40C6,  0x40C7,  0x40C8,  0x40C9,  0x40CA,  0x40CB,  0x40CC,  0x40CD,  0x40CE,  0x40CF, 
                0x40D0,  0x40D1,  0x40D2,  0x40D3,  0x40D4,  0x40D5,  0x40D6,  0x40D7,  0x40EC,  0x40ED,  0x40EE,  0x40EF, 
                0x40F0,  0x40F1,  0x40F2,  0x40F3,  0x40F8,  0x40F9,  0x40FA,  0x40FB,  0x415D,  0x415E,  0x415F,  0x4160, 
                0x4161,  0x4162,  0x4163,  0x4164,  0x4165,  0x4166,  0x4167,  0x4168,  0x4324,  0x4325,  0x4326,  0x4327, 
                0x4328,  0x4329,  0x432A,  0x432B,  

                0x45F1,  0x45F2,  0x45F3,  0x45F4,  0x45F5,  0x45F6,  0x45F7,  0x45F8,  0x45F9,  0x45FA,  
                0x45FB,  0x45FC,  0x45FD,  0x45FE,  0x45FF,  0x4600,  0x4601,  0x4602,  0x4603,  0x4604,  
                0x4605,  0x4606,  0x4607,  0x4608,  0x4609,  0x460A,  0x460B,  0x460C,  0x460D,  0x460E,  
                0x460F,  0x4610,  0x4611,  0x4612,  0x4613,  0x4614,  0x4653,  0x4654,  0x4655,  0x4656, 
                0x465B,  0x465C,  0x465D,  0x465E,  0x465F,  0x4660,  0x4661,  0x4662,  0x466B,  0x466C, 
                0x466D,  0x466E,  0x46AF,  0x46B0,  0x46B1,  0x46B2,  0x46B3,  0x46B4,  0x46BB,  0x46BC,  
                0x46BD,  0x46BE,  0x4709,  0x470A,  0x470B,  0x470C,  0x4715,  0x4716,  0x4717,  0x4718,
                0x4719,  0x471A,  0x471B,  0x471C, 
            };

        private static int[] m_JungleTiles = new int[]
			{
                0x40AC,  0x40AD,  0x40AE,  0x40AF,  0x40B0,  0x40B1,  0x40B2,  0x40B3,  0x40B4,  0x40B5,  0x40B6,  0x40B7, 
                0x40B8,  0x40B9,  0x40BA,  0x40BB,  0x40BC,  0x40BD,  0x40BE,  0x40BF,  0x40FC,  0x40FD,  0x40FE,  0x40FF, 
                0x4100,  0x4101,  0x4102,  0x4103,  0x4108,  0x4109,  0x410A,  0x410B,  0x41F0,  0x41F1,  0x41F2,  0x41F3, 
                0x426E,  0x426F,  0x4270,  0x4271,  0x4276,  0x4277,  0x4278,  0x4279,  0x427A,  0x427B,  0x427C,  0x427D, 
                0x4286,  0x4287,  0x4288,  0x4289,  0x4292,  0x4293,  0x4294,  0x4295,

                0x4581,  0x4582,  0x4583,  0x4584,  0x4585,  0x4586,  0x4587,  0x4588,  0x4589,  0x458A, 
                0x458D,  0x458E,  0x458F,  0x4590,  0x459F,  0x45A0,  0x45A1,  0x45A2,  0x45A3,  0x45A4, 
                0x45A5,  0x45A6,  0x45B3,  0x45B4,  0x45B5,  0x45B6,  0x45B7,  0x45B8,  0x45B9,  0x45BA, 
                0x4615,  0x4616,  0x4617,  0x4618,  0x4733,  0x4734,  0x4735,  0x4736,  0x4737,  0x4738, 
                0x4739,  0x473A,  0x473F,  0x4740,  0x4741,  0x4742, 
            };

        private static int[] m_SnowTiles = new int[]
			{
                0x410C,  0x410D,  0x410E,  0x410F,  0x4110,  0x4111,  0x4112,  0x4113,  0x4114,  0x4115,  0x4116,  0x4117, 
                0x4118,  0x4119,  0x411A,  0x411B,  0x411C,  0x411D,  0x412E,  0x412F,  0x4130,  0x4131,  0x4179,  0x417A, 
                0x417B,  0x417C,  0x417D,  0x417E,  0x417F,  0x4180,  0x4181,  0x4182,  0x4183,  0x4184,  0x4185,  0x4186, 
                0x4187,  0x4188,  0x4189,  0x418A,  0x4385,  0x4386,  0x4387,  0x4388,  0x4389,  0x438A,  0x438B,  0x438C, 
                0x4391,  0x4392,  0x4393,  0x4394,  0x439D,  0x439E,  0x439F,  0x43A0,  0x43A1,  0x43A2,  0x43A3,  0x43A4, 
                0x43A9,  0x43AA,  0x43AB,  0x43AC, 

                0x45BF,  0x45C0,  0x45C1,  0x45C2,  0x45C3,  0x45C4,  0x45C5,  0x45C6,  0x45C7,  0x45C8, 
                0x45C9,  0x45CA,  0x45CB,  0x45CC,  0x45CD,  0x45CE,  0x45CF,  0x45D0,  0x45D1,  0x45D2, 
                0x45D3,  0x45D4,  0x45D5,  0x45D6,  0x45DF,  0x45E0,  0x45E1,  0x45E2,  0x4751,  0x4752, 
                0x4753,  0x4754,  0x4755,  0x4756,  0x4757,  0x4758,  0x475D,  0x475E,  0x475F,  0x4760, 
                0x476D,  0x476E,  0x476F,  0x4770,  0x4771,  0x4772,  0x4773, 
            };
    }
}
