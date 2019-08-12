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
   	public class RarePetDye : Item 
   	{     
      	[Constructable] 
      	public RarePetDye() : base( 0xE2B ) 
      	{ 
         	Weight = 1.0;  
         	Movable = true;
         	Hue = Utility.RandomMinMax( 1150, 1175 );
         	Name="a bottle of rare pet dye"; 
		} 

		public RarePetDye( Serial serial ) : base( serial ) 
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
				from.SendMessage( "Which pet would you like to use this dye on?" ); 
				from.Target = new PetDyeTarget( this );
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

  		private class PetDyeTarget : Target 
		{ 
			private Mobile m_Owner; 
			private Item m_Dye; 

			public PetDyeTarget( Item dye ) : base ( 10, false, TargetFlags.None ) 
			{ 
				m_Dye = dye; 
			} 
          
			protected override void OnTarget( Mobile from, object target ) 
			{ 
				if ( target == from ) 
					from.SendMessage( "You cant dye yourself!" );

				else if ( target is PlayerMobile )
					from.SendMessage( "You cannon dye a player with this!" );

				else if ( target is Item )
					from.SendMessage( "You can only dye creatures with this." );

				else if ( target is BaseCreature ) 
				{ 
					BaseCreature c = (BaseCreature)target;
					
					if ( c.BodyValue == 400 || c.BodyValue == 401 && c.Controlled == false )
					{
						from.SendMessage( "You cannont dye a human!" );
					}
					else if ( c.ControlMaster != from && c.Controlled == false )
					{
						from.SendMessage( "You can only dye a pet you own." );
					}
					else if ( c.Summoned )
					{
						from.SendMessage( "You cannot use this on summoned creatures." );
					}
					else if ( c.Controlled == true && c.ControlMaster == from)
					{
						c.Hue = m_Dye.Hue;
						from.SendMessage( 53, "You dye your pet." );
						from.PlaySound( 0x23E );
						m_Dye.Delete();
					}
				}
			} 
		} 
   	} 
} 
