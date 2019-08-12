using System; 
using Server; 
using System.Collections; 
using Server.Gumps; 
using Server.Items; 
using Server.Network; 
using Server.Targeting; 
using Server.ContextMenus; 

namespace Server.Mobiles 
{ 
	public class AnimalTrader : BaseCreature//was BaseVendor 
	{
		[Constructable]
		public AnimalTrader() : base(AIType.AI_Thief, FightMode.None, 10, 1, 0.4, 1.6 ) 
		{ 
			InitStats( 85, 75, 65 ); 
			Name = "Mildred";
			Title = "the Animal Trader";
			
			Body = 0x191;
			Hue = Utility.RandomSkinHue(); 
			
			AddItem( new FancyShirt(Utility.RandomNeutralHue()));
   			AddItem( new Shoes()); 
   			AddItem( new TricorneHat());
   			AddItem( new ShepherdsCrook());
			AddItem( new Cloak(96));
			AddItem( new Skirt(Utility.RandomBirdHue()));

            Blessed = true;
			
			HairItemID = 0x203C;   // The ItemID of the hair you want
			HairHue = 1175; 
		}
		
		private class PetSaleTarget : Target 
		{ 
			private AnimalTrader m_Trader; 

			public PetSaleTarget( AnimalTrader trader ) : base( 12, false, TargetFlags.None ) 
			{ 
				m_Trader = trader; 
			} 

			protected override void OnTarget( Mobile from, object targeted ) 
			{
                if (targeted is BaseCreature)
                {
                    m_Trader.EndPetSale(from, (BaseCreature)targeted);
                }
                else if (targeted == from)
                {
                    m_Trader.SayTo(from, 502672); // HA HA HA! Sorry, I am not an inn. 
                }
				
			} 
		} 

		public void BeginPetSale( Mobile from ) 
		{ 
			if ( Deleted || !from.CheckAlive() ) 
			return; 

			SayTo( from, "Which beast are you selling?" ); 

			from.Target = new PetSaleTarget( this ); 
			
		} 

		//RUFO beginfunction
		private void SellPetForGold(Mobile from, BaseCreature pet, int goldamount)
		{
			Item gold = new Gold(goldamount);
			pet.ControlTarget = null; 
			pet.ControlOrder = OrderType.None; 
			pet.Internalize(); 
			pet.SetControlMaster( null ); 
			pet.SummonMaster = null;
			pet.Delete();
			
			Container backpack = from.Backpack;
			if ( backpack == null || !backpack.TryDropItem( from, gold, false ) ) 
			{ 
				gold.MoveToWorld( from.Location, from.Map );
				SayTo( from, "Thank you for the Beast." );
			}

		}
		//RUFO endfunction


		public void EndPetSale( Mobile from, BaseCreature pet ) 
		{ 
			if ( Deleted || !from.CheckAlive() ) 
			return;

			if (pet is BaseAnimal)
			{
				BaseAnimal ba = (BaseAnimal) pet;
				if (from != ba.Owner && ba.Owner != null)
				{
					SayTo(from, 1042562);
					return;
				}
				else if ((ba.Owner == null) && (!pet.Controlled || pet.ControlMaster != from ))
				{
					SayTo(from, 1042562);
					return;
				}
			}
			else if ( !pet.Controlled || pet.ControlMaster != from ) 
			{
				SayTo( from, 1042562 ); // You do not own that pet! 
				return;
			}
			
			if ( pet.IsDeadPet ) SayTo( from, 1049668 ); // Living pets only, please. 
			else if ( pet.Summoned ) SayTo( from, "That is a summoned creature!" ); 
			else if ( pet.Body.IsHuman ) SayTo( from, 502672 ); // HA HA HA! Sorry, I am not an inn. 
			else if ( (pet is PackLlama || pet is PackHorse || pet is Beetle) && (pet.Backpack != null && pet.Backpack.Items.Count > 0) ) SayTo( from, 1042563 ); // You need to unload your pet. 
			else if ( pet.Combatant != null && pet.InRange( pet.Combatant, 12 ) && pet.Map == pet.Combatant.Map ) SayTo( from, 1042564 ); // I'm sorry.  Your pet seems to be busy. 
			else 
			{ 
				if ( pet is FarmChicken )
				{
					FarmChicken ba = (FarmChicken) pet;
					double sellbonus = 0;
					if (ba.MotherBreed == ChickenBreed.Leghorn || ba.FatherBreed == ChickenBreed.Leghorn)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.MotherBreed == ChickenBreed.Barnevelder || ba.FatherBreed == ChickenBreed.Barnevelder)
					{
						sellbonus += (ba.IsPurebred() ? 4.0 : 2.0 );
					}
					if (ba.MotherBreed == ChickenBreed.Orpington || ba.FatherBreed == ChickenBreed.Orpington)
					{
						sellbonus += (ba.IsPurebred() ? 2.0 : 1.0 );
					}
					if (ba.MotherBreed == ChickenBreed.Poltava || ba.FatherBreed == ChickenBreed.Poltava)
					{
						sellbonus += (ba.IsPurebred() ? 2.0 : 1.0 );
					}
					if (ba.MotherBreed == ChickenBreed.Bresse || ba.FatherBreed == ChickenBreed.Bresse)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.MotherBreed == ChickenBreed.Braekel || ba.FatherBreed == ChickenBreed.Braekel)
					{
						sellbonus += (ba.IsPurebred() ? 3.0 : 1.5 );
					}
					if (ba.Age == AgeDescription.Baby)
					{
						sellbonus += 1.0;
					}
					else if (ba.Age == AgeDescription.Young)
					{
						sellbonus += 4.0;
					}
					else if (ba.Age == AgeDescription.Adult)
					{
						sellbonus += 6.0;
					}
					else if (ba.Age == AgeDescription.Senior)
					{
						sellbonus -= 10.0;
					}
					sellbonus += (ba.Female? 10:0);
					SellPetForGold(from, pet, 15 + (int)sellbonus);
				}
				else if (pet is PackHorse ) SellPetForGold(from, pet, 303);
				else if (pet is Rabbit ) SellPetForGold(from, pet, 39);
				else if (pet is PackLlama ) SellPetForGold(from, pet, 245);
				else if (pet is Dog ) SellPetForGold(from, pet, 90);
				else if (pet is Cat ) SellPetForGold(from, pet, 69);
				else if (pet is Pig )
				{
					FarmPig ba = (FarmPig) pet;
					double sellbonus = 0;
					if (ba.MotherBreed == PigBreed.Duroc || ba.FatherBreed == PigBreed.Duroc)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.MotherBreed == PigBreed.Iberian || ba.FatherBreed == PigBreed.Iberian)
					{
						sellbonus += (ba.IsPurebred() ? 4.0 : 2.0 );
					}
					if (ba.MotherBreed == PigBreed.Tamworth || ba.FatherBreed == PigBreed.Tamworth)
					{
						sellbonus += (ba.IsPurebred() ? 2.0 : 1.0 );
					}
					if (ba.MotherBreed == PigBreed.White || ba.FatherBreed == PigBreed.White)
					{
						sellbonus += (ba.IsPurebred() ? 2.0 : 1.0 );
					}
					if (ba.MotherBreed == PigBreed.Feral || ba.FatherBreed == PigBreed.Feral)
					{
						sellbonus -= (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.Age == AgeDescription.Baby)
					{
						sellbonus += 5.0;
					}
					else if (ba.Age == AgeDescription.Young)
					{
						sellbonus += 10.0;
					}
					else if (ba.Age == AgeDescription.Adult)
					{
						sellbonus += 15.0;
					}
					else if (ba.Age == AgeDescription.Senior)
					{
						sellbonus -= 20.0;
					}
					sellbonus += (ba.Female? 10:0);
					SellPetForGold(from, pet, 40 + (int)sellbonus);
				}
				else if (pet is WildBoar )
				{
					WildBoar ba = (WildBoar) pet;
					double sellbonus = 0;
					if (ba.MotherBreed == PigBreed.Duroc || ba.FatherBreed == PigBreed.Duroc)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.MotherBreed == PigBreed.Iberian || ba.FatherBreed == PigBreed.Iberian)
					{
						sellbonus += (ba.IsPurebred() ? 4.0 : 2.0 );
					}
					if (ba.MotherBreed == PigBreed.Tamworth || ba.FatherBreed == PigBreed.Tamworth)
					{
						sellbonus += (ba.IsPurebred() ? 2.0 : 1.0 );
					}
					if (ba.MotherBreed == PigBreed.White || ba.FatherBreed == PigBreed.White)
					{
						sellbonus += (ba.IsPurebred() ? 2.0 : 1.0 );
					}
					if (ba.MotherBreed == PigBreed.Feral || ba.FatherBreed == PigBreed.Feral)
					{
						sellbonus -= (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.Age == AgeDescription.Baby)
					{
						sellbonus += 5.0;
					}
					else if (ba.Age == AgeDescription.Young)
					{
						sellbonus += 10.0;
					}
					else if (ba.Age == AgeDescription.Adult)
					{
						sellbonus += 15.0;
					}
					else if (ba.Age == AgeDescription.Senior)
					{
						sellbonus -= 20.0;
					}
					sellbonus += (ba.Female? 10:0);
					SellPetForGold(from, pet, 40 + (int)sellbonus);
				}
				else if (pet is Horse ) SellPetForGold(from, pet, 250);
				else if (pet is WildHorse )
				{
					WildHorse ba = (WildHorse) pet;
					double sellbonus = 0;
					if (ba.MotherBreed == HorseBreed.Andalusian || ba.FatherBreed == HorseBreed.Andalusian)
					{
						sellbonus += (ba.IsPurebred() ? 10.0 : 5.0 );
					}
					if (ba.MotherBreed == HorseBreed.Arabian || ba.FatherBreed == HorseBreed.Arabian)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.MotherBreed == HorseBreed.Appaloosa || ba.FatherBreed == HorseBreed.Appaloosa)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.MotherBreed == HorseBreed.Haflinger || ba.FatherBreed == HorseBreed.Haflinger)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.MotherBreed == HorseBreed.Thoroughbred || ba.FatherBreed == HorseBreed.Thoroughbred)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.MotherBreed == HorseBreed.Hackney || ba.FatherBreed == HorseBreed.Hackney)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.Age == AgeDescription.Baby)
					{
						sellbonus += 5.0;
					}
					else if (ba.Age == AgeDescription.Young)
					{
						sellbonus += 10.0;
					}
					else if (ba.Age == AgeDescription.Adult)
					{
						sellbonus += 15.0;
					}
					else if (ba.Age == AgeDescription.Senior)
					{
						sellbonus -= 20.0;
					}
					sellbonus += (ba.Female? 10:0);
					SellPetForGold(from, pet, 50 + (int)sellbonus);
				}
				else if (pet is ForestOstard ) SellPetForGold(from, pet, 301);
				else if (pet is Cow )
				{
					FarmCow ba = (FarmCow) pet;
					double sellbonus = 0;
					if (ba.MotherBreed == CowBreed.Holstein || ba.FatherBreed == CowBreed.Holstein)
					{
						sellbonus += (ba.IsPurebred() ? 10.0 : 5.0 );
					}
					if (ba.MotherBreed == CowBreed.Guernsey || ba.FatherBreed == CowBreed.Guernsey)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.MotherBreed == CowBreed.Hereford || ba.FatherBreed == CowBreed.Hereford)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.MotherBreed == CowBreed.Angus || ba.FatherBreed == CowBreed.Angus)
					{
						sellbonus += (ba.IsPurebred() ? 10.0 : 5.0 );
					}
					if (ba.MotherBreed == CowBreed.Gloucester || ba.FatherBreed == CowBreed.Gloucester)
					{
						sellbonus += (ba.IsPurebred() ? 3.0 : 1.5 );
					}
					if (ba.MotherBreed == CowBreed.Montbeliarde || ba.FatherBreed == CowBreed.Montbeliarde)
					{
						sellbonus += (ba.IsPurebred() ? 3.0 : 1.5 );
					}
					if (ba.MotherBreed == CowBreed.Corriente || ba.FatherBreed == CowBreed.Corriente)
					{
						sellbonus += (ba.IsPurebred() ? 4.0 : 2.0 );
					}
					if (ba.MotherBreed == CowBreed.ToroBravo || ba.FatherBreed == CowBreed.ToroBravo)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.Age == AgeDescription.Baby)
					{
						sellbonus += 5.0;
					}
					else if (ba.Age == AgeDescription.Young)
					{
						sellbonus += 10.0;
					}
					else if (ba.Age == AgeDescription.Adult)
					{
						sellbonus += 15.0;
					}
					else if (ba.Age == AgeDescription.Senior)
					{
						sellbonus -= 20.0;
					}
					sellbonus += (ba.Female? 10:0);
					SellPetForGold(from, pet, 70 + (int)sellbonus);
				}
				else if (pet is FarmBull )
				{
					FarmBull ba = (FarmBull) pet;
					double sellbonus = 0;
					if (ba.MotherBreed == CowBreed.Holstein || ba.FatherBreed == CowBreed.Holstein)
					{
						sellbonus += (ba.IsPurebred() ? 10.0 : 5.0 );
					}
					if (ba.MotherBreed == CowBreed.Guernsey || ba.FatherBreed == CowBreed.Guernsey)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.MotherBreed == CowBreed.Hereford || ba.FatherBreed == CowBreed.Hereford)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.MotherBreed == CowBreed.Angus || ba.FatherBreed == CowBreed.Angus)
					{
						sellbonus += (ba.IsPurebred() ? 10.0 : 5.0 );
					}
					if (ba.MotherBreed == CowBreed.Gloucester || ba.FatherBreed == CowBreed.Gloucester)
					{
						sellbonus += (ba.IsPurebred() ? 3.0 : 1.5 );
					}
					if (ba.MotherBreed == CowBreed.Montbeliarde || ba.FatherBreed == CowBreed.Montbeliarde)
					{
						sellbonus += (ba.IsPurebred() ? 3.0 : 1.5 );
					}
					if (ba.MotherBreed == CowBreed.Corriente || ba.FatherBreed == CowBreed.Corriente)
					{
						sellbonus += (ba.IsPurebred() ? 4.0 : 2.0 );
					}
					if (ba.MotherBreed == CowBreed.ToroBravo || ba.FatherBreed == CowBreed.ToroBravo)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.Age == AgeDescription.Baby)
					{
						sellbonus += 5.0;
					}
					else if (ba.Age == AgeDescription.Young)
					{
						sellbonus += 10.0;
					}
					else if (ba.Age == AgeDescription.Adult)
					{
						sellbonus += 15.0;
					}
					else if (ba.Age == AgeDescription.Senior)
					{
						sellbonus -= 20.0;
					}
					sellbonus += (ba.Female? 10:0);
					SellPetForGold(from, pet, 100 + (int)sellbonus);
				}
				else if (pet is Hind )
				SellPetForGold(from, pet, 75);
				else if (pet is GreatHart )
				SellPetForGold(from, pet, 200);
				else if (pet is Eagle )
				SellPetForGold(from, pet, 201);
				else if (pet is Sheep )
				{
					FarmSheep ba = (FarmSheep) pet;
					double sellbonus = 0;
					if (ba.MotherBreed == SheepBreed.Cormo || ba.FatherBreed == SheepBreed.Cormo)
					{
						sellbonus += (ba.IsPurebred() ? 8.0 : 4.0 );
					}
					if (ba.MotherBreed == SheepBreed.Cotswold || ba.FatherBreed == SheepBreed.Cotswold)
					{
						sellbonus += (ba.IsPurebred() ? 7.0 : 3.5 );
					}
					if (ba.MotherBreed == SheepBreed.Swaledale || ba.FatherBreed == SheepBreed.Swaledale)
					{
						sellbonus += (ba.IsPurebred() ? 6.0 : 3.0 );
					}
					if (ba.MotherBreed == SheepBreed.Coopworth || ba.FatherBreed == SheepBreed.Coopworth)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.0 );
					}
					if (ba.MotherBreed == SheepBreed.Racka || ba.FatherBreed == SheepBreed.Racka)
					{
						sellbonus += (ba.IsPurebred() ? 10.0 : 5.0 );
					}
					if (ba.MotherBreed == SheepBreed.Latxa || ba.FatherBreed == SheepBreed.Latxa)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.MotherBreed == SheepBreed.Awassi || ba.FatherBreed == SheepBreed.Awassi)
					{
						sellbonus += (ba.IsPurebred() ? 4.0 : 2.0 );
					}
					if (ba.Age == AgeDescription.Baby)
					{
						sellbonus += 2.0;
					}
					else if (ba.Age == AgeDescription.Young)
					{
						sellbonus += 5.0;
					}
					else if (ba.Age == AgeDescription.Adult)
					{
						sellbonus += 10.0;
					}
					else if (ba.Age == AgeDescription.Senior)
					{
						sellbonus -= 20.0;
					}
					sellbonus += (ba.Female? 10:0);
					SellPetForGold(from, pet, 60 + (int)sellbonus);
				}
				else if (pet is FarmGoat )
				{
					FarmGoat ba = (FarmGoat) pet;
					double sellbonus = 0;
					if (ba.MotherBreed == GoatBreed.Pyrenean || ba.FatherBreed == GoatBreed.Pyrenean)
					{
						sellbonus += (ba.IsPurebred() ? 8.0 : 4.0 );
					}
					if (ba.MotherBreed == GoatBreed.Saanen || ba.FatherBreed == GoatBreed.Saanen)
					{
						sellbonus += (ba.IsPurebred() ? 7.0 : 3.5 );
					}
					if (ba.MotherBreed == GoatBreed.Angora || ba.FatherBreed == GoatBreed.Angora)
					{
						sellbonus += (ba.IsPurebred() ? 10.0 : 5.0 );
					}
					if (ba.MotherBreed == GoatBreed.Cashmere || ba.FatherBreed == GoatBreed.Cashmere)
					{
						sellbonus += (ba.IsPurebred() ? 12.0 : 6.0 );
					}
					if (ba.MotherBreed == GoatBreed.Boer || ba.FatherBreed == GoatBreed.Boer)
					{
						sellbonus += (ba.IsPurebred() ? 5.0 : 2.5 );
					}
					if (ba.MotherBreed == GoatBreed.Stiefelgeiss || ba.FatherBreed == GoatBreed.Stiefelgeiss)
					{
						sellbonus += (ba.IsPurebred() ? 4.0 : 2.0 );
					}
					if (ba.Age == AgeDescription.Baby)
					{
						sellbonus += 2.0;
					}
					else if (ba.Age == AgeDescription.Young)
					{
						sellbonus += 5.0;
					}
					else if (ba.Age == AgeDescription.Adult)
					{
						sellbonus += 10.0;
					}
					else if (ba.Age == AgeDescription.Senior)
					{
						sellbonus -= 20.0;
					}
					sellbonus += (ba.Female? 10:0);
					SellPetForGold(from, pet, 60 + (int)sellbonus);
				}
				else if (pet is BlackBear )
				SellPetForGold(from, pet, 317);
				else if (pet is Bird )
				SellPetForGold(from, pet, 25);
				else if (pet is TimberWolf )
				SellPetForGold(from, pet, 384);
				else if (pet is GreyWolf )
				SellPetForGold(from, pet, 384);
				else if (pet is DireWolf )
				SellPetForGold(from, pet, 384);
				else if (pet is BlackBear )
				SellPetForGold(from, pet, 210);
				else if (pet is Panther )
				SellPetForGold(from, pet, 635);
				else if (pet is Cougar )
				SellPetForGold(from, pet, 635);
				else if (pet is BrownBear )
				SellPetForGold(from, pet, 427);
				else if (pet is GrizzlyBear )
				SellPetForGold(from, pet, 883);
				else if (pet is Rat )
				SellPetForGold(from, pet, 53);
				else if (pet is RidableLlama )
				SellPetForGold(from, pet, 101);
				else if (pet is Llama )
				SellPetForGold(from, pet, 150);
				else 
				SayTo( from, "I dont want that Beast, go away" ); // You can't PetSale that!
			}
		}
		
		public override bool HandlesOnSpeech( Mobile from ) 
		{ 
			return true; 
		} 

		public override void OnSpeech( SpeechEventArgs e ) 
		{
			if( e.Mobile.InRange( this, 4 ))//change this value to a 1 or 2 to make the range smaller
			{
				if ( ( e.Speech.ToLower() == "sell" ) )//was sellpet
				{
					BeginPetSale( e.Mobile );
				}
				else 
				{ 
					base.OnSpeech( e ); 
				}
			}
		} 

		public AnimalTrader( Serial serial ) : base( serial ) 
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