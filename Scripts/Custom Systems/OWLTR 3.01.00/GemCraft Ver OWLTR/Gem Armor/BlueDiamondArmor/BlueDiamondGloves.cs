using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2643, 0x2644 )]
	public class BlueDiamondGloves : BaseArmor
	{

		public override int BasePhysicalResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 30; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public BlueDiamondGloves() : base( 0x2643 )
		{
            Name = "BlueDiamond Gloves";
            Hue = 1173;
			Weight = 2.0;

            Attributes.BonusDex = Utility.RandomMinMax(4, 9);
            Attributes.BonusMana = Utility.RandomMinMax(2, 7);
            Attributes.BonusStam = Utility.RandomMinMax(2, 15);
            Attributes.CastRecovery = Utility.RandomMinMax(2, 5);
            Attributes.CastSpeed = Utility.RandomMinMax(4, 9);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(10, 25);
            Attributes.RegenHits = Utility.RandomMinMax(5, 12);
            Attributes.WeaponDamage = Utility.RandomMinMax(14, 26);

            PhysicalBonus = Utility.RandomMinMax(7, 23);
            PoisonBonus = Utility.RandomMinMax(1, 10);
		}

        public BlueDiamondGloves(Serial serial)
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