using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2643, 0x2644 )]
	public class FireRubyGloves : BaseArmor
	{

		public override int BaseFireResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 30; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public FireRubyGloves() : base( 0x2643 )
		{
            Name = "FireRuby Gloves";
            Hue = 2117;
			Weight = 2.0;

            ArmorAttributes.SelfRepair = Utility.RandomMinMax(3, 8);
            Attributes.DefendChance = Utility.RandomMinMax(7, 19);
            Attributes.LowerRegCost = Utility.RandomMinMax(8, 20);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(9, 20);
            ArmorAttributes.MageArmor = 1;
            Attributes.RegenStam = Utility.RandomMinMax(3, 5);

            PoisonBonus = Utility.RandomMinMax(3, 6);
            FireBonus = Utility.RandomMinMax(5, 12);
		}

        public FireRubyGloves(Serial serial)
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