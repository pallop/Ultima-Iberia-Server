using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class VenisonJerky : Food
	{
		[Constructable]
		public VenisonJerky() : this( 1 )
		{
		}

		[Constructable]
		public VenisonJerky( int amount ) : base( amount, 0x979 )
		{
            Name = "Venison jerky";
			Weight = 0.2;
			FillFactor = 1;
            Stackable = true;
            Amount = amount;
		}

		public VenisonJerky( Serial serial ) : base( serial )
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