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
   	public class PetBleach : Item 
   	{     
      	[Constructable] 
      	public PetBleach() : base( 0xE2B ) 
      	{ 
         	Weight = 1.0;  
         	Movable = true;
         	Name="a bottle of pet bleach"; 
		} 

		public PetBleach( Serial serial ) : base( serial ) 
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
				from.SendMessage( "What pet do you want to bleach." ); 
				from.Target = new PetBleachTarget( this );
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

  		private class PetBleachTarget : Target 
		{ 
			private Mobile m_Owner; 
			private Item m_Dye; 

			public PetBleachTarget( Item dye ) : base ( 10, false, TargetFlags.None ) 
			{ 
				m_Dye = dye; 
			} 
          
			protected override void OnTarget( Mobile from, object target ) 
			{ 
				if ( target == from ) 
					from.SendMessage( "You would not like the outcome of this!" );

				else if ( target is PlayerMobile )
					from.SendMessage( "You cannot bleach a player!" );

				else if ( target is Item )
					from.SendMessage( "You cannon bleach an item!" );

				else if ( target is BaseCreature ) 
				{ 
					BaseCreature c = (BaseCreature)target;
					
					if ( c.BodyValue == 400 || c.BodyValue == 401 && c.Controlled == false )
					{
						from.SendMessage( "You cannon bleach humans." );
					}
					else if ( c.ControlMaster != from && c.Controlled == false )
					{
						from.SendMessage( "You can only bleach pets you own!" );
					}
					else if ( c.Summoned )
					{
						from.SendMessage( "You cannot use this on summoned creatures." );
					}
					else if ( c.Controlled == true && c.ControlMaster == from)
					{
						c.Hue = 0;
						from.SendMessage( "You have washed away all pigment from the animal." );
						from.PlaySound( 0x23E );
						m_Dye.Delete();
					}
				}
			} 
		} 
   	} 
} 
