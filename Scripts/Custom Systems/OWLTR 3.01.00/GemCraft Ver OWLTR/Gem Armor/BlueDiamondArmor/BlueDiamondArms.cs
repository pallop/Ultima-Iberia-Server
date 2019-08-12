using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2657, 0x2658 )]
	public class BlueDiamondArms : BaseArmor
	{

		public override int BasePhysicalResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public BlueDiamondArms() : base( 0x2657 )
		{
            Name = "BlueDiamond Arms";
            Hue = 1173;
			Weight = 5.0;

            Attributes.BonusInt = Utility.RandomMinMax(3, 5);
            Attributes.LowerRegCost = Utility.RandomMinMax(4, 15);
            Attributes.Luck = Utility.RandomMinMax(25, 50);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(7, 12);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);
            ArmorAttributes.DurabilityBonus = 12;

            PhysicalBonus = Utility.RandomMinMax(9, 22);
            ColdBonus = Utility.RandomMinMax(2, 6);
		}

        public BlueDiamondArms(Serial serial)
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