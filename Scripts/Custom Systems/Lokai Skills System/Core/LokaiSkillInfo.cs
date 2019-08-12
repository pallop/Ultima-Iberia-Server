/***************************************************************************
 *   Based off the RunUO Skills system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;

namespace Server
{
    public delegate TimeSpan LokaiSkillUseCallback(Mobile user);

    public class LokaiSkillInfo
    {
        private int m_LokaiSkillID;
        private string m_Name;
        private string m_Title;
        private double m_StrScale;
        private double m_DexScale;
        private double m_IntScale;
        private double m_StatTotal;
        private bool m_ClickToUse;
        private LokaiSkillUseCallback m_Callback;
        private static LokaiSkillInfo[] m_Table;

        public int LokaiSkillID { get { return m_LokaiSkillID; } }
        public string Name { get { return m_Name; } set { m_Name = value; } }
        public string Title { get { return m_Title; } set { m_Title = value; } }
        public double StrScale { get { return m_StrScale; } set { m_StrScale = value; } }
        public double DexScale { get { return m_DexScale; } set { m_DexScale = value; } }
        public double IntScale { get { return m_IntScale; } set { m_IntScale = value; } }
        public double StatTotal { get { return m_StatTotal; } set { m_StatTotal = value; } }
        public bool ClickToUse { get { return m_ClickToUse; } set { m_ClickToUse = value; } }
        public LokaiSkillUseCallback Callback { get { return m_Callback; } set { m_Callback = value; } }
        public static LokaiSkillInfo[] Table { get { return LokaiSkillInfo.m_Table; } set { LokaiSkillInfo.m_Table = value; } }

        public LokaiSkillInfo(int lokaiSkillID, string name, string title, double strScale, double dexScale, double intScale, bool clickToUse, LokaiSkillUseCallback callback)
        {
            m_LokaiSkillID = lokaiSkillID;
            m_Name = name;
            m_Title = title;
            m_StrScale = strScale / 100.0;
            m_DexScale = dexScale / 100.0;
            m_IntScale = intScale / 100.0;
            m_StatTotal = strScale + dexScale + intScale;
            m_Callback = callback;
            m_ClickToUse = clickToUse;
        }

        static LokaiSkillInfo()
        {
            // When these lokaiSkills are implemented, if there is direct-use code for the lokaiSkill,
            //   change the boolean value to 'true'
            LokaiSkillInfo[] lokaiSkillarray;
            lokaiSkillarray = new LokaiSkillInfo[30];
            lokaiSkillarray[0] = new LokaiSkillInfo(0, "Butchering", "Butcher", 5.0, 5.0, 10.0, false, null);
            lokaiSkillarray[1] = new LokaiSkillInfo(1, "Skinning", "Skinner", 10.0, 0.0, 10.0, false, null);
            lokaiSkillarray[2] = new LokaiSkillInfo(2, "Animal Riding", "Rider", 10.0, 10.0, 0.0, false, null);
            lokaiSkillarray[3] = new LokaiSkillInfo(3, "Sailing", "Sailor", 15.0, 5.0, 0.0, false, null);
            lokaiSkillarray[4] = new LokaiSkillInfo(4, "Detect Evil", "Cleric", 0.0, 0.0, 10.0, true, null);
            lokaiSkillarray[5] = new LokaiSkillInfo(5, "Cure Disease", "Cleric", 0.0, 0.0, 10.0, true, null);
            lokaiSkillarray[6] = new LokaiSkillInfo(6, "Pick Pocket", "Rogue", 0.0, 10.0, 0.0, true, null);
            lokaiSkillarray[7] = new LokaiSkillInfo(7, "Pilfering", "Rogue", 0.0, 5.0, 10.0, true, null);
            lokaiSkillarray[8] = new LokaiSkillInfo(8, "Framing", "Framer", 0.0, 0.0, 0.0, false, null);
            lokaiSkillarray[9] = new LokaiSkillInfo(9, "Brick Laying", "Bricklayer", 10.0, 10.0, 0.0, false, null);
            lokaiSkillarray[10] = new LokaiSkillInfo(10, "Roofing", "Roofer", 10.0, 10.0, 0.0, false, null);
            lokaiSkillarray[11] = new LokaiSkillInfo(11, "Stone Masonry", "Mason", 15.0, 5.0, 0.0, false, null);
            lokaiSkillarray[12] = new LokaiSkillInfo(12, "Ventriloquism", "Ventriloquist", 0.0, 10.0, 10.0, true, null);
            lokaiSkillarray[13] = new LokaiSkillInfo(13, "Hypnotism", "Hypnotist", 0.0, 5.0, 15.0, true, null);
            lokaiSkillarray[14] = new LokaiSkillInfo(14, "Prey Tracking", "Ranger", 5.0, 10.0, 5.0, false, null);
            lokaiSkillarray[15] = new LokaiSkillInfo(15, "Speak To Animals", "Ranger", 5.0, 0.0, 10.0, true, null);
            lokaiSkillarray[16] = new LokaiSkillInfo(16, "Woodworking", "Woodworker", 5.0, 10.0, 0.0, true, null);
            lokaiSkillarray[17] = new LokaiSkillInfo(17, "Cooperage", "Cooper", 0.0, 20.0, 0.0, true, null);
            lokaiSkillarray[18] = new LokaiSkillInfo(18, "Spinning", "Weaver", 10.0, 5.0, 0.0, false, null);
            lokaiSkillarray[19] = new LokaiSkillInfo(19, "Weaving", "Weaver", 10.0, 5.0, 0.0, false, null);
            lokaiSkillarray[20] = new LokaiSkillInfo(20, "Construction", "Merchant", 20.0, 0.0, 0.0, false, null);
            lokaiSkillarray[21] = new LokaiSkillInfo(21, "Commerce", "Merchant", 0.0, 0.0, 20.0, true, null);
            lokaiSkillarray[22] = new LokaiSkillInfo(22, "Brewing", "Herbalist", 0.0, 0.0, 10.0, true, null);
            lokaiSkillarray[23] = new LokaiSkillInfo(23, "Herblore", "Herbalist", 0.0, 0.0, 10.0, true, null);
            lokaiSkillarray[24] = new LokaiSkillInfo(24, "Tree Picking", "Harvester", 10.0, 10.0, 0.0, false, null);
            lokaiSkillarray[25] = new LokaiSkillInfo(25, "Tree Sapping", "Harvester", 10.0, 10.0, 0.0, false, null);
            lokaiSkillarray[26] = new LokaiSkillInfo(26, "Tree Carving", "Harvester", 15.0, 5.0, 0.0, false, null);
            lokaiSkillarray[27] = new LokaiSkillInfo(27, "Tree Digging", "Harvester", 15.0, 5.0, 0.0, false, null);
            lokaiSkillarray[28] = new LokaiSkillInfo(28, "Teaching", "Scholar", 0.0, 0.0, 10.0, false, null);
            lokaiSkillarray[29] = new LokaiSkillInfo(29, "Linguistics", "Scholar", 0.0, 5.0, 5.0, false, null);
            LokaiSkillInfo.m_Table = lokaiSkillarray;
        }
    }
}
