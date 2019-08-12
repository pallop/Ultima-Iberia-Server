/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;

namespace Server.Items
{
    public class FoundationItem : ResourceItem
    {
        [Constructable]
        public FoundationItem() { Weight = 5.0; }
        public FoundationItem(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodFoundation : FoundationItem
    {
        [Constructable]
        public WoodFoundation() { Name = "Wood Foundation"; ItemID = 0x738; }
        public WoodFoundation(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneFoundation : FoundationItem
    {
        [Constructable]
        public StoneFoundation() { Name = "Stone Foundation"; ItemID = 0x750; }
        public StoneFoundation(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BrickFoundation : FoundationItem
    {
        [Constructable]
        public BrickFoundation() { Name = "Brick Foundation"; ItemID = 0x7A3; }
        public BrickFoundation(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SandstoneFoundation : FoundationItem
    {
        [Constructable]
        public SandstoneFoundation() { Name = "Sandstone Foundation"; ItemID = 0x76C; }
        public SandstoneFoundation(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class MarbleFoundation : FoundationItem
    {
        [Constructable]
        public MarbleFoundation() { Name = "Marble Foundation"; ItemID = 0x709; }
        public MarbleFoundation(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}
