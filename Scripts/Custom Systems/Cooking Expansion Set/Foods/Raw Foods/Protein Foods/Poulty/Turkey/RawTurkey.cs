
using System;
using Server.Network;

namespace Server.Items
{
    public class RawTurkey : Food, ICarvable
    {
        [Constructable]
        public RawTurkey() : this(1) { }

        [Constructable]
        public RawTurkey(int amount)
            : base(amount, 0x9B9)
        {
            Name = "Raw Turkey";
            Stackable = true;
            Amount = amount;
            Raw = true;
        }

        public void Carve(Mobile from, Item item)
        {
            if (!this.Movable)
                return;

            base.ScissorHelper(from, new RawTurkeyLeg(), 2);
            from.SendMessage("You cut the Turkey.");
        }

        public RawTurkey(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}