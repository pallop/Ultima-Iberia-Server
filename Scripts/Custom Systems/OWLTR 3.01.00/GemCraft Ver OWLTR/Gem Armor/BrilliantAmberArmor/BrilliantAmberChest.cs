using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2641, 0x2642 )]
	public class BrilliantAmberChest : BaseArmor
	{

		public override int BasePoisonResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 60; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public BrilliantAmberChest() : base( 0x2641 )
		{
            Name = "BrilliantAmber Chest";
            Hue = 1281;
			Weight = 10.0;

            Attributes.BonusDex = Utility.RandomMinMax(3, 5);
            Attributes.DefendChance = Utility.RandomMinMax(4, 15);
            Attributes.LowerManaCost = Utility.RandomMinMax(7, 15);
            Attributes.Luck = Utility.RandomMinMax(75, 125);
            Attributes.ReflectPhysical = Utility.RandomMinMax(4, 12);
            Attributes.RegenStam = Utility.RandomMinMax(2, 6);
            ArmorAttributes.SelfRepair = 3;
            ArmorAttributes.DurabilityBonus = 10;

            EnergyBonus = Utility.RandomMinMax(3, 5);
            PoisonBonus = Utility.RandomMinMax(4, 12);
		}

        public BrilliantAmberChest(Serial serial)
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