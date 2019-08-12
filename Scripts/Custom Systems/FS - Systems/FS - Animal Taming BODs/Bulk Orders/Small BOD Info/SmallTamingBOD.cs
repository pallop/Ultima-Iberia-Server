using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Engines.Craft;

namespace Server.Engines.BulkOrders
{
	[TypeAlias( "Scripts.Engines.BulkOrders.SmallTamingBOD" )]
	public class SmallTamingBOD : SmallMobileBOD
	{

		public override int ComputeFame()
		{
			int bonus = 0;

			bonus += 20;

			return 10 + Utility.Random( bonus );
		}

		public override int ComputeGold()
		{
			int bonus = 0;

			bonus += 500;

			return 750 + Utility.Random( bonus );
		}

		public override ArrayList ComputeRewards()
		{
			if ( Type == null )
				return new ArrayList();

			bool tps5 = false, tps10 = false;
			bool lps5 = false, lps10 = false;
			bool vps5 = false, vps10 = false;
			bool ts5 = false, ts10 = false;
			bool ls5 = false, ls10 = false;
			bool vs5 = false, vs10 = false;
			bool pmd = false;
			bool pbl = false;
			bool dye = false;
			bool lowpps = false;
			bool midpps = false;

			if ( Type.IsSubclassOf( typeof( BaseCreature ) ) )
			{
				BaseCreature bc = null;

				if ( Type != null )
				{
					object o = Activator.CreateInstance( Type );
        				bc = o as BaseCreature;
				}

				if ( bc.MinTameSkill <= 25.0 ) // Tier one Rewards
				{
					if ( AmountMax == 10 )
					{
						pmd = true;
					}
					else if ( AmountMax == 15 )
					{
						pbl = true;
					}
					else if ( AmountMax == 20 )
					{
						ts5 = true;
						ls5 = true;
						vs5 = true;
					}
				}
				else if ( bc.MinTameSkill <= 65.0 ) // Tier two Rewards
				{
					if ( AmountMax == 10 )
					{
						ts5 = true;
						ls5 = true;
						vs5 = true;
					}
					else if ( AmountMax == 15 )
					{
						ts10 = true;
						ls10 = true;
						vs10 = true;
					}
					else if ( AmountMax == 20 )
					{
						lowpps = true;
					}
				}
				else if ( bc.MinTameSkill <= 80.0 ) // Tier three Rewards
				{
					if ( AmountMax == 10 )
					{
						lowpps = true;
					}
					else if ( AmountMax == 15 )
					{
						dye = true;
					}
					else if ( AmountMax == 20 )
					{
						midpps = true;
					}
				}
				else							// Tier four rewards
				{
					if ( AmountMax == 10 )
					{
						midpps = true;
					}
					else if ( AmountMax == 15 )
					{
						tps5 = true;
						lps5 = true;
						vps5 = true;
					}
					else if ( AmountMax == 20 )
					{
						tps10 = true;
						lps10 = true;
						vps10 = true;
					}
				}

				bc.Delete();
			}

			ArrayList list = new ArrayList();

			if ( tps5 )
				list.Add( new PowerScroll( SkillName.AnimalTaming, 105 ) );

			if ( tps10 )
				list.Add( new PowerScroll( SkillName.AnimalTaming, 110 ) );

			if ( lps5 )
				list.Add( new PowerScroll( SkillName.AnimalLore, 105 ) );

			if ( lps10 )
				list.Add( new PowerScroll( SkillName.AnimalLore, 110 ) );

			if ( vps5 )
				list.Add( new PowerScroll( SkillName.Veterinary, 105 ) );
			
			if ( dye )
				list.Add( new PetBleach() );
			
			if ( pbl )
				list.Add( new PetBleach() );
			
			if ( pmd )
				list.Add( new PetSkillCapDeed() );
			
			if ( ts5 )
				list.Add( new SandalsOfAnimalTaming( 5 ) );
			
			if ( ts10 )
				list.Add( new SandalsOfAnimalTaming( 10 ) );
			
			if ( ls5 )
				list.Add( new SandalsOfAnimalLore( 5 ) );
			
			if ( ls10 )
				list.Add( new SandalsOfAnimalLore( 10 ) );
			
			if ( vs5 )
				list.Add( new SandalsOfAnimalLore( 5 ) );
			
			if ( vs10 )
				list.Add( new SandalsOfAnimalLore( 10 ) );

			if ( vps10 )
				list.Add( new PowerScroll( SkillName.Veterinary, 110 ) );

			if ( lowpps )
				list.Add( PetPowerScroll.CreateRandom( 5, 10 ) );
			
			if ( midpps )
				list.Add( PetPowerScroll.CreateRandom( 10, 15 ) );
			
			return list;
		}

		public static SmallTamingBOD CreateRandomFor( Mobile m )
		{
			SmallMobileBulkEntry[] entries;

			entries = SmallMobileBulkEntry.TamingMounts;

			if ( entries.Length > 0 )
			{
				double theirSkill = m.Skills[SkillName.AnimalTaming].Base;
				int amountMax;

				if ( theirSkill >= 70.1 )
					amountMax = Utility.RandomList( 10, 15, 20, 20 );
				else if ( theirSkill >= 50.1 )
					amountMax = Utility.RandomList( 10, 15, 15, 20 );
				else
					amountMax = Utility.RandomList( 10, 10, 15, 20 );

				SmallMobileBulkEntry entry = entries[Utility.Random( entries.Length )];

				if ( entry != null )
					return new SmallTamingBOD( entry, amountMax );
			}

			return null;
		}

		private SmallTamingBOD( SmallMobileBulkEntry entry,  int amountMax )
		{
			this.Hue = 0x1CA;
			this.AmountMax = amountMax;
			this.Type = entry.Type;
			this.AnimalName = entry.AnimalName;
			this.Graphic = entry.Graphic;
		}

		[Constructable]
		public SmallTamingBOD()
		{
			SmallMobileBulkEntry[] entries;

			entries = SmallMobileBulkEntry.TamingMounts;

			if ( entries.Length > 0 )
			{
				int hue = 0x1CA;
				int amountMax = Utility.RandomList( 10, 15, 20 );

				SmallMobileBulkEntry entry = entries[Utility.Random( entries.Length )];

				this.Hue = hue;
				this.AmountMax = amountMax;
				this.Type = entry.Type;
				this.AnimalName = entry.AnimalName;
				this.Graphic = entry.Graphic;
			}
		}

		public SmallTamingBOD( int amountCur, int amountMax, Type type, string animalname, int graphic )
		{
			this.Hue = 0x1CA;
			this.AmountMax = amountMax;
			this.AmountCur = amountCur;
			this.Type = type;
			this.AnimalName = animalname;
			this.Graphic = graphic;
		}

		public SmallTamingBOD( Serial serial ) : base( serial )
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