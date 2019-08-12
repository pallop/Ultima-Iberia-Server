using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2643, 0x2644 )]
	public class WhitePearlGloves : BaseArmor
	{

		public override int BaseEnergyResistance{ get{ return 7; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 30; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public WhitePearlGloves() : base( 0x2643 )
		{
            Name = "WhitePearl Gloves";
            Hue = 1150;
			Weight = 2.0;

            ArmorAttributes.MageArmor = 1;
            Attributes.DefendChance = Utility.RandomMinMax(4, 15);
            Attributes.LowerRegCost = Utility.RandomMinMax(2, 12);
            Attributes.Luck = Utility.RandomMinMax(100, 250);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(3, 16);
            Attributes.RegenHits = Utility.RandomMinMax(3, 5);
            Attributes.RegenStam = Utility.RandomMinMax(3, 7);

            EnergyBonus = Utility.RandomMinMax(9, 25);
            ColdBonus = Utility.RandomMinMax(8, 15);
		}

        public WhitePearlGloves(Serial serial)
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
				Weight = 2.0;
		}
	}
}