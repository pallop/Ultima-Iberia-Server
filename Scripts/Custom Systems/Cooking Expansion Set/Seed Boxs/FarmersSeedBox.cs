using System;
using System.Collections;
using Server;
using Server.Prompts;
using Server.Mobiles;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Items.Crops;
using Server.Network;
using Server.Targeting;
using Server.Multis;
using Server.Regions;


namespace Server.Items
{
    public class FarmersSeedBox : Item
    {
        private int m_CottonSeed;
    	private int m_FlaxSeed;
    	private int m_HaySeed;
    	private int m_OatsSeed;
		private int m_RiceSeed;
        private int m_SugarcaneSeed;
        private int m_WheatSeed;
        private int m_BitterHopsSeed;
        private int m_ElvenHopsSeed;
        private int m_SnowHopsSeed;
        private int m_SweetHopsSeed;

        private int m_StorageLimit;
        private int m_WithdrawIncrement;

        [CommandProperty(AccessLevel.GameMaster)]
        public int StorageLimit { get { return m_StorageLimit; } set { m_StorageLimit = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int WithdrawIncrement { get { return m_WithdrawIncrement; } set { m_WithdrawIncrement = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int CottonSeed { get { return m_CottonSeed; } set { m_CottonSeed = value; InvalidateProperties(); } }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public int FlaxSeed { get { return m_FlaxSeed; } set { m_FlaxSeed = value; InvalidateProperties(); } }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public int HaySeed { get { return m_HaySeed; } set { m_HaySeed = value; InvalidateProperties(); } }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public int OatsSeed { get { return m_OatsSeed; } set { m_OatsSeed = value; InvalidateProperties(); } }
        
		[CommandProperty(AccessLevel.GameMaster)]
		public int RiceSeed { get { return m_RiceSeed; } set { m_RiceSeed = value; InvalidateProperties(); } }
        
		[CommandProperty(AccessLevel.GameMaster)]
		public int SugarcaneSeed { get { return m_SugarcaneSeed; } set { m_SugarcaneSeed = value; InvalidateProperties(); } }
        
		[CommandProperty(AccessLevel.GameMaster)]
		public int WheatSeed { get { return m_WheatSeed; } set { m_WheatSeed = value; InvalidateProperties(); } }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public int BitterHopsSeed { get { return m_BitterHopsSeed; } set { m_BitterHopsSeed = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ElvenHopsSeed { get { return m_ElvenHopsSeed; } set { m_ElvenHopsSeed = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SnowHopsSeed { get { return m_SnowHopsSeed; } set { m_SnowHopsSeed = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SweetHopsSeed { get { return m_SweetHopsSeed; } set { m_SweetHopsSeed = value; InvalidateProperties(); } }

        [Constructable]
        public FarmersSeedBox() : base( 0x9AA )
        {
            Movable = true;
            Weight = 1.0;
            Hue = 88;
            Name = "Farmers Seed Box";
            //LootType = LootType.Blessed;
            StorageLimit = 60000;
            WithdrawIncrement = 100;
        }

        [Constructable]
        public FarmersSeedBox(int storageLimit, int withdrawIncrement) : base( 0x9AA )
        {
            Movable = true;
            Weight = 1.0;
            Hue = 88;
            Name = "Farmers Seed Box";
            //LootType = LootType.Blessed;
            StorageLimit = storageLimit;
            WithdrawIncrement = withdrawIncrement;
        }

        public override void OnDoubleClick(Mobile from)
        {
			if (!(from is PlayerMobile))
				return;
			if (IsChildOf(from.Backpack))
                from.SendGump(new FarmersSeedBoxGump((PlayerMobile)from, this));
			else
				from.SendMessage("This must be in your backpack.");
        }
        public void BeginCombine(Mobile from)
        {
            from.Target = new FarmersSeedBoxTarget(this);
        }
        public void EndCombine(Mobile from, object o)
        {
			if (o is Item && (((Item)o).IsChildOf(from.Backpack) || ((Item)o).IsChildOf(from.BankBox)))
            {
                Item curItem = o as Item;
				if (curItem is CottonSeed)
				{
					if (CottonSeed + curItem.Amount > StorageLimit)
						from.SendMessage("You are trying to add "+ ( (CottonSeed + curItem.Amount) - m_StorageLimit ) +" too much! The warehouse can store only "+ m_StorageLimit +" of this resource.");
					else
					{
						CottonSeed += curItem.Amount;
						curItem.Delete();
						from.SendGump(new FarmersSeedBoxGump((PlayerMobile)from, this));
						BeginCombine(from);
					}
				}
				
				else if (curItem is FlaxSeed)
				{
					if (FlaxSeed + curItem.Amount > StorageLimit)
						from.SendMessage("You are trying to add "+ ( (FlaxSeed + curItem.Amount) - m_StorageLimit ) +" too much! The warehouse can store only "+ m_StorageLimit +" of this resource.");
					else
					{
						FlaxSeed += curItem.Amount;
						curItem.Delete();
						from.SendGump(new FarmersSeedBoxGump((PlayerMobile)from, this));
						BeginCombine(from);
					}
				}
				
				else if (curItem is HaySeed)
				{
					if (HaySeed + curItem.Amount > StorageLimit)
						from.SendMessage("You are trying to add "+ ( (HaySeed + curItem.Amount) - m_StorageLimit ) +" too much! The warehouse can store only "+ m_StorageLimit +" of this resource.");
					else
					{
						HaySeed += curItem.Amount;
						curItem.Delete();
						from.SendGump(new FarmersSeedBoxGump((PlayerMobile)from, this));
						BeginCombine(from);
					}
				}
				else if (curItem is OatsSeed)
				{
					if (OatsSeed + curItem.Amount > StorageLimit)
						from.SendMessage("You are trying to add "+ ( (OatsSeed + curItem.Amount) - m_StorageLimit ) +" too much! The warehouse can store only "+ m_StorageLimit +" of this resource.");
					else
					{
						OatsSeed += curItem.Amount;
						curItem.Delete();
						from.SendGump(new FarmersSeedBoxGump((PlayerMobile)from, this));
						BeginCombine(from);
					}
				}
				
				else if (curItem is RiceSeed)
				{
					if (RiceSeed + curItem.Amount > StorageLimit)
						from.SendMessage("You are trying to add "+ ( (RiceSeed + curItem.Amount) - m_StorageLimit ) +" too much! The warehouse can store only "+ m_StorageLimit +" of this resource.");
					else
					{
						RiceSeed += curItem.Amount;
						curItem.Delete();
						from.SendGump(new FarmersSeedBoxGump((PlayerMobile)from, this));
						BeginCombine(from);
					}
				}
				else if (curItem is SugarcaneSeed)
				{
					if (SugarcaneSeed + curItem.Amount > StorageLimit)
						from.SendMessage("You are trying to add "+ ( (SugarcaneSeed + curItem.Amount) - m_StorageLimit ) +" too much! The warehouse can store only "+ m_StorageLimit +" of this resource.");
					else
					{
						SugarcaneSeed += curItem.Amount;
						curItem.Delete();
						from.SendGump(new FarmersSeedBoxGump((PlayerMobile)from, this));
						BeginCombine(from);
					}
				}
				else if (curItem is WheatSeed)
				{
					if (WheatSeed + curItem.Amount > StorageLimit)
						from.SendMessage("You are trying to add "+ ( (WheatSeed + curItem.Amount) - m_StorageLimit ) +" too much! The warehouse can store only "+ m_StorageLimit +" of this resource.");
					else
					{
						WheatSeed += curItem.Amount;
						curItem.Delete();
						from.SendGump(new FarmersSeedBoxGump((PlayerMobile)from, this));
						BeginCombine(from);
					}
				}
				else if (curItem is BitterHopsSeed)
                {
                    if (BitterHopsSeed + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add "+ ( (BitterHopsSeed + curItem.Amount) - m_StorageLimit ) +" too much! The warehouse can store only "+ m_StorageLimit +" of this resource.");
                    else
                    {
                        BitterHopsSeed += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new FarmersSeedBoxGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is ElvenHopsSeed)
                {
                    if (ElvenHopsSeed + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add "+ ( (ElvenHopsSeed + curItem.Amount) - m_StorageLimit ) +" too much! The warehouse can store only "+ m_StorageLimit +" of this resource.");
                    else
                    {
                        ElvenHopsSeed += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new FarmersSeedBoxGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is SnowHopsSeed)
                {
                    if (SnowHopsSeed + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add "+ ( (SnowHopsSeed + curItem.Amount) - m_StorageLimit ) +" too much! The warehouse can store only "+ m_StorageLimit +" of this resource.");
                    else
                    {
                        SnowHopsSeed += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new FarmersSeedBoxGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is SweetHopsSeed)
                {
                    if (SweetHopsSeed + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add "+ ( (SweetHopsSeed + curItem.Amount) - m_StorageLimit ) +" too much! The warehouse can store only "+ m_StorageLimit +" of this resource.");
                    else
                    {
                        SweetHopsSeed += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new FarmersSeedBoxGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
            }
            else
            {
                from.SendLocalizedMessage(1045158); // You must have the item in your backpack to target it.
            }
        }
        public FarmersSeedBox(Serial serial) : base( serial )
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
			writer.Write( (int) 0 ); // version

            writer.Write((int)m_CottonSeed);
        	writer.Write((int)m_FlaxSeed);
        	writer.Write((int)m_HaySeed);
        	writer.Write((int)m_OatsSeed);
			writer.Write((int)m_RiceSeed);
			writer.Write((int)m_SugarcaneSeed);
			writer.Write((int)m_WheatSeed);
			writer.Write((int)m_BitterHopsSeed);
            writer.Write((int)m_ElvenHopsSeed);
            writer.Write((int)m_SnowHopsSeed);
            writer.Write((int)m_SweetHopsSeed);
            writer.Write((int)m_StorageLimit);
            writer.Write((int)m_WithdrawIncrement);
        }
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
					goto case 0;
				case 0:
				{
                    m_CottonSeed = reader.ReadInt();
					m_FlaxSeed = reader.ReadInt();
					m_HaySeed = reader.ReadInt();
					m_OatsSeed = reader.ReadInt();
					m_RiceSeed = reader.ReadInt();
					m_SugarcaneSeed = reader.ReadInt();
					m_WheatSeed = reader.ReadInt();
					m_BitterHopsSeed = reader.ReadInt();
					m_ElvenHopsSeed = reader.ReadInt();
					m_SnowHopsSeed = reader.ReadInt();
					m_SweetHopsSeed = reader.ReadInt();
					m_StorageLimit = reader.ReadInt();
					m_WithdrawIncrement = reader.ReadInt();
					break;
				}
			}
		}
    }
}

namespace Server.Items
{
    public class FarmersSeedBoxGump : Gump
    {
        private PlayerMobile m_From;
        private FarmersSeedBox m_Key;

        public FarmersSeedBoxGump(PlayerMobile from, FarmersSeedBox key)
            : base(25, 25)
        {
            m_From = from;
            m_Key = key;

            m_From.CloseGump(typeof(FarmersSeedBoxGump));

            AddPage(0);
			
			AddBackground(50, 10, 455, 230, 5054);
			AddImageTiled(58, 20, 438, 210, 2624);
			AddAlphaRegion(58, 20, 438, 210);

            AddLabel(200, 25, 88, "Farmers Seed Box");

            AddLabel(125, 50, 0x486, "Cotton Seed");
            AddLabel(225, 50, 0x480, key.CottonSeed.ToString());
            AddButton(75, 50, 4005, 4007, 1, GumpButtonType.Reply, 0);

            AddLabel(125, 75, 0x486, "Flax Seed");
			AddLabel(225, 75, 0x480, key.FlaxSeed.ToString());
			AddButton(75, 75, 4005, 4007, 2, GumpButtonType.Reply, 0);

            AddLabel(125, 100, 0x486, "Hay Seed");
			AddLabel(225, 100, 0x480, key.HaySeed.ToString());
			AddButton(75, 100, 4005, 4007, 3, GumpButtonType.Reply, 0);

            AddLabel(125, 125, 0x486, "Oats Seed");
			AddLabel(225, 125, 0x480, key.OatsSeed.ToString());
			AddButton(75, 125, 4005, 4007, 4, GumpButtonType.Reply, 0);

            AddLabel(125, 150, 0x486, "Rice Seed");
            AddLabel(225, 150, 0x480, key.RiceSeed.ToString());
            AddButton(75, 150, 4005, 4007, 5, GumpButtonType.Reply, 0);

            AddLabel(125, 175, 0x486, "Sugarcane Seed");
			AddLabel(225, 175, 0x480, key.SugarcaneSeed.ToString());
			AddButton(75, 175, 4005, 4007, 6, GumpButtonType.Reply, 0);

            AddLabel(125, 200, 0x486, "Wheat Seed");
			AddLabel(225, 200, 0x480, key.WheatSeed.ToString());
			AddButton(75, 200, 4005, 4007, 7, GumpButtonType.Reply, 0);
			
	        AddLabel(325, 50, 0x486, "Bitter Hops Seed");
            AddLabel(425, 50, 0x480, key.BitterHopsSeed.ToString());
            AddButton(275, 50, 4005, 4007, 8, GumpButtonType.Reply, 0);

	        AddLabel(325, 75, 0x486, "Elven Hops Seed");
            AddLabel(425, 75, 0x480, key.ElvenHopsSeed.ToString());
            AddButton(275, 75, 4005, 4007, 9, GumpButtonType.Reply, 0);
        	
            AddLabel(325, 100, 0x486, "Snow Hops Seed");
            AddLabel(425, 100, 0x480, key.SnowHopsSeed.ToString());
            AddButton(275, 100, 4005, 4007, 10, GumpButtonType.Reply, 0);
        	
	        AddLabel(325, 125, 0x486, "Sweet Hops Seed"); 
            AddLabel(425, 125, 0x480, key.SweetHopsSeed.ToString());
            AddButton(275, 125, 4005, 4007, 11, GumpButtonType.Reply, 0);
        				
			AddLabel(325, 175, 88, "Each Max:" );
			AddLabel(425, 175, 0x480, key.StorageLimit.ToString() );	

			AddLabel(325, 200, 88, "Add resource");
			AddButton(275, 200, 4005, 4007, 12, GumpButtonType.Reply, 0);
        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (m_Key.Deleted)
                return;
			else if (info.ButtonID == 1)
			{
				if (m_Key.CottonSeed >= m_Key.WithdrawIncrement)
				{
					m_From.AddToBackpack(new CottonSeed(m_Key.WithdrawIncrement));
					m_Key.CottonSeed = m_Key.CottonSeed - m_Key.WithdrawIncrement;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
				}
				else if (m_Key.CottonSeed > 0)
				{
					m_From.AddToBackpack(new CottonSeed(m_Key.CottonSeed));
					m_Key.CottonSeed = 0;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
				}
				else
				{
					m_From.SendMessage("You do not have any of that Cotton Seed!");
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
					m_Key.BeginCombine(m_From);
				}
			}            
			
			else if (info.ButtonID == 2)
			{
				if (m_Key.FlaxSeed >= m_Key.WithdrawIncrement)
				{
					m_From.AddToBackpack(new FlaxSeed(m_Key.WithdrawIncrement));
					m_Key.FlaxSeed = m_Key.FlaxSeed - m_Key.WithdrawIncrement;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
				}
				else if (m_Key.FlaxSeed > 0)
				{
					m_From.AddToBackpack(new FlaxSeed(m_Key.FlaxSeed));
					m_Key.FlaxSeed = 0;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
				}
				else
				{
					m_From.SendMessage("You do not have any of that Flax Seed!");
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
					m_Key.BeginCombine(m_From);
				}
			}
			
			else if (info.ButtonID == 3)
			{
				if (m_Key.HaySeed >= m_Key.WithdrawIncrement)
				{
					m_From.AddToBackpack(new HaySeed(m_Key.WithdrawIncrement));
					m_Key.HaySeed = m_Key.HaySeed - m_Key.WithdrawIncrement;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
				}
				else if (m_Key.HaySeed > 0)
				{
					m_From.AddToBackpack(new HaySeed(m_Key.HaySeed));
					m_Key.HaySeed = 0;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
				}
				else
				{
					m_From.SendMessage("You do not have any of that Hay Seed!");
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
					m_Key.BeginCombine(m_From);
				}
			}
			else if (info.ButtonID == 4)
			{
				if (m_Key.OatsSeed >= m_Key.WithdrawIncrement)
				{
					m_From.AddToBackpack(new OatsSeed(m_Key.WithdrawIncrement));
					m_Key.OatsSeed = m_Key.OatsSeed - m_Key.WithdrawIncrement;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
				}
				else if (m_Key.OatsSeed > 0)
				{
					m_From.AddToBackpack(new OatsSeed(m_Key.OatsSeed));
					m_Key.OatsSeed = 0;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
				}
				else
				{
					m_From.SendMessage("You do not have any of that Oats Seed!");
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
					m_Key.BeginCombine(m_From);
				}
			}
			
			else if (info.ButtonID == 5)
			{
				if (m_Key.RiceSeed >= m_Key.WithdrawIncrement)
				{
					m_From.AddToBackpack(new RiceSeed(m_Key.WithdrawIncrement));
					m_Key.RiceSeed = m_Key.RiceSeed - m_Key.WithdrawIncrement;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
				}
				else if (m_Key.RiceSeed > 0)
				{
					m_From.AddToBackpack(new RiceSeed(m_Key.RiceSeed));
					m_Key.RiceSeed = 0;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
				}
				else
				{
					m_From.SendMessage("You do not have any of that Rice Seed!");
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
					m_Key.BeginCombine(m_From);
				}
			}
			else if (info.ButtonID == 6)
			{
				if (m_Key.SugarcaneSeed >= m_Key.WithdrawIncrement)
				{
					m_From.AddToBackpack(new SugarcaneSeed(m_Key.WithdrawIncrement));
					m_Key.SugarcaneSeed = m_Key.SugarcaneSeed - m_Key.WithdrawIncrement;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
				}
				else if (m_Key.SugarcaneSeed > 0)
				{
					m_From.AddToBackpack(new SugarcaneSeed(m_Key.SugarcaneSeed));
					m_Key.SugarcaneSeed = 0;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
				}
				else
				{
					m_From.SendMessage("You do not have any of that Sugarcane Seed!");
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
					m_Key.BeginCombine(m_From);
				}
			}
			else if (info.ButtonID == 7)
			{
				if (m_Key.WheatSeed >= m_Key.WithdrawIncrement)
				{
					m_From.AddToBackpack(new WheatSeed(m_Key.WithdrawIncrement));
					m_Key.WheatSeed = m_Key.WheatSeed - m_Key.WithdrawIncrement;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
				}
				else if (m_Key.WheatSeed > 0)
				{
					m_From.AddToBackpack(new WheatSeed(m_Key.WheatSeed));
					m_Key.WheatSeed = 0;
					m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
				}
				else
				{
					m_From.SendMessage("You do not have any of that Wheat Seed!");
					m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
					m_Key.BeginCombine(m_From);
				}
			}			
			else if (info.ButtonID == 8)
            {
                if (m_Key.BitterHopsSeed >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new BitterHopsSeed(m_Key.WithdrawIncrement));
                    m_Key.BitterHopsSeed = m_Key.BitterHopsSeed - m_Key.WithdrawIncrement;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
                }
                else if (m_Key.BitterHopsSeed > 0)
                {
                    m_From.AddToBackpack(new BitterHopsSeed(m_Key.BitterHopsSeed));
                    m_Key.BitterHopsSeed = 0;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any Bitter Hop Seeds stored!");
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 9)
            {
                if (m_Key.ElvenHopsSeed > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new ElvenHopsSeed(m_Key.WithdrawIncrement));
                    m_Key.ElvenHopsSeed = m_Key.ElvenHopsSeed - m_Key.WithdrawIncrement;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
                }
                else if (m_Key.ElvenHopsSeed > 0)
                {
                    m_From.AddToBackpack(new ElvenHopsSeed(m_Key.ElvenHopsSeed));
                    m_Key.ElvenHopsSeed = 0;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any Elven Hop Seeds stored!");
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 10)
            {
                if (m_Key.SnowHopsSeed > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new SnowHopsSeed(m_Key.WithdrawIncrement));
                    m_Key.SnowHopsSeed = m_Key.SnowHopsSeed - m_Key.WithdrawIncrement;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
                }
                else if (m_Key.SnowHopsSeed > 0)
                {
                    m_From.AddToBackpack(new SnowHopsSeed(m_Key.SnowHopsSeed));
                    m_Key.SnowHopsSeed = 0;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any Snow Hop Seeds stored!");
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 11)
            {
                if (m_Key.SweetHopsSeed > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new SweetHopsSeed(m_Key.WithdrawIncrement));
                    m_Key.SweetHopsSeed = m_Key.SweetHopsSeed - m_Key.WithdrawIncrement;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
                }
                else if (m_Key.SweetHopsSeed > 0)
                {
                    m_From.AddToBackpack(new SweetHopsSeed(m_Key.SweetHopsSeed));
                    m_Key.SweetHopsSeed = 0;
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any Sweet Hop Seeds stored!");
                    m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 12)
            {
                m_From.SendGump(new FarmersSeedBoxGump(m_From, m_Key));
                m_Key.BeginCombine(m_From);
            }
        }
    }
}

namespace Server.Items
{
    public class FarmersSeedBoxTarget : Target
    {
        private FarmersSeedBox m_Key;

        public FarmersSeedBoxTarget(FarmersSeedBox key) : base( 18, false, TargetFlags.None )
        {
            m_Key = key;
        }
        protected override void OnTarget(Mobile from, object targeted)
        {
            if (m_Key.Deleted)
                return;

            m_Key.EndCombine(from, targeted);
        }
    }
}
