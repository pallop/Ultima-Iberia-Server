using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Engines.BulkOrders
{
    public class SmallBODAcceptGump : Gump
    {
        private readonly SmallBOD m_Deed;
        private readonly Mobile m_From;
        public SmallBODAcceptGump(Mobile from, SmallBOD deed)
            : base(50, 50)
        {
            this.m_From = from;
            this.m_Deed = deed;

            this.m_From.CloseGump(typeof(LargeBODAcceptGump));
            this.m_From.CloseGump(typeof(SmallBODAcceptGump));

            this.AddPage(0);

            this.AddBackground(25, 10, 430, 264, 5054);

            this.AddImageTiled(33, 20, 413, 245, 2624);
            this.AddAlphaRegion(33, 20, 413, 245);

            this.AddImage(20, 5, 10460);
            this.AddImage(430, 5, 10460);
            this.AddImage(20, 249, 10460);
            this.AddImage(430, 249, 10460);

            this.AddHtmlLocalized(190, 25, 120, 20, 1045133, 0x7FFF, false, false); // A bulk order
            this.AddHtmlLocalized(40, 48, 350, 20, 1045135, 0x7FFF, false, false); // Ah!  Thanks for the goods!  Would you help me out?

            this.AddHtmlLocalized(40, 72, 210, 20, 1045138, 0x7FFF, false, false); // Amount to make:
            this.AddLabel(250, 72, 1152, deed.AmountMax.ToString());

            this.AddHtmlLocalized(40, 96, 120, 20, 1045136, 0x7FFF, false, false); // Item requested:
            this.AddItem(385, 96, deed.Graphic);
            this.AddHtmlLocalized(40, 120, 210, 20, deed.Number, 0xFFFFFF, false, false);

            if (deed.RequireExceptional || deed.Material != BulkMaterialType.None)
            {
                this.AddHtmlLocalized(40, 144, 210, 20, 1045140, 0x7FFF, false, false); // Special requirements to meet:

                if (deed.RequireExceptional)
                    this.AddHtmlLocalized(40, 168, 350, 20, 1045141, 0x7FFF, false, false); // All items must be exceptional.

                if (deed.Material != BulkMaterialType.None)
				//daat99 OWLTR start - custom resources
					AddHtml( 40, deed.RequireExceptional ? 192 : 168, 350, 25, "<basefont color=#FF0000>All items must be crafted with " + LargeBODGump.GetMaterialStringFor( deed.Material ), false, false );
				//daat99 OWLTR end - cusotom resources
            }

            this.AddHtmlLocalized(40, 216, 350, 20, 1045139, 0x7FFF, false, false); // Do you want to accept this order?

            this.AddButton(100, 240, 4005, 4007, 1, GumpButtonType.Reply, 0);
            this.AddHtmlLocalized(135, 240, 120, 20, 1006044, 0x7FFF, false, false); // Ok

            this.AddButton(275, 240, 4005, 4007, 0, GumpButtonType.Reply, 0);
            this.AddHtmlLocalized(310, 240, 120, 20, 1011012, 0x7FFF, false, false); // CANCEL
        }

        public static int GetMaterialNumberFor(BulkMaterialType material)
        {
            if (material >= BulkMaterialType.DullCopper && material <= BulkMaterialType.Valorite)
                return 1045142 + (int)(material - BulkMaterialType.DullCopper);
            else if (material >= BulkMaterialType.Spined && material <= BulkMaterialType.Barbed)
                return 1049348 + (int)(material - BulkMaterialType.Spined);

            return 0;
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (info.ButtonID == 1) // Ok
            {
                if (this.m_From.PlaceInBackpack(this.m_Deed))
                {
                    this.m_From.SendLocalizedMessage(1045152); // The bulk order deed has been placed in your backpack.
                }
                else
                {
                    this.m_From.SendLocalizedMessage(1045150); // There is not enough room in your backpack for the deed.
                    this.m_Deed.Delete();
                }
            }
            else
            {
                this.m_Deed.Delete();
            }
        }
		//daat99 OWLTR start - REMOVED - make sure nobody calls this!
		/*
		public static int GetMaterialNumberFor( BulkMaterialType material )
		{
			if ( material >= BulkMaterialType.DullCopper && material <= BulkMaterialType.Valorite )
				return 1045142 + (int)(material - BulkMaterialType.DullCopper);
			else if ( material >= BulkMaterialType.Spined && material <= BulkMaterialType.Barbed )
				return 1049348 + (int)(material - BulkMaterialType.Spined);

			return 0;
		}*/
		//daat99 OWLTR end - REMOVED - make sure nobody calls this!
    }
}