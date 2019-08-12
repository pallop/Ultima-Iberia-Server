/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
    public class MasterLokaiSkillItem : Item
    {
        [Constructable]
        public MasterLokaiSkillItem()
            : base(0x0E3B)
        {
            Hue = 777;
            Name = "a Master Lokai Skill Item";
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel >= AccessLevel.GameMaster && from.PlaceInBackpack(new LokaiSkillsBag()))
                from.SendMessage("You have been given a Lokai Skills Bag for testing.");
        }

        public MasterLokaiSkillItem(Serial s)
            : base(s)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)6); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 6:
                default: break;
            }
        }
    }
}
