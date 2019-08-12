using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2657, 0x2658 )]
	public class EcruCitrineArms : BaseArmor
	{


		public override int BaseEnergyResistance{ get{ return 7; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public EcruCitrineArms() : base( 0x2657 )
		{
            Name = "EcruCitrine Arms";
            Hue = 1922;
			Weight = 5.0;

            Attributes.BonusStam = Utility.RandomMinMax(4, 11);
            Attributes.AttackChance = Utility.RandomMinMax(5, 8);
            Attributes.SpellDamage = Utility.RandomMinMax(20, 35);
            Attributes.Luck = Utility.RandomMinMax(110, 250);
            Attributes.BonusStr = Utility.RandomMinMax(5, 10);
            Attributes.LowerManaCost = Utility.RandomMinMax(8, 20);
            Attributes.SpellChanneling = 1;
            ArmorAttributes.SelfRepair = 3;

            EnergyBonus = Utility.RandomMinMax(9, 15);
            ColdBonus = Utility.RandomMinMax(6, 14);
		}

        public EcruCitrineArms(Serial serial)
            : base(serial)
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 15.0;
		}
	}
}