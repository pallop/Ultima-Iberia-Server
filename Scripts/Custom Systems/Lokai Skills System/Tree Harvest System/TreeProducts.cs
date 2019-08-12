/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Network;
using Server.Engines.Harvest;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items
{
    public enum TreeProduct
    {
        AppleSauce, AppleCider, AppleVinegar, ApplePowder, BananaPuree, CoconutOil, ShavedCoconut, OliveOil, MincedDates, PeachWine, 
        FigNewtons, CherrySyrup, CherryStain, PearSauce, SandalwoodSyrup, YuccaExtract, Syrup, PalmOil, PineTar, MapleSyrup,
        SandalwoodPowder, IronwoodPowder, BeechGum, CassiaPowder, DogwoodPowder, WillowOil, OakStain, BlackOakStain, HeartOfPalm,
        CedarChips, AshStain, WalnutStain, CrushedBeechnut, CrushedAcorn, SpiderSprigs, JuniperTea, HickoryFlavor, EssenceOfEucalyptus,
        CypressPaste, CactusNeedle, PruneJuice, OhiiPowder, YewShavings
    }

    public class TreeProductItem : Item
    {
        private TreeProduct m_Product;
        public TreeProduct Product { get { return m_Product; } set { m_Product = value; } }

        private TreeProductType m_ProductType;
        public TreeProductType ProductType { get { return m_ProductType; } set { m_ProductType = value; } }

        public override void OnDoubleClick(Mobile from)
        {
            TreeHarvest.GetHarvestInfo(from, this);
        }

        [Constructable]
        public TreeProductItem(TreeProduct product)
        {
            Stackable = false;
            m_Product = product;
            switch (product)
            {
                case TreeProduct.CrushedAcorn: ItemID = 0x1604; ProductType = TreeProductType.Spice; break;
                case TreeProduct.AshStain: ItemID = 0x183B; Hue = 845; ProductType = TreeProductType.Crafting; break;
                case TreeProduct.BeechGum: ItemID = 0x1604; ProductType = TreeProductType.Food; break;
                case TreeProduct.CrushedBeechnut: ItemID = 0x1604; ProductType = TreeProductType.Spice; break;
                case TreeProduct.CherryStain: ItemID = 0x1604; Hue = 405; ProductType = TreeProductType.Crafting; break;
                case TreeProduct.BlackOakStain: ItemID = 0x183B; Hue = 435; ProductType = TreeProductType.Crafting; break;
                case TreeProduct.OliveOil: ItemID = 0x183B; ProductType = TreeProductType.Food; break;
                case TreeProduct.CactusNeedle: ItemID = 0x1604; Hue = 270; ProductType = TreeProductType.Crafting; break;
                case TreeProduct.CassiaPowder: ItemID = 0x1604; Hue = 735; ProductType = TreeProductType.Medicine; break;
                case TreeProduct.CedarChips: ItemID = 0x1604; Hue = 835; ProductType = TreeProductType.Reagent; break;
                case TreeProduct.CherrySyrup: ItemID = 0x183C; Hue = 135; ProductType = TreeProductType.Medicine; break;
                case TreeProduct.CypressPaste: ItemID = 0x1604; ProductType = TreeProductType.Medicine; break;
                case TreeProduct.DogwoodPowder: ItemID = 0x1604; Hue = 868; ProductType = TreeProductType.Reagent; break;
                case TreeProduct.EssenceOfEucalyptus: ItemID = 0x183B; ProductType = TreeProductType.Medicine; break;
                case TreeProduct.FigNewtons: ItemID = 0x1604; Hue = 20; ProductType = TreeProductType.Food; break;
                case TreeProduct.PearSauce: ItemID = 0x183B; Hue = 51; ProductType = TreeProductType.Food; break;
                case TreeProduct.PeachWine: ItemID = 0x183B; ProductType = TreeProductType.Food; break;
                case TreeProduct.HickoryFlavor: ItemID = 0x183B; Hue = 860; ProductType = TreeProductType.Spice; break;
                case TreeProduct.IronwoodPowder: ItemID = 0x1604; Hue = 749; ProductType = TreeProductType.Reagent; break;
                case TreeProduct.JuniperTea: ItemID = 0x183B; ProductType = TreeProductType.Food; break;
                case TreeProduct.MapleSyrup: ItemID = 0x183B; Hue = 147; ProductType = TreeProductType.Food; break;
                case TreeProduct.OakStain: ItemID = 0x183B; Hue = 644; ProductType = TreeProductType.Crafting; break;
                case TreeProduct.OhiiPowder: ItemID = 0x1604; ProductType = TreeProductType.Medicine; break;
                case TreeProduct.HeartOfPalm: ItemID = 0x1604; Hue = 345; ProductType = TreeProductType.Medicine; break;
                case TreeProduct.PalmOil: ItemID = 0x183B; Hue = 245; ProductType = TreeProductType.Medicine; break;
                case TreeProduct.PineTar: ItemID = 0x183B; Hue = 260; ProductType = TreeProductType.Crafting; break;
                case TreeProduct.PruneJuice: ItemID = 0x183C; ProductType = TreeProductType.Food; break;
                case TreeProduct.AppleSauce: ItemID = 0x1604; ProductType = TreeProductType.Food; break;
                case TreeProduct.AppleCider: ItemID = 0x183B; ProductType = TreeProductType.Food; break;
                case TreeProduct.AppleVinegar: ItemID = 0x183B; ProductType = TreeProductType.Medicine; break;
                case TreeProduct.ApplePowder: ItemID = 0x1604; ProductType = TreeProductType.Spice; break;
                case TreeProduct.BananaPuree: ItemID = 0x1604; ProductType = TreeProductType.Food; break;
                case TreeProduct.SandalwoodPowder: ItemID = 0x1604; Hue = 248; ProductType = TreeProductType.Reagent; break;
                case TreeProduct.SandalwoodSyrup: ItemID = 0x183B; Hue = 248; ProductType = TreeProductType.Spice; break;
                case TreeProduct.SpiderSprigs: ItemID = 0x1604; ProductType = TreeProductType.Reagent; break;
                case TreeProduct.Syrup: ItemID = 0x183B; ProductType = TreeProductType.Food; break;
                case TreeProduct.MincedDates: ItemID = 0x1604; ProductType = TreeProductType.Food; break;
                case TreeProduct.WalnutStain: ItemID = 0x183C; ProductType = TreeProductType.Crafting; break;
                case TreeProduct.ShavedCoconut: ItemID = 0x1604; ProductType = TreeProductType.Food; break;
                case TreeProduct.WillowOil: ItemID = 0x183B; ProductType = TreeProductType.Crafting; break;
                case TreeProduct.YewShavings: ItemID = 0x1604; Hue = 865; ProductType = TreeProductType.Crafting; break;
                case TreeProduct.YuccaExtract: ItemID = 0x183C; ProductType = TreeProductType.Medicine; break;
            }
        }

        public override double DefaultWeight
        {
            get
            {
                switch (Product)
                {
                    case TreeProduct.CrushedAcorn: return 0.2;
                    case TreeProduct.AshStain: return 0.2;
                    case TreeProduct.BeechGum: return 0.2;
                    case TreeProduct.CrushedBeechnut: return 0.2;
                    case TreeProduct.CherryStain: return 0.2;
                    case TreeProduct.BlackOakStain: return 0.2;
                    case TreeProduct.OliveOil: return 0.2;
                    case TreeProduct.CactusNeedle: return 0.2;
                    case TreeProduct.CassiaPowder: return 0.2;
                    case TreeProduct.CedarChips: return 0.2;
                    case TreeProduct.CherrySyrup: return 0.2;
                    case TreeProduct.CypressPaste: return 0.2;
                    case TreeProduct.DogwoodPowder: return 0.2;
                    case TreeProduct.EssenceOfEucalyptus: return 0.2;
                    case TreeProduct.FigNewtons: return 0.2;
                    case TreeProduct.PearSauce: return 0.2;
                    case TreeProduct.PeachWine: return 0.2;
                    case TreeProduct.HickoryFlavor: return 0.2;
                    case TreeProduct.IronwoodPowder: return 0.2;
                    case TreeProduct.JuniperTea: return 0.2;
                    case TreeProduct.MapleSyrup: return 0.2;
                    case TreeProduct.OakStain: return 0.2;
                    case TreeProduct.OhiiPowder: return 0.2;
                    case TreeProduct.HeartOfPalm: return 0.2;
                    case TreeProduct.PalmOil: return 0.2;
                    case TreeProduct.PineTar: return 0.2;
                    case TreeProduct.PruneJuice: return 0.2;
                    case TreeProduct.AppleSauce: return 0.2;
                    case TreeProduct.AppleCider: return 0.2;
                    case TreeProduct.AppleVinegar: return 0.2;
                    case TreeProduct.ApplePowder: return 0.2;
                    case TreeProduct.BananaPuree: return 0.2;
                    case TreeProduct.SandalwoodPowder: return 0.2;
                    case TreeProduct.SandalwoodSyrup: return 0.2;
                    case TreeProduct.SpiderSprigs: return 0.2;
                    case TreeProduct.Syrup: return 0.2;
                    case TreeProduct.MincedDates: return 0.2;
                    case TreeProduct.WalnutStain: return 0.2;
                    case TreeProduct.ShavedCoconut: return 0.2;
                    case TreeProduct.WillowOil: return 0.2;
                    case TreeProduct.YewShavings: return 0.2;
                    case TreeProduct.YuccaExtract: return 0.2;
                }
                return 0.4;
            }
        }

        public override string DefaultName
        {
            get
            {
                switch (Product)
                {
                    case TreeProduct.CrushedAcorn: return "crushed acorn";
                    case TreeProduct.AshStain: return "ash stain";
                    case TreeProduct.BeechGum: return "beech gum";
                    case TreeProduct.CrushedBeechnut: return "crushed beechnut";
                    case TreeProduct.CherryStain: return "cherry stain";
                    case TreeProduct.BlackOakStain: return "black oak stain";
                    case TreeProduct.OliveOil: return "olive oil";
                    case TreeProduct.CactusNeedle: return "cactus needle";
                    case TreeProduct.CassiaPowder: return "cassia powder";
                    case TreeProduct.CedarChips: return "cedar chips";
                    case TreeProduct.CherrySyrup: return "cherry syrup";
                    case TreeProduct.CypressPaste: return "cypress paste";
                    case TreeProduct.DogwoodPowder: return "dogwood powder";
                    case TreeProduct.EssenceOfEucalyptus: return "essence of eucalyptus";
                    case TreeProduct.FigNewtons: return "fig newtons";
                    case TreeProduct.PearSauce: return "pear sauce";
                    case TreeProduct.PeachWine: return "peach wine";
                    case TreeProduct.HickoryFlavor: return "hickory flavor";
                    case TreeProduct.IronwoodPowder: return "Ironwood powder";
                    case TreeProduct.JuniperTea: return "juniper tea";
                    case TreeProduct.MapleSyrup: return "maple syrup";
                    case TreeProduct.OakStain: return "oak stain";
                    case TreeProduct.OhiiPowder: return "ohii powder";
                    case TreeProduct.HeartOfPalm: return "heart of palm";
                    case TreeProduct.PalmOil: return "palm oil";
                    case TreeProduct.PineTar: return "pine tar";
                    case TreeProduct.PruneJuice: return "prune Juice";
                    case TreeProduct.AppleSauce: return "apple sauce";
                    case TreeProduct.AppleCider: return "apple cider";
                    case TreeProduct.AppleVinegar: return "apple vinegar";
                    case TreeProduct.ApplePowder: return "apple powder";
                    case TreeProduct.BananaPuree: return "banana puree";
                    case TreeProduct.SandalwoodPowder: return "sandalwood powder";
                    case TreeProduct.SandalwoodSyrup: return "sandalwood syrup";
                    case TreeProduct.SpiderSprigs: return "spider sprigs";
                    case TreeProduct.Syrup: return "syrup";
                    case TreeProduct.MincedDates: return "minced dates";
                    case TreeProduct.WalnutStain: return "walnut stain";
                    case TreeProduct.ShavedCoconut: return "shaved coconut";
                    case TreeProduct.WillowOil: return "willow oil";
                    case TreeProduct.YewShavings: return "yew shavings";
                    case TreeProduct.YuccaExtract: return "yucca extract";
                }
                return "tree product";
            }
        }

        public TreeProductItem(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((int)m_ProductType);
            writer.Write((int)m_Product);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_ProductType = (TreeProductType)reader.ReadInt();
            m_Product = (TreeProduct)reader.ReadInt();
        }
    }
}
