using System;
using Server;

namespace Server.Items
{
	public class BrewDistilleryEastAddon : BaseAddon
	{
        public override BaseAddonDeed Deed { get { return new BrewDistilleryEastDeed(); } }

		public override bool RetainDeedHue{ get{ return true; } }

		[Constructable]
		public BrewDistilleryEastAddon() : this( 0 )
		{
		}

		[Constructable]
		public BrewDistilleryEastAddon( int hue )
		{
			AddComponent( new LocalizedAddonComponent( 0x3DBB, 1150640  ), 0, 1, 0 );
			AddComponent( new LocalizedAddonComponent( 0x3DBA, 1150640 ), 0, 0, 0 );
			Hue = hue;
		}

        public BrewDistilleryEastAddon(Serial serial)
            : base(serial)
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

	public class BrewDistilleryEastDeed : BaseAddonDeed
	{
        public override BaseAddon Addon { get { return new BrewDistilleryEastAddon(this.Hue); } }
		public override int LabelNumber{ get{ return 1150664; } }

		[Constructable]
		public BrewDistilleryEastDeed()
		{
		}

        public BrewDistilleryEastDeed(Serial serial)
            : base(serial)
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

	public class BrewDistillerySouthAddon : BaseAddon
	{
        public override BaseAddonDeed Deed { get { return new BrewDistillerySouthDeed(); } }

		public override bool RetainDeedHue{ get{ return true; } }

		[Constructable]
		public BrewDistillerySouthAddon() : this( 0 )
		{
		}

		[Constructable]
		public BrewDistillerySouthAddon( int hue )
		{
			AddComponent( new LocalizedAddonComponent( 0x3DB8, 1150640 ), 1, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x3DB9, 1150640 ), 0, 0, 0 );
			Hue = hue;
		}

        public BrewDistillerySouthAddon(Serial serial)
            : base(serial)
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

	public class BrewDistillerySouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new BrewDistillerySouthAddon( this.Hue ); } }
		public override int LabelNumber{ get{ return 1150663; } }

		[Constructable]
		public BrewDistillerySouthDeed()
		{
		}

        public BrewDistillerySouthDeed(Serial serial)
            : base(serial)
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