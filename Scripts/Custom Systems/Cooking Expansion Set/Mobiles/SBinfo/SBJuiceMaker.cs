using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBJuiceMaker : SBInfo
	{
        private readonly List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private readonly IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBJuiceMaker()
		{
		}

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
				this.Add( new GenericBuyInfo( typeof( JuicersTools ), 500, 20, 0xE4F, 0 ) );
				this.Add( new GenericBuyInfo( typeof( EmptyJuiceBottle ), 20, 50, 0x99B, 0 ) );
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