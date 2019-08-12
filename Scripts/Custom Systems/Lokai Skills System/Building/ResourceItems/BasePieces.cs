/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;

namespace Server.Items
{
    public class ResourceItem : Item
    {
        public ResourceItem() {}
        public ResourceItem(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BasePieceItem : ResourceItem
    {
        public BasePieceItem() { Weight = 5.0; }
        public BasePieceItem(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodPiece : BasePieceItem
    {
        [Constructable]
        public WoodPiece() { Name = "Wood Piece"; ItemID = 0x18; }
        public WoodPiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StonePiece : BasePieceItem
    {
        [Constructable]
        public StonePiece() { Name = "Stone Piece"; ItemID = 0x69; }
        public StonePiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BrickPiece : BasePieceItem
    {
        [Constructable]
        public BrickPiece() { Name = "Brick Piece"; ItemID = 0x55; }
        public BrickPiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class LogPiece : BasePieceItem
    {
        [Constructable]
        public LogPiece() { Name = "Log Piece"; ItemID = 0xA2; }
        public LogPiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WroughtIronPiece : BasePieceItem
    {
        [Constructable]
        public WroughtIronPiece() { Name = "Wrought Iron Piece"; ItemID = 0x084B; }
        public WroughtIronPiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SandstonePiece : BasePieceItem
    {
        [Constructable]
        public SandstonePiece() { Name = "Sandstone Piece"; ItemID = 0x199; }
        public SandstonePiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class MarblePiece : BasePieceItem
    {
        [Constructable]
        public MarblePiece() { Name = "Marble Piece"; ItemID = 0x2B6; }
        public MarblePiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class TilePiece : BasePieceItem
    {
        [Constructable]
        public TilePiece() { Name = "Tile Piece"; ItemID = 0xA2; }
        public TilePiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class MetalPiece : BasePieceItem
    {
        [Constructable]
        public MetalPiece() { Name = "Metal Piece"; ItemID = 0x084B; }
        public MetalPiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ThatchPiece : BasePieceItem
    {
        [Constructable]
        public ThatchPiece() { Name = "Thatch Piece"; ItemID = 0x199; }
        public ThatchPiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class PalmPiece : BasePieceItem
    {
        [Constructable]
        public PalmPiece() { Name = "Palm Piece"; ItemID = 0x199; }
        public PalmPiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SlatePiece : BasePieceItem
    {
        [Constructable]
        public SlatePiece() { Name = "Slate Piece"; ItemID = 0x2B6; }
        public SlatePiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class RugPiece : BasePieceItem
    {
        [Constructable]
        public RugPiece() { Name = "Rug Piece"; ItemID = 0x1AE; }
        public RugPiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class RatanPiece : BasePieceItem
    {
        [Constructable]
        public RatanPiece() { Name = "Ratan Piece"; ItemID = 0x1AE; }
        public RatanPiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class HidePiece : BasePieceItem
    {
        [Constructable]
        public HidePiece() { Name = "Hide Piece"; ItemID = 0x1069; }
        public HidePiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BambooPiece : BasePieceItem
    {
        [Constructable]
        public BambooPiece() { Name = "Bamboo Piece"; ItemID = 0x11D8; }
        public BambooPiece(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}
