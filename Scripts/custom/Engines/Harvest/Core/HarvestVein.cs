using System;

namespace Server.Engines.Harvest
{
    public class HarvestVein
    {
        private double m_VeinChance;
        private double m_ChanceToFallback;
        private HarvestResource m_PrimaryResource;
        private HarvestResource m_FallbackResource;
		//daat99 OWLTR start - custom harvesting
		private bool b_IsProspected = false;
		public bool IsProspected{ get{ return b_IsProspected; } set{ b_IsProspected = value; } }
		//daat99 OWLTR end - custom harvesting
        public HarvestVein(double veinChance, double chanceToFallback, HarvestResource primaryResource, HarvestResource fallbackResource)
        {
            this.m_VeinChance = veinChance;
            this.m_ChanceToFallback = chanceToFallback;
            this.m_PrimaryResource = primaryResource;
            this.m_FallbackResource = fallbackResource;
        }

        public double VeinChance
        {
            get
            {
                return this.m_VeinChance;
            }
            set
            {
                this.m_VeinChance = value;
            }
        }
        public double ChanceToFallback
        {
            get
            {
                return this.m_ChanceToFallback;
            }
            set
            {
                this.m_ChanceToFallback = value;
            }
        }
        public HarvestResource PrimaryResource
        {
            get
            {
                return this.m_PrimaryResource;
            }
            set
            {
                this.m_PrimaryResource = value;
            }
        }
        public HarvestResource FallbackResource
        {
            get
            {
                return this.m_FallbackResource;
            }
            set
            {
                this.m_FallbackResource = value;
            }
        }
    }
}