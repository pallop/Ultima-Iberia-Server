using System;
using Server.Network;

namespace Server.Items
{
	public class Butter : Food
	{
		[Constructable]
		public Butter() : this( 1 )
		{
		}
		
		[Constructable]
		public Butter(int amount) : base( amount, 0x1044 )
		{
			Weight = 0.5;
			Amount = amount;
			Stackable = true;
			Name = "Butter";
			Hue = 55;
			FillFactor = 0;
		}

		public Butter( Serial serial ) : base( serial )
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