/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;

namespace Server.Items
{
    public class StairItem : ResourceItem
    {
        [Constructable]
        public StairItem() { Weight = 5.0; }
        public StairItem(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
//WOOD

    public class WoodStair : StairItem
    {
        [Constructable]
        public WoodStair() { Name = "Wood Stair Sur"; ItemID = 0x7339; }
        public WoodStair(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodStairE : StairItem
    {
        [Constructable]
        public WoodStairE() { Name = "Wood Stair Este"; ItemID = 0x73A; }
        public WoodStairE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodStairN : StairItem
    {
        [Constructable]
        public WoodStairN() { Name = "Wood Stair Norte"; ItemID = 0x73B; }
        public WoodStairN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodStairW : StairItem
    {
        [Constructable]
        public WoodStairW() { Name = "Wood Stair Oeste"; ItemID = 0x73C; }
        public WoodStairW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//STONE
    public class StoneStair : StairItem
    {
        [Constructable]
        public StoneStair() { Name = "Stone Stair Sur"; ItemID = 0x71F; }
        public StoneStair(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneStairE : StairItem
    {
        [Constructable]
        public StoneStairE() { Name = "Stone Stair Este"; ItemID = 0x736; }
        public StoneStairE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneStairN : StairItem
    {
        [Constructable]
        public StoneStairN() { Name = "Stone Stair Norte"; ItemID = 0x737; }
        public StoneStairN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneStairw : StairItem
    {
        [Constructable]
        public StoneStairw() { Name = "Stone Stair Oeste"; ItemID = 0x749; }
        public StoneStairw(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//sandstone

    public class SandstoneStair : StairItem
    {
        [Constructable]
        public SandstoneStair() { Name = "Sandstone Stair Sur"; ItemID = 0x76D; }
        public SandstoneStair(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SandstoneStairE : StairItem
    {
        [Constructable]
        public SandstoneStairE() { Name = "Sandstone Stair Este"; ItemID = 0x76E; }
        public SandstoneStairE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SandstoneStairN : StairItem
    {
        [Constructable]
        public SandstoneStairN() { Name = "Sandstone Stair Norte"; ItemID = 0x76F; }
        public SandstoneStairN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SandstoneStairW : StairItem
    {
        [Constructable]
        public SandstoneStairW() { Name = "Sandstone Stair Oeste"; ItemID = 0x770; }
        public SandstoneStairW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//marble

    public class MarbleStair : StairItem
    {
        [Constructable]
        public MarbleStair() { Name = "Marble Stair Sur"; ItemID = 0x70A; }
        public MarbleStair(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class MarbleStairE : StairItem
    {
        [Constructable]
        public MarbleStairE() { Name = "Marble Stair Este"; ItemID = 0x70B; }
        public MarbleStairE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class MarbleStairN : StairItem
    {
        [Constructable]
        public MarbleStairN() { Name = "Marble Stair Norte"; ItemID = 0x70C; }
        public MarbleStairN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class MarbleStairW : StairItem
    {
        [Constructable]
        public MarbleStairW() { Name = "Marble Stair Oeste"; ItemID = 0x70D; }
        public MarbleStairW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//Brick
    public class BrickStair : StairItem
    {
        [Constructable]
        public BrickStair() { Name = "Brick Stair Sur"; ItemID = 0x70A; }
        public BrickStair(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BrickStairE : StairItem
    {
        [Constructable]
        public BrickStairE() { Name = "Brick Stair Este"; ItemID = 0x70A; }
        public BrickStairE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BrickStairN : StairItem
    {
        [Constructable]
        public BrickStairN() { Name = "Brick Stair Norte"; ItemID = 0x70A; }
        public BrickStairN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BrickStairW : StairItem
    {
        [Constructable]
        public BrickStairW() { Name = "Brick Stair Oeste"; ItemID = 0x70A; }
        public BrickStairW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}
