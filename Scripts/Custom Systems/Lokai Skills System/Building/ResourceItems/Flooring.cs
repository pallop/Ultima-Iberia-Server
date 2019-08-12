/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;

namespace Server.Items
{
    public class FlooringItem : ResourceItem
    {
        [Constructable]
        public FlooringItem() { Weight = 5.0; }
        public FlooringItem(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodFlooring : FlooringItem
    {
        [Constructable]
        public WoodFlooring() { Name = "Wood Flooring"; ItemID = 0x4C6; }
        public WoodFlooring(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class TileFlooring : FlooringItem
    {
        [Constructable]
        public TileFlooring() { Name = "Tile Flooring"; ItemID = 0x4F2; }
        public TileFlooring(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class RugFlooring : FlooringItem
    {
        [Constructable]
        public RugFlooring() { Name = "Rug Flooring"; ItemID = 0xABC; }
        public RugFlooring(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneFlooring : FlooringItem
    {
        [Constructable]
        public StoneFlooring() { Name = "Stone Flooring"; ItemID = 0x51D; }
        public StoneFlooring(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SandStoneFlooring : FlooringItem
    {
        [Constructable]
        public SandStoneFlooring() { Name = "SandStone Flooring"; ItemID = 0x52F; }
        public SandStoneFlooring(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class FlagStoneFlooring : FlooringItem
    {
        [Constructable]
        public FlagStoneFlooring() { Name = "FlagStone Flooring"; ItemID = 0x4FF; }
        public FlagStoneFlooring(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class CobbleStoneFlooring : FlooringItem
    {
        [Constructable]
        public CobbleStoneFlooring() { Name = "CobbleStone Flooring"; ItemID = 0x518; }
        public CobbleStoneFlooring(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BrickFlooring : FlooringItem
    {
        [Constructable]
        public BrickFlooring() { Name = "Brick Flooring"; ItemID = 0x4E7; }
        public BrickFlooring(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class MarbleFlooring : FlooringItem
    {
        [Constructable]
        public MarbleFlooring() { Name = "Marble Flooring"; ItemID = 0x50D; }
        public MarbleFlooring(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}
