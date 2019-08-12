using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class VenisonRoastSlices : Food
	{
		[Constructable]
		public VenisonRoastSlices() : this( 1 )
		{
		}

		[Constructable]
		public VenisonRoastSlices( int amount ) : base( amount, 0x1E1F )
		{
			this.Weight = 0.2;
			this.FillFactor = 1;
			Name = "venison roast slices";
			Hue = 1831;
		}

		public VenisonRoastSlices( Serial serial ) : base( serial )
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