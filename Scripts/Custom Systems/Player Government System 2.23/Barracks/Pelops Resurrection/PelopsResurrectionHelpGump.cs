
//Ashlar, beloved of Morrigan
//With help from GumpStudio

using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Ashlar
{
	public class PelopsResurrectionHelpGump : Gump
	{
		public PelopsResurrectionHelpGump( Mobile from ) : base( 5, 0 )
		{                                                                           //This gump is sent if the Help button is pressed.
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			this.AddPage(0);

			this.AddBackground(304, 3, 246, 450, 83);
            this.AddBackground( 315, 16, 225, 426, 9300 );

			this.AddLabel(322, 27, 0, @"Pelops' Resurrection Help.");

            this.AddLabel( 322, 62, 0, @"GoTo" );
            this.AddButton( 353, 64, 2224, 2224, ( int )Buttons.GoTo, GumpButtonType.Reply, 0 );
            this.AddLabel( 380, 62, 0, @"View the chosen tile." );

            this.AddButton( 325, 90, 2223, 2223, ( int )Buttons.Back, GumpButtonType.Reply, 0 );
			this.AddButton(350, 90, 2224, 2224, (int)Buttons.Next, GumpButtonType.Reply, 0);
			this.AddLabel(380, 86, 0, @"Back and Next tile.");

			this.AddButton(322, 140, 2331, 2331, (int)Buttons.UnlockTargetButton, GumpButtonType.Reply, 0);
			this.AddLabel(350, 140, 0, @"Set on target.");

			this.AddCheck(322, 115, 2510, 2511, false, (int)Buttons.UnlockRadioButton);
			this.AddLabel(350, 115, 0, @"A new item has this set?");

            this.AddTextEntry( 322, 165, 35, 20, 0, ( int )Buttons.HueTextEntry, @"" );
            this.AddBackground( 318, 165, 40, 20, 9350 );
            this.AddLabel( 370, 165, 0, @"Number and text entry." );

			this.AddLabel(322, 205, 0, @"Use the arrow keys to move the");
			this.AddLabel(322, 225, 0, @" targeted item in that direction.");

			this.AddButton(322, 255, 5534, 5534, (int)Buttons.New, GumpButtonType.Reply, 0);
			this.AddLabel(390, 257, 0, @"-target tile-");
			this.AddButton(470, 255, 5531, 5531, (int)Buttons.Delete, GumpButtonType.Reply, 0);
			this.AddLabel(321, 277, 0, @"Delete also removes from collection.");


            this.AddLabel( 385, 305, 0, @"To unlock tiles:" );
            this.AddLabel( 370, 325, 0, @"I wish to release this" );

			this.AddLabel(321, 355, 0, @"----------{ Created by }----------");
			this.AddLabel(321, 375, 0, @"   Ashlar, beloved of Morrigan.");
			this.AddLabel(322, 395, 0, @"    -( Special thanks to )-");
			this.AddLabel(323, 415, 0, @"    Anya for Pandora's Box");
		}
		public enum Buttons
		{
            Exit,
			UnlockRadioButton,
			HuePicker,
			DecZ,
            Back,
			Next,
			Previous,
			Delete,
			New,
			HueTextEntry,
            GoTo,
			UnlockTargetButton,
			CopyofDecZ,
		}
	}
    public class PelopsResurrectionCostGump : Gump
    {
        PelopsResurrection m_Pelops;

        public PelopsResurrectionCostGump( Mobile from, PelopsResurrection pelops )
            : base( 5, 0 )
        {                                                                           //This gump is sent on a Help button resopnse if the bool HasCost is true in the item constructable
            m_Pelops = pelops;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            this.AddPage( 0 );

            this.AddBackground( 304, 445, 246, 144, 83 );
            this.AddBackground( 315, 458, 225, 120, 9300 );

            this.AddLabel( 330, 460, 0, @"Pelops' Resurrection Price List." );

            this.AddLabel( 340, 485, 0, @"Create New price: " + m_Pelops.NewCost() + " " + m_Pelops.PayTypeName() );
            this.AddLabel( 340, 505, 0, @" Reset ID price:  " + m_Pelops.ResetIDCost() + " " + m_Pelops.PayTypeName() );
            this.AddLabel( 340, 525, 0, @"  Rename price: " + m_Pelops.LabelCost() + " " + m_Pelops.PayTypeName() );
            this.AddLabel( 340, 545, 0, @"   Rehue price: " + m_Pelops.HueCost() + " " + m_Pelops.PayTypeName() );
        }
        public enum Buttons
        {
            Exit,
        }
    }
}