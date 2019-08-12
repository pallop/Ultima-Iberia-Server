using System;
using Server;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using Server.Commands;
using Server.Mobiles;
using UltimaLive;

namespace Server.Items 
{
	public class ForestWinterStone : Item
	{ 
		[Constructable] 
		public ForestWinterStone() :  base( 0x1870 ) 
		{ 
			Weight = 1.0; 
			Hue = 0x5A; 
			Name = "Forest Winter Stone"; 
			LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile m ) 
		{
			PlayerMobile pm = (PlayerMobile) m;

			if ( pm.AccessLevel >= AccessLevel.GameMaster )
			{
				LandTile lt;
				Map map = Map.Trammel;
				for (int x=5150; x < 7168; x++)
				{
					for (int y=1;y < 2020;y++)
					{
						lt = map.Tiles.GetLandTile(x, y);
						if (lt.ID == 282) {new SetLandID(x,y, 1, 312).DoOperation();}
						if (lt.ID == 283) {new SetLandID(x,y, 1, 313).DoOperation();}
						if (lt.ID == 284) {new SetLandID(x,y, 1, 314).DoOperation();}
						if (lt.ID == 285) {new SetLandID(x,y, 1, 315).DoOperation();}
						
					}
				}
			}

			else
			{
				m.SendMessage( "You are unable to use that!" );
				//this.Delete();
			}
		} 

		public ForestWinterStone( Serial serial ) : base( serial ) 
		{ 
		} 
       
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 1 ); // version 
		}

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 
	}
}