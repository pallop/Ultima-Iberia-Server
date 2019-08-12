using System;

namespace Server.Items
{
	public class WhiteRose2 : Item
	{
		[Constructable]
		public WhiteRose2() : this( 1 ){}

		[Constructable]
		public WhiteRose2( int amount ) : base( 0x234B )
		{
			Name = "White Rose";
			Hue = 1953;
		}

		public WhiteRose2( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
} 