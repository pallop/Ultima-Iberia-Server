using System;
using Server;

namespace Server.Items
{
	public class PetrafiedWood : Item
	{
		[Constructable]
		public PetrafiedWood() : this( 1 )
		{
		}

		[Constructable]
		public PetrafiedWood( int amount ) : base( 0x97A )
		{
			Stackable = true;
			Weight = 0.0;
			Amount = amount;
			Name = "petrafied wood";
			Hue = 0x46C;
		}

		public PetrafiedWood( Serial serial ) : base( serial )
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