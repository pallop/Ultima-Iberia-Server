
using System;
using Server.Network;

namespace Server.Items
{
    public class UnbakedMeatPie : Food
    {
        public override int LabelNumber { get { return 1041338; } }

        [Constructable]
        public UnbakedMeatPie()
            : base(0x1042)
        {
            this.Raw = true;
        }

        public UnbakedMeatPie(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}