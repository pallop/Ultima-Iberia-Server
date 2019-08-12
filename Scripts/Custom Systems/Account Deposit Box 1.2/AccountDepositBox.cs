/*
					  _________________________
					=(__    ___      ___      _)=
					  |     Scripted By:      |
					  |        JBob           |
					  |    Shout Out To       |
					  |   Enroq, Ravenwolfe,  |
					  |    zerodowned and     |
					  | stnewton, For Helping |
					  |    			          |
					  |     Version 1.2       |
					  |    Using DateTime     |
					  |    Date: 9/1/2014     |
					  |__    ___    ___    ___|
					=(_________________________)=
 
Version 1.2 Changes
	-Renewal Gump
	-DateTime Checks
	-Player Buys use of the Account Chest for 50k for 1 month.
	 When that month is up, a Renewal Gump will be presented on next use of the Chest.
	 If the player does not have the money in the bank to Renew, Everything will be put in the players BankBox.
ToDo
	Check for players BankBox weight/item amount and figure out what to do if the bank is full or close to full.
		(Possibly find a way to distribute the items to the next available player character on that account, if there is one.)
	More Testing.
*/

using Server;
using Server.Items;
using Server.Multis;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;
using Server.Gumps;
using Server.Commands;
using System;
using System.Collections.Generic;


namespace Server.Items
{
	[FlipableAttribute( 0xE41, 0xE40 )] 
	public class AccountDepositBox : BaseContainer 
	{
		private string m_Account;
		private DateTime m_RenewTime;		
		
		[CommandProperty(AccessLevel.GameMaster)]
		public string Account
		{
			get
			{
				return this.m_Account;
			}
			set
			{
				this.m_Account = value;
				if (this.m_Account == null)
					this.Name = "Account Deposit Box";
					
				InvalidateProperties();
			}
		}

		
		[CommandProperty(AccessLevel.GameMaster)]
		public DateTime RenewTime
		{
			get
			{
				return this.m_RenewTime;
			}
			set
			{
				this.m_RenewTime = value;
			}
		}

		[Constructable]
		public AccountDepositBox()
			: base(0xE41)
		{
			this.m_Account = null;
			this.m_RenewTime = DateTime.UtcNow + TimeSpan.FromDays(30.0);//Change How long you want the Rent Time.
			//this.m_RenewTime = DateTime.UtcNow + TimeSpan.FromSeconds(30.0);//Testing Purpose
			this.Name = "Account Deposit Box";
			this.Movable = false;
			this.Hue = 0;
		}
		
		[Constructable]
		public AccountDepositBox(string account)
			: base(0xE41)
		{
			this.m_Account = account;
			this.m_RenewTime = DateTime.UtcNow + TimeSpan.FromDays(30.0);//Change How long you want the Rent Time.
			//this.m_RenewTime = DateTime.UtcNow + TimeSpan.FromSeconds(30.0);//Testing Purpose
			this.Name = "Account Deposit Box";
			this.Movable = false;
			this.Hue = 0x482;
		} 
		
		public AccountDepositBox( Serial serial ) : base( serial ) 
		{ 
		}
		
		public override bool IsDecoContainer{ get{ return false; } }
		public override int MaxWeight{ get{ return 1200; } }
		public virtual new int MaxItems{ get{ return 250; } }

		public override void OnDoubleClick(Mobile from)
		{
			if (from.InRange( this.GetWorldLocation(), 2 ) == false)
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
				return;
			}
			else if (DateTime.UtcNow >= this.RenewTime)
			{
				Console.WriteLine("Time To Renew");
				if (this.Account != null)
				{
					//Console.WriteLine("Player is online, Sending Gump...");//Debug Line
					from.SendGump(new AccountDBoxRenewGump(from, this));
				}
			}
			else if ( this.m_Account == null && DateTime.UtcNow < this.RenewTime )
			{
				from.SendGump(new AccountDBoxGump(from, this));
			}
			else
			{
				if ( this.m_Account == from.Account.Username || from.AccessLevel >= AccessLevel.Seer )
				{
					
					if ( from.AccessLevel >= AccessLevel.Seer || from.InRange( this.GetWorldLocation(), 2 ) )
					{
						Open( from );
						return;
					}
					if ( this.RootParent is PlayerVendor )
					{
						Open( from );
						return;
					}
					else
					{
						from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
					}
					return;
				}
				else if ( this.m_Account != from.Account.Username )
				{
					from.SendMessage( "This is not yours to use.  You should consider buying your own Account Deposit Box." );
					return;
				}
			}
			return;
		}

		public override bool OnDragDrop(Mobile from, Item dropped)
		{
			if (from.Account.Username != this.m_Account)
			{
				from.SendMessage("This is not your Account Deposit Box.");
				return false;
			}
			else
			{
				DropItem(dropped);
				return true;
			}
			return base.OnDragDrop(from, dropped);
		}

		public override void AddNameProperty(ObjectPropertyList list)
		{
			base.AddNameProperty( list );
			if (m_Account == null)
			{ 
				list.Add( "<BASEFONT COLOR=#669966>" + "[Price: 300 Gold]" + "<BASEFONT COLOR=#FFFFFF>" );
	  		}
			else if (m_Account != null)
			{
				list.Add( "<BASEFONT COLOR=#669966>" + "[Unavailable]" + "<BASEFONT COLOR=#FFFFFF>" );
			}
		}

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write(0);
			
			writer.Write(m_Account);
			writer.Write(m_RenewTime);
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt();
			
			this.m_Account = reader.ReadString();
			this.m_RenewTime = reader.ReadDateTime();
		}
	} 
}