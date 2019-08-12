using System;
using Server;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefFenceCrafting : CraftSystem
	{
        public override SkillName MainSkill { get { return SkillName.Carpentry; } }

		public override int GumpTitleNumber { get { return 0; } }

        public override string GumpTitleString { get { return "<basefont color=#FFFFFF><CENTER>Fence Crafting MENU</CENTER></basefont>"; } }

		private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem { get { if (m_CraftSystem == null) m_CraftSystem = new DefFenceCrafting(); return m_CraftSystem; } }

		public override double GetChanceAtMin( CraftItem item ) { return 0.5; }

        private DefFenceCrafting() : base(1, 1, 1.25) { }

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 ) return 1044038;
			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x241 );
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
            index = AddCraft(typeof(Fence), "Fence", "a Fence", 30.5, 70.5, typeof(Board), "Board", 4, "You don't have enough Boards.");
            AddRes(index, typeof(FencePost), "Fence Post", 2, "You need Fence Posts");

            index = AddCraft(typeof(FenceCorner), "Fence", "a Corner Fence", 35.0, 75.0, typeof(Board), "Board", 8, "You don't have enough Boards.");
            AddRes(index, typeof(FencePost), "Fence Post", 1, "You need a Fence Post");

            index = AddCraft(typeof(FencePost), "Fence", "a Fence Post", 10.0, 25.0, typeof(Log), "Log", 1, "You need a log.");

            index = AddCraft(typeof(NorthGate), "Fence", "a North Gate", 50.0, 90.0, typeof(Board), "Board", 7, "You don't have enough Boards.");
            AddRes(index, typeof(Hinge), "Hinge", 2, "You need Hinges");

            index = AddCraft(typeof(WestGate), "Fence", "a West Gate", 50.0, 90.0, typeof(Board), "Board", 7, "You don't have enough Boards.");
            AddRes(index, typeof(Hinge), "Hinge", 2, "You need Hinges");

			MarkOption = true;
			Repair = false;
		}
	}
}