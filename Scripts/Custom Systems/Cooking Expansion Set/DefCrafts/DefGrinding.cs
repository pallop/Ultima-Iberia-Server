using System;
using Server;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefGrinding : CraftSystem
	{
		public override SkillName MainSkill { get { return SkillName.Cooking; } }

		public override int GumpTitleNumber { get { return 0; } }

		public override string GumpTitleString { get { return "<basefont color=#FFFFFF><CENTER>Grinding Menu</CENTER></basefont>"; } }

		private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem { get { if (m_CraftSystem == null) m_CraftSystem = new DefGrinding(); return m_CraftSystem; } }

		public override double GetChanceAtMin( CraftItem item ) { return 0.5; }

        private DefGrinding() : base(1, 1, 1.25) { }

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 ) return 1044038;
			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x5AA );
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

            index = AddCraft(typeof(BagOfCoffee), "Grounds", "Bag of Coffee", 50.0, 55.0, typeof(CoffeeBean), "Coffee Bean", 50, "You need more Coffee Bean's.");
            AddRes(index, typeof(Bag), "Bag", 1, "You need a bag to put the coffee grounds in.");

            index = AddCraft(typeof(BagOfCocoa), "Grounds", "Bag of Cocoa", 100.0, 105.6, typeof(CocoaBean), "Cocoa Bean", 50, "You need more Cocoa Bean's.");
            AddRes(index, typeof(Bag), "Bag", 1, "You need a bag to put the cocoa grounds in.");

			MarkOption = true;
			Repair = false;
		}
	}
}