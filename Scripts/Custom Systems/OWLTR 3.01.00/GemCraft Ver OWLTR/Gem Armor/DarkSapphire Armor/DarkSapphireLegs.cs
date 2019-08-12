using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2647, 0x2648 )]
	public class DarkSapphireLegs : BaseArmor
	{

		public override int BaseColdResistance{ get{ return 7; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 60; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public DarkSapphireLegs() : base( 0x2647 )
		{
            Name = "DarkSapphire Legs";
            Hue = 1176;
			Weight = 6.0;

            ArmorAttributes.MageArmor = 1;
            Attributes.DefendChance = Utility.RandomMinMax(4, 15);
            Attributes.LowerRegCost = Utility.RandomMinMax(2, 12);
            Attributes.Luck = Utility.RandomMinMax(100, 250);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(3, 16);
            Attributes.RegenHits = Utility.RandomMinMax(3, 5);
            Attributes.RegenStam = Utility.RandomMinMax(3, 7);

            EnergyBonus = Utility.RandomMinMax(2, 5);
            ColdBonus = Utility.RandomMinMax(8, 25);
		}

        public DarkSapphireLegs(Serial serial)
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