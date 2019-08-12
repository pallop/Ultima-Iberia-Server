using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class LocksmithTools : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefLocksmithing.CraftSystem; } }

		[Constructable]
		public LocksmithTools() : base( 0x1EBA )
		{
			Weight = 1.0;
			Name = "Locksmith Tools";
		}

		[Constructable]
		public LocksmithTools( int uses ) : base( uses, 0x1EBA )
		{
			Weight = 1.0;
		}

		public LocksmithTools( Serial serial )
			: base( serial )
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