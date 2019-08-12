using System;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Factions;
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using CustomsFramework.Systems.ShrinkSystem;

namespace Server.Items
{
	public class SellPet : ContextMenuEntry
	{
		private Mobile m_From;
		private ShrinkItem m_ShrinkItem;

		public SellPet( Mobile from, ShrinkItem shrink ) : base( 6104, 5 )
		{
			m_From = from;
			m_ShrinkItem = shrink;
		}

		public override void OnClick()
		{
			if ( m_From == m_ShrinkItem.Owner )
				m_From.SendGump( new ShrinkItem.SellConfirmGump( m_From, m_ShrinkItem ) );
		}
	}
	
    public class ShrinkItem : Item
    {
    	private Mobile m_Creature;
    	private Mobile m_Owner;
    	private bool m_ForSale;
    	
    	public Mobile Creature
		{
			get{ return m_Creature; }
			set
			{ 
				m_Creature = value;
				
				this.ItemID = ItemID = ShrinkTable.Lookup( m_Creature );
			}
    	}
    	
    	public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
    	}
    	
    	public bool ForSale
		{
			get{ return m_ForSale; }
			set{ m_ForSale = value; }
    	}
    	
        public ShrinkItem() : base( 0x14EF )
        {
        	Name = "a shrunken pet";
        	
        	LootType = LootType.Blessed;
        	Weight = 25.0;
        }

        public override void OnDoubleClick( Mobile from )
        {
        	BaseCreature bc = (BaseCreature)Creature;
        	
        	if ( this.Owner != from && this.ForSale )
        		this.Owner = from;
        	
        	if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
        	else if ( Creature == null )
        	{
        		this.Delete();
        		from.SendMessage( "Nothing was linked to this shrink item. Contact a game master if you feel you got this message in error." );
        	}
        	else if ( !ShrinkSystemCore.Core.Enabled )
        	{
        		from.SendMessage( "The shrink system is currently offline! Contact a game master for details." );
        	}
        	else if ( this.Owner != from )
        	{
        		from.SendMessage( "You are not the owner of this pet you cannot claim it." );
        	}
        	else if ( !bc.CheckControlChance( from ) )
        	{
        		from.SendMessage( "You lack the required skill to control this pet." );
        	}
        	else if ( ( bc.ControlSlots + from.Followers ) > from.FollowersMax )
        	{
        		from.SendMessage( "You have to many followers to claim this pet." );
        	}
        	else if ( Server.Spells.SpellHelper.CheckCombat( from ) )
        	{
        		from.SendMessage( "You cannot claim this pet while in combat." );
        	}
        	else
        	{
        		IEntity p1a = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z ), from.Map );
				IEntity p2a = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z + 50 ), from.Map );

				Effects.SendMovingParticles( p1a, p2a, ShrinkTable.Lookup( Creature ), 1, 0, true, false, 0, 3, 1153, 1, 0, EffectLayer.Head, 0x100 );
				from.PlaySound( 492 );

				bc.ControlMaster = from;
				bc.ControlTarget = from;
				bc.ControlOrder = OrderType.Follow;

				if ( bc.IsBonded == true && this.ForSale == true )
					bc.IsBonded = false;

				bc.Direction = Direction.South;
				bc.MoveToWorld( from.Location, from.Map );
				from.SendMessage( "You unshrink the animal." );

				this.Delete();
        	}
        }
        
        public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( from.Alive )
			{
				if ( this.ForSale == false && this.Owner == from )
				{
					list.Add( new SellPet( from, this ) );
				}
			}
		}
        
        public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			
			if ( this.Creature != null )
			{
				Type type = this.Creature.GetType();
				string s = type.Name;

				int capsbreak = s.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);

				if( capsbreak > -1 )
				{
					string secondhalf = s.Substring( capsbreak );
 					string firsthalf = s.Substring(0, capsbreak );

 					if ( this.Creature != null )
						list.Add( 1060663, "Name\t{0} Breed: {1} {2}", this.Creature.Name, firsthalf, secondhalf );
				}
				else
				{
					if ( this.Creature != null )
						list.Add( 1060663, "Name\t{0} Breed: {1}", this.Creature.Name, type.Name );
				}
				
				if ( this.Creature.Female )
					list.Add( 3000119 );
				else
					list.Add( 3000118 );
				
				/*if ( ms.Level != 0 )
					list.Add(1060659, "Level\t{0}", ms.Level); // ~1_val~: ~2_val~*/
			}
		}
        
        public class SellConfirmGump : Gump
        {
            private readonly Mobile m_Mobile;
            private readonly ShrinkItem m_ShrinkItem;

            public SellConfirmGump( Mobile mobile, ShrinkItem pet ) : base( 25, 50 )
            {
                this.m_Mobile = mobile;
                this.m_ShrinkItem = pet;

                this.AddPage(0);

                this.AddBackground(25, 10, 420, 200, 5054);

                this.AddImageTiled(33, 20, 401, 181, 2624);
                this.AddAlphaRegion(33, 20, 401, 181);

                this.AddHtml(40, 48, 387, 100, "<CENTER>Warning!!!<BR>Your about to convert this pet into an animal sale contract. This will remove any bonded status you have with the animal and make it able to transfer to a new owner and be sold. Once you do this it cannont be undone.", true, true);

                this.AddHtml(125, 148, 200, 20, "<BASEFONT COLOR=#FFFFFF>Do you wish to proceed?</BASEFONT>", false, false );

                this.AddButton(100, 172, 4005, 4007, 1, GumpButtonType.Reply, 0);
                this.AddHtmlLocalized(135, 172, 120, 20, 1046362, 0xFFFFFF, false, false); // Yes

                this.AddButton(275, 172, 4005, 4007, 0, GumpButtonType.Reply, 0);
                this.AddHtmlLocalized(310, 172, 120, 20, 1046363, 0xFFFFFF, false, false); // No
            }

            public override void OnResponse(NetState state, RelayInfo info)
            {
                if (info.ButtonID == 1)
                {
                    m_ShrinkItem.Name = "an animal contract of sale";
                    m_ShrinkItem.ItemID = 0x14EF;
                    m_ShrinkItem.ForSale = true;
                    m_Mobile.SendMessage( "You have marked this pet for sale. It can now be sold on a vendor." );
                }
            }
        }

        public ShrinkItem(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            
            writer.Write((Mobile)m_Creature);
            writer.Write((Mobile)m_Owner);
            writer.Write((bool)m_ForSale);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch ( version )
            {
                case 0:
                {
            		m_Creature = reader.ReadMobile();
            		m_Owner = reader.ReadMobile();
            		m_ForSale = reader.ReadBool();
					break;
                }
            }
        }
    }
}