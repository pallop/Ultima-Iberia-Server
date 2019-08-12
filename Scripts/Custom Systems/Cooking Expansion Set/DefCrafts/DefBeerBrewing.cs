using System;
using Server;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefBeerBrewing : CraftSystem
	{
		public override SkillName MainSkill { get { return SkillName.Cooking; } }

		public override int GumpTitleNumber { get { return 0; } }

        public override string GumpTitleString { get { return "<basefont color=#FFFFFF><CENTER>Beer Brewing</CENTER></basefont>"; } }

		private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem { get { if (m_CraftSystem == null) m_CraftSystem = new DefBeerBrewing(); return m_CraftSystem; } }

		public override double GetChanceAtMin( CraftItem item ) { return 0.5; }

        private DefBeerBrewing() : base(1, 1, 1.25) { }

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 ) return 1044038;
			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
            from.PlaySound(0x5B0);
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken ) from.SendLocalizedMessage( 1044038 );

			if ( failed )
			{
				if ( lostMaterial ) return 1044043;
				else return 1044157;
			}
			else
			{
				if ( quality == 0 ) return 502785;
				else if ( makersMark && quality == 2 ) return 1044156;
				else if ( quality == 2 ) return 1044155;
				else return 1044154;
			}
		}

		public override void InitCraftList()
		{
			int index = -1;
			string skillNotice = "You have no idea how to brew with this type of hops.";

            index = AddCraft(typeof(LongNeckBottleOfMillerLite), "Long Necks", "a LongNeck Bottle Of Miller Lite", 0.0, 10.4, typeof(SnowHops), "Snow Hops", 2, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Brewers Yeast", 1, "You need more Brewers Yeast");
            SetNeedDistillery(index, true);

            index = AddCraft(typeof(LongNeckBottleOfMGD), "Long Necks", "a LongNeck Bottle Of Miller Genuine Draft", 10.0, 20.4, typeof(SweetHops), "Sweet Hops", 2, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Brewers Yeast", 1, "You need more Brewers Yeast");
            SetNeedDistillery(index, true);

            index = AddCraft(typeof(LongNeckBottleOfCoolersLite), "Long Necks", "a LongNeck Bottle Of Coolers Lite", 20.0, 35.4, typeof(SnowHops), "Snow Hops", 1, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Brewers Yeast", 1, "You need more Brewers Yeast");
            SetNeedDistillery(index, true);

            index = AddCraft(typeof(LongNeckBottleOfBudLight), "Long Necks", "a LongNeck Bottle Of Bud Light", 35.0, 50.4, typeof(BitterHops), "Bitter Hops", 1, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Brewers Yeast", 1, "You need more Brewers Yeast");
            SetNeedDistillery(index, true);

            index = AddCraft(typeof(LongNeckBottleOfBudWiser), "Long Necks", "a LongNeck Bottle Of Bud Wiser", 50.0, 65.4, typeof(BitterHops), "Bitter Hops", 2, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Brewers Yeast", 1, "You need more Brewers Yeast");
            SetNeedDistillery(index, true);

            index = AddCraft(typeof(LongNeckBottleOfCorna), "Long Necks", "a LongNeck Bottle Of Corna", 65.0, 78.4, typeof(ElvenHops), "Elven Hops", 2, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Brewers Yeast", 1, "You need more Brewers Yeast");
            SetNeedDistillery(index, true);

            index = AddCraft(typeof(LongNeckBottleOfCornaLite), "Long Necks", "a LongNeck Bottle Of Corna Lite", 78.0, 90.4, typeof(ElvenHops), "Elven Hops", 1, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Brewers Yeast", 1, "You need more Brewers Yeast");
            SetNeedDistillery(index, true);

            index = AddCraft(typeof(LongNeckBottleOfWildturkey), "Long Necks", "a LongNeck Bottle Of Wild Turkey", 90.0, 105.4, typeof(SweetHops), "Sweet Hops", 2, "You need more Hops");
            AddRes(index, typeof(BaseBeverage), "Water", 2, "You need more water");
            AddRes(index, typeof(BrewersYeast), "Brewers Yeast", 1, "You need more Brewers Yeast");
            SetNeedDistillery(index, true);

			SetSubRes( typeof( BitterHops ),	"Bitter Hops" );

			AddSubRes( typeof( BitterHops ),	"Bitter Hops", 40.0, skillNotice);
			AddSubRes( typeof( SnowHops ),	"Snow Hops", 100.0, skillNotice);
			AddSubRes( typeof( ElvenHops ),	"Elven Hops", 110.0, skillNotice);
			AddSubRes( typeof( SweetHops ),	"Sweet Hops", 120.0, skillNotice);

			MarkOption = true;
			Repair = false;
		}
	}
}