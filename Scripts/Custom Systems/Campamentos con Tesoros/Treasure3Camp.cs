using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Multis
{
	public class Treasure3Camp : BaseCamp
	{

		[Constructable]
		public Treasure3Camp() : base( 0xF7A )
		{
		}

		public override void AddComponents()
		{
			ChestLevel3 chest = new ChestLevel3();

			TreasureMapChest.Fill( chest, 3 );// Treasure level 3

			AddItem( chest, 0, 0, 0 );// This is where the box will spawn away from center if you like
		}


		public Treasure3Camp( Serial serial ) : base( serial )
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