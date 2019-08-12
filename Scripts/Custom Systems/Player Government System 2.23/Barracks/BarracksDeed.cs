using Server;
using System;
using Server.Items;
using Server.Multis;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;

namespace Server.Items
{
	public class BarracksAddonDeed : CityDeed
	{
		[Constructable]
		public BarracksAddonDeed() : base( 0x13F9, new Point3D( 0, 5, 0 ) )
		{
			Name = "a barracks deed";
			Type = CivicStrutureType.Barracks;
		}

		public BarracksAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			PlayerMobile pm = (PlayerMobile)from;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if ( pm.City != null )
			{
                                 ArmyController army = from.Backpack.FindItemByType( typeof(ArmyController)  ) as ArmyController;
                                
				if ( pm.City.Mayor != from && pm.City.AssistMayor != from && pm.City.General != from)
				{
					from.SendMessage( "You must be the mayor, assistant mayor, or general of a city to place this structure." );
				}
				else if ( !PlayerGovernmentSystem.IsAtCity( from ) )
				{
					from.SendMessage( "You must be inside your city to place this structure." );
				}
                                else if ( army != null )
				{
					from.SendMessage( "You appear to already have an army barracks. You may only control one army." );
				}
				else if ( !PlayerGovernmentSystem.IsCityLevelReached( pm.City.Mayor, 3 ) )
				{
					from.SendMessage( "Your city must be at least level 3 before you can place this structure." );
				}
				else if ( PlayerGovernmentSystem.NeedsForensics && from.Skills[SkillName.Forensics].Base < 40.0 )
				{
					from.SendMessage( "You lack the required skill to place this building, You need at least 40.0 points in forensics." );
				}
				else
				{
					base.OnDoubleClick( from );
				}
			}
			else
			{
				from.SendMessage( "You must be the mayor, assistant mayor, or general of a city in order to use this." );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
