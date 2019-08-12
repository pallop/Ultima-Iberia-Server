using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Multis
{
	public class LockedDoor1 : BaseCamp
	{
		private Mobile m_Mob1;  //  This is the first mob.
		private Mobile m_Mob2; //  This is the second one.
		private BaseDoor m_Gate;

		[Constructable]
		public LockedDoor1() : base( 0x6B3 )
		{
		}

		public override void AddComponents()
		{
			StrongWoodDoor gate = new StrongWoodDoor( DoorFacing.EastCCW );  /// You can change the door and wich way it faces here.
			m_Gate = gate;

			gate.KeyValue = Key.RandomValue();
			gate.Locked = true;

			AddItem( gate, 0, 0, 0 ); ///  This is where the door will spawn.  Place your spawner in the doorway 

			switch ( Utility.Random( 2 ) )  ///  You can set up random key holders or just one.
			{
				case 0: m_Mob1 = new RatmanMage(); break;
				case 1: m_Mob1 = new RatmanArcher(); break;
			}

			AddMobile( m_Mob1, 5, 0, 4, 0 ); ///  It is nice to have key holder on both sides of the door.

			m_Mob1.AddItem( new Key( KeyType.Iron, gate.KeyValue ) );  /// This line adds the key to the mobs guarding the door.

			switch ( Utility.Random( 2 ) )
			{
				case 0: m_Mob2 = new RatmanMage(); break;
				case 1: m_Mob2 = new RatmanArcher(); break;
			}

			AddMobile( m_Mob2, 5, 0, -4, 0 ); // First number is wander distance then X,Y, and Z location

			m_Mob2.AddItem( new Key( KeyType.Iron, gate.KeyValue ) );

		}

		public LockedDoor1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Gate );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Gate = reader.ReadItem() as BaseDoor;
					break;
				}
			}
		}
	}
}