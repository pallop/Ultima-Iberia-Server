using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseScales : Item, ICommodity
	{
		public override int LabelNumber{ get{ return 1053139; } } // dragon scales

		private CraftResource m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; InvalidateProperties(); }
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}
		
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Resource = (CraftResource)reader.ReadInt();
					break;
				}
			}
		}

		public BaseScales( CraftResource resource ) : this( resource, 1 )
		{
		}

		public BaseScales( CraftResource resource, int amount ) : base( 0x26B4 )
		{
			Stackable = true;
			Amount = amount;
			Hue = CraftResources.GetHue( resource );

			m_Resource = resource;
		}

		public BaseScales( Serial serial ) : base( serial )
		{
		}
	}

	public class RedScales : BaseScales
	{
		[Constructable]
		public RedScales() : this( 1 )
		{
		}

		[Constructable]
		public RedScales( int amount ) : base( CraftResource.RedScales, amount )
		{
			Name = "Red Scales"; //daat99 OWLTR - custom resource name
		}

		public RedScales( Serial serial ) : base( serial )
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

	public class YellowScales : BaseScales
	{
		[Constructable]
		public YellowScales() : this( 1 )
		{
		}

		[Constructable]
		public YellowScales( int amount ) : base( CraftResource.YellowScales, amount )
		{
			Name = "Yellow Scales"; //daat99 OWLTR - custom resource name
		}

		public YellowScales( Serial serial ) : base( serial )
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

	public class BlackScales : BaseScales
	{
		[Constructable]
		public BlackScales() : this( 1 )
		{
		}

		[Constructable]
		public BlackScales( int amount ) : base( CraftResource.BlackScales, amount )
		{
			Name = "Black Scales"; //daat99 OWLTR - custom resource name
		}

		public BlackScales( Serial serial ) : base( serial )
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

	public class GreenScales : BaseScales
	{
		[Constructable]
		public GreenScales() : this( 1 )
		{
		}

		[Constructable]
		public GreenScales( int amount ) : base( CraftResource.GreenScales, amount )
		{
			Name = "Green Scales"; //daat99 OWLTR - custom resource name
		}

		public GreenScales( Serial serial ) : base( serial )
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

	public class WhiteScales : BaseScales
	{
		[Constructable]
		public WhiteScales() : this( 1 )
		{
		}

		[Constructable]
		public WhiteScales( int amount ) : base( CraftResource.WhiteScales, amount )
		{
			Name = "White Scales"; //daat99 OWLTR - custom resource name
		}

		public WhiteScales( Serial serial ) : base( serial )
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

	public class BlueScales : BaseScales
	{
		public override int LabelNumber{ get{ return 1053140; } } // sea serpent scales

		[Constructable]
		public BlueScales() : this( 1 )
		{
		}

		[Constructable]
		public BlueScales( int amount ) : base( CraftResource.BlueScales, amount )
		{
			Name = "Blue Scales"; //daat99 OWLTR - custom resource name
		}

		public BlueScales( Serial serial ) : base( serial )
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
	public class CopperScales : BaseScales
	{
		[Constructable]
		public CopperScales() : this( 1 )
		{
		}

		[Constructable]
		public CopperScales( int amount ) : base( CraftResource.CopperScales, amount )
		{
			Name = "Copper Scales"; //daat99 OWLTR - custom resource name
		}

		public CopperScales( Serial serial ) : base( serial )
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
	public class SilverScales : BaseScales
	{
		[Constructable]
		public SilverScales() : this( 1 )
		{
		}

		[Constructable]
		public SilverScales( int amount ) : base( CraftResource.SilverScales, amount )
		{
			Name = "Silver Scales"; //daat99 OWLTR - custom resource name
		}

		public SilverScales( Serial serial ) : base( serial )
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
	public class GoldScales : BaseScales
	{
		[Constructable]
		public GoldScales() : this( 1 )
		{
		}

		[Constructable]
		public GoldScales( int amount ) : base( CraftResource.GoldScales, amount )
		{
			Name = "Gold Scales"; //daat99 OWLTR - custom resource name
		}

		public GoldScales( Serial serial ) : base( serial )
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