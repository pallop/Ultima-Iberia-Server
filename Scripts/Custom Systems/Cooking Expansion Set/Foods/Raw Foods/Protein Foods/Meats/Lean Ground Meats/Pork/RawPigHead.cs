using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 7822, 7823 )]
	public class RawPigHead : Food
	{
		[Constructable]
		public RawPigHead() : this( 1 )
		{
		}

		[Constructable]
        public RawPigHead(int amount)
            : base(amount, 7822)
		{
			Name = "pig's head";
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
            Raw = true;
		}

		public RawPigHead( Serial serial ) : base( serial )
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