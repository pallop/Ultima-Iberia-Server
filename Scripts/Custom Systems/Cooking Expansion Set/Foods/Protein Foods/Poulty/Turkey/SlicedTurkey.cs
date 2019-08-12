using System;
using System.Collections;
using Server.Network;
namespace Server.Items
{
	public class SlicedTurkey : Food
	{
		[Constructable]
		public SlicedTurkey() : this( 1 ) { }
		[Constructable]
		public SlicedTurkey( int amount ) : base( amount, 0x1E1F )
		{
			Weight = 0.2;
			FillFactor = 3;
			Name = "Sliced Turkey";
			Hue = 0x457;
			Stackable = true;
		}
		public SlicedTurkey( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}