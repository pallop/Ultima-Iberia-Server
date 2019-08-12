using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class BeefTBone : Food
	{
		[Constructable]
		public BeefTBone() : this( 1 )
		{
		}

		[Constructable]
		public BeefTBone( int amount ) : base( amount, 0x9F2 )
		{
			Weight = 1.0;
			FillFactor = 5;
			Name = "t-bone steak";
            Stackable = true;
            Amount = amount;
		}

		public BeefTBone( Serial serial ) : base( serial )
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