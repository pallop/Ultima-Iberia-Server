using System;
using Server.Mobiles;
using Server;

namespace Server
{
    public class OppositionGroup
    {
        private static readonly OppositionGroup m_TerathansAndOphidians = new OppositionGroup(new Type[][]
        {
            new Type[]
            {
                typeof(TerathanAvenger),
                typeof(TerathanDrone),
                typeof(TerathanMatriarch),
                typeof(TerathanWarrior)
            },
            new Type[]
            {
                typeof(OphidianArchmage),
                typeof(OphidianKnight),
                typeof(OphidianMage),
                typeof(OphidianMatriarch),
                typeof(OphidianWarrior)
            }
        });
        private static readonly OppositionGroup m_SavagesAndOrcs = new OppositionGroup(new Type[][]
        {
            new Type[]
            {
                typeof(Orc),
                typeof(OrcBomber),
                typeof(OrcBrute),
                typeof(OrcCaptain),
                typeof(OrcishLord),
                typeof(OrcishMage),
                typeof(SpawnedOrcishLord)
            },
            new Type[]
            {
                typeof(Savage),
                typeof(SavageRider),
                typeof(SavageRidgeback),
                typeof(SavageShaman)
            }
        });
        private static readonly OppositionGroup m_FeyAndUndead = new OppositionGroup(new Type[][]
        {
            new Type[]
            {
                typeof(Centaur),
                typeof(EtherealWarrior),
                typeof(Kirin),
                typeof(LordOaks),
                typeof(Pixie),
                typeof(Silvani),
                typeof(Unicorn),
                typeof(Wisp),
                typeof(Treefellow),
                typeof(MLDryad),
                typeof(Satyr)
            },
            new Type[]
            {
                typeof(AncientLich),
                typeof(Bogle),
                typeof(LichLord),
                typeof(Shade),
                typeof(Spectre),
                typeof(Wraith),
                typeof(BoneKnight),
                typeof(Ghoul),
                typeof(Mummy),
                typeof(SkeletalKnight),
                typeof(Skeleton),
                typeof(Zombie),
                typeof(ShadowKnight),
                typeof(DarknightCreeper),
                typeof(RevenantLion),
                typeof(LadyOfTheSnow),
                typeof(RottingCorpse),
                typeof(SkeletalDragon),
                typeof(Lich)
            }
        });
        private readonly Type[][] m_Types;
        public OppositionGroup(Type[][] types)
        {
            this.m_Types = types;
        }

        public static OppositionGroup TerathansAndOphidians
        {
            get
            {
                return m_TerathansAndOphidians;
            }
        }
        public static OppositionGroup SavagesAndOrcs
        {
            get
            {
                return m_SavagesAndOrcs;
            }
        }

        //modificacion guardias
       public static OppositionGroup FeyAndUndead
        {
            get{ return m_FeyAndUndead; }
        }
        private static OppositionGroup m_newguards = new OppositionGroup(new Type[][]
{
new Type[]
{
typeof( NewMageGuard ),
typeof( NewArcherGuard ),
typeof( NewWarriorGuard ),
typeof( NewMageEvilGuard ),
typeof( NewArcherEvilGuard ),
typeof( NewWarriorEvilGuard ),
typeof( NewMageNoCriminalGuard ),
typeof( NewWarriorNoCriminalGuard ),
typeof( NewArcherNoCriminalGuard )
},
new Type[]
{
typeof( AncientLich ),
typeof( Bogle ),
typeof( LichLord ),
typeof( Shade ),
typeof( Spectre ),
typeof( Wraith ),
typeof( BoneKnight ),
typeof( Ghoul ),
typeof( Mummy ),
typeof( SkeletalKnight ),
typeof( Skeleton ),
typeof( Zombie ),
typeof( ShadowKnight ),
typeof( DemonKnight ),
typeof( DarknightCreeper ),
typeof( RevenantLion ),
typeof( LadyOfTheSnow ),
typeof( RottingCorpse ),
typeof( SkeletalDragon ),
typeof( Lich ),

typeof( Centaur ),
typeof( EtherealWarrior ),
typeof( Kirin ),
typeof( LordOaks ),
typeof( Pixie ),
typeof( Silvani ),
typeof( Unicorn ),
typeof( Wisp ),
typeof( Treefellow ),

typeof( Orc ),
typeof( OrcBomber ),
typeof( OrcBrute ),
typeof( OrcCaptain ),
typeof( OrcishLord ),
typeof( OrcishMage ),
typeof( SpawnedOrcishLord ),

typeof( Savage ),
typeof( SavageRider ),
typeof( SavageRidgeback ),
typeof( SavageShaman ),

typeof( TerathanAvenger ),
typeof( TerathanDrone ),
typeof( TerathanMatriarch ),
typeof( TerathanWarrior ),
typeof( OphidianArchmage ),
typeof( OphidianKnight ),
typeof( OphidianMage ),
typeof( OphidianMatriarch ),
typeof( OphidianWarrior ),

typeof( DireWolf ),
typeof( HellCat ),
typeof( PredatorHellCat ),
typeof( GiantToad ),
typeof( HellSteed ),
typeof( BlackSolenInfiltratorQueen ),
typeof( LesserHiryu ),
typeof( SavageRidgeback ),
typeof( ScaledSwampDragon ),
typeof( SkeletalMount ),
typeof( Alligator ),
typeof( GiantSerpent ),
typeof( IceSerpent ),
typeof( IceSnake ),
typeof( LavaLizard ),
typeof( LavaSerpent ),
typeof( LavaSnake ),
typeof( SilverSerpent ),
typeof( Snake ),
typeof( GiantRat ),
typeof( Sewerrat ),
typeof( VampireBat ),
typeof( EvilHealer ),
typeof( EvilWanderingHealer ),
typeof( Harrower ),
typeof( HarrowerTentacles ),
typeof( BlackSolenInfiltratorWarrior ),
typeof( BlackSolenQueen ),
typeof( BlackSolenWarrior ),
typeof( BlackSolenWorker ),
typeof( RedSolenInfiltratorQueen ),
typeof( RedSolenInfiltratorWarrior ),
typeof( RedSolenQueen ),
typeof( RedSolenWarrior ),
typeof( RedSolenWorker ),
typeof( AbysmalHorror ),
typeof( BoneDemon ),
typeof( Devourer ),
typeof( FleshGolem ),
typeof( FleshRenderer ),
typeof( Gibberling ),
typeof( GoreFiend ),
typeof( Impaler ),
typeof( MoundOfMaggots ),
typeof( PatchworkSkeleton ),
typeof( Ravager ),
typeof( Revenant ),
typeof( SkitteringHopper ),
typeof( VampireBat ),
typeof( WailingBanshee ),
typeof( WandererOfTheVoid ),
typeof( DreadSpider ),
typeof( FrostSpider ),
typeof( GiantBlackWidow ),
typeof( GiantSpider ),
typeof( AirElemental ),
typeof( IceElemental ),
typeof( ToxicElemental ),
typeof( PoisonElemental ),
typeof( FireElemental ),
typeof( WaterElemental ),
typeof( EarthElemental ),
typeof( Efreet ),
typeof( SnowElemental ),
typeof( AgapiteElemental ),
typeof( BronzeElemental ),
typeof( CopperElemental ),
typeof( DullCopperElemental ),
typeof( GoldenElemental ),
typeof( ShadowIronElemental ),
typeof( ValoriteElemental ),
typeof( VeriteElemental ),
typeof( ArcaneDaemon ),
typeof( Balron ),
typeof( Betrayer ),
typeof( BoneMagi ),
typeof( ElderGazer ),
typeof( EvilMage ),
typeof( EvilMageLord ),
typeof( Gargoyle ),
typeof( GargoyleDestroyer ),
typeof( GargoyleEnforcer ),
typeof( Gazer),
typeof( GolemController ),
typeof( IceFiend ),
typeof( Imp ),
typeof( OrcishMage ),
typeof( RatmanMage ),
typeof( SkeletalMage ),
typeof( Succubus ),
typeof( Titan ),
typeof( ArcticOgreLord ),
typeof( Brigand ),
typeof( ChaosDaemon ),
typeof( Cursed ),
typeof( Cyclops ),
typeof( Doppleganger ),
typeof( EnslavedGargoyle ),
typeof( Ettin ),
typeof( Executioner ),
typeof( FrostTroll ),
typeof( GazerLarva ),
typeof( HeadlessOne ),
typeof( Juggernaut ),
typeof( KhaldunRevenant ),
typeof( KhaldunSummoner ),
typeof( KhaldunZealot ),
typeof( Moloch ),
typeof( Mongbat ),
typeof( Mummy ),
typeof( Ogre ),
typeof( OgreLord ),
typeof( Ratman ),
typeof( RatmanArcher ),
typeof( RestlessSoul ),
typeof( WhiteWyrm ),
typeof( ShadowFiend ),
typeof( SpectralArmour ),
typeof( StoneGargoyle ),
typeof( StrongMongbat ),
typeof( Troll ),
typeof( ExodusMinion ),
typeof( ExodusOverseer ),
typeof( JukaLord ),
typeof( JukaMage ),
typeof( JukaWarrior ),
typeof( MeerCaptain ),
typeof( MeerEternal ),
typeof( MeerMage ),
typeof( MeerWarrior ),
typeof( HellHound ),
typeof( VorpalBunny ),
typeof( DarkWisp ),
typeof( BladeSpirits ),
typeof( EnergyVortex ),
typeof( FrostOoze ),
typeof( Golem ),
typeof( PlagueBeast ),
typeof( PlagueSpawn ),
typeof( SandVortex ),
typeof( Slime ),
typeof( Reaper ),
typeof( BogThing ),
typeof( Bogling ),
typeof( Corpser ),
typeof( Quagmire ),
typeof( SwampTentacle ),
typeof( WhippingVine ),
typeof( AncientWyrm ),
typeof( DeepSeaSerpent ),
typeof( Dragon ),
typeof( Leviathan ),
typeof( SerpentineDragon ),
typeof( ShadowWyrm ),
typeof( Drake ),
typeof( Harpy ),
typeof( Kraken ),
typeof( Lizardman ),
typeof( Scorpion ),
typeof( StoneHarpy ),
typeof( Wyvern ),
typeof( DeathwatchBeetle ),
typeof( DeathwatchBeetleHatchling ),
typeof( EliteNinja ),
typeof( FanDancer ),
typeof( FireBeetle ),
typeof( Kappa ),
typeof( KazeKemono ),
typeof( Oni ),
typeof( RaiJu ),
typeof( Ronin ),
typeof( RuneBeetle ),
typeof( TsukiWolf ),
typeof( YomotsuWarrior ),
typeof( Yamandon),
typeof( YomotsuElder ),
typeof( YomotsuPriest ),
typeof( SummonedDaemon ),
typeof( Rat ),
typeof( Daemon )
}
});

public static OppositionGroup newguards
{
get { return m_newguards; }

//fin modificacion guardias


        }
        public bool IsEnemy(object from, object target)
        {
            int fromGroup = this.IndexOf(from);
            int targGroup = this.IndexOf(target);

            return (fromGroup != -1 && targGroup != -1 && fromGroup != targGroup);
        }

        public int IndexOf(object obj)
        {
            if (obj == null)
                return -1;

            Type type = obj.GetType();

            for (int i = 0; i < this.m_Types.Length; ++i)
            {
                Type[] group = this.m_Types[i];

                bool contains = false;

                for (int j = 0; !contains && j < group.Length; ++j)
                    contains = group[j].IsAssignableFrom(type);

                if (contains)
                    return i;
            }

            return -1;
        }

    }
}
