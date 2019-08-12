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

    public class RaceControl : Item
    {

        private int amount_Races;
        private int r_current;
        private bool r_location;
        private int r_current_res;
        private bool race_title = false;

        public RaceControl()
            : base(6249)
        {
            Stackable = false;
            Weight = 0.0;
            Visible = false;
            Name = "Race Control DO NOT DELETE";
        }

        public int A_Races
        {
            get { return amount_Races; }
            set { amount_Races = value; }
        }

        public int A_Current
        {
            get { return r_current; }
            set { r_current = value; }
        }

        public int R_Current
        {
            get { return r_current_res; }
            set { r_current_res = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public bool Set_Location
        {
            get { return r_location; }
            set { r_location = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public bool Race_Title
        {
            get { return race_title; }
            set { race_title = value; }
        }


        public RaceControl(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);

            writer.Write((bool)race_title);
            writer.Write((int)r_current_res);
            writer.Write((int)amount_Races);
            writer.Write((int)r_current);
            writer.Write((bool)r_location);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {

                case 1:
                    {

                        race_title = reader.ReadBool();
                        goto case 0;

                    }

                case 0:
                    {

                        r_current_res = reader.ReadInt();
                        amount_Races = reader.ReadInt();
                        r_current = reader.ReadInt();
                        r_location = reader.ReadBool();
                        break;

                    }

            }

        }
    }
}