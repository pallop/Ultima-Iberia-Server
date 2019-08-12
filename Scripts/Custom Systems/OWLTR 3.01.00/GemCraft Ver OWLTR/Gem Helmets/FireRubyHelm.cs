using System;
using Server.Items;

namespace Server.Items
{

	public class FireRubyHelm : BaseArmor
	{

		public override int BaseFireResistance{ get{ return 6; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public FireRubyHelm() : base( 5134 )
		{
            Name = "FireRuby Helm";
            Hue = 2117;
			Weight = 5.0;

            Attributes.BonusDex = Utility.RandomMinMax(3, 5);
            Attributes.DefendChance = Utility.RandomMinMax(4, 15);
            Attributes.LowerManaCost = Utility.RandomMinMax(7, 15);
            Attributes.Luck = Utility.RandomMinMax(75, 125);
            Attributes.ReflectPhysical = Utility.RandomMinMax(4, 12);
            Attributes.RegenStam = Utility.RandomMinMax(2, 6);
            ArmorAttributes.SelfRepair = 3;
            ArmorAttributes.DurabilityBonus = 10;

            EnergyBonus = Utility.RandomMinMax(3, 5);
            FireBonus = Utility.RandomMinMax(4, 12);
		}

        public FireRubyHelm(Serial serial)
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