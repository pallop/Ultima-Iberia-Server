using System;
using Server;
using Server.Engines.Craft;
using Server.Engines.Build;

namespace Server.Items
{
	public class ArchitectTool : BaseBuildingTool
	{
		//public override CraftSystem CraftSystem{ get{ return DefArchitect.CraftSystem; } }
	public override BuildSystem BuildSystem { get { return DefArchitect.BuildSystem; } }
	public override string DefaultName { get { return "Architect Tool"; } }

		[Constructable]
		public ArchitectTool() : base( 7866 )
		{
			Weight = 2.0;
			Name = "Architecture Tool";
		}

		[Constructable]
		public ArchitectTool( int uses ) : base( uses, 7866 )
		{
			Weight = 2.0;
			Name = "Architecture Tool";
		}

		public ArchitectTool( Serial serial ) : base( serial )
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