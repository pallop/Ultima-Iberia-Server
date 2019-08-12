using System;
using Server;
using Server.Items;

namespace Server.Traps
{
	public enum DoorTrapType
	{
		None = 0,
		Arrow,
		Dart,
		Poison,
		Explosion,
		Guillotine
	}

	public class BaseDoorTrap
	{
		private Mobile _owner;
		private DoorTrapType _type;
		private bool _active;
		private bool _refillable;
		private int _uses;
		private BaseDoor _door;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get { return _owner; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public DoorTrapType TrapType { get { return _type; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active { get { return _active; } set { _active = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Refillable { get { return _refillable; } set { _refillable = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Uses { get { return _uses; } set { _uses = value; } }

		[CommandProperty( AccessLevel.GameMaster, AccessLevel.Administrator + 1 )]
		public BaseDoor Door { get { return _door; } set { _door = value; } }

		public BaseDoorTrap( Mobile owner, DoorTrapType type, bool refillable, int uses )
		{
			_owner = owner;
			_type = type;
			_active = false;
			_refillable = refillable;
			_uses = uses;
		}

		public static BaseDoorTrap CreateTrapByType( DoorTrapType type, Mobile owner, int uses )
		{
			BaseDoorTrap trap = null;

			switch( type )
			{
				case DoorTrapType.Arrow:
					trap = new DoorArrowTrap( owner, uses );
					break;
				case DoorTrapType.Dart:
					trap = new DoorDartTrap( owner, uses );
					break;
				case DoorTrapType.Explosion:
					trap = new DoorExplosionTrap( owner );
					break;
				case DoorTrapType.Guillotine:
					trap = new DoorGuillotineTrap( owner );
					break;
				case DoorTrapType.Poison:
					trap = new DoorPoisonTrap( owner, uses );
					break;
			}

			return trap;
		}

		public virtual void ExecuteTrap( Mobile from )
		{
			from.DoHarmful( Owner, true );
			from.RevealingAction();
			from.SendMessage( "You've set off a trap!" );
		}

		public virtual void Recharge( int amount )
		{
			Uses += amount;

			if( Uses > 0 )
				Active = true;
		}

		public virtual bool Trigger( Mobile from )
		{
			if( !Active || TrapType == DoorTrapType.None || Uses <= 0 )
				return false;

			if( --Uses <= 0 )
				Active = false;

			ExecuteTrap( from );

			return true;
		}

		#region +virtual void Serialize( GenericWriter )
		public virtual void Serialize( GenericWriter writer )
		{
			writer.Write( (int)0 );

			writer.Write( _owner );
			writer.Write( (int)_type );
			writer.Write( _active );
			writer.Write( _refillable );
			writer.Write( _uses );
			writer.Write( _door );
		}
		#endregion

		#region +BaseDoorTrap( GenericReader )
		public BaseDoorTrap( GenericReader reader )
		{
			int version = reader.ReadInt();

			_owner = reader.ReadMobile();
			_type = (DoorTrapType)reader.ReadInt();
			_active = reader.ReadBool();
			_refillable = reader.ReadBool();
			_uses = reader.ReadInt();
			_door = (BaseDoor)reader.ReadItem();
		}
		#endregion
	}
}