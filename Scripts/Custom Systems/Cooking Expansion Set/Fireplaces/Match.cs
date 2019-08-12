using System;
using Server.Mobiles;

namespace Server.Items
{
	public class MatchLight : Item
	{
        [Constructable]
        public MatchLight()
            : this(1)
        {
        }

		[Constructable]
		public MatchLight( int amount ) : base( 0x1044 )
		{
            Name = "an match light";
            Amount = amount;
            Stackable = true;
            Hue = 1150;
		}

        public MatchLight(Serial serial)
            : base(serial)
		{
		}

        public override double DefaultWeight
        {
            get
            {
                return 0.1;
            }
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