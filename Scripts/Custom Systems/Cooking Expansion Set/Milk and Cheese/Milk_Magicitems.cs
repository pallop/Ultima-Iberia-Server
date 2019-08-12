using System;

namespace Server.Items
{
	public abstract class BaseMagicCheese : Item
	{
		public virtual int Bonus{ get{ return 0; } }
		public virtual StatType Type{ get{ return StatType.Str; } }

		public BaseMagicCheese( int hue ) : base( 0x97E )
		{

			if ( Utility.RandomBool() )
				Hue = Utility.RandomList(0x135, 0xcd, 0x38, 0x3b, 0x42, 0x4f, 0x11e, 0x60, 0x317, 0x10, 0x136, 0x1f9, 0x1a, 0xeb, 0x86, 0x2e, 0x0497, 0x0481);
			else
				Hue = 3 + (Utility.Random( 20 ) * 5);

		}

		public BaseMagicCheese( Serial serial ) : base( serial ) { }

		public virtual bool Apply( Mobile from )
		{
			bool applied = Spells.SpellHelper.AddStatOffset( from, Type, Bonus, TimeSpan.FromMinutes( 5.0 ) );

			if ( !applied )
				from.SendLocalizedMessage( 502173 );

			return applied;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 );
			}
			else if ( Apply( from ) )
			{
				from.FixedEffect( 0x375A, 10, 15 );
				from.PlaySound( 0x1E7 );
				Delete();
			}
		}

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class FromageDeChevreMagic : BaseMagicCheese
	{
		public override int Bonus{ get{ return 5; } }
		public override StatType Type{ get{ return StatType.Str; } }

		[Constructable]
		public FromageDeChevreMagic() : base( 151 )
		{
			this.Name = "crottin de Chavignol (magic goat cheese)";
		}

		public FromageDeChevreMagic( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
	public class FromageDeVacheMagic : BaseMagicCheese
	{
		public override int Bonus{ get{ return 5; } }
		public override StatType Type{ get{ return StatType.Int; } }

		[Constructable]
		public FromageDeVacheMagic() : base( 151 )
		{
			this.Name = "Maroille (magic cow cheese)";
		}

		public FromageDeVacheMagic( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class FromageDeBrebisMagic : BaseMagicCheese
	{
		public override int Bonus{ get{ return 5; } }
		public override StatType Type{ get{ return StatType.Dex; } }

		[Constructable]
		public FromageDeBrebisMagic() : base( 151 )
		{
			this.Name = "Roquefort (magic sheep cheese)";
		}

		public FromageDeBrebisMagic( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
