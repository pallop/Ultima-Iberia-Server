using System;
using System.Collections;
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class PennyCandy : Food
	{
		[Constructable]
		public PennyCandy() : this( 1 ){}

		[Constructable]
		public PennyCandy( int amount ) : base( amount, 0x3BC7 )
		{
			Name = "Candy";
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public PennyCandy( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}