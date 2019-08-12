using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBBrewer : SBInfo
	{
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBrewer() { }

        public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
        public override List<GenericBuyInfo> BuyInfo
        {
            get
            {
                return this.m_BuyInfo;
            }
        }

        public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
                this.Add(new GenericBuyInfo(typeof(Keg), 50, 20, 0xE7F, 0));
                this.Add(new GenericBuyInfo(typeof(BrewersTools), 500, 20, 0x1EBC, 0));
                this.Add(new GenericBuyInfo(typeof(BreweryLabelMaker), 500, 20, 0xFBF, 0));
                this.Add(new GenericBuyInfo(typeof(Malt), 10, 20, 0x103D, 0));
                this.Add(new GenericBuyInfo(typeof(Barley), 20, 20, 0x103F, 0));
                this.Add(new GenericBuyInfo(typeof(BrewersYeast), 20, 20, 0x103F, 0));
                this.Add(new GenericBuyInfo(typeof(EmptyAleBottle), 10, 20, 0x99B, 0));
                this.Add(new GenericBuyInfo(typeof(EmptyMeadBottle), 10, 20, 0x99B, 0));
                this.Add(new GenericBuyInfo(typeof(EmptyJug), 10, 20, 0x9C8, 0));
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
}