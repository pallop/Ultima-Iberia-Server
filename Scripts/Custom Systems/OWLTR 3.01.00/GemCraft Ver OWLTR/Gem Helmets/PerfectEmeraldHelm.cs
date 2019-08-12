using System;
using Server.Items;

namespace Server.Items
{

	public class PerfectEmeraldHelm : BaseArmor
	{

		public override int BasePoisonResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public PerfectEmeraldHelm() : base( 5134 )
		{
            Name = "PerfectEmerald Helm";
            Hue = 1372;
			Weight = 5.0;

            Attributes.BonusInt = Utility.RandomMinMax(2, 6);
            Attributes.DefendChance = Utility.RandomMinMax(5, 15);
            Attributes.SpellDamage = Utility.RandomMinMax(9, 15);
            Attributes.LowerRegCost = Utility.RandomMinMax(3, 10);
            Attributes.Luck = Utility.RandomMinMax(75, 150);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);
            Attributes.NightSight = 1;

            PoisonBonus = Utility.RandomMinMax(6, 16);
            PhysicalBonus = Utility.RandomMinMax(5, 12);
		}

        public PerfectEmeraldHelm(Serial serial)
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