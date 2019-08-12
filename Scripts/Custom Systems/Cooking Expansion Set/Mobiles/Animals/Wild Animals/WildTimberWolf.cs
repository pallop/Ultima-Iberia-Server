using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a timber wolf corpse" )]
	[TypeAlias( "Server.Mobiles.Timberwolf" )]
	public class WildTimberWolf : BaseAnimal //BaseCreature
	{
		[Constructable]
		public WildTimberWolf() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Activity = ActivityType.Any;
			Eats = EatType.Carnivore;
			Grouping = GroupingType.Pack;
			
			MateActive = true;
			LitterSize = 2;
			MaxAge = 30;
            MatingSeason = Seasons.Winter;

			Name = "a timber wolf";
			Body = 225;
			BaseSoundID = 0xE5;

			SetStr( 56, 80 );
			SetDex( 56, 75 );
			SetInt( 11, 25 );

			SetHits( 34, 48 );
			SetMana( 0 );

			SetDamage( 5, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 27.6, 45.0 );
			SetSkill( SkillName.Tactics, 30.1, 50.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 450;
			Karma = 0;

			VirtualArmor = 16;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 23.1;
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			TypeName = "a timber wolf";
		}
		
		public override void MakeBaby()
		{
			SetStr( 2,20 );
			SetDex( 5,15 );
			SetInt( 1 );
			SetDamage( 1 );
			Name = "a timber wolf";
			Title = "(pup)";
			base.MakeBaby();
		}
		
		public override void MakeYoung()
		{
			SetStr( 15,45 );
			SetDex( 15, 45 );
			SetInt( 3, 12 );
			SetDamage( 2,6 );
			if (Name == "a timber wolf") Name = "a timber wolf";
			Title = "(pup)";
			base.MakeYoung();
		}
		
		public override void MakeAdult()
		{
			SetStr( 56, 80 );
			SetDex( 56, 75 );
			SetInt( 11, 25 );
			SetDamage( 5, 9 );
			if (Name == "a timber wolf") Name = "a timber wolf";
			Title = (this.Female ? "(female)": "(male)");
			base.MakeAdult();
		}
		
		public override void MakeSenior()
		{
			SetStr( 45,60 );
			SetDex( 45,60 );
			SetInt( 15,30 );
			SetDamage( 4,12 );
			if (Name == "a timber wolf") Name = "a timber wolf";
			Title = (this.Female ? "(female)": "(male)");
			base.MakeSenior();
		}
		
		public override void CreateBaby()
		{
			for (int j = 0; j < this.LitterSize; j++) 
			{
				WildTimberWolf puppy = new WildTimberWolf();
				puppy.Female = ((Utility.RandomMinMax(1,4) == 1) ? false: true);
				puppy.MakeBaby();
				puppy.Mother = this;
				puppy.Spawned = false;
				puppy.TypeName = "a timber wolf";
				puppy.Hue = Hue;
				if (this.Brand != "" && this.Brand != null) puppy.Owner = this.Owner;
				puppy.MoveToWorld(this.Location, this.Map);
			}
			base.CreateBaby();
		}
		
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 5; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Canine; } }

        public WildTimberWolf(Serial serial)
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