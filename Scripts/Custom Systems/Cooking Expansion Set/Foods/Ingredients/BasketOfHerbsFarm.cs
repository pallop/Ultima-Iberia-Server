using System;
using Server.Network;

namespace Server.Items
{
	public class BasketOfHerbsFarm : Item
	{
		[Constructable]
		public BasketOfHerbsFarm() : base( 0x194F )
		{
			Weight = 2.0;
			Name = "Basket of Herbs";
		}

        public BasketOfHerbsFarm(Serial serial) : base(serial) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}