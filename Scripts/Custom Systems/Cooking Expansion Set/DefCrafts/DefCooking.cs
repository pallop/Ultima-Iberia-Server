using System;
using Server.Items;

namespace Server.Engines.Craft
{
    #region Recipes
    public enum CookRecipes
    {
        // magical
        RotWormStew = 500,
        
        #region Custom Recipes
        #region Ingredients Recipes 5000 - 5100
        SackFlour = 5000,
        BagOfSugar = 5001,
        Dough = 5002,
        SweetDough = 5003,
        CocoaButter = 5004,
        CocoaLiquor = 5005,
         #endregion
        #region Oils Recipes 5101- 5200
        Batter = 5101,
        Butter = 5102,
        Cream =  5103,
        CookingOil = 5104,
        Vinegar = 5105,
        #endregion
        #region Sauces Recipes 5201 - 5300
        BarbecueSauce = 5201,
        CheeseSauce = 5202,
        EnchiladaSauce = 5203,
        Gravy = 5204,
        HotSauce = 5205,
        SoySauce = 5206,
        Teriyaki = 5207,
        TomatoSauce = 5208,
        #endregion
        #region Raw Meat Prep Recipes 5301 - 5400
        VenisonSteak = 5301,
        VenisonJerky = 5302,
        VenisonRoast = 5303,
        BeefPorterhouse = 5304,
        BeefPrimeRib = 5305,
        BeefRibeye = 5306,
        BeefRibs = 5307,
        BeefRoast = 5308,
        BeefSirloin = 5309,
        BeefJerky = 5310,
        BeefTBone = 5311,
        BeefTenderloin = 5312,
        GroundBeef = 5313,
        GoatSteak = 5314,
        GoatRoast = 5315,
        RawGroundPork = 5317,
        Bacon = 5318,
        BaconSlab = 5319,
        Ham = 5320,
        HamSlices = 5321,
        PigHead = 5322,
        PorkChop = 5323,
        PorkRoast = 5324,
        PorkSpareRibs = 5325,
        Trotters = 5326,
        Sausage = 5327,
        MuttonSteak = 5328,
        MuttonRoast = 5329,
        LambLeg = 5330,
        GroundPork = 5331,
        RoastChicken = 5332,
        ChickenLeg = 5333,
        RoastTurkey = 5334,
        TurkeyLeg = 5335,
        TurkeyPlatter = 5336,
        SlicedTurkey = 5337,
        RoastDuck = 5338,
        DuckLeg = 5339,
        CookedBird = 5340,
        Ribs = 5341,
        CookedSteak = 5342,
        #endregion
        #region Preparations Recipes 5401 - 5500
        PastaNoodles = 5401,
        PeanutButter = 5402,
        FruitJam = 5403,
        Tortilla = 5404,
        WoodPulp = 5405,
        GreenTea = 5406,
        WasabiClumps = 5407,
        SushiRolls = 5408,
        SushiPlatter = 5409,
        TribalPaint = 5410,
        EggBomb = 5411,
        DriedOnions = 5412,
        DriedHerbs = 5413,
        BasketOfHerbsFarm = 5414,
        CakeMix = 5415,
        CookieMix = 5416,
        ChocolateMix = 5417,
        AsianVegMix = 5418,
        MixedVegetables = 5419,
        PizzaCrust = 5420,
        WaffleMix = 5421,
        BowlCornFlakes = 5422,
        BowlRiceKrisps = 5423,
        FruitBasket = 5424,
        Tofu = 5425,
        ParrotWafer = 5426,
        PieMix = 5427,
        UnbakedQuiche = 5428,
        UnbakedMeatPie = 5429,
        UncookedSausagePizza = 5430,
        UncookedCheesePizza = 5431,
        UnbakedFruitPie = 5432,
        UnbakedPeachCobbler = 5433,
        UnbakedApplePie = 5434,
        UnbakedPumpkinPie = 5435,
        #endregion
        #region Baking Recipes 5501 - 5600
        BreadLoaf = 5501,
        GarlicBread = 5502,
        BananaBread = 5503,
        PumpkinBread = 5504,
        CornBread = 5505,
        Cookies = 5506,
        AlmondCookies = 5507,
        ChocChipCookies = 5508,
        GingerSnaps = 5509,
        OatmealCookies = 5510,
        PeanutButterCookies = 5511,
        PumpkinCookies = 5512,
        Cake = 5513,
        BananaCake = 5514,
        CarrotCake = 5515,
        ChocolateCake = 5516,
        CoconutCake = 5517,
        LemonCake = 5518,
        Muffins = 5519,
        BlueberryMuffins = 5520,
        PumpkinMuffins = 5521,
        SausagePizza = 5522,
        CheesePizza = 5523,
        HamPineapplePizza = 5524,
        MushroomOnionPizza = 5525,
        SausOnionMushPizza = 5526,
        TacoPizza = 5527,
        VeggiePizza = 5528,
        Quiche = 5529,
        MeatPie = 5530,
        FruitPie = 5531,
        PeachCobbler = 5532,
        ApplePie = 5533,
        PumpkinPie = 5534,
        BlueberryPie = 5535,
        CherryPie = 5536,
        KeyLimePie = 5537,
        LemonMerenguePie = 5538,
        BlackberryCobbler = 5539,
        ShepherdsPie = 5540,
        TurkeyPie = 5541,
        ChickenPie = 5542,
        BeefPie = 5543,
        Brownies = 5544,
        ChocSunflowerSeeds = 5545,
        RiceKrispTreat = 5546,
        BowlOatmeal = 5547,
        Popcorn = 5548,
        Pancakes = 5549,
        Waffles = 5550,
        #endregion
        #region Boiling Recipes 5601 - 5700
        ChickenNoodleSoup = 5601,
        TomatoRice = 5602,
        BowlOfStew = 5603,
        TomatoSoup = 5604,
        HarpyEggSoup = 5605,
        BowlBeets = 5606,
        BowlBroccoli = 5607,
        BowlCauliflower = 5608,
        BowlGreenBeans = 5609,
        BowlRice = 5610,
        BowlSpinach = 5611,
        BowlTurnips = 5612,
        BowlMashedPotatos = 5613,
        BowlCookedVeggies = 5614,
        WoodenBowlCabbage = 5615,
        WoodenBowlCarrot = 5616,
        WoodenBowlCorn = 5617,
        WoodenBowlLettuce = 5618,
        WoodenBowlPea = 5619,
        PewterBowlOfPotatos = 5620,
        CornOnCob = 5621,
        Hotdog = 5622,
        MisoSoup = 5623,
        WhiteMisoSoup =5624,
        RedMisoSoup = 5625,
        AwaseMisoSoup = 5626
        #endregion
        #region Begin Barbecue Recipes 5701 - 5800
        #endregion
        #region Dinners Recipes 5801 - 5900
        #endregion
        #region Begin Chocolatiering Recipes 5901 - 5999
        #endregion
        #endregion
    }
    #endregion

    #region class info
    public class DefCooking : CraftSystem
    {
        public override SkillName MainSkill
        {
            get
            {
                return SkillName.Cooking;
            }
        }

        public override int GumpTitleNumber
        {
            get
            {
                return 1044003;
            }// <CENTER>COOKING MENU</CENTER>
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefCooking();

                return m_CraftSystem;
            }
        }

        public override CraftECA ECA
        {
            get
            {
                return CraftECA.ChanceMinusSixtyToFourtyFive;
            }
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.0; // 0%
        }

        private DefCooking()
            : base(1, 1, 1.25)// base( 1, 1, 1.5 )
        {
        }

        public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
            else if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            return 0;
        }

        public override void PlayCraftEffect(Mobile from)
        {
        }

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

            if (failed)
            {
                if (lostMaterial)
                    return 1044043; // You failed to create the item, and some of your materials are lost.
                else
                    return 1044157; // You failed to create the item, but no materials were lost.
            }
            else
            {
                if (quality == 0)
                    return 502785; // You were barely able to make this item.  It's quality is below average.
                else if (makersMark && quality == 2)
                    return 1044156; // You create an exceptional quality item and affix your maker's mark.
                else if (quality == 2)
                    return 1044155; // You create an exceptional quality item.
                else 
                    return 1044154; // You create the item.
            }
        }
    #endregion

        public override void InitCraftList()
        {
            int index = -1;

            #region Ingredients
            index = AddCraft(typeof(SackFlour), 1044495, 1024153, 0.0, 100.0, typeof(WheatSheaf), 1044489, 20, 1044490);
            AddRecipe(index, (int)CookRecipes.SackFlour);
            SetNeedMill(index, true);

            index = AddCraft(typeof(BagOfSugar), 1044495, "Bag of Sugar", 0.0, 100.0, typeof(Sugarcane), "Sugarcane", 20, "You don't have enough sugarcane.");
            AddRecipe(index, (int)CookRecipes.BagOfSugar);
            SetNeedMill(index, true);

            index = AddCraft(typeof(Dough), 1044495, 1024157, 10.0, 100.0, typeof(SackFlour), 1044468, 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Dough);

            index = AddCraft(typeof(SweetDough), 1044495, 1041340, 10.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(JarHoney), 1044472, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.SweetDough);

            if (Core.ML)
            {
                index = AddCraft(typeof(CocoaButter), 1044495, 1079998, 15.0, 100.0, typeof(CocoaPulp), 1080530, 1, 1044253);
                AddRecipe(index, (int)CookRecipes.CocoaButter);
                SetItemHue(index, 0x457);
                SetNeededExpansion(index, Expansion.ML);
                SetNeedOven(index, true);

                index = AddCraft(typeof(CocoaLiquor), 1044495, 1079999, 15.0, 100.0, typeof(CocoaPulp), 1080530, 1, 1044253);
                AddRes(index, typeof(EmptyPewterBowl), 1025629, 1, 1044253);
                AddRecipe(index, (int)CookRecipes.CocoaLiquor);
                SetItemHue(index, 0x46A);
                SetNeededExpansion(index, Expansion.ML);
                SetNeedOven(index, true);
            }
            #endregion

            #region Oils
            index = AddCraft(typeof(Batter), "Oils", "Batter", 20.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Eggs), "Eggs", 1, 1044253);
            AddRes(index, typeof(JarHoney), 1044472, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Batter);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(Butter), "Oils", "Butter", 20.0, 100.0, typeof(Cream), "Cream", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Butter);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(Cream), "Oils", "Cream", 20.0, 100.0, typeof(BaseBeverage), "Milk", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Cream);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(CookingOil), "Oils", "Cooking Oil", 25.0, 100.0, typeof(Peanut), "Peanut", 10, 1044253);
            AddRecipe(index, (int)CookRecipes.CookingOil);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(Vinegar), "Oils", "Vinegar", 25.0, 100.0, typeof(Apple), "apples", 5, 1044253);
            AddRes(index, typeof(BottleOfWine), "Wine", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Vinegar);
            SetNeedHeat(index, true);
            #endregion

            #region Sauces
            index = AddCraft(typeof(BarbecueSauce), "Sauces", "Barbecue Sauce", 25.0, 100.0, typeof(Tomato), "Tomato", 1, 1044253);
            AddRes(index, typeof(JarHoney), "Honey", 1, 1044253);
            AddRes(index, typeof(BasketOfHerbsFarm), "Herbs", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BarbecueSauce);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(CheeseSauce), "Sauces", "Cheese Sauce", 25.0, 100.0, typeof(Butter), "Butter", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), "Milk", 1, 1044253);
            AddRes(index, typeof(CheeseWheel), "Cheese Wheel", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.CheeseSauce);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(EnchiladaSauce), "Sauces", "Enchilada Sauce", 25.0, 100.0, typeof(Tomato), "Tomato", 1, 1044253);
            AddRes(index, typeof(ChiliPepper), "Chili Pepper", 1, 1044253);
            AddRes(index, typeof(BasketOfHerbsFarm), "Herbs", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.EnchiladaSauce);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(Gravy), "Sauces", "Gravy", 25.0, 100.0, typeof(Dough), 1044469, 2, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRes(index, typeof(BasketOfHerbsFarm), "Herbs", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Gravy);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(HotSauce), "Sauces", "Hot Sauce", 25.0, 100.0, typeof(Tomato), "Tomato", 2, 1044253);
            AddRes(index, typeof(ChiliPepper), "Chili Pepper", 3, 1044253);
            AddRes(index, typeof(BasketOfHerbsFarm), "Herbs", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.HotSauce);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(SoySauce), "Sauces", "Soy Sauce", 25.0, 100.0, typeof(BagOfSoy), "Bag of Soy", 1, 1044253);
            AddRes(index, typeof(BagOfSugar), "Bag of Sugar", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.SoySauce);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(Teriyaki), "Sauces", "Teriyaki", 25.0, 100.0, typeof(SoySauce), "Soy Sauce", 1, 1044253);
            AddRes(index, typeof(BottleOfWine), "Bottle of Wine", 1, 1044253);
            AddRes(index, typeof(JarHoney), "Honey", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Teriyaki);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(TomatoSauce), "Sauces", "Tomato Sauce", 25.0, 100.0, typeof(Tomato), "Tomato", 3, 1044253);
            AddRes(index, typeof(BasketOfHerbsFarm), "Herbs", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.TomatoSauce);
            SetNeedHeat(index, true);
            #endregion

            #region Raw Meat Prep

            #region Game Meats

            #region Deer
            index = AddCraft(typeof(VenisonSteak), "Raw Meat Prep", "Venison Steak", 25.0, 100.0, typeof(RawVenisonSteak), "Raw Venison Steak", 1, "You need more Raw Venison Steak");
            AddRecipe(index, (int)CookRecipes.VenisonSteak);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(VenisonJerky), "Raw Meat Prep", "Venison Jerky", 25.0, 100.0, typeof(RawVenisonSlice), "Raw Venison Slice", 1, "You need more Raw Venison Slice");
            AddRecipe(index, (int)CookRecipes.VenisonJerky);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(VenisonRoast), "Raw Meat Prep", "Venison Roast", 25.0, 100.0, typeof(RawVenisonRoast), "Raw Venison Roast", 1, "You need more Raw Venison Roast");
            AddRecipe(index, (int)CookRecipes.VenisonRoast);
            SetNeedHeat(index, true);
            #endregion

            #endregion

            #region Lean Ground Meats

            #region Beef
            index = AddCraft(typeof(BeefPorterhouse), "Raw Meat Prep", "Beef Porterhouse", 30.0, 100.0, typeof(RawBeefPorterhouse), "Raw Beef Porterhouse", 1, "You need more Raw Beef Porterhouse");
            AddRecipe(index, (int)CookRecipes.BeefPorterhouse);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(BeefPrimeRib), "Raw Meat Prep", "Beef Prime Rib", 30.0, 100.0, typeof(RawBeefPrimeRib), "Raw Beef Prime Rib", 1, "You need more Raw Beef Prime Rib");
            AddRecipe(index, (int)CookRecipes.BeefPrimeRib);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(BeefRibeye), "Raw Meat Prep", "Beef Ribeye", 30.0, 100.0, typeof(RawBeefRibeye), "Raw Beef Ribeye", 1, "You need more Raw Beef Ribeye");
            AddRecipe(index, (int)CookRecipes.BeefRibeye);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(BeefRibs), "Raw Meat Prep", "Beef Ribs", 30.0, 100.0, typeof(RawBeefRibs), "Raw Beef Ribs", 1, "You need more Raw Beef Ribs");
            AddRecipe(index, (int)CookRecipes.BeefRibs);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(BeefRoast), "Raw Meat Prep", "Beef Roast", 30.0, 100.0, typeof(RawBeefRoast), "Raw Beef Roast", 1, "You need more Raw Beef Roast");
            AddRecipe(index, (int)CookRecipes.BeefRoast);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(BeefSirloin), "Raw Meat Prep", "Beef Sirloin", 30.0, 100.0, typeof(RawBeefSirloin), "Raw Beef Sirloin", 1, "You need more Raw Beef Sirloin");
            AddRecipe(index, (int)CookRecipes.BeefSirloin);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(BeefJerky), "Raw Meat Prep", "Beef Jerky", 30.0, 100.0, typeof(RawBeefSlice), "Raw Beef Slice", 1, "You need more Raw Beef Slices");
            AddRecipe(index, (int)CookRecipes.BeefJerky);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(BeefTBone), "Raw Meat Prep", "Beef T-Bone", 30.0, 100.0, typeof(RawBeefTBone), "Raw Beef T-Bone", 1, "You need more Raw Beef T-Bone");
            AddRecipe(index, (int)CookRecipes.BeefTBone);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(BeefTenderloin), "Raw Meat Prep", "Beef Tenderloin", 30.0, 100.0, typeof(RawBeefTenderloin), "Raw Beef Tenderloin", 1, "You need more Raw Raw Beef Tenderloin");
            AddRecipe(index, (int)CookRecipes.BeefTenderloin);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(GroundBeef), 1044496, "Ground Beef", 30.0, 100.0, typeof(BeefHock), "Beef Hock", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.GroundBeef);
            #endregion

            #region Goat
            index = AddCraft(typeof(GoatSteak), "Raw Meat Prep", "Goat Steak", 35.0, 100.0, typeof(RawGoatSteak), "Raw Goat Steak", 1, "You need more Raw Goat Steak");
            AddRecipe(index, (int)CookRecipes.GoatSteak);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(GoatRoast), "Raw Meat Prep", "Goat Roast", 35.0, 100.0, typeof(RawGoatRoast), "Raw Goat Roast", 1, "You need more Raw Goat Roast");
            AddRecipe(index, (int)CookRecipes.GoatRoast);
            SetNeedHeat(index, true);
            #endregion

            #region Pork
            index = AddCraft(typeof(RawGroundPork), "Raw Meat Prep", "Raw Ground Pork", 40.0, 100.0, typeof(PorkHock), "Pork Hock", 1, "You need more Pork Hock");
            AddRecipe(index, (int)CookRecipes.RawGroundPork);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(Bacon), "Raw Meat Prep", "Bacon", 40.0, 100.0, typeof(RawBacon), "Raw Bacon", 1, "You need more Raw Bacon");
            AddRecipe(index, (int)CookRecipes.Bacon);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(BaconSlab), "Raw Meat Prep", "Bacon Slab", 40.0, 100.0, typeof(RawBaconSlab), "Raw Bacon Slab", 1, "You need more Raw Bacon Slab");
            AddRecipe(index, (int)CookRecipes.BaconSlab);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(Ham), "Raw Meat Prep", "Ham", 40.0, 100.0, typeof(RawHam), "Raw Ham", 1, "You need more Raw Ham");
            AddRecipe(index, (int)CookRecipes.Ham);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(HamSlices), "Raw Meat Prep", "Ham Slices", 40.0, 100.0, typeof(RawHamSlices), "Raw Ham Slices", 1, "You need more Raw Ham Slices");
            AddRecipe(index, (int)CookRecipes.HamSlices);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(PigHead), "Raw Meat Prep", "Pig Head", 40.0, 100.0, typeof(RawPigHead), "Raw Pig Head", 1, "You need more Raw Pig Head");
            AddRecipe(index, (int)CookRecipes.PigHead);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(PorkChop), "Raw Meat Prep", "Pork Chop", 40.0, 100.0, typeof(RawPorkChop), "Raw Pork Chop", 1, "You need more Raw Pork Chop");
            AddRecipe(index, (int)CookRecipes.PorkChop);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(PorkRoast), "Raw Meat Prep", "Pork Roast", 40.0, 100.0, typeof(RawPorkRoast), "Raw Pork Roast", 1, "You need more Raw Pork Roast");
            AddRecipe(index, (int)CookRecipes.PorkRoast);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(PorkSpareRibs), "Raw Meat Prep", "Pork Spare Ribs", 40.0, 100.0, typeof(RawSpareRibs), "Raw Spare Ribs", 1, "You need more Raw Spare Ribs");
            AddRecipe(index, (int)CookRecipes.PorkSpareRibs);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(Trotters), "Raw Meat Prep", "Trotters", 40.0, 100.0, typeof(RawTrotters), "Raw Trotters", 1, "You need more Raw Trotters");
            AddRecipe(index, (int)CookRecipes.Trotters);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(Sausage), "Raw Meat Prep", "Sausage", 40.0, 100.0, typeof(RawPorkSlice), "Raw Pork Slice", 1, "You need more Raw Pork Slice");
            AddRecipe(index, (int)CookRecipes.Sausage);
            SetNeedHeat(index, true);
            #endregion

            #region Sheep
            index = AddCraft(typeof(MuttonSteak), "Raw Meat Prep", "Mutton Steak", 40.0, 100.0, typeof(RawMuttonSteak), "Raw Mutton Steak", 1, "You need more Raw Mutton Steak");
            AddRecipe(index, (int)CookRecipes.MuttonSteak);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(MuttonRoast), "Raw Meat Prep", "Mutton Roast", 40.0, 100.0, typeof(RawMuttonRoast), "Raw Mutton Roast", 1, "You need more Raw Mutton Roast");
            AddRecipe(index, (int)CookRecipes.MuttonRoast);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(LambLeg), "Raw Meat Prep", 1025642, 40.0, 100.0, typeof(RawLambLeg), 1044478, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.LambLeg);
            SetNeedHeat(index, true);
            SetUseAllRes(index, true);
            #endregion
            #endregion

            #region Poultry

            #region Chicken
            index = AddCraft(typeof(RoastChicken), "Raw Meat Prep", 1153506, 40.0, 100.0, typeof(RawChicken), "Raw Chicken", 1, "You need more Raw Chicken");
            AddRecipe(index, (int)CookRecipes.RoastChicken);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(ChickenLeg), "Raw Meat Prep", 1025640, 40.0, 100.0, typeof(RawChickenLeg), 1044473, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.ChickenLeg);
            SetNeedHeat(index, true);
            SetUseAllRes(index, true);
            #endregion

            #region Turkey
            index = AddCraft(typeof(RoastTurkey), "Raw Meat Prep", 1153507, 45.0, 100.0, typeof(RawTurkey), "Raw Turkey", 1, "You need more Raw Turkey");
            AddRecipe(index, (int)CookRecipes.RoastTurkey);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(TurkeyLeg), "Raw Meat Prep", 1153508, 45.0, 100.0, typeof(RawTurkeyLeg), "Raw Turkey Leg", 1, "You need more Raw Turkey Leg");
            AddRecipe(index, (int)CookRecipes.TurkeyLeg);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(TurkeyPlatter), "Raw Meat Prep", 1153517, 45.0, 100.0, typeof(RawTurkey), "Raw Chicken", 1, "You need more Raw Turkey");
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            AddRecipe(index, (int)CookRecipes.TurkeyPlatter);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(SlicedTurkey), 1044496, "Sliced Turkey", 45.0, 100.0, typeof(TurkeyHock), "Turkey Hock", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.SlicedTurkey);

            #endregion

            #region Duck
            index = AddCraft(typeof(RoastDuck), "Raw Meat Prep", 1153505, 45.0, 100.0, typeof(RawChicken), "Raw Chicken", 1, "You need more Raw Chicken");
            AddRecipe(index, (int)CookRecipes.RoastDuck);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(DuckLeg), "Raw Meat Prep", "Duck Leg", 45.0, 100.0, typeof(RawDuckLeg), "Raw Duck Leg", 1, "You need more Raw Duck Legs");
            AddRecipe(index, (int)CookRecipes.DuckLeg);
            SetNeedHeat(index, true);
            #endregion

            #region Bird
            index = AddCraft(typeof(CookedBird), "Raw Meat Prep", 1022487, 45.0, 100.0, typeof(RawBird), 1044470, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.CookedBird);
            SetNeedHeat(index, true);
            SetUseAllRes(index, true);
            #endregion

            #endregion

            #region Misc Meats
            index = AddCraft(typeof(Ribs), "Raw Meat Prep", 1022546, 50.0, 100.0, typeof(RawRibs), 1044485, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Ribs);
            SetNeedHeat(index, true);
            SetUseAllRes(index, true);

            index = AddCraft(typeof(CookedSteak), "Raw Meat Prep", "Cooked Steak", 50.0, 100.0, typeof(RawSteak), "Raw Steak", 1, "You need more Raw Steak");
            AddRecipe(index, (int)CookRecipes.CookedSteak);
            SetNeedHeat(index, true);
            #endregion

            #endregion

            #region Preparations

            index = AddCraft(typeof(PastaNoodles), 1044496, "Pasta Noodles", 50.0, 100.0, typeof(SackFlour), "Sack of Flour", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.PastaNoodles);
            AddRes(index, typeof(Eggs), "eggs", 5, 1044253);

            index = AddCraft(typeof(PeanutButter), 1044496, "Peanut Butter", 55.0, 100.0, typeof(Peanut), "Peanuts", 30, 1044253);
            AddRecipe(index, (int)CookRecipes.PeanutButter);

            index = AddCraft(typeof(FruitJam), 1044496, "Fruit Jam", 55.0, 100.0, typeof(FruitBasket), "Fruit Basket", 1, 1044253);
            AddRes(index, typeof(BagOfSugar), "Bag of Sugar", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.FruitJam);
            SetNeedOven(index, true);

            index = AddCraft(typeof(Tortilla), 1044496, "Tortilla", 55.0, 100.0, typeof(BagOfCornmeal), "Bag of Cornmeal", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Tortilla);
            SetNeedOven(index, true);

            index = AddCraft(typeof(WoodPulp), 1044496, 1113136, 60.0, 100.0, typeof(BarkFragment), 1032687, 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.WoodPulp);

            if (Core.SE)
            {
                index = AddCraft(typeof(GreenTea), 1044496, 1030315, 80.0, 120.0, typeof(GreenTeaBasket), 1030316, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
                AddRecipe(index, (int)CookRecipes.GreenTea);
                SetNeededExpansion(index, Expansion.SE);
                SetNeedOven(index, true);

                index = AddCraft(typeof(WasabiClumps), 1044496, 1029451, 70.0, 120.0, typeof(BaseBeverage), 1046458, 1, 1044253);
                AddRes(index, typeof(WoodenBowlOfPeas), 1025633, 3, 1044253);
                AddRecipe(index, (int)CookRecipes.WasabiClumps);
                SetNeededExpansion(index, Expansion.SE);

                index = AddCraft(typeof(SushiRolls), 1044496, 1030303, 90.0, 120.0, typeof(BaseBeverage), 1046458, 1, 1044253);
                AddRes(index, typeof(RawFishSteak), 1044476, 10, 1044253);
                AddRecipe(index, (int)CookRecipes.SushiRolls);
                SetNeededExpansion(index, Expansion.SE);

                index = AddCraft(typeof(SushiPlatter), 1044496, 1030305, 90.0, 120.0, typeof(BaseBeverage), 1046458, 1, 1044253);
                AddRes(index, typeof(RawFishSteak), 1044476, 10, 1044253);
                AddRecipe(index, (int)CookRecipes.SushiPlatter);
                SetNeededExpansion(index, Expansion.SE);
            }

            index = AddCraft(typeof(TribalPaint), 1044496, 1040000, Core.ML ? 55.0 : 80.0, Core.ML ? 105.0 : 80.0, typeof(SackFlour), 1044468, 1, 1044253);
            AddRes(index, typeof(TribalBerry), 1046460, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.TribalPaint);

            if (Core.SE)
            {
                index = AddCraft(typeof(EggBomb), 1044496, 1030249, 90.0, 120.0, typeof(Eggs), 1044477, 1, 1044253);
                AddRes(index, typeof(SackFlour), 1044468, 3, 1044253);
                AddRecipe(index, (int)CookRecipes.EggBomb);
                SetNeededExpansion(index, Expansion.SE);
            }

            index = AddCraft(typeof(DriedOnions), 1044496, "Dried Onions", 60.0, 100.0, typeof(Onion), "Onions", 5, 1044253);
            AddRecipe(index, (int)CookRecipes.DriedOnions);

            index = AddCraft(typeof(DriedHerbs), 1044496, "Dried Herbs", 60.0, 100.0, typeof(Garlic), "Garlic", 2, 1044253);
            AddRes(index, typeof(Ginseng), "Ginseng", 2, 1044253);
            AddRes(index, typeof(TanGinger), "Tan Ginger", 2, "You need more tan ginger");
            AddRecipe(index, (int)CookRecipes.DriedHerbs);

            index = AddCraft(typeof(BasketOfHerbsFarm), 1044496, "Basket of Herbs", 60.0, 100.0, typeof(DriedHerbs), "Dried Herbs", 1, 1044253);
            AddRes(index, typeof(DriedOnions), "Dried Onions", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BasketOfHerbsFarm);

            index = AddCraft(typeof(CakeMix), 1044496, 1041002, 60.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(CookingOil), "Cooking Oil", 1, 1044253);
            AddRes(index, typeof(BagOfSugar), "Bag of Sugar", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.CakeMix);

            index = AddCraft(typeof(CookieMix), 1044496, 1024159, 60.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Butter), "Butter", 1, 1044253);
            AddRes(index, typeof(JarHoney), "Honey", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.CookieMix);

            index = AddCraft(typeof(ChocolateMix), 1044496, "Chocolate Mix", 60.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(BagOfCocoa), "Bag of Cocoa", 1, 1044253);
            AddRes(index, typeof(BagOfSugar), "Bag of Sugar", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.ChocolateMix);

            index = AddCraft(typeof(AsianVegMix), 1044496, "Asian Vegetable Mix", 60.0, 100.0, typeof(Cabbage), "Cabbage", 1, 1044253);
            AddRes(index, typeof(Onion), "Onion", 1, 1044253);
            AddRes(index, typeof(RedMushroom), "Red Mushroom", 1, "You need a Red Mushroom");
            AddRes(index, typeof(Carrot), "Carrot", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.AsianVegMix);

            index = AddCraft(typeof(MixedVegetables), 1044496, "Mixed Vegetables", 60.0, 100.0, typeof(Potato), "Potato", 2, 1044253);
            AddRes(index, typeof(Carrot), "Carrot", 1, 1044253);
            AddRes(index, typeof(Celery), "Celery", 1, 1044253);
            AddRes(index, typeof(Onion), "Onion", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.MixedVegetables);

            index = AddCraft(typeof(PizzaCrust), 1044496, "Pizza Crust", 60.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.PizzaCrust);

            index = AddCraft(typeof(WaffleMix), 1044496, "Waffle Mix", 60.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Eggs), "Eggs", 2, 1044253);
            AddRes(index, typeof(CookingOil), "Cooking Oil", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.WaffleMix);

            index = AddCraft(typeof(BowlCornFlakes), 1044497, "Bowl of Corn Flakes", 60.0, 100.0, typeof(BagOfCornmeal), "Bag of Cornmeal", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BowlCornFlakes);

            index = AddCraft(typeof(BowlRiceKrisps), 1044497, "Bowl of Rice Krisps", 60.0, 100.0, typeof(BagOfRicemeal), "Bag of Ricemeal", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BowlRiceKrisps);

            index = AddCraft(typeof(FruitBasket), 1044497, "Fruit Basket", 60.0, 100.0, typeof(Apple), "Apple", 5, 1044253);
            AddRes(index, typeof(Peach), "Peach", 5, 1044253);
            AddRes(index, typeof(Pear), "Pear", 5, 1044253);
            AddRes(index, typeof(Cherry), "Cherries", 5, 1044253);
            AddRecipe(index, (int)CookRecipes.FruitBasket);

            index = AddCraft(typeof(Tofu), 1044497, "Tofu", 60.0, 100.0, typeof(BagOfSoy), "Bag of Soy", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Tofu);

            if (Core.ML)
            {
                index = AddCraft(typeof(ParrotWafer), 1044496, 1032246, 37.5, 87.5, typeof(Dough), 1044469, 1, 1044253);
                AddRes(index, typeof(JarHoney), 1044472, 1, 1044253);
                AddRes(index, typeof(RawFishSteak), 1044476, 10, 1044253);
                AddRecipe(index, (int)CookRecipes.ParrotWafer);
                SetNeededExpansion(index, Expansion.ML);
            }

            index = AddCraft(typeof(PieMix), 1044496, "Pie Mix", 60.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Butter), "Butter", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.PieMix);

            index = AddCraft(typeof(UnbakedQuiche), 1044496, 1041339, 60.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Eggs), 1044477, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.UnbakedQuiche);

            // TODO: This must also support chicken and lamb legs
            index = AddCraft(typeof(UnbakedMeatPie), 1044496, 1041338, 60.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(RawRibs), 1044482, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.UnbakedMeatPie);

            index = AddCraft(typeof(UncookedSausagePizza), 1044496, 1041337, 60.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Sausage), 1044483, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.UncookedSausagePizza);

            index = AddCraft(typeof(UncookedCheesePizza), 1044496, 1041341, 60.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(CheeseWheel), 1044486, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.UncookedCheesePizza);

            index = AddCraft(typeof(UnbakedFruitPie), 1044496, 1041334, 60.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Pear), 1044481, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.UnbakedFruitPie);

            index = AddCraft(typeof(UnbakedPeachCobbler), 1044496, 1041335, 60.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Peach), 1044480, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.UnbakedPeachCobbler);

            index = AddCraft(typeof(UnbakedApplePie), 1044496, 1041336, 60.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Apple), 1044479, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.UnbakedApplePie);

            index = AddCraft(typeof(UnbakedPumpkinPie), 1044496, 1041342, 60.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRes(index, typeof(Pumpkin), 1044484, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.UnbakedPumpkinPie);
            #endregion

            #region Baking
            index = AddCraft(typeof(BreadLoaf), 1044497, 1024156, 70.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BreadLoaf);
            SetNeedOven(index, true);

            index = AddCraft(typeof(GarlicBread), 1044497, "Garlic Bread", 70.0, 100.0, typeof(BreadLoaf), 1024156, 1, 1044253);
            AddRes(index, typeof(Butter), "Butter", 1, 1044253);
            AddRes(index, typeof(Garlic), "Garlic", 2, 1044253);
            AddRes(index, typeof(BasketOfHerbsFarm), "Herbs", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.GarlicBread);
            SetNeedOven(index, true);

            index = AddCraft(typeof(BananaBread), 1044497, "Banana Bread", 70.0, 100.0, typeof(SweetDough), "Sweet Dough", 1, 1044253);
            AddRes(index, typeof(Banana), "Banana", 6, 1044253);
            AddRecipe(index, (int)CookRecipes.BananaBread);
            SetNeedOven(index, true);

            index = AddCraft(typeof(PumpkinBread), 1044497, "Pumpkin Bread", 70.0, 100.0, typeof(SweetDough), "Sweet Dough", 1, 1044253);
            AddRes(index, typeof(Pumpkin), "Pumpkin", 3, 1044253);
            AddRecipe(index, (int)CookRecipes.PumpkinBread);
            SetNeedOven(index, true);

            index = AddCraft(typeof(CornBread), 1044497, "Corn Bread", 70.0, 100.0, typeof(BagOfCornmeal), "Bag of Cornmeal", 1, 1044253);
            AddRes(index, typeof(Batter), "Batter", 1, 1044253);
            AddRes(index, typeof(BagOfSugar), "Bag of Sugar", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.CornBread);
            SetNeedOven(index, true);

            index = AddCraft(typeof(Cookies), 1044497, 1025643, 70.0, 100.0, typeof(CookieMix), 1044474, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Cookies);
            SetNeedOven(index, true);

            index = AddCraft(typeof(AlmondCookies), 1044497, "Almond Cookies", 70.0, 100.0, typeof(CookieMix), "Cookie Mix", 1, 1044253);
            AddRes(index, typeof(Almond), "Almond", 12, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            AddRecipe(index, (int)CookRecipes.AlmondCookies);
            SetNeedOven(index, true);

            index = AddCraft(typeof(ChocChipCookies), 1044497, "Chocolate Chip Cookies", 70.0, 100.0, typeof(CookieMix), "Cookie Mix", 1, 1044253);
            AddRes(index, typeof(BagOfCocoa), "Bag of Cocoa", 1, "YUou need a bag of cocoa");
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            AddRecipe(index, (int)CookRecipes.ChocChipCookies);
            SetNeedOven(index, true);

            index = AddCraft(typeof(GingerSnaps), 1044497, "Ginger Snaps", 70.0, 100.0, typeof(CookieMix), "Cookie Mix", 1, 1044253);
            AddRes(index, typeof(TanGinger), "Tan Ginger", 12, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            AddRecipe(index, (int)CookRecipes.GingerSnaps);
            SetNeedOven(index, true);

            index = AddCraft(typeof(OatmealCookies), 1044497, "Oatmeal Cookies", 70.0, 100.0, typeof(CookieMix), "Cookie Mix", 1, 1044253);
            AddRes(index, typeof(BagOfOats), "Bag of Oats", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            AddRecipe(index, (int)CookRecipes.OatmealCookies);
            SetNeedOven(index, true);

            index = AddCraft(typeof(PeanutButterCookies), 1044497, "Peanut Butter Cookies", 70.0, 100.0, typeof(CookieMix), "Cookie Mix", 1, 1044253);
            AddRes(index, typeof(PeanutButter), "Peanut Butter", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            AddRecipe(index, (int)CookRecipes.PeanutButterCookies);
            SetNeedOven(index, true);

            index = AddCraft(typeof(PumpkinCookies), 1044497, "Pumpkin Cookies", 70.0, 100.0, typeof(CookieMix), "Cookie Mix", 1, 1044253);
            AddRes(index, typeof(Pumpkin), "Pumpkin", 6, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            AddRecipe(index, (int)CookRecipes.PumpkinCookies);
            SetNeedOven(index, true);

            index = AddCraft(typeof(Cake), 1044497, 1022537, 70.0, 100.0, typeof(CakeMix), 1044471, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Cake);
            SetNeedOven(index, true);

            index = AddCraft(typeof(BananaCake), 1044497, "Banana Cake", 70.0, 100.0, typeof(CakeMix), 1044471, 1, 1044253);
            AddRes(index, typeof(Banana), "Banana", 4, 1044253);
            AddRecipe(index, (int)CookRecipes.BananaCake);
            SetNeedOven(index, true);

            index = AddCraft(typeof(CarrotCake), 1044497, "Carrot Cake", 70.0, 100.0, typeof(CakeMix), 1044471, 1, 1044253);
            AddRes(index, typeof(Carrot), "Carrot", 6, 1044253);
            AddRecipe(index, (int)CookRecipes.CarrotCake);
            SetNeedOven(index, true);

            index = AddCraft(typeof(ChocolateCake), 1044497, "Chocolate Cake", 70.0, 100.0, typeof(CakeMix), 1044471, 1, 1044253);
            AddRes(index, typeof(BagOfCocoa), "Bag of Cocoa", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.ChocolateCake);
            SetNeedOven(index, true);

            index = AddCraft(typeof(CoconutCake), 1044497, "Coconut Cake", 70.0, 100.0, typeof(CakeMix), 1044471, 1, 1044253);
            AddRes(index, typeof(Coconut), "Coconut", 2, 1044253);
            AddRecipe(index, (int)CookRecipes.CoconutCake);
            SetNeedOven(index, true);

            index = AddCraft(typeof(LemonCake), 1044497, "Lemon Cake", 70.0, 100.0, typeof(CakeMix), 1044471, 1, 1044253);
            AddRes(index, typeof(Lemon), "Lemon", 4, 1044253);
            AddRecipe(index, (int)CookRecipes.LemonCake);
            SetNeedOven(index, true);

            index = AddCraft(typeof(Muffins), 1044497, 1022539, 70.0, 100.0, typeof(SweetDough), 1044475, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Muffins);
            SetNeedOven(index, true);

            index = AddCraft(typeof(BlueberryMuffins), 1044497, "Blueberry Muffins", 70.0, 100.0, typeof(SweetDough), "Sweet Dough", 1, 1044253);
            AddRes(index, typeof(Blueberry), "Blueberry", 6, 1044253);
            AddRecipe(index, (int)CookRecipes.BlueberryMuffins);
            SetNeedOven(index, true);

            index = AddCraft(typeof(PumpkinMuffins), 1044497, "Pumpkin Muffins", 70.0, 100.0, typeof(SweetDough), "Sweet Dough", 1, 1044253);
            AddRes(index, typeof(Pumpkin), "Pumpkin", 2, 1044253);
            AddRecipe(index, (int)CookRecipes.PumpkinMuffins);
            SetNeedOven(index, true);

            index = AddCraft(typeof(SausagePizza), 1044497, 1044517, 70.0, 100.0, typeof(UncookedSausagePizza), 1044520, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.SausagePizza);
            SetNeedOven(index, true);

            index = AddCraft(typeof(CheesePizza), 1044497, 1044516, 70.0, 100.0, typeof(UncookedCheesePizza), 1044521, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.CheesePizza);
            SetNeedOven(index, true);

            index = AddCraft(typeof(HamPineapplePizza), 1044497, "Ham and Pineapple Pizza", 70.0, 100.0, typeof(UncookedPizza), "Uncooked Pizza", 1, 1044253);
            AddRes(index, typeof(RawHamSlices), "Raw Ham Slices", 1, 1044253);
            AddRes(index, typeof(Pineapple), "Pineapple", 2, 1044253);
            AddRecipe(index, (int)CookRecipes.HamPineapplePizza);
            SetNeedOven(index, true);

            index = AddCraft(typeof(MushroomOnionPizza), 1044497, "Mushroom and Onion Pizza", 70.0, 100.0, typeof(UncookedPizza), "Uncooked Pizza", 1, 1044253);
            AddRes(index, typeof(TanMushroom), "Tan Mushrooms", 3, 1044253);
            AddRes(index, typeof(Onion), "Onion", 3, 1044253);
            AddRecipe(index, (int)CookRecipes.MushroomOnionPizza);
            SetNeedOven(index, true);

            index = AddCraft(typeof(SausOnionMushPizza), 1044497, "Sausage Onion and Mushroom Pizza", 70.0, 100.0, typeof(UncookedPizza), "Uncooked Pizza", 1, 1044253);
            AddRes(index, typeof(Sausage), "Sausage", 2, 1044253);
            AddRes(index, typeof(Onion), "Onion", 2, 1044253);
            AddRes(index, typeof(RedMushroom), "Red Mushrooms", 2, 1044253);
            AddRecipe(index, (int)CookRecipes.SausOnionMushPizza);
            SetNeedOven(index, true);

            index = AddCraft(typeof(TacoPizza), 1044497, "Taco Pizza", 70.0, 100.0, typeof(UncookedPizza), "Uncooked Pizza", 1, 1044253);
            AddRes(index, typeof(GroundBeef), "Ground Beef", 1, 1044253);
            AddRes(index, typeof(CheeseWheel), "CheeseWheel", 1, 1044253);
            AddRes(index, typeof(EnchiladaSauce), "Enchilada Sauce", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.TacoPizza);
            SetNeedOven(index, true);

            index = AddCraft(typeof(VeggiePizza), 1044497, "Vegetable Pizza", 70.0, 100.0, typeof(UncookedPizza), "Uncooked Pizza", 1, 1044253);
            AddRes(index, typeof(MixedVegetables), "Mixed Vegetables", 1, 1044523);
            AddRecipe(index, (int)CookRecipes.VeggiePizza);
            SetNeedOven(index, true);

            index = AddCraft(typeof(Quiche), 1044497, 1041345, 70.0, 100.0, typeof(UnbakedQuiche), 1044518, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Quiche);
            SetNeedOven(index, true);

            index = AddCraft(typeof(MeatPie), 1044497, 1041347, 70.0, 100.0, typeof(UnbakedMeatPie), 1044519, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.MeatPie);
            SetNeedOven(index, true);

            index = AddCraft(typeof(FruitPie), 1044497, 1041346, 70.0, 100.0, typeof(UnbakedFruitPie), 1044522, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.FruitPie);
            SetNeedOven(index, true);

            index = AddCraft(typeof(PeachCobbler), 1044497, 1041344, 70.0, 100.0, typeof(UnbakedPeachCobbler), 1044523, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.PeachCobbler);
            SetNeedOven(index, true);

            index = AddCraft(typeof(ApplePie), 1044497, 1041343, 70.0, 100.0, typeof(UnbakedApplePie), 1044524, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.ApplePie);
            SetNeedOven(index, true);

            index = AddCraft(typeof(PumpkinPie), 1044497, 1041348, 70.0, 100.0, typeof(UnbakedPumpkinPie), 1046461, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.PumpkinPie);
            SetNeedOven(index, true);

            index = AddCraft(typeof(BlueberryPie), 1044497, "Blueberry Pie", 70.0, 100.0, typeof(PieMix), "Pie Mix", 1, 1044253);
            AddRes(index, typeof(Blueberry), "Blueberry", 8, 1044253);
            AddRecipe(index, (int)CookRecipes.BlueberryPie);
            SetNeedOven(index, true);

            index = AddCraft(typeof(CherryPie), 1044497, "Cherry Pie", 70.0, 100.0, typeof(PieMix), "Pie Mix", 1, 1044253);
            AddRes(index, typeof(Cherry), "Cherry", 8, 1044253);
            AddRecipe(index, (int)CookRecipes.CherryPie);
            SetNeedOven(index, true);

            index = AddCraft(typeof(KeyLimePie), 1044497, "Key Lime Pie", 70.0, 100.0, typeof(PieMix), "Pie Mix", 1, 1044253);
            AddRes(index, typeof(Lime), "Lime", 12, 1044253);
            AddRecipe(index, (int)CookRecipes.KeyLimePie);
            SetNeedOven(index, true);

            index = AddCraft(typeof(LemonMerenguePie), 1044497, "Lemon Merengue Pie", 70.0, 100.0, typeof(PieMix), "Pie Mix", 1, 1044253);
            AddRes(index, typeof(Lemon), "Lemon", 12, 1044253);
            AddRes(index, typeof(Cream), "Cream", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.LemonMerenguePie);
            SetNeedOven(index, true);

            index = AddCraft(typeof(BlackberryCobbler), 1044497, "Blackberry Cobbler", 70.0, 100.0, typeof(PieMix), "Pie Mix", 1, 1044253);
            AddRes(index, typeof(Blackberry), "Blackberry", 10, 1044253);
            AddRes(index, typeof(JarHoney), "Honey", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BlackberryCobbler);
            SetNeedOven(index, true);

            index = AddCraft(typeof(ShepherdsPie), 1044497, "Shepherds Pie", 70.0, 100.0, typeof(PieMix), "Pie Mix", 1, 1044253);
            AddRes(index, typeof(GroundBeef), "Ground Beef", 1, 1044253);
            AddRes(index, typeof(BowlMashedPotatos), "Bowl of Mashed Potatos", 1, 1044253);
            AddRes(index, typeof(Corn), "Corn", 2, 1044253);
            AddRecipe(index, (int)CookRecipes.ShepherdsPie);
            SetNeedOven(index, true);

            index = AddCraft(typeof(TurkeyPie), 1044497, "Turkey Pie", 70.0, 100.0, typeof(PieMix), "Pie Mix", 1, 1044253);
            AddRes(index, typeof(SlicedTurkey), "Sliced Turkey", 2, 1044253);
            AddRes(index, typeof(MixedVegetables), "Mixed Vegetables", 1, 1044253);
            AddRes(index, typeof(Gravy), "Gravy", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.TurkeyPie);
            SetNeedOven(index, true);

            index = AddCraft(typeof(ChickenPie), 1044497, "Chicken Pie", 70.0, 100.0, typeof(RawChicken), "Raw Chicken", 1, 1044253);
            AddRes(index, typeof(PieMix), "Pie Mix", 1, 1044253);
            AddRes(index, typeof(MixedVegetables), "Mixed Vegetables", 1, 1044253);
            AddRes(index, typeof(Gravy), "Gravy", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.ChickenPie);
            SetNeedOven(index, true);

            index = AddCraft(typeof(BeefPie), 1044497, "Beef Pie", 70.0, 100.0, typeof(GroundBeef), "Ground Beef", 1, 1044253);
            AddRes(index, typeof(PieMix), "Pie Mix", 1, 1044253);
            AddRes(index, typeof(MixedVegetables), "Mixed Vegetables", 1, 1044253);
            AddRes(index, typeof(Gravy), "Gravy", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BeefPie);
            SetNeedOven(index, true);

            index = AddCraft(typeof(Brownies), 1044497, "Brownies", 70.0, 100.0, typeof(ChocolateMix), "Chocolate Mix", 1, 1044253);
            AddRes(index, typeof(Eggs), "Eggs", 2, 1044253);
            AddRes(index, typeof(CookingOil), "Cooking Oil", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Brownies);
            SetNeedOven(index, true);

            index = AddCraft(typeof(ChocSunflowerSeeds), 1044497, "Chocolate Sunflower Seeds", 70.0, 100.0, typeof(EdibleSun), "Sunflower Seeds", 1, 1044253);
            AddRes(index, typeof(BagOfCocoa), "Bag of Cocoa", 1, "you need a bag oc cocoa");
            AddRecipe(index, (int)CookRecipes.ChocSunflowerSeeds);
            SetNeedOven(index, true);

            index = AddCraft(typeof(RiceKrispTreat), 1044497, "RiceKrispTreat", 70.0, 100.0, typeof(BowlRiceKrisps), "Bowl Of Rice Krips", 1, 1044253);
            AddRes(index, typeof(Butter), "Butter", 1, 1044253);
            AddRes(index, typeof(BagOfSugar), "Bag of Sugar", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.RiceKrispTreat);
            SetNeedOven(index, true);

            index = AddCraft(typeof(BowlOatmeal), 1044497, "Bowl of Oatmeal", 70.0, 100.0, typeof(BagOfOats), "Bag of Oats", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRes(index, typeof(JarHoney), "Honey", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BowlOatmeal);
            SetNeedOven(index, true);

            index = AddCraft(typeof(Popcorn), 1044497, "Popcorn", 70.0, 100.0, typeof(Corn), "Corn", 2, 1044253);
            AddRes(index, typeof(CookingOil), "Cooking Oil", 1, 1044253);
            AddRes(index, typeof(Butter), "Butter", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Popcorn);
            SetNeedOven(index, true);

            index = AddCraft(typeof(Pancakes), 1044497, "Pancakes", 70.0, 100.0, typeof(Batter), "Batter", 1, 1044253);
            AddRes(index, typeof(JarHoney), "Honey", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Pancakes);
            SetNeedOven(index, true);

            index = AddCraft(typeof(Waffles), 1044497, "Waffles", 70.0, 100.0, typeof(WaffleMix), "Waffle Mix", 1, 1044253);
            AddRes(index, typeof(JarHoney), "Honey", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Waffles);
            SetNeedOven(index, true);
            #endregion

            #region Boiling
            index = AddCraft(typeof(ChickenNoodleSoup), "Boiling", "Chicken Noodle Soup", 80.0, 100.0, typeof(RoastChicken), 1153506, 1, 1044253);
            AddRes(index, typeof(PastaNoodles), "Pasta Noodles", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.ChickenNoodleSoup);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(TomatoRice), "Boiling", "Tomato and Rice", 80.0, 100.0, typeof(Tomato), "Tomato", 3, 1044253);
            AddRes(index, typeof(BowlRice), "Bowl of Rice", 1, 1044253);
            AddRes(index, typeof(BasketOfHerbsFarm), "Herbs", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.TomatoRice);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(BowlOfStew), "Boiling", "Beef Stew", 80.0, 100.0, typeof(GroundBeef), "Ground Beef", 1, 1044253);
            AddRes(index, typeof(Gravy), "Gravy", 1, 1044253);
            AddRes(index, typeof(BowlCookedVeggies), "Cooked Bowl of Vegetables", 1, 1044253);
            AddRes(index, typeof(BasketOfHerbsFarm), "Herbs", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BowlOfStew);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(TomatoSoup), "Boiling", "Tomato Soup", 80.0, 100.0, typeof(Tomato), "Tomato", 5, 1044253);
            AddRes(index, typeof(BasketOfHerbsFarm), "Herbs", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.TomatoSoup);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(HarpyEggSoup), "Boiling", "Harpy Egg Soup", 80.0, 100.0, typeof(HarpyEggs), "Harpy Eggs", 5, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.HarpyEggSoup);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(BowlBeets), "Boiling", "Bowl of Beets", 80.0, 100.0, typeof(Beet), "Beet", 4, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BowlBeets);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(BowlBroccoli), "Boiling", "Bowl of Broccoli", 80.0, 100.0, typeof(Broccoli), "Broccoli", 4, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BowlBroccoli);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(BowlCauliflower), "Boiling", "Bowl of Cauliflower", 80.0, 100.0, typeof(Cauliflower), "Cauliflower", 4, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BowlCauliflower);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(BowlGreenBeans), "Boiling", "Bowl of Green Beans", 80.0, 100.0, typeof(GreenBean), "Green Beans", 20, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRes(index, typeof(Bacon), "Bacon", 3, 1044253);
            AddRecipe(index, (int)CookRecipes.BowlGreenBeans);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(BowlRice), "Boiling", "Bowl of Rice", 80.0, 100.0, typeof(BagOfRicemeal), "Bag of Rice", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BowlRice);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(BowlSpinach), "Boiling", "Bowl of Spinach", 80.0, 100.0, typeof(Spinach), "Spinach", 8, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRes(index, typeof(Vinegar), "Vinegar", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BowlSpinach);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(BowlTurnips), "Boiling", "Bowl of Turnips", 80.0, 100.0, typeof(Turnip), "Turnip", 4, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BowlTurnips);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(BowlMashedPotatos), "Boiling", "Bowl of Mashed Potatos", 80.0, 100.0, typeof(Potato), "Potato", 5, 1044253);
            AddRes(index, typeof(Butter), "Butter", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), "Milk", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BowlMashedPotatos);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(BowlCookedVeggies), "Boiling", "Cooked Bowl of Vegetables", 80.0, 100.0, typeof(MixedVegetables), "Mixed Vegetables", 1, 1044253);
            AddRes(index, typeof(SoySauce), "Soy Sauce", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.BowlCookedVeggies);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(WoodenBowlCabbage), "Boiling", "Bowl of Cabbage", 80.0, 100.0, typeof(Cabbage), "Cabbage", 2, 1044253);
            AddRes(index, typeof(Vinegar), "Vinegar", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.WoodenBowlCabbage);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(WoodenBowlCarrot), "Boiling", "Bowl of Carrots", 80.0, 100.0, typeof(Carrot), "Carrot", 12, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.WoodenBowlCarrot);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(WoodenBowlCorn), "Boiling", "Bowl of Corn", 80.0, 100.0, typeof(Corn), "Corn", 3, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.WoodenBowlCorn);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(WoodenBowlLettuce), "Boiling", "Bowl of Lettuce", 80.0, 100.0, typeof(Lettuce), "Lettuce", 2, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.WoodenBowlLettuce);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(WoodenBowlPea), "Boiling", "Bowl of Peas", 80.0, 100.0, typeof(Peas), "Peas", 20, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.WoodenBowlPea);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(PewterBowlOfPotatos), "Boiling", "Bowl of Potatos", 80.0, 100.0, typeof(Potato), "Potato", 5, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.PewterBowlOfPotatos);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(CornOnCob), "Boiling", "Corn on the Cob", 80.0, 100.0, typeof(Corn), "Corn", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRecipe(index, (int)CookRecipes.CornOnCob);
            SetNeedCauldron(index, true);

            index = AddCraft(typeof(Hotdog), "Boiling", "Hotdog", 80.0, 100.0, typeof(GroundPork), "Ground Pork", 1, 1044253);
            AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
            AddRes(index, typeof(BreadLoaf), "Bread", 1, 1044253);
            AddRecipe(index, (int)CookRecipes.Hotdog);
            SetNeedCauldron(index, true);

            if (Core.SE)
            {
                index = AddCraft(typeof(MisoSoup), "Boiling", 1030317, 80.0, 110.0, typeof(RawFishSteak), 1044476, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
                AddRecipe(index, (int)CookRecipes.MisoSoup);
                SetNeededExpansion(index, Expansion.SE);
                SetNeedCauldron(index, true);

                index = AddCraft(typeof(WhiteMisoSoup), "Boiling", 1030318, 80.0, 110.0, typeof(RawFishSteak), 1044476, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
                AddRecipe(index, (int)CookRecipes.WhiteMisoSoup);
                SetNeededExpansion(index, Expansion.SE);
                SetNeedCauldron(index, true);

                index = AddCraft(typeof(RedMisoSoup), "Boiling", 1030319, 80.0, 110.0, typeof(RawFishSteak), 1044476, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
                AddRecipe(index, (int)CookRecipes.RedMisoSoup);
                SetNeededExpansion(index, Expansion.SE);
                SetNeedCauldron(index, true);

                index = AddCraft(typeof(AwaseMisoSoup), "Boiling", 1030320, 80.0, 110.0, typeof(RawFishSteak), 1044476, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1046458, 1, 1044253);
                AddRecipe(index, (int)CookRecipes.AwaseMisoSoup);
                SetNeededExpansion(index, Expansion.SE);
                SetNeedCauldron(index, true);
            }
            #endregion

            #region Begin Barbecue
            index = AddCraft(typeof(Hotwings), 1044498, "Hotwings", 90.0, 100.0, typeof(RawChickenLeg), "Raw Chicken Leg", 1, 1044253);
            AddRes(index, typeof(JarHoney), "Honey", 1, 1044253);
            AddRes(index, typeof(HotSauce), "Hot Sauce", 1, 1044253);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(PotatoFries), 1044498, "Potato Fries", 90.0, 100.0, typeof(Potato), "Potato", 3, 1044253);
            AddRes(index, typeof(Onion), "Onion", 1, 1044253);
            AddRes(index, typeof(Butter), "Butter", 1, 1044253);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(Taco), 1044498, "Taco", 90.0, 100.0, typeof(GroundBeef), "Ground Beef", 1, 1044253);
            AddRes(index, typeof(Tortilla), "Tortilla", 1, 1044253);
            AddRes(index, typeof(CheeseWheel), "Cheese Wheel", 1, 1044253);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(BroccoliCheese), 1044498, "Broccoli and Cheese", 90.0, 100.0, typeof(Broccoli), "Broccoli", 5, 1044253);
            AddRes(index, typeof(CheeseSauce), "Cheese Sauce", 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(BroccoliCaulCheese), 1044498, "Broccoli, Cauliflower and Cheese", 90.0, 100.0, typeof(Broccoli), "Broccoli", 5, 1044253);
            AddRes(index, typeof(Cauliflower), "Cauliflower", 2, 1044253);
            AddRes(index, typeof(CheeseSauce), "Cheese Sauce", 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(CauliflowerCheese), 1044498, "Cauliflower and Cheese", 90.0, 100.0, typeof(Cauliflower), "Cauliflower", 5, 1044253);
            AddRes(index, typeof(CheeseSauce), "Cheese Sauce", 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(Meatballs), 1044498, "Meatballs", 90.0, 100.0, typeof(GroundBeef), "Ground Beef", 1, 1044253);
            AddRes(index, typeof(BreadLoaf), "Bread", 1, 1044253);
            AddRes(index, typeof(Eggs), "Eggs", 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(Meatloaf), 1044498, "Meatloaf", 90.0, 100.0, typeof(GroundBeef), "Ground Beef", 2, 1044253);
            AddRes(index, typeof(Eggs), "Eggs", 2, 1044253);
            AddRes(index, typeof(Onion), "Onion", 2, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(PotatoStrings), "Food", "Potato Strings", 90.0, 100.0, typeof(Potato), "Potato", 1, 1044253);
            SetNeedOven(index, true);

            index = AddCraft(typeof(FriedEggs), 1044498, 1022486, 90.0, 100.0, typeof(Eggs), 1044477, 1, 1044253);
            SetNeedHeat(index, true);
            SetUseAllRes(index, true);

            index = AddCraft(typeof(FishSteak), 1044498, 1022427, 30.0, 100.0, typeof(RawFishSteak), 1044476, 1, 1044253);
            SetNeedHeat(index, true);

            index = AddCraft(typeof(HalibutFishSteak), 1044498, "Halibut Fish Steak", 50.0, 120.0, typeof(RawHalibutSteak), "Raw Halibut Fish Steak", 1, "you need more Raw Halibut Fish Steaks");
            SetNeedHeat(index, true);

            index = AddCraft(typeof(FlukeFishSteak), 1044498, "Fluke Fish Steak", 50.0, 120.0, typeof(RawFlukeSteak), "Raw Fluke Fish Steak", 1, "you need more Raw Fluke Fish Steaks");
            SetNeedHeat(index, true);

            index = AddCraft(typeof(MahiFishSteak), 1044498, "Mahi Fish Steak", 50.0, 120.0, typeof(RawMahiSteak), "Raw Mahi Fish Steak", 1, "you need more Raw Mahi Fish Steaks");
            SetNeedHeat(index, true);

            index = AddCraft(typeof(SalmonFishSteak), 1044498, "Salmon Fish Steak", 50.0, 120.0, typeof(RawSalmonSteak), "Raw Salmon Fish Steak", 1, "you need more Raw Salmon Fish Steaks");
            SetNeedHeat(index, true);

            index = AddCraft(typeof(RedSnapperFishSteak), 1044498, "Red Snapper Fish Steak", 50.0, 120.0, typeof(RawRedSnapperSteak), "Raw Red Snapper Fish Steak", 1, "you need more Raw Red Snapper Fish Steaks");
            SetNeedHeat(index, true);

            index = AddCraft(typeof(ParrotFishSteak), 1044498, "Parrot Fish Steak", 50.0, 120.0, typeof(RawParrotFishSteak), "Raw Parrot Fish Steak", 1, "you need more Raw Parrot Fish Steaks");
            SetNeedHeat(index, true);

            index = AddCraft(typeof(TroutFishSteak), 1044498, "Trout Fish Steak", 50.0, 120.0, typeof(RawTroutSteak), "Raw Trout Fish Steak", 1, "you need more Raw Trout Fish Steaks");
            SetNeedHeat(index, true);

            index = AddCraft(typeof(CookedShrimp), 1044498, "Cooked Shrimp", 50.0, 120.0, typeof(RawShrimp), "Raw Shrimp", 1, "you need more Raw Shrimp");
            SetNeedHeat(index, true);

            #endregion

            #region Dinners
            index = AddCraft(typeof(ChickenParmesian), "Dinners", "Chicken Parmesian", 100.0, 100.0, typeof(RawBird), "Raw Bird", 1, 1044253);
            AddRes(index, typeof(TomatoSauce), "Tomato Sauce", 1, 1044253);
            AddRes(index, typeof(CheeseWheel), "Cheese Wheel", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(CheeseEnchilada), "Dinners", "Cheese Enchilada", 100.0, 100.0, typeof(CheeseWheel), "Cheese Wheel", 1, 1044253);
            AddRes(index, typeof(Tortilla), "Tortilla", 1, 1044253);
            AddRes(index, typeof(EnchiladaSauce), "Enchilada Sauce", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(ChickenEnchilada), "Dinners", "Chicken Enchilada", 100.0, 100.0, typeof(RawBird), "Raw Bird", 1, 1044253);
            AddRes(index, typeof(Tortilla), "Tortilla", 1, 1044253);
            AddRes(index, typeof(EnchiladaSauce), "Enchilada Sauce", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(Lasagna), "Dinners", "Lasagna", 100.0, 100.0, typeof(PastaNoodles), "Pasta Noodles", 3, 1044253);
            AddRes(index, typeof(GroundBeef), "Ground Beef", 1, 1044253);
            AddRes(index, typeof(CheeseWheel), "Cheese Wheel", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(LemonChicken), "Dinners", "Lemon Chicken", 100.0, 100.0, typeof(RawBird), "Raw Bird", 1, 1044253);
            AddRes(index, typeof(Lemon), "Lemon", 1, 1044253);
            AddRes(index, typeof(BasketOfHerbsFarm), "Herbs", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(OrangeChicken), "Dinners", "Orange Chicken", 100.0, 100.0, typeof(RawBird), "Raw Bird", 1, 1044253);
            AddRes(index, typeof(Orange), "Orange", 1, 1044253);
            AddRes(index, typeof(BasketOfHerbsFarm), "Herbs", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(VealParmesian), "Dinners", "Veal Parmesian", 100.0, 100.0, typeof(RawLambLeg), "Raw Lamb Leg", 2, 1044253);
            AddRes(index, typeof(TomatoSauce), "Tomato Sauce", 1, 1044253);
            AddRes(index, typeof(CheeseWheel), "Cheese Wheel", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(BeefBBQRibs), "Dinners", "Beef Barbecue Ribs", 100.0, 100.0, typeof(RawRibs), "Raw Ribs", 1, 1044253);
            AddRes(index, typeof(BarbecueSauce), "Barbecue Sauce", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(BeefBroccoli), "Dinners", "Beef and Broccoli", 100.0, 100.0, typeof(GroundBeef), "Ground Beef", 1, 1044253);
            AddRes(index, typeof(Broccoli), "Broccoli", 4, 1044253);
            AddRes(index, typeof(SoySauce), "Soy Sauce", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(ChoChoBeef), "Dinners", "Cho Cho Beef", 100.0, 100.0, typeof(GroundBeef), "Ground Beef", 1, 1044253);
            AddRes(index, typeof(Teriyaki), "Teriyaki", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(BeefSnowpeas), "Dinners", "Beef and Snow Peas", 100.0, 100.0, typeof(GroundBeef), "Ground Beef", 1, 1044253);
            AddRes(index, typeof(SnowPeas), "Snow Peas", 4, 1044253);
            AddRes(index, typeof(SoySauce), "Soy Sauce", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(Hamburger), "Dinners", "Hamburger", 100.0, 100.0, typeof(GroundBeef), "Ground Beef", 1, 1044253);
            AddRes(index, typeof(BreadLoaf), "Bread", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(BeefLoMein), "Dinners", "Beef Lo Mein", 100.0, 100.0, typeof(GroundBeef), "Ground Beef", 1, 1044253);
            AddRes(index, typeof(BowlCookedVeggies), "Cooked Mixed Vegetables", 1, 1044253);
            AddRes(index, typeof(PastaNoodles), "Pasta Noodles", 2, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(BeefStirfry), "Dinners", "Beef Stirfry", 100.0, 100.0, typeof(GroundBeef), "Ground Beef", 1, 1044253);
            AddRes(index, typeof(BowlCookedVeggies), "Cooked Mixed Vegetables", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(ChickenStirfry), "Dinners", "Chicken Stirfry", 100.0, 100.0, typeof(RawBird), "Raw Bird", 1, 1044253);
            AddRes(index, typeof(BowlCookedVeggies), "Cooked Mixed Vegetables", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(MooShuPork), "Dinners", "Moo Shu Pork", 100.0, 100.0, typeof(GroundPork), "Ground Pork", 1, 1044253);
            AddRes(index, typeof(BowlCookedVeggies), "Cooked Mixed Vegetables", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(MoPoTofu), "Dinners", "Mo Po Tofu", 100.0, 100.0, typeof(Tofu), "Tofu", 1, 1044253);
            AddRes(index, typeof(BowlCookedVeggies), "Cooked Mixed Vegetables", 1, 1044253);
            AddRes(index, typeof(ChiliPepper), "Chili Pepper", 3, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(PorkStirfry), "Dinners", "Pork Stirfry", 100.0, 100.0, typeof(GroundPork), "Ground Pork", 1, 1044253);
            AddRes(index, typeof(BowlCookedVeggies), "Cooked Mixed Vegetables", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(SweetSourChicken), "Dinners", "Sweet and Sour Chicken", 100.0, 100.0, typeof(RawBird), "Raw Bird", 1, 1044253);
            AddRes(index, typeof(JarHoney), "Honey", 1, 1044253);
            AddRes(index, typeof(SoySauce), "SoySauce", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(SweetSourPork), "Dinners", "Sweet and Sour Pork", 100.0, 100.0, typeof(GroundPork), "Ground Pork", 1, 1044253);
            AddRes(index, typeof(JarHoney), "Honey", 1, 1044253);
            AddRes(index, typeof(SoySauce), "SoySauce", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(BaconAndEgg), "Dinners", "Bacon and Eggs", 100.0, 100.0, typeof(Eggs), "Eggs", 2, 1044253);
            AddRes(index, typeof(RawBacon), "Raw Bacon", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(Spaghetti), "Dinners", "Spaghetti", 100.0, 100.0, typeof(PastaNoodles), "Pasta Noodles", 3, 1044253);
            AddRes(index, typeof(TomatoSauce), "Tomato Sauce", 1, 1044253);
            AddRes(index, typeof(GroundBeef), "Ground Beef", 1, 1044253);
            AddRes(index, typeof(FoodPlate), "Plate", 1, "You need a plate!");
            SetNeedOven(index, true);

            index = AddCraft(typeof(MacaroniCheese), "Dinners", "Macaroni and Cheese", 100.0, 100.0, typeof(PastaNoodles), "Pasta Noodles", 3, 1044253);
            AddRes(index, typeof(CheeseSauce), "Cheese Sauce", 1, 1044253);
            SetNeedOven(index, true);
            #endregion

            #region Begin Chocolatiering
            if (Core.ML)
            {
                index = AddCraft(typeof(DarkChocolate), 1080001, 1079994, 15.0, 100.0, typeof(SackOfSugar), 1079997, 1, 1044253);
                AddRes(index, typeof(CocoaButter), 1079998, 1, 1044253);
                AddRes(index, typeof(CocoaLiquor), 1079999, 1, 1044253);
                SetItemHue(index, 0x465);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(MilkChocolate), 1080001, 1079995, 32.5, 107.5, typeof(SackOfSugar), 1079997, 1, 1044253);
                AddRes(index, typeof(CocoaButter), 1079998, 1, 1044253);
                AddRes(index, typeof(CocoaLiquor), 1079999, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1022544, 1, 1044253);
                SetBeverageType(index, BeverageType.Milk);
                SetItemHue(index, 0x461);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(WhiteChocolate), 1080001, 1079996, 52.5, 127.5, typeof(SackOfSugar), 1079997, 1, 1044253);
                AddRes(index, typeof(CocoaButter), 1079998, 1, 1044253);
                AddRes(index, typeof(Vanilla), 1080000, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1022544, 1, 1044253);
                SetBeverageType(index, BeverageType.Milk);
                SetItemHue(index, 0x47E);
                SetNeededExpansion(index, Expansion.ML);
            }
            #endregion/* End Chocolatiering */

            #region Mondain's Legacy /* Begin Enchanted */
            if (Core.ML)
            {
                index = AddCraft(typeof(FoodEngraver), 1073108, 1072951, 75.0, 100.0, typeof(Dough), 1044469, 1, 1044253);
                AddRes(index, typeof(JarHoney), 1044472, 1, 1044253);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(EnchantedApple), 1073108, 1072952, 60.0, 110.0, typeof(Apple), 1044479, 1, 1044253);
                AddRes(index, typeof(GreaterHealPotion), 1073467, 1, 1044253);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(WrathGrapes), 1073108, 1072953, 95.0, 145.0, typeof(Grapes), 1073468, 1, 1044253);
                AddRes(index, typeof(GreaterStrengthPotion), 1073466, 1, 1044253);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(FruitBowl), 1073108, 1072950, 55.0, 105.0, typeof(EmptyWoodenBowl), 1073472, 1, 1044253);
                AddRes(index, typeof(Pear), 1044481, 3, 1044253);
                AddRes(index, typeof(Apple), 1044479, 3, 1044253);
                AddRes(index, typeof(Banana), 1073470, 3, 1044253);
                SetNeededExpansion(index, Expansion.ML);
            }
            /* End Enchanted */
            #endregion

            #region Stygian Abyss /* Plant Pigments*/
            if (Core.SA)
            {
                index = AddCraft(typeof(PlantPigment), "Plant Pigments", 1112132, 33.0, 83.0, typeof(PlantClippings), 1112131, 1, 1044253);
                AddRes(index, typeof(Bottle), 1023854, 1, 1044253);
                SetNeededExpansion(index, Expansion.SA);

                index = AddCraft(typeof(NaturalDye), "Plant Pigments", 1112136, 75.0, 100.0, typeof(PlantPigment), 1112132, 1, 1044253);
                AddRes(index, typeof(ColorFixative), 1112135, 1, 1044253);
                SetNeededExpansion(index, Expansion.SA);

                index = AddCraft(typeof(ColorFixative), "Plant Pigments", 1112135, 75.0, 100.0, typeof(SilverSerpentVenom), 1112173, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1044476, 1, 1044253);//TODO correct Consumption of BaseBeverage...
                SetNeededExpansion(index, Expansion.SA);
            }
            #endregion

            #region High Seas
            if (Core.HS)
            {
                // Fish Oil Flask ( Oléo de peixe )
                index = AddCraft(typeof(FishOil), 1044496, 1150863, 60.0, 120.0, typeof(BaseBeverage), 1046458, 1, 1044253);
                AddRes(index, typeof(RawFishSteak), 1044476, 8, 1044253);
                AddRes(index, typeof(SackFlour), 1044468, 1, 1044253);
                SetNeededExpansion(index, Expansion.HS);
            }
            #endregion
        }
    }
}