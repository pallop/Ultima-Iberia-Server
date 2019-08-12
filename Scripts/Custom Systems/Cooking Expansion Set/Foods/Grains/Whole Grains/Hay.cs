using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class Hay : Item
	{
		[Constructable]
		public Hay() : this( 1 ) { }

		[Constructable]
		public Hay( int amount ) : base( 0xF36 )
		{
            this.Name = "Hay Sheath";
            this.Weight = 4.0;
            this.Stackable = true;
            this.Amount = amount;
		}

		public Hay( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}