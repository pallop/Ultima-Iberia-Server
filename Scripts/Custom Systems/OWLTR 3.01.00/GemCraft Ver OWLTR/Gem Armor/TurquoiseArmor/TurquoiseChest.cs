using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2641, 0x2642 )]
	public class TurquoiseChest : BaseArmor
	{

		public override int BasePhysicalResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 60; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public TurquoiseChest() : base( 0x2641 )
		{
            Name = "Turquoise Chest";
            Hue = 1366;
			Weight = 10.0;

            Attributes.NightSight = 1;
            Attributes.BonusStr = Utility.RandomMinMax(2, 6);
            Attributes.BonusDex = Utility.RandomMinMax(4, 15);
            Attributes.RegenStam = Utility.RandomMinMax(4, 10);
            Attributes.AttackChance = Utility.RandomMinMax(5, 10);
            Attributes.DefendChance = Utility.RandomMinMax(7, 15);
            Attributes.WeaponDamage = Utility.RandomMinMax(14, 29);
            Attributes.WeaponSpeed = Utility.RandomMinMax(20, 35);
            Attributes.ReflectPhysical = Utility.RandomMinMax(4, 10);
            ArmorAttributes.SelfRepair = Utility.RandomMinMax(4, 15);
		}

        public TurquoiseChest(Serial serial)
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