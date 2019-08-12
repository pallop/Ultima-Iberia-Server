using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class RawBeefRibs : Food
	{
		[Constructable]
		public RawBeefRibs() : this( 1 )
		{
		}

		[Constructable]
        public RawBeefRibs(int amount)
            : base(amount, 0x9F1)
		{
			Weight = 1.0;
			Stackable = true;
			Amount = amount;
			Name = "raw beef ribs";
            Raw = true;
		}

		public RawBeefRibs( Serial serial ) : base( serial )
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