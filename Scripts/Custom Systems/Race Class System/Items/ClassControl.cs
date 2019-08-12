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

    public class ClassControl : Item
    {

        private int amount_Classes;
        private int c_current;
        private int sec_cap = 80;
        private int ter_cap = 70;
        private int def_cap = 20;
        private int start_skills = 3;
        private int start_skills_value = 50;
        private bool dress_players = false;
        private int stat_cap = 225;
         private int skill_cap = 700;
        private SpellbookType[] loaded_books = {SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid,	
									SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid,
									SpellbookType.Invalid, SpellbookType.Invalid};
        private int l_books = 0;
        private bool class_title = false;

        public ClassControl()
            : base(6249)
        {
            Stackable = false;
            Weight = 0.0;
            Visible = false;
            Name = "Class Control DO NOT DELETE";
        }


        public int A_Classes
        {
            get { return amount_Classes; }
            set { amount_Classes = value; }
        }

        public int C_Current
        {
            get { return c_current; }
            set { c_current = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Secondary_Cap
        {
            get { return sec_cap; }
            set { sec_cap = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Tertiary_Cap
        {
            get { return ter_cap; }
            set { ter_cap = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Default_Cap
        {
            get { return def_cap; }
            set { def_cap = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Amount_Start_Skills
        {
            get { return start_skills; }
            set { start_skills = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Start_Skills_Value
        {
            get { return start_skills_value; }
            set { start_skills_value = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public bool Equip_Players
        {
            get { return dress_players; }
            set { dress_players = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Stat_Cap
        {
            get { return stat_cap; }
            set { stat_cap = value; }
        }

        public SpellbookType[] LoadBook
        {
            get { return loaded_books; }
            set { loaded_books = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int Loaded_Books
        {
            get { return l_books; }
            set { l_books = value; }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public bool Class_Title
        {
            get { return class_title; }
            set { class_title = value; }
        }

        public ClassControl(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)2);

            writer.Write((bool)class_title);
            writer.Write((int)l_books);
            for (int i = 0; i < 20; i++)
            {
                writer.Write((int)loaded_books[i]);
            }
            writer.Write((int)stat_cap);
            writer.Write((int)sec_cap);
            writer.Write((int)ter_cap);
            writer.Write((int)def_cap);
            writer.Write((int)start_skills);
            writer.Write((int)start_skills_value);
            writer.Write((bool)dress_players);
            writer.Write((int)amount_Classes);
            writer.Write((int)c_current);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {

                case 2:
                    {

                        class_title = reader.ReadBool();
                        goto case 1;
                    }

                case 1:
                    {

                        l_books = reader.ReadInt();
                        for (int i = 0; i < 20; i++)
                        {
                            loaded_books[i] = (SpellbookType)reader.ReadInt();
                        }
                        goto case 0;
                    }

                case 0:
                    {

                        stat_cap = reader.ReadInt();
                        sec_cap = reader.ReadInt();
                        ter_cap = reader.ReadInt();
                        def_cap = reader.ReadInt();
                        start_skills = reader.ReadInt();
                        start_skills_value = reader.ReadInt();
                        dress_players = reader.ReadBool();
                        amount_Classes = reader.ReadInt();
                        c_current = reader.ReadInt();
                        break;
                    }

            }

        }
    }
}