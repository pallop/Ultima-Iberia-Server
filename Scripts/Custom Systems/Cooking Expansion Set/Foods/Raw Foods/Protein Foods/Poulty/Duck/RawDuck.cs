
using System;
using Server.Network;

namespace Server.Items
{
    public class RawDuck : Food, ICarvable
    {
        [Constructable]
        public RawDuck() : this(1) { }

        [Constructable]
        public RawDuck(int amount)
            : base(amount, 0x9B9)
        {
            Name = "Raw Duck";
            Stackable = true;
            Amount = amount;
            Raw = true;
        }

        public void Carve(Mobile from, Item item)
        {
            if (!this.Movable)
                return;

            base.ScissorHelper(from, new RawDuckLeg(), 2);
            from.SendMessage("You cut the Duck.");
        }

        public RawDuck(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}