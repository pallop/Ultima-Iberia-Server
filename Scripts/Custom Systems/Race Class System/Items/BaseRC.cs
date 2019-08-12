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

    public abstract class BaseRC : Item
    {


        private string m_Class;
        private string m_Race;
        private int m_Third;
        private int m_Secondary;
        private string[] m_sec = new string[10];
        private string[] m_ter = new string[10];
        private string m_bonus;
        private bool active = false;
        private Bag a_bag;
        private Bag w_bag;

        private Mobile m_Owner;


        public BaseRC(int itemID)
            : base(itemID)
        {
            Stackable = false;
            Weight = 0.0;
            Movable = false;
            Visible = false;
            Name = "a verify DO NOT DELETE";
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Second
        {
            get { return m_Secondary; }
            set { m_Secondary = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Third
        {
            get { return m_Third; }
            set { m_Third = value; }
        }

        public string[] Secondary
        {
            get { return m_sec; }
            set { m_sec = value; }
        }

        public string[] Tertiary
        {
            get { return m_ter; }
            set { m_ter = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string BonusPack
        {
            get { return m_bonus; }
            set { m_bonus = value; }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string P_Class
        {
            get { return m_Class; }
            set { m_Class = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Race
        {
            get { return m_Race; }
            set { m_Race = value; }
        }

        public Bag A_Bag
        {
            get { return a_bag; }
            set { a_bag = value; }
        }

        public Bag W_Bag
        {
            get { return w_bag; }
            set { w_bag = value; }
        }


        public BaseRC(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);

            writer.Write(w_bag);
            writer.Write(a_bag);
            writer.Write((bool)active);
            writer.Write((int)m_Secondary);
            writer.Write((int)m_Third);
            writer.Write((string)m_bonus);
            writer.Write((string)m_Class);
            writer.Write((string)m_Race);
            for (int i = 0; i < 10; i++)
            {
                writer.Write((string)m_sec[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                writer.Write((string)m_ter[i]);
            }

            writer.Write((Mobile)m_Owner);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {

                case 1:
                    {

                        w_bag = (Bag)reader.ReadItem();
                        a_bag = (Bag)reader.ReadItem();
                        goto case 0;

                    }

                case 0:
                    {

                        active = reader.ReadBool();
                        m_Secondary = reader.ReadInt();
                        m_Third = reader.ReadInt();
                        m_bonus = reader.ReadString();
                        m_Class = reader.ReadString();
                        m_Race = reader.ReadString();
                        for (int i = 0; i < 10; i++)
                        {
                            m_sec[i] = reader.ReadString();
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            m_ter[i] = reader.ReadString();
                        }

                        m_Owner = (Mobile)reader.ReadMobile();

                        if (m_Owner != null)
                        {
                            this.Delete();
                        }
                        break;
                    }

            }

        }
    }
}