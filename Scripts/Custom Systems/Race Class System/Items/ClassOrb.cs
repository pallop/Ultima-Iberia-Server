/************************************************************************************************/
/**********Echo Dynamic Race Class System V1.0, ©2005********************************************/
/************************************************************************************************/

using System;
using System.Text;
using System.Collections;
using Server.Network;
using Server.Mobiles;


namespace Server.Items
{

    public class ClassOrb : Item
    {

        private int c_number;
        private string c_name;
        private string c_desc;
        private string[] primary_skills = new string[10];
        private int[] primary_values = new int[10];
        private int amount_ter = 3;
        private int amount_sec = 3;
        private string[] res_skills = new string[10];
        private int amount_pri = 0;
        private int amount_res = 0;
        private int strength = 0;
        private int dexterity = 0;
        private int intelligence = 0;
        private int str_cap = 0;
        private int dex_cap = 0;
        private int int_cap = 0;
        private bool activated = false;
        private SpellbookType[] res_books =    {SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid,	
									SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid};
        private string buname;

        public ClassOrb()
            : base(6249)
        {
            Stackable = false;
            Weight = 0.0;
            Visible = false;
            Name = "Class Orb DO NOT DELETE";
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int ClassNumber
        {
            get { return c_number; }
            set { c_number = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public string ClassName
        {
            get { return c_name; }
            set { c_name = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public string Description
        {
            get { return c_desc; }
            set { c_desc = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Amount_Tertiary
        {
            get { return amount_ter; }
            set { amount_ter = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Amount_Secondary
        {
            get { return amount_sec; }
            set { amount_sec = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Amount_Primary
        {
            get { return amount_pri; }
            set { amount_pri = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Amount_Restricted
        {
            get { return amount_res; }
            set { amount_res = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Set_Str
        {
            get { return strength; }
            set { strength = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Set_Dex
        {
            get { return dexterity; }
            set { dexterity = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Set_Int
        {
            get { return intelligence; }
            set { intelligence = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Str_Cap
        {
            get { return str_cap; }
            set { str_cap = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Dex_Cap
        {
            get { return dex_cap; }
            set { dex_cap = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Int_Cap
        {
            get { return int_cap; }
            set { int_cap = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public bool Activated
        {
            get { return activated; }
            set { activated = value; }
        }

        public int[] Primary_Values
        {
            get { return primary_values; }
            set { primary_values = value; }
        }

        public string[] Primary_Skills
        {
            get { return primary_skills; }
            set { primary_skills = value; }
        }

        public string[] Restricted_Skills
        {
            get { return res_skills; }
            set { res_skills = value; }
        }

        public SpellbookType[] Res_Books
        {
            get { return res_books; }
            set { res_books = value; }
        }

        public string BackUpName
        {
            get { return buname; }
            set { buname = value; }
        }

        public ClassOrb(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);

            writer.Write((string)buname);
            for (int i = 0; i < 20; i++)
            {
                writer.Write((int)res_books[i]);
            }
            writer.Write((int)dex_cap);
            writer.Write((int)int_cap);
            writer.Write((int)str_cap);
            writer.Write((bool)activated);
            writer.Write((int)strength);
            writer.Write((int)dexterity);
            writer.Write((int)intelligence);
            writer.Write((string)c_name);
            writer.Write((string)c_desc);
            writer.Write((int)c_number);
            writer.Write((int)amount_sec);
            writer.Write((int)amount_ter);
            writer.Write((int)amount_pri);
            writer.Write((int)amount_res);
            for (int i = 0; i < 10; i++)
            {
                writer.Write((int)primary_values[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                writer.Write((string)primary_skills[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                writer.Write((string)res_skills[i]);
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
                        for (int i = 0; i < 20; i++)
                        {
                            res_books[i] = (SpellbookType)reader.ReadInt();
                        }
                        goto case 0;
                    }

                case 0:
                    {

                        dex_cap = reader.ReadInt();
                        int_cap = reader.ReadInt();
                        str_cap = reader.ReadInt();
                        activated = reader.ReadBool();
                        strength = reader.ReadInt();
                        dexterity = reader.ReadInt();
                        intelligence = reader.ReadInt();
                        c_name = reader.ReadString();
                        c_desc = reader.ReadString();
                        c_number = reader.ReadInt();
                        amount_sec = reader.ReadInt();
                        amount_ter = reader.ReadInt();
                        amount_pri = reader.ReadInt();
                        amount_res = reader.ReadInt();
                        for (int i = 0; i < 10; i++)
                        {
                            primary_values[i] = reader.ReadInt();
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            primary_skills[i] = reader.ReadString();
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            res_skills[i] = reader.ReadString();
                        }
                        break;
                    }

            }

        }
    }
}