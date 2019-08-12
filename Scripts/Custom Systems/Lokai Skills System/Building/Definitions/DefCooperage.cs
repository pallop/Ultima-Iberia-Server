/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Build
{
    public class DefCooperage : BuildSystem
    {
        public override LokaiSkillName MainLokaiSkill
        {
            get { return LokaiSkillName.Cooperage; }
        }

        public override string GumpTitleString
        {
            get { return "Cooperage"; }
        }

        public override void PlayBuildEffect(Mobile from) { }
        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken,
            int quality, bool makersMark, BuildItem item) { return 0; }

        public override int CanBuild(Mobile from, BaseBuildingTool tool, Type itemType) { return 0; }

        private static BuildSystem m_BuildSystem;

        public static BuildSystem BuildSystem
        {
            get
            {
                if (m_BuildSystem == null)
                    m_BuildSystem = new DefCooperage();

                return m_BuildSystem;
            }
        }

        public override double GetChanceAtMin(BuildItem item)
        {
            return 0.0; // 0% 
        }

        private DefCooperage()
            : base(1, 1, 1.25)// base( 1, 2, 1.7 ) 
        {
        }

        public override void InitBuildList()
        {
            ///a cooper's work include but are not limited to casks, barrels, buckets, 
            ///tubs, butter churns, hogsheads, firkins, tierces, rundlets, puncheons, 
            ///pipes, tuns, butts, pins, and breakers/scuttlebutt
            ///gallon rundlet barrel tierce hogshead firkin, puncheon, tertian    pipe, butt      tun  
            ///                                                                                   1 tun 
            ///                                                                   1               2 pipes, butts 
            ///                                       1                           1+1⁄2           3 firkins, puncheons, tertians 
            ///                               1       1+1⁄3                       2               4 hogsheads 
            ///                        1      1+1⁄2   2                           3               6 tierces 
            ///                1       1+1⁄3  2       2+2⁄3                       4               8 barrels 
            ///       1        1+3⁄4   2+1⁄3  3+1⁄2   4+2⁄3                       7               14 rundlets 
            /// 1     18       31+1⁄2  42     63      84                          126             252 gallons (US/wine) 

            int index = -1;

            index = AddBuild(typeof(Barrel), "Barrels", "Barrel", 10.0, 100.0, typeof(BarrelStaves), 1044288, 12, 1044253);
            AddRes(index, typeof(BarrelHoops), "Barrel Hoops", 3);
            AddRes(index, typeof(BarrelLid), "Barrel Hoops", 1);
            AddRes(index, typeof(BarrelTap), "Barrel Hoops", 1);

            index = AddBuild(typeof(Keg), "Barrels", 1023711, 57.8, 82.8, typeof(BarrelStaves), 1044288, 3, 1044253);
            AddRes(index, typeof(BarrelHoops), 1044289, 1, 1044253);
            AddRes(index, typeof(BarrelLid), 1044251, 1, 1044253);

            MarkOption = false;
            Repair = false;

            SetSubRes(typeof(BarrelStaves), 1044525);
        }
    }
}