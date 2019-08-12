using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.BulkOrders
{
	[TypeAlias( "Scripts.Engines.BulkOrders.LargeTamingBOD" )]
	public class LargeTamingBOD : LargeMobileBOD
	{

		public override int ComputeFame()
		{
			int bonus = 0;

			bonus += 50;

			return 100 + Utility.Random( bonus );
		}

		private static int[][][] m_GoldTable = new int[][][]
			{
				new int[][] // Animals
				{
					new int[]{ 500, 750, 1000, 1250, 1500, 1750, 2000, 2225, 2500 },
					new int[]{ 1000, 1250, 1500, 1750, 2000, 2225, 2500, 2750, 3000 },
					new int[]{ 1500, 1750, 2000, 2225, 2500, 2750, 3000, 3225, 3500 }
				},
			};

		public override int ComputeGold()
		{
			Type primaryType;

			if ( Entries.Length > 0 )
				primaryType = Entries[0].Details.Type;
			else
				return 0;

			bool isMount = ( primaryType == typeof( BaseMount ) );
			bool isCreature = ( primaryType == typeof( BaseCreature ) );

			int index;

			if ( isMount )
				index = 2;
			else if ( isCreature )
				index = 1;
			else
				index = 0;

			index *= 2;

			if ( index < m_GoldTable.Length )
			{
				int[][] table = m_GoldTable[index];

				if ( AmountMax >= 20 )
					index = 2;
				else if ( AmountMax >= 15 )
					index = 1;
				else
					index = 0;

				if ( index < table.Length )
				{
					int[] list = table[index];

					if ( index < list.Length )
						return list[index];
				}
			}

			return 0;
		}

		[Constructable]
		public LargeTamingBOD()
		{
			LargeMobileBulkEntry[] entries;

			switch ( Utility.Random( 10 ) )
			{
				default:
				case  0: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Mounts );  break;
				case  1: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.HardMounts ); break;
				case  2: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Dragons ); break;
				case  3: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.FarmAnimals ); break;
				case  4: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Spiders ); break;
				case  5: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Felines ); break;
				case  6: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Kanines ); break;
				case  7: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Bears ); break;
				case  8: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Birds ); break;
				case  9: entries = LargeMobileBulkEntry.ConvertEntries( this, LargeMobileBulkEntry.Rodents ); break;
			}

			int hue = 0x1CA;
			int amountMax = Utility.RandomList( 10, 15, 20, 20 );

			this.Hue = hue;
			this.AmountMax = amountMax;
			this.Entries = entries;
		}

		public LargeTamingBOD( int amountMax, LargeMobileBulkEntry[] entries )
		{
			this.Hue = 0x1CA;
			this.AmountMax = amountMax;
			this.Entries = entries;
		}

		public override ArrayList ComputeRewards()
		{
			Type primaryType;

			if ( Entries.Length > 0 )
				primaryType = Entries[0].Details.Type;
			else
				return new ArrayList();

			bool tps15 = false, tps20 = false;
			bool lps15 = false, lps20 = false;
			bool vps15 = false, vps20 = false;
			bool midpps = false, highpps = false;
			bool ts15 = false, ts20 = false;
			bool ls15 = false, ls20 = false;
			bool vs15 = false, vs20 = false;
			bool rpd = false;
			bool epd = false;
			bool pbd = false;
			bool pcd = false;
			bool tps10 = false, lps10 = false, vps10 = false;

			if ( Entries.Length == 2 )
			{
				if ( AmountMax == 10 )
				{
					tps10 = true;
					lps10 = true;
					vps10 = true;
				}
				else if ( AmountMax == 15 )
				{
					midpps = true;
				}
				else if ( AmountMax == 20 )
				{
					highpps = true;
				}
			}
			else if ( Entries.Length == 3 )
			{
				if ( AmountMax == 10 )
				{
					highpps = true;
				}
				else if ( AmountMax == 15 )
				{
					ts15 = true;
					ls15 = true;
					vs15 = true;
				}
				else if ( AmountMax == 20 )
				{
					ts20 = true;
					ls20 = true;
					vs20 = true;
				}
			}
			else if ( Entries.Length == 4 )
			{
				if ( AmountMax == 10 )
				{
					ts20 = true;
					ls20 = true;
					vs20 = true;
				}
				else if ( AmountMax == 15 )
				{
					rpd = true;
				}
				else if ( AmountMax == 20 )
				{
					epd = true;
				}
			}
			else if ( Entries.Length == 5 )
			{
				if ( AmountMax == 10 )
				{
					epd = true;
				}
				else if ( AmountMax == 15 )
				{
					tps15 = true;
					lps15 = true;
					vps15 = true;
				}
				else if ( AmountMax == 20 )
				{
					tps20 = true;
					lps20 = true;
					vps20 = true;
				}
			}
			else
			{
				if ( AmountMax == 10 )
				{
					tps20 = true;
					lps20 = true;
					vps20 = true;
				}
				else if ( AmountMax == 15 )
				{
					pbd = true;
				}
				else if ( AmountMax == 20 )
				{
					pcd = true;
				}
			}

			ArrayList list = new ArrayList();

			if ( tps15 )
				list.Add( new PowerScroll( SkillName.AnimalTaming, 115 ) );

			if ( tps20 )
				list.Add( new PowerScroll( SkillName.AnimalTaming, 120 ) );

			if ( lps15 )
				list.Add( new PowerScroll( SkillName.AnimalLore, 115 ) );

			if ( lps20 )
				list.Add( new PowerScroll( SkillName.AnimalLore, 120 ) );

			if ( vps15 )
				list.Add( new PowerScroll( SkillName.Veterinary, 115 ) );

			if ( vps20 )
				list.Add( new PowerScroll( SkillName.Veterinary, 120 ) );

            if ( ts15 )
                list.Add( new SandalsOfAnimalTaming( 15 ) );

            if ( ls15 )
                list.Add( new SandalsOfAnimalLore( 15 ) );

            if ( vs15 )
                list.Add( new SandalsOfVeterinary( 15 ) );

            if ( ts20 )
                list.Add( new SandalsOfAnimalTaming( 20 ) );

            if ( ls20 )
                list.Add( new SandalsOfAnimalLore( 20) );

            if ( vs20 )
                list.Add( new SandalsOfVeterinary( 20 ) );

            if ( midpps )
                list.Add( PetPowerScroll.CreateRandom( 10, 15 ) );

            if ( highpps )
                list.Add( PetPowerScroll.CreateRandom( 15, 20 ) );
            
            if ( rpd )
                list.Add( new RarePetDye() );
            
            if ( epd )
                list.Add( new EtherealPetDye() );
            
            if ( pbd )
                list.Add( new PetBondingDeed() );
            
            if ( pcd )
                list.Add( new PetControlSlotDeed() );

			if ( tps10 )
				list.Add( new PowerScroll( SkillName.AnimalTaming, 110 ) );

			if ( lps10 )
				list.Add( new PowerScroll( SkillName.AnimalLore, 110 ) );
			
			if ( vps10 )
				list.Add( new PowerScroll( SkillName.Veterinary, 110 ) );

			return list;
		}

		public LargeTamingBOD( Serial serial ) : base( serial )
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