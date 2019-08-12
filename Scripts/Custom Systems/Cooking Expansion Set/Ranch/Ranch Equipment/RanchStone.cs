using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Regions;
using Server.Prompts;
using Server.Multis;
using Server.Gumps;

namespace Server.Items
{
	[Flipable( 0x14F0, 0x14EF )]
	public class RanchDeed : Item
	{
		[Constructable]
		public RanchDeed() : base( 0x14F0 )
		{
			Name = "a ranch deed";
			Weight = 1.0;
		}

		public virtual bool NotForbiddenRegions(Mobile from)
		{
			if (from.Region is NoHousingRegion) return false;
			if (from.Region is GuardedRegion) return false;
			if (from.Region is DungeonRegion) return false;
			if (from.Region is TownRegion) return false;
			return true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if (this.IsChildOf( from.Backpack ))
			{
				bool canplace = true;

				foreach ( Item item in this.GetItemsInRange( 11 ) ) 
				{
					if ((item is BaseHouse ) || (item is RanchStone)) canplace = false;
				}

				if ( (NotForbiddenRegions(from) && canplace) || (from.AccessLevel >= AccessLevel.GameMaster))
				{
					Point3D rd = new Point3D(0,0,0);
					if (from.Direction == Direction.North) rd = new Point3D(from.X, from.Y -1, from.Z);
					else if (from.Direction == Direction.South) rd = new Point3D(from.X, from.Y +1, from.Z);
					else if (from.Direction == Direction.West) rd = new Point3D(from.X-1, from.Y, from.Z);
					else if (from.Direction == Direction.East) rd = new Point3D(from.X+1, from.Y, from.Z);

					else if (from.Direction == Direction.Up) rd = new Point3D(from.X-1, from.Y-1, from.Z);
					else if (from.Direction == Direction.Down) rd = new Point3D(from.X+1, from.Y+1, from.Z);
					else if (from.Direction == Direction.Left) rd = new Point3D(from.X-1, from.Y+1, from.Z);
					else rd = new Point3D(from.X+1, from.Y-1, from.Z);
	
					RanchStone rs = new RanchStone();
					rs.Owner = from;
					rs.Ranch = from.Name + "'s Ranch";
					rs.Movable = false;
					rs.MoveToWorld(rd, from.Map);
					this.Delete();
				}
				else
				{
					from.SendMessage("That can't be placed here");
				}
			}
			else
			{
				from.SendMessage("That must be in your backpack to use");
			}
		}

		public RanchDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class RanchStone : Item, IChopable
	{
		private Mobile m_Owner; 
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}

		private String m_Ranch; 
		[CommandProperty( AccessLevel.GameMaster )]
		public String Ranch
		{
			get{ return m_Ranch; }
			set{ m_Ranch = value; InvalidateProperties(); }
		}

		private int m_Size = 10; 
		[CommandProperty( AccessLevel.GameMaster )]
		public int Size
		{
			get{ return m_Size; }
			set{ m_Size = value; InvalidateProperties(); }
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add(m_Ranch);
			if (m_Owner != null && !m_Owner.Deleted) list.Add("Owner: " + m_Owner.Name);
		}

		[Constructable]
		public RanchStone() : base(0xED6) 
		{
			Name = "a ranch stone";
			m_Size = 10;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if (!(from.HasGump( typeof( RanchStoneGump))))
			{
				if (from == m_Owner || from.AccessLevel >= AccessLevel.GameMaster)
				{
					from.SendGump( new RanchStoneGump(this) );
				}
				else from.SendMessage("You aren't the ranch owner.");
			}
		}

		public virtual void OnChop(Mobile from)
		{
			if (from == Owner)
			{
				Effects.PlaySound( GetWorldLocation(), Map, 0x3B3 );
				RanchDeed rd = new RanchDeed();
				from.AddToBackpack(rd);
				from.SendMessage("Your ranch has been redeeded");
				if(m_Size > 10)
				{
					for ( int i = 0; i < ((m_Size-10)/5); ++i )
					{
						from.AddToBackpack(new RanchExtensionDeed());
					}
				}
				this.Delete();
			}
		}

		public virtual void UpdateFences()
		{
			foreach ( Item item in this.GetItemsInRange( m_Size+1 ) ) 
			{
				if (item is FenceGate)
				{
					FenceGate fg = (FenceGate) item;
					if (fg.ranchstone == this)
					{
						fg.InvalidateProperties();
					}
				}
				else if (item is BaseFence)
				{
					BaseFence f = (BaseFence) item;
					if (f.ranchstone == this)
					{
						f.InvalidateProperties();
					}
				}
			}
		}

		public override void OnDelete()
		{
			foreach ( Item item in this.GetItemsInRange( m_Size+1 ) ) 
			{
				if (item is FenceGate)
				{
					FenceGate fg = (FenceGate) item;
					if (fg.ranchstone == this)
					{
						fg.ranchstone = null;
						fg.Movable = true;
					}
				}
				else if (item is BaseFence)
				{
					BaseFence f = (BaseFence) item;
					if (f.ranchstone == this)
					{
						f.ranchstone = null;
						f.Movable = true;
					}
				}
			}
		}

		public RanchStone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
			
			writer.Write(m_Size);
			
			writer.Write( m_Owner );
			writer.Write( (string) m_Ranch );
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch (version)
			{
			case 1:
				{
					m_Size = reader.ReadInt();
					goto case 0;
				}
			case 0:
				{
					m_Owner = reader.ReadMobile();
					m_Ranch = reader.ReadString();
					break;
				}
			}
		}
	}
}
	
namespace Server.Gumps
{
	public class RanchStoneGump : Gump
	{
		public enum Buttons
		{
			None,
			BrandingIron,
			FencingHammer,
			UnlockFences,
			RanchName,
			Redeed,
			TransferOwnership,
			ReclaimAnimal,
			AbsorbRanch,
			TransferAnimal,
		}
		
		private RanchStone m_rs;
		
		public RanchStoneGump(RanchStone rs) : base( 0, 0 )
		{
			m_rs = rs;

			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(0, 0, 250, 300, 9200);
			//this.AddLabel(15, 45, 1160, @"Ranch Name: "+m_rs.Ranch);//ranch name
			this.AddHtml(15, 40, 220, 35, (String.Format("<CENTER><BIG><BASEFONT SIZE=7 COLOR=#FFFF00>{0}<BASEFONT COLOR=#000000></BIG></CENTER>",m_rs.Ranch)), false, false);//"Ranch Name: "+m_rs.Ranch);//ranch name
			this.AddHtml(15, 60, 220, 35, (String.Format("<CENTER><BASEFONT SIZE=4 COLOR=#FFFF00>Owner: {0} <BASEFONT COLOR=#000000></CENTER>",m_rs.Owner.Name)), false, false);//ranch owner
			this.AddLabel(30, 80, 0, @"Toggle Branding Iron");
			this.AddLabel(30, 100, 0, @"Reclaim Animal");
			this.AddLabel(30, 120, 0, @"Transfer Animal");
			this.AddLabel(30, 140, 0, @"Toggle Fencing Hammer");
			this.AddLabel(30, 160, 0, @"Unlock Fences");
			this.AddLabel(30, 180, 0, @"Rename Ranch");
			this.AddLabel(30, 200, 0, @"Redeed Ranch Stone");
			this.AddLabel(30, 220, 0, @"Absorb Other Ranch");
			this.AddLabel(30, 240, 0, @"Transfer Ownership");
			this.AddButton(190, 85, 1209, 1210, (int)Buttons.BrandingIron, GumpButtonType.Reply, 0);
			this.AddButton(190, 105, 1209, 1210, (int)Buttons.ReclaimAnimal, GumpButtonType.Reply, 0);
			this.AddButton(190, 125, 1209, 1210, (int)Buttons.TransferAnimal, GumpButtonType.Reply, 0);
			this.AddButton(190, 145, 1209, 1210, (int)Buttons.FencingHammer, GumpButtonType.Reply, 0);
			this.AddButton(190, 165, 1209, 1210, (int)Buttons.UnlockFences, GumpButtonType.Reply, 0);
			this.AddButton(190, 185, 1209, 1210, (int)Buttons.RanchName, GumpButtonType.Reply, 0);
			this.AddButton(190, 205, 1209, 1210, (int)Buttons.Redeed, GumpButtonType.Reply, 0);
			this.AddButton(190, 225, 1209, 1210, (int)Buttons.AbsorbRanch, GumpButtonType.Reply, 0);
			this.AddButton(190, 245, 1209, 1210, (int)Buttons.TransferOwnership, GumpButtonType.Reply, 0);
			this.AddImage(5, 5, 10460);
			this.AddImage(210, 5, 10460);
			this.AddImage(5, 265, 10460);
			this.AddImage(210, 265, 10460);
			this.AddImage(42, 262, 11392);
			this.AddLabel(50, 270, 1160, @"Size: "+ m_rs.Size);//ranch size
			this.AddLabel(120, 270, 1160, @"Area: "+ (int) (m_rs.Size * m_rs.Size * 3.14159));//ranch area
			this.AddImage(42, 5, 11392);
			this.AddLabel(85, 12, 1152, @"Ranch Stone");
		}
		
		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile from = sender.Mobile;
			if (info.ButtonID == (int)Buttons.BrandingIron)
			{
				Container pack = from.Backpack;
				ArrayList bagitems = new ArrayList( pack.Items );
				int ironTotal = 0;
				foreach (Item item in bagitems)
				{
					if (item is BrandingIron)
					{
						BrandingIron iron = (BrandingIron)item;
						if (iron.ranchstone == m_rs)
						{
							iron.Delete();
							ironTotal++;
						}
					}
				}

				if (ironTotal > 0)
				{
					from.SendMessage("You return the branding iron.");
				}
				else
				{
					from.SendMessage("Added a branding iron to your pack.");
					BrandingIron bi = new BrandingIron();
					bi.ranchstone = m_rs;
					bi.Hot = true;
					from.AddToBackpack(bi);
				}
				from.SendGump( new RanchStoneGump(m_rs) );
			}
			else if (info.ButtonID == (int)Buttons.FencingHammer)
			{
				Container pack = from.Backpack;
				ArrayList equipitems = new ArrayList(from.Items);
				ArrayList bagitems = new ArrayList( pack.Items );

				int hammerTotal = 0;

				foreach (Item item in equipitems)
				{
					if (item is FencingHammer)
					{
						FencingHammer hammer = (FencingHammer)item;
						if (hammer.ranchstone == m_rs)
						{
							hammer.Delete();
							hammerTotal++;
						}
					}
				}
				foreach (Item item in bagitems)
				{
					if (item is FencingHammer)
					{
						FencingHammer hammer = (FencingHammer)item;
						if (hammer.ranchstone == m_rs)
						{
							hammer.Delete();
							hammerTotal++;
						}
					}
				}

				if (hammerTotal > 0)
				{
					from.SendMessage("You return the fencing hammer.");
				}
				else
				{
					from.SendMessage("Added a fencing hammer to your pack.");
					FencingHammer fh = new FencingHammer();
					fh.Identified = true;
					fh.ranchstone = m_rs;
					from.AddToBackpack(fh);
				}
				from.SendGump( new RanchStoneGump(m_rs) );
			}
			else if (info.ButtonID == (int)Buttons.UnlockFences)
			{
				foreach ( Item item in m_rs.GetItemsInRange( m_rs.Size+1 ) ) 
				{
					if (item is FenceGate)
					{
						FenceGate fg = (FenceGate) item;
						if (fg.ranchstone == m_rs)
						{
							fg.ranchstone = null;
							fg.Movable = true;
						}
					}
					else if (item is BaseFence)
					{
						BaseFence f = (BaseFence) item;
						if (f.ranchstone == m_rs)
						{
							f.ranchstone = null;
							f.Movable = true;
						}
					}
				}
				from.SendGump( new RanchStoneGump(m_rs) );
			}
			else if (info.ButtonID == (int)Buttons.RanchName)
			{
				from.SendMessage ( "Enter new ranch name." ); 
				from.Prompt = new RenamePrompt( m_rs );
			}
			else if (info.ButtonID == (int)Buttons.Redeed)
			{
				RanchDeed rd = new RanchDeed();
				from.AddToBackpack(rd);
				from.SendMessage("Your ranch has been redeeded.");
				if(m_rs.Size > 10)
				{
					for ( int i = 0; i < ((m_rs.Size-10)/5); ++i )
					{
						from.AddToBackpack(new RanchExtensionDeed());
					}
				}
				m_rs.Delete();
			}
			else if (info.ButtonID == (int)Buttons.TransferOwnership)
			{
				from.SendMessage ( "Target the person to transfer ownership to." ); 
				from.Target = new OwnershipTarget(m_rs);
			}
			else if (info.ButtonID == (int)Buttons.ReclaimAnimal)
			{
				from.SendMessage ( "Target the animal you wish to claim." ); 
				from.Target = new ClaimTarget(m_rs);
			}
			else if (info.ButtonID == (int)Buttons.TransferAnimal)
			{
				from.SendMessage ( "Target the animal you wish to transfer." ); 
				from.Target = new TransferTarget(m_rs);
			}
			else if (info.ButtonID == (int)Buttons.AbsorbRanch)
			{
				from.SendMessage ( "Target the ranch you wish to absorb." ); 
				from.Target = new AbsorbTarget(m_rs);
			}
		}
		
		private class RenamePrompt : Prompt
		{
			private RanchStone m_RS;

			public RenamePrompt( RanchStone RS )
			{
				m_RS = RS;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_RS.Ranch = text;
				m_RS.UpdateFences();
				from.SendMessage ( "The ranch name has been changed." ); 
				from.SendGump( new RanchStoneGump(m_RS) );
			}
		}
		
		private class OwnershipTarget : Target
		{
			private RanchStone t_rs;

			public OwnershipTarget(RanchStone rs) : base( 1, false, TargetFlags.None )
			{
				t_rs = rs;
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is PlayerMobile)
				{
					PlayerMobile pm = (PlayerMobile) targ;
					t_rs.Owner = pm;
					from.SendMessage("Ownership has been transferred.");
					pm.SendMessage("You are now the owner of "+t_rs.Ranch);
					pm.SendGump( new RanchStoneGump(t_rs) );
				}
				else
				{
					from.SendMessage ("That can't take control of your ranch.");
					from.SendGump( new RanchStoneGump(t_rs) );
				}
			}
		}
		
		private class ClaimTarget : Target
		{
			private RanchStone t_rs;

			public ClaimTarget(RanchStone rs) : base( 10, false, TargetFlags.None )
			{
				t_rs = rs;
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is BaseAnimal)
				{
					BaseAnimal ba = (BaseAnimal) targ;
					if (ba.Owner == from)
					{
						if (from.Followers + ba.ControlSlots <= from.FollowersMax)
						{
							ba.Controlled = true;
							ba.ControlMaster = from;
							ba.ControlTarget = from;
							ba.ControlOrder = OrderType.Follow;
							from.SendMessage("You claim your animal.");
						}
						else from.SendMessage("You have too many followers!");
						from.SendGump( new RanchStoneGump(t_rs) );
					}
					else
					{
						from.SendMessage("You don't own that.");
						from.SendGump( new RanchStoneGump(t_rs) );
					}
				}
				else
				{
					from.SendMessage ("You can't claim that.");
					from.SendGump( new RanchStoneGump(t_rs) );
				}
			}
		}
		
		private class TransferTarget : Target
		{
			private RanchStone t_rs;

			public TransferTarget(RanchStone rs) : base( 10, false, TargetFlags.None )
			{
				t_rs = rs;
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is BaseAnimal)
				{
					BaseAnimal ba = (BaseAnimal) targ;
					if (ba.Owner == from)
					{
						from.SendMessage("Target player to transfer to.");
						from.Target = new TransferToTarget(ba, t_rs);
					}
					else
					{
						from.SendMessage("You don't own that.");
						from.SendGump( new RanchStoneGump(t_rs) );
					}
				}
				else
				{
					from.SendMessage ("You can't claim that.");
					from.SendGump( new RanchStoneGump(t_rs) );
				}
			}
		}
		
		private class TransferToTarget : Target
		{
			private BaseAnimal t_ba;
			private RanchStone t_rs;

			public TransferToTarget(BaseAnimal ba, RanchStone rs) : base( 10, false, TargetFlags.None )
			{
				t_ba = ba;
				t_rs = rs;
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is PlayerMobile)
				{
					if (targ != from)
					{
						PlayerMobile pm = (PlayerMobile) targ;
						if (pm.Followers + t_ba.ControlSlots <= pm.FollowersMax)
						{
							t_ba.Owner = pm;
							t_ba.Controlled = true;
							t_ba.ControlMaster = pm;
							t_ba.ControlTarget = pm;
							t_ba.ControlOrder = OrderType.Follow;
							from.SendMessage(t_ba.Name + " has been transferred to " + pm.Name + ".");
							pm.SendMessage(from.Name + " has transferred " + t_ba.Name + " to you.");
						}
						else
						{
							pm.SendMessage("You have too many followers!");
							from.SendMessage(pm.Name + " has too many followers!");
						}
					}
					else from.SendMessage("You can't transfer your animal to yourself!");
				}
				else from.SendMessage ("You can't transfer your animal to that.");
				from.SendGump( new RanchStoneGump(t_rs) );
			}
		}
		
		private class AbsorbTarget : Target
		{
			private RanchStone t_rs;

			public AbsorbTarget(RanchStone rs) : base( 1, false, TargetFlags.None )
			{
				t_rs = rs;
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is RanchStone)
				{
					RanchStone stone = (RanchStone) targ;
					if (stone.Owner == from)
					{
						if(t_rs.Map == stone.Map)
						{
							int ranchrange = (int)(from.GetDistanceToSqrt(t_rs.Location));
							int ranchto = (int)(stone.Size/2);
							int ranchfrom = (int)(t_rs.Size/2);
							if ((ranchfrom >= (ranchrange - (ranchto + ranchfrom))) && t_rs.Size > stone.Size)
							{
								foreach ( Item item in stone.GetItemsInRange( stone.Size+1 ) ) 
								{
									if (item is FenceGate)
									{
										FenceGate fg = (FenceGate) item;
										if (fg.ranchstone == stone)
										{
											fg.ranchstone = t_rs;
											fg.InvalidateProperties();
										}
									}
									else if (item is BaseFence)
									{
										BaseFence f = (BaseFence) item;
										if (f.ranchstone == stone)
										{
											f.ranchstone = t_rs;
											f.InvalidateProperties();
										}
									}
								}
								RanchDeed rd = new RanchDeed();
								from.AddToBackpack(rd);
								from.SendMessage("You absorb the ranch.");
								stone.Delete();
							}
							else from.SendMessage("Your ranch isn't big enough to absorb target ranch!");
						}
						else from.SendMessage("That ranch is on a different facet!");
					}
					else from.SendMessage("That's not your ranch!");
				}
				else
				{
					from.SendMessage ("You can't absorb that!");
				}
				from.SendGump( new RanchStoneGump(t_rs) );
			}
		}
	}
}

