using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class Grasses : Item
	{
		public int m_GrassesAmount;

		[CommandProperty(AccessLevel.GameMaster)]
		public int GrassesAmount
		{
			get { return m_GrassesAmount; }
			set { m_GrassesAmount = value; }
		}
		[Constructable]
		public Grasses( ): this(25)
		{
		}
		//looked through insideuo I think I got the Grass ItemIds
		private static int[] m_ItemIDs = new int[]
		{
			0xCC5, 0xCC7, 0xCb6, 0xCAF, 0xCAD, 0xCb3, 0xCC3, 0xCC4, 0xCC6
		};
		[Constructable]
		public Grasses(int amount): base()
		{
			ItemID = Utility.RandomList(m_ItemIDs); 
			Stackable = true;
			Name = "grass";
			GrassesAmount = amount;
			Weight = 1.0;
		}

		public override bool OnDragLift(Mobile from)
		{
			from.SendMessage("You pull some grass out of the ground.");
			this.Consume();
			return true;
		}

		public Grasses(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
			writer.Write((int)m_GrassesAmount);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
			m_GrassesAmount = reader.ReadInt();
		}
	}
}