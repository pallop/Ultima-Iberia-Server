using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.BulkOrders
{
    public class BOBFilterGump : Gump
    {
		private PlayerMobile m_From;
		private BulkOrderBook m_Book;
		private const int LabelColor = 0x7FFF;
        private static readonly int[,] m_MaterialFilters = new int[,]
        {
            { 1044067, 1 }, // Blacksmithy
            { 1062226, 3 }, // Iron
            { 1018332, 4 }, // Dull Copper
            { 1018333, 5 }, // Shadow Iron
            { 1018334, 6 }, // Copper
            { 1018335, 7 }, // Bronze

            { 0, 0 }, // --Blank--
            { 1018336, 8 }, // Golden
            { 1018337, 9 }, // Agapite
            { 1018338, 10 }, // Verite
            { 1018339, 11 }, // Valorite
				//daat99 OWLTR start - custom ores
				{       0, 12 }, // Blaze
            	{ 0, 0 }, // --Blank--
				{       0, 13 }, // Ice
				{       0, 14 }, // Toxic
				{       0, 15 }, // Electrum
				{       0, 16 }, // Platinum
				//daat99 OWLTR end - custom ores
				{       0,  0 }, // --Blank--

            { 1044094, 2 }, // Tailoring
				//daat99 OWLTR start - custom leather
				{ 1044286, 17 }, // Cloth
				{ 1062235, 18 }, // Leather
				{ 1062236, 19 }, // Spined
				{ 1062237, 20 }, // Horned
				{ 1062238, 21 }, // Barbed
				{       0,  0 }, // --Blank--
				{		0, 22 }, // Polar
				{		0, 23 }, // Synthetic
				{		0, 24 }, // BlazeL
				{		0, 25 }, // Daemonic
				{       0, 26 }, // Shadow
				{       0,  0 }, // --Blank--
				{		0, 27 }, // Frost
				{		0, 28 }, // Ethereal
				{       0,  0 }, // --Blank--
				{       0,  0 }	 // --Blank--
				//daat99 OWLTR end - custom leather
        };
        private static readonly int[,] m_TypeFilters = new int[,]
        {
            { 1062229, 0 }, // All
            { 1062224, 1 }, // Small
            { 1062225, 2 }// Large
        };
        private static readonly int[,] m_QualityFilters = new int[,]
        {
            { 1062229, 0 }, // All
            { 1011542, 1 }, // Normal
            { 1060636, 2 }// Exceptional
        };
        private static readonly int[,] m_AmountFilters = new int[,]
        {
            { 1062229, 0 }, // All
            { 1049706, 1 }, // 10
            { 1016007, 2 }, // 15
            { 1062239, 3 }// 20
        };
        private static readonly int[][,] m_Filters = new int[][,]
        {
            m_TypeFilters,
            m_QualityFilters,
            m_MaterialFilters,
            m_AmountFilters
        };
        private static readonly int[] m_XOffsets_Type = new int[] { 0, 75, 170 };
        private static readonly int[] m_XOffsets_Quality = new int[] { 0, 75, 170 };
        private static readonly int[] m_XOffsets_Amount = new int[] { 0, 75, 180, 275 };
        private static readonly int[] m_XOffsets_Material = new int[] { 0, 105, 210, 305, 390, 485 };
        private static readonly int[] m_XWidths_Small = new int[] { 50, 50, 70, 50 };
        private static readonly int[] m_XWidths_Large = new int[] { 80, 50, 50, 50, 50, 50 };
		private void AddFilterList( int x, int y, int[] xOffsets, int yOffset, int[,] filters, int[] xWidths, int filterValue, int filterIndex )
		{
			for ( int i = 0; i < filters.GetLength( 0 ); ++i )
			{
				int number = filters[i, 0];

				if ( number == 0 )
					continue;
				bool isSelected = ( filters[i, 1] == filterValue );
				if ( !isSelected && (i % xOffsets.Length) == 0 )
					isSelected = ( filterValue == 0 );
				//daat99 OWLTR start - filter	
				if ( filters[i, 1] == 0 )
					continue;
				else
				{
					switch (filters[i, 1])
					{
						 case 12:
        {
							 AddHtml( x + 35 + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), xWidths[i % xOffsets.Length], 32, isSelected ? "<basefont color=#8484FF>Blaze" : "<basefont color=#FFFFFF>Blaze", false, false );
							 AddButton( x + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), 4005, 4007, filterIndex + ((filters[i, 1]) * 4), GumpButtonType.Reply, 0 ); continue;	
						 }
						 case 13:
						 {
							 AddHtml( x + 35 + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), xWidths[i % xOffsets.Length], 32, isSelected ? "<basefont color=#8484FF>Ice" : "<basefont color=#FFFFFF>Ice", false, false );
							 AddButton( x + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), 4005, 4007, 4 + filterIndex + ((filters[i, 1]) * 4), GumpButtonType.Reply, 0 ); continue;	
						 }
						 case 14:
						 {
							 AddHtml( x + 35 + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), xWidths[i % xOffsets.Length], 32, isSelected ? "<basefont color=#8484FF>Toxic" : "<basefont color=#FFFFFF>Toxic", false, false );
							 AddButton( x + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), 4005, 4007, 4 + filterIndex + ((filters[i, 1]) * 4), GumpButtonType.Reply, 0 ); continue;	
						 }
						 case 15:
						 {
							 AddHtml( x + 35 + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), xWidths[i % xOffsets.Length], 32, isSelected ? "<basefont color=#8484FF>Electrum" : "<basefont color=#FFFFFF>Electrum", false, false );
							 AddButton( x + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), 4005, 4007, 4 + filterIndex + ((filters[i, 1]) * 4), GumpButtonType.Reply, 0 ); continue;	
						 }
						 case 16:
						 {
							 AddHtml( x + 35 + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), xWidths[i % xOffsets.Length], 32, isSelected ? "<basefont color=#8484FF>Platinum" : "<basefont color=#FFFFFF>Platinum", false, false );
							 AddButton( x + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), 4005, 4007, 4 + filterIndex + ((filters[i, 1]) * 4), GumpButtonType.Reply, 0 ); continue;	
						 }
						 case 22:
						 {
							 AddHtml( x + 35 + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), xWidths[i % xOffsets.Length], 32, isSelected ? "<basefont color=#8484FF>Polar" : "<basefont color=#FFFFFF>Polar", false, false );
							 AddButton( x + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), 4005, 4007, 16 + filterIndex + ((filters[i, 1]) * 4), GumpButtonType.Reply, 0 ); continue;	
						 }
						 case 23:
						 {
							 AddHtml( x + 35 + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), xWidths[i % xOffsets.Length], 32, isSelected ? "<basefont color=#8484FF>Synthetic" : "<basefont color=#FFFFFF>Synthetic", false, false );
							 AddButton( x + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), 4005, 4007, 16 + filterIndex + ((filters[i, 1]) * 4), GumpButtonType.Reply, 0 ); continue;	
						 }
						 case 24:
						 {
							 AddHtml( x + 35 + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), xWidths[i % xOffsets.Length], 32, isSelected ? "<basefont color=#8484FF>Blaze" : "<basefont color=#FFFFFF>Blaze", false, false );
							 AddButton( x + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), 4005, 4007, 16 + filterIndex + ((filters[i, 1]) * 4), GumpButtonType.Reply, 0 ); continue;	
						 }
						 case 25:
						 {
							 AddHtml( x + 35 + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), xWidths[i % xOffsets.Length], 32, isSelected ? "<basefont color=#8484FF>Daemonic" : "<basefont color=#FFFFFF>Daemonic", false, false );
							 AddButton( x + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), 4005, 4007, 16 + filterIndex + ((filters[i, 1]) * 4), GumpButtonType.Reply, 0 ); continue;	
						 }
						 case 26:
						 {
							 AddHtml( x + 35 + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), xWidths[i % xOffsets.Length], 32, isSelected ? "<basefont color=#8484FF>Shadow" : "<basefont color=#FFFFFF>Shadow", false, false );
							 AddButton( x + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), 4005, 4007, 16 + filterIndex + ((filters[i, 1]) * 4), GumpButtonType.Reply, 0 ); continue;	
						 }
						 case 27:
						 {
							 AddHtml( x + 35 + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), xWidths[i % xOffsets.Length], 32, isSelected ? "<basefont color=#8484FF>Frost" : "<basefont color=#FFFFFF>Frost", false, false );
							 AddButton( x + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), 4005, 4007, 20 + filterIndex + ((filters[i, 1]) * 4), GumpButtonType.Reply, 0 ); continue;	
						 }
						 case 28:
						 {
							 AddHtml( x + 35 + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), xWidths[i % xOffsets.Length], 32, isSelected ? "<basefont color=#8484FF>Ethereal" : "<basefont color=#FFFFFF>Ethereal", false, false );
							 AddButton( x + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), 4005, 4007, 20 + filterIndex + ((filters[i, 1]) * 4), GumpButtonType.Reply, 0 );continue;	
						 }
					 }
				}
				//daat99 OWLTR start - filter

				AddHtmlLocalized( x + 35 + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), xWidths[i % xOffsets.Length], 32, number, isSelected ? 16927 : LabelColor, false, false );
				AddButton( x + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), 4005, 4007, 4 + filterIndex + (i * 4), GumpButtonType.Reply, 0 );
        }
		}

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            BOBFilter f = (this.m_From.UseOwnFilter ? this.m_From.BOBFilter : this.m_Book.Filter);

            int index = info.ButtonID;

            switch ( index )
            {
                case 0: // Apply
                    {
                        this.m_From.SendGump(new BOBGump(this.m_From, this.m_Book));

                        break;
                    }
                case 1: // Set Book Filter
                    {
                        this.m_From.UseOwnFilter = false;
                        this.m_From.SendGump(new BOBFilterGump(this.m_From, this.m_Book));

                        break;
                    }
                case 2: // Set Your Filter
                    {
                        this.m_From.UseOwnFilter = true;
                        this.m_From.SendGump(new BOBFilterGump(this.m_From, this.m_Book));

                        break;
                    }
                case 3: // Clear Filter
                    {
                        f.Clear();
                        this.m_From.SendGump(new BOBFilterGump(this.m_From, this.m_Book));

                        break;
                    }
                default:
                    {
                        index -= 4;

                        int type = index % 4;
                        index /= 4;

                        if (type >= 0 && type < m_Filters.Length)
                        {
                            int[,] filters = m_Filters[type];

                            if (index >= 0 && index < filters.GetLength(0))
                            {
							//daat99 OWLTR start - custom ores
							if ( filters[index, 0] == 0 && filters[index, 1] == 0 )
							//daat99 OWLTR end - custom ores
                                    break;

                                switch ( type )
                                {
                                    case 0:
                                        f.Type = filters[index, 1];
                                        break;
                                    case 1:
                                        f.Quality = filters[index, 1];
                                        break;
                                    case 2:
                                        f.Material = filters[index, 1];
                                        break;
                                    case 3:
                                        f.Quantity = filters[index, 1];
                                        break;
                                }

                                this.m_From.SendGump(new BOBFilterGump(this.m_From, this.m_Book));
                            }
                        }

                        break;
                    }
            }
        }

		public BOBFilterGump( PlayerMobile from, BulkOrderBook book ) : base( 12, 24 )
        {
			from.CloseGump( typeof( BOBGump ) );
			from.CloseGump( typeof( BOBFilterGump ) );

			m_From = from;
			m_Book = book;

			BOBFilter f = ( from.UseOwnFilter ? from.BOBFilter : book.Filter );

			AddPage( 0 );
			//daat99 OWLTR start - bigger gump
			AddBackground( 10, 10, 630, 439, 5054 );

			AddImageTiled( 18, 20, 613, 420, 2624 );
			AddAlphaRegion( 18, 20, 613, 420 );

			AddImage( 5, 5, 10460 );
			AddImage( 615, 5, 10460 );
			AddImage( 5, 424, 10460 );
			AddImage( 615, 424, 10460 );

			AddHtmlLocalized( 270, 20, 200, 32, 1062223, LabelColor, false, false ); // Filter Preference

			AddHtmlLocalized( 26, 35, 120, 32, 1062228, LabelColor, false, false ); // Bulk Order Type
			AddFilterList( 25, 61, m_XOffsets_Type, 40, m_TypeFilters, m_XWidths_Small, f.Type, 0 );

			AddHtmlLocalized( 320, 35, 50, 32, 1062215, LabelColor, false, false ); // Quality
			AddFilterList( 320, 61, m_XOffsets_Quality, 40, m_QualityFilters, m_XWidths_Small, f.Quality, 1 );
			AddHtmlLocalized( 26, 100, 120, 32, 1062232, LabelColor, false, false ); // Material Type
			AddFilterList( 25, 132, m_XOffsets_Material, 40, m_MaterialFilters, m_XWidths_Large, f.Material, 2 );
			AddHtmlLocalized( 26, 350, 120, 32, 1062217, LabelColor, false, false ); // Amount
			AddFilterList( 25, 372, m_XOffsets_Amount, 40, m_AmountFilters, m_XWidths_Small, f.Quantity, 3 );
			//daat99 OWLTR end - bigger gump
			AddHtmlLocalized( 75, 416, 120, 32, 1062477, ( from.UseOwnFilter ? LabelColor : 16927 ), false, false ); // Set Book Filter
			AddButton( 40, 416, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 235, 416, 120, 32, 1062478, ( from.UseOwnFilter ? 16927 : LabelColor ), false, false ); // Set Your Filter
			AddButton( 200, 416, 4005, 4007, 2, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 405, 416, 120, 32, 1062231, LabelColor, false, false ); // Clear Filter
			AddButton( 370, 416, 4005, 4007, 3, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 540, 416, 50, 32, 1011046, LabelColor, false, false ); // APPLY
			AddButton( 505, 416, 4017, 4018, 0, GumpButtonType.Reply, 0 );
        }
    }
}