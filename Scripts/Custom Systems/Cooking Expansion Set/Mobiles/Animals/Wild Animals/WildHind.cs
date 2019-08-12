using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a deer corpse" )]
	public class WildHind : BaseAnimal //BaseCreature
	{
		[Constructable]
		public WildHind() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Activity = ActivityType.Twilight;
			Eats = EatType.Herbivore;
			Grouping = GroupingType.Herd;
			
			MateActive = true;
			LitterSize = 1;
			MaxAge = 30;
            MatingSeason = Seasons.Spring;

			Name = "a hind";
			Body = 0xED;

			SetStr( 21, 51 );
			SetDex( 47, 77 );
			SetInt( 17, 47 );

			SetHits( 15, 29 );
			SetMana( 0 );

			SetDamage( 4 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 5, 15 );
			SetResistance( ResistanceType.Cold, 5 );

			SetSkill( SkillName.MagicResist, 15.0 );
			SetSkill( SkillName.Tactics, 19.0 );
			SetSkill( SkillName.Wrestling, 26.0 );

			Fame = 300;
			Karma = 0;

			VirtualArmor = 8;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 23.1;
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Female = true;
			TypeName = "a deer";
		}
		
		public override void MakeBaby()
		{
			SetStr( 3,15 );
			SetDex( 5,15 );
			SetInt( 3,8 );
			SetDamage( 1 );
			Name = "a fawn";
			base.MakeBaby();
		}
		
		public override void MakeYoung()
		{
			SetStr( 10,20 );
			SetDex( 30,40 );
			SetInt( 10,30 );
			SetDamage( 2 );
			if (Name == "a fawn") 
				Name = "a doe";
			base.MakeYoung();
		}
		
		public override void MakeAdult()
		{
			SetStr( 21, 51 );
			SetDex( 47, 77 );
			SetInt( 17, 47 );
			SetDamage( 4 );
			if (Name == "a doe" || Name == "a fawn") 
				Name = "a hind";
			base.MakeAdult();
		}
		
		public override void MakeSenior()
		{
			SetStr( 5 );
			SetDex( 20 );
			SetInt( 8 );
			SetDamage( 3 );
			if (Name == "a hind" || Name == "a doe" || Name == "a fawn") 
				Name = "a hind";
			base.MakeSenior();
		}
		
		public override void CreateBaby()
		{
			for (int j = 0; j < this.LitterSize; j++) 
			{
				int gender = Utility.RandomMinMax(1,4);
				if (gender == 1) 
				{
					WildGreatHart fawn = new WildGreatHart();
					fawn.Female = false;
					fawn.Mother = this;
					fawn.Spawned = false;
					fawn.TypeName = "a deer";
					fawn.MakeBaby();
					if (this.Brand != "" && this.Brand != null) fawn.Owner = this.Owner;
					fawn.MoveToWorld(this.Location, this.Map);
				}	
				else
				{
					WildHind fawn = new WildHind();
					fawn.Female = true;
					fawn.Mother = this;
					fawn.Spawned = false;
					fawn.TypeName = "a deer";
					fawn.MakeBaby();
					if (this.Brand != "" && this.Brand != null) fawn.Owner = this.Owner;
					fawn.MoveToWorld(this.Location, this.Map);
				}
			}
			base.CreateBaby();
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
				meatbonus += 2;
			}
			else if (Age == AgeDescription.Senior)
			{
				meatbonus += 1;
			}
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
			if (meatbonus > 2) corpse.DropItem( new RawVenisonSteak(2));
		}
		
		//public override int Meat{ get{ return 5; } }
		//public override int Hides{ get{ return 8; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public WildHind(Serial serial) : base(serial)
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