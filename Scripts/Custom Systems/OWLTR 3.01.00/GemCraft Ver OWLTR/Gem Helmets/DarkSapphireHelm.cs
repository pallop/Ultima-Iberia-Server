using System;
using Server.Items;

namespace Server.Items
{

	public class DarkSapphireHelm : BaseArmor
	{

		public override int BaseColdResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
        public DarkSapphireHelm()
            : base(5134)
		{
            Name = "DarkSapphire Helm";
            Hue = 1176;
			Weight = 5.0;

            Attributes.BonusDex = Utility.RandomMinMax(2, 8);
            Attributes.DefendChance = Utility.RandomMinMax(4, 10);
            Attributes.LowerRegCost = Utility.RandomMinMax(3, 12);
            Attributes.ReflectPhysical = Utility.RandomMinMax(4, 15);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);

            PoisonBonus = Utility.RandomMinMax(3, 8);
            ColdBonus = Utility.RandomMinMax(4, 10);
		}

        public DarkSapphireHelm(Serial serial)
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