/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server;
using Server.Engines.Build;

namespace Server.Items
{
    public class WoodworkersTool : BaseBuildingTool
    {
        public override BuildSystem BuildSystem { get { return DefWoodworking.BuildSystem; } }
        public override string DefaultName { get { return "Woodworker's Tool"; } }

        [Constructable]
        public WoodworkersTool()
            : base(0x1030)
        {
            Weight = 2.0;
        }

        public WoodworkersTool(int uses, int itemID)
            : base(uses, itemID)
        {
        }

        public WoodworkersTool(Serial serial)
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