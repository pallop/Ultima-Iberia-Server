using System;
using System.Collections;
using Server.Network;
namespace Server.Items
{
    public class FriedDuckEggs : Food
	{
		[Constructable]
		public FriedDuckEggs() : this( 1 ) { }
		[Constructable]
		public FriedDuckEggs( int amount ) : base( amount, 0x9B6 )
		{
            this.Name = "Fried Duck Eggs";
			this.Weight = 1.0;
			this.FillFactor = 4;
		}
        public FriedDuckEggs(Serial serial) : base(serial) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}