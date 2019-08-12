using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Multis
{
	public class Treasure1Camp : BaseCamp
	{

		[Constructable]
		public Treasure1Camp() : base( 0xF7A )
		{
		}

		public override void AddComponents()
		{

			ChestLevel1 chest = new ChestLevel1();

			TreasureMapChest.Fill( chest, 1 );// Treasure level 1

			AddItem( chest, 0, 0, 0 );// This is where the box will spawn away from center if you like
		}


		public Treasure1Camp( Serial serial ) : base( serial )
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