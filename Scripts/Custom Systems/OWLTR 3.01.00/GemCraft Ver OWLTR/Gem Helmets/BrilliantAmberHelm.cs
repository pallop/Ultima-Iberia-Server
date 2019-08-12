using System;
using Server.Items;

namespace Server.Items
{

	public class BrilliantAmberHelm : BaseArmor
	{

		public override int BasePoisonResistance{ get{ return 7; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
        public BrilliantAmberHelm()
            : base(5134)
		{
            Name = "BrilliantAmber Helm";
            Hue = 1281;
			Weight = 5.0;

            Attributes.BonusStam = Utility.RandomMinMax(3, 5);
            Attributes.AttackChance = Utility.RandomMinMax(6, 15);
            Attributes.LowerRegCost = Utility.RandomMinMax(3, 12);
            Attributes.Luck = Utility.RandomMinMax(75, 125);
            Attributes.ReflectPhysical = Utility.RandomMinMax(3, 15);
            Attributes.RegenMana = Utility.RandomMinMax(2, 5);

            PoisonBonus = Utility.RandomMinMax(7, 15);
		}

        public BrilliantAmberHelm(Serial serial)
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