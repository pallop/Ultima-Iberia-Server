/////////////////////////////////////////////////
//                                             //
// Automatically generated by the              //
// AddonGenerator script by Arya               //
//                                             //
/////////////////////////////////////////////////
using System;
using Server;
using Server.Items;
using Server.Engines.XmlSpawner2;
namespace Server.Items
{
	public class StoneCityMarketAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return null;
			}
		}

		[ Constructable ]
		public StoneCityMarketAddon()
		{
            XmlAttach.AttachTo(this, new XmlCitySiege(10000, 60, 60, 1, 1, 5)); //<-------new CitySiege Attachment
			AddComponent( new AddonComponent( 1315 ), -2, 2, 7 );
			AddComponent( new AddonComponent( 1308 ), -2, 2, 27 );
			AddComponent( new AddonComponent( 1305 ), -2, -1, 7 );
			AddComponent( new AddonComponent( 1311 ), -2, -1, 27 );
			AddComponent( new AddonComponent( 99 ), 2, 4, 0 );
			AddComponent( new AddonComponent( 1305 ), 2, 4, 7 );
			AddComponent( new AddonComponent( 489 ), 2, 4, 27 );
			AddComponent( new AddonComponent( 1308 ), 2, 4, 27 );
			AddComponent( new AddonComponent( 1305 ), 3, 0, 7 );
			AddComponent( new AddonComponent( 1311 ), 3, 0, 27 );
			AddComponent( new AddonComponent( 1305 ), 3, -1, 7 );
			AddComponent( new AddonComponent( 1308 ), 3, -1, 27 );
			AddComponent( new AddonComponent( 1315 ), 3, 2, 7 );
			AddComponent( new AddonComponent( 1311 ), 3, 2, 27 );
			AddComponent( new AddonComponent( 1305 ), 3, 1, 7 );
			AddComponent( new AddonComponent( 1308 ), 3, 1, 27 );
			AddComponent( new AddonComponent( 100 ), 5, 2, 0 );
			AddComponent( new AddonComponent( 1305 ), 5, 2, 7 );
			AddComponent( new AddonComponent( 490 ), 5, 2, 27 );
			AddComponent( new AddonComponent( 1311 ), 5, 2, 27 );
			AddComponent( new AddonComponent( 100 ), 5, 1, 0 );
			AddComponent( new AddonComponent( 1305 ), 5, 1, 7 );
			AddComponent( new AddonComponent( 490 ), 5, 1, 27 );
			AddComponent( new AddonComponent( 1308 ), 5, 1, 27 );
			AddComponent( new AddonComponent( 100 ), 5, 0, 0 );
			AddComponent( new AddonComponent( 1305 ), 5, 0, 7 );
			AddComponent( new AddonComponent( 490 ), 5, 0, 27 );
			AddComponent( new AddonComponent( 1311 ), 5, 0, 27 );
			AddComponent( new AddonComponent( 1928 ), 5, 5, 0 );
			AddComponent( new AddonComponent( 101 ), 5, 4, 0 );
			AddComponent( new AddonComponent( 1305 ), 5, 4, 7 );
			AddComponent( new AddonComponent( 475 ), 5, 4, 7 );
			AddComponent( new AddonComponent( 488 ), 5, 4, 27 );
			AddComponent( new AddonComponent( 1311 ), 5, 4, 27 );
			AddComponent( new AddonComponent( 100 ), 5, 3, 0 );
			AddComponent( new AddonComponent( 1305 ), 5, 3, 7 );
			AddComponent( new AddonComponent( 490 ), 5, 3, 27 );
			AddComponent( new AddonComponent( 1308 ), 5, 3, 27 );
			AddComponent( new AddonComponent( 1 ), 0, -1, 0 );
			AddComponent( new AddonComponent( 1305 ), 0, -1, 7 );
			AddComponent( new AddonComponent( 1311 ), 0, -1, 27 );
			AddComponent( new AddonComponent( 5139 ), 0, -1, 0 );
			AddComponent( new AddonComponent( 1305 ), -3, 1, 7 );
			AddComponent( new AddonComponent( 1308 ), -3, 1, 27 );
			AddComponent( new AddonComponent( 1305 ), -3, 0, 7 );
			AddComponent( new AddonComponent( 1311 ), -3, 0, 27 );
			AddComponent( new AddonComponent( 1305 ), -3, -1, 7 );
			AddComponent( new AddonComponent( 1308 ), -3, -1, 27 );
			AddComponent( new AddonComponent( 100 ), 5, -2, 0 );
			AddComponent( new AddonComponent( 1305 ), 5, -2, 7 );
			AddComponent( new AddonComponent( 490 ), 5, -2, 27 );
			AddComponent( new AddonComponent( 1311 ), 5, -2, 27 );
			AddComponent( new AddonComponent( 100 ), 5, -3, 0 );
			AddComponent( new AddonComponent( 1305 ), 5, -3, 7 );
			AddComponent( new AddonComponent( 490 ), 5, -3, 27 );
			AddComponent( new AddonComponent( 1308 ), 5, -3, 27 );
			AddComponent( new AddonComponent( 100 ), 5, -4, 0 );
			AddComponent( new AddonComponent( 1305 ), 5, -4, 7 );
			AddComponent( new AddonComponent( 478 ), 5, -4, 7 );
			AddComponent( new AddonComponent( 490 ), 5, -4, 27 );
			AddComponent( new AddonComponent( 1311 ), 5, -4, 27 );
			AddComponent( new AddonComponent( 99 ), 5, -5, 0 );
			AddComponent( new AddonComponent( 477 ), 5, -5, 7 );
			AddComponent( new AddonComponent( 489 ), 5, -5, 27 );
			AddComponent( new AddonComponent( 1315 ), 4, 3, 7 );
			AddComponent( new AddonComponent( 1311 ), 4, 3, 27 );
			AddComponent( new AddonComponent( 99 ), 4, 4, 0 );
			AddComponent( new AddonComponent( 1305 ), 4, 4, 7 );
			AddComponent( new AddonComponent( 489 ), 4, 4, 27 );
			AddComponent( new AddonComponent( 1308 ), 4, 4, 27 );
			AddComponent( new AddonComponent( 1929 ), 4, 5, 0 );
			AddComponent( new AddonComponent( 1929 ), 3, 5, 0 );
			AddComponent( new AddonComponent( 99 ), -1, 4, 0 );
			AddComponent( new AddonComponent( 1305 ), -1, 4, 7 );
			AddComponent( new AddonComponent( 489 ), -1, 4, 27 );
			AddComponent( new AddonComponent( 1311 ), -1, 4, 27 );
			AddComponent( new AddonComponent( 99 ), 0, 4, 0 );
			AddComponent( new AddonComponent( 1305 ), 0, 4, 7 );
			AddComponent( new AddonComponent( 489 ), 0, 4, 27 );
			AddComponent( new AddonComponent( 1308 ), 0, 4, 27 );
			AddComponent( new AddonComponent( 1313 ), 1, 0, 7 );
			AddComponent( new AddonComponent( 1311 ), 1, 0, 27 );
			AddComponent( new AddonComponent( 1929 ), -1, 5, 0 );
			AddComponent( new AddonComponent( 1305 ), 1, -2, 7 );
			AddComponent( new AddonComponent( 1311 ), 1, -2, 27 );
			AddComponent( new AddonComponent( 1305 ), 1, -3, 7 );
			AddComponent( new AddonComponent( 1308 ), 1, -3, 27 );
			AddComponent( new AddonComponent( 1305 ), 1, -4, 7 );
			AddComponent( new AddonComponent( 1311 ), 1, -4, 27 );
			AddComponent( new AddonComponent( 99 ), 1, -5, 0 );
			AddComponent( new AddonComponent( 489 ), 1, -5, 27 );
			AddComponent( new AddonComponent( 99 ), 0, -5, 0 );
			AddComponent( new AddonComponent( 489 ), 0, -5, 27 );
			AddComponent( new AddonComponent( 1305 ), 0, -4, 7 );
			AddComponent( new AddonComponent( 1308 ), 0, -4, 27 );
			AddComponent( new AddonComponent( 1305 ), 0, -3, 7 );
			AddComponent( new AddonComponent( 1311 ), 0, -3, 27 );
			AddComponent( new AddonComponent( 1305 ), -2, -4, 7 );
			AddComponent( new AddonComponent( 1308 ), -2, -4, 27 );
			AddComponent( new AddonComponent( 1929 ), 2, 5, 0 );
			AddComponent( new AddonComponent( 1305 ), -1, -1, 7 );
			AddComponent( new AddonComponent( 1308 ), -1, -1, 27 );
			AddComponent( new AddonComponent( 1305 ), 0, 1, 7 );
			AddComponent( new AddonComponent( 1311 ), 0, 1, 27 );
			AddComponent( new AddonComponent( 1305 ), -1, 1, 7 );
			AddComponent( new AddonComponent( 1308 ), -1, 1, 27 );
			AddComponent( new AddonComponent( 1305 ), 1, 2, 7 );
			AddComponent( new AddonComponent( 1311 ), 1, 2, 27 );
			AddComponent( new AddonComponent( 1305 ), 2, 2, 7 );
			AddComponent( new AddonComponent( 1308 ), 2, 2, 27 );
			AddComponent( new AddonComponent( 99 ), -3, 4, 0 );
			AddComponent( new AddonComponent( 1305 ), -3, 4, 7 );
			AddComponent( new AddonComponent( 476 ), -3, 4, 7 );
			AddComponent( new AddonComponent( 489 ), -3, 4, 27 );
			AddComponent( new AddonComponent( 1311 ), -3, 4, 27 );
			AddComponent( new AddonComponent( 1313 ), 2, 0, 7 );
			AddComponent( new AddonComponent( 1308 ), 2, 0, 27 );
			AddComponent( new AddonComponent( 100 ), -4, 3, 0 );
			AddComponent( new AddonComponent( 490 ), -4, 3, 27 );
			AddComponent( new AddonComponent( 100 ), -4, 4, 0 );
			AddComponent( new AddonComponent( 479 ), -4, 4, 7 );
			AddComponent( new AddonComponent( 490 ), -4, 4, 27 );
			//AddComponent( new AddonComponent( 3026 ), -4, 5, 7 );
			//AddComponent( new AddonComponent( 3026 ), -3, 5, 5 );
			AddComponent( new AddonComponent( 2978 ), -4, 5, 7 );
			AddComponent( new AddonComponent( 1315 ), 3, -3, 7 );
			AddComponent( new AddonComponent( 1308 ), 3, -3, 27 );
			AddComponent( new AddonComponent( 1305 ), -3, -2, 7 );
			AddComponent( new AddonComponent( 1311 ), -3, -2, 27 );
			AddComponent( new AddonComponent( 1305 ), -3, -3, 7 );
			AddComponent( new AddonComponent( 1308 ), -3, -3, 27 );
			AddComponent( new AddonComponent( 1305 ), -3, -4, 7 );
			AddComponent( new AddonComponent( 1311 ), -3, -4, 27 );
			AddComponent( new AddonComponent( 100 ), -4, 2, 0 );
			AddComponent( new AddonComponent( 490 ), -4, 2, 27 );
			AddComponent( new AddonComponent( 102 ), -4, -5, 0 );
			AddComponent( new AddonComponent( 466 ), -4, -5, 7 );
			AddComponent( new AddonComponent( 491 ), -4, -5, 27 );
			AddComponent( new AddonComponent( 1929 ), 1, 5, 0 );
			AddComponent( new AddonComponent( 1305 ), 2, 3, 7 );
			AddComponent( new AddonComponent( 1311 ), 2, 3, 27 );
			AddComponent( new AddonComponent( 1305 ), 1, 3, 7 );
			AddComponent( new AddonComponent( 1308 ), 1, 3, 27 );
			AddComponent( new AddonComponent( 1929 ), -2, 5, 0 );
			AddComponent( new AddonComponent( 1315 ), -1, 3, 7 );
			AddComponent( new AddonComponent( 1308 ), -1, 3, 27 );
			AddComponent( new AddonComponent( 1929 ), 0, 5, 0 );
			AddComponent( new AddonComponent( 99 ), -2, 4, 0 );
			AddComponent( new AddonComponent( 1305 ), -2, 4, 7 );
			AddComponent( new AddonComponent( 489 ), -2, 4, 27 );
			AddComponent( new AddonComponent( 1308 ), -2, 4, 27 );
			AddComponent( new AddonComponent( 99 ), 4, -5, 0 );
			AddComponent( new AddonComponent( 489 ), 4, -5, 27 );
			AddComponent( new AddonComponent( 1305 ), 4, -4, 7 );
			AddComponent( new AddonComponent( 1308 ), 4, -4, 27 );
			AddComponent( new AddonComponent( 1315 ), 4, -3, 7 );
			AddComponent( new AddonComponent( 1311 ), 4, -3, 27 );
			AddComponent( new AddonComponent( 100 ), 5, -1, 0 );
			AddComponent( new AddonComponent( 1305 ), 5, -1, 7 );
			AddComponent( new AddonComponent( 490 ), 5, -1, 27 );
			AddComponent( new AddonComponent( 1308 ), 5, -1, 27 );
			AddComponent( new AddonComponent( 1305 ), -1, -4, 7 );
			AddComponent( new AddonComponent( 1311 ), -1, -4, 27 );
			AddComponent( new AddonComponent( 99 ), -1, -5, 0 );
			AddComponent( new AddonComponent( 489 ), -1, -5, 27 );
			AddComponent( new AddonComponent( 1315 ), -1, -2, 7 );
			AddComponent( new AddonComponent( 1311 ), -1, -2, 27 );
			AddComponent( new AddonComponent( 1305 ), 0, -2, 7 );
			AddComponent( new AddonComponent( 1308 ), 0, -2, 27 );
			AddComponent( new AddonComponent( 1305 ), -1, 0, 7 );
			AddComponent( new AddonComponent( 1311 ), -1, 0, 27 );
			AddComponent( new AddonComponent( 100 ), -4, -4, 0 );
			AddComponent( new AddonComponent( 478 ), -4, -4, 7 );
			AddComponent( new AddonComponent( 490 ), -4, -4, 27 );
			AddComponent( new AddonComponent( 100 ), -4, -3, 0 );
			AddComponent( new AddonComponent( 490 ), -4, -3, 27 );
			AddComponent( new AddonComponent( 1305 ), -2, 0, 7 );
			AddComponent( new AddonComponent( 1308 ), -2, 0, 27 );
			AddComponent( new AddonComponent( 1305 ), 4, -1, 7 );
			AddComponent( new AddonComponent( 1311 ), 4, -1, 27 );
			AddComponent( new AddonComponent( 1305 ), 4, 0, 7 );
			AddComponent( new AddonComponent( 1308 ), 4, 0, 27 );
			AddComponent( new AddonComponent( 1305 ), 4, 1, 7 );
			AddComponent( new AddonComponent( 1311 ), 4, 1, 27 );
			AddComponent( new AddonComponent( 1315 ), 4, 2, 7 );
			AddComponent( new AddonComponent( 1308 ), 4, 2, 27 );
			AddComponent( new AddonComponent( 99 ), 3, 4, 0 );
			AddComponent( new AddonComponent( 1305 ), 3, 4, 7 );
			AddComponent( new AddonComponent( 489 ), 3, 4, 27 );
			AddComponent( new AddonComponent( 1311 ), 3, 4, 27 );
			AddComponent( new AddonComponent( 1315 ), 3, 3, 7 );
			AddComponent( new AddonComponent( 1308 ), 3, 3, 27 );
			AddComponent( new AddonComponent( 1315 ), -1, 2, 7 );
			AddComponent( new AddonComponent( 1311 ), -1, 2, 27 );
			AddComponent( new AddonComponent( 100 ), -4, -2, 0 );
			AddComponent( new AddonComponent( 490 ), -4, -2, 27 );
			AddComponent( new AddonComponent( 1305 ), 0, 3, 7 );
			AddComponent( new AddonComponent( 1311 ), 0, 3, 27 );
			AddComponent( new AddonComponent( 1313 ), 1, 1, 7 );
			AddComponent( new AddonComponent( 1308 ), 1, 1, 27 );
			AddComponent( new AddonComponent( 1305 ), 2, -1, 7 );
			AddComponent( new AddonComponent( 1311 ), 2, -1, 27 );
			AddComponent( new AddonComponent( 1313 ), 1, -1, 7 );
			AddComponent( new AddonComponent( 1308 ), 1, -1, 27 );
			AddComponent( new AddonComponent( 1305 ), 2, -3, 7 );
			AddComponent( new AddonComponent( 1311 ), 2, -3, 27 );
			AddComponent( new AddonComponent( 1305 ), 2, -2, 7 );
			AddComponent( new AddonComponent( 1308 ), 2, -2, 27 );
			AddComponent( new AddonComponent( 99 ), 2, -5, 0 );
			AddComponent( new AddonComponent( 489 ), 2, -5, 27 );
			AddComponent( new AddonComponent( 1305 ), 2, -4, 7 );
			AddComponent( new AddonComponent( 1308 ), 2, -4, 27 );
			AddComponent( new AddonComponent( 1315 ), -2, -3, 7 );
			AddComponent( new AddonComponent( 1311 ), -2, -3, 27 );
			AddComponent( new AddonComponent( 1315 ), -2, -2, 7 );
			AddComponent( new AddonComponent( 1308 ), -2, -2, 27 );
			AddComponent( new AddonComponent( 99 ), -2, -5, 0 );
			AddComponent( new AddonComponent( 489 ), -2, -5, 27 );
			AddComponent( new AddonComponent( 99 ), -3, -5, 0 );
			AddComponent( new AddonComponent( 476 ), -3, -5, 7 );
			AddComponent( new AddonComponent( 489 ), -3, -5, 27 );
			AddComponent( new AddonComponent( 1305 ), -2, 1, 7 );
			AddComponent( new AddonComponent( 1311 ), -2, 1, 27 );
			AddComponent( new AddonComponent( 1313 ), 0, 0, 7 );
			AddComponent( new AddonComponent( 1308 ), 0, 0, 27 );
			AddComponent( new AddonComponent( 99 ), 1, 4, 0 );
			AddComponent( new AddonComponent( 1305 ), 1, 4, 7 );
			AddComponent( new AddonComponent( 489 ), 1, 4, 27 );
			AddComponent( new AddonComponent( 1311 ), 1, 4, 27 );
			AddComponent( new AddonComponent( 1305 ), 0, 2, 7 );
			AddComponent( new AddonComponent( 1308 ), 0, 2, 27 );
			AddComponent( new AddonComponent( 1305 ), 2, 1, 7 );
			AddComponent( new AddonComponent( 1311 ), 2, 1, 27 );
			AddComponent( new AddonComponent( 1928 ), -3, 5, 0 );
			AddComponent( new AddonComponent( 1315 ), -2, 3, 7 );
			AddComponent( new AddonComponent( 1311 ), -2, 3, 27 );
			AddComponent( new AddonComponent( 1305 ), -3, 3, 7 );
			AddComponent( new AddonComponent( 1308 ), -3, 3, 27 );
			AddComponent( new AddonComponent( 1305 ), 3, -4, 7 );
			AddComponent( new AddonComponent( 1311 ), 3, -4, 27 );
			AddComponent( new AddonComponent( 99 ), 3, -5, 0 );
			AddComponent( new AddonComponent( 489 ), 3, -5, 27 );
			AddComponent( new AddonComponent( 1315 ), 3, -2, 7 );
			AddComponent( new AddonComponent( 1311 ), 3, -2, 27 );
			AddComponent( new AddonComponent( 1315 ), 4, -2, 7 );
			AddComponent( new AddonComponent( 1308 ), 4, -2, 27 );
			AddComponent( new AddonComponent( 100 ), -4, -1, 0 );
			AddComponent( new AddonComponent( 490 ), -4, -1, 27 );
			AddComponent( new AddonComponent( 100 ), -4, 0, 0 );
			AddComponent( new AddonComponent( 490 ), -4, 0, 27 );
			AddComponent( new AddonComponent( 100 ), -4, 1, 0 );
			AddComponent( new AddonComponent( 490 ), -4, 1, 27 );
			AddComponent( new AddonComponent( 1315 ), -1, -3, 7 );
			AddComponent( new AddonComponent( 1308 ), -1, -3, 27 );
			AddComponent( new AddonComponent( 1305 ), -3, 2, 7 );
			AddComponent( new AddonComponent( 1311 ), -3, 2, 27 );
			AddonComponent ac;
			ac = new AddonComponent( 2978 );
			AddComponent( ac, -4, 5, 7 );

		}

		public StoneCityMarketAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	
}