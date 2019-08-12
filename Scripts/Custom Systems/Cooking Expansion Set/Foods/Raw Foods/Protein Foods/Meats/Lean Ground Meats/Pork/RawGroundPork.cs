using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class RawGroundPork : Food
	{
		[Constructable]
		public RawGroundPork() : this( 1 )
		{
		}

		[Constructable]
		public RawGroundPork( int amount ) : base( 0x1727 )
		{
			//ItemID = 5927;
			Weight = 1.0;
			Stackable = true;
			Amount = amount;
			Hue = 2117;
			Name = "raw ground pork";
            Raw = true;
		}

		public RawGroundPork( Serial serial ) : base( serial )
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