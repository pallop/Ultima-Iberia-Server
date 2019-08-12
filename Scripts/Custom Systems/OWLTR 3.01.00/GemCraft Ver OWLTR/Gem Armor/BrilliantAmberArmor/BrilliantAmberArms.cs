using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2657, 0x2658 )]
	public class BrilliantAmberArms : BaseArmor
	{

		public override int BasePoisonResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public BrilliantAmberArms() : base( 0x2657 )
		{
            Name = "BrilliantAmber Arms";
            Hue = 1281;
			Weight = 5.0;

            Attributes.BonusInt = Utility.RandomMinMax(3, 5);
            Attributes.LowerRegCost = Utility.RandomMinMax(4, 15);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(5, 8);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);
            ArmorAttributes.DurabilityBonus = 12;

            PoisonBonus = Utility.RandomMinMax(7, 18);
            PhysicalBonus = Utility.RandomMinMax(2, 7);
		}

        public BrilliantAmberArms(Serial serial)
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