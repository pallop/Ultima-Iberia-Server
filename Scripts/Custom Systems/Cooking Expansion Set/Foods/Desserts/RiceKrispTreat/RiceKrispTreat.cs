using System;
using System.Collections;
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
    public class RiceKrispTreat : Food
    {
        [Constructable]
        public RiceKrispTreat() : this(1) { }
        [Constructable]
        public RiceKrispTreat(int amount)
            : base(amount, 0x1044)
        {
            this.Weight = 1.0;
            this.FillFactor = 2;
            this.Hue = 0x9B;
            this.Name = "Rice Krisp Treat";
        }
        public RiceKrispTreat(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }

    }
}