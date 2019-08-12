using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class RawHamSlices : Food
	{
		[Constructable]
		public RawHamSlices() : this( 1 )
		{
		}

		[Constructable]
        public RawHamSlices(int amount)
            : base(amount, 0x1E1F)
		{
			Name = "raw sliced ham";
			Weight = 1.0;
			Stackable = true;
			Amount = amount;
			Hue = 336;
            Raw = true;
		}

		public RawHamSlices( Serial serial ) : base( serial )
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