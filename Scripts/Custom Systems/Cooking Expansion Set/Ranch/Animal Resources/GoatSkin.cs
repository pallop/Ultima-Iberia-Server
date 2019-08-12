using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0x1079, 0x1078 )]
	public class GoatHide : Item, IScissorable
	{
		public override int Hue { get { return 0x41C; } }

		[Constructable]
		public GoatHide() : this( 1 )
		{
		}

		[Constructable]
		public GoatHide( int amount ) : base( 0x1079 )
		{
			Stackable = true;
			Amount = amount;
			Name = "goat hide";
            Hue = 0x41C;
            Stackable = true;
            Amount = amount;
		}

		public GoatHide( Serial serial ) : base( serial )
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
			base.ScissorHelper( from, new GoatSkin(), 1 );
			return true;
		}
	}

	[FlipableAttribute( 0x1081, 0x1082 )]
	public class GoatSkin : Item
	{
		public override int Hue { get { return 0x41C; } }

		[Constructable]
		public GoatSkin() : this( 1 )
		{
			Hue = 0x41C;
		}

		[Constructable]
		public GoatSkin( int amount ) : base( 0x1081 )
		{
			Stackable = true;
			Amount = amount;
			Name = "goat skin";
		}

		public GoatSkin ( Serial serial ) : base( serial )
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
