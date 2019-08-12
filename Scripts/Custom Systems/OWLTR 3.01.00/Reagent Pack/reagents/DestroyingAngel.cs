using System;
using Server;

namespace Server.Items
{
	public class DestroyingAngel : Item
	{
		[Constructable]
		public DestroyingAngel() : this( 1 )
		{
		}

		[Constructable]
		public DestroyingAngel( int amount ) : base( 0xE1F )
		{
			Stackable = true;
			Weight = 0.0;
			Amount = amount;
			Name = "destroying angel";
			Hue = 0x290;
		}

		public DestroyingAngel( Serial serial ) : base( serial )
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