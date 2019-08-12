using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Multis
{
	public class Treasure4Camp : BaseCamp
	{

		[Constructable]
		public Treasure4Camp() : base( 0xF7A )
		{
		}

		public override void AddComponents()
		{
			ChestLevel4 chest = new ChestLevel4();

			TreasureMapChest.Fill( chest, 4 );// Treasure level 4

			AddItem( chest, 0, 0, 0 );// This is where the box will spawn away from center if you like
		}


		public Treasure4Camp( Serial serial ) : base( serial )
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