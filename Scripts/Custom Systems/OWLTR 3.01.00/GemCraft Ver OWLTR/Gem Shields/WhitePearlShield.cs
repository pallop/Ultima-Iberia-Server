using System;
using Server;
namespace Server.Items
{
    public class WhitePearlShield : BaseShield
    {

        public override int BaseEnergyResistance { get { return 8; } }

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 165; } }

        public override int AosStrReq { get { return 50; } }

        public override int ArmorBase { get { return 23; } }
        [Constructable]
        public WhitePearlShield() : base(7108)
        {
            Weight = 6.0;
            Hue = 1150;
            Name = "WhitePearl Shield";

            Attributes.SpellDamage = Utility.RandomMinMax(4, 11);
            Attributes.RegenHits = Utility.RandomMinMax(5, 8);
            Attributes.SpellDamage = Utility.RandomMinMax(20, 35);
            Attributes.Luck = Utility.RandomMinMax(110, 250);
            Attributes.BonusStr = Utility.RandomMinMax(5, 10);
            Attributes.LowerRegCost = Utility.RandomMinMax(8, 20);
            Attributes.SpellChanneling = 1;
            ArmorAttributes.SelfRepair = 3;

            EnergyBonus = Utility.RandomMinMax(9, 15);
            PoisonBonus = Utility.RandomMinMax(6, 14);

        }

        public WhitePearlShield(Serial serial)
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