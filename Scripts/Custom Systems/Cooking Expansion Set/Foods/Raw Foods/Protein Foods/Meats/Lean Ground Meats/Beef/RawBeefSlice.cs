using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class RawBeefSlice : Food
	{
		[Constructable]
		public RawBeefSlice() : this( 1 )
		{
		}

		[Constructable]
        public RawBeefSlice(int amount)
            : base(amount, 0x979)
		{
			Name = "raw slice of beef";
			Weight = 1.0;
			Stackable = true;
			Amount = amount;
			Hue = 336;
            Raw = true;
		}

		public RawBeefSlice( Serial serial ) : base( serial )
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