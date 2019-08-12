/* Created by Hammerhand*/

using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a Gem Elemental corpse" )]
    public class GemElemental : BaseCreature
	{
        public override WeaponAbility GetWeaponAbility()
        {
            return WeaponAbility.BleedAttack;
        }
		[Constructable]
		public GemElemental () : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Gem Elemental";
			Body = 300;
            Hue = Utility.RandomList(1173, 1755, 1922, 1281, 2117, 1366, 1372, 1176, 1150);
			BaseSoundID = 278;

			SetStr( 136, 160 );
			SetDex( 151, 165 );
			SetInt( 86, 110 );

			SetHits( 1000, 1500 );

			SetDamage( 45, 65 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 80, 95 );
			SetResistance( ResistanceType.Fire, 45, 60 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 55, 70 );

			SetSkill( SkillName.EvalInt, 70.1, 75.0 );
			SetSkill( SkillName.Magery, 70.1, 75.0 );
			SetSkill( SkillName.Meditation, 65.1, 75.0 );
			SetSkill( SkillName.MagicResist, 80.1, 90.0 );
			SetSkill( SkillName.Tactics, 75.1, 85.0 );
			SetSkill( SkillName.Wrestling, 98.5, 105.4 );

			Fame = 6500;
			Karma = -6500;

			VirtualArmor = 54;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
		}
        public override int GetDeathSound()
        {
            return 0x41;
        }
        public override bool OnBeforeDeath()
        {

            switch (Utility.Random(64))
            {
                case 0: PackItem(new BDRunicGemCraftTool()); break;
                case 1: PackItem(new DSRunicGemCraftTool()); break;
                case 2: PackItem(new ECRunicGemCraftTool()); break;
                case 3: PackItem(new FRRunicGemCraftTool()); break;
                case 4: PackItem(new PERunicGemCraftTool()); break;
                case 5: PackItem(new TRunicGemCraftTool()); break;
                case 6: PackItem(new BARunicGemCraftTool()); break;
                case 7: PackItem(new WPRunicGemCraftTool()); break;
                case 8: PackItem(new BlueDiamond(2)); break;
                case 9: PackItem(new DarkSapphire(2)); break;
                case 10: PackItem(new EcruCitrine(2)); break;
                case 11: PackItem(new FireRuby(2)); break;
                case 12: PackItem(new PerfectEmerald(2)); break;
                case 13: PackItem(new Turquoise(2)); break;
                case 14: PackItem(new BrilliantAmber(2)); break;
                case 15: PackItem(new WhitePearl(2)); break; 
            }

            return base.OnBeforeDeath();
        }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
        public override bool Uncalmable { get { return Core.SE; } }
        public override bool IsScaryToPets { get { return true; } }

		public GemElemental( Serial serial ) : base( serial )
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