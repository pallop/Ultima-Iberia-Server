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

    public class BonusPackOrb : Item
    {

        private int b_number;
        private string b_name;
        private string[] pack_skills = new string[5];
        private int[] skill_values = new int[5];
        private int amount_skills;
        private int secskillcost;
        private bool activated = false;
        private string buname;

        public BonusPackOrb()
            : base(6249)
        {
            Stackable = false;
            Weight = 0.0;
            Visible = false;
            Name = "Bonus Pack Orb DO NOT DELETE";
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int BPNumber
        {
            get { return b_number; }
            set { b_number = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public string BPName
        {
            get { return b_name; }
            set { b_name = value; }
        }


        [CommandProperty(AccessLevel.Administrator)]
        public int AmountSkills
        {
            get { return amount_skills; }
            set { amount_skills = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Sec_Skill_Cost
        {
            get { return secskillcost; }
            set { secskillcost = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public bool Activated
        {
            get { return activated; }
            set { activated = value; }
        }


        public int[] BPValues
        {
            get { return skill_values; }
            set { skill_values = value; }
        }

        public string[] BPSkills
        {
            get { return pack_skills; }
            set { pack_skills = value; }
        }

        public string BackUpName
        {
            get { return buname; }
            set { buname = value; }
        }

        public BonusPackOrb(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);

            writer.Write((string)buname);
            writer.Write((bool)activated);
            writer.Write((string)b_name);
            writer.Write((int)b_number);
            writer.Write((int)amount_skills);
            writer.Write((int)secskillcost);
            for (int i = 0; i < 5; i++)
            {
                writer.Write((string)pack_skills[i]);
            }
            for (int i = 0; i < 5; i++)
            {
                writer.Write((int)skill_values[i]);
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
                        goto case 0;
                    }

                case 0:
                    {

                        activated = reader.ReadBool();
                        b_name = reader.ReadString();
                        b_number = reader.ReadInt();
                        amount_skills = reader.ReadInt();
                        secskillcost = reader.ReadInt();
                        for (int i = 0; i < 5; i++)
                        {
                            pack_skills[i] = reader.ReadString();
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            skill_values[i] = reader.ReadInt();
                        }
                        break;
                    }

            }

        }
    }
}