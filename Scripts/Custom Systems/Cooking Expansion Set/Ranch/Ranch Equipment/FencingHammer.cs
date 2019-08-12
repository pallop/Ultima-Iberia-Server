using System;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Items;
using Server.Mobiles;
using Server.Regions;
using Server.Prompts;
using Server.Multis;

namespace Server.Items
{
	[FlipableAttribute( 0xFB5, 0xFB4 )]
	public class FencingHammer : BaseBashing
	{
		public override WeaponAbility PrimaryAbility { get { return WeaponAbility.Dismount; } }
		public override WeaponAbility SecondaryAbility { get { return WeaponAbility.Disarm; } }

		public override int AosStrengthReq{ get{ return 5; } }
		public override int AosMinDamage{ get{ return 9; } }
		public override int AosMaxDamage{ get{ return 11; } }
		public override int AosSpeed{ get{ return 40; } }
		
		#region Mondain's Legacy
		public override float MlSpeed{ get{ return 2.75f; } }
		#endregion

		public override int OldStrengthReq{ get{ return 0; } }
		public override int OldMinDamage{ get{ return 2; } }
		public override int OldMaxDamage{ get{ return 6; } }
		public override int OldSpeed{ get{ return 35; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 110; } }

		private RanchStone m_ranchstone;
		[CommandProperty( AccessLevel.GameMaster )]
		public RanchStone ranchstone
		{
			get { return m_ranchstone; }
			set {	m_ranchstone = value; InvalidateProperties(); }
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			if (m_ranchstone != null) list.Add(m_ranchstone.Ranch);
		}

		[Constructable]
		public FencingHammer() : base( 0xFB5 )
		{
			Name = "a fencing hammer";
			Layer = Layer.OneHanded;
		}

		public virtual bool NotForbiddenRegions(Mobile from)
		{
			if (from.Map != m_ranchstone.Map) return false;
			if (from.Region is NoHousingRegion) return false;
			if (from.Region is GuardedRegion) return false;
			if (from.Region is DungeonRegion) return false;
			if (from.Region is TownRegion) return false;
			return true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if (m_ranchstone.Deleted || m_ranchstone == null) 
			{
				this.Delete();
				return;
			}
			double distance = from.GetDistanceToSqrt(m_ranchstone);

			if ((this.IsChildOf(from.Backpack)) && (distance < 5))
			{
				if (m_ranchstone.Owner == from)
				{
					from.SendMessage ( "Enter new ranch name." ); 
					from.Prompt = new RenamePrompt( m_ranchstone );
					return;
				}
			}

			if (from != Parent) 
			{
				from.SendMessage("You must equip that in order to use it.");
				return;
			}
			
			if (((distance < 10) && NotForbiddenRegions(from)) || (from.AccessLevel >= AccessLevel.GameMaster))
			{
				from.SendMessage("Target the fence.");
				from.Target = new FencingTarget(this);
			}
			else if (distance > 20)
			{
				from.SendMessage("You are WELL out of range!");
				return;
			}
			else
			{
				from.SendMessage("You are out of range of your Ranch Stone.");
				return;
			}
		}

		public FencingHammer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write(m_ranchstone);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_ranchstone = (RanchStone)reader.ReadItem();
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
			}
		}

		private class FencingTarget : Target
		{
			private FencingHammer t_fh;

			public FencingTarget(FencingHammer fh) : base( 1, false, TargetFlags.None )
			{
				t_fh = fh;
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is BaseFence)
				{
					BaseFence fg = (BaseFence) targ;

					if ( fg.Movable == true)
					{
						from.SendMessage("You set the fence into the ground.");
						from.PlaySound( 0x136 );
						fg.Movable = false;
						fg.ranchstone = t_fh.ranchstone;
					}
					else
					{
						from.SendMessage("You knock it free from the ground.");
						from.PlaySound( 0x13F );
						fg.Movable = true;
						fg.ranchstone = null;
					}
					return;
				}
				else if (targ is FenceGate)
				{
					FenceGate fg = (FenceGate) targ;

					if ( fg.Movable == true)
					{
						from.SendMessage("You set the fence into the ground.");
						from.PlaySound( 0x136 );
						fg.Movable = false;
						fg.ranchstone = t_fh.ranchstone;
					}
					else
					{
						from.SendMessage("You knock it free from the ground.");
						from.PlaySound( 0x13F );
						fg.Movable = true;
						fg.ranchstone = null;
					}
					return;
				}
				else if (targ is RanchStone)
				{
					RanchStone rs = (RanchStone) targ;
					if (rs.Owner == from)
					{
						from.SendMessage ( "Enter new ranch name." ); 
						from.Prompt = new RenamePrompt( rs );
					}
					else
						from.SendMessage("You don't own that!");
				}
				else
				{
					from.SendMessage ("You can't do that.");
				}
			}
		}
	}
}