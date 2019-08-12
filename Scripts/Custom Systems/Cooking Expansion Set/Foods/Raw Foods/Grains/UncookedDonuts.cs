using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class UncookedDonuts : Food
	{
		[Constructable]
        public UncookedDonuts()
            : base(6867)
		{
			this.Hue = 51;
			this.Name = "uncooked donuts";
		}

		public UncookedDonuts( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}