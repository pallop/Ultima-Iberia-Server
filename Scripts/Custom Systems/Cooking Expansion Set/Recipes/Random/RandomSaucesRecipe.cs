using System;
using System.Collections;
using Server;
using Server.Engines.Craft;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class RandomSaucesRecipe : RecipeScroll
	{

		[Constructable]
        public RandomSaucesRecipe()
            : base(Utility.RandomMinMax(5201, 5208))
		{
			
		}

        public RandomSaucesRecipe(Serial serial)
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
