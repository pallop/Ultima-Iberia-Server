using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class FencingKit : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefFenceCrafting.CraftSystem; } }

		[Constructable]
		public FencingKit() : base( 0x1EBA )
		{
			Weight = 2.0;
			Name = "a fencing kit";
		}

		[Constructable]
		public FencingKit( int uses ) : base( uses, 0x1EBA )
		{
			Weight = 2.0;
		}

		public FencingKit( Serial serial ) : base( serial )
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