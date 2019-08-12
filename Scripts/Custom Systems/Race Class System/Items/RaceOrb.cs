/************************************************************************************************/
/**********Echo Dynamic Race Class System V2.0, ©2005********************************************/
/************************************************************************************************/

using System;
using System.Text;
using System.Collections;
using Server.Network;
using Server.Mobiles;


namespace Server.Items
{

    public class RaceOrb : Item
    {

        private int r_number;
        private string r_name;
        private string r_desc;
        private int bodyhue_amount;
        private int hairhue_amount;
        private int[] body_hues = new int[10];
        private int[] hair_hues = new int[10];
        private Map r_map;
        private int r_x;
        private int r_y;
        private int r_z;
        private string[] res_classes = new string[5];
        private int num_res = 0;
        private bool activated = false;
        private int[] body_value = new int[5];
        private int[] ss_hue = new int[5];
        private string[] ss_name = new string[5];
        private string buname;

        public RaceOrb()
            : base(6249)
        {
            Stackable = false;
            Weight = 0.0;
            Visible = false;
            Name = "Race Orb DO NOT DELETE";
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int RaceNumber
        {
            get { return r_number; }
            set { r_number = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public string RaceName
        {
            get { return r_name; }
            set { r_name = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public string Description
        {
            get { return r_desc; }
            set { r_desc = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int AmountBodyHues
        {
            get { return bodyhue_amount; }
            set { bodyhue_amount = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int AmountHairHues
        {
            get { return hairhue_amount; }
            set { hairhue_amount = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Res_Class_Num
        {
            get { return num_res; }
            set { num_res = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public Map Race_Map
        {
            get { return r_map; }
            set { r_map = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Race_X
        {
            get { return r_x; }
            set { r_x = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Race_Y
        {
            get { return r_y; }
            set { r_y = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Race_Z
        {
            get { return r_z; }
            set { r_z = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public bool Activated
        {
            get { return activated; }
            set { activated = value; }
        }

        public int[] BodyHues
        {
            get { return body_hues; }
            set { body_hues = value; }
        }

        public int[] HairHues
        {
            get { return hair_hues; }
            set { hair_hues = value; }
        }

        public string[] Restricted_C
        {
            get { return res_classes; }
            set { res_classes = value; }
        }

        public int[] BodyValue
        {
            get { return body_value; }
            set { body_value = value; }
        }

        public int[] SS_Hue
        {
            get { return ss_hue; }
            set { ss_hue = value; }
        }

        public string[] SS_Name
        {
            get { return ss_name; }
            set { ss_name = value; }
        }

        public string BackUpName
        {
            get { return buname; }
            set { buname = value; }
        }

        public RaceOrb(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);

            writer.Write((string)buname);
            for (int i = 0; i < 5; i++)
            {
                writer.Write((string)ss_name[i]);
            }
            for (int i = 0; i < 5; i++)
            {
                writer.Write((int)ss_hue[i]);
            }
            for (int i = 0; i < 5; i++)
            {
                writer.Write((int)body_value[i]);
            }
            writer.Write((int)num_res);
            writer.Write((bool)activated);
            writer.Write((string)r_name);
            writer.Write((string)r_desc);
            writer.Write((int)bodyhue_amount);
            writer.Write((int)hairhue_amount);
            writer.Write((int)r_number);
            writer.Write((int)r_x);
            writer.Write((int)r_y);
            writer.Write((int)r_z);
            writer.Write(r_map);
            for (int i = 0; i < 10; i++)
            {
                writer.Write((int)body_hues[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                writer.Write((int)hair_hues[i]);
            }
            for (int i = 0; i < 5; i++)
            {
                writer.Write((string)res_classes[i]);
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {

                case 1:
                    {

                        buname = reader.ReadString();
                        for (int i = 0; i < 5; i++)
                        {
                            ss_name[i] = reader.ReadString();
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            ss_hue[i] = reader.ReadInt();
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            body_value[i] = reader.ReadInt();
                        }
                        goto case 0;
                    }

                case 0:
                    {

                        num_res = reader.ReadInt();
                        activated = reader.ReadBool();
                        r_name = reader.ReadString();
                        r_desc = reader.ReadString();
                        bodyhue_amount = reader.ReadInt();
                        hairhue_amount = reader.ReadInt();
                        r_number = reader.ReadInt();
                        r_x = reader.ReadInt();
                        r_y = reader.ReadInt();
                        r_z = reader.ReadInt();
                        r_map = reader.ReadMap();
                        for (int i = 0; i < 10; i++)
                        {
                            body_hues[i] = reader.ReadInt();
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            hair_hues[i] = reader.ReadInt();
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            res_classes[i] = reader.ReadString();
                        }
                        break;
                    }
            }

        }
    }
}