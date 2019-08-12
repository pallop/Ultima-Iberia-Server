using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class SleepingAnimal : Item
	{
		private Mobile m_Owner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}
		
		[Constructable]
		public SleepingAnimal() : base( 0x2006 )
		{
			Name = "a sleeping animal";
			//Amount = Owner.Body;
			Stackable = true;
			Movable = false;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( GetWorldLocation(), 2 ) )
			{
				if (m_Owner != null)
				{
					BaseAnimal ba = (BaseAnimal) m_Owner;
					ba.PlaySound(ba.GetAngerSound());
					ba.Awake();
				}
				else 
				{
					from.SendMessage("That animal won't wake up...oops!!");
					Delete();
				}
			}
			else
			{
				from.SendMessage("You can't reach that!");
			}
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			//base.GetProperties( list );
			list.Add(Name);
		}
		
		public SleepingAnimal(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			
			writer.Write(  m_Owner );
			if (m_Owner == null) Delete();
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			switch( version )
			{
				case 1:
				{
					goto case 0;
				}
				case 0:
				{
					m_Owner = reader.ReadMobile();
					break;
				}
			}
		}
	}
}