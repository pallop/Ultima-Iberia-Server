using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a deer corpse" )]
	[TypeAlias( "Server.Mobiles.Greathart" )]
	public class WildGreatHart : BaseAnimal // BaseCreature
	{
		[Constructable]
		public WildGreatHart() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Activity = ActivityType.Twilight;
			Eats = EatType.Herbivore;
			Grouping = GroupingType.Herd;
			
			MateActive = true;
			LitterSize = 1;
			MaxAge = 30;
			MatingSeason = Seasons.Any;

			Name = "a great hart";
			Body = 0xEA;

			SetStr( 41, 71 );
			SetDex( 47, 77 );
			SetInt( 27, 57 );

			SetHits( 27, 41 );
			SetMana( 0 );

			SetDamage( 5, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Cold, 5, 10 );

			SetSkill( SkillName.MagicResist, 26.8, 44.5 );
			SetSkill( SkillName.Tactics, 29.8, 47.5 );
			SetSkill( SkillName.Wrestling, 29.8, 47.5 );

			Fame = 300;
			Karma = 0;

			VirtualArmor = 24;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 59.1;
		}
		
		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Female = false;
			TypeName = "a deer";
		}
		
		public override void MakeBaby()
		{
			SetStr( 5,15 );
			SetDex( 5,20 );
			SetInt( 5,10 );
			SetDamage( 1 );
			Name = "a fawn";
			base.MakeBaby();
		}
		
		public override void MakeYoung()
		{
			SetStr( 20,60 );
			SetDex( 30,60 );
			SetInt( 15,30 );
			SetDamage( 2,5 );
			if (Name == "a fawn") 
				Name = "a buck";
			base.MakeYoung();
		}
		
		public override void MakeAdult()
		{
			SetStr( 41, 71 );
			SetDex( 47, 77 );
			SetInt( 27, 57 );
			SetDamage( 5, 9 );
			if (Name == "a buck" || Name == "a fawn") 
				Name = "a great hart";
			base.MakeAdult();
		}
		
		public override void MakeSenior()
		{
			SetStr( 30,65 );
			SetDex( 40,65 );
			SetInt( 15,40 );
			SetDamage( 3,8 );
			if (Name == "a great hart" || Name == "a buck" || Name == "a fawn") 
				Name = "an old stag";
			base.MakeSenior();
		}

		public override void OnCarve( Mobile from, Corpse corpse, Item with)
		{
			base.OnCarve( from, corpse, with );
			double meatbonus = 1;
			if (Age == AgeDescription.Baby)
			{
				meatbonus += 0;
			}
			else if (Age == AgeDescription.Young)
			{
				meatbonus += 1;
			}
			else if (Age == AgeDescription.Adult)
			{
				meatbonus += 3;
			}
			else if (Age == AgeDescription.Senior)
			{
				meatbonus += 2;
			}
            corpse.Carved = true;
			DropResources(from, meatbonus, corpse);
			corpse.DropItem(new DeerHide((int)meatbonus + 2));
			//if (((int)(meatbonus *10))>1) corpse.DropItem( new RawRibs((int)(meatbonus * 10)));
			//corpse.DropItem( new BeefHock( 4 ) );
		}
		
		public virtual void DropResources(Mobile from, double meatbonus, Corpse corpse)
		{
			bool CanUse = from.CheckSkill( SkillName.Anatomy, 30, 70 );
			if (meatbonus < 1) from.SendMessage("Nothing but grisle!");
			if (meatbonus > 0) corpse.DropItem( new RawVenisonSteak(2));
			if (meatbonus > 1 && CanUse) corpse.DropItem( new RawVenisonRoast(2));
			if (meatbonus > 2) corpse.DropItem( new RawVenisonSteak(4));
			if (meatbonus > 3 && CanUse) corpse.DropItem( new RawVenisonRoast(2));
		}
		
		//public override int Meat{ get{ return 6; } }
		//public override int Hides{ get{ return 15; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public WildGreatHart(Serial serial) : base(serial)
		{
		}

		public override int GetAttackSound() 
		{ 
			return 0x82; 
		} 

		public override int GetHurtSound() 
		{ 
			return 0x83; 
		} 

		public override int GetDeathSound() 
		{ 
			return 0x84; 
		} 

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}