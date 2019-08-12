using System;
using Server.Mobiles;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a horse corpse" )]
	[TypeAlias( "Server.Mobiles.BrownHorse", "Server.Mobiles.DirtyHorse", "Server.Mobiles.GrayHorse", "Server.Mobiles.TanHorse" )]
	public class Horse : BaseMount
	{
		private static int[] m_IDs = new int[]
			{
				0xC8, 0x3E9F,
				0xE2, 0x3EA0,
				0xE4, 0x3EA1,
				0xCC, 0x3EA2
			};

		[Constructable]
		public Horse() : this( "a horse" )
		{
		}

		[Constructable]
		public Horse( string name ) : base( name, 0xE2, 0x3EA0, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			int random = Utility.Random( 4 );

			Body = m_IDs[random * 2];
			ItemID = m_IDs[random * 2 + 1];
			BaseSoundID = 0xA8;

			SetStr( 22, 98 );
			SetDex( 56, 75 );
			SetInt( 6, 10 );

			SetHits( 28, 45 );
			SetMana( 0 );

			SetDamage( 3, 4 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );

			SetSkill( SkillName.MagicResist, 25.1, 30.0 );
			SetSkill( SkillName.Tactics, 29.3, 44.0 );
			SetSkill( SkillName.Wrestling, 29.3, 44.0 );

			Fame = 300;
			Karma = 300;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 29.1;
		}

	/*	*************************************************	*/
	/* 	This mod is intended to allow for horse stealing.	*/
	/*	***************** By: SHAMBAMPOW ****************	*/


		public override void OnDoubleClick( Mobile from )
		{
			if( !this.Controlled )
			{
				base.OnDoubleClick(from);
				return;
			}
			if( this.ControlMaster == from || this.ControlMaster.AccessLevel > from.AccessLevel || from.AccessLevel > this.ControlMaster.AccessLevel )
			{
				base.OnDoubleClick(from);
				return;
			}
			else if( Server.Misc.NotorietyHandlers.Mobile_AllowHarmful( from, this ) )	// Only work if you can do harmful actions to them (Aka, in fel, guild memebers, apposing factions, etc)
			{
				if ( IsDeadPet )
					return;

				if ( from.IsBodyMod && !from.Body.IsHuman )
				{
					if ( Core.AOS ) // You cannot ride a mount in your current form.
						PrivateOverheadMessage( Network.MessageType.Regular, 0x3B2, 1062061, from.NetState );
					else
						from.SendLocalizedMessage( 1061628 ); // You can't do that while polymorphed.
					return;
				}
				if ( from.Mounted )
				{
					from.SendLocalizedMessage( 1005583 ); // Please dismount first.
					return;
				}
				if ( !Multis.DesignContext.Check( from ) )
					return;

				if ( from.HasTrade )
				{
					from.SendLocalizedMessage( 1042317, "", 0x41 ); // You may not ride at this time
					return;
				}

				if ( from.InRange( this, 1 ) )
				{
					if( !this.Controlled )
					{
						base.OnDoubleClick(from);
						return;
					}
					if( this.Controlled && from != this.ControlMaster )	// If the horse is tamed, and the one double clicking is not the owner of the horse
					{
						if( from.Skills[SkillName.Stealing].Value >= MinTameSkill )	// Only attempt to steal if you have stealing skill
						{
							double stealChance = 0.50;	// Base chance to steal successfully without skill bonuses
							double crimChance = 0.50;	// Base chance to go criminal without skill bonuses

							if( from.Skills[SkillName.Stealing].Value >= 100.0 )
							{
								stealChance+=0.10;	// 10% extra chance if player has 120.0 stealing (60% total)

								if( from.Skills[SkillName.Stealing].Value >= 120.0 )
									stealChance+=0.10;	// 10% extra chance if player has 120.0 stealing (on top of GM Stealing Bonus) (70% total)
							}
							if( Utility.RandomDouble() < stealChance )
							{
								
								this.ControlMaster = from;
								this.Rider = from;
						
								if( this.IsBonded )
								{
									this.IsBonded = false;
								}
								from.SendMessage( 1153, "You steal the horse!" );
								//from.SendMessage( "Your stealChance is {0}%", stealChance*100 );	// Uncomment for debug purposes
							}
							else
							{
								from.SendMessage( 1150, "You fail to steal the horse." );
							}
							if( from.Skills[SkillName.Stealing].Value >= 100.0 )
							{
								crimChance-=0.10;	// 10% less chance if player has 120.0 stealing (40% total)

								if( from.Skills[SkillName.Stealing].Value >= 120.0 )
									crimChance-=0.10;	// 10% less chance if player has 120.0 stealing (on top of GM Stealing Bonus) (30% total)
							}
							if( Utility.RandomDouble() < crimChance )	// chance to turn grey.
							{
								from.Criminal = true;
								from.SendMessage( "You have been flagged criminal!" );
								//from.SendMessage( "Your crimChance is {0%}", crimChance*100 );	// Uncomment for debug purposes
							}
							else
							{
								from.SendMessage( "Your actions go unnoticed." );
							}
						}
						else
						{
							from.SendMessage("You do not have enough stealing skill to steal this mount!");
							base.OnDoubleClick(from);
							return;
						}
					}
				}
				else
				{
					from.SendLocalizedMessage( 500206 ); // That is too far away to ride.
				}
			}
		}

		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 10; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public Horse( Serial serial ) : base( serial )
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