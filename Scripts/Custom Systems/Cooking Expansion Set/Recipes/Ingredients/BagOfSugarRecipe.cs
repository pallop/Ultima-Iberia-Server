using System;
using System.Collections;
using Server;
using Server.Engines.Craft;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class BagOfSugarRecipe : RecipeScroll
	{

		[Constructable]
		public BagOfSugarRecipe() : base( 5001 )
		{
			
		}

        public BagOfSugarRecipe(Serial serial)
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
