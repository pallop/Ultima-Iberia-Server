using System;

namespace Server.Items
{
	public class BlackRose2 : Item
	{
		[Constructable]
		public BlackRose2() : this( 1 ){}

		[Constructable]
		public BlackRose2( int amount ) : base( 0x234B )
		{
			Name = "Black Rose";
			Hue = 2393;
		}

		public BlackRose2( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
} 