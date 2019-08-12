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
    public enum LokaiSkillName
    {
        // Naturalist LokaiSkills	
        Butchering = 0,
        Skinning,
        AnimalRiding,
        Sailing,

        // Cleric LokaiSkills	
        DetectEvil,
        CureDisease,

        // Rogue LokaiSkills	
        PickPocket,
        Pilfering,

        // Laborer LokaiSkills
        Framing,
        BrickLaying,
        Roofing,
        StoneMasonry,

        // Illusionist LokaiSkills	
        Ventriloquism,
        Hypnotism,

        // Ranger LokaiSkills	
        PreyTracking,
        SpeakToAnimals,

        // Craftsman LokaiSkills
        Woodworking,
        Cooperage,

        // Weaver LokaiSkills
        Spinning,
        Weaving,

        // Merchant LokaiSkills	
        Construction,
        Commerce,

        //Herbalist LokaiSkills
        Brewing,
        Herblore,

        // Tree Harvest LokaiSkills	
        TreePicking,
        TreeSapping,
        TreeCarving,
        TreeDigging,

        // Scholar LokaiSkills	
        Teaching,
        Linguistics
    }

    public struct LokaiSkillNameValue
    {
        public LokaiSkillName Name;
        public int Value;

        public LokaiSkillNameValue(LokaiSkillName name, int val)
        {
            Name = name;
            Value = val;
        }
    }
}
