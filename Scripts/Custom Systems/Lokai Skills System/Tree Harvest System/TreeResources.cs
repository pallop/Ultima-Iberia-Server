/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Engines.Harvest;
using Server.Network;
using Server.Targeting;
using System.Collections.Generic;

namespace Server.Items
{
    public enum TreeResource
    {
        RedApple, RipeBanana, WholeCoconut, BlackOlives, TropicalDates, GreenOlives, HarvestPeach, FigFruit, Cherry,
        BlackCherry, GoldenPear, SandalwoodRoot, YuccaRoot, TreeSap, PalmTreeSap, PineTreeSap, MapleTreeSap, SandalwoodSap,
        IronwoodBark, BeechBark, CassiaBark, DogwoodBark, WillowBark, OakBark, BlackOakBark, PalmHusks, AppleBark,
        CedarBark, AshBark, Walnut, Beechnut, Acorn, SpiderTreeLeaves, JuniperLeaves, HickoryBark, EucalyptusLeaves,
        CypressLeaves, CactusSpine, Plum, OhiiRoot, YewBark
    }

    public class TreeResourceItem : Item
    {
        private TreeResourceType m_ResourceType;
        public TreeResourceType ResourceType { get { return m_ResourceType; } set { m_ResourceType = value; } }

        private TreeResource m_Resource;
        public TreeResource Resource { get { return m_Resource; } set { m_Resource = value; } }

        public override void OnDoubleClick(Mobile from)
        {
            from.BeginTarget(-1, true, TargetFlags.None, new TargetCallback(Resource_OnTarget));
            from.SendLocalizedMessage(1010086); // What do you want to use this on?
            // TreeHarvest.GetHarvestInfo(from, this); 
        }

        private TreeProduct grindResult;
        public TreeProduct GrindResult { get { return grindResult; } }
        private TreeProduct cookResult;
        public TreeProduct CookResult { get { return cookResult; } }

        private bool grindable;
        public bool Grindable { get { return grindable; } }
        private bool cookable;
        public bool Cookable { get { return cookable; } }

        public virtual void Resource_OnTarget(Mobile from, object targ)
        {
            LokaiSkills skills = LokaiSkillUtilities.XMLGetSkills(from);
            LokaiSkill brewLokaiSkill = skills[LokaiSkillName.Brewing];
            LokaiSkill herbLokaiSkill = skills[LokaiSkillName.Herblore];

            if (this.grindable)
            {
                if (targ is MortarPestle)
                {
                    Container pack = from.Backpack;
                    if (!(pack == null || pack.Deleted))
                        Grind(this, from, LokaiSkillUtilities.CheckLokaiSkill(from, herbLokaiSkill, 0.0, 100.0), grindResult, pack);
                }
                else
                    from.SendMessage("Try using this on a mortar and pestle.");
            }
            else if (this.cookable)
            {
                if (IsHeatSource(targ))
                {
                    Container pack = from.Backpack;
                    if (!(pack == null || pack.Deleted))
                        Cook(this, from, LokaiSkillUtilities.CheckLokaiSkill(from, brewLokaiSkill, 0.0, 100.0), cookResult, pack);
                }
                else
                    from.SendMessage("Try using this on a heat source.");
            }
        }

        private List<int> m_HeatSources = new List<int>()
			{
				0x461, 0x48E, // Sandstone oven/fireplace
				0x92B, 0x96C, // Stone oven/fireplace
				0xDE3, 0xDE9, // Campfire
				0xFAC, 0xFAC, // Firepit
				0x184A, 0x184C, // Heating stand (left)
				0x184E, 0x1850, // Heating stand (right)
				0x398C, 0x399F,  // Fire field
				0x2DDB, 0x2DDC,	//Elven stove
				0x19AA, 0x19BB,	// Veteran Reward Brazier
				0x197A, 0x19A9, // Large Forge 
				0x0FB1, 0x0FB1, // Small Forge
				0x2DD8, 0x2DD8 // Elven Forge
			};

        private bool IsHeatSource(object targeted)
        {
            int itemID;

            if (targeted is Item)
                itemID = ((Item)targeted).ItemID;
            else if (targeted is StaticTarget)
                itemID = ((StaticTarget)targeted).ItemID;
            else
                return false;

            if (m_HeatSources.Contains(itemID))
                return true;

            return false;
        }

        public static void Grind(TreeResourceItem resource, Mobile from, SuccessRating rating, TreeProduct product, Container pack)
        {
            switch (rating)
            {
                case SuccessRating.PartialSuccess:
                case SuccessRating.Success:
                case SuccessRating.CompleteSuccess:
                case SuccessRating.ExceptionalSuccess:
                case SuccessRating.TooEasy:
                    {
                        TreeProductItem item = new TreeProductItem(product);
                        if (item.ItemID == 0x183B && !pack.ConsumeTotal(typeof(LargeEmptyFlask), 1))
                        {
                            from.SendMessage("You need an empty flask in your pack to store the {0}. The resource was lost.", item.Name);
                            resource.Consume(1);
                            item.Delete();
                        }
                        else if (item.ItemID == 0x1604 && !pack.ConsumeTotal(typeof(EmptyWoodenBowl), 1))
                        {
                            from.SendMessage("You need an empty bowl in your pack to store the {0}. The resource was lost.", item.Name);
                            resource.Consume(1);
                            item.Delete();
                        }
                        else if (from.AddToBackpack(item) || item.DropToWorld(from, from.Location))
                        {
                            from.SendMessage("You grind the resource into {0}.", item.Name);
                            resource.Consume(1);
                        }
                        else
                        {
                            from.SendMessage("Unable to create the {0}.", item.Name);
                            item.Delete();
                        }
                        break;
                    }
                case SuccessRating.Failure:
                    {
                        from.SendMessage("You fail to grind the resource.");
                        break;
                    }
                case SuccessRating.HazzardousFailure:
                    {
                        from.SendMessage("You grind the resource but are left with nothing usable.");
                        resource.Consume(1);
                        break;
                    }
                case SuccessRating.CriticalFailure:
                case SuccessRating.TooDifficult:
                    {
                        from.SendMessage("You grind your fingers, and ruin the resource!");
                        from.Damage(Utility.RandomMinMax(1, 5));
                        from.Animate(34, 5, 1, true, false, 0);
                        from.Emote("Ouch!");
                        resource.Consume(1);
                        break;
                    }
                case SuccessRating.LokaiSkillNotEnabled:
                    {
                        from.SendMessage("This lokaiSkill is not enabled."); // Should never happen, but just in case...
                        break;
                    }
            }
        }

        public static void Cook(TreeResourceItem resource, Mobile from, SuccessRating rating, TreeProduct product, Container pack)
        {
            switch (rating)
            {
                case SuccessRating.PartialSuccess:
                case SuccessRating.Success:
                case SuccessRating.CompleteSuccess:
                case SuccessRating.ExceptionalSuccess:
                case SuccessRating.TooEasy:
                    {
                        TreeProductItem item = new TreeProductItem(product);
                        if (item.ItemID == 0x183B && !pack.ConsumeTotal(typeof(LargeEmptyFlask), 1))
                        {
                            from.SendMessage("You need an empty flask in your pack to store the {0}. The resource was lost.", item.Name);
                            resource.Consume(1);
                            item.Delete();
                        }
                        else if (item.ItemID == 0x1604 && !pack.ConsumeTotal(typeof(EmptyWoodenBowl), 1))
                        {
                            from.SendMessage("You need an empty bowl in your pack to store the {0}. The resource was lost.", item.Name);
                            resource.Consume(1);
                            item.Delete();
                        }
                        else if (from.AddToBackpack(item) || item.DropToWorld(from, from.Location))
                        {
                            from.SendMessage("You cook the resource and turn it in to {0}.", item.Name);
                            resource.Consume(1);
                        }
                        else
                        {
                            from.SendMessage("Unable to create the {0}.", item.Name);
                            item.Delete();
                        }
                        break;
                    }
                case SuccessRating.Failure:
                    {
                        from.SendMessage("You fail to cook the resource.");
                        break;
                    }
                case SuccessRating.HazzardousFailure:
                    {
                        from.SendMessage("You burn the resource and are left with nothing usable.");
                        resource.Consume(1);
                        break;
                    }
                case SuccessRating.CriticalFailure:
                case SuccessRating.TooDifficult:
                    {
                        from.SendMessage("You burn the resource and your hands too!");
                        from.Damage(Utility.RandomMinMax(1, 5));
                        from.Animate(34, 5, 1, true, false, 0);
                        from.Emote("Ouch!");
                        resource.Consume(1);
                        break;
                    }
                case SuccessRating.LokaiSkillNotEnabled:
                    {
                        from.SendMessage("This lokaiSkill is not enabled."); // Should never happen, but just in case...
                        break;
                    }
            }
        }

        [Constructable]
        public TreeResourceItem(TreeResource resource)
            : this(resource, 1)
        {
        }

        [Constructable]
        public TreeResourceItem(TreeResource resource, int amount)
        {
            Stackable = true;
            Amount = amount;
            m_Resource = resource;
            ResetValues();
        }

        private void ResetValues()
        {
            grindable = false;
            cookable = false;
            grindResult = (TreeProduct)(-1);
            cookResult = (TreeProduct)(-1);
            switch (m_Resource)
            {
                case TreeResource.Acorn: ItemID = 0x09EA; ResourceType = TreeResourceType.FruitNut; grindResult = TreeProduct.CrushedAcorn; break;
                case TreeResource.AppleBark: ItemID = 0x318F; Hue = 652; ResourceType = TreeResourceType.BarkSkin; grindResult = TreeProduct.ApplePowder; break;
                case TreeResource.AshBark: ItemID = 0x318F; Hue = 845; ResourceType = TreeResourceType.BarkSkin; cookResult = TreeProduct.AshStain; break;
                case TreeResource.BeechBark: ItemID = 0x318F; ResourceType = TreeResourceType.BarkSkin; grindResult = TreeProduct.BeechGum; break;
                case TreeResource.Beechnut: ItemID = 0x09EA; ResourceType = TreeResourceType.FruitNut; grindResult = TreeProduct.CrushedBeechnut; break;
                case TreeResource.BlackCherry: ItemID = 0xF7A; Hue = 405; ResourceType = TreeResourceType.FruitNut; grindResult = TreeProduct.CherryStain; break;
                case TreeResource.BlackOakBark: ItemID = 0x318F; Hue = 435; ResourceType = TreeResourceType.BarkSkin; cookResult = TreeProduct.BlackOakStain; break;
                case TreeResource.BlackOlives: ItemID = 0x1727; Hue = 0; ResourceType = TreeResourceType.FruitNut; grindResult = TreeProduct.OliveOil; break;
                case TreeResource.CactusSpine: ItemID = 0x1BD4; Hue = 270; ResourceType = TreeResourceType.LeafSpine; grindResult = TreeProduct.CactusNeedle; break;
                case TreeResource.CassiaBark: ItemID = 0x318F; Hue = 735; ResourceType = TreeResourceType.BarkSkin; grindResult = TreeProduct.CassiaPowder; break;
                case TreeResource.CedarBark: ItemID = 0x318F; Hue = 835; ResourceType = TreeResourceType.BarkSkin; grindResult = TreeProduct.CedarChips; break;
                case TreeResource.Cherry: ItemID = 0xF7A; Hue = 135; ResourceType = TreeResourceType.FruitNut; cookResult = TreeProduct.CherrySyrup; break;
                case TreeResource.CypressLeaves: ItemID = 0x0C3B; ResourceType = TreeResourceType.LeafSpine; grindResult = TreeProduct.CypressPaste; break;
                case TreeResource.DogwoodBark: ItemID = 0x2F5F; Hue = 868; ResourceType = TreeResourceType.BarkSkin; grindResult = TreeProduct.DogwoodPowder; break;
                case TreeResource.EucalyptusLeaves: ItemID = 0x0C3D; ResourceType = TreeResourceType.LeafSpine; cookResult = TreeProduct.EssenceOfEucalyptus; break;
                case TreeResource.FigFruit: ItemID = 0x994; Hue = 20; ResourceType = TreeResourceType.FruitNut; cookResult = TreeProduct.FigNewtons; break;
                case TreeResource.GoldenPear: ItemID = 0x994; Hue = 51; ResourceType = TreeResourceType.FruitNut; cookResult = TreeProduct.PearSauce; break;
                case TreeResource.GreenOlives: ItemID = 0x1727; Hue = 50; ResourceType = TreeResourceType.FruitNut; cookResult = TreeProduct.OliveOil; break;
                case TreeResource.HarvestPeach: ItemID = 0x9D2; ResourceType = TreeResourceType.FruitNut; cookResult = TreeProduct.PeachWine; break;
                case TreeResource.HickoryBark: ItemID = 0x318F; Hue = 860; ResourceType = TreeResourceType.BarkSkin; grindResult = TreeProduct.HickoryFlavor; break;
                case TreeResource.IronwoodBark: ItemID = 0x318F; Hue = 749; ResourceType = TreeResourceType.BarkSkin; grindResult = TreeProduct.IronwoodPowder; break;
                case TreeResource.JuniperLeaves: ItemID = 0x1784; ResourceType = TreeResourceType.LeafSpine; cookResult = TreeProduct.JuniperTea; break;
                case TreeResource.MapleTreeSap: ItemID = 0x09EC; Hue = 147; ResourceType = TreeResourceType.SapJuice; cookResult = TreeProduct.MapleSyrup; break;
                case TreeResource.OakBark: ItemID = 0x318F; Hue = 644; ResourceType = TreeResourceType.BarkSkin; cookResult = TreeProduct.OakStain; break;
                case TreeResource.OhiiRoot: ItemID = 0x0C73; ResourceType = TreeResourceType.RootBranch; grindResult = TreeProduct.OhiiPowder; break;
                case TreeResource.PalmHusks: ItemID = 0x09EA; Hue = 345; ResourceType = TreeResourceType.LeafSpine; grindResult = TreeProduct.HeartOfPalm; break;
                case TreeResource.PalmTreeSap: ItemID = 0x09EC; Hue = 245; ResourceType = TreeResourceType.SapJuice; cookResult = TreeProduct.PalmOil; break;
                case TreeResource.PineTreeSap: ItemID = 0x09EC; Hue = 260; ResourceType = TreeResourceType.SapJuice; cookResult = TreeProduct.PineTar; break;
                case TreeResource.Plum: ItemID = 0x9D2; Hue = 312; ResourceType = TreeResourceType.FruitNut; grindResult = TreeProduct.PruneJuice; break;
                case TreeResource.RedApple: ItemID = 0x9D0; ResourceType = TreeResourceType.FruitNut; grindResult = TreeProduct.AppleSauce; break;
                case TreeResource.RipeBanana: ItemID = 0x1720; ResourceType = TreeResourceType.FruitNut; grindResult = TreeProduct.BananaPuree; break;
                case TreeResource.SandalwoodRoot: ItemID = 0x0C73; Hue = 248; ResourceType = TreeResourceType.RootBranch; grindResult = TreeProduct.SandalwoodPowder; break;
                case TreeResource.SandalwoodSap: ItemID = 0x09EC; Hue = 248; ResourceType = TreeResourceType.SapJuice; cookResult = TreeProduct.SandalwoodSyrup; break;
                case TreeResource.SpiderTreeLeaves: ItemID = 0x0CB4; ResourceType = TreeResourceType.LeafSpine; grindResult = TreeProduct.SpiderSprigs; break;
                case TreeResource.TreeSap: ItemID = 0x09EC; ResourceType = TreeResourceType.SapJuice; cookResult = TreeProduct.Syrup; break;
                case TreeResource.TropicalDates: ItemID = 0x1727; ResourceType = TreeResourceType.FruitNut; grindResult = TreeProduct.MincedDates; break;
                case TreeResource.Walnut: ItemID = 0x09EA; ResourceType = TreeResourceType.FruitNut; cookResult = TreeProduct.WalnutStain; break;
                case TreeResource.WholeCoconut: ItemID = 0x1726; ResourceType = TreeResourceType.FruitNut; grindResult = TreeProduct.CoconutOil; break;
                case TreeResource.WillowBark: ItemID = 0x318F; ResourceType = TreeResourceType.BarkSkin; cookResult = TreeProduct.WillowOil; break;
                case TreeResource.YewBark: ItemID = 0x318F; Hue = 865; ResourceType = TreeResourceType.BarkSkin; grindResult = TreeProduct.YewShavings; break;
                case TreeResource.YuccaRoot: ItemID = 0x0C73; ResourceType = TreeResourceType.RootBranch; cookResult = TreeProduct.YuccaExtract; break;
            }
            if ((int)grindResult >= 0) grindable = true;
            if ((int)cookResult >= 0) cookable = true;
        }

        public override double DefaultWeight
        {
            get
            {
                switch (Resource)
                {
                    case TreeResource.Acorn: return 0.2;
                    case TreeResource.AppleBark: return 0.4;
                    case TreeResource.AshBark: return 0.4;
                    case TreeResource.BeechBark: return 0.4;
                    case TreeResource.Beechnut: return 0.2;
                    case TreeResource.BlackCherry: return 0.1;
                    case TreeResource.BlackOakBark: return 0.4;
                    case TreeResource.BlackOlives: return 0.2;
                    case TreeResource.CactusSpine: return 0.1;
                    case TreeResource.CassiaBark: return 0.4;
                    case TreeResource.CedarBark: return 0.4;
                    case TreeResource.Cherry: return 0.1;
                    case TreeResource.CypressLeaves: return 0.3;
                    case TreeResource.DogwoodBark: return 0.4;
                    case TreeResource.EucalyptusLeaves: return 0.3;
                    case TreeResource.FigFruit: return 0.3;
                    case TreeResource.GoldenPear: return 0.5;
                    case TreeResource.GreenOlives: return 0.2;
                    case TreeResource.HarvestPeach: return 0.5;
                    case TreeResource.HickoryBark: return 0.4;
                    case TreeResource.IronwoodBark: return 0.4;
                    case TreeResource.JuniperLeaves: return 0.4;
                    case TreeResource.MapleTreeSap: return 0.5;
                    case TreeResource.OakBark: return 0.4;
                    case TreeResource.OhiiRoot: return 0.3;
                    case TreeResource.PalmHusks: return 0.3;
                    case TreeResource.PalmTreeSap: return 0.5;
                    case TreeResource.PineTreeSap: return 0.5;
                    case TreeResource.Plum: return 0.3;
                    case TreeResource.RedApple: return 0.4;
                    case TreeResource.RipeBanana: return 0.2;
                    case TreeResource.SandalwoodRoot: return 0.3;
                    case TreeResource.SandalwoodSap: return 0.5;
                    case TreeResource.SpiderTreeLeaves: return 0.3;
                    case TreeResource.TreeSap: return 0.5;
                    case TreeResource.TropicalDates: return 0.2;
                    case TreeResource.Walnut: return 0.1;
                    case TreeResource.WholeCoconut: return 0.2;
                    case TreeResource.WillowBark: return 0.4;
                    case TreeResource.YewBark: return 0.4;
                    case TreeResource.YuccaRoot: return 0.3;
                }
                return 0.4;
            }
        }

        public override string DefaultName
        {
            get
            {
                switch (Resource)
                {
                    case TreeResource.Acorn: return string.Format("acorn{0}", Amount > 1 ? "s" : "");
                    case TreeResource.AppleBark: return "apple bark";
                    case TreeResource.AshBark: return "ash bark";
                    case TreeResource.BeechBark: return "beech bark";
                    case TreeResource.Beechnut: return string.Format("beechnut{0}", Amount > 1 ? "s" : "");
                    case TreeResource.BlackCherry: return string.Format("black cherr{0}", Amount > 1 ? "ies" : "y");
                    case TreeResource.BlackOakBark: return "black oak bark";
                    case TreeResource.BlackOlives: return string.Format("bunch{0} of black olives", Amount > 1 ? "es" : "");
                    case TreeResource.CactusSpine: return string.Format("cactus spine{0}", Amount > 1 ? "s" : "");
                    case TreeResource.CassiaBark: return "cassia bark";
                    case TreeResource.CedarBark: return "cedar bark";
                    case TreeResource.Cherry: return string.Format("cherr{0}", Amount > 1 ? "ies" : "y");
                    case TreeResource.CypressLeaves: return "cypress leaves";
                    case TreeResource.DogwoodBark: return "dogwood bark";
                    case TreeResource.EucalyptusLeaves: return "eucalyptus leaves";
                    case TreeResource.FigFruit: return string.Format("fig fruit{0}", Amount > 1 ? "s" : "");
                    case TreeResource.GoldenPear: return string.Format("golden pear{0}", Amount > 1 ? "s" : "");
                    case TreeResource.GreenOlives: return string.Format("bunch{0} of green olives", Amount > 1 ? "es" : "");
                    case TreeResource.HarvestPeach: return string.Format("harvest peach{0}", Amount > 1 ? "es" : "");
                    case TreeResource.HickoryBark: return "hickory bark";
                    case TreeResource.IronwoodBark: return "ironwood bark";
                    case TreeResource.JuniperLeaves: return "juniper leaves";
                    case TreeResource.MapleTreeSap: return "maple tree sap";
                    case TreeResource.OakBark: return "oak bark";
                    case TreeResource.OhiiRoot: return string.Format("ohii root{0}", Amount > 1 ? "s" : "");
                    case TreeResource.PalmHusks: return string.Format("palm husk{0}", Amount > 1 ? "s" : "");
                    case TreeResource.PalmTreeSap: return "palm tree sap";
                    case TreeResource.PineTreeSap: return "pine tree sap";
                    case TreeResource.Plum: return string.Format("plum{0}", Amount > 1 ? "s" : "");
                    case TreeResource.RedApple: return string.Format("red apple{0}", Amount > 1 ? "s" : "");
                    case TreeResource.RipeBanana: return string.Format("ripe banana{0}", Amount > 1 ? "s" : "");
                    case TreeResource.SandalwoodRoot: return string.Format("sandalwood root{0}", Amount > 1 ? "s" : "");
                    case TreeResource.SandalwoodSap: return "sandalwood sap";
                    case TreeResource.SpiderTreeLeaves: return "spider tree leaves";
                    case TreeResource.TreeSap: return "tree sap";
                    case TreeResource.TropicalDates: return string.Format("bunch{0} of tropical dates", Amount > 1 ? "es" : "");
                    case TreeResource.Walnut: return string.Format("walnut{0}", Amount > 1 ? "s" : "");
                    case TreeResource.WholeCoconut: return string.Format("whole coconut{0}", Amount > 1 ? "s" : "");
                    case TreeResource.WillowBark: return "willow bark";
                    case TreeResource.YewBark: return "yew bark";
                    case TreeResource.YuccaRoot: return string.Format("yucca root{0}", Amount > 1 ? "s" : "");
                }
                return "tree resource";
            }
        }

        public TreeResourceItem(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version

            writer.Write((int)m_Resource);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0) m_ResourceType = (TreeResourceType)reader.ReadInt();
            m_Resource = (TreeResource)reader.ReadInt();

            ResetValues();
        }
    }
}
