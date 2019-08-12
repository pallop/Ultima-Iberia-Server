using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a cat corpse" )]
	[TypeAlias( "Server.Mobiles.Housecat" )]
	public class TownCat : BaseAnimal //BaseCreature
	{
		[Constructable]
		public TownCat() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Activity = ActivityType.Twilight;
			Eats = EatType.Carnivore;
			Animal = AnimalType.Town;
			
			MateActive = true;
			LitterSize = 4;
			MaxAge = 30;
			MatingSeason = Seasons.Any;

			Name = "a cat";
			Body = 0xC9;
			Hue = Utility.RandomAnimalHue();
			BaseSoundID = 0x69;

			SetStr( 9 );
			SetDex( 35 );
			SetInt( 5 );

			SetHits( 6 );
			SetMana( 0 );

			SetDamage( 1 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 5, 10 );

			SetSkill( SkillName.MagicResist, 5.0 );
			SetSkill( SkillName.Tactics, 4.0 );
			SetSkill( SkillName.Wrestling, 5.0 );

			Fame = 0;
			Karma = 150;

			VirtualArmor = 8;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = -0.9;
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			TypeName = "a cat";
		}
		
		public override void MakeBaby()
		{
			SetStr( 2 );
			SetDex( 5 );
			SetInt( 1 );
			SetDamage( 0 );
			Name = "a kitten";
			base.MakeBaby();
		}
		
		public override void MakeYoung()
		{
			SetStr( 4 );
			SetDex( 15 );
			SetInt( 3 );
			SetDamage( 1 );
			if (Name == "a kitten") 
				Name = "a kitten";
			base.MakeYoung();
		}
		
		public override void MakeAdult()
		{
			SetStr( 9 );
			SetDex( 35 );
			SetInt( 5 );
			SetDamage( 2 );
			if (Name == "a kitten") 
				Name = (this.Female ? "a cat": "a tom cat");
			base.MakeAdult();
		}
		
		public override void MakeSenior()
		{
			SetStr( 5 );
			SetDex( 20 );
			SetInt( 8 );
			SetDamage( 1 );
			if (Name == "a cat" || Name == "a tom cat" || Name == "a kitten") 
				Name = (this.Female ? "a cat": "a tom cat");
			base.MakeSenior();
		}
		
		public override void CreateBaby()
		{
			for (int j = 0; j < this.LitterSize; j++) 
			{
				TownCat kitten = new TownCat();
				kitten.Female = ((Utility.RandomMinMax(1,4) == 1) ? false: true);
				kitten.MakeBaby();
				kitten.Mother = this;
				kitten.Spawned = false;
				kitten.TypeName = "a cat";
				kitten.Hue = Hue;
				kitten.MoveToWorld(this.Location, this.Map);
			}
			base.CreateBaby();
		}
		
		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Feline; } }

        public TownCat(Serial serial)
            : base(serial)
		{
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