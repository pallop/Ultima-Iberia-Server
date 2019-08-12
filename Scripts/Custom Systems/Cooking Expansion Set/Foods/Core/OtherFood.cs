using System;
using Server.Network;
namespace Server.Items
{
	public class Meatballs : Food
	{
		[Constructable]
		public Meatballs() : this( 1 ) { }
		[Constructable]
		public Meatballs( int amount ) : base( amount, 0xE74 )
		{
			Weight = 1.0;
			Stackable = true;
			Hue = 0x475;
			FillFactor = 4;
			Name = "Meatballs";
		}
		public Meatballs( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class Meatloaf : Food
	{
		[Constructable]
		public Meatloaf() : this( 1 ) { }
		[Constructable]
		public Meatloaf( int amount ) : base( amount, 0xE79 )
		{
			Weight = 1.0;
			Stackable = true;
			Hue = 0x475;
			FillFactor = 5;
			Name = "Meatloaf";
		}
		public Meatloaf( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class PotatoStrings : Food
	{
		[Constructable]
		public PotatoStrings() : this( 1 ) { }
		[Constructable]
		public PotatoStrings( int amount ) : base( amount, 0x1B8D )
		{
			Weight = 1.0;
			Stackable = true;
			Hue = 0x225;
			FillFactor = 3;
			Name = "Potato Strings";
		}
		public PotatoStrings( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class Pancakes : Food
	{
		[Constructable]
		public Pancakes() : this( 1 ) { }
		[Constructable]
		public Pancakes( int amount ) : base( amount, 0x1E1F )
		{
			Weight = 1.0;
			Stackable = true;
			Hue = 0x22A;
			FillFactor = 5;
			Name = "Pancakes";
		}
		public Pancakes( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class Waffles : Food
	{
		[Constructable]
		public Waffles() : this( 1 ) { }
		[Constructable]
		public Waffles( int amount ) : base( amount, 0x1E1F )
		{
			Weight = 1.0;
			Stackable = true;
			Hue = 0x1C7;
			FillFactor = 4;
			Name = "Waffles";
		}
		public Waffles( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class GrilledHam : Food
	{
		[Constructable]
		public GrilledHam() : this( 1 ) { }
		[Constructable]
		public GrilledHam( int amount ) : base( amount, 0x1E1F )
		{
			Weight = 1.0;
			Stackable = true;
			Hue = 0x33D;
			FillFactor = 4;
			Name = "Grilled Ham";
		}
		public GrilledHam( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class Hotwings : Food
	{
		[Constructable]
		public Hotwings() : this( 1 ) { }
		[Constructable]
		public Hotwings( int amount ) : base( amount, 0x1608 )
		{
			Weight = 1.0;
			Stackable = true;
			Hue = 0x21A;
			FillFactor = 3;
			Name = "Hotwings";
		}
		public Hotwings( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class Taco : Food
	{
		[Constructable]
		public Taco() : this( 1 ) { }
		[Constructable]
		public Taco( int amount ) : base( amount, 0x1370 )
		{
			Weight = 1.0;
			Stackable = true;
			Hue = 0x465;
			FillFactor = 3;
			Name = "Taco";
		}
		public Taco( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class CornOnCob : Food
	{
		[Constructable]
		public CornOnCob() : this( 1 ) { }
		[Constructable]
		public CornOnCob( int amount ) : base( amount, 0xC81 )
		{
			Weight = 1.0;
			Stackable = true;
			Hue = 0x0;
			FillFactor = 3;
			Name = "Cooked Corn on the Cob";
		}
		public CornOnCob( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class Hotdog : Food
	{
		[Constructable]
		public Hotdog() : this( 1 ) { }
		[Constructable]
		public Hotdog( int amount ) : base( amount, 0xC7F )
		{
			Weight = 1.0;
			Stackable = true;
			Hue = 0x457;
			FillFactor = 3;
			Name = "Hotdog";
		}
		public Hotdog( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}