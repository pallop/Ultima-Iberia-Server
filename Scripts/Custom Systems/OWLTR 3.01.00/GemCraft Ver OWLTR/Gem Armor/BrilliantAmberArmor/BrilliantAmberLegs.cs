using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2647, 0x2648 )]
	public class BrilliantAmberLegs : BaseArmor
	{

		public override int BasePoisonResistance{ get{ return 7; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 60; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public BrilliantAmberLegs() : base( 0x2647 )
		{
            Name = "BrilliantAmber Legs";
            Hue = 1281;
			Weight = 6.0;

            ArmorAttributes.SelfRepair = Utility.RandomMinMax(3, 8);
            Attributes.DefendChance = Utility.RandomMinMax(7, 19);
            Attributes.LowerRegCost = Utility.RandomMinMax(8, 20);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(9, 20);
            ArmorAttributes.MageArmor = 1;
            Attributes.RegenStam = Utility.RandomMinMax(3, 5);

            FireBonus = Utility.RandomMinMax(3, 9);
            PoisonBonus = Utility.RandomMinMax(6, 15);
		}

        public BrilliantAmberLegs(Serial serial)
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
		}
	}
}