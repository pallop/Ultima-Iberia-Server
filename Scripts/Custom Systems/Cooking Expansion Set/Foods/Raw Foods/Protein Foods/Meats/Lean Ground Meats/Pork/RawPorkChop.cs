using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class RawPorkChop : CookableFood, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			if ( !Movable )
				return;

			base.ScissorHelper( from, new RawPorkSlice(), 5 );

			from.SendMessage( "You slice the pork chop into thin strips." );
		}
		
		[Constructable]
		public RawPorkChop() : this( 1 )
		{
		}

		[Constructable]
        public RawPorkChop(int amount)
            : base(amount, 0x9F1)
		{
			Weight = 1.0;
			Stackable = true;
			Amount = amount;
			Name = "raw pork chop";
		}

		public RawPorkChop( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
		
		public override Food Cook()
		{
			return new PorkChop();
		}
	}
}