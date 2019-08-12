/*
 created by:
     /\            888                   888     .d8888b.   .d8888b.  
____/_ \____       888                   888    d88P  Y88b d88P  Y88b 
\  ___\ \  /       888                   888    888    888 888    888 
 \/ /  \/ /    .d88888  8888b.   8888b.  888888 Y88b. d888 Y88b. d888 
 / /\__/_/\   d88" 888     "88b     "88b 888     "Y888P888  "Y888P888 
/__\ \_____\  888  888 .d888888 .d888888 888           888        888 
    \  /      Y88b 888 888  888 888  888 Y88b.  Y88b  d88P Y88b  d88P 
     \/        "Y88888 "Y888888 "Y888888  "Y888  "Y8888P"   "Y8888P"  
*/
using Server;
using Server.Mobiles;

namespace daat99
{
	public class BaseStorageDeed : Item
	{
		private BaseStorage storage;
		[CommandProperty(AccessLevel.GameMaster)]
		public BaseStorage Storage { get { return storage; } private set { storage = value; } }

		[Constructable]
		public BaseStorageDeed(BaseStorage storage)
			: base(0x14F0)
		{
			Storage = storage;
			Weight = 1.0;
			Movable = true;
			Name = "Master Storage Deed: " + storage.Name;
		}

		public BaseStorageDeed(Serial serial) : base(serial) { }

		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
				from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
			else
			{
				MasterStorage backpack = MasterStorageUtils.GetMasterStorage(from as PlayerMobile);
				if (backpack == null || backpack.Deleted)
					from.SendMessage(1173, "You must have your Master Storage in your backpack!");
				else if (backpack.HasStorage(Storage))
					from.SendMessage(1173, "You can store these items already.");
				else if (this.Deleted)
					from.SendMessage(1173, "You can't use this deed anymore.");
				else
				{
					backpack.AddStorage(storage);
					this.Delete();
					from.SendMessage(1173, "You can store {0} items now.", Storage.Name);
				}
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0);
			Storage.Serialize(writer);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Storage = new BaseStorage(reader);
		}
	} 
} 
