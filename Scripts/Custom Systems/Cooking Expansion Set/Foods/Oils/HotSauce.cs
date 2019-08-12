using System;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Items
{
    public class HotSauce : Item
    {
        [Constructable]
        public HotSauce()
            : base(0xEFD)
        {
            Stackable = true;
            Name = "Hot Sauce";
            Hue = 0x25;
        }
        public HotSauce(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}