using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class AnimalNest : Item
	{
		private Mobile m_Owner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}
		
		private int m_HasEggs;
		[CommandProperty( AccessLevel.GameMaster )]
		public int HasEggs
		{
			get { return m_HasEggs; }
			set {	m_HasEggs = value; }
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			if (m_HasEggs == 1) list.Add(m_HasEggs + " egg");
			else if (m_HasEggs > 1) list.Add(m_HasEggs + " eggs");
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if(from.InRange( this.GetWorldLocation(), 1 ))
			{
				if (m_Owner != null)
				{
					if (m_HasEggs > 0)
					{
						if (m_Owner is BaseAnimal)
						{
							BaseAnimal ba = (BaseAnimal) m_Owner;
							if (ba.Owner != from && ba.Owner != null) ba.Combatant = from;
							if (ba.IsPregnant) ba.IsPregnant = false;
						}
						Eggs egg = new Eggs();
						from.AddToBackpack(egg);
						m_HasEggs -= 1;
						InvalidateProperties();
						from.SendMessage("You take an egg from the nest.");
						if (m_HasEggs < 1) ItemID = 6869;
					}
					else 
					{
						from.SendMessage("The nest is empty.");
						ItemID = 6869;
					}
				}
				else 
				{
					Movable = true;
					m_Owner = from;
					from.SendMessage("You free the nest.");
				}
			}
			else from.SendMessage("You are too far away!");
		}
		
		[Constructable]
		public AnimalNest() : base(6869)
		{
			//Eggshells=2484, Eggs=2485, Nest with eggs=6868, Empty nest=6869
			Name = "a nest";
			Weight = 1.0;
			Movable = false;
		}

		public AnimalNest(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
				base.Serialize(writer);

				writer.Write((int)0); // version
			
				writer.Write(  m_Owner );
				writer.Write( (int) m_HasEggs);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
			
			m_Owner = reader.ReadMobile();
			m_HasEggs = reader.ReadInt();
		}
	}
}