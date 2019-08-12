using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
    [FlipableAttribute(0x9A97, 0x9A98)]
	public class Grinder : BaseTool
	{
        public override CraftSystem CraftSystem { get { return DefGrinding.CraftSystem; } }

		[Constructable]
        public Grinder()
            : base(0x9A97)
		{
			Name = "Grinder";
			Weight = 2.0;
		}

		[Constructable]
        public Grinder(int uses)
            : base(uses, 0x1F81)
		{
            Name = "Grinder";
			Weight = 2.0;
		}

        public Grinder(Serial serial)
            : base(serial)
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

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}
}