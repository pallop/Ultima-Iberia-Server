using System;
using Server;

namespace Server.Items
{
	public class PotOfMoltenGold : Item
	{

		[Constructable]
		public PotOfMoltenGold() : base( 15290 )
		{
			Name = "Pot Of Molten Gold";
			Weight = 3.0;
			Hue = 0;
			Movable = true;
		}

		public PotOfMoltenGold( Serial serial ) : base( serial )
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