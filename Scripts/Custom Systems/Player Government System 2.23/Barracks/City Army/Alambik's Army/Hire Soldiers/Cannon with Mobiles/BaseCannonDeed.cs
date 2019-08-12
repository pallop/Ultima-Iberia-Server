using System;
using System.Collections;
using Server;
using Server.Targeting;

namespace Server.Items
{
	public abstract class BaseCannonDeed : Item
	{
		
		public abstract BaseCannon FireCannon{ get; }
		
		public BaseCannonDeed() : base( 0xE3D )  
		{
			Weight = 0.1;
		}
		
		public BaseCannonDeed( Serial serial ) : base( serial )
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
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
				from.Target = new InternalTarget( this );
			else
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
		}
		
		private class InternalTarget : Target
		{
			private BaseCannonDeed m_Deed;
			
			public InternalTarget( BaseCannonDeed deed ) : base( -1, true, TargetFlags.None )
			{
				m_Deed = deed;
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				IPoint3D p = targeted as IPoint3D;
				Map map = from.Map;
				
				if ( p == null || map == null || m_Deed.Deleted )
					return;
				if ( targeted is Container )
				{
					from.SendLocalizedMessage( 501978 ); // The weight is too great to combine in a container.
					return;
				}
				if ( targeted is Item )
				{
					if( ((Item)targeted).Parent != null )
					{
						from.SendLocalizedMessage( 501978 ); // The weight is too great to combine in a container.
						return;
					}
				}
				if ( m_Deed.IsChildOf( from.Backpack ) )
				{
					BaseCannon cannon = m_Deed.FireCannon;
					cannon.Owner = from;
					m_Deed.Delete();
					cannon.MoveToWorld(new Point3D(p),map);
				}
				else
				{
					from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				}
			}
		}
	}
}
