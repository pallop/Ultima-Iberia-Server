/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server;
using Server.Targeting;
using Server.Engines.Build;

namespace Server.Items
{
    public class WoodWorkersBenchEastAddon : BaseAddon
    {
        public override BaseAddonDeed Deed { get { return new WoodWorkersBenchEastDeed(); } }

        [Constructable]
        public WoodWorkersBenchEastAddon()
        {
            AddComponent(new AddonComponent(0x19F3), 0, 0, 0);
            AddComponent(new AddonComponent(0x19F2), 0, 1, 0);
            AddComponent(new AddonComponent(0x19F1), 0, 2, 0);
        }

        public WoodWorkersBenchEastAddon(Serial serial)
            : base(serial)
        {
        }

        public override void OnComponentUsed(AddonComponent c, Mobile from)
        {
            from.SendMessage("Target a woodworking tool.");
            from.Target = new WoodWorkersBenchTarget();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class WoodWorkersBenchTarget : Target
    {
        public WoodWorkersBenchTarget()
            : base(2, false, TargetFlags.None)
        {
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            if (targeted is BaseTool)
            {
                BaseTool baseTool = targeted as BaseTool;
                if (baseTool.CraftSystem == Engines.Craft.DefCarpentry.CraftSystem)
                {
                    WoodworkersTool tool = new WoodworkersTool(baseTool.UsesRemaining, baseTool.ItemID);
                    tool.Name = baseTool.DefaultName + ": a Woodworker's Tool";
                    if (from.AddToBackpack(tool))
                    {
                        baseTool.Delete();
                        from.SendGump(new BuildGump(from, DefWoodworking.BuildSystem, tool, ""));
                    }
                }
                else
                    from.SendMessage("That is not a woodworking tool.");
            }
            else if (targeted is BaseBuildingTool)
            {
                BaseBuildingTool tool = targeted as BaseBuildingTool;
                BuildSystem syst = tool.BuildSystem;
                if (syst == DefWoodworking.BuildSystem)
                {
                    from.SendGump(new BuildGump(from, syst, tool, ""));
                }
                else
                    from.SendMessage("That is not a woodworking tool.");
            }
            else
                from.SendMessage("That is not a woodworking tool.");
        }
    }

    public class WoodWorkersBenchEastDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new WoodWorkersBenchEastAddon(); } }
        public override int LabelNumber { get { return 1026641; } } // woodworker's bench

        [Constructable]
        public WoodWorkersBenchEastDeed()
        {
        }

        public WoodWorkersBenchEastDeed(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}