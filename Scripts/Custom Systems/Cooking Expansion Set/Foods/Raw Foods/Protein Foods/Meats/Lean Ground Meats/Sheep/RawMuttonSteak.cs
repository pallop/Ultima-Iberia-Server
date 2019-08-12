using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class RawMuttonSteak : Food
	{
		[Constructable]
		public RawMuttonSteak() : this( 1 )
		{
		}

		[Constructable]
        public RawMuttonSteak(int amount)
            : base(amount, 0x9F1)
		{
			Weight = 1.0;
			Stackable = true;
			Amount = amount;
			Name = "raw mutton steak";
            Raw = true;
		}

		public RawMuttonSteak( Serial serial ) : base( serial )
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