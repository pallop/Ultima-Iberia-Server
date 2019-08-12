/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;

namespace Server.Items
{
    public class DoorItem : ResourceItem
    {
        [Constructable]
        public DoorItem() { Weight = 5.0; }
        public DoorItem(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }




    //fence door
 public class FenceDoorEst1 : DoorItem
    {
        [Constructable]
        public FenceDoorEst1() { Name = "Fence Door Este1"; ItemID = 0x866; }
        public FenceDoorEst1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    } public class FenceDoorEst2 : DoorItem
    {
        [Constructable]
        public FenceDoorEst2() { Name = "Fence Door Este2"; ItemID = 0x868; }
        public FenceDoorEst2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    } public class FenceDoorSur1 : DoorItem
    {
        [Constructable]
        public FenceDoorSur1() { Name = "Fence Door Sur1 "; ItemID = 0x870; }
        public FenceDoorSur1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    } public class FenceDoorSur2 : DoorItem
    {
        [Constructable]
        public FenceDoorSur2() { Name = "Fence Door Sur2"; ItemID = 0x86E; }
        public FenceDoorSur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    //wooddoor

    public class WoodDoor : DoorItem
    {
        [Constructable]
        public WoodDoor() { Name = "Wood Door Est1"; ItemID = 0x6ED; }
        public WoodDoor(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class WoodDoorEst2 : DoorItem
    {
        [Constructable]
        public WoodDoorEst2() { Name = "Wood Door Est2"; ItemID = 0x6EF; }
        public WoodDoorEst2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class WoodDoorSur1 : DoorItem
    {
        [Constructable]
        public WoodDoorSur1() { Name = "Wood Door Sur1"; ItemID = 0x6E5; }
        public WoodDoorSur1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

 public class WoodDoorSur2 : DoorItem
    {
        [Constructable]
        public WoodDoorSur2() { Name = "Wood Door Sur2"; ItemID = 0x6E7; }
        public WoodDoorSur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }




    //metaldoor

    public class Metal_Door : DoorItem
    {
        [Constructable]
        public Metal_Door() { Name = "Metal Door Sur 1"; ItemID = 0x675; }
        public Metal_Door(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class Metal_DoorSur2 : DoorItem
    {
        [Constructable]
        public Metal_DoorSur2() { Name = "Metal Door Sur 2"; ItemID = 0x677; }
        public Metal_DoorSur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class Metal_DoorEst1 : DoorItem
    {
        [Constructable]
        public Metal_DoorEst1() { Name = "Metal Door Este 1"; ItemID = 0x670; }
        public Metal_DoorEst1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class Metal_DoorEst2 : DoorItem
    {
        [Constructable]
        public Metal_DoorEst2() { Name = "Metal Door Este 2"; ItemID = 0x67F; }
        public Metal_DoorEst2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }



//barredmetaldoor
    public class BarredMetal_Door : DoorItem
    {
        [Constructable]
        public BarredMetal_Door() { Name = "Barred Metal Door Sur1"; ItemID = 0x685; }
        public BarredMetal_Door(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class BarredMetal_DoorSur2 : DoorItem
    {
        [Constructable]
        public BarredMetal_DoorSur2() { Name = "Barred Metal Door Sur2"; ItemID = 0x687; }
        public BarredMetal_DoorSur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class BarredMetal_DoorEst1 : DoorItem
    {
        [Constructable]
        public BarredMetal_DoorEst1() { Name = "Barred Metal Door Este1"; ItemID = 0x68D; }
        public BarredMetal_DoorEst1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class BarredMetal_DoorEst2 : DoorItem
    {
        [Constructable]
        public BarredMetal_DoorEst2() { Name = "Barred Metal Door Este2"; ItemID = 0x68F; }
        public BarredMetal_DoorEst2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }



//logdoor???? SERIUSLY?
    public class LogDoor : DoorItem
    {
        [Constructable]
        public LogDoor() { Name = "Log Door"; ItemID = 0x228; }
        public LogDoor(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//WroughtIronGate
    public class WroughtIronGate : DoorItem
    {
        [Constructable]
        public WroughtIronGate() { Name = "Wrought Iron Gate Sur1"; ItemID = 0x824; }
        public WroughtIronGate(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WroughtIronGateSur2 : DoorItem
    {
        [Constructable]
        public WroughtIronGateSur2() { Name = "Wrought Iron Gate Sur2"; ItemID = 0x826; }
        public WroughtIronGateSur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WroughtIronGateEst1 : DoorItem
    {
        [Constructable]
        public WroughtIronGateEst1() { Name = "Wrought Iron Gate Este1"; ItemID = 0x82C; }
        public WroughtIronGateEst1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class WroughtIronGateEst2 : DoorItem
    {
        [Constructable]
        public WroughtIronGateEst2() { Name = "Wrought Iron Gate Este2"; ItemID = 0x82E; }
        public WroughtIronGateEst2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//RatanDoor
    public class RatanDoor : DoorItem
    {
        [Constructable]
        public RatanDoor() { Name = "Ratan Door Sur1"; ItemID = 0x695; }
        public RatanDoor(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class RatanDoorSur2 : DoorItem
    {
        [Constructable]
        public RatanDoorSur2() { Name = "Ratan Door Sur2"; ItemID = 0x697; }
        public RatanDoorSur2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class RatanDoorEst1 : DoorItem
    {
        [Constructable]
        public RatanDoorEst1() { Name = "Ratan Door Este1"; ItemID = 0x69D; }
        public RatanDoorEst1(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class RatanDoorEst2 : DoorItem
    {
        [Constructable]
        public RatanDoorEst2() { Name = "Ratan Door Este2"; ItemID = 0x69F; }
        public RatanDoorEst2(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }


//HideDoor
    public class HideDoor : DoorItem
    {
        [Constructable]
        public HideDoor() { Name = "Hide Door"; ItemID = 0x22C; }
        public HideDoor(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

    //Porticus
     public class PorticusDoorSur: DoorItem
    {
        [Constructable]
        public PorticusDoorSur() { Name = "Porticus Door Sur"; ItemID = 0x6F5; }
        public PorticusDoorSur(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
     public class PorticusDoorEst : DoorItem
    {
        [Constructable]
        public PorticusDoorEst() { Name = "Porticus Door Este"; ItemID = 0x6F6; }
        public PorticusDoorEst(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
   
//BambooDoor
    public class BambooDoor : DoorItem
    {
        [Constructable]
        public BambooDoor() { Name = "Bamboo Door"; ItemID = 0x2D49; }
        public BambooDoor(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}
