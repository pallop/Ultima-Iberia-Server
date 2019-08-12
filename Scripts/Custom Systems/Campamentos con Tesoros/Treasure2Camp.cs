using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Multis
{
	public class Treasure2Camp : BaseCamp
	{

		[Constructable]
		public Treasure2Camp() : base( 0xF7A )
		{
		}

		public override void AddComponents()
		{

			ChestLevel2 chest = new ChestLevel2();

			TreasureMapChest.Fill( chest, 2 );// Treasure level 2

			AddItem( chest, 0, 0, 0 );// This is where the box will spawn away from center if you like
		}


		public Treasure2Camp( Serial serial ) : base( serial )
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