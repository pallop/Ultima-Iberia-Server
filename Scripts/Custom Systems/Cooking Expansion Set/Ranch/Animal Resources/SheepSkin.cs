using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0x1079, 0x1078 )]
	public class SheepHide : Item, IScissorable
	{
		public override int Hue { get { return 0x41C; } }

		[Constructable]
		public SheepHide() : this( 1 )
		{
			Hue = 0x41C;
		}

		[Constructable]
        public SheepHide(int amount) : base(0x1079)
		{
			Stackable = true;
			Amount = amount;
			Name = "sheep hide";
		}

		public SheepHide( Serial serial ) : base( serial )
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

		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) ) return false;
			base.ScissorHelper( from, new SheepSkin(), 1 );
			return true;
		}
	}

	[FlipableAttribute( 0x1081, 0x1082 )]
	public class SheepSkin : Item
	{
		public override int Hue { get { return 0x41C; } }

		[Constructable]
		public SheepSkin() : this( 1 )
		{
			Hue = 0x41C;
		}

		[Constructable]
		public SheepSkin( int amount ) : base( 0x1081 )
		{
			Stackable = true;
			Amount = amount;
			Name = "sheep skin";
		}

		public SheepSkin ( Serial serial ) : base( serial )
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
	}
}
