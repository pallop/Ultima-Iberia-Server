/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class LokaiSkillsBag : Bag
    {
        public override string DefaultName
        {
            get { return "Skills Bag"; }
        }

        [Constructable]
        public LokaiSkillsBag()
            : this(1)
        {
            Movable = true;
            Hue = 0x315;
        }

        [Constructable]
        public LokaiSkillsBag(int amount)
        {
            DropItem(new BrickLayersTool());
            DropItem(new FramingTool());
            DropItem(new RoofersTool());
            DropItem(new StoneMasonsTool());
            DropItem(new WoodworkersTool());
            DropItem(new TreeHarvestTool());
            DropItem(new RawCotton(20));
            DropItem(new RawWool(20));
            DropItem(new RawFlax(20));
        }

        public LokaiSkillsBag(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}