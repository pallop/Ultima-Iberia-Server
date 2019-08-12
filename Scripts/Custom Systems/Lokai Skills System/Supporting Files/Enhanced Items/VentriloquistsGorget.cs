using System;
using Server.Items;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
	public class VentriloquistsGorget : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 30; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override int AosStrReq{ get{ return 20; } }
		public override int OldStrReq{ get{ return 10; } }

        public override int ArmorBase { get { return 13; } }

        public override string DefaultName { get { return "Ventriloquist's Gorget"; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public VentriloquistsGorget() : base( 0x13C7 )
		{
            Weight = 1.0;
            XmlAttach.AttachTo(this, new LokaiSkillMod(LokaiSkillName.Ventriloquism, false, true, 10.0, null));
		}

        public VentriloquistsGorget(Serial serial)
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