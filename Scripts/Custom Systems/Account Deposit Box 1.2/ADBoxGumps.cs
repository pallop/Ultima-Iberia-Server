using Server;
using Server.Items;
using Server.Multis;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;
using Server.Gumps;
using System;
using System.Collections.Generic;

namespace Server.Gumps
{

	public class AccountDBoxGump : Gump
	{
		private AccountDepositBox m_box;
		
		public AccountDBoxGump(Mobile from, AccountDepositBox box) : this()
		{
			m_box = box;
		}

		public AccountDBoxGump() : base( 0, 0 )
		{
			this.Closable=false;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;

			AddPage(0);
			AddBackground(213, 137, 215, 168, 9200);
			AddHtml( 221, 145, 198, 90, @"You are about to buy use of this Account Deposit Box for 300 Gold.", (bool)true, (bool)false);
			AddLabel(291, 244, 0, @"Continue?");
			AddButton(241, 271, 247, 248, (int)Buttons.Okay, GumpButtonType.Reply, 0);
			AddButton(339, 271, 241, 243, (int)Buttons.Cancel, GumpButtonType.Reply, 0);  
		}
		
		public enum Buttons
		{
			Okay,
			Cancel
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile from = sender.Mobile;
			if (from == null)
				return;
			
			Container cont = from.FindBankNoCreate();
			
			if (cont == null)
				return;
			
			switch(info.ButtonID)
			{
				case (int)Buttons.Okay:
				{
					if ( from.Backpack.ConsumeTotal( typeof( Gold ), 300 ) == true )
					{
						//m_box.RenewTime = DateTime.Now + TimeSpan.FromSeconds(30.0);//Testing Purpose
						m_box.RenewTime = DateTime.Now + TimeSpan.FromDays(30.0);//Change How long you want the Rent Time. 
						m_box.Account = from.Account.Username;
						m_box.Hue = 0x482;
						m_box.Name = from.Name + "'s Account Deposit Box";
						from.SendMessage("You successfully bought use of this Account Deposit Box!");
						from.SendMessage("This Account Deposit Box is now linked to all characters on this account.");
					}
					else
					{
						from.SendMessage( "You do not have enough gold in the Backpack to purchase the Account Deposit Box." );
					}
					break;
				}
				case (int)Buttons.Cancel:
				{
					from.SendMessage("You decide against buying use of this Account Deposit Box.");
					from.CloseGump(typeof(AccountDBoxGump));
					break;
				}
			}
		}
	}

	public class AccountDBoxRenewGump : Gump
	{
		private AccountDepositBox m_box;
		
		public AccountDBoxRenewGump(Mobile from, AccountDepositBox box) : this()
		{
			m_box = box;
		}

		public AccountDBoxRenewGump() : base( 0, 0 )
		{
			this.Closable=false;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;

			AddPage(0);
			AddBackground(213, 137, 215, 168, 9200);
			AddHtml( 221, 145, 198, 90, @"You are about to renew use of this Account Deposit Box for 300 Gold.", (bool)true, (bool)false);
			AddLabel(291, 244, 0, @"Continue?");
			AddButton(241, 271, 247, 248, (int)Buttons.Okay, GumpButtonType.Reply, 0);
			AddButton(339, 271, 241, 243, (int)Buttons.Cancel, GumpButtonType.Reply, 0);  
		}

		public enum Buttons
		{
			Okay,
			Cancel
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile from = sender.Mobile;
			if (from == null)
				return;
			
			Container cont = from.FindBankNoCreate();
			
			if (cont == null)
				return;
			
			switch(info.ButtonID)
			{
				case (int)Buttons.Okay:
				{
					if ( from.Backpack.ConsumeTotal( typeof( Gold ), 300 ) == true )
					{
						//m_box.RenewTime = DateTime.Now + TimeSpan.FromSeconds(30.0);//Testing Purpose
						m_box.RenewTime = DateTime.Now + TimeSpan.FromDays(30.0);//Change How long you want the Rent Time.
						from.SendMessage("You successfully renewed your use of this Account Deposit Box!");
					}
					else
					{
						m_box.Account = null;
						m_box.Name = "Account Deposit Box";
						m_box.Hue = 0;
						#region Move Items to Bank
						List<Item> Items = new List<Item>();
	 
						foreach (Item item in m_box.Items)
							Items.Add(item);
	 
						for (int x = 0; x < Items.Count; x++)
							from.Backpack.DropItem(Items[x]);
						#endregion
						from.SendMessage( "You do not have enough gold in your bank to renew your Account Deposit Box." );
					}
					break;
				}
				case (int)Buttons.Cancel:
				{
					m_box.Account = null;
					m_box.Name = "Account Deposit Box";
					m_box.Hue = 0;
					from.SendMessage("You decide against renewing use of your Account Deposit Box.");
					from.CloseGump(typeof(AccountDBoxRenewGump));
					#region Move Items to Bank
					List<Item> Items = new List<Item>();
 
					foreach (Item item in m_box.Items)
						Items.Add(item);
 
					for (int x = 0; x < Items.Count; x++)
						sender.Mobile.BankBox.DropItem(Items[x]);
					#endregion
					break;
				}
			}
		}
	}
}