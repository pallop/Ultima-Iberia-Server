/************************************************************************************************/
/**********Echo Dynamic Race Class System V2.0, ©2005********************************************/
/************************************************************************************************/

using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    public class RCCONTROL : BaseRC
    {
        [Constructable]
        public RCCONTROL()
            : base(6249)
        {
            Movable = false;
            Visible = false;
            LootType = LootType.Newbied;
            Name = "a race/class controller DO NOT DELETE";
        }

        public RCCONTROL(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
