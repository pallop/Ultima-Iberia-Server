using System;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Items
{
    public class Teriyaki : Item
    {
        [Constructable]
        public Teriyaki()
            : base(0xE2C)
        {
            Stackable = true;
            Name = "Teriyaki";
            Hue = 0x362;
        }
        public Teriyaki(Serial serial)
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