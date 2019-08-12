using System;
using Server.Items;

namespace Server.Items
{
	public class BlueDiamondHelm : BaseArmor
	{

		public override int BasePhysicalResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int OldDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public BlueDiamondHelm() : base( 5134 )
		{
            Name = "BlueDiamond Helm";
            Hue = 1173;
			Weight = 5.0;

            Attributes.BonusInt = Utility.RandomMinMax(2, 6);
            Attributes.DefendChance = Utility.RandomMinMax(5, 15);
            Attributes.SpellDamage = Utility.RandomMinMax(9, 15);
            Attributes.LowerRegCost = Utility.RandomMinMax(3, 10);
            Attributes.Luck = Utility.RandomMinMax(75, 150);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);
            Attributes.NightSight = 1;

            EnergyBonus = Utility.RandomMinMax(3, 5);
            PhysicalBonus = Utility.RandomMinMax(5, 12);
		}

        public BlueDiamondHelm(Serial serial)
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