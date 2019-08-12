using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0x1079, 0x1078 )]
	public class DeerHide : Item, IScissorable
	{
		public override int Hue { get { return 0x41C; } }

		[Constructable]
		public DeerHide() : this( 1 )
		{
			Hue = 0x41C;
		}

		[Constructable]
		public DeerHide( int amount ) : base( 0x1079 )
		{
			Stackable = true;
			Amount = amount;
			Name = "deer hide";
		}

		public DeerHide( Serial serial ) : base( serial )
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
			base.ScissorHelper( from, new DeerSkin(), 1 );
			return true;
		}
	}

	[FlipableAttribute( 0x1081, 0x1082 )]
	public class DeerSkin : Item
	{
		public override int Hue { get { return 0x41C; } }

		[Constructable]
		public DeerSkin() : this( 1 )
		{
			Hue = 0x41C;
		}

		[Constructable]
		public DeerSkin( int amount ) : base( 0x1081 )
		{
			Stackable = true;
			Amount = amount;
			Name = "deer skin";
		}

		public DeerSkin ( Serial serial ) : base( serial )
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
