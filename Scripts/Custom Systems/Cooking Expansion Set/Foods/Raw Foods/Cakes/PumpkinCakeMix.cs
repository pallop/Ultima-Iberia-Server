using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class PumpkinCakeMix : Food
	{
		public override int LabelNumber{ get{ return 1041002; } }

		[Constructable]
        public PumpkinCakeMix(int amount)
            : base(amount, 0x103F)
		{
			this.Name = "pumpkin cake mix";
			this.Hue = 243;
		}

		public PumpkinCakeMix( Serial serial ) : base( serial )
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