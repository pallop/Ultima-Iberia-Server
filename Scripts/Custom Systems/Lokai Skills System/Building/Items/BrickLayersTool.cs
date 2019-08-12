/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server;
using Server.Engines.Build;

namespace Server.Items
{
    public class BrickLayersTool : BaseBuildingTool
    {
        public override BuildSystem BuildSystem { get { return DefBrickLaying.BuildSystem; } }
        public override string DefaultName { get { return "Brick Layer's Tool"; } }

        [Constructable]
        public BrickLayersTool()
            : base(0x10E4)
        {
            Weight = 2.0;
        }

        public BrickLayersTool(Serial serial)
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