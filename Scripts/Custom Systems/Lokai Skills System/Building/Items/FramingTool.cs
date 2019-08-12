/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server;
using Server.Engines.Build;

namespace Server.Items
{
    [Flipable(0x1030, 0x1031)]
    public class FramingTool : BaseBuildingTool
    {
        public override BuildSystem BuildSystem { get { return DefFraming.BuildSystem; } }
        public override string DefaultName { get { return "Framing Tool"; } }

        [Constructable]
        public FramingTool()
            : base(0x1030)
        {
            Weight = 2.0;
        }

        public FramingTool(Serial serial)
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