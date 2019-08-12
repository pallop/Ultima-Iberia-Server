using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Multis
{
	public class Camp : BaseCamp
	{
		private Mobile m_mob;

		[Constructable]
		public Camp() : base( 0xDE3 )
		{
		}

		public override void AddComponents()/// In this section you can define any item and have it place in the camp x,y,z location.
		{

			ChestLevel1 chest = new ChestLevel1();////Chest level 1-6 This is gonna decide what it looks like.
			FirePit firepit = new FirePit();//// Camps firepit.
			Campfire campfire = new Campfire();//// The fire in the pit.
 
			firepit.ItemID = 0xFAC;
			campfire.ItemID = 0xDE3;

			TreasureMapChest.Fill( chest, 1 );//// Chest level 1-6 This decides what kinda loot inside.

			AddItem( chest, 4, 4, 0 );//// Adding items to the ground around the spawner. (x,y,z)
			AddItem( firepit, 0, 0, 0 );
			AddItem( campfire, 0, 0, 1 );

			AddMobile( new Orc(), 15, 2, -2, 0 );/// Adding mobs to the area around the spawner.  (WanderRange,x,y,z)
			AddMobile( new Orc(), 15, -2,  -2, 0 );
			AddMobile( new OrcCaptain(), 15, 2, 2, 0 );
			AddMobile( new OrcishLord(), 15, -2,  2, 0 );

			switch ( Utility.Random( 2 ) )////  This is used for adding random mobs to the camp.
			{
				case 0: m_mob = new Ogre(); break;
				case 1: m_mob = new Ettin(); break;
			}

			AddMobile( m_mob, 15, -2, 0, 0 ); /// This decides where to place the random mob. (WanderRange,x,y,z)
		}

		public Camp( Serial serial ) : base( serial )
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