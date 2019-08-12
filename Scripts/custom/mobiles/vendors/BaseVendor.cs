using System;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Engines.BulkOrders;
using Server.Factions;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Regions;
using daat99;

namespace Server.Mobiles
{
	public enum VendorShoeType
	{
		None,
		Shoes,
		Boots,
		Sandals,
		ThighBoots
	}

	public abstract class BaseVendor : BaseCreature, IVendor
	{
		private const int MaxSell = 500;

		protected abstract List<SBInfo> SBInfos { get; }

		private readonly ArrayList m_ArmorBuyInfo = new ArrayList();
		private readonly ArrayList m_ArmorSellInfo = new ArrayList();

		private DateTime m_LastRestock;
		private DateTime m_NextTrickOrTreat;

		public override bool CanTeach
		{
			get
			{
				return true;
			}
		}

		public override bool BardImmune
		{
			get
			{
				return true;
			}
		}

		public override bool PlayerRangeSensitive
		{
			get
			{
				return true;
			}
		}

		public virtual bool IsActiveVendor
		{
			get
			{
				return true;
			}
		}
		public virtual bool IsActiveBuyer
		{
			get
			{
				return this.IsActiveVendor;
			}
		}// response to vendor SELL
		public virtual bool IsActiveSeller
		{
			get
			{
				return this.IsActiveVendor;
			}
		}// repsonse to vendor BUY

		public virtual NpcGuild NpcGuild
		{
			get
			{
				return NpcGuild.None;
			}
		}

		public override bool IsInvulnerable
		{
			get
			{
				return true;
			}
		}

		public virtual DateTime NextTrickOrTreat
		{ 
			get 
			{ 
				return this.m_NextTrickOrTreat; 
			}
			set
			{
				this.m_NextTrickOrTreat = value;
			}
		}

		public override bool ShowFameTitle
		{
			get
			{
				return false;
			}
		}

		public virtual bool IsValidBulkOrder(Item item)
		{
			return false;
		}

		public virtual Item CreateBulkOrder(Mobile from, bool fromContextMenu)
		{
			return null;
		}

		public virtual bool SupportsBulkOrders(Mobile from)
		{
			return false;
		}

		public virtual TimeSpan GetNextBulkOrder(Mobile from)
		{
			return TimeSpan.Zero;
		}

		public virtual void OnSuccessfulBulkOrderReceive(Mobile from)
		{
		}

		#region Faction
		public virtual int GetPriceScalar()
		{
			Town town = Town.FromRegion(this.Region);

			if (town != null)
				return (100 + town.Tax);

			return 100;
		}

		public void UpdateBuyInfo()
		{
			int priceScalar = this.GetPriceScalar();

			IBuyItemInfo[] buyinfo = (IBuyItemInfo[])this.m_ArmorBuyInfo.ToArray(typeof(IBuyItemInfo));

			if (buyinfo != null)
			{
				foreach (IBuyItemInfo info in buyinfo)
					info.PriceScalar = priceScalar;
			}
		}

		#endregion

		private class BulkOrderInfoEntry : ContextMenuEntry
		{
			private readonly Mobile m_From;
			private readonly BaseVendor m_Vendor;

			public BulkOrderInfoEntry(Mobile from, BaseVendor vendor)
				: base(6152)
			{
				this.m_From = from;
				this.m_Vendor = vendor;
			}

			public override void OnClick()
			{
				if (this.m_Vendor.SupportsBulkOrders(this.m_From))
				{
					TimeSpan ts = this.m_Vendor.GetNextBulkOrder(this.m_From);

					int totalSeconds = (int)ts.TotalSeconds;
					int totalHours = (totalSeconds + 3599) / 3600;
					int totalMinutes = (totalSeconds + 59) / 60;

					if (((Core.SE) ? totalMinutes == 0 : totalHours == 0))
					{
						this.m_From.SendLocalizedMessage(1049038); // You can get an order now.

						if (Core.AOS)
						{
							Item bulkOrder = this.m_Vendor.CreateBulkOrder(this.m_From, true);

							if (bulkOrder is LargeBOD)
								this.m_From.SendGump(new LargeBODAcceptGump(this.m_From, (LargeBOD)bulkOrder));
							else if (bulkOrder is SmallBOD)
								this.m_From.SendGump(new SmallBODAcceptGump(this.m_From, (SmallBOD)bulkOrder));
						}
					}
					else
					{
						int oldSpeechHue = this.m_Vendor.SpeechHue;
						this.m_Vendor.SpeechHue = 0x3B2;

						if (Core.SE)
							this.m_Vendor.SayTo(this.m_From, 1072058, totalMinutes.ToString()); // An offer may be available in about ~1_minutes~ minutes.
						else
							this.m_Vendor.SayTo(this.m_From, 1049039, totalHours.ToString()); // An offer may be available in about ~1_hours~ hours.

						this.m_Vendor.SpeechHue = oldSpeechHue;
					}
				}
			}
		}

		public BaseVendor(string title)
			: base(AIType.AI_Vendor, FightMode.None, 2, 1, 0.5, 2)
		{
			this.LoadSBInfo();

			this.Title = title;
			
			this.InitBody();
			this.InitOutfit();

			Container pack;
			//these packs MUST exist, or the client will crash when the packets are sent
			pack = new Backpack();
			pack.Layer = Layer.ShopBuy;
			pack.Movable = false;
			pack.Visible = false;
			this.AddItem(pack);

			pack = new Backpack();
			pack.Layer = Layer.ShopResale;
			pack.Movable = false;
			pack.Visible = false;
			this.AddItem(pack);

			this.m_LastRestock = DateTime.Now;
		}

		public BaseVendor(Serial serial)
			: base(serial)
		{
		}

		public DateTime LastRestock
		{
			get
			{
				return this.m_LastRestock;
			}
			set
			{
				this.m_LastRestock = value;
			}
		}

		public virtual TimeSpan RestockDelay
		{
			get
			{
				return TimeSpan.FromHours(1);
			}
		}

		public Container BuyPack
		{
			get
			{
				Container pack = this.FindItemOnLayer(Layer.ShopBuy) as Container;

				if (pack == null)
				{
					pack = new Backpack();
					pack.Layer = Layer.ShopBuy;
					pack.Visible = false;
					this.AddItem(pack);
				}

				return pack;
			}
		}

		public abstract void InitSBInfo();

		public virtual bool IsTokunoVendor
		{
			get
			{
				return (this.Map == Map.Tokuno);
			}
		}

		protected void LoadSBInfo()
		{
			this.m_LastRestock = DateTime.Now;

			for (int i = 0; i < this.m_ArmorBuyInfo.Count; ++i)
			{
				GenericBuyInfo buy = this.m_ArmorBuyInfo[i] as GenericBuyInfo;

				if (buy != null)
					buy.DeleteDisplayEntity();
			}

			this.SBInfos.Clear();

			this.InitSBInfo();

			this.m_ArmorBuyInfo.Clear();
			this.m_ArmorSellInfo.Clear();

			for (int i = 0; i < this.SBInfos.Count; i++)
			{
				SBInfo sbInfo = (SBInfo)this.SBInfos[i];
				this.m_ArmorBuyInfo.AddRange(sbInfo.BuyInfo);
				this.m_ArmorSellInfo.Add(sbInfo.SellInfo);
			}
		}

		public virtual bool GetGender()
		{
			return Utility.RandomBool();
		}

		public virtual void InitBody()
		{
			this.InitStats(100, 100, 25);

			this.SpeechHue = Utility.RandomDyedHue();
			this.Hue = Utility.RandomSkinHue();

			if (this.IsInvulnerable && !Core.AOS)
				this.NameHue = 0x35;

			if (this.Female = this.GetGender())
			{
				this.Body = 0x191;
				this.Name = NameList.RandomName("female");
			}
			else
			{
				this.Body = 0x190;
				this.Name = NameList.RandomName("male");
			}
		}

		public virtual int GetRandomHue()
		{
			switch ( Utility.Random(5) )
			{
				default:
				case 0:
					return Utility.RandomBlueHue();
				case 1:
					return Utility.RandomGreenHue();
				case 2:
					return Utility.RandomRedHue();
				case 3:
					return Utility.RandomYellowHue();
				case 4:
					return Utility.RandomNeutralHue();
			}
		}

		public virtual int GetShoeHue()
		{
			if (0.1 > Utility.RandomDouble())
				return 0;

			return Utility.RandomNeutralHue();
		}

		public virtual VendorShoeType ShoeType
		{
			get
			{
				return VendorShoeType.Shoes;
			}
		}

		public virtual int RandomBrightHue()
		{
			if (0.1 > Utility.RandomDouble())
				return Utility.RandomList(0x62, 0x71);

			return Utility.RandomList(0x03, 0x0D, 0x13, 0x1C, 0x21, 0x30, 0x37, 0x3A, 0x44, 0x59);
		}

		public virtual void CheckMorph()
		{
			if (this.CheckGargoyle())
				return;
			#region SA
			else if (this.CheckTerMur())
				return;
			#endregion
			else if (this.CheckNecromancer())
				return;
			else if (this.CheckTokuno())
				return;

			if (this.Female = this.GetGender())
			{
				this.Body = 0x191;
				this.Name = NameList.RandomName("female");
			}
			else
			{
				this.Body = 0x190;
				this.Name = NameList.RandomName("male");
			}
		}

		public virtual bool CheckTokuno()
		{
			if (this.Map != Map.Tokuno)
				return false;

			NameList n;

			if (this.Female)
				n = NameList.GetNameList("tokuno female");
			else
				n = NameList.GetNameList("tokuno male");

			if (!n.ContainsName(this.Name))
				this.TurnToTokuno();

			return true;
		}

		public virtual void TurnToTokuno()
		{
			if (this.Female)
				this.Name = NameList.RandomName("tokuno female");
			else
				this.Name = NameList.RandomName("tokuno male");
		}

		public virtual bool CheckGargoyle()
		{
			Map map = this.Map;

			if (map != Map.Ilshenar)
				return false;

			if (!this.Region.IsPartOf("Gargoyle City"))
				return false;

			if (this.Body != 0x2F6 || (this.Hue & 0x8000) == 0)
				this.TurnToGargoyle();

			return true;
		}

		#region SA Change
		public virtual bool CheckTerMur()
		{
			Map map = this.Map;

			if (map != Map.TerMur)
				return false;

			if (!this.Region.IsPartOf("Royal City") && !this.Region.IsPartOf("Holy City"))
				return false;

			if (this.Body != 0x29A || this.Body != 0x29B)
				this.TurnToGargRace();

			return true;
		}

		#endregion

		public virtual bool CheckNecromancer()
		{
			Map map = this.Map;

			if (map != Map.Malas)
				return false;

			if (!this.Region.IsPartOf("Umbra"))
				return false;

			if (this.Hue != 0x83E8)
				this.TurnToNecromancer();

			return true;
		}

		public override void OnAfterSpawn()
		{
			this.CheckMorph();
		}

		protected override void OnMapChange(Map oldMap)
		{
			base.OnMapChange(oldMap);

			this.CheckMorph();

			this.LoadSBInfo();
		}

		public virtual int GetRandomNecromancerHue()
		{
			switch ( Utility.Random(20) )
			{
				case 0:
					return 0;
				case 1:
					return 0x4E9;
				default:
					return Utility.RandomList(0x485, 0x497);
			}
		}

		public virtual void TurnToNecromancer()
		{
			for (int i = 0; i < this.Items.Count; ++i)
			{
				Item item = this.Items[i];

				if (item is Hair || item is Beard)
					item.Hue = 0;
				else if (item is BaseClothing || item is BaseWeapon || item is BaseArmor || item is BaseTool)
					item.Hue = this.GetRandomNecromancerHue();
			}

			this.HairHue = 0;
			this.FacialHairHue = 0;

			this.Hue = 0x83E8;
		}

		public virtual void TurnToGargoyle()
		{
			for (int i = 0; i < this.Items.Count; ++i)
			{
				Item item = this.Items[i];

				if (item is BaseClothing || item is Hair || item is Beard)
					item.Delete();
			}

			this.HairItemID = 0;
			this.FacialHairItemID = 0;

			this.Body = 0x2F6;
			this.Hue = this.RandomBrightHue() | 0x8000;
			this.Name = NameList.RandomName("gargoyle vendor");

			this.CapitalizeTitle();
		}
		
		#region SA
		public virtual void TurnToGargRace()
		{
			for (int i = 0; i < this.Items.Count; ++i)
			{
				Item item = this.Items[i];

				if (item is BaseClothing)
					item.Delete();
			}

			this.Race = Race.Gargoyle;
			
			this.Hue = this.Race.RandomSkinHue();
			
			this.HairItemID = this.Race.RandomHair(this.Female);
			this.HairHue = this.Race.RandomHairHue();
			
			this.FacialHairItemID = this.Race.RandomFacialHair(this.Female);
			if (this.FacialHairItemID != 0)
			{
				this.FacialHairHue = this.Race.RandomHairHue();
			}
			else
			{
				this.FacialHairHue = 0;
			}

			this.InitGargOutfit();

			if ( Female = GetGender() )
			{
				Body = 0x29B;
				Name = NameList.RandomName( "gargoyle female" );
			}
			else
			{
				Body = 0x29A;
				Name = NameList.RandomName( "gargoyle male" );
			}

			this.CapitalizeTitle();
		}

		#endregion

		public virtual void CapitalizeTitle()
		{
			string title = this.Title;

			if (title == null)
				return;

			string[] split = title.Split(' ');

			for (int i = 0; i < split.Length; ++i)
			{
				if (Insensitive.Equals(split[i], "the"))
					continue;

				if (split[i].Length > 1)
					split[i] = Char.ToUpper(split[i][0]) + split[i].Substring(1);
				else if (split[i].Length > 0)
					split[i] = Char.ToUpper(split[i][0]).ToString();
			}

			this.Title = String.Join(" ", split);
		}

		public virtual int GetHairHue()
		{
			return Utility.RandomHairHue();
		}

		public virtual void InitOutfit()
		{
			switch ( Utility.Random(3) )
			{
				case 0:
					this.AddItem(new FancyShirt(this.GetRandomHue()));
					break;
				case 1:
					this.AddItem(new Doublet(this.GetRandomHue()));
					break;
				case 2:
					this.AddItem(new Shirt(this.GetRandomHue()));
					break;
			}

			switch ( this.ShoeType )
			{
				case VendorShoeType.Shoes:
					this.AddItem(new Shoes(this.GetShoeHue()));
					break;
				case VendorShoeType.Boots:
					this.AddItem(new Boots(this.GetShoeHue()));
					break;
				case VendorShoeType.Sandals:
					this.AddItem(new Sandals(this.GetShoeHue()));
					break;
				case VendorShoeType.ThighBoots:
					this.AddItem(new ThighBoots(this.GetShoeHue()));
					break;
			}

			int hairHue = this.GetHairHue();

			Utility.AssignRandomHair(this, hairHue);
			Utility.AssignRandomFacialHair(this, hairHue);

			if (this.Female)
			{
				switch ( Utility.Random(6) )
				{
					case 0:
						this.AddItem(new ShortPants(this.GetRandomHue()));
						break;
					case 1:
					case 2:
						this.AddItem(new Kilt(this.GetRandomHue()));
						break;
					case 3:
					case 4:
					case 5:
						this.AddItem(new Skirt(this.GetRandomHue()));
						break;
				}
			}
			else
			{
				switch ( Utility.Random(2) )
				{
					case 0:
						this.AddItem(new LongPants(this.GetRandomHue()));
						break;
					case 1:
						this.AddItem(new ShortPants(this.GetRandomHue()));
						break;
				}
			}
			
			this.PackGold(100, 200);
		}
		
		#region SA
		public virtual void InitGargOutfit()
		{
			for (int i = 0; i < this.Items.Count; ++i)
			{
				Item item = this.Items[i];

				if (item is BaseClothing)
					item.Delete();
			}

			if (this.Female)
			{
				switch ( Utility.Random(2) )
				{
					case 0:
						this.AddItem(new FemaleGargishClothLegs(this.GetRandomHue())); 
						this.AddItem(new FemaleGargishClothKilt(this.GetRandomHue()));
						this.AddItem(new FemaleGargishClothChest(this.GetRandomHue()));
						break;
					case 1:
						this.AddItem(new FemaleGargishClothKilt(this.GetRandomHue())); 
						this.AddItem(new FemaleGargishClothChest(this.GetRandomHue()));
						break;
				}
			}
			else
			{
				switch ( Utility.Random(2) )
				{
					case 0:
						this.AddItem(new MaleGargishClothLegs(this.GetRandomHue())); 
						this.AddItem(new MaleGargishClothKilt(this.GetRandomHue()));
						this.AddItem(new MaleGargishClothChest(this.GetRandomHue()));
						break;
					case 1:
						this.AddItem(new MaleGargishClothKilt(this.GetRandomHue())); 
						this.AddItem(new MaleGargishClothChest(this.GetRandomHue()));
						break;
				}
			}
			this.PackGold(100, 200);
		}

		#endregion
		
		public virtual void Restock()
		{
			this.m_LastRestock = DateTime.Now;

			IBuyItemInfo[] buyInfo = this.GetBuyInfo();

			foreach (IBuyItemInfo bii in buyInfo)
				bii.OnRestock();
		}

		private static readonly TimeSpan InventoryDecayTime = TimeSpan.FromHours(1.0);

		public virtual void VendorBuy(Mobile from)
		{
			if (!this.IsActiveSeller)
				return;

			if (!from.CheckAlive())
				return;

			if (!this.CheckVendorAccess(from))
			{
				this.Say(501522); // I shall not treat with scum like thee!
				return;
			}

			if (DateTime.Now - this.m_LastRestock > this.RestockDelay)
				this.Restock();

			this.UpdateBuyInfo();

			int count = 0;
			List<BuyItemState> list;
			IBuyItemInfo[] buyInfo = this.GetBuyInfo();
			IShopSellInfo[] sellInfo = this.GetSellInfo();

			list = new List<BuyItemState>(buyInfo.Length);
			Container cont = this.BuyPack;

			List<ObjectPropertyList> opls = null;

			for (int idx = 0; idx < buyInfo.Length; idx++)
			{
				IBuyItemInfo buyItem = (IBuyItemInfo)buyInfo[idx];

				if (buyItem.Amount <= 0 || list.Count >= 250)
					continue;

				// NOTE: Only GBI supported; if you use another implementation of IBuyItemInfo, this will crash
				GenericBuyInfo gbi = (GenericBuyInfo)buyItem;
				IEntity disp = gbi.GetDisplayEntity();

				list.Add(new BuyItemState(buyItem.Name, cont.Serial, disp == null ? (Serial)0x7FC0FFEE : disp.Serial, buyItem.Price, buyItem.Amount, buyItem.ItemID, buyItem.Hue));
				count++;

				if (opls == null)
				{
					opls = new List<ObjectPropertyList>();
				}

				if (disp is Item)
				{
					opls.Add(((Item)disp).PropertyList);
				}
				else if (disp is Mobile)
				{
					opls.Add(((Mobile)disp).PropertyList);
				}
			}

			List<Item> playerItems = cont.Items;

			for (int i = playerItems.Count - 1; i >= 0; --i)
			{
				if (i >= playerItems.Count)
					continue;

				Item item = playerItems[i];

				if ((item.LastMoved + InventoryDecayTime) <= DateTime.Now)
					item.Delete();
			}

			for (int i = 0; i < playerItems.Count; ++i)
			{
				Item item = playerItems[i];

				int price = 0;
				string name = null;

				foreach (IShopSellInfo ssi in sellInfo)
				{
					if (ssi.IsSellable(item))
					{
						price = ssi.GetBuyPriceFor(item);
						name = ssi.GetNameFor(item);
						break;
					}
				}

				if (name != null && list.Count < 250)
				{
					list.Add(new BuyItemState(name, cont.Serial, item.Serial, price, item.Amount, item.ItemID, item.Hue));
					count++;

					if (opls == null)
					{
						opls = new List<ObjectPropertyList>();
					}

					opls.Add(item.PropertyList);
				}
			}

			//one (not all) of the packets uses a byte to describe number of items in the list.  Osi = dumb.
			//if ( list.Count > 255 )
			//	Console.WriteLine( "Vendor Warning: Vendor {0} has more than 255 buy items, may cause client errors!", this );

			if (list.Count > 0)
			{
				list.Sort(new BuyItemStateComparer());

				this.SendPacksTo(from);

				NetState ns = from.NetState;

				if (ns == null)
					return;

				if (ns.ContainerGridLines)
					from.Send(new VendorBuyContent6017(list));
				else
					from.Send(new VendorBuyContent(list));

				from.Send(new VendorBuyList(this, list));

				if (ns.HighSeas)
					from.Send(new DisplayBuyListHS(this));
				else
					from.Send(new DisplayBuyList(this));

				from.Send(new MobileStatusExtended(from));//make sure their gold amount is sent

				if (opls != null)
				{
					for (int i = 0; i < opls.Count; ++i)
					{
						from.Send(opls[i]);
					}
				}

				this.SayTo(from, 500186); // Greetings.  Have a look around.
			}
		}

		public virtual void SendPacksTo(Mobile from)
		{
			Item pack = this.FindItemOnLayer(Layer.ShopBuy);

			if (pack == null)
			{
				pack = new Backpack();
				pack.Layer = Layer.ShopBuy;
				pack.Movable = false;
				pack.Visible = false;
				this.AddItem(pack);
			}

			from.Send(new EquipUpdate(pack));

			pack = this.FindItemOnLayer(Layer.ShopSell);

			if (pack != null)
				from.Send(new EquipUpdate(pack));

			pack = this.FindItemOnLayer(Layer.ShopResale);

			if (pack == null)
			{
				pack = new Backpack();
				pack.Layer = Layer.ShopResale;
				pack.Movable = false;
				pack.Visible = false;
				this.AddItem(pack);
			}

			from.Send(new EquipUpdate(pack));
		}

		public virtual void VendorSell(Mobile from)
		{
			if (!this.IsActiveBuyer)
				return;

			if (!from.CheckAlive())
				return;

			if (!this.CheckVendorAccess(from))
			{
				this.Say(501522); // I shall not treat with scum like thee!
				return;
			}

			Container pack = from.Backpack;

			if (pack != null)
			{
				IShopSellInfo[] info = this.GetSellInfo();

				Hashtable table = new Hashtable();

				foreach (IShopSellInfo ssi in info)
				{
					Item[] items = pack.FindItemsByType(ssi.Types);

					foreach (Item item in items)
					{
						if (item is Container && ((Container)item).Items.Count != 0)
							continue;

						if (item.IsStandardLoot() && item.Movable && ssi.IsSellable(item))
							table[item] = new SellItemState(item, ssi.GetSellPriceFor(item), ssi.GetNameFor(item));
					}
				}

				if (table.Count > 0)
				{
					this.SendPacksTo(from);

					from.Send(new VendorSellList(this, table));
				}
				else
				{
					this.Say(true, "You have nothing I would be interested in.");
				}
			}
		}

		public override bool OnDragDrop(Mobile from, Item dropped)
		{
			/* TODO: Thou art giving me? and fame/karma for gold gifts */
			if (dropped is SmallBOD || dropped is LargeBOD)
			{
				PlayerMobile pm = from as PlayerMobile;

				if (Core.ML && pm != null && pm.NextBODTurnInTime > DateTime.Now)
				{
					this.SayTo(from, 1079976); // You'll have to wait a few seconds while I inspect the last order.
					return false;
				}
				else if (!this.IsValidBulkOrder(dropped) || !this.SupportsBulkOrders(from))
				{
					this.SayTo(from, 1045130); // That order is for some other shopkeeper.
					return false;
				}
				else if ((dropped is SmallBOD && !((SmallBOD)dropped).Complete) || (dropped is LargeBOD && !((LargeBOD)dropped).Complete))
				{
					this.SayTo(from, 1045131); // You have not completed the order yet.
					return false;
				}

				Item reward;
				int gold, fame;

				if (dropped is SmallBOD)
					((SmallBOD)dropped).GetRewards(out reward, out gold, out fame);
				else
					((LargeBOD)dropped).GetRewards(out reward, out gold, out fame);

				from.SendSound(0x3D);

				this.SayTo(from, 1045132); // Thank you so much!  Here is a reward for your effort.

				if (reward != null)
					from.AddToBackpack(reward);

				if (gold > 1000)
					from.AddToBackpack(new BankCheck(gold));
				else if (gold > 0)
					from.AddToBackpack(new Gold(gold));
				//daat99 OWLTR start - give tokens for bods
				if (OWLTROptionsManager.IsEnabled(OWLTROptionsManager.OPTIONS_ENUM.BOD_GIVE_TOKENS) && gold > 100)
					TokenSystem.GiveTokensToPlayer(from as PlayerMobile, (int)(gold / 100));
				//daat99 OWLTR end - give tokens for bods

				Titles.AwardFame(from, fame, true);

				this.OnSuccessfulBulkOrderReceive(from);

				if (Core.ML && pm != null)
					pm.NextBODTurnInTime = DateTime.Now + TimeSpan.FromSeconds(10.0);

				dropped.Delete();
				return true;
			}

			return base.OnDragDrop(from, dropped);
		}

		private GenericBuyInfo LookupDisplayObject(object obj)
		{
			IBuyItemInfo[] buyInfo = this.GetBuyInfo();

			for (int i = 0; i < buyInfo.Length; ++i)
			{
				GenericBuyInfo gbi = (GenericBuyInfo)buyInfo[i];

				if (gbi.GetDisplayEntity() == obj)
					return gbi;
			}

			return null;
		}

		private void ProcessSinglePurchase(BuyItemResponse buy, IBuyItemInfo bii, List<BuyItemResponse> validBuy, ref int controlSlots, ref bool fullPurchase, ref int totalCost)
		{
			int amount = buy.Amount;

			if (amount > bii.Amount)
				amount = bii.Amount;

			if (amount <= 0)
				return;

			int slots = bii.ControlSlots * amount;

			if (controlSlots >= slots)
			{
				controlSlots -= slots;
			}
			else
			{
				fullPurchase = false;
				return;
			}

			totalCost += bii.Price * amount;
			validBuy.Add(buy);
		}

		private void ProcessValidPurchase(int amount, IBuyItemInfo bii, Mobile buyer, Container cont)
		{
			if (amount > bii.Amount)
				amount = bii.Amount;

			if (amount < 1)
				return;

			bii.Amount -= amount;

			IEntity o = bii.GetEntity();

			if (o is Item)
			{
				Item item = (Item)o;

				if (item.Stackable)
				{
					item.Amount = amount;

					if (cont == null || !cont.TryDropItem(buyer, item, false))
						item.MoveToWorld(buyer.Location, buyer.Map);
				}
				else
				{
					item.Amount = 1;

					if (cont == null || !cont.TryDropItem(buyer, item, false))
						item.MoveToWorld(buyer.Location, buyer.Map);

					for (int i = 1; i < amount; i++)
					{
						item = bii.GetEntity() as Item;

						if (item != null)
						{
							item.Amount = 1;

							if (cont == null || !cont.TryDropItem(buyer, item, false))
								item.MoveToWorld(buyer.Location, buyer.Map);
						}
					}
				}
			}
			else if (o is Mobile)
			{
				Mobile m = (Mobile)o;

				m.Direction = (Direction)Utility.Random(8);
				m.MoveToWorld(buyer.Location, buyer.Map);
				m.PlaySound(m.GetIdleSound());

				if (m is BaseCreature)
					((BaseCreature)m).SetControlMaster(buyer);

				for (int i = 1; i < amount; ++i)
				{
					m = bii.GetEntity() as Mobile;

					if (m != null)
					{
						m.Direction = (Direction)Utility.Random(8);
						m.MoveToWorld(buyer.Location, buyer.Map);

						if (m is BaseCreature)
							((BaseCreature)m).SetControlMaster(buyer);
					}
				}
			}
		}

		public virtual bool OnBuyItems(Mobile buyer, List<BuyItemResponse> list)
		{
			if (!this.IsActiveSeller)
				return false;

			if (!buyer.CheckAlive())
				return false;

			if (!this.CheckVendorAccess(buyer))
			{
				this.Say(501522); // I shall not treat with scum like thee!
				return false;
			}

			this.UpdateBuyInfo();

			IBuyItemInfo[] buyInfo = this.GetBuyInfo();
			IShopSellInfo[] info = this.GetSellInfo();
			int totalCost = 0;
			List<BuyItemResponse> validBuy = new List<BuyItemResponse>(list.Count);
			Container cont;
			bool bought = false;
			bool fromBank = false;
			bool fullPurchase = true;
			int controlSlots = buyer.FollowersMax - buyer.Followers;

			foreach (BuyItemResponse buy in list)
			{
				Serial ser = buy.Serial;
				int amount = buy.Amount;

				if (ser.IsItem)
				{
					Item item = World.FindItem(ser);

					if (item == null)
						continue;

					GenericBuyInfo gbi = this.LookupDisplayObject(item);

					if (gbi != null)
					{
						this.ProcessSinglePurchase(buy, gbi, validBuy, ref controlSlots, ref fullPurchase, ref totalCost);
					}
					else if (item != this.BuyPack && item.IsChildOf(this.BuyPack))
					{
						if (amount > item.Amount)
							amount = item.Amount;

						if (amount <= 0)
							continue;

						foreach (IShopSellInfo ssi in info)
						{
							if (ssi.IsSellable(item))
							{
								if (ssi.IsResellable(item))
								{
									totalCost += ssi.GetBuyPriceFor(item) * amount;
									validBuy.Add(buy);
									break;
								}
							}
						}
					}
				}
				else if (ser.IsMobile)
				{
					Mobile mob = World.FindMobile(ser);

					if (mob == null)
						continue;

					GenericBuyInfo gbi = this.LookupDisplayObject(mob);

					if (gbi != null)
						this.ProcessSinglePurchase(buy, gbi, validBuy, ref controlSlots, ref fullPurchase, ref totalCost);
				}
			}//foreach

			if (fullPurchase && validBuy.Count == 0)
				this.SayTo(buyer, 500190); // Thou hast bought nothing!
			else if (validBuy.Count == 0)
				this.SayTo(buyer, 500187); // Your order cannot be fulfilled, please try again.

			if (validBuy.Count == 0)
				return false;

			bought = (buyer.AccessLevel >= AccessLevel.GameMaster);

			cont = buyer.Backpack;
			if (!bought && cont != null)
			{
				if (cont.ConsumeTotal(typeof(Gold), totalCost))
					bought = true;
				else if (totalCost < 2000)
					this.SayTo(buyer, 500192);//Begging thy pardon, but thou casnt afford that.
			}

			if (!bought && totalCost >= 2000)
			{
				cont = buyer.FindBankNoCreate();
				if (cont != null && cont.ConsumeTotal(typeof(Gold), totalCost))
				{
					bought = true;
					fromBank = true;
				}
				else
				{
					this.SayTo(buyer, 500191); //Begging thy pardon, but thy bank account lacks these funds.
				}
			}

			if (!bought)
				return false;
			else
				buyer.PlaySound(0x32);

			cont = buyer.Backpack;
			if (cont == null)
				cont = buyer.BankBox;

			foreach (BuyItemResponse buy in validBuy)
			{
				Serial ser = buy.Serial;
				int amount = buy.Amount;

				if (amount < 1)
					continue;

				if (ser.IsItem)
				{
					Item item = World.FindItem(ser);

					if (item == null)
						continue;

					GenericBuyInfo gbi = this.LookupDisplayObject(item);

					if (gbi != null)
					{
						this.ProcessValidPurchase(amount, gbi, buyer, cont);
					}
					else
					{
						if (amount > item.Amount)
							amount = item.Amount;

						foreach (IShopSellInfo ssi in info)
						{
							if (ssi.IsSellable(item))
							{
								if (ssi.IsResellable(item))
								{
									Item buyItem;
									if (amount >= item.Amount)
									{
										buyItem = item;
									}
									else
									{
										buyItem = Mobile.LiftItemDupe(item, item.Amount - amount);

										if (buyItem == null)
											buyItem = item;
									}

									if (cont == null || !cont.TryDropItem(buyer, buyItem, false))
										buyItem.MoveToWorld(buyer.Location, buyer.Map);

									break;
								}
							}
						}
					}
				}
				else if (ser.IsMobile)
				{
					Mobile mob = World.FindMobile(ser);

					if (mob == null)
						continue;

					GenericBuyInfo gbi = this.LookupDisplayObject(mob);

					if (gbi != null)
						this.ProcessValidPurchase(amount, gbi, buyer, cont);
				}
			}//foreach

			if (fullPurchase)
			{
				if (buyer.AccessLevel >= AccessLevel.GameMaster)
					this.SayTo(buyer, true, "I would not presume to charge thee anything.  Here are the goods you requested.");
				else if (fromBank)
					this.SayTo(buyer, true, "The total of thy purchase is {0} gold, which has been withdrawn from your bank account.  My thanks for the patronage.", totalCost);
				else
					this.SayTo(buyer, true, "The total of thy purchase is {0} gold.  My thanks for the patronage.", totalCost);
			}
			else
			{
				if (buyer.AccessLevel >= AccessLevel.GameMaster)
					this.SayTo(buyer, true, "I would not presume to charge thee anything.  Unfortunately, I could not sell you all the goods you requested.");
				else if (fromBank)
					this.SayTo(buyer, true, "The total of thy purchase is {0} gold, which has been withdrawn from your bank account.  My thanks for the patronage.  Unfortunately, I could not sell you all the goods you requested.", totalCost);
				else
					this.SayTo(buyer, true, "The total of thy purchase is {0} gold.  My thanks for the patronage.  Unfortunately, I could not sell you all the goods you requested.", totalCost);
			}

			return true;
		}

		public virtual bool CheckVendorAccess(Mobile from)
		{
			GuardedRegion reg = (GuardedRegion)this.Region.GetRegion(typeof(GuardedRegion));

			if (reg != null && !reg.CheckVendorAccess(this, from))
				return false;

			if (this.Region != from.Region)
			{
				reg = (GuardedRegion)from.Region.GetRegion(typeof(GuardedRegion));

				if (reg != null && !reg.CheckVendorAccess(this, from))
					return false;
			}

			return true;
		}

		public virtual bool OnSellItems(Mobile seller, List<SellItemResponse> list)
		{
			if (!this.IsActiveBuyer)
				return false;

			if (!seller.CheckAlive())
				return false;

			if (!this.CheckVendorAccess(seller))
			{
				this.Say(501522); // I shall not treat with scum like thee!
				return false;
			}

			seller.PlaySound(0x32);

			IShopSellInfo[] info = this.GetSellInfo();
			IBuyItemInfo[] buyInfo = this.GetBuyInfo();
			int GiveGold = 0;
			int Sold = 0;
			Container cont;

			foreach (SellItemResponse resp in list)
			{
				if (resp.Item.RootParent != seller || resp.Amount <= 0 || !resp.Item.IsStandardLoot() || !resp.Item.Movable || (resp.Item is Container && ((Container)resp.Item).Items.Count != 0))
					continue;

				foreach (IShopSellInfo ssi in info)
				{
					if (ssi.IsSellable(resp.Item))
					{
						Sold++;
						break;
					}
				}
			}

			if (Sold > MaxSell)
			{
				this.SayTo(seller, true, "You may only sell {0} items at a time!", MaxSell);
				return false;
			}
			else if (Sold == 0)
			{
				return true;
			}

			foreach (SellItemResponse resp in list)
			{
				if (resp.Item.RootParent != seller || resp.Amount <= 0 || !resp.Item.IsStandardLoot() || !resp.Item.Movable || (resp.Item is Container && ((Container)resp.Item).Items.Count != 0))
					continue;

				foreach (IShopSellInfo ssi in info)
				{
					if (ssi.IsSellable(resp.Item))
					{
						int amount = resp.Amount;

						if (amount > resp.Item.Amount)
							amount = resp.Item.Amount;

						if (ssi.IsResellable(resp.Item))
						{
							bool found = false;

							foreach (IBuyItemInfo bii in buyInfo)
							{
								if (bii.Restock(resp.Item, amount))
								{
									resp.Item.Consume(amount);
									found = true;

									break;
								}
							}

							if (!found)
							{
								cont = this.BuyPack;

								if (amount < resp.Item.Amount)
								{
									Item item = Mobile.LiftItemDupe(resp.Item, resp.Item.Amount - amount);

									if (item != null)
									{
										item.SetLastMoved();
										cont.DropItem(item);
									}
									else
									{
										resp.Item.SetLastMoved();
										cont.DropItem(resp.Item);
									}
								}
								else
								{
									resp.Item.SetLastMoved();
									cont.DropItem(resp.Item);
								}
							}
						}
						else
						{
							if (amount < resp.Item.Amount)
								resp.Item.Amount -= amount;
							else
								resp.Item.Delete();
						}

						GiveGold += ssi.GetSellPriceFor(resp.Item) * amount;
						break;
					}
				}
			}

			if (GiveGold > 0)
			{
				while (GiveGold > 60000)
				{
					seller.AddToBackpack(new Gold(60000));
					GiveGold -= 60000;
				}

				seller.AddToBackpack(new Gold(GiveGold));

				seller.PlaySound(0x0037);//Gold dropping sound

				if (this.SupportsBulkOrders(seller))
				{
					Item bulkOrder = this.CreateBulkOrder(seller, false);

					if (bulkOrder is LargeBOD)
						seller.SendGump(new LargeBODAcceptGump(seller, (LargeBOD)bulkOrder));
					else if (bulkOrder is SmallBOD)
						seller.SendGump(new SmallBODAcceptGump(seller, (SmallBOD)bulkOrder));
				}
			}
			//no cliloc for this?
			//SayTo( seller, true, "Thank you! I bought {0} item{1}. Here is your {2}gp.", Sold, (Sold > 1 ? "s" : ""), GiveGold );

			return true;
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)1); // version

			List<SBInfo> sbInfos = this.SBInfos;

			for (int i = 0; sbInfos != null && i < sbInfos.Count; ++i)
			{
				SBInfo sbInfo = sbInfos[i];
				List<GenericBuyInfo> buyInfo = sbInfo.BuyInfo;

				for (int j = 0; buyInfo != null && j < buyInfo.Count; ++j)
				{
					GenericBuyInfo gbi = (GenericBuyInfo)buyInfo[j];

					int maxAmount = gbi.MaxAmount;
					int doubled = 0;

					switch ( maxAmount )
					{
						case 40:
							doubled = 1;
							break;
						case 80:
							doubled = 2;
							break;
						case 160:
							doubled = 3;
							break;
						case 320:
							doubled = 4;
							break;
						case 640:
							doubled = 5;
							break;
						case 999:
							doubled = 6;
							break;
					}

					if (doubled > 0)
					{
						writer.WriteEncodedInt(1 + ((j * sbInfos.Count) + i));
						writer.WriteEncodedInt(doubled);
					}
				}
			}

			writer.WriteEncodedInt(0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			this.LoadSBInfo();

			List<SBInfo> sbInfos = this.SBInfos;

			switch ( version )
			{
				case 1:
					{
						int index;

						while ((index = reader.ReadEncodedInt()) > 0)
						{
							int doubled = reader.ReadEncodedInt();

							if (sbInfos != null)
							{
								index -= 1;
								int sbInfoIndex = index % sbInfos.Count;
								int buyInfoIndex = index / sbInfos.Count;

								if (sbInfoIndex >= 0 && sbInfoIndex < sbInfos.Count)
								{
									SBInfo sbInfo = sbInfos[sbInfoIndex];
									List<GenericBuyInfo> buyInfo = sbInfo.BuyInfo;

									if (buyInfo != null && buyInfoIndex >= 0 && buyInfoIndex < buyInfo.Count)
									{
										GenericBuyInfo gbi = (GenericBuyInfo)buyInfo[buyInfoIndex];

										int amount = 20;

										switch ( doubled )
										{
											case 1:
												amount = 40;
												break;
											case 2:
												amount = 80;
												break;
											case 3:
												amount = 160;
												break;
											case 4:
												amount = 320;
												break;
											case 5:
												amount = 640;
												break;
											case 6:
												amount = 999;
												break;
										}

										gbi.Amount = gbi.MaxAmount = amount;
									}
								}
							}
						}

						break;
					}
			}

			if (this.IsParagon)
				this.IsParagon = false;

			if (Core.AOS && this.NameHue == 0x35)
				this.NameHue = -1;

			Timer.DelayCall(TimeSpan.Zero, new TimerCallback(CheckMorph));
		}

		public override void AddCustomContextEntries(Mobile from, List<ContextMenuEntry> list)
		{
			if (from.Alive && this.IsActiveVendor)
			{
				if (this.SupportsBulkOrders(from))
					list.Add(new BulkOrderInfoEntry(from, this));
				
				if (this.IsActiveSeller)
					list.Add(new VendorBuyEntry(from, this));

				if (this.IsActiveBuyer)
					list.Add(new VendorSellEntry(from, this));
			}

			base.AddCustomContextEntries(from, list);
		}

		public virtual IShopSellInfo[] GetSellInfo()
		{
			return (IShopSellInfo[])this.m_ArmorSellInfo.ToArray(typeof(IShopSellInfo));
		}

		public virtual IBuyItemInfo[] GetBuyInfo()
		{
			return (IBuyItemInfo[])this.m_ArmorBuyInfo.ToArray(typeof(IBuyItemInfo));
		}

		public override bool CanBeDamaged()
		{
			return !this.IsInvulnerable;
		}
	}
}

namespace Server.ContextMenus
{
	public class VendorBuyEntry : ContextMenuEntry
	{
		private readonly BaseVendor m_Vendor;

		public VendorBuyEntry(Mobile from, BaseVendor vendor)
			: base(6103, 8)
		{
			this.m_Vendor = vendor;
			this.Enabled = vendor.CheckVendorAccess(from);
		}

		public override void OnClick()
		{
			this.m_Vendor.VendorBuy(this.Owner.From);
		}
	}

	public class VendorSellEntry : ContextMenuEntry
	{
		private readonly BaseVendor m_Vendor;

		public VendorSellEntry(Mobile from, BaseVendor vendor)
			: base(6104, 8)
		{
			this.m_Vendor = vendor;
			this.Enabled = vendor.CheckVendorAccess(from);
		}

		public override void OnClick()
		{
			this.m_Vendor.VendorSell(this.Owner.From);
		}
	}
}

namespace Server
{
	public interface IShopSellInfo
	{
		//get display name for an item
		string GetNameFor(Item item);

		//get price for an item which the player is selling
		int GetSellPriceFor(Item item);

		//get price for an item which the player is buying
		int GetBuyPriceFor(Item item);

		//can we sell this item to this vendor?
		bool IsSellable(Item item);

		//What do we sell?
		Type[] Types { get; }

		//does the vendor resell this item?
		bool IsResellable(Item item);
	}

	public interface IBuyItemInfo
	{
		//get a new instance of an object (we just bought it)
		IEntity GetEntity();

		int ControlSlots { get; }

		int PriceScalar { get; set; }

		//display price of the item
		int Price { get; }

		//display name of the item
		string Name { get; }

		//display hue
		int Hue { get; }

		//display id
		int ItemID { get; }

		//amount in stock
		int Amount { get; set; }

		//max amount in stock
		int MaxAmount { get; }

		//Attempt to restock with item, (return true if restock sucessful)
		bool Restock(Item item, int amount);

		//called when its time for the whole shop to restock
		void OnRestock();
	}
}