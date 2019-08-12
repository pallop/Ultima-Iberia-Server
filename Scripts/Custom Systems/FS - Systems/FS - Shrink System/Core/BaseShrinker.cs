using System;
using Server.Mobiles;
using Server.Targeting;
using CustomsFramework.Systems.ShrinkSystem;

namespace Server.Items
{
    public abstract class BaseShrinker : Item
    {
    	private int m_Uses;
    	
		[CommandProperty( AccessLevel.GameMaster )]
		public int Uses
		{
			get{ return m_Uses; }
			set{ m_Uses = value; InvalidateProperties(); }
		}
    	
        public BaseShrinker( int itemID ) : this( itemID, 1 )
        {
        }

        public BaseShrinker( int itemID, int amount ) : base( itemID )
        {
            this.Stackable = false;
            this.m_Uses = amount;
        }
        
        #region OnDoubleClick
		public override void OnDoubleClick( Mobile from ) 
		{ 
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if ( !ShrinkSystemCore.Core.Enabled )
			{
				from.SendMessage( "The shrink system is currently offline! Contact a game master for details." );
			}
			else
			{
				from.Target = new ShrinkTarget( from, this );
				from.SendMessage( "What animal would you like to shrink?" );
			}
		}
		#endregion
		
		#region ShrinkTarget
		private class ShrinkTarget : Target 
      	{ 
			private Mobile m_Owner; 
			private BaseShrinker m_Shrinker; 

			public ShrinkTarget( Mobile owner, BaseShrinker shrinker ) : base ( 10, false, TargetFlags.None ) 
			{
				m_Owner = owner;
				m_Shrinker = shrinker;
			} 
          
			protected override void OnTarget( Mobile from, object target ) 
			{
				if ( Server.Spells.SpellHelper.CheckCombat( from ) )
					from.SendMessage( "You cannot shrink your pet while your in combat." );
				else if ( target is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)target;
					
					if ( Server.Spells.SpellHelper.CheckCombat( bc ) )
						from.SendMessage( "You cannont shrink your pet while its in combat." );
					else if ( bc.Summoned )
						from.SendMessage( "You cannont shrink a summoned pet." );
					else if ( !from.InRange( bc, 5 ) )
						from.SendMessage( "You need to be closer to shrink that pet." );
					else if ( bc.BodyMod != 0 )
						from.SendMessage( "You cannont shrink your pet while its polymorphed." );
					else if ( bc.IsDeadPet )
						from.SendMessage( "That pet is dead." );
					else if ( ( bc is PackLlama || bc is PackHorse || bc is Beetle ) && ( bc.Backpack != null && bc.Backpack.Items.Count > 0 ) )
						from.SendMessage( "You must unload your pets pack before you shrink it." );
					else if ( !bc.Controlled )
						from.SendMessage( "You have to tame the creature first before you can shrink it." );
					else if ( bc.Controlled && bc.ControlMaster == from )
					{
						#region Creation
						ShrinkItem si = new ShrinkItem();
						si.Creature = bc;
						si.Owner = from;
						
						if ( bc.Hue != 0 )
							si.Hue = bc.Hue;
						#endregion

						#region Effect
						IEntity p1 = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z ), from.Map );
						IEntity p2 = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z + 50 ), from.Map );
						Effects.SendMovingParticles( p2, p1, ShrinkTable.Lookup( bc ), 1, 0, true, false, 0, 3, 1153, 1, 0, EffectLayer.Head, 0x100 );
						from.PlaySound( 492 );
						#endregion 

						#region Internalize
						bc.Controlled = true; 
						bc.ControlMaster = null;
						bc.Internalize();
						bc.OwnerAbandonTime = DateTime.MinValue;
						bc.IsStabled = true;
						#endregion
						
						#region Uses
						m_Shrinker.Uses -= 1;
						
						if ( m_Shrinker.Uses ==  0 )
							m_Shrinker.Delete();
						#endregion
				
						from.AddToBackpack( si );
					}
					else
						from.SendMessage( "Thats not your pet you may not shrink it." );
				}
				else
					from.SendMessage( "You cannont shrink that." );
			}
		}
		#endregion
        
		#region AddNameProperties
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			if ( m_Uses > 0 )
				list.Add( 1060584, m_Uses.ToString() ); // uses remaining: ~1_val~
		}
		#endregion

		#region Serialize / Deserialize
        public BaseShrinker(Serial serial) : base(serial)
        {
        }
        
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            
            writer.Write((int)m_Uses);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            
            m_Uses = reader.ReadInt();
        }
        #endregion
    }
}