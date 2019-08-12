using Server;
using Server.Items;
using Server.Targeting;

namespace Server.Traps
{
    public class DoorTrapInstaller : Item
    {
        private Mobile _owner;
        private DoorTrapType _trapType;
        private int _initialUses;

        public Mobile Owner { get { return _owner; } }
        public DoorTrapType TrapType { get { return _trapType; } }
        public int InitialUses { get { return _initialUses; } }

        public DoorTrapInstaller( Mobile owner, DoorTrapType type, int initialUses )
            : base(0x1EBB)
        {
            _owner = owner;
            _trapType = type;
            _initialUses = initialUses;

            Name = "an installation kit for a door trap";
        }

        public DoorTrapInstaller( Serial serial ) : base(serial) { }

        public virtual bool Install( Mobile m, BaseDoor door, out string message )
        {
            if( door.CanInstallTrap(m) )
            {
                if( door.HasTrap() && this.TrapType == door.TrapType && door.DoorTrap.Refillable )
                {
                    door.DoorTrap.Recharge(this.InitialUses);
                    message = "A trap of the same type was already installed on this door, so you refill its ammunition.";
                    return true;
                }

                if (door.AttachTrap(BaseDoorTrap.CreateTrapByType(_trapType, _owner, _initialUses)))
                {
                    message = "You successfully install the trap.";
                    return true;
                }

                if (door.HasTrap() && this.TrapType != door.TrapType)
                {
                    message = "This door already appears to be trapped.";
                    return false;
                }
            }

            message = "You fail to install the trap.";
            return false;
        }

        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties(list);

            list.Add(1049644, _trapType.ToString()); //[~stuff~]
        }

        public override void OnDoubleClick( Mobile from )
        {
            if( _owner == null )
                _owner = from;

            from.BeginTarget(1, false, TargetFlags.None, new TargetStateCallback(this_doorSelected), this);
            from.SendMessage("Select the door to install this trap onto.");
        }

        private void this_doorSelected( Mobile from, object target, object state )
        {
            BaseDoor door = target as BaseDoor;
            DoorTrapInstaller inst = state as DoorTrapInstaller;
            string message = "";

            if( door == null )
            {
                from.BeginTarget(1, false, TargetFlags.None, new TargetStateCallback(this_doorSelected), inst);
                message = "That is not a door. Try again.";
            }
            else if( inst != null && inst.Install(from, door, out message) )
            {
                inst.Delete();
            }

            from.SendMessage(message);
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write(_owner);
            writer.Write((int)_trapType);
            writer.Write(_initialUses);
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            _owner = reader.ReadMobile();
            _trapType = (DoorTrapType)reader.ReadInt();
            _initialUses = reader.ReadInt();
        }
    }
}