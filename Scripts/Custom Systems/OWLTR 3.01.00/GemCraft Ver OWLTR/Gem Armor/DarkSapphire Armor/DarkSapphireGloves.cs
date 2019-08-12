using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2643, 0x2644 )]
	public class DarkSapphireGloves : BaseArmor
	{

		public override int BaseColdResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 30; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public DarkSapphireGloves() : base( 0x2643 )
		{
            Name = "DarkSapphire Gloves";
            Hue = 1176;
			Weight = 2.0;

            Attributes.BonusStr = Utility.RandomMinMax(2, 6);
            Attributes.AttackChance = Utility.RandomMinMax(5, 15);
            Attributes.LowerRegCost = Utility.RandomMinMax(3, 10);
            Attributes.Luck = Utility.RandomMinMax(75, 150);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(7, 15);
            Attributes.RegenMana = Utility.RandomMinMax(2, 5);

            EnergyBonus = Utility.RandomMinMax(2, 6);
            ColdBonus = Utility.RandomMinMax(8, 15);
		}

        public DarkSapphireGloves(Serial serial)
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