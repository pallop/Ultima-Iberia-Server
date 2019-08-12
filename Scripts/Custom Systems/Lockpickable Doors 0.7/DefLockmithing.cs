using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Multis;
using Server.Gumps;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Engines.Craft
{
	public class DefLocksmithing : CraftSystem
	{
		public override SkillName MainSkill {
			get { return SkillName.Tinkering; }
		}

		public override string GumpTitleString {
			get { return ""; }
		}

		private static CraftSystem _craftSystem;

		public static CraftSystem CraftSystem {
			get {
				if( _craftSystem == null )
					_craftSystem = new DefLocksmithing();

				return _craftSystem;
			}
		}

		private DefLocksmithing()
			: base( 1, 1, 5 ) {
		}

		public override double GetChanceAtMin( CraftItem item ) {
			if( item.NameNumber == 1044258 || item.NameNumber == 1046445 ) // potion keg and faction trap removal kit
				return 0.5; // 50%

			return 0.0; // 0%
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType ) {
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public override void PlayCraftEffect( Mobile from ) {
			// no sound
			//from.PlaySound( 0x241 );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item ) {
			if( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if( failed ) {
				if( lostMaterial )
					return -1; // You failed to create the item, and some of your materials are lost.
				else
					return -1; // You failed to create the item, but no materials were lost.
			} else {
				if( quality == 0 )
					return -1; // You were barely able to make this item.  It's quality is below average.
				else if( makersMark && quality == 2 )
					return -1; // You create an exceptional quality item and affix your maker's mark.
				else if( quality == 2 )
					return -1; // You create an exceptional quality item.
				else
					return -1; // You create the item.
			}
		}

		public override bool ConsumeOnFailure( Mobile from, Type resourceType, CraftItem craftItem ) {

			return base.ConsumeOnFailure( from, resourceType, craftItem );
		}
		public override void InitCraftList() {
			int index = -1;


			#region Locks

			index = AddCraft( typeof( InstallDoorLock ), "Locks", "Install Lock", 30.0, 80.0, typeof( IronIngot ), "Iron Ingots", 10, "You need more ingots." );
			AddRes( index, typeof( Gears ), "Gears", 5, "Not enough gears." );
			AddRes( index, typeof( Springs ), "Springs", 3, "Not enough springs." );
			AddRes( index, typeof( Hinge ), "Hinges", 2, "Not enough hinges." );

			index = AddCraft( typeof( UninstallDoorLock ), "Locks", "Uninstall Lock", 30.0, 80.0, typeof( IronIngot ), "", 0, "" );

			#endregion

			SetSubRes( typeof( IronIngot ), 1044022 );

			AddSubRes( typeof( IronIngot ), 1044022, 00.0, 1044036, 1044267 );

			MarkOption = false;
			Repair = false;
			CanEnhance = false;
		}
	}

	/*public abstract class DoorTrapCraft : CustomCraft
	{
		private BaseDoor _door;
		private int _check;
		private string _message;
		private BaseTool _tool;
		private Mobile _from;
		private CraftSystem _craftSystem;

		public BaseDoor Door { get { return _door; } }

		public abstract DoorTrapType DoorTrapType { get; }

		public DoorTrapCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality )
			: base( from, craftItem, craftSystem, typeRes, tool, quality ) {

			_tool = tool;
			_from = from;
			_craftSystem = craftSystem;


		}

		private string Message( BaseDoor door )
        {
            if (door is BaseHouseDoor)
            {
                BaseHouse house = ((BaseHouseDoor)door).FindHouse();

                if (house != null)
                {
                    if (!house.IsFriend(From))
                        return "You do not have permission to modify this lock.";
                }
            }

			if( door == null || door.HasLock == false )
				return "You must target a lockable door.";

			if( door.LockLevel == 0 || door.LockLevel == -255 )
				return "This isn't locked by normal means.";

			if( From.Map != door.Map || !From.InRange( door.GetWorldLocation(), 1 ) )
				return "That is too far away.";

			if( !door.IsAccessibleTo( From ) )
				return "That is inaccessable.";

			if( door.Locked )
				return "You must target an unlocked door.";

			if( door.DoorTrapType != DoorTrapType.None )
				return "That is already trapped!";

			if( door.Open == false )
				return "You must target an open door.";

				return null;
		}
		private int Verify( BaseDoor door ) {

            if (door is BaseHouseDoor)
            {
                BaseHouse house = ((BaseHouseDoor)door).FindHouse();

                if (house != null)
                {

                    if (!house.IsFriend(From))
                        return 1;
                }
            }

			if( door == null || door.HasLock == false )
				return 1;

			if( door.LockLevel == 0 || door.LockLevel == -255 )
				return 1;

			if( From.Map != door.Map || !From.InRange( door.GetWorldLocation(), 1 ) )
				return 1;
			
			if( !door.IsAccessibleTo( From ) )
				return 1;
			
			if( door.Locked ) 
				return 1;

			if( door.DoorTrapType != DoorTrapType.None )
				return 1;
			
			if( door.Open == false )
				return 1;

			return 0;
		}
		private bool Acquire( object target, out string _message, out int _check ) {

			BaseDoor door = target as BaseDoor;
            BaseHouseDoor housedoor = target as BaseHouseDoor;

			_check = Verify( door );
			_message = Message( door );

			if( _check != 0 ) {
				return false;
			} else {
				_door = door;
				return true;
			}
		}

		public override void EndCraftAction() {
			From.SendLocalizedMessage( 502921 ); // What would you like to set a trap on?
			From.Target = new DoorTarget( this, _check, _message );
		}

		private class DoorTarget : Target
		{
			private DoorTrapCraft _doorTrapCraft;
			private int _check;
			private string _message;

			public DoorTarget( DoorTrapCraft doorTrapCraft, int check, string message )
				: base( -1, false, TargetFlags.None ) {
				_doorTrapCraft = doorTrapCraft;
				_check = check;
				_message = message;
			}

			protected override void OnTarget( Mobile from, object targeted ) {

				if( _doorTrapCraft.Acquire( targeted, out _message, out _check ) )
					_doorTrapCraft.CraftItem.CompleteCraft( _doorTrapCraft.Quality, false, _doorTrapCraft.From, _doorTrapCraft.CraftSystem, _doorTrapCraft.TypeRes, _doorTrapCraft.Tool, _doorTrapCraft );
				else
					Failure();
			}

			protected override void OnTargetCancel( Mobile from, TargetCancelType cancelType ) {
				if( cancelType == TargetCancelType.Canceled )
					Failure();
			}

			private void Failure() {
				Mobile from = _doorTrapCraft.From;
				BaseTool tool = _doorTrapCraft.Tool;

				if( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
					from.SendGump( new CraftGump( from, _doorTrapCraft.CraftSystem, tool, _message ) );
				else if( _check > 0 )
					from.SendMessage( _message );
			}
		}

		public override Item CompleteCraft( out int _check ) {
			_check = Verify( this.Door );

			if( _check == 0 ) {
				_from.SendGump( new CraftGump( _from, _craftSystem, _tool, "You successfully trap the door." ) );
				int trapLevel = (int)(From.Skills.Tinkering.Value / 10);

				From.PlaySound( 0x3A4 );
				From.SendMessage( "You successfully trap the door." );

				Door.DoorTrapType = this.DoorTrapType;
				Door.TrapPower = trapLevel * 9;
				Door.TrapLevel = trapLevel;
				Door.TrapOnLockpick = true;

				if( Door.Link != null ) {

					Door.Link.DoorTrapType = this.DoorTrapType;
					Door.Link.TrapPower = trapLevel * 9;
					Door.Link.TrapLevel = trapLevel;
					Door.Link.TrapOnLockpick = true;
				}
			}

			return null;
		}
	}*/

	public abstract class DoorLockCraft : CustomCraft
	{
		private BaseDoor _door;
		private int _check;
		private string _message;

		public BaseDoor Door { get { return _door; } }

		public DoorLockCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality )
			: base( from, craftItem, craftSystem, typeRes, tool, quality ) {
		}

		private string Message( BaseDoor door ) {

            if (door is BaseHouseDoor)
            {
                BaseHouse house = ((BaseHouseDoor)door).FindHouse();

                if (house != null)
                {
                    if (!house.IsFriend(From))
                        return "You do not have permission to modify this lock.";
                }
            }

			if( door == null || door.HasLock )
				return "This door already has a lock.";

			if( From.Map != door.Map || !From.InRange( door.GetWorldLocation(), 1 ) )
				return "That is too far away.";

			if( !door.IsAccessibleTo( From ) )
				return "That is inaccessable.";

			if( door.Locked )
				return "You must target an unlocked door.";

			if( !door.Open)
				return "You must target an open door.";

            if (door.IsPublic)
                return "You may not install locks on public buildings.";

			return null;
		}
		private int Verify( BaseDoor door ) {

            if (door is BaseHouseDoor)
            {
                BaseHouse house = ((BaseHouseDoor)door).FindHouse();

                if (house != null)
                {
                    if (!house.IsFriend(From))
                        return 1;
                }
            }

			if(door == null || door.HasLock)
				return 1;

			if(From.Map != door.Map || !From.InRange( door.GetWorldLocation(), 1 ))
				return 1;

			if(!door.IsAccessibleTo( From ))
				return 1;

			if(door.Locked)
				return 1;

			if(!door.Open)
				return 1;

            if (door.IsPublic)
                return 1;

			return 0;
		}
		private bool Acquire( object target, out string _message, out int _check ) {
			BaseDoor door = target as BaseDoor;

			_check = Verify( door );
			_message = Message( door );

			if( _check != 0 ) {
				return false;
			} else {
				_door = door;
				return true;
			}
		}

		public override void EndCraftAction() {
			From.SendMessage( "Select the door you would like to install a lock on." );
			From.Target = new DoorTarget( this, _check, _message );
		}

		private class DoorTarget : Target
		{
			private DoorLockCraft _doorUnlockCraft;
			private int _check;
			private string _message;

			public DoorTarget( DoorLockCraft doorLockCraft, int check, string message )
				: base( -1, false, TargetFlags.None ) {
				_doorUnlockCraft = doorLockCraft;
				_check = check;
				_message = message;
			}

			protected override void OnTarget( Mobile from, object targeted ) {

				if( _doorUnlockCraft.Acquire( targeted, out _message, out _check ) )
					_doorUnlockCraft.CraftItem.CompleteCraft( _doorUnlockCraft.Quality, false, _doorUnlockCraft.From, _doorUnlockCraft.CraftSystem, _doorUnlockCraft.TypeRes, _doorUnlockCraft.Tool, _doorUnlockCraft );
				else
					Failure();
			}

			protected override void OnTargetCancel( Mobile from, TargetCancelType cancelType ) {
				if( cancelType == TargetCancelType.Canceled )
					Failure();
			}

			private void Failure() {
				Mobile from = _doorUnlockCraft.From;
				BaseTool tool = _doorUnlockCraft.Tool;

				if( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
					from.SendGump( new CraftGump( from, _doorUnlockCraft.CraftSystem, tool, _message ) );
				else if( _check > 0 )
					from.SendMessage( _message );
			}
		}

		public override Item CompleteCraft( out int _check ) {
			_check = Verify( this.Door );

			if( _check == 0 ) {
				int level = (int)(From.Skills.Tinkering.Value);
				uint KeyValue;

				Key key = new Key( KeyType.Iron, Key.RandomValue() );

				KeyValue = key.KeyValue;
				From.AddToBackpack( key );

				From.PlaySound( 0x3A4 );
				From.SendMessage( "You successfully install the lock." );

				Door.HasLock = true;
				Door.RequiredSkill = level - 20;
				Door.MaxLockLevel = level;
				Door.LockLevel = level - 10;
				Door.KeyValue = KeyValue;

				if( Door.Link != null ) {

					Door.Link.HasLock = true;
					Door.Link.RequiredSkill = level - 20;
					Door.Link.MaxLockLevel = level;
					Door.Link.LockLevel = level - 10;
					Door.Link.KeyValue = KeyValue;
				}
			}

			return null;
		}
	}

	[CraftItemID( 0x1EBA )]
	public class InstallDoorLock : DoorLockCraft
	{
		public InstallDoorLock( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality )
			: base( from, craftItem, craftSystem, typeRes, tool, quality ) {
		}
	}

	public abstract class DoorUnlockCraft : CustomCraft
	{
		private BaseDoor _door;
		private int _check;
		private string _message;

		public BaseDoor Door { get { return _door; } }

		public DoorUnlockCraft( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality )
			: base( from, craftItem, craftSystem, typeRes, tool, quality ) {
		}

		private string Message( BaseDoor door ) {

			if( door.RequiredSkill > (int)(From.Skills.Tinkering.Value) )
				return "You have no idea how to work this lock.";

			if( door == null || door.HasLock == false )
				return "This door doesn't have a lock.";

			if( From.Map != door.Map || !From.InRange( door.GetWorldLocation(), 1 ) )
				return "That is too far away.";

			if( !door.IsAccessibleTo( From ) )
				return "That is inaccessable.";

			if( door.Locked )
				return "You must target an unlocked door.";

			if( door.Open == false )
				return "You must target an open door.";

			return null;
		}
		private int Verify( BaseDoor door ) {

			if( door.RequiredSkill > (int)(From.Skills.Tinkering.Value) )
				return 1;

			if( door == null || door.HasLock == false )
				return 1;

			if( From.Map != door.Map || !From.InRange( door.GetWorldLocation(), 1 ) )
				return 1;

			if( !door.IsAccessibleTo( From ) )
				return 1;

			if( door.Locked )
				return 1;

			if( door.Open == false )
				return 1;

			return 0;
		}
		private bool Acquire( object target, out string _message, out int _check ) {
			BaseDoor door = target as BaseDoor;

			_check = Verify( door );
			_message = Message( door );

			if( _check != 0 ) {
				return false;
			} else {
				_door = door;
				return true;
			}
		}

		public override void EndCraftAction() {
			From.SendMessage( "Select the door you would like to uninstall the lock from." );
			From.Target = new DoorTarget( this, _check, _message );
		}

		private class DoorTarget : Target
		{
			private DoorUnlockCraft _doorUnlockCraft;
			private int _check;
			private string _message;

			public DoorTarget( DoorUnlockCraft doorLockCraft, int check, string message )
				: base( -1, false, TargetFlags.None ) {
				_doorUnlockCraft = doorLockCraft;
				_check = check;
				_message = message;
			}

			protected override void OnTarget( Mobile from, object targeted ) {

				if( _doorUnlockCraft.Acquire( targeted, out _message, out _check ) )
					_doorUnlockCraft.CraftItem.CompleteCraft( _doorUnlockCraft.Quality, false, _doorUnlockCraft.From, _doorUnlockCraft.CraftSystem, _doorUnlockCraft.TypeRes, _doorUnlockCraft.Tool, _doorUnlockCraft );
				else
					Failure();
			}

			protected override void OnTargetCancel( Mobile from, TargetCancelType cancelType ) {
				if( cancelType == TargetCancelType.Canceled )
					Failure();
			}

			private void Failure() {
				Mobile from = _doorUnlockCraft.From;
				BaseTool tool = _doorUnlockCraft.Tool;

				if( tool != null && !tool.Deleted && tool.UsesRemaining > 0 )
					from.SendGump( new CraftGump( from, _doorUnlockCraft.CraftSystem, tool, _message ) );
				else if( _check > 0 )
					from.SendMessage( _message );
			}
		}

		public override Item CompleteCraft( out int _check ) {
			_check = Verify( this.Door );

			if( _check == 0 ) {

				From.PlaySound( 0x3A4 );
				From.SendMessage( "You successfully disassemble the lock." );

				Door.HasLock = false;
				Door.RequiredSkill = 0;
				Door.MaxLockLevel = 0;
				Door.LockLevel = 0;
				Door.KeyValue = 0;

				if( Door.Link != null ) {

					Door.Link.HasLock = false;
					Door.Link.RequiredSkill = 0;
					Door.Link.MaxLockLevel = 0;
					Door.Link.LockLevel = 0;
					Door.Link.KeyValue = 0;
				}
			}

			return null;
		}
	}

	[CraftItemID( 0x1EBA )]
	public class UninstallDoorLock : DoorUnlockCraft
	{
		public UninstallDoorLock( Mobile from, CraftItem craftItem, CraftSystem craftSystem, Type typeRes, BaseTool tool, int quality )
			: base( from, craftItem, craftSystem, typeRes, tool, quality ) {
		}
	}
}