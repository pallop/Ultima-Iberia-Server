using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2647, 0x2648 )]
	public class WhitePearlLegs : BaseArmor
	{


		public override int BaseEnergyResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 60; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public WhitePearlLegs() : base( 0x2647 )
		{
            Name = "WhitePearl Legs";
            Hue = 1150;
			Weight = 6.0;

            ArmorAttributes.MageArmor = 1;
            Attributes.DefendChance = Utility.RandomMinMax(4, 15);
            Attributes.LowerRegCost = Utility.RandomMinMax(2, 12);
            Attributes.Luck = Utility.RandomMinMax(100, 250);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(3, 16);
            Attributes.RegenHits = Utility.RandomMinMax(3, 5);
            Attributes.RegenStam = Utility.RandomMinMax(3, 7);

            EnergyBonus = Utility.RandomMinMax(2, 15);
            ColdBonus = Utility.RandomMinMax(8, 12);
		}

        public WhitePearlLegs(Serial serial)
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