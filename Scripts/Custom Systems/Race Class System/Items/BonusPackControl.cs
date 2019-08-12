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

    public class BonusPackControl : Item
    {

        private int amount_BonusPacks;
        private int b_current;

        public BonusPackControl()
            : base(6249)
        {
            Stackable = false;
            Weight = 0.0;
            Visible = false;
            Name = "Bonus Pack Control DO NOT DELETE";
        }

        public int A_BP
        {
            get { return amount_BonusPacks; }
            set { amount_BonusPacks = value; }
        }

        public int B_Current
        {
            get { return b_current; }
            set { b_current = value; }
        }


        public BonusPackControl(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write((int)amount_BonusPacks);
            writer.Write((int)b_current);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            amount_BonusPacks = reader.ReadInt();
            b_current = reader.ReadInt();

        }
    }
}