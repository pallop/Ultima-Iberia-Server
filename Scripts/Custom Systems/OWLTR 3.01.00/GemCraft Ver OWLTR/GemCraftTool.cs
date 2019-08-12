using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class GemCraftTool : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefGemCraft.CraftSystem; } }

		[Constructable]
        public GemCraftTool() : base( 0x1EBC)
		{
			Weight = 8.0;
            Name = "GemCraftTool";
		}

		[Constructable]
        public GemCraftTool( int uses) : base(uses, 0x1EBC)
		{
			Weight = 8.0;
            Name = "GemCraftTool";
		}

        public GemCraftTool(Serial serial)
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
		}
	}
}