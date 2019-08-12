using System;
using Server;

namespace Server.Items
{
	public class SpringWater : Item
	{
		[Constructable]
		public SpringWater() : this( 1 )
		{
		}

		[Constructable]
		public SpringWater( int amount ) : base( 0xE24 )
		{
			Stackable = true;
			Weight = 0.0;
			Amount = amount;
			Name = "spring water";
			Hue = 0x47F;
		}

		public SpringWater( Serial serial ) : base( serial )
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
}