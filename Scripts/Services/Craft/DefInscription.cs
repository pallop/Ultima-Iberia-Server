using System;
using Server.Items;
using Server.Spells;
using System.Collections;
using Server;
using Server.Commands;
using Server.Items;
using Server.Network;
using Server.Spells;
using Server.Targeting;
using Server.ACC.CSS.Systems.Ancient;
using Server.ACC.CSS.Systems.Rogue;
using Server.ACC.CSS.Systems.Druid;
using Server.ACC.CSS.Systems.Avatar;
using Server.ACC.CSS.Systems.Ranger;
using Server.ACC.CSS.Systems.Cleric;
using Server.ACC.CSS.Systems.Bard;



namespace Server.Engines.Craft
{
    public class DefInscription : CraftSystem
    {
        public override SkillName MainSkill
        {
            get
            {
                return SkillName.Inscribe;
            }
        }

        public override int GumpTitleNumber
        {
            get
            {
                return 1044009;
            }// <CENTER>INSCRIPTION MENU</CENTER>
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefInscription();

                return m_CraftSystem;
            }
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.0; // 0%
        }

        private DefInscription()
            : base(1, 1, 1.25)// base( 1, 1, 3.0 )
        {
        }

        public override int CanCraft(Mobile from, BaseTool tool, Type typeItem)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
            else if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            if (typeItem != null)
            {
                object o = Activator.CreateInstance(typeItem);

                if (o is SpellScroll)
                {
                    SpellScroll scroll = (SpellScroll)o;
                    Spellbook book = Spellbook.Find(from, scroll.SpellID);

                    bool hasSpell = (book != null && book.HasSpell(scroll.SpellID));

                    scroll.Delete();

                    return (hasSpell ? 0 : 1042404); // null : You don't have that spell!
                }
                else if (o is Item)
                {
                    ((Item)o).Delete();
                }
            }

            return 0;
        }

        public override void PlayCraftEffect(Mobile from)
        {
            from.PlaySound(0x249);
        }

        private static readonly Type typeofSpellScroll = typeof(SpellScroll);

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

            if (!typeofSpellScroll.IsAssignableFrom(item.ItemType)) //  not a scroll
            {
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
            else
            {
                if (failed)
                    return 501630; // You fail to inscribe the scroll, and the scroll is ruined.
                else
                    return 501629; // You inscribe the spell and put the scroll in your backpack.
            }
        }

        
        private int m_Index;

    
      
        
        
        

        public override void InitCraftList()
        {
            

            int index;
            
            if (Core.ML)
            {
               
            }
            
            // Runebook
            index = this.AddCraft(typeof(Runebook), "Runicos", 1041267, 45.0, 95.0, typeof(BlankScroll), 1044377, 8, 1044378);
            this.AddRes(index, typeof(RecallScroll), 1044445, 1, 1044253);
            this.AddRes(index, typeof(GateTravelScroll), 1044446, 1, 1044253);

            this.AddCraft(typeof(BlankScroll), "Scrolls", "Blank Scroll", 10.0, 95.0, typeof(Log), 1044041, 1, 1044351);
 

                index =   this.AddCraft(typeof(WitchsBookofFoodCrafts), "Libro de Cocina", "libro de Cocina mejorada", 30.0, 70.0, typeof(BlankScroll), 1044377, 80, 1044378);
                this.AddRes(index, typeof(Leather), 1044462, 8, 1044463);

            index = this.AddCraft(typeof(MasonryBook), "Libros de Artesanias", "Masonry Book", 100.0, 100.0, typeof(BlankScroll), 1044377, 150, 1044378);
                this.AddRes(index, typeof(Leather), 1044462, 8, 1044463);
            index = this.AddCraft(typeof(StoneMiningBook), "Libros de Artesanias", "Stone Mining Book", 100.0, 100.0, typeof(BlankScroll), 1044377, 150, 1044378);
                this.AddRes(index, typeof(Leather), 1044462, 8, 1044463);
            index = this.AddCraft(typeof(BasketWeavingBook), "Libros de Artesanias", "Basket Weaving Book", 100.0, 100.0, typeof(BlankScroll), 1044377, 150, 1044378);
                this.AddRes(index, typeof(Leather), 1044462, 8, 1044463);
                index = this.AddCraft(typeof(SandMiningBook), "Libros de Artesanias", "Sand Mining Book", 100.0, 100.0, typeof(BlankScroll), 1044377, 150, 1044378);
                this.AddRes(index, typeof(Leather), 1044462, 8, 1044463);
                index = this.AddCraft(typeof(GlassblowingBook), "Libros de Artesanias", "Glassblowing Book", 100.0, 100.0, typeof(BlankScroll), 1044377, 150, 1044378);
                this.AddRes(index, typeof(Leather), 1044462, 8, 1044463);

             //index = this.AddCraft(typeof(AncientSpellbook), "Libros de Magias", "Ancient spellbook", 60.0, 100.0, typeof(BlankScroll), 1044377, 150, 1044378);
                //this.AddRes(index, typeof(Leather), 1044462, 8, 1044463);
                //  AddCraft(typeof(SpellweavingBook), 1044294, "Spellweaving book", 50.0, 100.0, typeof(BlankScroll), 1044377, 10, 1044378);
              //index =  this.AddCraft(typeof(RogueSpellbook), "Libros de Magias", "Rogue spellbook", 60.0, 100.0, typeof(BlankScroll), 1044377, 150, 1044378);
                //this.AddRes(index, typeof(Leather), 1044462, 8, 1044463);
               index = this.AddCraft(typeof(DruidSpellbook), "Libros de Magias", "Druid spellbook", 60.0, 100.0, typeof(BlankScroll), 1044377, 150, 1044378);
                this.AddRes(index, typeof(Leather), 1044462, 8, 1044463);
               //index = this.AddCraft(typeof(AvatarSpellbook), "Libros de Magias", "Avatar spellbook", 100.0, 100.0, typeof(BlankScroll), 1044377, 150, 1044378);
               // this.AddRes(index, typeof(Leather), 1044462, 8, 1044463);
             index =   this.AddCraft(typeof(RangerSpellbook), "Libros de Magias", "Ranger spellbook", 60.0, 100.0, typeof(BlankScroll), 1044377, 150, 1044378);
                this.AddRes(index, typeof(Leather), 1044462, 8, 1044463);
              index =  this.AddCraft(typeof(ClericSpellbook), "Libros de Magias", "Cleric spellbook", 60.0, 100.0, typeof(BlankScroll), 1044377, 150, 1044378);
                this.AddRes(index, typeof(Leather), 1044462, 8, 1044463);
                index =  this.AddCraft(typeof(BardSpellbook), "Libros de Magias", "Bard spellbook", 60.0, 100.0, typeof(BlankScroll), 1044377, 150, 1044378);
                this.AddRes(index, typeof(Leather), 1044462, 8, 1044463);

            if (Core.AOS)
            {
                this.AddCraft(typeof(Engines.BulkOrders.BulkOrderBook), "Runicos", 1028793, 65.0, 115.0, typeof(BlankScroll), 1044377, 10, 1044378);
                this.AddCraft(typeof(Engines.BulkOrders.TamingBulkOrderBook), "Runicos", 1028793, 65.0, 115.0, typeof(BlankScroll), 1044377, 10, 1044378);
            }

           
            
            #region Mondain's Legacy    
            if (Core.ML)
            {
                index = this.AddCraft(typeof(ScrappersCompendium), 1044294, 1072940, 75.0, 125.0, typeof(BlankScroll), 1044377, 100, 1044378);
                this.AddRes(index, typeof(DreadHornMane), 1032682, 1, 1044253);
                this.AddRes(index, typeof(Taint), 1032679, 10, 1044253);
                this.AddRes(index, typeof(Corruption), 1032676, 10, 1044253);
                this.AddRecipe(index, (int)TinkerRecipes.ScrappersCompendium);
                this.ForceNonExceptional(index);
                this.SetNeededExpansion(index, Expansion.ML);
                
                index = this.AddCraft(typeof(SpellbookEngraver), 1044294, 1072151, 75.0, 100.0, typeof(Feather), 1044562, 1, 1044563);
                this.AddRes(index, typeof(BlackPearl), 1015001, 7, 1044253);
                this.SetNeededExpansion(index, Expansion.ML);
                
               


            }
// Construccion
            index = this.AddCraft(typeof(RanchDeed), "Planos Construccion", "RanchDeed", 60.0, 100.0, typeof(BlankScroll), 1044377, 150, 1044378);
            this.AddRes(index, typeof(Leather), 1044462, 8, 1044463);
            index = this.AddCraft(typeof(RanchExtensionDeed), "Planos Construccion", "RanchExtensionDeed", 60.0, 100.0, typeof(BlankScroll), 1044377, 150, 1044378);
  







            #endregion

            #region OS-Edit for SA items
            if (Core.SA)
            {
                index = this.AddCraft(typeof(GargoyleBook100), 1044294, 1113290, 60.0, 100.0, typeof(BlankScroll), 1044377, 40, 1044378);
                this.AddRes(index, typeof(Beeswax), 1025154, 2, "You do not have enough beeswax.");
                
                index = this.AddCraft(typeof(GargoyleBook200), 1044294, 1113291, 72.0, 100.0, typeof(BlankScroll), 1044377, 40, 1044378);
                this.AddRes(index, typeof(Beeswax), 1025154, 4, "You do not have enough beeswax.");

                index = AddCraft(typeof(ScrollBinderDeed), 1044294, ("Scroll Binder"), 75.0, 100.0, typeof(WoodPulp), ("Wood Pulp"), 1, ("You do not have enough Wood Pulp")); //Todo check Clilocs
            }
            #endregion

            this.MarkOption = true;
        }
    }
}