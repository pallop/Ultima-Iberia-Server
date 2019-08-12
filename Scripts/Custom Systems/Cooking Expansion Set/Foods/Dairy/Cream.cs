using System;
using Server.Network;

namespace Server.Items
{
	public class Cream : Item
	{
		[Constructable]
		public Cream() : base( 0x1F8C )
		{
		      	Weight = 1.0;
			Stackable = true;
			Name = "Cream";
			Hue = 0x0;
		}

		public Cream( Serial serial ) : base( serial )
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