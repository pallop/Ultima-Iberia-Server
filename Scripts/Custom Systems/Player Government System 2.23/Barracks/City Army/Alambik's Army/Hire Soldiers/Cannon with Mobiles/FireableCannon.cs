using Server.Engines.XmlSpawner2;
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Spells;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.ContextMenus;
using Server.Ashlar;

namespace Server.Items
{
	
	public class HitchItem : Item
	{
		private Mobile m_Owner;
		private Mobile m_Hitch;
		private MovableCannon m_MCannon;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Hitch
		{
			get{ return m_Hitch; }
			set{ m_Hitch = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public MovableCannon MCannon
		{
			get{ return m_MCannon; }
			set{ m_MCannon = value; }
		}
		
		public HitchItem(Mobile from, Mobile hitch, MovableCannon mcan ) : base( 0x1f13 )
		{
			Visible = false;
			Movable = false;
			Weight = 200.0;
			m_Owner = from;
			m_Hitch = hitch;
			m_MCannon = mcan;
                    
		}
		
		public HitchItem( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize(writer);
			writer.Write((int)0); //Version
			writer.Write(m_Owner);
			writer.Write(m_Hitch);
			writer.Write(m_MCannon);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_Owner = reader.ReadMobile();
			m_Hitch = reader.ReadMobile();
			m_MCannon = (MovableCannon)reader.ReadItem();
		}
	}
 
	public class CannonComponent : AddonComponent
	{
		private BaseCannon m_cannon;
		public DateTime NextShot = DateTime.Now;
		
		public BaseCannon cannon{ get{ return m_cannon; } }
		
		public override bool ForceShowProperties{ get{ return true;} }
		
		public CannonComponent( int itemID, BaseCannon c ) : base( itemID )
		{
			m_cannon = c;
		}
  
		public override bool HandlesOnSpeech{ get{ return true; } }
		public override void OnSpeech( SpeechEventArgs e )
		{
			Mobile from = e.Mobile;
			if( from != m_cannon.Owner || !from.InRange(this,3) )
				return;
			string said = e.Speech.ToLower();
			if( said.IndexOf("hitch") != - 1 && said.IndexOf("unhitch") == - 1)
			{
				if( NextShot > DateTime.Now )
					from.SendMessage("You must wait for the cannon to cool down before you can hitch it.");
				else
					from.Target = new InternalTarget1(this);
			}
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			//list.Add( 1060658, "#{0}\t{1}", "1049578", m_Hits ); // ~1_val~: ~2_val~
			if ( m_cannon.Owner != null )
				list.Add( 1041602, m_cannon.Owner.Name ); // Owner: ~1_val~
			else
				list.Add( 1041602, "#1011051" ); // Owner: ~1_val~
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if( from.AccessLevel > AccessLevel.Counselor ) // No checks for staff
			{
				from.Target = new InternalTarget(this);
				return;
			}
			if ( from != m_cannon.Owner )
			{
				from.SendLocalizedMessage( 500364 ); // You can't use that, it belongs to someone else.
				return;
			}
			if( from.Mounted )
			{
				from.SendLocalizedMessage( 1010097 ); // You cannot use this while mounted.
				return;
			}
			if( !from.InRange( m_cannon.Location, 3 ) )
			{
				from.SendLocalizedMessage( 500295 ); // You are too far away to do that.
				return;
			}
			if( NextShot < DateTime.Now )
				from.Target = new InternalTarget(this);
			else
				from.SendMessage( "The cannon is too hot! You must wait {0} seconds before firing again.", ((TimeSpan)(NextShot - DateTime.Now)).Seconds );
		}
		
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			if ( (from == cannon.Owner || from.AccessLevel >= AccessLevel.GameMaster) && from.Alive )
			{
				list.Add( new Disassemble( m_cannon ) );
				//list.Add( new Fix( this ) );
			}
		}
		
		public bool CheckFiringAngle( Point3D p, Mobile from )
		{
			if ( !from.InLOS(p) )
			{
				from.SendLocalizedMessage( 500237 ); // Target cannot be seen.
				return false;
			}
			bool los = false;
			int x = 0;
			int y = 0;
			// Shooting arc only +-45º
			if (m_cannon.Direction == Direction.North && p.Y < Location.Y)
			{
				x = Math.Abs(Location.X - p.X);
				y = Location.Y - p.Y;
				if ( (y - x) >= 0 )
					los = true;
			}
			if (m_cannon.Direction == Direction.South && p.Y > Location.Y)
			{
				x = Math.Abs(Location.X - p.X);
				y = p.Y - Location.Y;
				if ( (y - x) >= 0 )
					los = true;
			}
			if (m_cannon.Direction == Direction.East && p.X > Location.X)
			{
				x = p.X - Location.X;
				y =  Math.Abs(p.Y - Location.Y);
				if ( (x - y) >= 0 )
					los = true;
			}
			if (m_cannon.Direction == Direction.West && p.X < Location.X)
			{
				x = Location.X - p.X;
				Math.Abs(y = p.Y - Location.Y);
				if ( (x - y) >= 0 )
					los = true;
			}
			if ( los )
			{
				Point3D p2 = new Point3D( Location.X, Location.Y, Location.Z + 10);
				if ( from is BaseCannonGuard || (from.Backpack.GetAmount( typeof( SiegeCannonball )) > 0 && from.Backpack.GetAmount( typeof( SulfurousAsh )) > 4 && from.InRange( m_cannon.Location, 3 ) && !from.Mounted) )
				{
					from.Backpack.ConsumeTotal( typeof( SulfurousAsh ), 5 );
					from.Backpack.ConsumeTotal( typeof( SiegeCannonball ), 1 );
					IEntity to;
					to = new Entity( Serial.Zero, new Point3D(p.X, p.Y, p.Z + 5), from.Map );
					IEntity fro;
					fro = new Entity( Serial.Zero, p2, from.Map );
					Effects.SendMovingEffect( fro, to, 0xE73, 1, 0, false, true, 0, 0 );
					Effects.PlaySound(Location,from.Map,519);
					Explode( from, new Point3D(p), from.Map );
					NextShot = DateTime.Now + TimeSpan.FromSeconds(20);// 10 seconds to next time you can fire
					return true;
				}
				else if ( from.Backpack.GetAmount( typeof( SiegeCannonball )) == 0)
					from.Say("You don't have any cannon balls!");
				else if ( from.Backpack.GetAmount( typeof( SulfurousAsh )) < 5)
					from.SendLocalizedMessage( 1049617 ); // You do not have enough sulfurous ash.
				else if ( from.Mounted )
					from.SendLocalizedMessage( 1010097 ); // You cannot use this while mounted.
				else
					from.Say("You are too far from the cannon!");
			}
			else
				from.SendLocalizedMessage( 500237 ); // Target cannot be seen.
			return false;
		}
		
		private void Explode( Mobile from, Point3D loc, Map map )
		{
			if ( map == null )
				return;
			
			IPooledEnumerable eable = map.GetObjectsInRange( loc, 2 );
			ArrayList toExplode = new ArrayList();
			
			foreach ( object o in eable )
			{
				if ( o is Mobile || o is BaseMulti || o is BaseAddon || o is PelopsItem)
					toExplode.Add( o );
			}
			
			eable.Free();
			int d = 0; // Damage scalar
			int damage = 0;
                        int MinDamage = 6;
                        int MaxDamage = 60;
			for ( int i = 0; i < toExplode.Count; ++i )
			{
				object o;
				o = toExplode[i];
				
				if ( o is Mobile )
				{
					Mobile m = (Mobile)o;
					if ( m.InRange( loc, 0 ) )
						d = 1;
					else if ( m.InRange( loc, 1 ) )
						d = 2;
					else if ( m.InRange( loc, 2 ) )
						d = 3;
					if ( from != null || (SpellHelper.ValidIndirectTarget( from, m ) && from.CanBeHarmful( m, false )) )
					{
						if ( from != null )
							from.DoHarmful( m );
						damage = Utility.RandomMinMax( (MinDamage / d), (MaxDamage / d) );
						if( d == 1 )
							AOS.Damage( m, from, damage, 50, 50, 0, 0, 0 ); // Same tile 50% physical 50% fire
						else
							AOS.Damage( m, from, damage, 0, 100, 0, 0, 0 ); // 2 tile radius 100% fire damage
					}
				}
	                        else if ( o is BaseMulti || o is BaseAddon /*|| o is PelopsItem*/)
				{
				// apply siege damage, all physical
				XmlSiege.Attack(from, o, 60, 0);
				}
			}
		}
		
		public CannonComponent( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
			writer.Write( m_cannon );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_cannon = reader.ReadItem() as BaseCannon;
		}
		
		private class Disassemble : ContextMenuEntry
		{
			private BaseCannon m_Cannon;
			
			public Disassemble( BaseCannon cannon ) : base( 6142, 3 )
			{
				m_Cannon = cannon;
			}
			
			public override void OnClick()
			{
				if ( !Owner.From.InRange( m_Cannon, 3 ) )
				{
					Owner.From.SendLocalizedMessage( 500446 );
				}
				else if( m_Cannon.CCom.NextShot > DateTime.Now )
					Owner.From.SendMessage("You must wait for the cannon to cool down before you can crate it.");
				else
				{  //................need a timer here so its not instantaneous
					BaseCannonDeed deed = (BaseCannonDeed)Activator.CreateInstance( m_Cannon.Deed.GetType() );
					m_Cannon.Delete();
					Owner.From.PlaceInBackpack( deed );
                                        
				}
                                  return;
			}
			
		}
		
		/*private class Fix : ContextMenuEntry
		{
			private CannonComponent m_Cannon;
			
			public Fix( CannonComponent cannon ) : base( 2014, 3 )
			{
				m_Cannon = cannon;
			}
			
			public override void OnClick()
			{
				if ( !Owner.From.InRange( m_Cannon, 3 ) )
				{
					Owner.From.SendLocalizedMessage( 500446 );
				}
				else
				{
					m_Cannon.Repair(Owner.From);
				}
			}
			
		}*/
		
		private class InternalTarget1 : Target
		{
			private CannonComponent m_comp;
			public InternalTarget1( CannonComponent comp ) : base( 15, true, TargetFlags.None )
			{
				m_comp = comp;
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				if( targeted is BaseMount )
				{
					BaseMount mount = (BaseMount)targeted;
					
					Item[] items = from.Backpack.FindItemsByType( typeof(HitchItem) );
					foreach( HitchItem hi in items )
					{
						if( hi.Hitch == mount )
						{
							from.SendMessage("You can only hitch 1 cannon per mount.");
							return;
						}
					}
					
					if( mount.ControlMaster == from )
					{
						MovableCannon mc = new MovableCannon(from,mount);
						m_comp.Delete();
					}
				}
			}
		}
		
		private class InternalTarget : Target
		{
			private CannonComponent m_comp;
			
			public InternalTarget( CannonComponent comp ) : base( 20, true, TargetFlags.None )
			{
				m_comp = comp;
			}
			
			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;
				m_comp.CheckFiringAngle( new Point3D (p), from );
			}
		}
	}
	
	public class BaseCannon : BaseAddon//, ISiegeWeapon
	{
		private Mobile m_Owner;
		private CannonComponent m_CCom;
		
		public new virtual BaseCannonDeed Deed{ get{ return null; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public CannonComponent CCom
		{
			get{ return m_CCom; }
			set{ m_CCom = value; }
		}
		
		public BaseCannon(Direction dir) : this (null,dir)
		{
		}
		
		public BaseCannon(Mobile owner, Direction dir)
		{
                        // make them siegable by default
			// XmlSiege( hitsmax, resistfire, resistphysical, wood, iron, stone)
			XmlAttach.AttachTo(this, new XmlSiege(100, 10, 10, 20, 30, 0));
   
			m_Owner = owner;
			Weight = 400;
			Direction = dir;
			switch (Direction)
			{
				case Direction.North: //North
					{
						AddComponent( new AddonComponent( 0xE8D ), 0, 0, 0 );
						AddCannonComponent( 0xE8C , 0, 1, 0 );
						AddComponent( new AddonComponent( 0xE8B ), 0, 2, 0 ); break;
					}
				case Direction.South: //South
					{
						AddComponent( new AddonComponent( 0xE93 ), 0, 0, 0);
						AddComponent( new AddonComponent( 0xE92 ), 0, 1, 0 );
						AddCannonComponent( 0xE91, 0, 2, 0 ); break;
					}
				case Direction.East: //East
					{
						AddComponent( new AddonComponent( 0xE94 ), 0, 0, 0 );
						AddComponent( new AddonComponent( 0xE95 ), 1, 0, 0 );
						AddCannonComponent( 0xE96, 2, 0, 0 ); break;
					}
				case Direction.West: //West
					{
						AddComponent( new AddonComponent( 0xE8E ), 0, 0, 0 );
						AddCannonComponent( 0xE8F, 1, 0, 0 );
						AddComponent( new AddonComponent( 0xE90 ), 2, 0, 0 ); break;
					}
			}
		}
		
		private void AddCannonComponent( int itemID, int x, int y, int z )
		{
			AddonComponent component = new CannonComponent( itemID, this );
			component.Name = "cannon";
			m_CCom = (CannonComponent)component;
			AddComponent( component, x, y, z );
		}
		
		public BaseCannon( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
			writer.Write( m_Owner );
			writer.Write( m_CCom );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			m_Owner = reader.ReadMobile();
			m_CCom = (CannonComponent)reader.ReadItem();
		}
	}
	
	public class MovableCannon : BaseAddon
	{
		private Mobile m_Owner;
		private Mobile m_Hitch;
		private HitchItem m_HControl;
		
		public override bool HandlesOnSpeech{ get{ return true; } }
		public override bool HandlesOnMovement{ get{return true;} }
		private static int[] NorthID = new int[]{0xE8D,0xE8C,0xE8B};
		private static int[] SouthID = new int[]{0xE93,0xE92,0xE91};
		private static int[] EastID = new int[]{0xE94,0xE95,0xE96};
		private static int[] WestID = new int[]{0xE8E,0xE8F,0xE90};
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Hitch
		{
			get{ return m_Hitch; }
			set{ m_Hitch = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public HitchItem HControl
		{
			get{ return m_HControl; }
			set{ m_HControl = value; }
		}
		
		public void UnHitch()
		{
			Mobile from = m_Owner;
			Mobile mount = m_Hitch;
			Map map = Map;
			Point3D p = Location;
			BaseCannon cn = null;
			switch( Direction )
			{
				case Direction.Up:
				case Direction.North:
					{
						cn = new CannonNorth(m_Owner);
						break;
					}
				case Direction.Down:
				case Direction.South:
					{
						cn = new CannonSouth(m_Owner);
						break;
					}
				case Direction.Right:
				case Direction.East:
					{
						cn = new CannonEast(m_Owner);
						break;
					}
				case Direction.Left:
				case Direction.West:
					{
						cn = new CannonWest(m_Owner);
						break;
					}
			}
			Delete();
			bool canspawn = true;
			if( !mount.Deleted && mount != null && !mount.IsDeadBondedPet && mount.Map != Map.Internal && from.InRange(p,5) && ((BaseCreature)m_Hitch).ControlMaster == m_Owner )
			{
				foreach( AddonComponent ac in cn.Components )
				{
					if( !map.CanSpawnMobile(new Point3D(p.X + ac.Offset.X,p.Y + ac.Offset.Y,p.Z + ac.Offset.Z) ) )
						canspawn = false;
				}
			}
			if( !canspawn )
			{
				from.SendLocalizedMessage( 1011578 ); // There is an obstacle blocking this location or part of the structure would be on invalid terrain.
				MovableCannon mc = new MovableCannon(from,mount);
				mc.Location = p;
				cn.Delete();
			}
			else
			{
				from.SendMessage("The Cannon has been unhitched.");
				cn.MoveToWorld(p,map);
			}
		}
		
		public override void OnDelete()
		{
			HControl.Delete();
		}
		
		public override void OnSpeech( SpeechEventArgs e )
		{
			Mobile from = e.Mobile;
			if( from != m_Owner || !from.InRange(this,5) )
				return;
			string said = e.Speech.ToLower();
			if( said.IndexOf("unhitch") != - 1 )
				UnHitch();
		}
		
		public override void OnMovement( Mobile from, Point3D oldLocation )
		{
			if( m_Hitch == null || m_Hitch.Deleted || m_Hitch.IsDeadBondedPet || m_Hitch.Map == Map.Internal || ((BaseCreature)m_Hitch).ControlMaster != m_Owner )
				UnHitch();
			if( from == m_Hitch )
			{
				if( !from.InRange(this,5) )
				{
					UnHitch();
					return;
				}
				GetDir(from);
				GetDir(from);
			}
		}
		
		public void GetDir( Mobile from )
		{
			int x,y,z;
			switch( from.Direction )
			{
				case Direction.Up:
				case Direction.North:
					{
						x = from.Location.X;
						y = from.Location.Y + 5;
						z = from.Location.Z;
						for( int i=0; i < Components.Count; i++ )
						{
							AddonComponent item = (AddonComponent)Components[i];
							item.ItemID = SouthID[i];
							item.Offset = new Point3D( 0, i, 0 );
							item.Location = new Point3D( x,y - i,z);
							item.Direction = Direction.South;
						}
						Direction = Direction.South;
						break;
					}
				case Direction.Down:
				case Direction.South:
					{
						x = from.Location.X;
						y = from.Location.Y - 3;
						z = from.Location.Z;
						for( int i=0; i < Components.Count; i++ )
						{
							AddonComponent item = (AddonComponent)Components[i];
							item.ItemID = NorthID[i];
							item.Location = new Point3D( x,y + i,z);
							item.Offset = new Point3D( 0, i, 0 );
							item.Direction = Direction.North;
						}
						Direction = Direction.North;
						break;
					}
				case Direction.Right:
				case Direction.East:
					{
						x = from.Location.X - 3;
						y = from.Location.Y;
						z = from.Location.Z;
						for( int i=0; i < Components.Count; i++ )
						{
							AddonComponent item = (AddonComponent)Components[i];
							item.ItemID = WestID[i];
							item.Location = new Point3D( x + i,y,z);
							item.Offset = new Point3D( i, 0, 0 );
							item.Direction = Direction.West;
						}
						Direction = Direction.West;
						break;
					}
				case Direction.Left:
				case Direction.West:
					{
						x = from.Location.X + 5;
						y = from.Location.Y;
						z = from.Location.Z;
						for( int i=0; i < Components.Count; i++ )
						{
							AddonComponent item = (AddonComponent)Components[i];
							item.ItemID = EastID[i];
							item.Offset = new Point3D( i, 0, 0 );
							item.Location = new Point3D( x - i,y,z);
							item.Direction = Direction.East;
						}
						Direction = Direction.East;
						break;
					}
			}
		}
		
		public MovableCannon(Mobile owner, Mobile hitch)
		{
                        // make them siegable by default
			// XmlSiege( hitsmax, resistfire, resistphysical, wood, iron, stone)
			XmlAttach.AttachTo(this, new XmlSiege(100, 10, 10, 20, 30, 0));
   
			if( owner.Backpack == null )
			{
				UnHitch();
				return;
			}
			m_Owner = owner;
			m_Hitch = hitch;
			m_HControl = new HitchItem(m_Owner,m_Hitch,this);
			m_Owner.Backpack.DropItem(m_HControl);
			Weight = 200;
			Map = hitch.Map;
			Location = hitch.Location;
			switch (hitch.Direction)
			{
				case Direction.Up:
				case Direction.North:
					{
						AddComponent( new AddonComponent( 0xE93 ), 0, 0, 0);
						AddComponent( new AddonComponent( 0xE92 ), 0, 1, 0 );
						AddComponent( new AddonComponent( 0xE91 ), 0, 2, 0 ); break;
					}
				case Direction.Down:
				case Direction.South:
					{
						AddComponent( new AddonComponent( 0xE8D ), 0, 0, 0 );
						AddComponent( new AddonComponent( 0xE8C ), 0, 1, 0 );
						AddComponent( new AddonComponent( 0xE8B ), 0, 2, 0 ); break;
					}
				case Direction.Right:
				case Direction.East:
					{
						AddComponent( new AddonComponent( 0xE8E ), 0, 0, 0 );
						AddComponent( new AddonComponent( 0xE8F ), 1, 0, 0 );
						AddComponent( new AddonComponent( 0xE90 ), 2, 0, 0 ); break;
					}
				case Direction.Left:
				case Direction.West:
					{
						AddComponent( new AddonComponent( 0xE94 ), 0, 0, 0 );
						AddComponent( new AddonComponent( 0xE95 ), 1, 0, 0 );
						AddComponent( new AddonComponent( 0xE96 ), 2, 0, 0 ); break;
					}
			}
		}
		
		public MovableCannon( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
			writer.Write( m_Owner );
			writer.Write( m_Hitch );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			m_Owner = reader.ReadMobile();
			m_Hitch = reader.ReadMobile();
		}
	}
	#region Cannons and deeds
	public class CannonNorth : BaseCannon
	{
		public override BaseCannonDeed Deed{ get{ return new CannonNorthDeed(); } }
		
		[Constructable]
		public CannonNorth () : this ( null )
		{
		}
		
		public CannonNorth ( Mobile owner ) : base ( owner, Direction.North )
		{
		}
		
		public CannonNorth( Serial serial ) : base( serial )
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
	public class CannonNorthDeed : BaseCannonDeed
	{
		
		public override BaseCannon FireCannon{ get{ return new CannonNorth(); } }
		
		[Constructable]
		public CannonNorthDeed() : base ()
		{
			Name = "crated cannon";
   Weight = 250.0;
		}
		
		public CannonNorthDeed( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	public class CannonSouth : BaseCannon
	{
		public override BaseCannonDeed Deed{ get{ return new CannonSouthDeed(); } }
		
		[Constructable]
		public CannonSouth () : this ( null )
		{

		}
		
		public CannonSouth ( Mobile owner ) : base ( owner, Direction.South )
		{
		}
		
		public CannonSouth( Serial serial ) : base( serial )
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
	public class CannonSouthDeed : BaseCannonDeed
	{
		public override BaseCannon FireCannon{ get{ return new CannonSouth(); } }
		
		[Constructable]
		public CannonSouthDeed() : base ()
		{
			Name = "crated cannon";
    Weight = 250.0;
		}
		
		public CannonSouthDeed( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	public class CannonEast : BaseCannon
	{
		public override BaseCannonDeed Deed{ get{ return new CannonEastDeed(); } }
		
		[Constructable]
		public CannonEast () : this ( null )
		{
		}
		
		public CannonEast ( Mobile owner ) : base ( owner, Direction.East )
		{
		}
		
		public CannonEast( Serial serial ) : base( serial )
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
	public class CannonEastDeed : BaseCannonDeed
	{
		public override BaseCannon FireCannon{ get{ return new CannonEast(); } }
		
		[Constructable]
		public CannonEastDeed() : base ()
		{
			Name = "crated cannon";
    Weight = 250.0;
		}
		
		public CannonEastDeed( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	public class CannonWest : BaseCannon
	{
		public override BaseCannonDeed Deed{ get{ return new CannonWestDeed(); } }
		
		[Constructable]
		public CannonWest () : this ( null )
		{
		}
		
		public CannonWest ( Mobile owner ) : base ( owner, Direction.West )
		{
		}
		
		public CannonWest( Serial serial ) : base( serial )
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
	public class CannonWestDeed : BaseCannonDeed
	{
		public override BaseCannon FireCannon{ get{ return new CannonWest(); } }
		
		[Constructable]
		public CannonWestDeed() : base ()
		{
			Name = "crated cannon";
    Weight = 250.0;
		}
		
		public CannonWestDeed( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
	#endregion
}
