using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class RawGroundBeef : Food
	{
		[Constructable]
		public RawGroundBeef() : this( 1 )
		{
		}

		[Constructable]
        public RawGroundBeef(int amount)
            : base(amount, 0x1727)
		{
			Weight = 1.0;
			Stackable = true;
			Amount = amount;
			Hue = 2117;
			Name = "raw ground beef";
            Raw = true;
		}

		public RawGroundBeef( Serial serial ) : base( serial )
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