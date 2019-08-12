using System;
using Server.Network;
namespace Server.Items
{
	public class ChocSunflowerSeeds : Food
	{
		[Constructable]
		public ChocSunflowerSeeds() : this( 1 ) { }
		[Constructable]
		public ChocSunflowerSeeds( int amount ) : base( amount, 0x9B4 )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Hue = 0x41B;
			this.Name = "Chocolate Sunflower Seeds";
		}
		public ChocSunflowerSeeds( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}