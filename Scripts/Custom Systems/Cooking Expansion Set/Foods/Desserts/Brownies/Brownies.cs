using System;
using System.Collections;
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
    public class Brownies : Food
    {
        [Constructable]
        public Brownies() : this(1) { }
        [Constructable]
        public Brownies(int amount)
            : base(amount, 0x160B)
        {
            this.Weight = 1.0;
            this.FillFactor = 2;
            this.Hue = 0x47D;
            this.Name = "Brownies";
        }
        public Brownies(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
}