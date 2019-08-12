using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class UnbakedChickenPotPie : Food
	{
		[Constructable]
        public UnbakedChickenPotPie()
            : base(0x1042)
		{
			this.Name = "unbaked chicken pot pie";
		}

		public UnbakedChickenPotPie( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}