/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Commands;
using Server.Mobiles;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Multis;

namespace Server.LokaiSkillHandlers
{
    public class Pilfering
    {
		public static void Initialize()
		{
            LokaiSkillInfo.Table[(int)LokaiSkillName.Pilfering].Callback = new LokaiSkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.Target = new InternalTarget();
            m.SendMessage("Select target.");
            return TimeSpan.FromSeconds(5.0);
        }

        private class InternalTarget : Target
        {
            private bool m_SetSkillTime = true;

            public InternalTarget()
                : base(12, false, TargetFlags.None)
            {
            }

            protected override void OnTargetFinish(Mobile from)
            {
                if (m_SetSkillTime)
                    from.NextSkillTime = Core.TickCount;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is StaticTarget)
                {
                    StaticTarget target = targeted as StaticTarget;
                    if (target.Location.CompareTo(from.Location) > 2)
                    {
                        from.SendMessage("You are too far away to do that.");
                        return;
                    }
                    PilferFlags flags = PilferTarget(target, from);

                    if (flags != PilferFlags.None)
                    {
                        if (Core.Debug)
                            from.SendMessage("TEST: pilfering target: {0}.", flags.ToString());
                        List<PilferFlags> list = new List<PilferFlags>();
                        foreach (PilferFlags flag in Enum.GetValues(typeof(PilferFlags)))
                        {
                            if (flag != PilferFlags.None && GetFlag(flag, flags))
                                list.Add(flag);
                        }

                        if (list.Count > 0)
                        {
                            PilferFlags pilfer = PilferFlags.None;
                            if (list.Count > 1)
                                pilfer = list[Utility.Random(list.Count)];
                            else
                                pilfer = list[0];

                            ///TEST FOR SUCCESSRATING
                            /// ---------------------

                            LokaiSkill lokaiSkill = (LokaiSkillUtilities.XMLGetSkills(from)).Pilfering;
                            SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, lokaiSkill, 0.0, 100.0);

                            ///IF SUCCESSFUL
                            /// ------------

                            if (rating >= SuccessRating.PartialSuccess)
                            {
                                CreateItem(pilfer, rating, from);
                            }
                            else
                            {
                                from.SendMessage("You fail to pilfer anything.");
                            }
                        }
                        else
                            from.SendMessage("There is nothing to pilfer there.");
                    }
                    else
                        from.SendMessage("There is nothing to pilfer there.");
                }
            }
        }

        private static void CreateItem(PilferFlags pilfer, SuccessRating rating, Mobile from)
        {
            switch (pilfer)
            {
                case PilferFlags.ArcheryWeapon:
                    {
                        Item item = Loot.RandomRangedWeapon();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Armor:
                    {
                        Item item = Loot.RandomArmorOrHat();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Clothes:
                    {
                        Item item = Loot.RandomClothing(from.Map == Map.Tokuno, true);
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Food:
                    {
                        Item item = RandomFood();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Jewel:
                    {
                        Item item = Loot.RandomJewelry();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.MetalWeapon:
                    {
                        Item item = Loot.RandomWeapon();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Potion:
                    {
                        Item item = Loot.RandomPotion();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Reagent:
                    {
                        Item item = Loot.RandomPossibleReagent();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Scroll:
                    {
                        int max = 0;
                        SpellbookType book = (SpellbookType)Utility.Random(2);
                        if (book == SpellbookType.Regular)
                        {
                            max = Loot.RegularScrollTypes.Length;
                            switch (rating)
                            {
                                case SuccessRating.PartialSuccess: { max /= 8; break; }
                                case SuccessRating.Success: { max /= 4; break; }
                                case SuccessRating.CompleteSuccess: { max /= 2; break; }
                                case SuccessRating.ExceptionalSuccess: { break; }
                            }
                        }
                        if (book == SpellbookType.Necromancer) max = Loot.SENecromancyScrollTypes.Length;
                        Item item = Loot.RandomScroll(0, max, book);
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Spellbook:
                    {
                        Item item = RandomSpellbook(rating);
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Wand:
                    {
                        BaseWand item = Loot.RandomWand();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.Effect.ToString()) ? "an" : "a", item.Effect.ToString());
                        break;
                    }
            }
        }

        private static bool StartsWithVowel(string text)
        {
            if (text == null || text == "") return false;
            return (text.StartsWith("a") || text.StartsWith("e") || text.StartsWith("i") || text.StartsWith("o") || text.StartsWith("u"));
        }

        private static Item RandomFood()
        {
            Type[] types = new Type[]{
                typeof(BreadLoaf), typeof(FrenchBread), typeof(Cake), typeof(Cookies),
                typeof(Muffins), typeof(CheesePizza), typeof(ApplePie), typeof(PeachCobbler),
                typeof(Quiche), typeof(Dough), typeof(JarHoney), typeof(Pitcher),
                typeof(SackFlour), typeof(Eggs), typeof(CheeseWheel), typeof(CookedBird),
                typeof(RoastPig), typeof(SackFlour), typeof(ChickenLeg), typeof(LambLeg),
                typeof(Skillet), typeof(FlourSifter), typeof(RollingPin),
                typeof(WoodenBowlOfCarrots), typeof(WoodenBowlOfCorn), typeof(WoodenBowlOfLettuce),
                typeof(WoodenBowlOfPeas), typeof(EmptyPewterBowl), typeof(PewterBowlOfCorn),
                typeof(PewterBowlOfLettuce), typeof(PewterBowlOfPeas), typeof(PewterBowlOfPotatos),
                typeof(WoodenBowlOfStew), typeof(WoodenBowlOfTomatoSoup)
            };

            try { return Activator.CreateInstance(types[Utility.Random(types.Length)]) as Item; }
            catch { return null; }
        }

        private static Item RandomSpellbook(SuccessRating rating)
        {
            Spellbook item = null;
            switch (Utility.Random(5))
            {
                default: item = new Spellbook(); break;
                case 1: item = new NecromancerSpellbook(); break;
                case 2: item = new BookOfBushido(); break;
                case 3: item = new BookOfChivalry(); break;
                case 4: item = new BookOfNinjitsu(); break;
            }
            if (item == null) return null;
            else
            {
                switch (item.SpellbookType)
                {
                    case SpellbookType.Regular:
                        {
                            switch (rating)
                            {
                                default:
                                    { item.Content = 0xFFFF; break; }
                                case SuccessRating.Success:
                                    { item.Content = 0xFFFFFFFF; break; }
                                case SuccessRating.CompleteSuccess:
                                    { item.Content = 0xFFFFFFFFFFFF; break; }
                                case SuccessRating.ExceptionalSuccess:
                                    { item.Content = ulong.MaxValue; break; }
                            }
                            break;
                        }
                    default: item.Content = (1ul << item.BookCount) - 1; break;
                }
            }
            return item;
        }

        [Flags]
        private enum PilferFlags
        {
            None = 0x000,
            Armor = 0x001,
            MetalWeapon = 0x002,
            Jewel = 0x004,
            Reagent = 0x008,
            Potion = 0x010,
            Food = 0x020,
            Clothes = 0x040,
            ArcheryWeapon = 0x080,
            Scroll = 0x100,
            Spellbook = 0x200,
            Wand = 0x400
        }

        private static bool IsFood(int itemID)
        {
            if ((itemID >= 0x970 && itemID <= 0x973) ||
                (itemID >= 0x976 && itemID <= 0x97E) ||
                (itemID >= 0x98C && itemID <= 0x9A7) ||
                (itemID >= 0x9AD && itemID <= 0x9AF) ||
                (itemID >= 0x9B3 && itemID <= 0x9FA) ||
                (itemID >= 0x1039 && itemID <= 0x1046) ||
                (itemID >= 0x15F8 && itemID <= 0x160C) ||
                (itemID >= 0x171D && itemID <= 0x172D) ||
                (itemID >= 0x1F7D && itemID <= 0x1F9E))
                return true;

            return false;
        }

        private static bool IsClothes(int itemID)
        {
            if ((itemID >= 0x1515 && itemID <= 0x1518) ||
                (itemID >= 0x152E && itemID <= 0x1531) ||
                (itemID >= 0x1537 && itemID <= 0x154C) ||
                (itemID >= 0x1EFD && itemID <= 0x1F04) ||
                (itemID >= 0x170B && itemID <= 0x171C))
                return true;

            return false;
        }

        private static bool IsArmor(int itemID)
        {
            if ((itemID >= 0x13BB && itemID <= 0x13E2) ||
                (itemID >= 0x13E5 && itemID <= 0x13F2) ||
                (itemID >= 0x1408 && itemID <= 0x141A) ||
                (itemID >= 0x144E && itemID <= 0x1457))
                return true;

            return false;
        }

        private static bool IsMetalWeapon(int itemID)
        {
            if ((itemID >= 0xF43 && itemID <= 0xF4E) ||
                (itemID >= 0xF51 && itemID <= 0xF52) ||
                (itemID >= 0xF5C && itemID <= 0xF63) ||
                (itemID >= 0x13AF && itemID <= 0x13B0) ||
                (itemID >= 0x13B5 && itemID <= 0x13BA) ||
                (itemID >= 0x13FA && itemID <= 0x13FB) ||
                (itemID >= 0x13FE && itemID <= 0x1407) ||
                (itemID >= 0x1438 && itemID <= 0x1443))
                return true;

            return false;
        }

        private static bool IsArcheryWeapon(int itemID)
        {
            if ((itemID >= 0xF4F && itemID <= 0xF50) ||
                (itemID >= 0x13B1 && itemID <= 0x13B2) ||
                (itemID >= 0x13FC && itemID <= 0x13FD))
                return true;

            return false;
        }

        private static PilferFlags ProcessDisplayCase(StaticTile[] tiles)
        {
            PilferFlags flags = PilferFlags.None;

            for (int i = 0; i < tiles.Length; ++i)
            {
                int tileID = tiles[i].ID;
                tileID &= 0x3FFF;
                flags |= PilferTileItem(tileID);
            }

            if (Core.Debug)
                Console.WriteLine("TEST: Processed {0} tiles in the display case.", tiles.Length.ToString());

            return flags;
        }

        private static PilferFlags PilferTileItem(int itemID)
        {
            PilferFlags res = PilferFlags.None;

            ItemData id = TileData.ItemTable[itemID];
            TileFlag flags = id.Flags;

            if ((flags & TileFlag.Wearable) != 0)
            {
                if (IsClothes(itemID))
                    res |= PilferFlags.Clothes;
                else if (IsArmor(itemID))
                    res |= PilferFlags.Armor;
                else if (IsMetalWeapon(itemID))
                    res |= PilferFlags.MetalWeapon;
                else if (IsArcheryWeapon(itemID))
                    res |= PilferFlags.ArcheryWeapon;
            }

            if (IsFood(itemID))
                res |= PilferFlags.Food;

            if ((itemID >= 0xF0F && itemID <= 0xF30) ||
                (itemID >= 0x1F05 && itemID <= 0x1F0A))
                res |= PilferFlags.Jewel;

            if (itemID >= 0xEFB && itemID <= 0xF0E)
                res |= PilferFlags.Potion;

            if (itemID >= 0xF78 && itemID <= 0xF91)
                res |= PilferFlags.Reagent;

            if ((itemID >= 0xE34 && itemID <= 0xE3A) || 
                (itemID >= 0xEF3 && itemID <= 0xEF9) ||
                (itemID >= 0x1F2D && itemID <= 0x1F72) ||
                (itemID >= 0x2260 && itemID <= 0x227C) ||
                (itemID >= 0x2D51 && itemID <= 0x2D60))
                res |= PilferFlags.Scroll;

            if (itemID == 0xE38 || itemID == 0xEFA || 
               (itemID >= 0x2252 && itemID <= 0x2254) ||
                itemID == 0x238C || itemID == 0x23A0 || itemID == 0x2D50)
                res |= PilferFlags.Spellbook;

            if (itemID >= 0xDF0 && itemID <= 0xDF5)
                res |= PilferFlags.Wand;

            return res;
        }

        private static PilferFlags PilferTarget(StaticTarget target, Mobile from)
        {
            int itemID = target.ItemID;
            itemID &= 0x3FFF;

            StaticTile[] tiles = from.Map.Tiles.GetStaticTiles(target.X, target.Y);

            for (int i = 0; i < tiles.Length; ++i)
            {
                int tileID = tiles[i].ID;
                tileID &= 0x3FFF;
                if ((tileID >= 0xA9F && tileID <= 0xAA5) ||
                    (tileID >= 0xADF && tileID <= 0xB18))   //display case IDs
                {
                    return ProcessDisplayCase(tiles);
                }
            }
            if (Core.Debug)
                Console.WriteLine("TEST: Not a display case.");

            return PilferTileItem(itemID);
        }

        private static bool GetFlag(PilferFlags flag, PilferFlags inFlags)
        {
            return ((inFlags & flag) != 0);
        }

        private static void SetFlag(PilferFlags flag, PilferFlags inFlags, bool value)
        {
            if (value)
                inFlags |= flag;
            else
                inFlags &= ~flag;
        }
    }
}