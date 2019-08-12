
using System;
using Server.Network;

namespace Server.Items
{
    public class RawChicken : Food, ICarvable
    {
        [Constructable]
        public RawChicken() : this(1) { }

        [Constructable]
        public RawChicken(int amount)
            : base(amount, 0x9B9)
        {
            Stackable = true;
            Amount = amount;
            Raw = true;
        }

        public void Carve(Mobile from, Item item)
        {
            if (!this.Movable)
                return;

            base.ScissorHelper(from, new RawChickenLeg(), 2);
            from.SendMessage("You cut the Chicken.");
        }

        public RawChicken(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}