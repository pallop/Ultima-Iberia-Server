/************************************************************************************************/
/**********Echo Dynamic Race Class System V2.0, ©2005********************************************/
/************************************************************************************************/

using System;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Items;

namespace Server.Items
{

    [DynamicFliping]
    [Flipable(0x9AB, 0xE7C)]
    public class RCChest : Container
    {

        private bool active = false;
        private Map start_map = Map.Ilshenar;
        private int s_x = 783;
        private int s_y = 1462;
        private int s_z = 0;
        private bool staff_login = true;
        private bool young_function = true;

        public override int DefaultGumpID { get { return 0x4A; } }
        public override int DefaultDropSound { get { return 0x42; } }

        public override Rectangle2D Bounds
        {
            get { return new Rectangle2D(18, 105, 144, 73); }
        }

        public RCChest()
            : base(0x9AB)
        {
            Weight = 25.0; // TODO: Real weight
            Visible = false;
            Movable = false;
            Name = "RC Chest";
            MaxItems = 1000;
        }

        public override int DefaultMaxWeight { get { return 10000; } }

        public RCChest(Serial serial)
            : base(serial)
        {
        }

        [CommandProperty(AccessLevel.Administrator)]
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public Map Start_Map
        {
            get { return start_map; }
            set { start_map = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Start_X
        {
            get { return s_x; }
            set { s_x = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Start_Y
        {
            get { return s_y; }
            set { s_y = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Start_Z
        {
            get { return s_z; }
            set { s_z = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public bool Staff_Login
        {
            get { return staff_login; }
            set { staff_login = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public bool Young_F
        {
            get { return young_function; }
            set { young_function = value; }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2); // version

            writer.Write((bool)young_function);
            writer.Write((bool)staff_login);
            writer.Write(start_map);
            writer.Write((int)s_x);
            writer.Write((int)s_y);
            writer.Write((int)s_z);
            writer.Write((bool)active);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {

                case 2:
                    {

                        young_function = reader.ReadBool();
                        goto case 1;

                    }

                case 1:
                    {

                        staff_login = reader.ReadBool();
                        goto case 0;

                    }

                case 0:
                    {

                        start_map = reader.ReadMap();
                        s_x = reader.ReadInt();
                        s_y = reader.ReadInt();
                        s_z = reader.ReadInt();
                        active = reader.ReadBool();
                        break;

                    }
            }
        }
    }
}