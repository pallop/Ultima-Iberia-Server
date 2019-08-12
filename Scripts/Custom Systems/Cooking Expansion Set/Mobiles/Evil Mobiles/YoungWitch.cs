using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a human corpse" )]
	public class YoungWitch : BaseCreature
	{
		[Constructable]
		public YoungWitch() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
            this.Name = NameList.RandomName("female");
            this.Title = "the young witch";
            this.Body = 401;

            this.SetStr(81, 105);
            this.SetDex(91, 115);
			SetInt( 196, 220 );

            this.SetHits(249, 263);
            this.SetDamage(5, 10);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 25, 50);
            this.SetResistance(ResistanceType.Fire, 25, 500);
            this.SetResistance(ResistanceType.Cold, 25, 50);
            this.SetResistance(ResistanceType.Poison, 25, 50);
            this.SetResistance(ResistanceType.Energy, 25, 50);

            this.SetSkill(SkillName.EvalInt, 125.1, 150.0);
            this.SetSkill(SkillName.Magery, 125.1, 150.0);
            this.SetSkill(SkillName.MagicResist, 175.0, 197.5);
            this.SetSkill(SkillName.Tactics, 65.0, 87.5);
            this.SetSkill(SkillName.Wrestling, 90.2, 160.0);

            this.Fame = 2500;
            this.Karma = -2500;

            this.VirtualArmor = 16;
            this.PackReg(6);
            this.AddItem(new Robe(Utility.RandomNeutralHue()));
            this.AddItem(new Sandals());
            this.AddItem(new LeatherSkirt());
            this.AddItem(new FemaleLeatherChest());
			if (Utility.RandomDouble() < .25 ) PackItem( new Boline() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls );
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public YoungWitch( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}