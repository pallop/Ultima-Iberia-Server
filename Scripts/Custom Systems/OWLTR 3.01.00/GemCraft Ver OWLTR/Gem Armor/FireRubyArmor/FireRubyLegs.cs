using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2647, 0x2648 )]
	public class FireRubyLegs : BaseArmor
	{

		public override int BaseFireResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 60; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public FireRubyLegs() : base( 0x2647 )
		{
            Name = "FireRuby Legs";
            Hue = 2117;
			Weight = 6.0;

            Attributes.BonusInt = Utility.RandomMinMax(3, 5);
            Attributes.AttackChance = Utility.RandomMinMax(3, 12);
            Attributes.DefendChance = Utility.RandomMinMax(2, 7);
            Attributes.LowerRegCost = Utility.RandomMinMax(3, 12);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(3, 15);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);
            Attributes.ReflectPhysical = Utility.RandomMinMax(7, 12);

            PhysicalBonus = Utility.RandomMinMax(3, 5);
            EnergyBonus = Utility.RandomMinMax(12, 18);
		}

        public FireRubyLegs(Serial serial)
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