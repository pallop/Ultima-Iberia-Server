/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server;
using Server.Engines.Build;

namespace Server.Items
{
    public class StoneMasonsTool : BaseBuildingTool
    {
        public override BuildSystem BuildSystem { get { return DefStoneMasonry.BuildSystem; } }
        public override string DefaultName { get { return "Stone Mason's Tool"; } }

        [Constructable]
        public StoneMasonsTool()
            : base(0x12B3)
        {
            Weight = 2.0;
        }

        public StoneMasonsTool(Serial serial)
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