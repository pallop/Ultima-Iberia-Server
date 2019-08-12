/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;

namespace Server.Items
{
    public class SupplyItem : ResourceItem
    {
        [Constructable]
        public SupplyItem() { Weight = 0.1; }
        public SupplyItem(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class CementSupply : SupplyItem
    {
        [Constructable]
        public CementSupply() { Name = "Cement Supply"; ItemID = 0x1BE8; }
        public CementSupply(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class MortarSupply : SupplyItem
    {
        [Constructable]
        public MortarSupply() { Name = "Mortar Supply"; ItemID = 0xE77; }
        public MortarSupply(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class PitchSupply : SupplyItem
    {
        [Constructable]
        public PitchSupply() { Name = "Pitch Supply"; ItemID = 0xFAB; }
        public PitchSupply(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class TarSupply : SupplyItem
    {
        [Constructable]
        public TarSupply() { Name = "Tar Supply"; ItemID = 0xFAB; }
        public TarSupply(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class NailSupply : SupplyItem
    {
        [Constructable]
        public NailSupply() { Name = "Nail Supply"; ItemID = 0x102E; }
        public NailSupply(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    class JointSupply : SupplyItem
    {
        [Constructable]
        public JointSupply() { Name = "Joint Supply"; ItemID = 0xF9D; }
        public JointSupply(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    class HingeSupply : SupplyItem
    {
        [Constructable]
        public HingeSupply() { Name = "Hinge Supply"; ItemID = 0x1055; }
        public HingeSupply(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class PaintSupply : SupplyItem
    {
        [Constructable]
        public PaintSupply() { Name = "Paint Supply"; ItemID = 0xE7F; }
        public PaintSupply(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StainSupply : SupplyItem
    {
        [Constructable]
        public StainSupply() { Name = "Stain Supply"; ItemID = 0xE7F; }
        public StainSupply(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}
