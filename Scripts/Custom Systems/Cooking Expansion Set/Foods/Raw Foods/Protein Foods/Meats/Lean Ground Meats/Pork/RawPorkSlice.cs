using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class RawPorkSlice : Food
	{
		[Constructable]
		public RawPorkSlice() : this( 1 )
		{
		}

		[Constructable]
		public RawPorkSlice( int amount ) : base( 0x979 )
		{
			Name = "raw slice of pork";
			Weight = 1.0;
			Stackable = true;
			Amount = amount;
			Hue = 336;
            Raw = true;
		}

		public RawPorkSlice( Serial serial ) : base( serial )
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