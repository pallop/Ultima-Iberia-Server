using System;
using System.Collections;
using Server;
using Server.Engines.Craft;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class RandomBoilingRecipe : RecipeScroll
	{

		[Constructable]
        public RandomBoilingRecipe()
            : base(Utility.RandomMinMax(5601, 5626))
		{
			
		}

        public RandomBoilingRecipe(Serial serial)
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
