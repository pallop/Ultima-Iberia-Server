using System;
using System.Collections;
using Server;
using Server.Engines.Craft;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class RandomBakingRecipe : RecipeScroll
	{

		[Constructable]
        public RandomBakingRecipe()
            : base(Utility.RandomMinMax(5501, 5550))
		{
			
		}

        public RandomBakingRecipe(Serial serial)
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
