using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	//I gave tham a long name to make sure they did not conflict with other ppls scripts
	public class BaseAnimalMushrooms : Item
	{
		public int m_BaseAnimalMushroomsAmount;

		[CommandProperty(AccessLevel.GameMaster)]
		public int BaseAnimalMushroomsAmount
		{
			get { return m_BaseAnimalMushroomsAmount; }
			set { m_BaseAnimalMushroomsAmount = value; }
		}
		//looked through insideuo I think I got the Grass ItemIds
		private static int[] m_ItemIDs = new int[]
		{
			0x1125, 0xD0F, 0xD0D, 0xD0E, 0xD0F, 0xD10, 0xD11, 0xD12, 0xD13, 0xD14, 0xD15, 0xD16, 0xD17, 0xD18, 0xD19
		};
		[Constructable]
		public BaseAnimalMushrooms()
			: this(25)
		{
		}

		[Constructable]
		public BaseAnimalMushrooms(int amount)
			: base()
		{

			ItemID = Utility.RandomList(m_ItemIDs);
			Stackable = true;
			Name = "mushroom";
			BaseAnimalMushroomsAmount = amount;
			Weight = 1.0;
		}

		public override bool OnDragLift(Mobile from)
		{
			from.SendMessage("You pull some mushrooms out of the ground.");
			this.Consume();
			return true;
		}

		public BaseAnimalMushrooms(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
			writer.Write((int)m_BaseAnimalMushroomsAmount);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
			m_BaseAnimalMushroomsAmount = reader.ReadInt();
		}
	}
}