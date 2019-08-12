using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2657, 0x2658 )]
	public class FireRubyArms : BaseArmor
	{

		public override int BaseFireResistance{ get{ return 7; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public FireRubyArms() : base( 0x2657 )
		{
            Name = "FireRuby Arms";
            Hue = 2117;
			Weight = 5.0;

            Attributes.BonusStr = Utility.RandomMinMax(2, 6);
            Attributes.AttackChance = Utility.RandomMinMax(5, 15);
            Attributes.LowerRegCost = Utility.RandomMinMax(3, 10);
            Attributes.Luck = Utility.RandomMinMax(75, 150);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(7, 15);
            Attributes.RegenMana = Utility.RandomMinMax(2, 5);

            EnergyBonus = Utility.RandomMinMax(2, 6);
            FireBonus = Utility.RandomMinMax(8, 15);
		}

        public FireRubyArms(Serial serial)
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