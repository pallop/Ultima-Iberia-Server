using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class Remains : Item
	{
		public int m_RemainsAmount;
		
		private DateTime m_CreationTime;
		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime CreationTime
		{
			get{ return m_CreationTime; }
			set{ m_CreationTime = value; }
		}

		private Mobile m_Owner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}

		private Mobile m_Killer;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Killer
		{
			get{ return m_Killer; }
			set{ m_Killer = value; }
		}
		
		[CommandProperty(AccessLevel.GameMaster)]
		public int RemainsAmount
		{
			get { return m_RemainsAmount; }
			set { m_RemainsAmount = value; }
		}

		[Constructable]
		public Remains()
			: this(0x1B19, 1, String.Empty)
		{
		}

		[Constructable]
		public Remains(int itemid, int amount, string name): this(itemid, amount, 0, name)
		{
		}

		[Constructable]
		public Remains(int itemid, int amount, int hue, string name): base(itemid)
		{
			Stackable = true;
			Name = name;
			m_RemainsAmount = amount;
			Weight = 10.0;
		}

		public override bool OnDragLift(Mobile from)
		{
			from.SendMessage("The remains fall apart in your hands.");
			this.Delete();
			return true;
		}

		public Remains(Serial serial): base(serial)
		{
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			if (String.IsNullOrEmpty(Name))
				list.Add("animal remains");
			else
				list.Add("the remains of " + Name);
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)1); // version

			writer.Write( m_CreationTime );
			writer.Write(  m_Owner );
			writer.Write(  m_Killer );
			
			writer.Write((int)m_RemainsAmount);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch( version )
			{
				case 1:
				{
					m_CreationTime = reader.ReadDateTime();
					m_Owner = reader.ReadMobile();
					m_Killer = reader.ReadMobile();
					goto case 0;
				}
				case 0:
				{
					m_RemainsAmount = reader.ReadInt();
					break;
				}
			}
			
			if (Name == "remains")
				Name = String.Empty;
		}
	}
}