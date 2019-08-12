/*
 created by:
     /\            888                   888     .d8888b.   .d8888b.  
____/_ \____       888                   888    d88P  Y88b d88P  Y88b 
\  ___\ \  /       888                   888    888    888 888    888 
 \/ /  \/ /    .d88888  8888b.   8888b.  888888 Y88b. d888 Y88b. d888 
 / /\__/_/\   d88" 888     "88b     "88b 888     "Y888P888  "Y888P888 
/__\ \_____\  888  888 .d888888 .d888888 888           888        888 
    \  /      Y88b 888 888  888 888  888 Y88b.  Y88b  d88P Y88b  d88P 
     \/        "Y88888 "Y888888 "Y888888  "Y888  "Y8888P"   "Y8888P"  
*/
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a heartwood corpse" )]
	public class HeartwoodElemental : BaseCreature
	{
		[Constructable]
		public HeartwoodElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a heartwood elemental";
			
			Body = 301;
			Hue = 1262;

			SetStr( 150, 200 );
			SetDex( 100, 120 );
			SetInt( 50, 60 );

			SetHits( 100, 120 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 10 );
			SetDamageType( ResistanceType.Poison, 5 );
			SetDamageType( ResistanceType.Fire, 5 );
			SetDamageType( ResistanceType.Cold, 5 );
			SetDamageType( ResistanceType.Energy, 5 );

			SetResistance( ResistanceType.Physical, 10, 20 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 10.0 );
			SetSkill( SkillName.Tactics, 50.0 );
			SetSkill( SkillName.Wrestling, 50.0 );

			Fame = 500;
			Karma = -500;

			VirtualArmor = 5;
			
			PackGem();
			PackGem();
			PackItem( new HeartwoodLog( 10 ) );
			PackMagicItems( 2, 5 );
		}

		public override bool AutoDispel{ get{ return true; } }

		public HeartwoodElemental( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[CorpseName( "an Bloodwood corpse" )]
	public class BloodwoodElemental : BaseCreature
	{
		[Constructable]
		public BloodwoodElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Bloodwood elemental";
			
			Body = 301;
			Hue = 1194;

			SetStr( 160, 210 );
			SetDex( 110, 130 );
			SetInt( 60, 70 );

			SetHits( 110, 130 );

			SetDamage( 10, 15 );

			SetDamageType( ResistanceType.Physical, 15 );
			SetDamageType( ResistanceType.Poison, 10 );
			SetDamageType( ResistanceType.Fire, 10 );
			SetDamageType( ResistanceType.Cold, 10 );
			SetDamageType( ResistanceType.Energy, 10 );

			SetResistance( ResistanceType.Physical, 15, 25 );
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 10, 15 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 15.0 );
			SetSkill( SkillName.Tactics, 55.0 );
			SetSkill( SkillName.Wrestling, 55.0 );

			Fame = 1000;
			Karma = -1000;

			VirtualArmor = 10;
			
			PackGem();
			PackGem();
			PackItem( new BloodwoodLog( 10 ) );
			PackMagicItems( 2, 5 );
		}

		public override bool AutoDispel{ get{ return true; } }

		public BloodwoodElemental( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[CorpseName( "a Frostwood corpse" )]
	public class FrostwoodElemental : BaseCreature
	{
		[Constructable]
		public FrostwoodElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Frostwood elemental";
			
			Body = 301;
			Hue = 1266;

			SetStr( 170, 220 );
			SetDex( 120, 140 );
			SetInt( 70, 80 );

			SetHits( 120, 140 );

			SetDamage( 15, 20 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Poison, 15 );
			SetDamageType( ResistanceType.Fire, 15 );
			SetDamageType( ResistanceType.Cold, 15 );
			SetDamageType( ResistanceType.Energy, 15 );

			SetResistance( ResistanceType.Physical, 20, 30 );
			SetResistance( ResistanceType.Fire, 15, 20 );
			SetResistance( ResistanceType.Cold, 15, 20 );
			SetResistance( ResistanceType.Poison, 15, 20 );
			SetResistance( ResistanceType.Energy, 15, 20 );

			SetSkill( SkillName.MagicResist, 20.0 );
			SetSkill( SkillName.Tactics, 60.0 );
			SetSkill( SkillName.Wrestling, 60.0 );

			Fame = 1500;
			Karma = -1500;

			VirtualArmor = 15;
			
			PackGem();
			PackGem();
			PackItem( new FrostwoodLog( 10 ) );
			PackMagicItems( 2, 5 );
		}

		public override bool AutoDispel{ get{ return true; } }

		public FrostwoodElemental( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[CorpseName( "a Oak corpse" )]
	public class OakWoodElemental : BaseCreature
	{
		[Constructable]
		public OakWoodElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Oak elemental";
			
			Body = 301;
			Hue = 1281;

			SetStr( 180, 230 );
			SetDex( 130, 150 );
			SetInt( 80, 90 );

			SetHits( 130, 150 );

			SetDamage( 20, 25 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Fire, 20, 25 );
			SetResistance( ResistanceType.Cold, 20, 25 );
			SetResistance( ResistanceType.Poison, 20, 25 );
			SetResistance( ResistanceType.Energy, 20, 25 );

			SetSkill( SkillName.MagicResist, 25.0 );
			SetSkill( SkillName.Tactics, 65.0 );
			SetSkill( SkillName.Wrestling, 65.0 );

			Fame = 2000;
			Karma = -2000;

			VirtualArmor = 20;
			
			PackGem();
			PackGem();
			PackItem( new OakLog( 10 ) );
			PackMagicItems( 2, 5 );
		}

		public override bool AutoDispel{ get{ return true; } }

		public OakWoodElemental( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[CorpseName( "an Ash wood corpse" )]
	public class AshWoodElemental : BaseCreature
	{
		[Constructable]
		public AshWoodElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an Ash wood elemental";
			
			Body = 301;
			Hue = 488;

			SetStr( 190, 240 );
			SetDex( 140, 160 );
			SetInt( 90, 100 );

			SetHits( 140, 160 );

			SetDamage( 25, 30 );

			SetDamageType( ResistanceType.Physical, 30 );
			SetDamageType( ResistanceType.Poison, 25 );
			SetDamageType( ResistanceType.Energy, 25 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Cold, 25 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Poison, 25, 30 );
			SetResistance( ResistanceType.Energy, 25, 30 );
			SetResistance( ResistanceType.Fire, 25, 30 );
			SetResistance( ResistanceType.Cold, 25, 30 );

			SetSkill( SkillName.MagicResist, 30.0 );
			SetSkill( SkillName.Wrestling, 70.0 );
			SetSkill( SkillName.Tactics, 70.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 25;
			
			PackGem();
			PackGem();
			PackItem( new AshLog( 10 ) );
			PackMagicItems( 2, 5 );
		}

		public override bool AutoDispel{ get{ return true; } }

		public AshWoodElemental( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[CorpseName( "a Yew wood corpse" )]
	public class YewWoodElemental : BaseCreature
	{
		[Constructable]
		public YewWoodElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Yew wood elemental";
			
			Body = 301;
			Hue = 2313;

			SetStr( 200, 250 );
			SetDex( 150, 170 );
			SetInt( 100, 110 );

			SetHits( 150, 170 );

			SetDamage( 30, 35 );

			SetDamageType( ResistanceType.Physical, 35 );
			SetDamageType( ResistanceType.Poison, 30 );
			SetDamageType( ResistanceType.Energy, 30 );
			SetDamageType( ResistanceType.Fire, 30 );
			SetDamageType( ResistanceType.Cold, 30 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Poison, 30, 35 );
			SetResistance( ResistanceType.Energy, 30, 35 );
			SetResistance( ResistanceType.Fire, 30, 35 );
			SetResistance( ResistanceType.Cold, 30, 35 );

			SetSkill( SkillName.MagicResist, 35.0 );
			SetSkill( SkillName.Wrestling, 75.0 );
			SetSkill( SkillName.Tactics, 75.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 30;
			
			PackGem();
			PackGem();
			PackItem( new YewLog( 10 ) );
			PackMagicItems( 2, 5 );
		}

		public override bool AutoDispel{ get{ return true; } }

		public YewWoodElemental( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[CorpseName( "an ebony wood corpse" )]
	public class EbonyWoodElemental : BaseCreature
	{
		[Constructable]
		public EbonyWoodElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ebony wood elemental";
			
			Body = 301;
			Hue = 1457;

			SetStr( 210, 260 );
			SetDex( 160, 180 );
			SetInt( 110, 120 );

			SetHits( 160, 180 );

			SetDamage( 35, 40 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Poison, 35 );
			SetDamageType( ResistanceType.Energy, 35 );
			SetDamageType( ResistanceType.Fire, 35 );
			SetDamageType( ResistanceType.Cold, 35 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Poison, 35, 40 );
			SetResistance( ResistanceType.Energy, 35, 40 );
			SetResistance( ResistanceType.Fire, 35, 40 );
			SetResistance( ResistanceType.Cold, 35, 40 );

			SetSkill( SkillName.MagicResist, 40.0 );
			SetSkill( SkillName.Wrestling, 80.0 );
			SetSkill( SkillName.Tactics, 80.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 35;
			
			PackGem();
			PackGem();
			PackItem( new EbonyLog( 10 ) );
			PackMagicItems( 2, 5 );
		}

		public override bool AutoDispel{ get{ return true; } }

		public EbonyWoodElemental( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[CorpseName( "a bamboo wood corpse" )]
	public class BambooWoodElemental : BaseCreature
	{
		[Constructable]
		public BambooWoodElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a bamboo wood elemental";
			
			Body = 301;
			Hue = 1719;

			SetStr( 220, 270 );
			SetDex( 170, 190 );
			SetInt( 120, 130 );

			SetHits( 170, 190 );

			SetDamage( 40, 45 );

			SetDamageType( ResistanceType.Physical, 45 );
			SetDamageType( ResistanceType.Poison, 40 );
			SetDamageType( ResistanceType.Energy, 40 );
			SetDamageType( ResistanceType.Fire, 40 );
			SetDamageType( ResistanceType.Cold, 40 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Poison, 40, 45 );
			SetResistance( ResistanceType.Energy, 40, 45 );
			SetResistance( ResistanceType.Fire, 40, 45 );
			SetResistance( ResistanceType.Cold, 40, 45 );

			SetSkill( SkillName.MagicResist, 45.0 );
			SetSkill( SkillName.Wrestling, 85.0 );
			SetSkill( SkillName.Tactics, 85.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 40;
			
			PackGem();
			PackGem();
			PackItem( new BambooLog( 10 ) );
			PackMagicItems( 2, 5 );
		}

		public override bool AutoDispel{ get{ return true; } }

		public BambooWoodElemental( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[CorpseName( "a purple heart wood corpse" )]
	public class PurpleHeartWoodElemental : BaseCreature
	{
		[Constructable]
		public PurpleHeartWoodElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a purple heart wood elemental";
			
			Body = 301;
			Hue = 114;

			SetStr( 230, 280 );
			SetDex( 180, 200 );
			SetInt( 130, 140 );

			SetHits( 180, 200 );

			SetDamage( 45, 50 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 45 );
			SetDamageType( ResistanceType.Energy, 45 );
			SetDamageType( ResistanceType.Fire, 45 );
			SetDamageType( ResistanceType.Cold, 45 );

			SetResistance( ResistanceType.Physical, 50, 60 );
			SetResistance( ResistanceType.Poison, 45, 50 );
			SetResistance( ResistanceType.Energy, 45, 50 );
			SetResistance( ResistanceType.Fire, 45, 50 );
			SetResistance( ResistanceType.Cold, 45, 50 );

			SetSkill( SkillName.MagicResist, 50.0 );
			SetSkill( SkillName.Wrestling, 90.0 );
			SetSkill( SkillName.Tactics, 90.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 45;
			
			PackGem();
			PackGem();
			PackItem( new PurpleHeartLog( 10 ) );
			PackMagicItems( 2, 5 );
		}

		public override bool AutoDispel{ get{ return true; } }

		public PurpleHeartWoodElemental( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[CorpseName( "a redwood wood corpse" )]
	public class RedwoodElemental : BaseCreature
	{
		[Constructable]
		public RedwoodElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a redwood wood elemental";
			
			Body = 301;
			Hue = 37;

			SetStr( 240, 290 );
			SetDex( 190, 210 );
			SetInt( 140, 150 );

			SetHits( 190, 210 );

			SetDamage( 50, 55 );

			SetDamageType( ResistanceType.Physical, 55 );
			SetDamageType( ResistanceType.Poison, 50 );
			SetDamageType( ResistanceType.Energy, 50 );
			SetDamageType( ResistanceType.Fire, 50 );
			SetDamageType( ResistanceType.Cold, 50 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Poison, 50, 55 );
			SetResistance( ResistanceType.Energy, 50, 55 );
			SetResistance( ResistanceType.Fire, 50, 55 );
			SetResistance( ResistanceType.Cold, 50, 55 );

			SetSkill( SkillName.MagicResist, 55.0 );
			SetSkill( SkillName.Wrestling, 95.0 );
			SetSkill( SkillName.Tactics, 95.0 );

			Fame = 5000;
			Karma = -5000;

			VirtualArmor = 50;
			
			PackGem();
			PackGem();
			PackItem( new RedwoodLog( 10 ) );
			PackMagicItems( 2, 5 );
		}

		public override bool AutoDispel{ get{ return true; } }

		public RedwoodElemental( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[CorpseName( "a petrified wood corpse" )]
	public class PetrifiedWoodElemental : BaseCreature
	{
		[Constructable]
		public PetrifiedWoodElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a petrified wood elemental";
			
			Body = 301;
			Hue = 1153;

			SetStr( 250, 300 );
			SetDex( 200, 220 );
			SetInt( 150, 160 );

			SetHits( 200, 220 );

			SetDamage( 55, 60 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Poison, 55 );
			SetDamageType( ResistanceType.Energy, 55 );
			SetDamageType( ResistanceType.Fire, 55 );
			SetDamageType( ResistanceType.Cold, 55 );

			SetResistance( ResistanceType.Physical, 60, 70 );
			SetResistance( ResistanceType.Poison, 55, 60 );
			SetResistance( ResistanceType.Energy, 55, 60 );
			SetResistance( ResistanceType.Fire, 55, 60 );
			SetResistance( ResistanceType.Cold, 55, 60 );

			SetSkill( SkillName.MagicResist, 60.0 );
			SetSkill( SkillName.Wrestling, 100.0 );
			SetSkill( SkillName.Tactics, 100.0 );

			Fame = 5500;
			Karma = -5500;

			VirtualArmor = 55;
			
			PackGem();
			PackGem();
			PackItem( new PetrifiedLog( 10 ) );
			PackMagicItems( 2, 5 );
		}

		public override bool AutoDispel{ get{ return true; } }

		public PetrifiedWoodElemental( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}