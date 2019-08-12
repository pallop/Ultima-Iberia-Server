/* Created by Hammerhand */

using System;
using Server;
using Server.Items;

namespace Server.Engines.Craft
{

    public class DefGemCraft : CraftSystem
    {
        public override SkillName MainSkill
        {
            get { return SkillName.Blacksmith; }
        }

        public override int GumpTitleNumber
        {
            get { return 0; }
        }
        public override string GumpTitleString
        {
            get { return "<basefont color=white><CENTER>Gem Crafting Menu</CENTER></basefont>"; }
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefGemCraft();

                return m_CraftSystem;
            }
        }

        public override CraftECA ECA { get { return CraftECA.ChanceMinusSixtyToFourtyFive; } }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.0; // 0%
        }

        private DefGemCraft()
            : base(1, 1, 1.25)// base( 1, 1, 3.0 )
        {
        }

        private static Type typeofAnvil = typeof(AnvilAttribute);
        private static Type typeofForge = typeof(ForgeAttribute);

        public static void CheckAnvilAndForge(Mobile from, int range, out bool anvil, out bool forge)
        {
            anvil = false;
            forge = false;

            Map map = from.Map;

            if (map == null)
                return;

            IPooledEnumerable eable = map.GetItemsInRange(from.Location, range);

            foreach (Item item in eable)
            {
                Type type = item.GetType();

                bool isAnvil = (type.IsDefined(typeofAnvil, false) || item.ItemID == 4015 || item.ItemID == 4016 || item.ItemID == 0x2DD5 || item.ItemID == 0x2DD6);
                bool isForge = (type.IsDefined(typeofForge, false) || item.ItemID == 4017 || (item.ItemID >= 6522 && item.ItemID <= 6569) || item.ItemID == 0x2DD8);

                if (isAnvil || isForge)
                {
                    if ((from.Z + 16) < item.Z || (item.Z + 16) < from.Z || !from.InLOS(item))
                        continue;

                    anvil = anvil || isAnvil;
                    forge = forge || isForge;

                    if (anvil && forge)
                        break;
                }
            }

            eable.Free();

            for (int x = -range; (!anvil || !forge) && x <= range; ++x)
            {
                for (int y = -range; (!anvil || !forge) && y <= range; ++y)
                {
                    StaticTile[] tiles = map.Tiles.GetStaticTiles(from.X + x, from.Y + y, true);

                    for (int i = 0; (!anvil || !forge) && i < tiles.Length; ++i)
                    {
                        int id = tiles[i].ID;

                        bool isAnvil = (id == 4015 || id == 4016 || id == 0x2DD5 || id == 0x2DD6);
                        bool isForge = (id == 4017 || (id >= 6522 && id <= 6569) || id == 0x2DD8);

                        if (isAnvil || isForge)
                        {
                            if ((from.Z + 16) < tiles[i].Z || (tiles[i].Z + 16) < from.Z || !from.InLOS(new Point3D(from.X + x, from.Y + y, tiles[i].Z + (tiles[i].Height / 2) + 1)))
                                continue;

                            anvil = anvil || isAnvil;
                            forge = forge || isForge;
                        }
                    }
                }
            }
        }
        public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
            else if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            return 0;
        }

        public override void PlayCraftEffect(Mobile from)
        {
            // no sound
            //from.PlaySound( 0x241 );
        }

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

            if (failed)
            {
                if (lostMaterial)
                    return 1044043; // You failed to create the item, and some of your materials are lost.
                else
                    return 1044157; // You failed to create the item, but no materials were lost.
            }
            else
            {
                if (quality == 0)
                    return 502785; // You were barely able to make this item.  It's quality is below average.
                else if (makersMark && quality == 2)
                    return 1044156; // You create an exceptional quality item and affix your maker's mark.
                else if (quality == 2)
                    return 1044155; // You create an exceptional quality item.
                else
                    return 1044154; // You create the item.
            }
        }

        public override bool ConsumeOnFailure(Mobile from, Type resourceType, CraftItem craftItem)
        {
         //   if (resourceType == typeof(Silver))
         //       return false;

            return base.ConsumeOnFailure(from, resourceType, craftItem);
        }

        public override void InitCraftList()
        {
            int index = -1;

            #region Gem Armor
            if (Core.ML)
            {
                index = AddCraft(typeof(BlueDiamondChest), "Gem Armor", "BlueDiamond Chest", 60.0, 110.0, typeof(BlueDiamond), 1026255, 6, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 10, 1044037);
                AddRes(index, typeof(MandrakeRoot), 1044357, 10, 1044365);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(BlueDiamondArms), "Gem Armor", "BlueDiamond Arms", 50.0, 100.0, typeof(BlueDiamond), 1026255, 3, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 5, 1044037);
                AddRes(index, typeof(MandrakeRoot), 1044357, 5, 1044365);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(BlueDiamondLegs), "Gem Armor", "BlueDiamond Legs", 50.0, 100.0, typeof(BlueDiamond), 1026255, 4, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 6, 1044037);
                AddRes(index, typeof(MandrakeRoot), 1044357, 6, 1044365);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(BlueDiamondGloves), "Gem Armor", "BlueDiamond Gloves", 40.0, 90.0, typeof(BlueDiamond), 1026255, 1, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(MandrakeRoot), 1044357, 3, 1044365);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(DarkSapphireChest), "Gem Armor", "DarkSapphire Chest", 60.0, 110.0, typeof(DarkSapphire), 1032690, 6, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 10, 1044037);
                AddRes(index, typeof(Nightshade), 1044358, 10, 1044366);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(DarkSapphireArms), "Gem Armor", "DarkSapphire Arms", 50.0, 100.0, typeof(IronIngot), 1044036, 5, 1044037);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 5, 1044037);
                AddRes(index, typeof(Nightshade), 1044358, 5, 1044366);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(DarkSapphireLegs), "Gem Armor", "DarkSapphire Legs", 50.0, 100.0, typeof(IronIngot), 1044036, 6, 1044037);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 6, 1044037);
                AddRes(index, typeof(Nightshade), 1044358, 6, 1044366);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(DarkSapphireGloves), "Gem Armor", "DarkSapphire Gloves", 40.0, 90.0, typeof(IronIngot), 1044036, 3, 1044037);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(Nightshade), 1044358, 3, 1044366);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(EcruCitrineChest), "Gem Armor", "EcruCitrine Chest", 60.0, 110.0, typeof(EcruCitrine), 1032693, 6, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 10, 1044037);
                AddRes(index, typeof(Garlic), 1044355, 10, 1044363);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(EcruCitrineArms), "Gem Armor", "EcruCitrine Arms", 50.0, 100.0, typeof(EcruCitrine), 1032693, 3, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 5, 1044037);
                AddRes(index, typeof(Garlic), 1044355, 5, 1044363);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(EcruCitrineLegs), "Gem Armor", "EcruCitrine Legs", 50.0, 100.0, typeof(EcruCitrine), 1032693, 4, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 6, 1044037);
                AddRes(index, typeof(Garlic), 1044355, 6, 1044363);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(EcruCitrineGloves), "Gem Armor", "EcruCitrine Gloves", 40.0, 90.0, typeof(EcruCitrine), 1032693, 1, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(Garlic), 1044355, 3, 1044363);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(FireRubyChest), "Gem Armor", "FireRuby Chest", 60.0, 110.0, typeof(FireRuby), 1032695, 6, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 10, 1044037);
                AddRes(index, typeof(SulfurousAsh), 1044359, 10, 1044367);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(FireRubyArms), "Gem Armor", "FireRuby Arms", 50.0, 100.0, typeof(FireRuby), 1032695, 3, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 5, 1044037);
                AddRes(index, typeof(SulfurousAsh), 1044359, 5, 1044367);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(FireRubyLegs), "Gem Armor", "FireRuby Legs", 50.0, 100.0, typeof(FireRuby), 1032695, 4, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 6, 1044037);
                AddRes(index, typeof(SulfurousAsh), 1044359, 6, 1044367);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(FireRubyGloves), "Gem Armor", "FireRuby Gloves", 40.0, 90.0, typeof(FireRuby), 1032695, 1, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(SulfurousAsh), 1044359, 3, 1044367);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(PerfectEmeraldChest), "Gem Armor", "PerfectEmerald Chest", 60.0, 110.0, typeof(PerfectEmerald), 1032692, 6, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 10, 1044037);
                AddRes(index, typeof(Ginseng), 1044356, 10, 1044364);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(PerfectEmeraldArms), "Gem Armor", "PerfectEmerald Arms", 50.0, 100.0, typeof(PerfectEmerald), 1032692, 3, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 5, 1044037);
                AddRes(index, typeof(Ginseng), 1044356, 5, 1044364);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(PerfectEmeraldLegs), "Gem Armor", "PerfectEmerald Legs", 50.0, 100.0, typeof(PerfectEmerald), 1032692, 4, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 6, 1044037);
                AddRes(index, typeof(Ginseng), 1044356, 6, 1044364);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(PerfectEmeraldGloves), "Gem Armor", "PerfectEmerald Gloves", 40.0, 90.0, typeof(PerfectEmerald), 1032692, 1, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(Ginseng), 1044356, 3, 1044364);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(TurquoiseChest), "Gem Armor", "Turquoise Chest", 60.0, 110.0, typeof(Turquoise), 1032691, 6, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 10, 1044037);
                AddRes(index, typeof(NoxCrystal), 1023982, 10, "You do not have enough nox crystal to make that.");
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(TurquoiseArms), "Gem Armor", "Turquoise Arms", 50.0, 100.0, typeof(Turquoise), 1032691, 3, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 5, 1044037);
                AddRes(index, typeof(NoxCrystal), 1023982, 5, "You do not have enough nox crystal to make that.");
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(TurquoiseLegs), "Gem Armor", "Turquoise Legs", 50.0, 100.0, typeof(Turquoise), 1032691, 4, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 6, 1044037);
                AddRes(index, typeof(NoxCrystal), 1023982, 6, "You do not have enough nox crystal to make that.");
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(TurquoiseGloves), "Gem Armor", "Turquoise Gloves", 40.0, 90.0, typeof(Turquoise), 1032691, 1, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(NoxCrystal), 1023982, 3, "You do not have enough nox crystal to make that.");
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(BrilliantAmberChest), "Gem Armor", "BrilliantAmber Chest", 60.0, 110.0, typeof(BrilliantAmber), 1032697, 6, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 10, 1044037);
                AddRes(index, typeof(Bloodmoss), 1044354, 10, 1044362);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(BrilliantAmberArms), "Gem Armor", "BrilliantAmber Arms", 50.0, 100.0, typeof(BrilliantAmber), 1032697, 3, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 5, 1044037);
                AddRes(index, typeof(Bloodmoss), 1044354, 5, 1044362);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(BrilliantAmberLegs), "Gem Armor", "BrilliantAmber Legs", 50.0, 100.0, typeof(BrilliantAmber), 1032697, 4, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 6, 1044037);
                AddRes(index, typeof(Bloodmoss), 1044354, 6, 1044362);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(BrilliantAmberGloves), "Gem Armor", "BrilliantAmber Gloves", 40.0, 90.0, typeof(BrilliantAmber), 1032697, 1, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(Bloodmoss), 1044354, 3, 1044362);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(WhitePearlChest), "Gem Armor", "WhitePearl Chest", 60.0, 110.0, typeof(WhitePearl), 1032694, 6, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 10, 1044037);
                AddRes(index, typeof(SpidersSilk), 1044360, 10, 1044368);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(WhitePearlArms), "Gem Armor", "WhitePearl Arms", 50.0, 100.0, typeof(WhitePearl), 1032694, 3, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 5, 1044037);
                AddRes(index, typeof(SpidersSilk), 1044360, 5, 1044368);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(WhitePearlLegs), "Gem Armor", "WhitePearl Legs", 50.0, 100.0, typeof(WhitePearl), 1032694, 4, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 6, 1044037);
                AddRes(index, typeof(SpidersSilk), 1044360, 6, 1044368);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(WhitePearlGloves), "Gem Armor", "WhitePearl Gloves", 40.0, 90.0, typeof(WhitePearl), 1032694, 1, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(SpidersSilk), 1044360, 3, 1044368);
                SetNeededExpansion(index, Expansion.ML);
        }
            #endregion

            #region Armor
            AddCraft(typeof(RingmailGloves), 1062760, 1025099, 12.0, 62.0, typeof(IronIngot), 1044036, 10, 1044037);
            AddCraft(typeof(RingmailLegs), 1062760, 1025104, 19.4, 69.4, typeof(IronIngot), 1044036, 16, 1044037);
            AddCraft(typeof(RingmailArms), 1062760, 1025103, 16.9, 66.9, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(RingmailChest), 1062760, 1025100, 21.9, 71.9, typeof(IronIngot), 1044036, 18, 1044037);
            AddCraft(typeof(ChainCoif), 1062760, 1025051, 14.5, 64.5, typeof(IronIngot), 1044036, 10, 1044037);
            AddCraft(typeof(ChainLegs), 1062760, 1025054, 36.7, 86.7, typeof(IronIngot), 1044036, 18, 1044037);
            AddCraft(typeof(ChainChest), 1062760, 1025055, 39.1, 89.1, typeof(IronIngot), 1044036, 20, 1044037);
            AddCraft(typeof(PlateArms), 1062760, 1025136, 66.3, 116.3, typeof(IronIngot), 1044036, 18, 1044037);
            AddCraft(typeof(PlateGloves), 1062760, 1025140, 58.9, 108.9, typeof(IronIngot), 1044036, 12, 1044037);
            AddCraft(typeof(PlateGorget), 1062760, 1025139, 56.4, 106.4, typeof(IronIngot), 1044036, 10, 1044037);
            AddCraft(typeof(PlateLegs), 1062760, 1025137, 68.8, 118.8, typeof(IronIngot), 1044036, 20, 1044037);
            AddCraft(typeof(PlateChest), 1062760, 1046431, 75.0, 125.0, typeof(IronIngot), 1044036, 25, 1044037);
            AddCraft(typeof(FemalePlateChest), 1062760, 1046430, 44.1, 94.1, typeof(IronIngot), 1044036, 20, 1044037);
            #endregion

            #region Helmets
            AddCraft(typeof(NorseHelm), 1011079, 1025134, 37.9, 87.9, typeof(IronIngot), 1044036, 15, 1044037);
            AddCraft(typeof(PlateHelm), 1011079, 1025138, 62.6, 112.6, typeof(IronIngot), 1044036, 15, 1044037);
            if (Core.SE)
            {
                index = AddCraft(typeof(PlateHatsuburi), 1011079, 1030176, 45.0, 95.0, typeof(IronIngot), 1044036, 20, 1044037);
                SetNeededExpansion(index, Expansion.SE);

                index = AddCraft(typeof(HeavyPlateJingasa), 1011079, 1030178, 45.0, 95.0, typeof(IronIngot), 1044036, 20, 1044037);
                SetNeededExpansion(index, Expansion.SE);

                index = AddCraft(typeof(PlateBattleKabuto), 1011079, 1030192, 90.0, 140.0, typeof(IronIngot), 1044036, 25, 1044037);
                SetNeededExpansion(index, Expansion.SE);
            }
            if (Core.ML)
            {
                index = AddCraft(typeof(BlueDiamondHelm), 1011079, "BlueDiamond Helm", 50.0, 100.0, typeof(BlueDiamond), 1026255, 4, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 7, 1044037);
                AddRes(index, typeof(MandrakeRoot), 1044357, 7, 1044365);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(DarkSapphireHelm), 1011079, "DarkSapphire Helm", 50.0, 100.0, typeof(IronIngot), 1044036, 7, 1044037);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 7, 1044037);
                AddRes(index, typeof(Nightshade), 1044358, 7, 1044366);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(EcruCitrineHelm), 1011079, "EcruCitrine Helm", 50.0, 100.0, typeof(EcruCitrine), 1032693, 4, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 7, 1044037);
                AddRes(index, typeof(Garlic), 1044355, 7, 1044363);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(FireRubyHelm), 1011079, "FireRuby Helm", 50.0, 100.0, typeof(FireRuby), 1032695, 4, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 7, 1044037);
                AddRes(index, typeof(SulfurousAsh), 1044359, 7, 1044367);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(PerfectEmeraldHelm), 1011079, "PerfectEmerald Helm", 50.0, 100.0, typeof(PerfectEmerald), 1032692, 4, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 7, 1044037);
                AddRes(index, typeof(Ginseng), 1044356, 7, 1044364);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(TurquoiseHelm), 1011079, "Turquoise Helm", 50.0, 100.0, typeof(Turquoise), 1032691, 4, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 7, 1044037);
                AddRes(index, typeof(NoxCrystal), 1023982, 7, "You do not have enough nox crystal to make that.");
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(BrilliantAmberHelm), 1011079, "BrilliantAmber Helm", 50.0, 100.0, typeof(BrilliantAmber), 1032697, 4, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 7, 1044037);
                AddRes(index, typeof(Bloodmoss), 1044354, 7, 1044362);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(WhitePearlHelm), 1011079, "WhitePearl Helm", 50.0, 100.0, typeof(WhitePearl), 1032694, 4, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 7, 1044037);
                AddRes(index, typeof(SpidersSilk), 1044360, 7, 1044368);
                SetNeededExpansion(index, Expansion.ML);
            }
            #endregion

            #region Weapons

            if (Core.AOS)
            {
                AddCraft(typeof(Broadsword), 1044566, 1023934, 35.4, 85.4, typeof(IronIngot), 1044036, 10, 1044037);
                AddCraft(typeof(Katana), 1044566, 1025119, 44.1, 94.1, typeof(IronIngot), 1044036, 8, 1044037);
                AddCraft(typeof(Longsword), 1044566, 1023937, 28.0, 78.0, typeof(IronIngot), 1044036, 12, 1044037);
                AddCraft(typeof(LargeBattleAxe), 1044566, 1025115, 28.0, 78.0, typeof(IronIngot), 1044036, 12, 1044037);
                AddCraft(typeof(TwoHandedAxe), 1044566, 1025187, 33.0, 83.0, typeof(IronIngot), 1044036, 16, 1044037);
                AddCraft(typeof(Mace), 1044566, 1023932, 14.5, 64.5, typeof(IronIngot), 1044036, 6, 1044037);
                AddCraft(typeof(WarMace), 1044566, 1025127, 28.0, 78.0, typeof(IronIngot), 1044036, 14, 1044037);
            }
            if (Core.SE)
            {
                index = AddCraft(typeof(NoDachi), 1044566, 1030221, 75.0, 125.0, typeof(IronIngot), 1044036, 18, 1044037);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(Daisho), 1044566, 1030228, 60.0, 110.0, typeof(IronIngot), 1044036, 15, 1044037);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(Lajatang), 1044566, 1030226, 80.0, 130.0, typeof(IronIngot), 1044036, 25, 1044037);
                SetNeededExpansion(index, Expansion.SE);
            }
            if (Core.ML)
            {

            }
            #endregion

            #region Shields
            AddCraft(typeof(HeaterShield), 1011080, 1027030, 24.3, 74.3, typeof(IronIngot), 1044036, 18, 1044037);
            AddCraft(typeof(MetalShield), 1011080, 1027035, -10.2, 39.8, typeof(IronIngot), 1044036, 14, 1044037);
            AddCraft(typeof(MetalKiteShield), 1011080, 1027028, 4.6, 54.6, typeof(IronIngot), 1044036, 16, 1044037);
            if (Core.AOS)
            {
                AddCraft(typeof(ChaosShield), 1011080, 1027107, 85.0, 135.0, typeof(IronIngot), 1044036, 25, 1044037);
                AddCraft(typeof(OrderShield), 1011080, 1027108, 85.0, 135.0, typeof(IronIngot), 1044036, 25, 1044037);
            }
            if (Core.ML)
            {
                index = AddCraft(typeof(BlueDiamondShield), 1011080, "BlueDiamondShield", 50.0, 100.0, typeof(BlueDiamond), 1026255, 2, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(MandrakeRoot), 1044357, 10, 1044365);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(DarkSapphireShield), 1011080, "DarkSapphireShield", 50.0, 100.0, typeof(DarkSapphire), 1026249, 2, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(Nightshade), 1044358, 3, 1044366);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(EcruCitrineShield), 1011080, "EcruCitrineShield", 50.0, 100.0, typeof(EcruCitrine), 1026252, 2, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(Garlic), 1044355, 3, 1044363);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(FireRubyShield), 1011080, "FireRubyShield", 50.0, 100.0, typeof(FireRuby), 1026254, 2, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(SulfurousAsh), 1044359, 3, 1044367);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(PerfectEmeraldShield), 1011080, "PerfectEmeraldShield", 50.0, 100.0, typeof(PerfectEmerald), 1026251, 2, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(Ginseng), 1044356, 3, 1044364);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(TurquoiseShield), 1011080, "TurquoiseShield", 50.0, 100.0, typeof(Turquoise), 1026250, 2, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(NoxCrystal), 1023982, 3, "You do not have enough nox crystal to make that.");
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(BrilliantAmberShield), 1011080, "BrilliantAmberShield", 50.0, 100.0, typeof(BrilliantAmber), 1026256, 2, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(Bloodmoss), 1044354, 3, 1044362);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(WhitePearlShield), 1011080, "WhitePearlShield", 50.0, 100.0, typeof(WhitePearl), 1026253, 2, 1044240);
                AddSkill(index, SkillName.Magery, 65.0, 125.0);
                AddRes(index, typeof(IronIngot), 1044036, 3, 1044037);
                AddRes(index, typeof(SpidersSilk), 1044360, 3, 1044368);
                SetNeededExpansion(index, Expansion.ML);
            }
            #endregion

            // Set the overridable material
            SetSubRes(typeof(IronIngot), 1044022);

            // Add every material you want the player to be able to choose from
            // This will override the overridable material
   /*         AddSubRes(typeof(IronIngot), 1044022, 00.0, 1044036, 1044267);
            AddSubRes(typeof(DullCopperIngot), 1044023, 40.0, 1044036, 1044268);
            AddSubRes(typeof(ShadowIronIngot), 1044024, 45.0, 1044036, 1044268);
            AddSubRes(typeof(CopperIngot), 1044025, 50.0, 1044036, 1044268);
            AddSubRes(typeof(BronzeIngot), 1044026, 55.0, 1044036, 1044268);
            AddSubRes(typeof(GoldIngot), 1044027, 60.0, 1044036, 1044268);
            AddSubRes(typeof(AgapiteIngot), 1044028, 65.0, 1044036, 1044268);
            AddSubRes(typeof(VeriteIngot), 1044029, 70.0, 1044036, 1044268);
            AddSubRes(typeof(ValoriteIngot), 1044030, 75.0, 1044036, 1044268); */
            AddSubRes(typeof(IronIngot), "Iron", 00.0, 1044267);
            AddSubRes(typeof(DullCopperIngot), "Dull Copper", 40.0, 1044268);
            AddSubRes(typeof(ShadowIronIngot), "Shadow Iron", 45.0, 1044268);
            AddSubRes(typeof(CopperIngot), "Copper", 50.0, 1044268);
            AddSubRes(typeof(BronzeIngot), "Bronze", 55.0, 1044268);
            AddSubRes(typeof(GoldIngot), "Gold", 60.0, 1044268);
            AddSubRes(typeof(AgapiteIngot), "Agapite", 65.0, 1044268);
            AddSubRes(typeof(VeriteIngot), "Verite", 70.0, 1044268);
            AddSubRes(typeof(ValoriteIngot), "Valorite", 75.0, 1044268);
            AddSubRes(typeof(BlazeIngot), "Blaze", 80.0, 1044268);
            AddSubRes(typeof(IceIngot), "Ice", 85.0, 1044268);
            AddSubRes(typeof(ToxicIngot), "Toxic", 90.0, 1044268);
            AddSubRes(typeof(ElectrumIngot), "Electrum", 95.0, 1044268);
            AddSubRes(typeof(PlatinumIngot), "Platinum", 99.0, 1044268);

            MarkOption = true;
            Repair = false;
            CanEnhance = Core.AOS;
        }
    }
}