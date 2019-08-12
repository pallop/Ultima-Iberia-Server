using System;
using Server.Network;
namespace Server.Items
{
	public class BlackberryCobbler: Food
	{
		[Constructable]
		public BlackberryCobbler() : this( 1 ) { }
		[Constructable]
		public BlackberryCobbler( int amount ) : base( amount, 0x1041 )
		{
			Weight = 1.0;
			FillFactor = 3;
			Name = "Blackberry Cobbler";
		}
		public BlackberryCobbler( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}