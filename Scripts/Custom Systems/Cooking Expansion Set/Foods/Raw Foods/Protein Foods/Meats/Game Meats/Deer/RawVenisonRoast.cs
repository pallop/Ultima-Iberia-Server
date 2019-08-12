using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class RawVenisonRoast : Food
	{
		[Constructable]
		public RawVenisonRoast() : this( 1 )
		{
		}

		[Constructable]
        public RawVenisonRoast(int amount)
            : base( amount, 0x9C9)
		{
			Name = "raw venison roast";
			Stackable = true;
			Weight = 1.0;
			Hue = 1194;
			Amount = amount;
            Raw = true;
		}

		public RawVenisonRoast( Serial serial ) : base( serial )
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