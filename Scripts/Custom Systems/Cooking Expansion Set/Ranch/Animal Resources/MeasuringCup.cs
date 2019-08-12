using System;
using Server.Network;

namespace Server.Items
{
	public class MeasuringCup : Item
	{
		[Constructable]
		public MeasuringCup() : base( 0x1F81 )
		{
			Weight = 1.0;
			Stackable = false;
			Name = "a measuring cup";
			Hue = 0x0;
		}

		public MeasuringCup( Serial serial ) : base( serial )
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