using Server.Targeting; 
using System; 
using Server; 
using Server.Gumps; 
using Server.Network; 
using Server.Menus; 
using Server.Menus.Questions; 
using Server.Mobiles; 
using System.Collections; 

namespace Server.Items 
{ 
   	public class PetControlSlotDeed : Item 
   	{     
      	[Constructable] 
      	public PetControlSlotDeed() : base( 0x14F0 )
      	{ 
         	Weight = 1.0;  
         	Movable = true;
         	Name="a pet control deed";
         	LootType = LootType.Blessed;
		} 

		public PetControlSlotDeed( Serial serial ) : base( serial ) 
		{ 
		}
		
		public override void OnDoubleClick( Mobile from ) 
		{ 
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if( from.InRange( this.GetWorldLocation(), 1 ) ) 
			{
				from.SendMessage( "Which animal would you like to use this on?" );
				from.SendMessage( 42, "WARNING: Using this deed on your pet will reduce its control slots by 1 but increase its skill needed to control by 10%." ); 
				from.Target = new ControSlotTarget( this );
			} 
			else 
			{ 
				from.SendLocalizedMessage( 500446 ); // That is too far away. 
			}
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 

  		private class ControSlotTarget : Target 
		{ 
			private Mobile m_Owner; 
			private Item m_Deed; 

			public ControSlotTarget( Item deed ) : base ( 10, false, TargetFlags.None ) 
			{ 
				m_Deed = deed; 
			} 
          
			protected override void OnTarget( Mobile from, object target ) 
			{ 
				if ( target == from ) 
					from.SendMessage( "This can only be used on pets." );

				else if ( target is PlayerMobile )
					from.SendMessage( "You cannont use this on them." );

				else if ( target is Item )
					from.SendMessage( "This does not work on that." );

				else if ( target is BaseCreature ) 
				{ 
					BaseCreature c = (BaseCreature)target;
					
					if ( c.ControlMaster != from && c.Controlled == false )
					{
						from.SendMessage( "You may only use this on a pet you own." );
					}
					else if ( c.Summoned )
					{
						from.SendMessage( "You cannot use this on summoned creatures." );
					}
					else if ( c.Controlled == true && c.ControlMaster == from)
					{
						if ( c.ControlSlots > 1 )
						{
							double increase = ( 0.1 * c.MinTameSkill );
							
							if ( from.Skills[SkillName.AnimalTaming].Value < ( increase + c.MinTameSkill ) )
							{
								from.SendMessage( "You would not be able to control this pet after using this deed on it." );
							}
							else
							{
								m_Deed.Delete();
								c.ControlSlots -= 1;
								c.MinTameSkill += increase;
								from.SendMessage( "You decrease this pets control slots by 1 and increase its skill needed to control by {0} points.", increase.ToString() );
								m_Deed.Delete();
							}
						}
						else
						{
							from.SendMessage( "You cannon reduce this pets control slots any further." );
						}
					}
				}
			} 
		} 
   	} 
} 
