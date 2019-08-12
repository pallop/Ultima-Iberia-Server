using System;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class BrightlyColoredEggs : Food
    {
        public override string DefaultName
        {
            get { return "brightly colored eggs"; }
        }

        [Constructable]
        public BrightlyColoredEggs()
            : base(0x9B5)
        {
            this.Weight = 0.5;
            this.Hue = 3 + (Utility.Random(20) * 5);
        }

        public BrightlyColoredEggs(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}