using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class TimedItem : Item
	{
		[Constructable]
		public TimedItem( double delay, int itemID )
			: base( itemID )
		{
			new InternalTimer( this, delay ).Start();
		}

		public TimedItem( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			Timer.DelayCall( TimeSpan.FromSeconds( 30.0 ), new TimerCallback( Delete ) );
		}

		private class InternalTimer : Timer
		{
			private TimedItem m_Item;

			public InternalTimer( TimedItem item, double delay )
				: base( TimeSpan.FromSeconds( delay ) )
			{
				m_Item = item;

				Priority = ComputePriority( this.Delay );
			}

			protected override void OnTick()
			{
				if( m_Item != null && !m_Item.Deleted )
					m_Item.Delete();

				this.Stop();
			}
		}
	}
}