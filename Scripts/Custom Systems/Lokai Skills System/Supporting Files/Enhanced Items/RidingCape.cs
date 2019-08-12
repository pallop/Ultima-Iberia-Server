using System;
using Server.Network;
using Server.Items;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
	[Flipable( 0x230A, 0x2309 )]
	public class RidingCape : BaseCloak
	{
		[Constructable]
		public RidingCape() : this( 0 )
		{
		}

		[Constructable]
		public RidingCape( int hue ) : base( 0x230A, hue )
		{
            Weight = 4.0;
            XmlAttach.AttachTo(this, new LokaiSkillMod(LokaiSkillName.AnimalRiding, false, true, 10.0, null));
		}

        public override string DefaultName { get { return "Riding Cape"; } }

		public RidingCape( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
