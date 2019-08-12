using System;
using System.Collections;
using Server.Network;
namespace Server.Items
{
	public class FriedChickenEggs : Food
	{
		[Constructable]
		public FriedChickenEggs() : this( 1 ) { }
		[Constructable]
		public FriedChickenEggs( int amount ) : base( amount, 0x9B6 )
		{
            Name = "Fried Chicken Eggs";
			Weight = 1.0;
			FillFactor = 4;
		}
        public FriedChickenEggs(Serial serial) : base(serial) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}