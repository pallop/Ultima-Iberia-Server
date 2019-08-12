using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2643, 0x2644 )]
	public class BrilliantAmberGloves : BaseArmor
	{

		public override int BasePoisonResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 30; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public BrilliantAmberGloves() : base( 0x2643 )
		{
            Name = "BrilliantAmber Gloves";
            Hue = 1281;
			Weight = 2.0;

            Attributes.BonusInt = Utility.RandomMinMax(3, 5);
            Attributes.AttackChance = Utility.RandomMinMax(3, 12);
            Attributes.DefendChance = Utility.RandomMinMax(2, 7);
            Attributes.LowerRegCost = Utility.RandomMinMax(3, 12);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(3, 15);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);
            Attributes.ReflectPhysical = Utility.RandomMinMax(7, 12);

            FireBonus = Utility.RandomMinMax(3, 5);
            PoisonBonus = Utility.RandomMinMax(12, 18);

		}

        public BrilliantAmberGloves(Serial serial)
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