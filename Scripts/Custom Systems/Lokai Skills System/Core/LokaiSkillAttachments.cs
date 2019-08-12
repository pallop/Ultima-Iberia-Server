/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.LokaiSkillHandlers;
using Server.Network;
using Server.Engines.XmlSpawner2;

namespace Server.Engines.XmlSpawner2
{
    public class LSA : XmlAttachment
    {
        private LokaiSkills m_Skills = null;
        private Mobile m_From = null;

        [CommandProperty(AccessLevel.GameMaster)]
        public LokaiSkills Skills { get { return m_Skills; } set { m_Skills = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile From { get { return m_From; } set { m_From = value; } }

        public LSA(ASerial serial)
            : base(serial)
        {
        }

        [Attachable]
        public LSA(Mobile from)
        {
            m_From = from;
            m_Skills = new LokaiSkills(m_From);
        }

        [Attachable]
        public LSA(LokaiSkills skills, Mobile from)
            : base()
        {
            m_Skills = skills;
            m_From = from;
        }

        public override void GetPlayerMobileProperties(ObjectPropertyList list)
        {
            base.AddProperties(list);
            if (Skills.Highest == Skills[LokaiSkillName.AnimalRiding])
            {
                int riding = (int)Skills[LokaiSkillName.AnimalRiding].Base;
                riding = riding >= (int)RidingRank.Master ? (int)RidingRank.Master : riding >= (int)RidingRank.Equestrian ?
                    (int)RidingRank.Equestrian : riding >= (int)RidingRank.Advanced ? (int)RidingRank.Advanced :
                    riding >= (int)RidingRank.Able ? (int)RidingRank.Able : riding >= (int)RidingRank.Intermediate ?
                    (int)RidingRank.Intermediate : riding >= (int)RidingRank.Amateur ? (int)RidingRank.Amateur :
                    riding >= (int)RidingRank.Novice ? (int)RidingRank.Novice : (int)RidingRank.Pedestrian;
                list.Add("{0} Rider", ((RidingRank)riding).ToString());
            }
            else if (Skills.Highest == Skills[LokaiSkillName.Sailing])
            {
                int sailing = (int)Skills[LokaiSkillName.Sailing].Base;
                sailing = sailing >= (int)SailingRank.Master ? (int)SailingRank.Master : sailing >= (int)SailingRank.Veteran ?
                    (int)SailingRank.Veteran : sailing >= (int)SailingRank.Journeyman ? (int)SailingRank.Journeyman :
                    sailing >= (int)SailingRank.Adept ? (int)SailingRank.Adept : sailing >= (int)SailingRank.Able ?
                    (int)SailingRank.Able : sailing >= (int)SailingRank.Amateur ? (int)SailingRank.Amateur :
                    sailing >= (int)SailingRank.Novice ? (int)SailingRank.Novice : (int)SailingRank.Landlubber;
                list.Add("{0} Sailor", ((SailingRank)sailing).ToString());
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
            // version 0
            writer.Write((Mobile)m_From);
            m_Skills.Serialize(writer);

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            // version 0
            m_From = reader.ReadMobile();
            m_Skills = new LokaiSkills(m_From, reader);
        }
    }
}