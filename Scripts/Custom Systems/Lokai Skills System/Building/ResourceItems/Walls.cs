/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;

namespace Server.Items
{
    public class WallItem : ResourceItem
    {
        [Constructable]
        public WallItem() { Weight = 5.0; }
        public WallItem(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    //woodwall1
    public class WoodWall : WallItem
    {
        [Constructable]
        public WoodWall() { Name = "Wood Wall Sur"; ItemID = 0x7; }
        public WoodWall(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodWallEst : WallItem
    {
        [Constructable]
        public WoodWallEst() { Name = "Wood Wall Este"; ItemID = 0x8; }
        public WoodWallEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodWallEst1 : WallItem
    {
        [Constructable]
        public WoodWallEst1() { Name = "Wood Wall Este1"; ItemID = 0xD; }
        public WoodWallEst1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodWallEst2 : WallItem
    {
        [Constructable]
        public WoodWallEst2() { Name = "Wood Wall Este2"; ItemID = 0xB; }
        public WoodWallEst2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodWallSur1 : WallItem
    {
        [Constructable]
        public WoodWallSur1() { Name = "Wood Wall Sur1"; ItemID = 0xC; }
        public WoodWallSur1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodWallSur2 : WallItem
    {
        [Constructable]
        public WoodWallSur2() { Name = "Wood Wall Sur2"; ItemID = 0xA; }
        public WoodWallSur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class WoodWallEsquina : WallItem
    {
        [Constructable]
        public WoodWallEsquina() { Name = "Wood Wall Esquina"; ItemID = 0x6; }
        public WoodWallEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }




//woodwall2

    public class WoodWall2Sur : WallItem
    {
        [Constructable]
        public WoodWall2Sur() { Name = "Wood Wall2 Sur"; ItemID = 0x21D7; }
        public WoodWall2Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodWall2Sur2 : WallItem
    {
        [Constructable]
        public WoodWall2Sur2() { Name = "Wood Wall2 Sur2"; ItemID = 0x21D9; }
        public WoodWall2Sur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodWall2Est : WallItem
    {
        [Constructable]
        public WoodWall2Est() { Name = "Wood Wall2 Este"; ItemID = 0xA7; }
        public WoodWall2Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodWall2Est2 : WallItem
    {
        [Constructable]
        public WoodWall2Est2() { Name = "Wood Wall2 Este2"; ItemID = 0x21DB; }
        public WoodWall2Est2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodWall2Esquina : WallItem
    {
        [Constructable]
        public WoodWall2Esquina() { Name = "Wood Wall2 Esquina"; ItemID = 0xA6; }
        public WoodWall2Esquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
//woodwall3


  public class WoodWall3Sur : WallItem
    {
        [Constructable]
        public WoodWall3Sur() { Name = "Wood Wall3 Sur"; ItemID = 0x228; }
        public WoodWall3Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class WoodWall3Sur2 : WallItem
    {
        [Constructable]
        public WoodWall3Sur2() { Name = "Wood Wall3 Sur2"; ItemID = 0x222; }
        public WoodWall3Sur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodWall3Est : WallItem
    {
        [Constructable]
        public WoodWall3Est() { Name = "Wood Wall3 Este"; ItemID = 0x227; }
        public WoodWall3Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodWall3Est2 : WallItem
    {
        [Constructable]
        public WoodWall3Est2() { Name = "Wood Wall3 Este2"; ItemID = 0x221; }
        public WoodWall3Est2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodWall3Esquina : WallItem
    {
        [Constructable]
        public WoodWall3Esquina() { Name = "Wood Wall3 Esquina "; ItemID = 0x226; }
        public WoodWall3Esquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodWall3Esquina2 : WallItem
    {
        [Constructable]
        public WoodWall3Esquina2() { Name = "Wood Wall3 Esquina2"; ItemID = 0x223; }
        public WoodWall3Esquina2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
//woodwall4
    public class WoodWall4Sur : WallItem
    {
        [Constructable]
        public WoodWall4Sur() { Name = "Wood Wall4 Sur"; ItemID = 0x2862; }
        public WoodWall4Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodWall4Esquina : WallItem
    {
        [Constructable]
        public WoodWall4Esquina() { Name = "Wood Wall4 Esquina "; ItemID = 0x285F; }
        public WoodWall4Esquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class  WoodWall4Est : WallItem
    {
        [Constructable]
        public WoodWall4Est() { Name = "Wood Wall4 Este"; ItemID = 0x2866; }
        public WoodWall4Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
//woodwall5
public class WoodWall5Sur : WallItem
    {
        [Constructable]
        public WoodWall5Sur() { Name = "Wood Wall5 Sur"; ItemID = 0x299A; }
        public WoodWall5Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodWall5Esquina : WallItem
    {
        [Constructable]
        public WoodWall5Esquina() { Name = "Wood Wall5 Esquina "; ItemID = 0x299D; }
        public WoodWall5Esquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class  WoodWall5Est : WallItem
    {
        [Constructable]
        public WoodWall5Est() { Name = "Wood Wall5 Este"; ItemID = 0x2997; }
        public WoodWall5Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
//LogWall
    public class LogWall : WallItem
    {
        [Constructable]
        public LogWall() { Name = "Log Wall"; ItemID = 0x95; }
        public LogWall(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 //Stone1


    public class StoneWall : WallItem
    {
        [Constructable]
        public StoneWall() { Name = "Stone Wall Sur"; ItemID = 0x1C; }
        public StoneWall(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class StoneWallSur1 : WallItem
    {
        [Constructable]
        public StoneWallSur1() { Name = "Stone Wall Sur1"; ItemID = 0x1E; }
        public StoneWallSur1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class StoneWallSur2 : WallItem
    {
        [Constructable]
        public StoneWallSur2() { Name = "Stone Wall Sur2"; ItemID = 0x21; }
        public StoneWallSur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class StoneWallEst : WallItem
    {
        [Constructable]
        public StoneWallEst() { Name = "Stone Wall Este"; ItemID = 0x1B; }
        public StoneWallEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class StoneWallEst1 : WallItem
    {
        [Constructable]
        public StoneWallEst1() { Name = "Stone Wall Este1"; ItemID = 0x1F; }
        public StoneWallEst1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class StoneWallEst2 : WallItem
    {
        [Constructable]
        public StoneWallEst2() { Name = "Stone Wall  Este2"; ItemID = 0x20; }
        public StoneWallEst2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class StoneWallEsquina : WallItem
    {
        [Constructable]
        public StoneWallEsquina() { Name = "Stone Wall Esquina"; ItemID = 0x1A; }
        public StoneWallEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }



 //Stone3
 public class StoneWall3 : WallItem
    {
        [Constructable]
        public StoneWall3() { Name = "Stone Wall 3"; ItemID = 0x84; }
        public StoneWall3(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class StoneWall3ArcoEst1 : WallItem
    {
        [Constructable]
        public StoneWall3ArcoEst1() { Name = "Stone Wall3 Arco Este 1"; ItemID = 0x7B; }
        public StoneWall3ArcoEst1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class StoneWall3ArcoEst2 : WallItem
    {
        [Constructable]
        public StoneWall3ArcoEst2() { Name = "Stone Wall3 Arco Este 2"; ItemID = 0x8B; }
        public StoneWall3ArcoEst2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class StoneWall3ArcoSur1 : WallItem
    {
        [Constructable]
        public StoneWall3ArcoSur1() { Name = "Stone Wall3  Arco Sur 1"; ItemID = 0x7A; }
        public StoneWall3ArcoSur1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class StoneWall3ArcoSur2 : WallItem
    {
        [Constructable]
        public StoneWall3ArcoSur2() { Name = "Stone Wall 3 Arco Sur 2"; ItemID = 0x8A; }
        public StoneWall3ArcoSur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//Stone4

    public class StoneWall4Pilar : WallItem
    {
        [Constructable]
        public StoneWall4Pilar() { Name = "Stone Wall 4 Pilar"; ItemID = 0xC5; }
        public StoneWall4Pilar(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall4Esquina : WallItem
    {
        [Constructable]
        public StoneWall4Esquina() { Name = "Stone Wall 4 Esquina"; ItemID = 0xC7; }
        public StoneWall4Esquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall4Sur : WallItem
    {
        [Constructable]
        public StoneWall4Sur() { Name = "Stone Wall 4 Sur "; ItemID = 0xC8; }
        public StoneWall4Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall4Est : WallItem
    {
        [Constructable]
        public StoneWall4Est() { Name = "Stone Wall 4 Este"; ItemID = 0xC9; }
        public StoneWall4Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall4EsquinaArco : WallItem
    {
        [Constructable]
        public StoneWall4EsquinaArco() { Name = "Stone Wall 4 Esquina Arco"; ItemID = 0xCD; }
        public StoneWall4EsquinaArco(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall4ArcoSur1 : WallItem
    {
        [Constructable]
        public StoneWall4ArcoSur1() { Name = "Stone Wall 4 Arco Sur 1"; ItemID = 0xD1; }
        public StoneWall4ArcoSur1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall4ArcoSur2 : WallItem
    {
        [Constructable]
        public StoneWall4ArcoSur2() { Name = "Stone Wall 4 Arco Sur 2"; ItemID = 0xCF; }
        public StoneWall4ArcoSur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall4ArcoEst1: WallItem
    {
        [Constructable]
        public StoneWall4ArcoEst1() { Name = "Stone Wall 4 Arco Este 1"; ItemID = 0xD0; }
        public StoneWall4ArcoEst1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall4ArcoEst2 : WallItem
    {
        [Constructable]
        public StoneWall4ArcoEst2() { Name = "Stone Wall 4 Arco Este 2"; ItemID = 0xCE; }
        public StoneWall4ArcoEst2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//Stone5

    
    public class StoneWall5Esquina : WallItem
    {
        [Constructable]
        public StoneWall5Esquina() { Name = "Stone Wall 5 Esquina"; ItemID = 0x1CF; }
        public StoneWall5Esquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall5Sur : WallItem
    {
        [Constructable]
        public StoneWall5Sur() { Name = "Stone Wall 5 Sur "; ItemID = 0x1D0; }
        public StoneWall5Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall5Est : WallItem
    {
        [Constructable]
        public StoneWall5Est() { Name = "Stone Wall 5 Este"; ItemID = 0x1D1; }
        public StoneWall5Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall5EsquinaArco : WallItem
    {
        [Constructable]
        public StoneWall5EsquinaArco() { Name = "Stone Wall 5 Esquina Arco"; ItemID = 0x1D5; }
        public StoneWall5EsquinaArco(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall5ArcoSur1 : WallItem
    {
        [Constructable]
        public StoneWall5ArcoSur1() { Name = "Stone Wall 5 Arco Sur 1"; ItemID = 0x1D6; }
        public StoneWall5ArcoSur1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall5ArcoSur2 : WallItem
    {
        [Constructable]
        public StoneWall5ArcoSur2() { Name = "Stone Wall 5 Arco Sur 2"; ItemID = 0x1D7; }
        public StoneWall5ArcoSur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall5ArcoEst1: WallItem
    {
        [Constructable]
        public StoneWall5ArcoEst1() { Name = "Stone Wall 5 Arco Este 1"; ItemID = 0x1D8; }
        public StoneWall5ArcoEst1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall5ArcoEst2 : WallItem
    {
        [Constructable]
        public StoneWall5ArcoEst2() { Name = "Stone Wall 5 Arco Este 2"; ItemID = 0x1D9; }
        public StoneWall5ArcoEst2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
//Stone7
    public class StoneWall7Esquina : WallItem
    {
        [Constructable]
        public StoneWall7Esquina() { Name = "Stone Wall 7 Esquina"; ItemID = 0xD3; }
        public StoneWall7Esquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWall7Sur: WallItem
    {
        [Constructable]
        public StoneWall7Sur() { Name = "Stone Wall 7 Sur"; ItemID = 0x3D4; }
        public StoneWall7Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class  StoneWall7Est : WallItem
    {
        [Constructable]
        public StoneWall7Est() { Name = "Stone Wall 7  Este "; ItemID = 0xD5; }
        public StoneWall7Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
//Clay

 public class ClayWallEsquina : WallItem
    {
        [Constructable]
        public ClayWallEsquina() { Name = "Clay Wall  Esquina"; ItemID = 0x2946; }
        public ClayWallEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ClayWallSur: WallItem
    {
        [Constructable]
        public ClayWallSur() { Name = "Clay Wall  Sur"; ItemID = 0x294F; }
        public ClayWallSur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class  ClayWallEst : WallItem
    {
        [Constructable]
        public ClayWallEst() { Name = "Clay Wall   Este "; ItemID = 0x2949; }
        public ClayWallEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }



//BrickWall
    public class BrickWall : WallItem
    {
        [Constructable]
        public BrickWall() { Name = "Brick Wall Sur"; ItemID = 0x58; }
        public BrickWall(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BrickWallEst : WallItem
    {
        [Constructable]
        public BrickWallEst() { Name = "Brick Wall Este"; ItemID = 0x57; }
        public BrickWallEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class BrickWallEsquina : WallItem
    {
        [Constructable]
        public BrickWallEsquina() { Name = "Brick Wall Esquina"; ItemID = 0x59; }
        public BrickWallEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class BrickWallArcoEsquina : WallItem
    {
        [Constructable]
        public BrickWallArcoEsquina() { Name = "Brick Wall Arco Esquina"; ItemID = 0x6D; }
        public BrickWallArcoEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class BrickWallArcoSur1 : WallItem
    {
        [Constructable]
        public BrickWallArcoSur1() { Name = "Brick Wall Arco Sur1"; ItemID = 0x6F; }
        public BrickWallArcoSur1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class BrickWallArcoSur2 : WallItem
    {
        [Constructable]
        public BrickWallArcoSur2() { Name = "Brick Wall Arco Sur 2"; ItemID = 0x70; }
        public BrickWallArcoSur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class BrickWallArcoEst1 : WallItem
    {
        [Constructable]
        public BrickWallArcoEst1() { Name = "Brick Wall  Arco Este 1"; ItemID = 0x71; }
        public BrickWallArcoEst1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class BrickWallArcoEst2 : WallItem
    {
        [Constructable]
        public BrickWallArcoEst2() { Name = "Brick Wall Arco Este 2"; ItemID = 0x6E; }
        public BrickWallArcoEst2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }







//WroughtIronFence
    public class WroughtIronFence : WallItem
    {
        [Constructable]
        public WroughtIronFence() { Name = "Wrought Iron Fence"; ItemID = 0x0823; }
        public WroughtIronFence(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//SandstoneWall
    

    public class SandstoneWall : WallItem
    {
        [Constructable]
        public SandstoneWall() { Name = "Sandstone Wall Sur"; ItemID = 0x24D; }
        public SandstoneWall(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SandstoneWallEst : WallItem
    {
        [Constructable]
        public SandstoneWallEst() { Name = "Sandstone Wall Este"; ItemID = 0x24E; }
        public SandstoneWallEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class SandstoneWallEsquina : WallItem
    {
        [Constructable]
        public SandstoneWallEsquina() { Name = "Sandstone Wall Esquina"; ItemID = 0x24C; }
        public SandstoneWallEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class SandstoneWallArcoEsquina : WallItem
    {
        [Constructable]
        public SandstoneWallArcoEsquina() { Name = "Sandstone Wall Arco Esquina"; ItemID = 0x16C; }
        public SandstoneWallArcoEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class SandstoneWallArcoSur1 : WallItem
    {
        [Constructable]
        public SandstoneWallArcoSur1() { Name = "Sandstone Wall Arco Sur1"; ItemID = 0x170; }
        public SandstoneWallArcoSur1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class SandstoneWallArcoSur2 : WallItem
    {
        [Constructable]
        public SandstoneWallArcoSur2() { Name = "Sandstone Wall Arco Sur 2"; ItemID = 0x16D; }
        public SandstoneWallArcoSur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class SandstoneWallArcoEst1 : WallItem
    {
        [Constructable]
        public SandstoneWallArcoEst1() { Name = "Sandstone Wall  Arco Este 1"; ItemID = 0x16F; }
        public SandstoneWallArcoEst1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class SandstoneWallArcoEst2 : WallItem
    {
        [Constructable]
        public SandstoneWallArcoEst2() { Name = "Sandstone Wall Arco Este 2"; ItemID = 0x16E; }
        public SandstoneWallArcoEst2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//MarbleWall
      public class MarbleWall : WallItem
    {
        [Constructable]
        public MarbleWall() { Name = "Marble Wall Sur"; ItemID = 0xF9; }
        public MarbleWall(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class MarbleWallEst : WallItem
    {
        [Constructable]
        public MarbleWallEst() { Name = "Marble Wall Este"; ItemID = 0xFA; }
        public MarbleWallEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class MarbleWallEsquina : WallItem
    {
        [Constructable]
        public MarbleWallEsquina() { Name = "Marble Wall Esquina"; ItemID = 0xF8; }
        public MarbleWallEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class MarbleWallArcoEsquina : WallItem
    {
        [Constructable]
        public MarbleWallArcoEsquina() { Name = "Marble Wall Arco Esquina"; ItemID = 0x438; }
        public MarbleWallArcoEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class MarbleWallArcoSur1 : WallItem
    {
        [Constructable]
        public MarbleWallArcoSur1() { Name = "Marble Wall Arco Sur1"; ItemID = 0x43A; }
        public MarbleWallArcoSur1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class MarbleWallArcoSur2 : WallItem
    {
        [Constructable]
        public MarbleWallArcoSur2() { Name = "Marble Wall Arco Sur 2"; ItemID = 0x439; }
        public MarbleWallArcoSur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class MarbleWallArcoEst1 : WallItem
    {
        [Constructable]
        public MarbleWallArcoEst1() { Name = "Marble Wall  Arco Este 1"; ItemID = 0x43C; }
        public MarbleWallArcoEst1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

public class MarbleWallArcoEst2 : WallItem
    {
        [Constructable]
        public MarbleWallArcoEst2() { Name = "Marble Wall Arco Este 2"; ItemID = 0x43B; }
        public MarbleWallArcoEst2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//PlasterWall

         public class PlasterWallEsquina : WallItem
    {
        [Constructable]
        public PlasterWallEsquina() { Name = "Plaster Wall Esquina"; ItemID = 0x127; }
        public PlasterWallEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class PlasterWallEsquina1 : WallItem
    {
        [Constructable]
        public PlasterWallEsquina1() { Name = "Plaster Wall Esquina 1"; ItemID = 0x12B; }
        public PlasterWallEsquina1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class PlasterWallSur : WallItem
    {
        [Constructable]
        public PlasterWallSur() { Name = "Plaster Wall Sur"; ItemID = 0x128; }
        public PlasterWallSur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class PlasterWallSur1 : WallItem
    {
        [Constructable]
        public PlasterWallSur1() { Name = "Plaster Wall Sur 1"; ItemID = 0x12C; }
        public PlasterWallSur1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class PlasterWallEst : WallItem
    {
        [Constructable]
        public PlasterWallEst() { Name = "Plaster Wall Este"; ItemID = 0x129; }
        public PlasterWallEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class PlasterWallEst1 : WallItem
    {
        [Constructable]
        public PlasterWallEst1() { Name = "Plaster Wall Este 1"; ItemID = 0x12D; }
        public PlasterWallEst1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }




//RatanWall
    public class RatanWall : WallItem
    {
        [Constructable]
        public RatanWall() { Name = "Ratan Wall Sur"; ItemID = 0x1A6B; }
        public RatanWall(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class RatanWallSur2 : WallItem
    {
        [Constructable]
        public RatanWallSur2() { Name = "Ratan Wall Sur2"; ItemID = 0x1A7; }
        public RatanWallSur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class RatanWallEst : WallItem
    {
        [Constructable]
        public RatanWallEst() { Name = "Ratan Wall Este"; ItemID = 0x1AC; }
        public RatanWallEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class RatanWallEst2 : WallItem
    {
        [Constructable]
        public RatanWallEst2() { Name = "Ratan Wall Este2"; ItemID = 0x1AA; }
        public RatanWallEst2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class RatanWallEsquina : WallItem
    {
        [Constructable]
        public RatanWallEsquina() { Name = "Ratan Wall Esquina" ; ItemID = 0x1A5; }
        public RatanWallEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }




//HideWall
    public class HideWall : WallItem
    {
        [Constructable]
        public HideWall() { Name = "Hide Wall Sur"; ItemID = 0x1C0; }
        public HideWall(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class HideWallEst : WallItem
    {
        [Constructable]
        public HideWallEst() { Name = "Hide Wall Este"; ItemID = 0x1C1; }
        public HideWallEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class HideWallEsquina : WallItem
    {
        [Constructable]
        public HideWallEsquina() { Name = "Hide Wall Esquina"; ItemID = 0x1B6; }
        public HideWallEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }



//Bamboowall
    public class BambooWall : WallItem
    {
        [Constructable]
        public BambooWall() { Name = "Bamboo Wall"; ItemID = 0x211; }
        public BambooWall(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}
