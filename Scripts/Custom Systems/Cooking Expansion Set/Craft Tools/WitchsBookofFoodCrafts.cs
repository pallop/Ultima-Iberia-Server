using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
    [FlipableAttribute(3643, 3643)]
	public class WitchsBookofFoodCrafts : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefWitchcraft.CraftSystem; } }

		[Constructable]
		public WitchsBookofFoodCrafts() : base( 3643 )
		{
            Weight = 1.0;
            Hue = 0x3CC;
            Name = "libro de Cocina mejorada";
		}

		[Constructable]
		public WitchsBookofFoodCrafts( int uses ) : base( uses, 3643 )
		{
            Weight = 1.0;
            Hue = 0x3CC;
            Name = "libro de Cocina mejorada";
		}

        public WitchsBookofFoodCrafts(Serial serial)
            : base(serial)
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
