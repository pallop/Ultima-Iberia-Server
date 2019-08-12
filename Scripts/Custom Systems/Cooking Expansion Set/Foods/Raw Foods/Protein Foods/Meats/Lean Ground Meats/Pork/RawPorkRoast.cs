using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class RawPorkRoast : Food
	{
		[Constructable]
		public RawPorkRoast() : this( 1 )
		{
		}

		[Constructable]
        public RawPorkRoast(int amount)
            : base(amount, 0x9C9)
		{
			Amount = amount;
			Stackable = true;
			Weight = 1.0;
			Hue = 1194;
			Name = "raw pork roast";
            Raw = true;
		}

		public RawPorkRoast( Serial serial ) : base( serial )
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