/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;

namespace Server.Items
{

    public class PanelItem : ResourceItem
    {
        [Constructable]
        public PanelItem() { Weight = 5.0; }
        public PanelItem(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    //wood panel1
    public class WoodPanel : PanelItem
    {
        [Constructable]
        public WoodPanel() { Name = "Wood Panel1 Sur"; ItemID = 0x12; }
        public WoodPanel(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class WoodPanel1EsquinaGrande : PanelItem
    {
        [Constructable]
        public WoodPanel1EsquinaGrande() { Name = "Wood Panel1 Esquina"; ItemID = 0x10; }
        public WoodPanel1EsquinaGrande(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class WoodPanel1Est : PanelItem
    {
        [Constructable]
        public WoodPanel1Est() { Name = "Wood Panel1 Este"; ItemID = 0x11; }
        public WoodPanel1Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class WoodPanel1EsquinaPeque : PanelItem
    {
        [Constructable]
        public WoodPanel1EsquinaPeque() { Name = "Wood Panel Esquina Pequeña"; ItemID = 0x14; }
        public WoodPanel1EsquinaPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class WoodPanel1EstPeque : PanelItem
    {
        [Constructable]
        public WoodPanel1EstPeque() { Name = "Wood Panel1 Este Peque"; ItemID = 0x15; }
        public WoodPanel1EstPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodPanel1SuPeque : PanelItem
    {
        [Constructable]
        public WoodPanel1SuPeque() { Name = "Wood Panel1 Sur Peque"; ItemID = 0x16; }
        public WoodPanel1SuPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    ///woodpanel2
 public class WoodPanel2Sur : PanelItem
    {
        [Constructable]
        public WoodPanel2Sur() { Name = "Wood Panel2 Sur"; ItemID = 0x21D1; }
        public WoodPanel2Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    
      public class WoodPanel2Est : PanelItem
    {
        [Constructable]
        public WoodPanel2Est() { Name = "Wood Panel2 Este"; ItemID = 0x21D3; }
        public WoodPanel2Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class WoodPanel2EsquinaPeque : PanelItem
    {
        [Constructable]
        public WoodPanel2EsquinaPeque() { Name = "Wood Panel2 Esquina Pequeña"; ItemID = 0xBD; }
        public WoodPanel2EsquinaPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class WoodPanel2EstPeque : PanelItem
    {
        [Constructable]
        public WoodPanel2EstPeque() { Name = "Wood Panel2 Este Peque"; ItemID = 0x21CE; }
        public WoodPanel2EstPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodPanel2SuPeque : PanelItem
    {
        [Constructable]
        public WoodPanel2SuPeque() { Name = "Wood Panel2 Sur Peque"; ItemID = 0x21CF; }
        public WoodPanel2SuPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    //WoodPanel3
  public class WoodPanel3PinchosFueraEste: PanelItem
    {
        [Constructable]
        public WoodPanel3PinchosFueraEste() { Name = "Wood Panel3 Pinchos Fuera Este"; ItemID = 0x21E; }
        public WoodPanel3PinchosFueraEste(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class WoodPanel3PinchosFueraSur : PanelItem
    {
        [Constructable]
        public WoodPanel3PinchosFueraSur() { Name = "Wood Panel3 Pinchos Fuera Sur"; ItemID = 0x21D; }
        public WoodPanel3PinchosFueraSur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class WoodPanel3PinchosDentroEst : PanelItem
    {
        [Constructable]
        public WoodPanel3PinchosDentroEst() { Name = "Wood Panel3 Pinchos Dentro Este"; ItemID = 0x220; }
        public WoodPanel3PinchosDentroEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class WoodPanel3PinchosDentroSur : PanelItem
    {
        [Constructable]
        public WoodPanel3PinchosDentroSur() { Name = "Wood Panel3 Pinchos Dentro Sur"; ItemID = 0x21F; }
        public WoodPanel3PinchosDentroSur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class WoodPanel3EstPeque : PanelItem
    {
        [Constructable]
        public WoodPanel3EstPeque() { Name = "Wood Panel3 Este Peque"; ItemID = 0x22B; }
        public WoodPanel3EstPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodPanel3SuPeque : PanelItem
    {
        [Constructable]
        public WoodPanel3SuPeque() { Name = "Wood Panel3 Sur Peque"; ItemID = 0x22A; }
        public WoodPanel3SuPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    //wood panel4

    public class WoodPanel4Sur : PanelItem
    {
        [Constructable]
        public WoodPanel4Sur() { Name = "Wood Panel4 Sur"; ItemID = 0x2863; }
        public WoodPanel4Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    
      public class WoodPanel4Est : PanelItem
    {
        [Constructable]
        public WoodPanel4Est() { Name = "Wood Panel4 Este"; ItemID = 0x2867; }
        public WoodPanel4Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
       public class WoodPanel4EsquinaGrande : PanelItem
    {
        [Constructable]
        public WoodPanel4EsquinaGrande() { Name = "Wood Panel4 Esquina Grande"; ItemID = 0x2860; }
        public WoodPanel4EsquinaGrande(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class WoodPanel4EsquinaPeque : PanelItem
    {
        [Constructable]
        public WoodPanel4EsquinaPeque() { Name = "Wood Panel4 Esquina Pequeña"; ItemID = 0x2861; }
        public WoodPanel4EsquinaPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class WoodPanel4EstPeque : PanelItem
    {
        [Constructable]
        public WoodPanel4EstPeque() { Name = "Wood Panel4 Este Peque"; ItemID = 0x2868; }
        public WoodPanel4EstPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodPanel4SuPeque : PanelItem
    {
        [Constructable]
        public WoodPanel4SuPeque() { Name = "Wood Panel4 Sur Peque"; ItemID = 0x2864; }
        public WoodPanel4SuPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
//wood panel 5
 public class WoodPanel5Sur : PanelItem
    {
        [Constructable]
        public WoodPanel5Sur() { Name = "Wood Panel5 Sur"; ItemID = 0x2999; }
        public WoodPanel5Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    
      public class WoodPanel5Est : PanelItem
    {
        [Constructable]
        public WoodPanel5Est() { Name = "Wood Panel5 Este"; ItemID = 0x2996; }
        public WoodPanel5Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
       public class WoodPanel5EsquinaGrande : PanelItem
    {
        [Constructable]
        public WoodPanel5EsquinaGrande() { Name = "Wood Panel5 Esquina Grande"; ItemID = 0x299C; }
        public WoodPanel5EsquinaGrande(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class WoodPanel5EsquinaPeque : PanelItem
    {
        [Constructable]
        public WoodPanel5EsquinaPeque() { Name = "Wood Panel5 Esquina Pequeña"; ItemID = 0x299B; }
        public WoodPanel5EsquinaPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class WoodPanel5EstPeque : PanelItem
    {
        [Constructable]
        public WoodPanel5EstPeque() { Name = "Wood Panel5 Este Peque"; ItemID = 0x2995; }
        public WoodPanel5EstPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WoodPanel5SuPeque : PanelItem
    {
        [Constructable]
        public WoodPanel5SuPeque() { Name = "Wood Panel5 Sur Peque"; ItemID = 0x2998; }
        public WoodPanel5SuPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    //Stone1
 public class Stone1Est : PanelItem
    {
        [Constructable]
        public Stone1Est() { Name = "Stone1 Este "; ItemID = 0x26; }
        public Stone1Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone1Sur : PanelItem
    {
        [Constructable]
        public Stone1Sur() { Name = "Stone1 Sur"; ItemID = 0x25; }
        public Stone1Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone1EsquinaGrande: PanelItem
    {
        [Constructable]
        public Stone1EsquinaGrande() { Name = "Stone1 Esquina Grande"; ItemID = 0x24; }
        public Stone1EsquinaGrande(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone1EsquinaPeque: PanelItem
    {
        [Constructable]
        public Stone1EsquinaPeque() { Name = "Stone1 Esquina Pequeña"; ItemID = 0x2D; }
        public Stone1EsquinaPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone1EstPeque : PanelItem
    {
        [Constructable]
        public Stone1EstPeque() { Name = "Stone1 Este Pequeña "; ItemID = 0x2E; }
        public Stone1EstPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone1SurPeque : PanelItem
    {
        [Constructable]
        public Stone1SurPeque() { Name = "Stone1 Sur Pequeña "; ItemID = 0x2F; }
        public Stone1SurPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    //Stone2

    public class StoneSlab : PanelItem
    {
        [Constructable]
        public StoneSlab() { Name = "Stone Slab Sur"; ItemID = 0x63; }
        public StoneSlab(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class StoneSlabEst : PanelItem
    {
        [Constructable]
        public StoneSlabEst() { Name = "Stone Slab Este"; ItemID = 0x63; }
        public StoneSlabEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneSlabEsquinaGrande : PanelItem
    {
        [Constructable]
        public StoneSlabEsquinaGrande() { Name = "Stone Slab Esquina Grande"; ItemID = 0x65; }
        public StoneSlabEsquinaGrande (Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class StoneSlabSurPeque : PanelItem
    {
        [Constructable]
        public StoneSlabSurPeque() { Name = "Stone Slab Sur Pequeña"; ItemID = 0x69; }
        public StoneSlabSurPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class StoneSlabEstPeque : PanelItem
    {
        [Constructable]
        public StoneSlabEstPeque() { Name = "Stone Slab Este Pequeña"; ItemID = 0x6A; }
        public StoneSlabEstPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class StoneSlabEsquinaPeque : PanelItem
    {
        [Constructable]
        public StoneSlabEsquinaPeque() { Name = "Stone Slab Esquina Pequeña"; ItemID = 0x6B; }
        public StoneSlabEsquinaPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

     public class StoneSlabRampa1Sur : PanelItem
    {
        [Constructable]
        public StoneSlabRampa1Sur() { Name = "Stone Slab Rampa1 Sur"; ItemID = 0x74; }
        public StoneSlabRampa1Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class StoneSlabRampa1Est : PanelItem
    {
        [Constructable]
        public StoneSlabRampa1Est () { Name = "Stone Slab Rampa1 Este"; ItemID = 0x72; }
        public StoneSlabRampa1Est (Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class StoneSlabRampa2Est : PanelItem
    {
        [Constructable]
        public StoneSlabRampa2Est() { Name = "Stone Slab Rampa2 Este"; ItemID = 0x75; }
        public StoneSlabRampa2Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class StoneSlabRampa2Sur : PanelItem
    {
        [Constructable]
        public StoneSlabRampa2Sur() { Name = "Stone Slab Rampa2 Sur"; ItemID = 0x76; }
        public StoneSlabRampa2Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class StoneSlabEsquinaNormal : PanelItem
    {
        [Constructable]
        public StoneSlabEsquinaNormal() { Name = "Stone Slab Esquina Normal"; ItemID = 0x73; }
        public StoneSlabEsquinaNormal(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class StoneSlabEsquinaPincho : PanelItem
    {
        [Constructable]
        public StoneSlabEsquinaPincho() { Name = "Stone Slab Esquina Pincho"; ItemID = 0x2201; }
        public StoneSlabEsquinaPincho(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class StoneSlabEsquinaPlana : PanelItem
    {
        [Constructable]
        public StoneSlabEsquinaPlana() { Name = "Stone Slab Esquina Plana"; ItemID = 0x77; }
        public StoneSlabEsquinaPlana(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    //stone 3

    public class Stone3placa1 : PanelItem
    {
        [Constructable]
        public Stone3placa1() { Name = "Stone3 Placa1"; ItemID = 0x7C; }
        public Stone3placa1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone3placa2 : PanelItem
    {
        [Constructable]
        public Stone3placa2() { Name = "Stone3 Placa2 "; ItemID = 0x89; }
        public Stone3placa2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

     public class Stone3EsquinaNort : PanelItem
    {
        [Constructable]
        public Stone3EsquinaNort() { Name = "Stone3 Esquina Norte "; ItemID = 0x2CE; }
        public Stone3EsquinaNort(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3EsquinaSur : PanelItem
    {
        [Constructable]
        public Stone3EsquinaSur() { Name = "Stone3 Esquina Sur "; ItemID = 0x2CF; }
        public Stone3EsquinaSur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3EsquinaSW : PanelItem
    {
        [Constructable]
        public Stone3EsquinaSW() { Name = "Stone3 Esquina SurOeste "; ItemID = 0x2D1; }
        public Stone3EsquinaSW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3EsquinaNE : PanelItem
    {
        [Constructable]
        public Stone3EsquinaNE() { Name = "Stone3 Esquina NorEste "; ItemID = 0x2D4; }
        public Stone3EsquinaNE (Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3EstGrandeFuera : PanelItem
    {
        [Constructable]
        public Stone3EstGrandeFuera() { Name = "Stone3 Este Grande Fuera "; ItemID = 0x2D5; }
        public Stone3EstGrandeFuera(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3EstGrandeDentro : PanelItem
    {
        [Constructable]
        public Stone3EstGrandeDentro() { Name = "Stone3 Este Grande Dentro "; ItemID = 0x2D3; }
        public Stone3EstGrandeDentro(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3SurGrandeFuera : PanelItem
    {
        [Constructable]
        public Stone3SurGrandeFuera() { Name = "Stone3 Sur Grande Fuera "; ItemID = 0x2D2; }
        public Stone3SurGrandeFuera(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3SurGrandeDentro : PanelItem
    {
        [Constructable]
        public Stone3SurGrandeDentro() { Name = "Stone3 Sur Grande Dentro "; ItemID = 0x2D0; }
        public Stone3SurGrandeDentro(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3SurPequeFuera : PanelItem
    {
        [Constructable]
        public Stone3SurPequeFuera() { Name = "Stone3 Sur Pequeña Fuera"; ItemID = 0x2D7; }
        public Stone3SurPequeFuera(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3SurPequeDentro : PanelItem
    {
        [Constructable]
        public Stone3SurPequeDentro() { Name = "Stone3 Sur Pequeña Dentro "; ItemID = 0x2D6; }
        public Stone3SurPequeDentro(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3EstPequeFuera : PanelItem
    {
        [Constructable]
        public Stone3EstPequeFuera() { Name = "Stone3 Oeste Pequeña Fuera "; ItemID = 0x2D9; }
        public Stone3EstPequeFuera(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3EstPequeDentro : PanelItem
    {
        [Constructable]
        public Stone3EstPequeDentro() { Name = "Stone3 Este Pequeña Dentro "; ItemID = 0x2D8; }
        public Stone3EstPequeDentro(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3RampaWest : PanelItem
    {
        [Constructable]
        public Stone3RampaWest() { Name = "Stone3 Rampa Oeste "; ItemID = 0x2C7; }
        public Stone3RampaWest(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3RampaEst : PanelItem
    {
        [Constructable]
        public Stone3RampaEst() { Name = "Stone3 Rampa Este "; ItemID = 0x2CC; }
        public Stone3RampaEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3RampaSur : PanelItem
    {
        [Constructable]
        public Stone3RampaSur() { Name = "Stone3 Rampa Sur "; ItemID = 0x2CA; }
        public Stone3RampaSur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3RampaNorte : PanelItem
    {
        [Constructable]
        public Stone3RampaNorte() { Name = "Stone3 Rampa Norte "; ItemID = 0x2C8; }
        public Stone3RampaNorte(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3EsquinaPuntaNE : PanelItem
    {
        [Constructable]
        public Stone3EsquinaPuntaNE() { Name = "Stone3 Esquina Punta NorEste "; ItemID = 0x2CD; }
        public Stone3EsquinaPuntaNE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3EsquinaPuntaSur : PanelItem
    {
        [Constructable]
        public Stone3EsquinaPuntaSur() { Name = "Stone3 Esquina Punta Sur "; ItemID = 0x2C9; }
        public Stone3EsquinaPuntaSur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class Stone3EsquinaPuntaSW : PanelItem
    {
        [Constructable]
        public Stone3EsquinaPuntaSW() { Name = "Stone3 Esquina Punta SurOeste"; ItemID = 0x2CB; }
        public Stone3EsquinaPuntaSW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }




//Stone4

    public class Stone4Sur: PanelItem
    {
        [Constructable]
        public Stone4Sur() { Name = "Stone4 Sur "; ItemID = 0xDE; }
        public Stone4Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone4Est : PanelItem
    {
        [Constructable]
        public Stone4Est() { Name = "Stone4 Este "; ItemID = 0xDD; }
        public Stone4Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone4Esquina : PanelItem
    {
        [Constructable]
        public Stone4Esquina() { Name = "Stone4 Esquina "; ItemID = 0xDC; }
        public Stone4Esquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone4Pilon : PanelItem
    {
        [Constructable]
        public Stone4Pilon() { Name = "Stone4 Pilon "; ItemID = 0xDB; }
        public Stone4Pilon(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone4PilonW : PanelItem
    {
        [Constructable]
        public Stone4PilonW() { Name = "Stone4 Pilon Oeste "; ItemID = 0xD2; }
        public Stone4PilonW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone4PilonNor : PanelItem
    {
        [Constructable]
        public Stone4PilonNor() { Name = "Stone4 Pilon Norte"; ItemID = 0xD3; }
        public Stone4PilonNor(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//Stone5

 public class Stone5Sur: PanelItem
    {
        [Constructable]
        public Stone5Sur() { Name = "Stone5 Sur "; ItemID = 0x1E9; }
        public Stone5Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone5Est : PanelItem
    {
        [Constructable]
        public Stone5Est() { Name = "Stone5 Este "; ItemID = 0x1EA; }
        public Stone5Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone5Esquina : PanelItem
    {
        [Constructable]
        public Stone5Esquina() { Name = "Stone5 Esquina "; ItemID = 0x1E8; }
        public Stone5Esquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone5Pilon : PanelItem
    {
        [Constructable]
        public Stone5Pilon() { Name = "Stone5 Pilon "; ItemID = 0x1DA; }
        public Stone5Pilon(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone5PilonW : PanelItem
    {
        [Constructable]
        public Stone5PilonW() { Name = "Stone5 Pilon Oeste "; ItemID = 0x1E7; }
        public Stone5PilonW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone5PilonNor : PanelItem
    {
        [Constructable]
        public Stone5PilonNor() { Name = "Stone5 Pilon Norte"; ItemID = 0x1E6; }
        public Stone5PilonNor(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//Stone7

 public class Stone7Sur: PanelItem
    {
        [Constructable]
        public Stone7Sur() { Name = "Stone7 Sur "; ItemID = 0x3C0; }
        public Stone7Sur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone7Est : PanelItem
    {
        [Constructable]
        public Stone7Est() { Name = "Stone7 Este "; ItemID = 0x3BE; }
        public Stone7Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone7Esquina : PanelItem
    {
        [Constructable]
        public Stone7Esquina() { Name = "Stone7 Esquina "; ItemID = 0x3C1; }
        public Stone7Esquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone7SurDeco: PanelItem
    {
        [Constructable]
        public Stone7SurDeco() { Name = "Stone7 Sur Deco"; ItemID = 0x3D0; }
        public Stone7SurDeco(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone7EstDeco : PanelItem
    {
        [Constructable]
        public Stone7EstDeco() { Name = "Stone7 Este Deco"; ItemID = 0x3D1; }
        public Stone7EstDeco(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class Stone7EsquinaDeco : PanelItem
    {
        [Constructable]
        public Stone7EsquinaDeco() { Name = "Stone7 Esquina Deco"; ItemID = 0x3CF; }
        public Stone7EsquinaDeco(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//Brick

    public class BrickPanel : PanelItem
    {
        [Constructable]
        public BrickPanel() { Name = "Brick Panel Pequeño Sur"; ItemID = 0x42; }
        public BrickPanel(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class BrickPanelEstPequeño : PanelItem
    {
        [Constructable]
        public BrickPanelEstPequeño() { Name = "Brick Panel Pequeño Este"; ItemID = 0x43; }
        public BrickPanelEstPequeño(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class BrickPanelEsquinaPequeña : PanelItem
    {
        [Constructable]
        public BrickPanelEsquinaPequeña() { Name = "Brick Panel Pequeño Esquina"; ItemID = 0x41; }
        public BrickPanelEsquinaPequeña (Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class BrickPanelSurGrande : PanelItem
    {
        [Constructable]
        public BrickPanelSurGrande() { Name = "Brick Panel Grande Sur"; ItemID = 0x3E; }
        public BrickPanelSurGrande(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class BrickPanelEstGrande : PanelItem
    {
        [Constructable]
        public BrickPanelEstGrande() { Name = "Brick Panel Grande Este"; ItemID = 0x3F; }
        public BrickPanelEstGrande(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class BrickPanelEsquinaGrande : PanelItem
    {
        [Constructable]
        public BrickPanelEsquinaGrande() { Name = "Brick Panel Grande Esquina"; ItemID = 0x3D; }
        public BrickPanelEsquinaGrande(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//Log

    public class LogPanel : PanelItem
    {
        [Constructable]
        public LogPanel() { Name = "Log Panel Sur"; ItemID = 0x9F; }
        public LogPanel(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class LogPanelEst : PanelItem
    {
        [Constructable]
        public LogPanelEst() { Name = "Log Panel Este"; ItemID = 0x9B; }
        public LogPanelEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class LogPanelEsquina : PanelItem
    {
        [Constructable]
        public LogPanelEsquina() { Name = "Log Panel Esquina"; ItemID = 0x9A; }
        public LogPanelEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class LogPanelTocon : PanelItem
    {
        [Constructable]
        public LogPanelTocon() { Name = "Log Panel Tocon"; ItemID = 0x9D; }
        public LogPanelTocon(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class LogPanelSurTocon : PanelItem
    {
        [Constructable]
        public LogPanelSurTocon() { Name = "Log Panel Sur Tocon"; ItemID = 0xA0; }
        public LogPanelSurTocon(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class LogPanelEstTocon : PanelItem
    {
        [Constructable]
        public LogPanelEstTocon() { Name = "Log Panel Este Tocon"; ItemID = 0xA1; }
        public LogPanelEstTocon(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class LogPanelSurPeque : PanelItem
    {
        [Constructable]
        public LogPanelSurPeque() { Name = "Log Panel Sur Pequeño"; ItemID = 0xA4; }
        public LogPanelSurPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class LogPanelEstPeque : PanelItem
    {
        [Constructable]
        public LogPanelEstPeque() { Name = "Log Panel Este Pequeño"; ItemID = 0xA5; }
        public LogPanelEstPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//Wrought

    public class WroughtIronPanel : PanelItem
    {
        [Constructable]
        public WroughtIronPanel() { Name = "Wrought Iron Panel Sur"; ItemID = 0x084B; }
        public WroughtIronPanel(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class WroughtIronPanelEste : PanelItem
    {
        [Constructable]
        public WroughtIronPanelEste() { Name = "Wrought Iron Panel Este"; ItemID = 0x821; }
        public WroughtIronPanelEste(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    public class WroughtIronPanelEsquina : PanelItem
    {
        [Constructable]
        public WroughtIronPanelEsquina() { Name = "Wrought Iron Panel Esquina"; ItemID = 0x822; }
        public WroughtIronPanelEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//Sandstone

    public class SandstonePanel : PanelItem
    {
        [Constructable]
        public SandstonePanel() { Name = "Sandstone Panel Sur"; ItemID = 0x184; }
        public SandstonePanel(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

     public class SandstonePanelEst : PanelItem
    {
        [Constructable]
        public SandstonePanelEst() { Name = "Sandstone Panel Este"; ItemID = 0x169; }
        public SandstonePanelEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

     public class SandstonePanelSurDeco : PanelItem
    {
        [Constructable]
        public SandstonePanelSurDeco() { Name = "Sandstone Panel Sur Deco"; ItemID = 0x165; }
        public SandstonePanelSurDeco(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


 public class SandstonePanelEstDeco : PanelItem
    {
        [Constructable]
        public SandstonePanelEstDeco() { Name = "Sandstone Panel Este Deco"; ItemID = 0x166; }
        public SandstonePanelEstDeco(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class SandstonePanelEsquinaSurDeco : PanelItem
    {
        [Constructable]
        public SandstonePanelEsquinaSurDeco() { Name = "Sandstone Panel Esquina Sur Deco"; ItemID = 0x168; }
        public SandstonePanelEsquinaSurDeco(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


 public class SandstonePanelEsquinaN : PanelItem
    {
        [Constructable]
        public SandstonePanelEsquinaN() { Name = "Sandstone Panel Esquina Norte"; ItemID = 0x183; }
        public SandstonePanelEsquinaN(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class SandstonePanelEsquinaS : PanelItem
    {
        [Constructable]
        public SandstonePanelEsquinaS() { Name = "Sandstone Panel Esquina Sur"; ItemID = 0x184; }
        public SandstonePanelEsquinaS(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class SandstonePanelEsquinaNE: PanelItem
    {
        [Constructable]
        public SandstonePanelEsquinaNE() { Name = "Sandstone Panel Esquina NorEste"; ItemID = 0x174; }
        public SandstonePanelEsquinaNE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class SandstonePanelEsquinaSW : PanelItem
    {
        [Constructable]
        public SandstonePanelEsquinaSW() { Name = "Sandstone Panel Esquina SurOeste"; ItemID = 0x182; }
        public SandstonePanelEsquinaSW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class SandstonePanelRampaSur : PanelItem
    {
        [Constructable]
        public SandstonePanelRampaSur() { Name = "Sandstone Panel Rampa Sur"; ItemID = 0x17A; }
        public SandstonePanelRampaSur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class SandstonePanelRampaEst : PanelItem
    {
        [Constructable]
        public SandstonePanelRampaEst() { Name = "Sandstone Panel Rampa Este"; ItemID = 0x179; }
        public SandstonePanelRampaEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class SandstonePanelEsquinaRampaSW : PanelItem
    {
        [Constructable]
        public SandstonePanelEsquinaRampaSW() { Name = "Sandstone Panel Esquina Rampa SurOeste"; ItemID = 0x17C; }
        public SandstonePanelEsquinaRampaSW(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class SandstonePanelEsquinaRampaNE : PanelItem
    {
        [Constructable]
        public SandstonePanelEsquinaRampaNE() { Name = "Sandstone Panel Esquina Rampa NorEste"; ItemID = 0x17D; }
        public SandstonePanelEsquinaRampaNE(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class SandstonePanelEsquinaRampaSur : PanelItem
    {
        [Constructable]
        public SandstonePanelEsquinaRampaSur() { Name = "Sandstone Panel Esquina Rampa Sur"; ItemID = 0x17B; }
        public SandstonePanelEsquinaRampaSur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//Marble

    class MarbleSlab : PanelItem
    {
        [Constructable]
        public MarbleSlab() { Name = "Marble Slab Sur"; ItemID = 0x10B; }
        public MarbleSlab(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     class MarbleSlabEst : PanelItem
    {
        [Constructable]
        public MarbleSlabEst() { Name = "Marble Slab Este"; ItemID = 0x10C; }
        public MarbleSlabEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     class MarbleSlabEsquinaGrande : PanelItem
    {
        [Constructable]
        public MarbleSlabEsquinaGrande() { Name = "Marble Slab Esquina Grande"; ItemID = 0x1DA; }
        public MarbleSlabEsquinaGrande(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     class MarbleSlabSurPeque : PanelItem
    {
        [Constructable]
        public MarbleSlabSurPeque() { Name = "Marble Slab Sur Pequeña"; ItemID = 0x10F; }
        public MarbleSlabSurPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     class MarbleSlabEstPeque : PanelItem
    {
        [Constructable]
        public MarbleSlabEstPeque() { Name = "Marble Slab Este Pequeña"; ItemID = 0x110; }
        public MarbleSlabEstPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     class MarbleSlabEsquinaPeque : PanelItem
    {
        [Constructable]
        public MarbleSlabEsquinaPeque() { Name = "Marble Slab Esquina Pequeña"  ; ItemID = 0x10E; }
        public MarbleSlabEsquinaPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }




//Ratan
    class RatanPanel : PanelItem
    {
        [Constructable]
        public RatanPanel() { Name = "Ratan Panel Sur"; ItemID = 0x1A6; }
        public RatanPanel(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     class RatanPanelEst : PanelItem
    {
        [Constructable]
        public RatanPanelEst() { Name = "Ratan Panel Este"; ItemID = 0x1A9; }
        public RatanPanelEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    class RatanPanelEstPeque : PanelItem
    {
        [Constructable]
        public RatanPanelEstPeque() { Name = "Ratan Panel Este Pequeña"; ItemID = 0x215A; }
        public RatanPanelEstPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    class RatanPanelSurPeque : PanelItem
    {
        [Constructable]
        public RatanPanelSurPeque() { Name = "Ratan Panel Sur Pequeña"; ItemID = 0x215B; }
        public RatanPanelSurPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    class RatanPanelEsquina : PanelItem
    {
        [Constructable]
        public RatanPanelEsquina() { Name = "Ratan Panel Esquina"; ItemID = 0x215C; }
        public RatanPanelEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//Hide
    public class HidePanel : PanelItem
    {
        [Constructable]
        public HidePanel() { Name = "Hide Panel Sur"; ItemID = 0x1C2; }
        public HidePanel(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

     public class HidePanelEst : PanelItem
    {
        [Constructable]
        public HidePanelEst() { Name = "Hide Panel Est"; ItemID = 0x1BD; }
        public HidePanelEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class HidePanelSurPeque : PanelItem
    {
        [Constructable]
        public HidePanelSurPeque() { Name = "Hide Panel Sur Pequeña"; ItemID = 0x1C8; }
        public HidePanelSurPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class HidePanelEstPeque : PanelItem
    {
        [Constructable]
        public HidePanelEstPeque() { Name = "Hide Panel Este Pequeña"; ItemID = 0x1C9; }
        public HidePanelEstPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//Bamboo
    public class BambooPanel : PanelItem
    {
        [Constructable]
        public BambooPanel() { Name = "Bamboo Panel 1" ; ItemID = 0x214; }
        public BambooPanel(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BambooPanel2 : PanelItem
    {
        [Constructable]
        public BambooPanel2() { Name = "Bamboo Panel 2"; ItemID = 0x211; }
        public BambooPanel2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BambooPanel3 : PanelItem
    {
        [Constructable]
        public BambooPanel3() { Name = "Bamboo Panel 3"; ItemID = 0x20F; }
        public BambooPanel3(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BambooPanel4 : PanelItem
    {
        [Constructable]
        public BambooPanel4() { Name = "Bamboo Panel 4"; ItemID = 0x210; }
        public BambooPanel4(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BambooPanel5 : PanelItem
    {
        [Constructable]
        public BambooPanel5() { Name = "Bamboo Panel 5"; ItemID = 0x216; }
        public BambooPanel5(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BambooPanel6 : PanelItem
    {
        [Constructable]
        public BambooPanel6() { Name = "Bamboo Panel 6"; ItemID = 0x217; }
        public BambooPanel6(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BambooPanel7 : PanelItem
    {
        [Constructable]
        public BambooPanel7() { Name = "Bamboo Panel 7"; ItemID = 0x215; }
        public BambooPanel7(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class BambooPanel8 : PanelItem
    {
        [Constructable]
        public BambooPanel8() { Name = "Bamboo Panel 8"; ItemID = 0x212; }
        public BambooPanel8(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class BambooPanel9 : PanelItem
    {
        [Constructable]
        public BambooPanel9() { Name = "Bamboo Panel 9"; ItemID = 0x213; }
        public BambooPanel9(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class BambooPanel10 : PanelItem
    {
        [Constructable]
        public BambooPanel10() { Name = "Bamboo Panel 10"; ItemID = 0x218; }
        public BambooPanel10(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class BambooPanel11 : PanelItem
    {
        [Constructable]
        public BambooPanel11() { Name = "Bamboo Panel 11"; ItemID = 0x21A; }
        public BambooPanel11(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
      public class BambooPanel12 : PanelItem
    {
        [Constructable]
        public BambooPanel12() { Name = "Bamboo Panel 12"; ItemID = 0x219; }
        public BambooPanel12(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    //Clay

       public class ClayPanelSur : PanelItem
    {
        [Constructable]
        public ClayPanelSur() { Name = "Clay Panel Sur"; ItemID = 0x294E; }
        public ClayPanelSur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
       public class ClayPanelEst : PanelItem
    {
        [Constructable]
        public ClayPanelEst() { Name = "Clay Panel Este"; ItemID = 0x2948; }
        public ClayPanelEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
       public class ClayPanelEsquina : PanelItem
    {
        [Constructable]
        public ClayPanelEsquina() { Name = "Clay Panel Esquina"; ItemID = 0x2945; }
        public ClayPanelEsquina(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
       public class ClayPanelSurPeque : PanelItem
    {
        [Constructable]
        public ClayPanelSurPeque() { Name = "Clay Panel Sur Pequeña"; ItemID = 0x294D; }
        public ClayPanelSurPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
       public class ClayPanelEstPeque : PanelItem
    {
        [Constructable]
        public ClayPanelEstPeque() { Name = "Clay Panel Este Pequeña"; ItemID = 0x2947; }
        public ClayPanelEstPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
       public class ClayPanelEsquinaPeque : PanelItem
    {
        [Constructable]
        public ClayPanelEsquinaPeque() { Name = "Clay Panel Esquina Pequeña"; ItemID = 0x2944; }
        public ClayPanelEsquinaPeque(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    //Plaster
   public class PlasterPanelSur : PanelItem
    {
        [Constructable]
        public PlasterPanelSur() { Name = "Plaster Panel Sur"; ItemID = 0x14A; }
        public PlasterPanelSur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
       public class PlasterPanelEst : PanelItem
    {
        [Constructable]
        public PlasterPanelEst() { Name = "Plaster Panel Este"; ItemID = 0x14B; }
        public PlasterPanelEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }




}
