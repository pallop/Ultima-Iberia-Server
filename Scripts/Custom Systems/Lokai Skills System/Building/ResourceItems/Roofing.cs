/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;

namespace Server.Items
{
    public class RoofingItem : ResourceItem
    {
        [Constructable]
        public RoofingItem() { Weight = 5.0; }
        public RoofingItem(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
//ShingleRoofing
    public class ShingleRoofing : RoofingItem
    {
        [Constructable]
        public ShingleRoofing() { Name = "Shingle Roofing1"; ItemID = 0x2C3F; }
        public ShingleRoofing(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ShingleRoofing2 : RoofingItem
    {
        [Constructable]
        public ShingleRoofing2() { Name = "Shingle Roofing2"; ItemID = 0x5C2; }
        public ShingleRoofing2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ShingleRoofingS : RoofingItem
    {
        [Constructable]
        public ShingleRoofingS() { Name = "Shingle Roofing Sur"; ItemID = 0x2173; }
        public ShingleRoofingS(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ShingleRoofingE : RoofingItem
    {
        [Constructable]
        public ShingleRoofingE() { Name = "Shingle Roofing Este"; ItemID = 0x2170; }
        public ShingleRoofingE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ShingleRoofingN : RoofingItem
    {
        [Constructable]
        public ShingleRoofingN() { Name = "Shingle Roofing Norte"; ItemID = 0x2177; }
        public ShingleRoofingN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ShingleRoofingW : RoofingItem
    {
        [Constructable]
        public ShingleRoofingW() { Name = "Shingle Roofing Oeste"; ItemID = 0x2175; }
        public ShingleRoofingW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ShingleRoofingEsquinaS : RoofingItem
    {
        [Constructable]
        public ShingleRoofingEsquinaS() { Name = "Shingle Roofing Esquina Sur"; ItemID = 0x5C8; }
        public ShingleRoofingEsquinaS(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ShingleRoofingEsquinaE : RoofingItem
    {
        [Constructable]
        public ShingleRoofingEsquinaE() { Name = "Shingle Roofing Esquina Este"; ItemID = 0x5C6; }
        public ShingleRoofingEsquinaE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ShingleRoofingEsquinaN : RoofingItem
    {
        [Constructable]
        public ShingleRoofingEsquinaN() { Name = "Shingle Roofing Esquina Norte"; ItemID = 0x5C7; }
        public ShingleRoofingEsquinaN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ShingleRoofingEsquinaW : RoofingItem
    {
        [Constructable]
        public ShingleRoofingEsquinaW() { Name = "Shingle Roofing Esquina Oeste"; ItemID = 0x5C5; }
        public ShingleRoofingEsquinaW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }




//TileRoofing
    public class TileRoofing : RoofingItem
    {
        [Constructable]
        public TileRoofing() { Name = "Tile Roofing"; ItemID = 0x2C30; }
        public TileRoofing(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class TileRoofing2 : RoofingItem
    {
        [Constructable]
        public TileRoofing2() { Name = "Tile Roofing2"; ItemID = 0x5B3; }
        public TileRoofing2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class TileRoofingS : RoofingItem
    {
        [Constructable]
        public TileRoofingS() { Name = "Tile Roofing Sur"; ItemID = 0x5C0; }
        public TileRoofingS(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class TileRoofingE : RoofingItem
    {
        [Constructable]
        public TileRoofingE() { Name = "Tile Roofing Este"; ItemID = 0x21E9; }
        public TileRoofingE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class TileRoofingN : RoofingItem
    {
        [Constructable]
        public TileRoofingN() { Name = "Tile Roofing Norte"; ItemID = 0x5C1; }
        public TileRoofingN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class TileRoofingW : RoofingItem
    {
        [Constructable]
        public TileRoofingW() { Name = "Tile Roofing Oeste"; ItemID = 0x21EB; }
        public TileRoofingW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class TileRoofingEsquinaS : RoofingItem
    {
        [Constructable]
        public TileRoofingEsquinaS() { Name = "Tile Roofing Esquina Sur"; ItemID = 0x6FD; }
        public TileRoofingEsquinaS(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class TileRoofingEsquinaE : RoofingItem
    {
        [Constructable]
        public TileRoofingEsquinaE() { Name = "Tile Roofing Esquina Este"; ItemID = 0x700; }
        public TileRoofingEsquinaE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class TileRoofingEsquinaN : RoofingItem
    {
        [Constructable]
        public TileRoofingEsquinaN() { Name = "Tile Roofing Esquina Norte"; ItemID = 0x6FF; }
        public TileRoofingEsquinaN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class TileRoofingEsquinaW : RoofingItem
    {
        [Constructable]
        public TileRoofingEsquinaW() { Name = "Tile Roofing Esquina Oeste"; ItemID = 0x6FE; }
        public TileRoofingEsquinaW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//LogRoofing
    public class LogRoofing : RoofingItem
    {
        [Constructable]
        public LogRoofing() { Name = "Log Roofing"; ItemID = 0x2C4E; }
        public LogRoofing(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class LogRoofing2 : RoofingItem
    {
        [Constructable]
        public LogRoofing2() { Name = "Log Roofing2"; ItemID = 0x5F0; }
        public LogRoofing2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class LogRoofingS : RoofingItem
    {
        [Constructable]
        public LogRoofingS() { Name = "Log Roofing Sur"; ItemID = 0x5FD; }
        public LogRoofingS(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class LogRoofingE : RoofingItem
    {
        [Constructable]
        public LogRoofingE() { Name = "Log Roofing Este"; ItemID = 0x5F1; }
        public LogRoofingE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class LogRoofingN : RoofingItem
    {
        [Constructable]
        public LogRoofingN() { Name = "Log Roofing Norte"; ItemID = 0x5FE; }
        public LogRoofingN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class LogRoofingW : RoofingItem
    {
        [Constructable]
        public LogRoofingW() { Name = "Log Roofing Oeste"; ItemID = 0x5F2; }
        public LogRoofingW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class LogRoofingEsquinaS : RoofingItem
    {
        [Constructable]
        public LogRoofingEsquinaS() { Name = "Log Roofing Esquina Sur"; ItemID = 0x5F6; }
        public LogRoofingEsquinaS(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class LogRoofingEsquinaE : RoofingItem
    {
        [Constructable]
        public LogRoofingEsquinaE() { Name = "Log Roofing Esquina Este"; ItemID = 0x5F4; }
        public LogRoofingEsquinaE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class LogRoofingEsquinaN : RoofingItem
    {
        [Constructable]
        public LogRoofingEsquinaN() { Name = "Log Roofing Esquina Norte"; ItemID = 0x5F5; }
        public LogRoofingEsquinaN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class LogRoofingEsquinaW : RoofingItem
    {
        [Constructable]
        public LogRoofingEsquinaW() { Name = "Log Roofing Esquina Oeste"; ItemID = 0x5F3; }
        public LogRoofingEsquinaW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//ThatchRoofing
    public class ThatchRoofing : RoofingItem
    {
        [Constructable]
        public ThatchRoofing() { Name = "Thatch Roofing"; ItemID = 0x26EA; }
        public ThatchRoofing(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ThatchRoofing2 : RoofingItem
    {
        [Constructable]
        public ThatchRoofing2() { Name = "Thatch Roofing2"; ItemID = 0x26DA; }
        public ThatchRoofing2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ThatchRoofingS : RoofingItem
    {
        [Constructable]
        public ThatchRoofingS() { Name = "Thatch Roofing Sur"; ItemID = 0x26EB; }
        public ThatchRoofingS(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ThatchRoofingE : RoofingItem
    {
        [Constructable]
        public ThatchRoofingE() { Name = "Thatch Roofing Este"; ItemID = 0x26DB; }
        public ThatchRoofingE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ThatchRoofingN : RoofingItem
    {
        [Constructable]
        public ThatchRoofingN() { Name = "Thatch Roofing Norte"; ItemID = 0x26EC; }
        public ThatchRoofingN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ThatchRoofingW : RoofingItem
    {
        [Constructable]
        public ThatchRoofingW() { Name = "Thatch Roofing Oeste"; ItemID = 0x26DC; }
        public ThatchRoofingW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ThatchRoofingEsquinaS : RoofingItem
    {
        [Constructable]
        public ThatchRoofingEsquinaS() { Name = "Thatch Roofing Esquina Sur"; ItemID = 0x26E6; }
        public ThatchRoofingEsquinaS(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ThatchRoofingEsquinaE : RoofingItem
    {
        [Constructable]
        public ThatchRoofingEsquinaE() { Name = "Thatch Roofing Esquina Este"; ItemID = 0x26E9; }
        public ThatchRoofingEsquinaE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ThatchRoofingEsquinaN : RoofingItem
    {
        [Constructable]
        public ThatchRoofingEsquinaN() { Name = "Thatch Roofing Esquina Norte"; ItemID = 0x26E7; }
        public ThatchRoofingEsquinaN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ThatchRoofingEsquinaW : RoofingItem
    {
        [Constructable]
        public ThatchRoofingEsquinaW() { Name = "Thatch Roofing Esquina Oeste"; ItemID = 0x26E8; }
        public ThatchRoofingEsquinaW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//PalmRoofing

    public class PalmRoofing : RoofingItem
    {
        [Constructable]
        public PalmRoofing() { Name = "Palm Roofing"; ItemID = 0x593; }
        public PalmRoofing(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class PalmRoofing2 : RoofingItem
    {
        [Constructable]
        public PalmRoofing2() { Name = "Palm Roofing2"; ItemID = 0x590; }
        public PalmRoofing2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class PalmRoofingS : RoofingItem
    {
        [Constructable]
        public PalmRoofingS() { Name = "Palm Roofing Sur"; ItemID = 0x592; }
        public PalmRoofingS(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class PalmRoofingE : RoofingItem
    {
        [Constructable]
        public PalmRoofingE() { Name = "Palm Roofing Este"; ItemID = 0x58F; }
        public PalmRoofingE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class PalmRoofingN : RoofingItem
    {
        [Constructable]
        public PalmRoofingN() { Name = "Palm Roofing Norte"; ItemID = 0x594; }
        public PalmRoofingN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class PalmRoofingW : RoofingItem
    {
        [Constructable]
        public PalmRoofingW() { Name = "Palm Roofing Oeste"; ItemID = 0x591; }
        public PalmRoofingW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class PalmRoofingEsquinaS : RoofingItem
    {
        [Constructable]
        public PalmRoofingEsquinaS() { Name = "Palm Roofing Esquina Sur"; ItemID = 0x589; }
        public PalmRoofingEsquinaS(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class PalmRoofingEsquinaE : RoofingItem
    {
        [Constructable] 
        public PalmRoofingEsquinaE() { Name = "Palm Roofing Esquina Este"; ItemID = 0x587; }
        public PalmRoofingEsquinaE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class PalmRoofingEsquinaN : RoofingItem
    {
        [Constructable]
        public PalmRoofingEsquinaN() { Name = "Palm Roofing Esquina Norte"; ItemID = 0x588; }
        public PalmRoofingEsquinaN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class PalmRoofingEsquinaW : RoofingItem
    {
        [Constructable]
        public PalmRoofingEsquinaW() { Name = "Palm Roofing Esquina Oeste"; ItemID = 0x586; }
        public PalmRoofingEsquinaW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//SlateRoofing
    
    public class SlateRoofing : RoofingItem
    {
        [Constructable]
        public SlateRoofing() { Name = "Slate Roofing"; ItemID = 0x2C7B; }
        public SlateRoofing(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SlateRoofing2 : RoofingItem
    {
        [Constructable]
        public SlateRoofing2() { Name = "Slate Roofing2"; ItemID = 0x595; }
        public SlateRoofing2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SlateRoofingS : RoofingItem
    {
        [Constructable]
        public SlateRoofingS() { Name = "Slate Roofing Sur"; ItemID = 0x217C; }
        public SlateRoofingS(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SlateRoofingE : RoofingItem
    {
        [Constructable]
        public SlateRoofingE() { Name = "Slate Roofing Este"; ItemID = 0x2178; }
        public SlateRoofingE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SlateRoofingN : RoofingItem
    {
        [Constructable]
        public SlateRoofingN() { Name = "Slate Roofing Norte"; ItemID = 0x217E; }
        public SlateRoofingN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SlateRoofingW : RoofingItem
    {
        [Constructable]
        public SlateRoofingW() { Name = "Slate Roofing Oeste"; ItemID = 0x217A; }
        public SlateRoofingW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SlateRoofingEsquinaS : RoofingItem
    {
        [Constructable]
        public SlateRoofingEsquinaS() { Name = "Slate Roofing Esquina Sur"; ItemID = 0x59B; }
        public SlateRoofingEsquinaS(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SlateRoofingEsquinaE : RoofingItem
    {
        [Constructable]
        public SlateRoofingEsquinaE() { Name = "Slate Roofing Esquina Este"; ItemID = 0x599; }
        public SlateRoofingEsquinaE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SlateRoofingEsquinaN : RoofingItem
    {
        [Constructable]
        public SlateRoofingEsquinaN() { Name = "Slate Roofing Esquina Norte"; ItemID = 0x59A; }
        public SlateRoofingEsquinaN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SlateRoofingEsquinaW : RoofingItem
    {
        [Constructable]
        public SlateRoofingEsquinaW() { Name = "Slate Roofing Esquina Oeste"; ItemID = 0x598; }
        public SlateRoofingEsquinaW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

}
