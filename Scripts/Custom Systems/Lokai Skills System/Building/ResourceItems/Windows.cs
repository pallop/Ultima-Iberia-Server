/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;

namespace Server.Items
{
    public class WindowItem : ResourceItem
    {
        [Constructable]
        public WindowItem() { Weight = 5.0; }
        public WindowItem(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
//wood1
    public class WoodWindow : WindowItem
    {
        [Constructable]
        public WoodWindow() { Name = "Wood Window Sur"; ItemID = 0xE; }
        public WoodWindow(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class WoodWindowEst : WindowItem
    {
        [Constructable]
        public WoodWindowEst() { Name = "Wood Window Este"; ItemID = 0xF; }
        public WoodWindowEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//wood2
    public class WoodWindow2 : WindowItem
    {
        [Constructable]
        public WoodWindow2() { Name = "Wood Window2 Sur"; ItemID = 0xBC; }
        public WoodWindow2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class WoodWindow2Est : WindowItem
    {
        [Constructable]
        public WoodWindow2Est() { Name = "Wood Window2 Este"; ItemID = 0xBB; }
        public WoodWindow2Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }





//wood4
public class WoodWindow4 : WindowItem
    {
        [Constructable]
        public WoodWindow4() { Name = "Wood Window4 Sur"; ItemID = 0x2865; }
        public WoodWindow4(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class WoodWindow4Est : WindowItem
    {
        [Constructable]
        public WoodWindow4Est() { Name = "Wood Window4 Este"; ItemID = 0x2869; }
        public WoodWindow4Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//Stone1
    public class StoneWindow : WindowItem
    {
        [Constructable]
        public StoneWindow() { Name = "Stone Window Sur"; ItemID = 0x29AD; }
        public StoneWindow(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWindowEst : WindowItem
    {
        [Constructable]
        public StoneWindowEst() { Name = "Stone Window Este"; ItemID = 0x29B2; }
        public StoneWindowEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
//Stone2
    public class StoneWindow2 : WindowItem
    {
        [Constructable]
        public StoneWindow2() { Name = "Stone Window2 Sur"; ItemID = 0x29A0; }
        public StoneWindow2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWindow2Est : WindowItem
    {
        [Constructable]
        public StoneWindow2Est() { Name = "Stone Window2 Este"; ItemID = 0x29A2; }
        public StoneWindow2Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//Stone3
     public class StoneWindow3 : WindowItem
    {
        [Constructable]
        public StoneWindow3() { Name = "Stone Window3 Sur"; ItemID = 0x833; }
        public StoneWindow3(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWindow3Est : WindowItem
    {
        [Constructable]
        public StoneWindow3Est() { Name = "Stone Window3 Este"; ItemID = 0x85; }
        public StoneWindow3Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//Stone4
     public class StoneWindow4 : WindowItem
    {
        [Constructable]
        public StoneWindow4() { Name = "Stone Window4 Sur"; ItemID = 0xCA; }
        public StoneWindow4(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWindow4Est : WindowItem
    {
        [Constructable]
        public StoneWindow4Est() { Name = "Stone Window4 Este"; ItemID = 0xCB; }
        public StoneWindow4Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//Stone5
    public class StoneWindow5 : WindowItem
    {
        [Constructable]
        public StoneWindow5() { Name = "Stone Window5 Sur"; ItemID = 0x29A5; }
        public StoneWindow5(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWindow5Est : WindowItem
    {
        [Constructable]
        public StoneWindow5Est() { Name = "Stone Window5 Este"; ItemID = 0x29A9; }
        public StoneWindow5Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//Stone9
    public class StoneWindow9 : WindowItem
    {
        [Constructable]
        public StoneWindow9() { Name = "Stone Window9 Sur"; ItemID = 0x3DE; }
        public StoneWindow9(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class StoneWindow9Est : WindowItem
    {
        [Constructable]
        public StoneWindow9Est() { Name = "Stone Window9 Este"; ItemID = 0x3DF; }
        public StoneWindow9Est(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//Brick
    public class BrickWindow : WindowItem
    {
        [Constructable]
        public BrickWindow() { Name = "Brick Window Sur"; ItemID = 0x29B7; }
        public BrickWindow(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class BrickWindowEst : WindowItem
    {
        [Constructable]
        public BrickWindowEst() { Name = "Brick Window Este"; ItemID = 0x29BC; }
        public BrickWindowEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//Log
    public class LogWindow : WindowItem
    {
        [Constructable]
        public LogWindow() { Name = "Log Window Sur"; ItemID = 0x24F5; }
        public LogWindow(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class LogWindowEst : WindowItem
    {
        [Constructable]
        public LogWindowEst() { Name = "Log Window Este"; ItemID = 0x24FB; }
        public LogWindowEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//Sandstone
    public class SandstoneWindow : WindowItem
    {
        [Constructable]
        public SandstoneWindow() { Name = "Sandstone Window Sur"; ItemID = 0x15C; }
        public SandstoneWindow(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class SandstoneWindowEst : WindowItem
    {
        [Constructable]
        public SandstoneWindowEst() { Name = "Sandstone Window Este"; ItemID = 0x15D; }
        public SandstoneWindowEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//Marble
    public class MarbleWindow : WindowItem
    {
        [Constructable]
        public MarbleWindow() { Name = "Marble Window Sur"; ItemID = 0x2519; }
        public MarbleWindow(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class MarbleWindowEst : WindowItem
    {
        [Constructable]
        public MarbleWindowEst() { Name = "Marble Window Este"; ItemID = 0x251F; }
        public MarbleWindowEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }



//PLASTER


public class PlasterWindow : WindowItem
    {
        [Constructable]
        public PlasterWindow() { Name = "Plaster Window Sur"; ItemID = 0x154; }
        public PlasterWindow(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class PlasterWindowEst : WindowItem
    {
        [Constructable]
        public PlasterWindowEst() { Name = "Plaster Window Este"; ItemID = 0x155; }
        public PlasterWindowEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//Ratan

    public class RatanWindow : WindowItem
    {
        [Constructable]
        public RatanWindow() { Name = "Ratan Window Sur"; ItemID = 0x1AD; }
        public RatanWindow(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class RatanWindowEst : WindowItem
    {
        [Constructable]
        public RatanWindowEst() { Name = "Ratan Window Este"; ItemID = 0x1AF; }
        public RatanWindowEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//hide
    public class HideWindow : WindowItem
    {
        [Constructable]
        public HideWindow() { Name = "Hide Window Sur"; ItemID = 0x1C4; }
        public HideWindow(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class HideWindowEst : WindowItem
    {
        [Constructable]
        public HideWindowEst() { Name = "Hide Window Este"; ItemID = 0x1C5; }
        public HideWindowEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

//Bamboo
    public class BambooWindow : WindowItem
    {
        [Constructable]
        public BambooWindow() { Name = "Bamboo Window Sur"; ItemID = 0x2D43; }
        public BambooWindow(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class BambooWindowEst : WindowItem
    {
        [Constructable]
        public BambooWindowEst() { Name = "Bamboo Window Este"; ItemID = 0x2D42; }
        public BambooWindowEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

}
