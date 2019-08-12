/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBForeman : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBForeman()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
                Add(new GenericBuyInfo("1041280", typeof(CementSupply), 5, 20, 0x1BE8, 0));
                Add(new GenericBuyInfo("1041280", typeof(MortarSupply), 5, 20, 0xE77, 0));
                Add(new GenericBuyInfo("1041280", typeof(PitchSupply), 5, 20, 0xFAB, 0));
                Add(new GenericBuyInfo("1041280", typeof(TarSupply), 5, 20, 0xFAB, 0));
                Add(new GenericBuyInfo("1041280", typeof(NailSupply), 5, 20, 0x102E, 0));
                Add(new GenericBuyInfo("1041280", typeof(JointSupply), 5, 20, 0xF9D, 0));
                Add(new GenericBuyInfo("1041280", typeof(HingeSupply), 5, 20, 0x1055, 0));
                Add(new GenericBuyInfo("1041280", typeof(PaintSupply), 5, 20, 0xE7F, 0));
                Add(new GenericBuyInfo("1041280", typeof(StainSupply), 5, 20, 0xE7F, 0));
                Add(new GenericBuyInfo("1041280", typeof(HouseRecipes.FieldStoneHouseRecipe), 200, 20, 0x14F0, 0));
                Add(new GenericBuyInfo("1041280", typeof(HouseRecipes.SmallBrickHouseRecipe), 200, 20, 0x14F0, 0));
                Add(new GenericBuyInfo("1041280", typeof(HouseRecipes.StonePlasterHouseRecipe), 200, 20, 0x14F0, 0));
                Add(new GenericBuyInfo("1041280", typeof(HouseRecipes.ThatchedRoofCottageRecipe), 200, 20, 0x14F0, 0));
                Add(new GenericBuyInfo("1041280", typeof(HouseRecipes.WoodHouseRecipe), 200, 20, 0x14F0, 0));
                Add(new GenericBuyInfo("1041280", typeof(HouseRecipes.WoodPlasterHouseRecipe), 200, 20, 0x14F0, 0));
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( CementSupply ), 2 );
				Add( typeof( MortarSupply ), 2 );
				Add( typeof( PitchSupply ), 2 );
				Add( typeof( TarSupply ), 2 );
				Add( typeof( NailSupply ), 2 );
				Add( typeof( JointSupply ), 2 );
				Add( typeof( HingeSupply ), 2 );
				Add( typeof( PaintSupply ), 2 );
				Add( typeof( StainSupply ), 2 );
			}
		}
	}
}