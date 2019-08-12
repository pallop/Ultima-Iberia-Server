using System;

namespace Server.Items
{
	public abstract class BaseMagicFood : Item
	{
		public virtual int Bonus{ get{ return 0; } }
		public virtual StatType Type{ get{ return StatType.Str; } }

		public override double DefaultWeight
		{
			get { return 1.0; }
		}

		public BaseMagicFood( int itemID ) : base( itemID )
		{
		}

		public BaseMagicFood( Serial serial ) : base( serial )
		{
		}

		public virtual bool Apply( Mobile from )
		{
			bool applied = Spells.SpellHelper.AddStatOffset( from, Type, Bonus, TimeSpan.FromMinutes( 1.0 ) );

			if ( !applied )
				from.SendLocalizedMessage( 502173 ); // You are already under a similar effect.

			return applied;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if ( Apply( from ) )
			{
				from.FixedEffect( 0x375A, 10, 15 );
				from.PlaySound( 0x1E7 );
				Delete();
			}
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

	public class GloriousBeefRibs : BaseMagicFood
	{
		public override int Bonus{ get{ return 5; } }
		public override StatType Type{ get{ return StatType.Str; } }

		[Constructable]
		public GloriousBeefRibs() : base( 0x9F2 )
		{
            Name = "Glorious Beef Ribs";
            Stackable = false;
		}

		public GloriousBeefRibs( Serial serial ) : base( serial )
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

			if ( Hue == 151 )
				Hue = 51;
		}
	}

	public class ElixirOfSpeed : BaseMagicFood
	{
		public override int Bonus{ get{ return 5; } }
		public override StatType Type{ get{ return StatType.Dex; } }

		[Constructable]
		public ElixirOfSpeed() : base( 0x99B )
		{
            Name = "Elixir of Speed";
            Hue = 0x47E;
            Stackable = false;
		}

		public ElixirOfSpeed( Serial serial ) : base( serial )
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

			if ( Hue == 286 )
				Hue = 86;
		}
	}

	public class Mooncake : BaseMagicFood
	{
		public override int Bonus{ get{ return 5; } }
		public override StatType Type{ get{ return StatType.Int; } }

		[Constructable]
		public Mooncake() : base( 0x9E9 )
		{
            Name = "Mooncake";
            Hue = 0x482;
            Stackable = false;
		}

		public Mooncake( Serial serial ) : base( serial )
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

			if ( Hue == 376 )
				Hue = 76;
		}
	}

	public class PerfectSalad : BaseMagicFood
	{

		[Constructable]
		public PerfectSalad() : base( 0x1601 )
		{
            Name = "Perfect Salad";
            Stackable = false;
		}

		public PerfectSalad( Serial serial ) : base( serial )
		{
		}

		public override bool Apply( Mobile from )
		{
			from.Stam += 10;
			return true;
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

			if ( Hue == 266 )
				Hue = 66;
		}
	}

	public class MeditationSteak : BaseMagicFood
	{

		[Constructable]
		public MeditationSteak() : base( 0x97B )
		{
            Name = "Meditation Steak";
            Hue = 0x542;
            Stackable = false;
		}

		public MeditationSteak( Serial serial ) : base( serial )
		{
		}

		public override bool Apply( Mobile from )
		{
			from.Mana += 10;
			return true;
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

			if ( Hue == 266 )
				Hue = 66;
		}
	}

	public class MuffinsOfHealth : BaseMagicFood
	{

		[Constructable]
		public MuffinsOfHealth() : base( 0x9EB )
		{
            Name = "Muffins of Health";
            Hue = 0x675;
            Stackable = false;
		}

		public MuffinsOfHealth( Serial serial ) : base( serial )
		{
		}

		public override bool Apply( Mobile from )
		{
			from.Hits += 10;
			return true;
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

			if ( Hue == 266 )
				Hue = 66;
		}
	}
}