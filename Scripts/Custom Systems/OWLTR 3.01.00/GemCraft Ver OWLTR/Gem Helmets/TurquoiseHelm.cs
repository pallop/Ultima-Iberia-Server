using System;
using Server.Items;

namespace Server.Items
{

	public class TurquoiseHelm : BaseArmor
	{

		public override int BasePhysicalResistance{ get{ return 7; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public TurquoiseHelm() : base( 5134 )
		{
            Name = "Turquoise Helm";
            Hue = 1366;
			Weight = 5.0;

            Attributes.BonusInt = Utility.RandomMinMax(3, 5);
            Attributes.LowerRegCost = Utility.RandomMinMax(4, 15);
            Attributes.Luck = Utility.RandomMinMax(25, 50);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(7, 12);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);
            ArmorAttributes.DurabilityBonus = 12;

            PhysicalBonus = Utility.RandomMinMax(9, 22);
            EnergyBonus = Utility.RandomMinMax(2, 16);
		
		}

        public TurquoiseHelm(Serial serial)
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