using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Multis
{
	public class Treasure6Camp : BaseCamp
	{

		[Constructable]
		public Treasure6Camp() : base( 0xF7A )
		{
		}

		public override void AddComponents()
		{
			ChestLevel6 chest = new ChestLevel6();

			TreasureMapChest.Fill( chest, 6 );// Treasure level 6

			AddItem( chest, 0, 0, 0 );// This is where the box will spawn away from center if you like
		}


		public Treasure6Camp( Serial serial ) : base( serial )
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