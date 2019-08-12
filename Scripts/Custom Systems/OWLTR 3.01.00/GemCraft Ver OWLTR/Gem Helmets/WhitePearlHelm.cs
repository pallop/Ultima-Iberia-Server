using System;
using Server.Items;

namespace Server.Items
{

	public class WhitePearlHelm : BaseArmor
	{

		public override int BaseEnergyResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public WhitePearlHelm() : base( 5134 )
		{
            Name = "WhitePearl Helm";
            Hue = 1150;
			Weight = 5.0;

            Attributes.BonusDex = Utility.RandomMinMax(2, 5);
            Attributes.BonusHits = Utility.RandomMinMax(3, 7);
            Attributes.DefendChance = Utility.RandomMinMax(3, 8);
            Attributes.LowerManaCost = Utility.RandomMinMax(8, 20);
            Attributes.NightSight = 1;
            Attributes.Luck = Utility.RandomMinMax(100, 250);
            Attributes.ReflectPhysical = Utility.RandomMinMax(5, 15);
            Attributes.RegenStam = Utility.RandomMinMax(3, 7);
            ArmorAttributes.SelfRepair = 3;

            EnergyBonus = Utility.RandomMinMax(9, 15);
            FireBonus = Utility.RandomMinMax(2, 10);
		}

        public WhitePearlHelm(Serial serial)
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
				Weight = 5.0;
		}
	}
}