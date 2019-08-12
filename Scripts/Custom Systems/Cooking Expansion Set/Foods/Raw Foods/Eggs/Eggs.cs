using System;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class Eggs : Food
    {
        [Constructable]
        public Eggs() : this(1) { }

        [Constructable]
        public Eggs(int amount)
            : base(amount, 0x9B5)
        {
            this.Stackable = true;
            this.Amount = amount;
        }

        public Eggs(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}