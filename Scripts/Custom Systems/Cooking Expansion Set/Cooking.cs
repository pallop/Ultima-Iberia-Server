using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class UtilityItem
	{
		static public int RandomChoice( int itemID1, int itemID2 )
		{
			int iRet = 0;
			switch ( Utility.Random( 2 ) )
			{
				default:
				case 0: iRet = itemID1; break;
				case 1: iRet = itemID2; break;
			}
			return iRet;
		}
	}
}