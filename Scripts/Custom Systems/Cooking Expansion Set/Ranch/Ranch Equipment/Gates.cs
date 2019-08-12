using Server;
using System;
using Server.Mobiles;

// -------------------- Gates ----------------------
namespace Server.Items
{
	public abstract class FenceGate : BaseDoor
	{
		private RanchStone m_ranchstone;
		[CommandProperty( AccessLevel.GameMaster )]
		public RanchStone ranchstone
		{
			get { return m_ranchstone; }
			set {	m_ranchstone = value; InvalidateProperties(); }
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			if (m_ranchstone != null) list.Add(m_ranchstone.Ranch);
		}
		
		public override void OnDoubleClick( Mobile m )
		{
			base.OnDoubleClick( m );
			if (m_ranchstone != null)
			{
				PlayerMobile pm = (PlayerMobile) m;
				if (ranchstone.Owner != pm)
				{
					pm.SendMessage("You may not open a ranch door that you don't own.");
					this.Locked = true;
				}
				else this.Locked = false;
			}
		}

		public FenceGate(DoorFacing facing): base(0x866 + (2 * (int)facing), 0x867 + (2 * (int)facing), 0xEB, 0xF2, BaseDoor.GetOffset(facing))
		{
			Weight = 50.0;
			Movable = true;
		}

		public FenceGate(Serial serial): base(serial)
		{
		}

		public override bool Decays { get { return false; } }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write(m_ranchstone);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_ranchstone = (RanchStone)reader.ReadItem();
		}
	}

	public class NorthGate : FenceGate
	{
		[Constructable]
		public NorthGate () : base(DoorFacing.NorthCCW) 
		{
			Name = "a north gate";
		}

		public NorthGate ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class WestGate : FenceGate
	{
		[Constructable]
		public WestGate (  ) : base( DoorFacing.WestCW )
		{
			Name = "a west gate";
		}

		public WestGate ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}