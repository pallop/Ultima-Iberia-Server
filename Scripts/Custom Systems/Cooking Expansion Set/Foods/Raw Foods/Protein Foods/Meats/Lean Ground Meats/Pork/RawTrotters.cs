using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 7820, 7821 )]
	public class RawTrotters : Food
	{
		[Constructable]
		public RawTrotters() : this( 1 )
		{
		}

		[Constructable]
        public RawTrotters(int amount)
            : base(amount, 7820)
		{
			Name = "pig's feet";
			Stackable = true;
            Amount = amount;
			Weight = 0.5;
            Raw = true;
		}

		public RawTrotters( Serial serial ) : base( serial )
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