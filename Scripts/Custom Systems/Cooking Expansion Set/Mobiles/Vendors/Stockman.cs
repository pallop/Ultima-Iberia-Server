using System;
using System.Collections;
using Server;
using Server.Items;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class Stockman : BaseVendor
	{
        private List<SBInfo> m_SBInfos = new List<SBInfo>();
        protected override List<SBInfo> SBInfos { get { return m_SBInfos; } }
		
		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBStockman() );
		}

		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			PlayerMobile pm = (PlayerMobile) from;
			if (( pm.Alive && IsActiveVendor /* && pm.NpcGuild == NpcGuild.RanchersGuild */) || (from.AccessLevel >= AccessLevel.GameMaster))
			{
				if ( IsActiveSeller)
					list.Add( new VendorBuyEntry( from, this ) );

				if ( IsActiveBuyer)
					list.Add( new VendorSellEntry( from, this ) );
			}
		}
				
		[Constructable]
		public Stockman() : base( "the Stockman" )
		{
			SetSkill( SkillName.Herding, 75.0, 100.0 );
			SetSkill( SkillName.AnimalLore, 75.0, 100.0 );
		
   			Hue = Utility.RandomSkinHue();
    		Body = 0x190;
    		Name = NameList.RandomName( "male" );

			HairItemID = Race.RandomHair( Female );
			if (Utility.RandomBool()) FacialHairItemID = Race.RandomFacialHair( Female );
			int hhue = Race.RandomHairHue();
			HairHue = hhue;
			FacialHairHue = hhue;
			
   			SetStr( 86, 100 );
   			SetDex( 81, 95 );
   			SetInt( 61, 75 );
		    SetDamage( 10, 23 );
   			AddItem( new FancyShirt(Utility.RandomNeutralHue()));
   			AddItem( new Shoes()); 
   			AddItem( new TricorneHat());
   			AddItem( new ShepherdsCrook());
			AddItem( new Cloak( 96 ));
			AddItem( new ShortPants( Utility.RandomBirdHue()));
		}
		
		public override void InitOutfit()
		{
		}
		
		public Stockman( Serial serial ) : base( serial )
		{
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
