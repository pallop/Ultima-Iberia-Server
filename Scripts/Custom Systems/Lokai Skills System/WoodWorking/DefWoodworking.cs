/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Items;

namespace Server.Engines.Build
{
    public class DefWoodworking : BuildSystem
    {
        public override LokaiSkillName MainLokaiSkill
        {
            get { return LokaiSkillName.Woodworking; }
        }

        public override string GumpTitleString
        {
            get { return "Woodworking"; }
        }

        //public override int GumpTitleNumber
        //{
        //    get { return 1016429; } // Woodworking Artistry
        //}

        private static BuildSystem m_BuildSystem;

        public static BuildSystem BuildSystem
        {
            get
            {
                if (m_BuildSystem == null)
                    m_BuildSystem = new DefWoodworking();

                return m_BuildSystem;
            }
        }

        public override double GetChanceAtMin(BuildItem item)
        {
            return 0.5; // 50%
        }

        private DefWoodworking()
            : base(1, 1, 1.25)// base( 1, 1, 3.0 )
        {
        }

        public override int CanBuild(Mobile from, BaseBuildingTool tool, Type itemType)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
            else if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            return 0;
        }

        public override void PlayBuildEffect(Mobile from)
        {
            from.PlaySound(0x23D);
        }

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, BuildItem item)
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

        public override void InitBuildList()
        {
            int index = -1;

            // Other Items
            index = AddBuild(typeof(LatheWork), "Unfinished Furniture", "Lathe Work", 10.0, 50.0, typeof(Log), 1044466, 5, 1044465);
            index = AddBuild(typeof(LargeTableLegs), "Unfinished Furniture", "Large Table Legs", 10.0, 50.0, typeof(Log), 1044466, 10, 1044465);
            index = AddBuild(typeof(UnfinishedStool), "Unfinished Furniture", "Unfinished Stool", 10.0, 50.0, typeof(Log), 1044466, 12, 1044465);
            index = AddBuild(typeof(UnfinishedBoard), "Unfinished Furniture", "Unfinished Board", 10.0, 50.0, typeof(Log), 1044466, 3, 1044465);
            index = AddBuild(typeof(UnfinishedChair), "Unfinished Furniture", "Unfinished Chair", 10.0, 50.0, typeof(Log), 1044466, 7, 1044465);
            index = AddBuild(typeof(UnfinishedDrawer), "Unfinished Furniture", "Unfinished Drawer", 10.0, 50.0, typeof(Log), 1044466, 5, 1044465);
            index = AddBuild(typeof(UnfinishedChestOfDrawers), "Unfinished Furniture", "Unfinished Chest Of Drawers", 10.0, 50.0, typeof(Log), 1044466, 16, 1044465);
            index = AddBuild(typeof(UnfinishedTable), "Unfinished Furniture", "Unfinished Table", 10.0, 50.0, typeof(Log), 1044466, 10, 1044465);
            index = AddBuild(typeof(UnfinishedTableLegs), "Unfinished Furniture", "Unfinished Table Legs", 10.0, 50.0, typeof(Log), 1044466, 8, 1044465);
            index = AddBuild(typeof(UnfinishedTableLeg), "Unfinished Furniture", "Unfinished Table Leg", 10.0, 50.0, typeof(Log), 1044466, 3, 1044465);
            index = AddBuild(typeof(UnfinishedShelves), "Unfinished Furniture", "Unfinished Shelves", 10.0, 50.0, typeof(Log), 1044466, 18, 1044465);
            index = AddBuild(typeof(UnfinishedWoodenBox), "Unfinished Furniture", "Unfinished Wooden Box", 10.0, 50.0, typeof(Log), 1044466, 9, 1044465);
            
            SetSubRes(typeof(Log), 1044022);

            AddSubRes(typeof(OakLog), 1075063, 30.0, 1044466, 1072652);
            AddSubRes(typeof(AshLog), 1075064, 45.0, 1044466, 1072652);
            AddSubRes(typeof(YewLog), 1075065, 60.0, 1044466, 1072652);
            AddSubRes(typeof(HeartwoodLog), 1075066, 75.0, 1044466, 1072652);
            AddSubRes(typeof(BloodwoodLog), 1075067, 90.0, 1044466, 1072652);
            AddSubRes(typeof(FrostwoodLog), 1075068, 99.5, 1044466, 1072652);

            MarkOption = true;
            Repair = Core.AOS;
        }
    }
}