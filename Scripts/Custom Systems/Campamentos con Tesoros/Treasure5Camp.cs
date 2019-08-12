using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Multis
{
	public class Treasure5Camp : BaseCamp
	{

		[Constructable]
		public Treasure5Camp() : base( 0xF7A )
		{
		}

		public override void AddComponents()
		{
			ChestLevel5 chest = new ChestLevel5();

			TreasureMapChest.Fill( chest, 5 );// Treasure level 5

			AddItem( chest, 0, 0, 0 );// This is where the box will spawn away from center if you like
		}


		public Treasure5Camp( Serial serial ) : base( serial )
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