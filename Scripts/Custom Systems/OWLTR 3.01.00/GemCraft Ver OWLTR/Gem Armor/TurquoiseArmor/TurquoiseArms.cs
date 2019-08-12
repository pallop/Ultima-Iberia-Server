using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2657, 0x2658 )]
	public class TurquoiseArms : BaseArmor
	{

		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 6; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public TurquoiseArms() : base( 0x2657 )
		{
            Name = "Turquoise Arms";
            Hue = 1366;
			Weight = 5.0;

            Attributes.BonusInt = Utility.RandomMinMax(2, 6);
            Attributes.DefendChance = Utility.RandomMinMax(5, 15);
            Attributes.LowerRegCost = Utility.RandomMinMax(3, 10);
            Attributes.Luck = Utility.RandomMinMax(75, 150);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(7, 15);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);

            EnergyBonus = Utility.RandomMinMax(2, 6);
            PhysicalBonus = Utility.RandomMinMax(4, 12);
		}

        public TurquoiseArms(Serial serial)
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