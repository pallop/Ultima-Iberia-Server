using System;
using Server;
using Server.Items;

namespace Server.Engines.BulkOrders
{
    public delegate Item ConstructCallback(int type);

    public sealed class RewardType
    {
        private readonly int m_Points;
        private readonly Type[] m_Types;

        public int Points
        {
            get
            {
                return this.m_Points;
            }
        }
        public Type[] Types
        {
            get
            {
                return this.m_Types;
            }
        }

        public RewardType(int points, params Type[] types)
        {
            this.m_Points = points;
            this.m_Types = types;
        }

        public bool Contains(Type type)
        {
            for (int i = 0; i < this.m_Types.Length; ++i)
            {
                if (this.m_Types[i] == type)
                    return true;
            }

            return false;
        }
    }

    public sealed class RewardItem
    {
        private readonly int m_Weight;
        private readonly ConstructCallback m_Constructor;
        private readonly int m_Type;

        public int Weight
        {
            get
            {
                return this.m_Weight;
            }
        }
        public ConstructCallback Constructor
        {
            get
            {
                return this.m_Constructor;
            }
        }
        public int Type
        {
            get
            {
                return this.m_Type;
            }
        }

        public RewardItem(int weight, ConstructCallback constructor)
            : this(weight, constructor, 0)
        {
        }

        public RewardItem(int weight, ConstructCallback constructor, int type)
        {
            this.m_Weight = weight;
            this.m_Constructor = constructor;
            this.m_Type = type;
        }

        public Item Construct()
        {
            try
            {
                return this.m_Constructor(this.m_Type);
            }
            catch
            {
                return null;
            }
        }
    }

    public sealed class RewardGroup
    {
        private readonly int m_Points;
        private readonly RewardItem[] m_Items;

        public int Points
        {
            get
            {
                return this.m_Points;
            }
        }
        public RewardItem[] Items
        {
            get
            {
                return this.m_Items;
            }
        }

        public RewardGroup(int points, params RewardItem[] items)
        {
            this.m_Points = points;
            this.m_Items = items;
        }

        public RewardItem AcquireItem()
        {
            if (this.m_Items.Length == 0)
                return null;
            else if (this.m_Items.Length == 1)
                return this.m_Items[0];

            int totalWeight = 0;

            for (int i = 0; i < this.m_Items.Length; ++i)
                totalWeight += this.m_Items[i].Weight;

            int randomWeight = Utility.Random(totalWeight);

            for (int i = 0; i < this.m_Items.Length; ++i)
            {
                RewardItem item = this.m_Items[i];

                if (randomWeight < item.Weight)
                    return item;

                randomWeight -= item.Weight;
            }

            return null;
        }
    }

    public abstract class RewardCalculator
    {
        private RewardGroup[] m_Groups;

        public RewardGroup[] Groups
        {
            get
            {
                return this.m_Groups;
            }
            set
            {
                this.m_Groups = value;
            }
        }

        public abstract int ComputePoints(int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type);

        public abstract int ComputeGold(int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type);

        public virtual int ComputeFame(SmallBOD bod)
        {
            int points = this.ComputePoints(bod) / 50;

            return points * points;
        }

        public virtual int ComputeFame(LargeBOD bod)
        {
            int points = this.ComputePoints(bod) / 50;

            return points * points;
        }

        public virtual int ComputePoints(SmallBOD bod)
        {
            return this.ComputePoints(bod.AmountMax, bod.RequireExceptional, bod.Material, 1, bod.Type);
        }

        public virtual int ComputePoints(LargeBOD bod)
        {
            return this.ComputePoints(bod.AmountMax, bod.RequireExceptional, bod.Material, bod.Entries.Length, bod.Entries[0].Details.Type);
        }

        public virtual int ComputeGold(SmallBOD bod)
        {
            return this.ComputeGold(bod.AmountMax, bod.RequireExceptional, bod.Material, 1, bod.Type);
        }

        public virtual int ComputeGold(LargeBOD bod)
        {
            return this.ComputeGold(bod.AmountMax, bod.RequireExceptional, bod.Material, bod.Entries.Length, bod.Entries[0].Details.Type);
        }

        public virtual RewardGroup LookupRewards(int points)
        {
            for (int i = this.m_Groups.Length - 1; i >= 1; --i)
            {
                RewardGroup group = this.m_Groups[i];

                if (points >= group.Points)
                    return group;
            }

            return this.m_Groups[0];
        }

        public virtual int LookupTypePoints(RewardType[] types, Type type)
        {
            for (int i = 0; i < types.Length; ++i)
            {
                if (types[i].Contains(type))
                    return types[i].Points;
            }

            return 0;
        }

        public RewardCalculator()
        {
        }
    }

    public sealed class SmithRewardCalculator : RewardCalculator
    {
        #region Constructors
        private static readonly ConstructCallback SturdyShovel = new ConstructCallback(CreateSturdyShovel);
        private static readonly ConstructCallback SturdyPickaxe = new ConstructCallback(CreateSturdyPickaxe);
        private static readonly ConstructCallback MiningGloves = new ConstructCallback(CreateMiningGloves);
        private static readonly ConstructCallback GargoylesPickaxe = new ConstructCallback(CreateGargoylesPickaxe);
        private static readonly ConstructCallback ProspectorsTool = new ConstructCallback(CreateProspectorsTool);
        private static readonly ConstructCallback PowderOfTemperament = new ConstructCallback(CreatePowderOfTemperament);
        private static readonly ConstructCallback RunicHammer = new ConstructCallback(CreateRunicHammer);
        private static readonly ConstructCallback PowerScroll = new ConstructCallback(CreatePowerScroll);
        private static readonly ConstructCallback ColoredAnvil = new ConstructCallback(CreateColoredAnvil);
        private static readonly ConstructCallback AncientHammer = new ConstructCallback(CreateAncientHammer);
		//daat99 OWLTR start - bod rewards
		private static readonly ConstructCallback Deco = new ConstructCallback( CreateDeco );
		private static readonly ConstructCallback SturdySmithHammer = new ConstructCallback( CreateSturdySmithHammer );
		private static readonly ConstructCallback SmithersProtector = new ConstructCallback( CreateSmithersProtector );
		private static readonly ConstructCallback SharpeningBlade = new ConstructCallback( CreateSharpeningBlade );
		private static readonly ConstructCallback ColoredForgeDeed = new ConstructCallback( CreateColoredForgeDeed );
		private static readonly ConstructCallback ChargedDyeTub = new ConstructCallback( CreateChargedDyeTub );
		private static readonly ConstructCallback DaatsBagOfResources = new ConstructCallback( CreateDaatsBagOfResources );
		private static readonly ConstructCallback ArmorOfCrafting = new ConstructCallback( CreateArmorOfCrafting );
		private static Item CreateDeco( int type )
		{
			switch (type)
			{
				case 0: default: return new Deco( 5053, "Chainmail Tunic" );
				case 1: return new Deco( 5052, "chainmail Leggings" );
				case 2: return new Deco( 5402, "Decorative Armor" );
				case 3: return new Deco( 5509, "Decorative Shield and Sword" );
				case 4: return new Deco( 7110, "Decorative Scale Shield" );
				case 5: return new Deco( 10324, "Sword Display" );
			}
		}
		private static Item CreateSturdySmithHammer( int type )
		{
			return new SturdySmithHammer();
		}
		private static Item CreateSmithersProtector( int type )
		{
			return new SmithersProtector();
		}
		private static Item CreateSharpeningBlade( int type )
		{
			return new SharpeningBlade();
		}
		private static Item CreateColoredForgeDeed( int type )
		{
			return new ColoredForgeDeed( CraftResources.GetHue( (CraftResource)Utility.RandomMinMax( (int)CraftResource.DullCopper, (int)CraftResource.Platinum ) ) );
		}
		private static Item CreateChargedDyeTub( int type )
		{
			return new ChargedDyeTub( 10, type );
		}
		private static Item CreateDaatsBagOfResources( int type )
		{
			return new DaatsBagOfResources();
		}
		private static Item CreateArmorOfCrafting( int type )
		{
			switch (type)
			{
				case 1: default: return new ArmorOfCrafting( 1, 5062, Utility.Random(2)); //gloves
				case 2: return new ArmorOfCrafting( 1, 7609, Utility.Random(2)); //cap
				case 3: return new ArmorOfCrafting( 1, 5068, Utility.Random(2)); //tunic
				case 4: return new ArmorOfCrafting( 1, 5063, Utility.Random(2)); //gorget
				case 5: return new ArmorOfCrafting( 1, 5069, Utility.Random(2)); //arms
				case 6: return new ArmorOfCrafting( 1, 5067, Utility.Random(2)); //leggings
				case 7: return new ArmorOfCrafting( 3, 5062, Utility.Random(2)); //gloves
				case 8: return new ArmorOfCrafting( 3, 7609, Utility.Random(2)); //cap
				case 9: return new ArmorOfCrafting( 3, 5068, Utility.Random(2)); //tunic
				case 10: return new ArmorOfCrafting( 3, 5063, Utility.Random(2)); //gorget
				case 11: return new ArmorOfCrafting( 3, 5069, Utility.Random(2)); //arms
				case 12: return new ArmorOfCrafting( 3, 5067, Utility.Random(2)); //leggings
				case 13: return new ArmorOfCrafting( 5, 5062, Utility.Random(2)); //gloves
				case 14: return new ArmorOfCrafting( 5, 7609, Utility.Random(2)); //cap
				case 15: return new ArmorOfCrafting( 5, 5068, Utility.Random(2)); //tunic
				case 16: return new ArmorOfCrafting( 5, 5063, Utility.Random(2)); //gorget
				case 17: return new ArmorOfCrafting( 5, 5069, Utility.Random(2)); //arms
				case 18: return new ArmorOfCrafting( 5, 5067, Utility.Random(2)); //leggings
			}
		}
		//daat99 OWLTR end - bod rewards

        private static Item CreateSturdyShovel(int type)
        {
            return new SturdyShovel();
        }

        private static Item CreateSturdyPickaxe(int type)
        {
            return new SturdyPickaxe();
        }

        private static Item CreateMiningGloves(int type)
        {
            if (type == 1)
                return new LeatherGlovesOfMining(1);
            else if (type == 3)
                return new StuddedGlovesOfMining(3);
            else if (type == 5)
                return new RingmailGlovesOfMining(5);

            throw new InvalidOperationException();
        }

        private static Item CreateGargoylesPickaxe(int type)
        {
            return new GargoylesPickaxe();
        }

        private static Item CreateProspectorsTool(int type)
        {
            return new ProspectorsTool();
        }

        private static Item CreatePowderOfTemperament(int type)
        {
            return new PowderOfTemperament();
        }

        private static Item CreateRunicHammer(int type)
        {
            if (type >= 1 && type <= 8)
                return new RunicHammer(CraftResource.Iron + type, Core.AOS ? (55 - (type * 5)) : 50);

            throw new InvalidOperationException();
        }

        private static Item CreatePowerScroll(int type)
        {
            if (type == 5 || type == 10 || type == 15 || type == 20)
                return new PowerScroll(SkillName.Blacksmith, 100 + type);

            throw new InvalidOperationException();
        }

        private static Item CreateColoredAnvil(int type)
        {
            // Generate an anvil deed, not an actual anvil.
            //return new ColoredAnvilDeed();
            return new ColoredAnvil();
        }

        private static Item CreateAncientHammer(int type)
        {
            if (type == 10 || type == 15 || type == 30 || type == 60)
                return new AncientSmithyHammer(type);

            throw new InvalidOperationException();
        }

        #endregion

        public static readonly SmithRewardCalculator Instance = new SmithRewardCalculator();

        private readonly RewardType[] m_Types = new RewardType[]
        {
            // Armors
            new RewardType(200, typeof(RingmailGloves), typeof(RingmailChest), typeof(RingmailArms), typeof(RingmailLegs)),
            new RewardType(300, typeof(ChainCoif), typeof(ChainLegs), typeof(ChainChest)),
            new RewardType(400, typeof(PlateArms), typeof(PlateLegs), typeof(PlateHelm), typeof(PlateGorget), typeof(PlateGloves), typeof(PlateChest)),

            // Weapons
            new RewardType(200, typeof(Bardiche), typeof(Halberd)),
            new RewardType(300, typeof(Dagger), typeof(ShortSpear), typeof(Spear), typeof(WarFork), typeof(Kryss)), //OSI put the dagger in there.  Odd, ain't it.
            new RewardType(350, typeof(Axe), typeof(BattleAxe), typeof(DoubleAxe), typeof(ExecutionersAxe), typeof(LargeBattleAxe), typeof(TwoHandedAxe)),
            new RewardType(350, typeof(Broadsword), typeof(Cutlass), typeof(Katana), typeof(Longsword), typeof(Scimitar), /*typeof( ThinLongsword ),*/ typeof(VikingSword)),
            new RewardType(350, typeof(WarAxe), typeof(HammerPick), typeof(Mace), typeof(Maul), typeof(WarHammer), typeof(WarMace))
        };

        public override int ComputePoints(int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type)
        {
            int points = 0;

            if (quantity == 10)
                points += 10;
            else if (quantity == 15)
                points += 25;
            else if (quantity == 20)
                points += 50;

            if (exceptional)
                points += 200;

            if (itemCount > 1)
                points += this.LookupTypePoints(this.m_Types, type);

			//daat99 OWLTR start - custom resources
			if ( material >= BulkMaterialType.DullCopper && material <= BulkMaterialType.Platinum )
			//daat99 OWLTR end - custom resources
                points += 200 + (50 * (material - BulkMaterialType.DullCopper));

            return points;
        }

        private static readonly int[][][] m_GoldTable = new int[][][]
        {
			//daat99 OWLTR start - custom gold reward
            new int[][] // 1-part (regular)
            {
				new int[]{ 100, 200, 300,  400,  500,  600,  700,  800,  900, 1000, 1100, 1200, 1300, 1400 },
				new int[]{ 200, 400, 600,  800, 1000, 1200, 1400, 1600, 1800, 2000, 2200, 2400, 2600, 2800 },
				new int[]{ 300, 600, 900, 1200, 1500, 1800, 2100, 2400, 2700, 3000, 3300, 3600, 3900, 4200 }
            },
            new int[][] // 1-part (exceptional)
            {
				new int[]{ 250, 500,  750, 1000, 1250, 1500, 1750, 2000,  2250, 2500, 2750, 3000, 3250, 3500 },
				new int[]{ 350, 700, 1050, 1400, 1750, 2100, 2450, 2800,  3150, 3500, 3850, 4200, 4550, 4900 },
				new int[]{ 450, 900, 1350, 1800, 2250, 2700, 3150, 3600,  4050, 4500, 4950, 5400, 5850, 6300 }
            },
            new int[][] // Ringmail (regular)
            {
				new int[]{ 2000, 4000,  6000,  8000, 10000, 12000, 14000, 16000, 18000, 20000, 22000, 24000, 26000, 28000 },
				new int[]{ 3000, 6000,  9000, 12000, 15000, 18000, 21000, 24000, 27000, 30000, 33000, 36000, 39000, 42000 },
				new int[]{ 4000, 8000, 12000, 16000, 20000, 24000, 28000, 32000, 36000, 40000, 44000, 48000, 52000, 56000 }
            },
            new int[][] // Ringmail (exceptional)
            {
				new int[]{ 4000,  8000, 12000, 16000, 20000, 24000, 28000, 32000, 36000, 40000, 44000, 48000,  52000,  56000 },
				new int[]{ 6000, 12000, 18000, 24000, 30000, 36000, 42000, 48000, 54000, 60000, 66000, 72000,  78000,  84000 },
				new int[]{ 8000, 16000, 24000, 32000, 40000, 48000, 56000, 64000, 72000, 80000, 88000, 96000, 104000, 112000 }
            },
            new int[][] // Chainmail (regular)
            {
				new int[]{ 4000,  8000, 12000, 16000, 20000, 24000, 28000, 32000, 36000, 40000, 44000, 48000,  52000,  56000 },
				new int[]{ 6000, 12000, 18000, 24000, 30000, 36000, 42000, 48000, 54000, 60000, 66000, 72000,  78000,  84000 },
				new int[]{ 8000, 16000, 24000, 32000, 40000, 48000, 56000, 64000, 72000, 80000, 88000, 96000, 104000, 112000 }
            },
            new int[][] // Chainmail (exceptional)
            {
				new int[]{  7000, 14000, 21000, 28000, 35000, 42000,  49000,  56000,  63000,  70000,  77000,  84000,  91000,  98000 },
				new int[]{ 10000, 20000, 30000, 40000, 50000, 60000,  70000,  80000,  90000, 100000, 110000, 120000, 130000, 140000 },
				new int[]{ 15000, 30000, 45000, 60000, 75000, 90000, 105000, 120000, 135000, 150000, 165000, 180000, 195000, 210000 }
            },
            new int[][] // Platemail (regular)
            {
				new int[]{  5000, 10000, 15000, 20000, 25000, 30000,  35000,  40000,  45000,  50000,  55000,  60000,  65000,  70000 },
				new int[]{  7500, 15000, 22500, 30000, 37500, 45000,  52500,  60000,  67500,  75000,  82500,  90000,  97500, 105000 },
				new int[]{ 10000, 20000, 30000, 40000, 50000, 60000,  70000,  80000,  90000, 100000, 110000, 120000, 130000, 140000 }
            },
            new int[][] // Platemail (exceptional)
            {
				new int[]{ 10000, 20000, 30000, 40000,  50000,  60000,  70000,  80000,  90000, 100000, 110000, 120000, 130000, 140000 },
				new int[]{ 15000, 30000, 45000, 60000,  75000,  90000, 105000, 120000, 135000, 150000, 165000, 180000, 195000, 210000 },
				new int[]{ 20000, 40000, 60000, 80000, 100000, 120000, 140000, 160000, 180000, 200000, 220000, 240000, 260000, 280000 }
            },
            new int[][] // 2-part weapons (regular)
            {
				new int[]{ 3000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 4500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 6000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            },
            new int[][] // 2-part weapons (exceptional)
            {
				new int[]{ 5000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 7500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 10000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            },
            new int[][] // 5-part weapons (regular)
            {
				new int[]{ 4000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 6000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 8000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            },
            new int[][] // 5-part weapons (exceptional)
            {
				new int[]{ 7500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 11250, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 15000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            },
            new int[][] // 6-part weapons (regular)
            {
				new int[]{ 4000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 6000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 10000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            },
            new int[][] // 6-part weapons (exceptional)
            {
				new int[]{ 7500, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 11250, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				new int[]{ 15000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            }
			//daat99 OWLTR start - custom gold reward
        };

        private int ComputeType(Type type, int itemCount)
        {
            // Item count of 1 means it's a small BOD.
            if (itemCount == 1)
                return 0;

            int typeIdx;

            // Loop through the RewardTypes defined earlier and find the correct one.
			//daat99 OWLTR start - don't use magic numbers...
			for ( typeIdx = 0; typeIdx < m_Types.Length; ++typeIdx )
			//daat99 OWLTR end - don't use magic numbers...
            {
                if (this.m_Types[typeIdx].Contains(type))
                    break;
            }

			//daat99 OWLTR start - custom bods
			//daat99 note: make it last 3 types, not specific index!
			// Types 5, 6 and 7 are Large Weapon BODs with the same rewards.
			if ( typeIdx > m_Types.Length-2 )
				typeIdx = m_Types.Length-2;
			//daat99 OWLTR end - custom bods

            return (typeIdx + 1) * 2;
        }

        public override int ComputeGold(int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type)
        {
            int[][][] goldTable = m_GoldTable;

            int typeIndex = this.ComputeType(type, itemCount);
            int quanIndex = (quantity == 20 ? 2 : quantity == 15 ? 1 : 0);
            int mtrlIndex = (material >= BulkMaterialType.DullCopper && material <= BulkMaterialType.Platinum) ? 1 + (int)(material - BulkMaterialType.DullCopper) : 0;

            if (exceptional)
                typeIndex++;
            if (typeIndex >= goldTable.GetUpperBound(0))
                typeIndex = goldTable.GetUpperBound(0);

            int gold = goldTable[typeIndex][quanIndex][mtrlIndex];

            int min = (gold * 9) / 10;
            int max = (gold * 10) / 9;

            return Utility.RandomMinMax(min, max);
        }

        public SmithRewardCalculator()
        {
            this.Groups = new RewardGroup[]
            {
				//daat99 OWLTR start - bod reward
				new RewardGroup(    0, new RewardItem( 1, SturdyShovel ), new RewardItem( 1, SturdySmithHammer ) ),
				new RewardGroup(   25, new RewardItem( 1, SturdyPickaxe ), new RewardItem( 1, SturdySmithHammer ) ),
				new RewardGroup(   50, new RewardItem( 90, SturdyShovel ), new RewardItem( 10, ArmorOfCrafting, Utility.RandomMinMax(1,6) ) ),
				new RewardGroup(  200, new RewardItem( 90, SturdyPickaxe ), new RewardItem( 10, ArmorOfCrafting, Utility.RandomMinMax(1,6) ) ),
				new RewardGroup(  400, new RewardItem( 90, ProspectorsTool ), new RewardItem( 10, ArmorOfCrafting, Utility.RandomMinMax(1,6) ) ),
				new RewardGroup(  450, new RewardItem( 2, PowderOfTemperament ), new RewardItem( 1, GargoylesPickaxe ), new RewardItem( 1, Deco, Utility.Random(6) ) ),
				new RewardGroup(  500, new RewardItem( 1, RunicHammer, 1 ), new RewardItem( 1, GargoylesPickaxe ), new RewardItem( 1, Deco, Utility.Random(6) ) ),
                new RewardGroup(  550, new RewardItem(3, RunicHammer, 1), new RewardItem(2, RunicHammer, 2)),
				new RewardGroup(  600, new RewardItem( 1, RunicHammer, 2 ), new RewardItem( 1, ColoredForgeDeed ) ),
				new RewardGroup(  625, new RewardItem( 3, RunicHammer, 2 ), new RewardItem( 1, ColoredAnvil ) ),
				new RewardGroup(  650, new RewardItem( 1, RunicHammer, 3 ), new RewardItem( 1, Deco, Utility.Random(6) ) ),
				new RewardGroup(  675, new RewardItem( 1, ColoredAnvil ), new RewardItem( 3, RunicHammer, 3 ) ),
				new RewardGroup(  700, new RewardItem( 1, RunicHammer, 4 ), new RewardItem(1, PowerScroll, 5), new RewardItem( 1, Deco, Utility.Random(6) ) ),
				new RewardGroup(  750, new RewardItem( 1, AncientHammer, 10 ), new RewardItem( 1, ArmorOfCrafting, Utility.RandomMinMax(7,12) ) ),
				new RewardGroup(  800, new RewardItem( 1, GargoylesPickaxe ), new RewardItem( 1, ArmorOfCrafting, Utility.RandomMinMax(7,12) ) ),
				new RewardGroup(  850, new RewardItem( 1, AncientHammer, 15 ), new RewardItem( 1, ArmorOfCrafting, Utility.RandomMinMax(7,12) ) ),
				new RewardGroup(  900, new RewardItem( 1, RunicHammer, 5 ), new RewardItem(1, PowerScroll, 10), new RewardItem( 1, ChargedDyeTub, Utility.RandomMinMax(1,2) ) ),
				new RewardGroup(  950, new RewardItem( 1, RunicHammer, 5 ), new RewardItem( 1, ChargedDyeTub, Utility.RandomMinMax(1,2) ) ),
				new RewardGroup( 1000, new RewardItem( 1, AncientHammer, 30 ), new RewardItem( 1, ChargedDyeTub, Utility.RandomMinMax(1,2) ) ),
				new RewardGroup( 1050, new RewardItem( 1, RunicHammer, 6 ), new RewardItem( 1, SmithersProtector ) ),
				new RewardGroup( 1100, new RewardItem( 1, AncientHammer, 60 ), new RewardItem(1, PowerScroll, 15), new RewardItem( 1, SmithersProtector ) ),
				new RewardGroup( 1150, new RewardItem( 1, RunicHammer, 7 ), new RewardItem( 1, ArmorOfCrafting, Utility.RandomMinMax(13,18) ) ),
				new RewardGroup( 1200, new RewardItem( 1, RunicHammer, 8 ), new RewardItem( 1, ArmorOfCrafting, Utility.RandomMinMax(13,18) ) ),
				new RewardGroup( 1250, new RewardItem( 1, RunicHammer, 9 ), new RewardItem( 1, ArmorOfCrafting, Utility.RandomMinMax(13,18) ) ),
				new RewardGroup( 1300, new RewardItem( 1, RunicHammer, 10 ), new RewardItem(1, PowerScroll, 20), new RewardItem( 1, DaatsBagOfResources ) ),
				new RewardGroup( 1350, new RewardItem( 1, RunicHammer, 11 ), new RewardItem( 1, DaatsBagOfResources ) ),
				new RewardGroup( 1400, new RewardItem( 1, RunicHammer, 12 ), new RewardItem( 1, SharpeningBlade ) ),
				new RewardGroup( 1450, new RewardItem( 1, RunicHammer, 13 ), new RewardItem( 1, SharpeningBlade ) )
				//daat99 OWLTR end - bod reward
            };
        }
    }

    public sealed class TailorRewardCalculator : RewardCalculator
    {
        #region Constructors
        private static readonly ConstructCallback Cloth = new ConstructCallback(CreateCloth);
        private static readonly ConstructCallback Sandals = new ConstructCallback(CreateSandals);
        private static readonly ConstructCallback StretchedHide = new ConstructCallback(CreateStretchedHide);
        private static readonly ConstructCallback RunicKit = new ConstructCallback(CreateRunicKit);
        private static readonly ConstructCallback Tapestry = new ConstructCallback(CreateTapestry);
        private static readonly ConstructCallback PowerScroll = new ConstructCallback(CreatePowerScroll);
        private static readonly ConstructCallback BearRug = new ConstructCallback(CreateBearRug);
        private static readonly ConstructCallback ClothingBlessDeed = new ConstructCallback(CreateCBD);
		//daat99 OWLTR start - bod reward
		private static readonly ConstructCallback ArmorOfCrafting = new ConstructCallback( CreateArmorOfCrafting );
		private static readonly ConstructCallback TailorsProtector = new ConstructCallback( CreateTailorsProtector );
		private static readonly ConstructCallback SturdySewingKit = new ConstructCallback( CreateSturdySewingKit );
		private static readonly ConstructCallback MastersKnife = new ConstructCallback( CreateMastersKnife );
		private static readonly ConstructCallback GargoylesKnife = new ConstructCallback( CreateGargoylesKnife );
		private static readonly ConstructCallback ColoredScissors = new ConstructCallback( CreateColoredScissors );
		private static readonly ConstructCallback ColoredLoom = new ConstructCallback( CreateColoredLoom );
		private static readonly ConstructCallback ChargedDyeTub = new ConstructCallback( CreateChargedDyeTub );
		private static readonly ConstructCallback BagOfResources = new ConstructCallback( CreateBagOfResources );
		private static readonly ConstructCallback Deco = new ConstructCallback( CreateDeco );
		private static Item CreateArmorOfCrafting( int type )
		{
			switch (type)
			{
				case 1: default: return new ArmorOfCrafting( 1, 5062, 2 ); //gloves
				case 2: return new ArmorOfCrafting( 1, 7609, 2 ); //cap
				case 3: return new ArmorOfCrafting( 1, 5068, 2 ); //tunic
				case 4: return new ArmorOfCrafting( 1, 5063, 2 ); //gorget
				case 5: return new ArmorOfCrafting( 1, 5069, 2 ); //arms
				case 6: return new ArmorOfCrafting( 1, 5067, 2 ); //leggings
				case 7: return new ArmorOfCrafting( 3, 5062, 2 ); //gloves
				case 8: return new ArmorOfCrafting( 3, 7609, 2 ); //cap
				case 9: return new ArmorOfCrafting( 3, 5068, 2 ); //tunic
				case 10: return new ArmorOfCrafting( 3, 5063, 2 ); //gorget
				case 11: return new ArmorOfCrafting( 3, 5069, 2 ); //arms
				case 12: return new ArmorOfCrafting( 3, 5067, 2 ); //leggings
				case 13: return new ArmorOfCrafting( 5, 5062, 2 ); //gloves
				case 14: return new ArmorOfCrafting( 5, 7609, 2 ); //cap
				case 15: return new ArmorOfCrafting( 5, 5068, 2 ); //tunic
				case 16: return new ArmorOfCrafting( 5, 5063, 2 ); //gorget
				case 17: return new ArmorOfCrafting( 5, 5069, 2 ); //arms
				case 18: return new ArmorOfCrafting( 5, 5067, 2 ); //leggings
			}
		}
		private static Item CreateTailorsProtector( int type )
		{
			return new TailorsProtector();
		}

		private static Item CreateSturdySewingKit( int type )
		{
			return new SturdySewingKit();
		}
		private static Item CreateGargoylesKnife( int type )
		{
			return new GargoylesKnife();
		}
		private static Item CreateMastersKnife( int type )
		{
			return new MastersKnife();
		}
		private static Item CreateColoredScissors( int type )
		{
			return new ColoredScissors( CraftResources.GetHue( (CraftResource)Utility.RandomMinMax( (int)CraftResource.SpinedLeather, (int)CraftResource.EtherealLeather ) ), 25 );
		}
		private static Item CreateColoredLoom( int type )
		{
			if (Utility.Random(2) == 1)
				return new ColoredLoomSouthDeed( CraftResources.GetHue( (CraftResource)Utility.RandomMinMax( (int)CraftResource.SpinedLeather, (int)CraftResource.EtherealLeather ) ) );
			else
				return new ColoredLoomEastDeed( CraftResources.GetHue( (CraftResource)Utility.RandomMinMax( (int)CraftResource.SpinedLeather, (int)CraftResource.EtherealLeather ) ) );
		}
		private static Item CreateChargedDyeTub( int type )
		{
			return new ChargedDyeTub( 0 );
		}
		private static Item CreateBagOfResources( int type )
		{
			return new BagOfResources();
		}
		private static Item CreateDeco( int type )
		{
			switch (type)
			{
				case 0: default: return new Deco( 4054, "Tapestry" );
				case 1: return new Deco( 9036, "Rose of Trinsic" );
				case 2: return new Deco( 15721, "Deer Corspe" );
				case 3: return new Deco( 5610, "Banner" );
			}
		}
		//daat99 OWLTR end - bod reward
		
        private static readonly int[][] m_ClothHues = new int[][]
        {
            new int[] { 0x483, 0x48C, 0x488, 0x48A },
            new int[] { 0x495, 0x48B, 0x486, 0x485 },
            new int[] { 0x48D, 0x490, 0x48E, 0x491 },
            new int[] { 0x48F, 0x494, 0x484, 0x497 },
            new int[] { 0x489, 0x47F, 0x482, 0x47E }
        };

        private static Item CreateCloth(int type)
        {
            if (type >= 0 && type < m_ClothHues.Length)
            {
                UncutCloth cloth = new UncutCloth(100);
                cloth.Hue = m_ClothHues[type][Utility.Random(m_ClothHues[type].Length)];
                return cloth;
            }

            throw new InvalidOperationException();
        }

        private static readonly int[] m_SandalHues = new int[]
        {
            0x489, 0x47F, 0x482,
            0x47E, 0x48F, 0x494,
            0x484, 0x497
        };

        private static Item CreateSandals(int type)
        {
            return new Sandals(m_SandalHues[Utility.Random(m_SandalHues.Length)]);
        }

        private static Item CreateStretchedHide(int type)
        {
            switch ( Utility.Random(4) )
            {
                default:
                case 0:
                    return new SmallStretchedHideEastDeed();
                case 1:
                    return new SmallStretchedHideSouthDeed();
                case 2:
                    return new MediumStretchedHideEastDeed();
                case 3:
                    return new MediumStretchedHideSouthDeed();
            }
        }

        private static Item CreateTapestry(int type)
        {
            switch ( Utility.Random(4) )
            {
                default:
                case 0:
                    return new LightFlowerTapestryEastDeed();
                case 1:
                    return new LightFlowerTapestrySouthDeed();
                case 2:
                    return new DarkFlowerTapestryEastDeed();
                case 3:
                    return new DarkFlowerTapestrySouthDeed();
            }
        }

        private static Item CreateBearRug(int type)
        {
            switch ( Utility.Random(4) )
            {
                default:
                case 0:
                    return new BrownBearRugEastDeed();
                case 1:
                    return new BrownBearRugSouthDeed();
                case 2:
                    return new PolarBearRugEastDeed();
                case 3:
                    return new PolarBearRugSouthDeed();
            }
        }

        private static Item CreateRunicKit(int type)
        {
			//daat99 OWLTR start - bod reward
			if ( type >= 1 && type <= 10 )
				return new RunicSewingKit( CraftResource.RegularLeather + type, 100 - (type*5) );
			//daat99 OWLTR end - bod reward

            throw new InvalidOperationException();
        }

        private static Item CreatePowerScroll(int type)
        {
            if (type == 5 || type == 10 || type == 15 || type == 20)
                return new PowerScroll(SkillName.Tailoring, 100 + type);

            throw new InvalidOperationException();
        }

        private static Item CreateCBD(int type)
        {
            return new ClothingBlessDeed();
        }

        #endregion

        public static readonly TailorRewardCalculator Instance = new TailorRewardCalculator();

        public override int ComputePoints(int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type)
        {
            int points = 0;

            if (quantity == 10)
                points += 10;
            else if (quantity == 15)
                points += 25;
            else if (quantity == 20)
                points += 50;

            if (exceptional)
                points += 100;

            if (itemCount == 4)
                points += 300;
            else if (itemCount == 5)
                points += 400;
            else if (itemCount == 6)
                points += 500;
			//daat99 OWLTR start - bod rewards
			if ( material >= BulkMaterialType.Spined && material <= BulkMaterialType.Ethereal )
				points += 50 + (50 * (material - BulkMaterialType.Spined));
			//daat99 OWLTR end - bod rewards

            return points;
        }

		//daat99 OWLTR start - gold reward
        private static readonly int[][][] m_AosGoldTable = new int[][][]
        {
            new int[][] // 1-part (regular)
            {
					new int[]{ 100, 200, 300,  400,  500,  600,  700,  800,  900, 1000, 1100 },
					new int[]{ 200, 400, 600,  800, 1000, 1200, 1400, 1600, 1800, 2000, 2200 },
					new int[]{ 300, 600, 900, 1200, 1500, 1800, 2100, 2400, 2700, 3000, 3300 }
            },
            new int[][] // 5-part (exceptional)
            {
					new int[]{ 250, 500,  750, 1000, 1250, 1500, 1750, 2000,  2250, 2500, 2750 },
					new int[]{ 350, 700, 1050, 1400, 1750, 2100, 2450, 2800,  3150, 3500, 3850 },
					new int[]{ 450, 900, 1350, 1800, 2250, 2700, 3150, 3600,  4050, 4500, 4950 }
            },
            new int[][] // 6-part (regular)
            {
					new int[]{ 2000, 4000,  6000,  8000, 10000, 12000, 14000, 16000, 18000, 20000, 22000 },
					new int[]{ 3000, 6000,  9000, 12000, 15000, 18000, 21000, 24000, 27000, 30000, 33000 },
					new int[]{ 4000, 8000, 12000, 16000, 20000, 24000, 28000, 32000, 36000, 40000, 44000 }
            },
            new int[][] // 6-part (exceptional)
            {
					new int[]{ 4000,  8000, 12000, 16000, 20000, 24000, 28000, 32000, 36000, 40000, 44000 },
					new int[]{ 6000, 12000, 18000, 24000, 30000, 36000, 42000, 48000, 54000, 60000, 66000 },
					new int[]{ 8000, 16000, 24000, 32000, 40000, 48000, 56000, 64000, 72000, 80000, 88000 }

            },
            new int[][] // 1-part (exceptional)
            {
					new int[]{ 4000,  8000, 12000, 16000, 20000, 24000, 28000, 32000, 36000, 40000, 44000 },
					new int[]{ 6000, 12000, 18000, 24000, 30000, 36000, 42000, 48000, 54000, 60000, 66000 },
					new int[]{ 8000, 16000, 24000, 32000, 40000, 48000, 56000, 64000, 72000, 80000, 88000 }
            },
            new int[][] // 4-part (regular)
            {
					new int[]{  7000, 14000, 21000, 28000, 35000, 42000,  49000,  56000,  63000,  70000,  77000 },
					new int[]{ 10000, 20000, 30000, 40000, 50000, 60000,  70000,  80000,  90000, 100000, 110000 },
					new int[]{ 15000, 30000, 45000, 60000, 75000, 90000, 105000, 120000, 135000, 150000, 165000 }
            },
            new int[][] // 4-part (exceptional)
            {
					new int[]{  5000, 10000, 15000, 20000, 25000, 30000,  35000,  40000,  45000,  50000,  55000 },
					new int[]{  7500, 15000, 22500, 30000, 37500, 45000,  52500,  60000,  67500,  75000,  82500 },
					new int[]{ 10000, 20000, 30000, 40000, 50000, 60000,  70000,  80000,  90000, 100000, 110000 }
            },
            new int[][] // 5-part (regular)
            {
					new int[]{ 10000, 20000, 30000, 40000,  50000,  60000,  70000,  80000,  90000, 100000, 110000 },
					new int[]{ 15000, 30000, 45000, 60000,  75000,  90000, 105000, 120000, 135000, 150000, 165000 },
					new int[]{ 20000, 40000, 60000, 80000, 100000, 120000, 140000, 160000, 180000, 200000, 220000 }
            }
        };

		private static int[][][] m_OldGoldTable = m_AosGoldTable;
		//daat99 OWLTR end - gold reward
        public override int ComputeGold(int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type)
        {
            int[][][] goldTable = (Core.AOS ? m_AosGoldTable : m_OldGoldTable);

            int typeIndex = ((itemCount == 6 ? 3 : itemCount == 5 ? 2 : itemCount == 4 ? 1 : 0) * 2) + (exceptional ? 1 : 0);
            int quanIndex = (quantity == 20 ? 2 : quantity == 15 ? 1 : 0);
			//daat99 OWLTR start - bod material
			int mtrlIndex = ( material >= BulkMaterialType.Spined && material <= BulkMaterialType.Ethereal ) ? 1 + (int)(material - BulkMaterialType.Spined) : 0;
			//daat99 OWLTR end - bod material

            int gold = goldTable[typeIndex][quanIndex][mtrlIndex];

            int min = (gold * 9) / 10;
            int max = (gold * 10) / 9;

            return Utility.RandomMinMax(min, max);
        }

        public TailorRewardCalculator()
        {
            this.Groups = new RewardGroup[]
			//daat99 OWLTR start - bod reward
            {
				new RewardGroup(    0, new RewardItem( 1, Cloth, 0 ), new RewardItem( 1, ColoredLoom ) ),
				new RewardGroup(   50, new RewardItem( 1, Cloth, 1 ), new RewardItem( 1, ColoredLoom ) ),
				new RewardGroup(  100, new RewardItem( 1, Cloth, 2 ), new RewardItem( 1, Sandals ) ),
				new RewardGroup(  150, new RewardItem( 6, Cloth, 3 ), new RewardItem( 1, ArmorOfCrafting, Utility.RandomMinMax(1,6) ), new RewardItem( 3, Sandals ) ),
				new RewardGroup(  200, new RewardItem( 2, Cloth, 4 ), new RewardItem( 1, ArmorOfCrafting, Utility.RandomMinMax(1,6) ), new RewardItem( 2, Sandals ) ),
				new RewardGroup(  300, new RewardItem( 1, StretchedHide ), new RewardItem( 1, ArmorOfCrafting, Utility.RandomMinMax(1,6) ), new RewardItem( 1, ColoredScissors ) ),
				new RewardGroup(  350, new RewardItem( 1, RunicKit, 1 ), new RewardItem(2, PowerScroll, 5), new RewardItem( 1, ColoredScissors ) ),
				new RewardGroup(  400, new RewardItem( 3, Tapestry ), new RewardItem( 1, SturdySewingKit ), new RewardItem( 1, ColoredScissors ) ),
				new RewardGroup(  450, new RewardItem( 1, BearRug ), new RewardItem( 1, SturdySewingKit ) ),
				new RewardGroup(  500, new RewardItem( 1, Deco, Utility.Random(4) ), new RewardItem( 1, MastersKnife ) ),
				new RewardGroup(  550, new RewardItem( 1, ClothingBlessDeed ), new RewardItem( 1, Deco, Utility.Random(4) ), new RewardItem( 1, MastersKnife ) ),
				new RewardGroup(  600, new RewardItem( 1, RunicKit, 2 ), new RewardItem(1, PowerScroll, 10), new RewardItem( 1, MastersKnife ) ),
				new RewardGroup(  650, new RewardItem( 1, RunicKit, 2 ), new RewardItem( 1, ArmorOfCrafting, Utility.RandomMinMax(7,12) ) ),
				new RewardGroup(  700, new RewardItem( 1, RunicKit, 3 ), new RewardItem( 1, ArmorOfCrafting, Utility.RandomMinMax(7,12) ) ),
				new RewardGroup(  750, new RewardItem( 1, RunicKit, 3 ), new RewardItem( 1, ArmorOfCrafting, Utility.RandomMinMax(7,12) ), new RewardItem( 1, GargoylesKnife ) ),
				new RewardGroup(  800, new RewardItem( 1, RunicKit, 4 ), new RewardItem( 1, ChargedDyeTub ), new RewardItem( 1, GargoylesKnife ) ),
				new RewardGroup(  850, new RewardItem( 1, RunicKit, 4 ), new RewardItem(1, PowerScroll, 15), new RewardItem( 1, ChargedDyeTub ) ),
				new RewardGroup(  900, new RewardItem( 1, RunicKit, 5 ), new RewardItem( 1, ArmorOfCrafting, Utility.RandomMinMax(13,18) ) ),
				new RewardGroup(  950, new RewardItem( 1, RunicKit, 6 ), new RewardItem( 1, ArmorOfCrafting, Utility.RandomMinMax(13,18) ) ),
				new RewardGroup( 1000, new RewardItem( 1, RunicKit, 7 ), new RewardItem( 1, TailorsProtector ), new RewardItem( 1, ArmorOfCrafting, Utility.RandomMinMax(13,18) ) ),
				new RewardGroup( 1050, new RewardItem( 1, RunicKit, 8 ), new RewardItem( 1, TailorsProtector ) ),
				new RewardGroup( 1100, new RewardItem( 1, RunicKit, 9 ), new RewardItem(1, PowerScroll, 20), new RewardItem( 1, BagOfResources ) ),
				new RewardGroup( 1150, new RewardItem( 1, RunicKit, 10 ), new RewardItem( 1, BagOfResources ) ),
            };
			//daat99 OWLTR end - bod reward
        }
    }
}
