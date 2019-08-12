using System;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class Sugarcane : Item
	{
		[Constructable]
		public Sugarcane() : this( 1 )
		{
		}

		[Constructable]
		public Sugarcane( int amount ) : base( 0x1A9D )
		{
            Amount = amount;
            Weight = 0.1;
            Hue = 0x23F;
            Stackable = true;
            Name = "Sugarcane";
		}

		public Sugarcane( Serial serial ) : base( serial )
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