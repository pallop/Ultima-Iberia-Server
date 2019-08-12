using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2641, 0x2642 )]
	public class PerfectEmeraldChest : BaseArmor
	{

		public override int BasePoisonResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return 155; } }
		public override int InitMaxHits{ get{ return 205; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 60; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public PerfectEmeraldChest() : base( 0x2641 )
		{
            Name = "PerfectEmerald Chest";
            Hue = 1372;
			Weight = 10.0;

            Attributes.RegenHits = Utility.RandomMinMax(5, 8);
            Attributes.SpellDamage = Utility.RandomMinMax(20, 35);
            Attributes.Luck = Utility.RandomMinMax(110, 250);
            Attributes.BonusDex = Utility.RandomMinMax(5, 10);
            Attributes.LowerManaCost = Utility.RandomMinMax(8, 20);
            Attributes.SpellChanneling = 1;
            ArmorAttributes.SelfRepair = 3;

            FireBonus = Utility.RandomMinMax(9, 15);
            PoisonBonus = Utility.RandomMinMax(6, 14);
		}

        public PerfectEmeraldChest(Serial serial)
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