/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using Server.Misc;
using Server.Gumps;
using Server.Multis.Deeds;
using Server.Mobiles;
using Server.Commands;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
    public class HouseRecipes
    {
        public class HouseRecipeGump : Gump
        {
            private HouseRecipe m_Recipe;

            public HouseRecipeGump(HouseRecipe recipe, Mobile from)
                : base(30, 30)
            {
                m_Recipe = recipe;
                AddBackground(30, 30, 600, 400, 9390);
                AddLabel(240, 60, 777, recipe.Name);
                AddLabel(90, 100, 0, "Resources");
                AddLabel(310, 100, 0, "Required");
                AddLabel(400, 100, 0, "Fulfilled");
                AddLabel(490, 100, 0, "Needed");
                for (int y = 140; y <= 360; y = y + 20)
                {
                    AddLabel(90, y, 0, recipe.Resources[(y / 20) - 7].Name);
                    AddLabel(240, y, 0, "................");
                    AddLabel(310, y, 0, recipe.Quantities[(y / 20) - 7].ToString());
                    AddLabel(400, y, 0, recipe.Provided[(y / 20) - 7].ToString());
                    AddLabel(490, y, 0, (recipe.Quantities[(y / 20) - 7] - recipe.Provided[(y / 20) - 7]).ToString());
                    if (recipe.Provided[(y / 20) - 7] >= recipe.Quantities[(y / 20) - 7])
                        AddImage(57, y - 2, 1154);
                }
                AddLabel(60, 405, 0, "Add Resource");
                AddButton(155, 405, 5541, 5542, 1, GumpButtonType.Reply, 0);
                AddLabel(230, 405, 0, "Show Recipe to Foreman");
                AddButton(390, 405, 5541, 5542, 2, GumpButtonType.Reply, 0);
                if (from.AccessLevel >= AccessLevel.GameMaster)
                {
                    AddLabel(460, 405, 777, "Fill Recipe");
                    AddButton(530, 405, 5541, 5542, 4, GumpButtonType.Reply, 0);
                }
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                Mobile m = sender.Mobile;
                if (info.ButtonID == 1)
                {
                    m.SendMessage("What Resource would you like to add?");
                    m.Target = new AddResource(m_Recipe);
                }
                if (info.ButtonID == 2)
                {
                    m.SendMessage("Select the Foreman.");
                    m.Target = new SelectForeman(m_Recipe);
                }
                else if (info.ButtonID == 4)
                {
                    m_Recipe.FillRecipe();
                    m.SendGump(new HouseRecipeGump(m_Recipe, m));
                }
            }

            private class AddResource : Target
            {
                private HouseRecipe hr;

                public AddResource(HouseRecipe recipe)
                    : base(3, false, TargetFlags.None)
                {
                    hr = recipe;
                }

                protected override void OnTarget(Mobile from, object targeted)
                {
                    if (targeted is ResourceItem)
                    {
                        ResourceItem item = targeted as ResourceItem;
                        if (hr.IsNeeded(item))
                        {
                            LokaiSkill lokaiSkill = (LokaiSkillUtilities.XMLGetSkills(from)).Construction;

                            SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, lokaiSkill, 0.0, 100.0);

                            if (rating >= SuccessRating.PartialSuccess)
                            {
                                from.SendMessage("You successfully added the Resource.");
                                hr.AddResource(item);
                            }
                            else
                            {
                                from.SendMessage("You failed to add the Resource.");
                                switch (rating)
                                {
                                    case SuccessRating.HazzardousFailure: { break; }//TODO: take damage or lose resource
                                    case SuccessRating.CriticalFailure: { break; }//TODO: lose the resource and take damage
                                    case SuccessRating.TooDifficult: { break; }//TODO: "you have no idea..."
                                    default: { break; }
                                }
                            }
                        }
                        else
                            from.SendMessage("That resource is not needed for this house.");
                    }
                    else
                        from.SendMessage("That is not a Resource Item!");
                    from.SendGump(new HouseRecipeGump(hr, from));
                }
            }

            private class SelectForeman : Target
            {
                private HouseRecipe hr;

                public SelectForeman(HouseRecipe recipe)
                    : base(3, false, TargetFlags.None)
                {
                    hr = recipe;
                }

                protected override void OnTarget(Mobile from, object targeted)
                {
                    if (targeted is Foreman)
                    {
                        Foreman foreman = targeted as Foreman;
                        if (hr.IsFilled())
                        {
                            foreman.Say("This house is ready to go. Here you are!");
                            hr.GiveDeedTo(from);
                        }
                        else
                        {
                            foreman.Say("OK. Select the Resource Item you want to give me.");
                            from.Target = new SelectResource(hr, foreman);
                        }
                    }
                    else
                        from.SendMessage("That is not a Foreman!");
                }
            }

            private class SelectResource : Target
            {
                private HouseRecipe hr;
                private Foreman fm;

                public SelectResource(HouseRecipe recipe, Foreman foreman)
                    : base(3, false, TargetFlags.None)
                {
                    hr = recipe;
                    fm = foreman;
                }

                protected override void OnTarget(Mobile from, object targeted)
                {
                    if (targeted is ResourceItem)
                    {
                        ResourceItem item = targeted as ResourceItem;
                        if (hr.IsNeeded(item))
                        {
                            fm.Say("I will give that to the workers.");
                            hr.AddResource(item);
                        }
                        else
                            fm.Say("That resource is not needed for this house.");
                    }
                    else
                        fm.Say("That is not a Resource Item!");
                    from.SendGump(new HouseRecipeGump(hr, from));
                }
            }
        }

        public class HouseRecipe : Item
        {
            private Type m_House;
            public Type House { get { return m_House; } set { m_House = value; } }

            private ResourceItem[] m_Resources;
            public ResourceItem[] Resources { get { return m_Resources; } set { m_Resources = value; } }

            private int[] m_Quantities;
            public int[] Quantities { get { return m_Quantities; } set { m_Quantities = value; } }

            private int[] m_Provided;
            public int[] Provided { get { return m_Provided; } set { m_Provided = value; } }

            public bool IsNeeded(ResourceItem item)
            {
                for (int x = 0; x < m_Resources.Length; x++)
                {
                    if (item.Name == m_Resources[x].Name)
                    {
                        if (m_Quantities[x] > m_Provided[x])
                            return true;
                    }
                }
                return false;
            }

            public void GiveDeedTo(Mobile mobile)
            {
                HouseDeed deed = Activator.CreateInstance(House) as HouseDeed;
                mobile.AddToBackpack(deed);
                this.Delete();
            }

            public void AddResource(ResourceItem item)
            {
                for (int x = 0; x < m_Resources.Length; x++)
                {
                    if (item.Name == m_Resources[x].Name)
                    {
                        if (m_Quantities[x] > m_Provided[x])
                        {
                            if (item.Stackable)
                            {
                                m_Provided[x] += item.Amount;
                                item.Delete();
                            }
                            else
                            {
                                m_Provided[x]++;
                                item.Delete();
                            }
                        }
                    }
                }
            }

            public bool IsFilled()
            {
                for (int x = 0; x < m_Resources.Length; x++)
                {
                    if (!(m_Provided[x] == m_Quantities[x]))
                        return false;
                }
                return true;
            }

            public void FillRecipe()
            {
                for (int x = 0; x < m_Resources.Length; x++)
                {
                    m_Provided[x] = m_Quantities[x];
                }
            }

            public override void OnDoubleClick(Mobile from)
            {
                from.SendGump(new HouseRecipeGump(this, from));
            }

            public HouseRecipe()
                : base(0x14F0)
            {
                Hue = 39;
            }

            public HouseRecipe(Serial serial)
                : base(serial)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write((int)0); // version
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);

                int version = reader.ReadInt();
            }
        }

        public class StonePlasterHouseRecipe : HouseRecipe
        {
            [Constructable]
            public StonePlasterHouseRecipe()
                : base()
            {
                House = typeof(StonePlasterHouseDeed);
                Name = "Stone and Plaster House Recipe";
                Resources = new ResourceItem[] {
                    new StoneSlab(),
                    new WoodDoor(),
                    new StoneWall(),
                    new StoneWindow(),
                    new StoneFoundation(),
                    new StoneStair(),
                    new TileRoofing(),
                    new WoodFlooring(),
                    new CementSupply(),
                    new JointSupply(),
                    new HingeSupply(),
                    new PaintSupply() };

                Quantities = new int[] { 4, 2, 11, 2, 20, 3, 900, 36, 50, 30, 6, 50 };
                Provided = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            }

            public StonePlasterHouseRecipe(Serial serial)
                : base(serial)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write((int)0); // version
                for (int x = 0; x < 12; x++)
                {
                    writer.Write(Provided[x]);
                }
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                int version = reader.ReadInt();

                House = typeof(StonePlasterHouseDeed);
                Name = "Stone and Plaster House Recipe";
                Resources = new ResourceItem[] {
                    new StoneSlab(),
                    new WoodDoor(),
                    new StoneWall(),
                    new StoneWindow(),
                    new StoneFoundation(),
                    new StoneStair(),
                    new TileRoofing(),
                    new WoodFlooring(),
                    new CementSupply(),
                    new JointSupply(),
                    new HingeSupply(),
                    new PaintSupply() };

                Quantities = new int[] { 4, 2, 11, 2, 20, 3, 900, 36, 50, 30, 6, 50 };
                Provided = new int[12];
                for (int x = 0; x < 12; x++)
                {
                    Provided[x] = reader.ReadInt();
                }
            }
        }

        public class FieldStoneHouseRecipe : HouseRecipe
        {
            [Constructable]
            public FieldStoneHouseRecipe()
                : base()
            {
                House = typeof(FieldStoneHouseDeed);
                Name = "Field Stone House Recipe";
                Resources = new ResourceItem[] {
                    new StoneSlab(),
                    new WoodDoor(),
                    new StoneWall(),
                    new StoneWindow(),
                    new StoneFoundation(),
                    new StoneStair(),
                    new ShingleRoofing(),
                    new WoodFlooring(),
                    new CementSupply(),
                    new JointSupply(),
                    new HingeSupply(),
                    new PaintSupply() };

                Quantities = new int[] { 4, 2, 11, 2, 20, 3, 400, 36, 50, 30, 6, 50 };
                Provided = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }

            public FieldStoneHouseRecipe(Serial serial)
                : base(serial)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write((int)0); // version
                for (int x = 0; x < 12; x++)
                {
                    writer.Write(Provided[x]);
                }
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                int version = reader.ReadInt();

                House = typeof(FieldStoneHouseDeed);
                Name = "Field Stone House Recipe";
                Resources = new ResourceItem[] {
                    new StoneSlab(),
                    new WoodDoor(),
                    new StoneWall(),
                    new StoneWindow(),
                    new StoneFoundation(),
                    new StoneStair(),
                    new ShingleRoofing(),
                    new WoodFlooring(),
                    new CementSupply(),
                    new JointSupply(),
                    new HingeSupply(),
                    new PaintSupply() };

                Quantities = new int[] { 4, 2, 11, 2, 20, 3, 400, 36, 50, 30, 6, 50 };
                Provided = new int[12];
                for (int x = 0; x < 12; x++)
                {
                    Provided[x] = reader.ReadInt();
                }
            }
        }

        public class SmallBrickHouseRecipe : HouseRecipe
        {
            [Constructable]
            public SmallBrickHouseRecipe()
                : base()
            {
                House = typeof(SmallBrickHouseDeed);
                Name = "Small Brick House Recipe";
                Resources = new ResourceItem[] {
                    new BrickPanel(),
                    new WoodDoor(),
                    new BrickWall(),
                    new BrickWindow(),
                    new BrickFoundation(),
                    new StoneStair(),
                    new ShingleRoofing(),
                    new WoodFlooring(),
                    new MortarSupply(),
                    new JointSupply(),
                    new HingeSupply(),
                    new PaintSupply() };

                Quantities = new int[] { 4, 2, 11, 2, 20, 3, 400, 36, 50, 30, 6, 50 };
                Provided = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }

            public SmallBrickHouseRecipe(Serial serial)
                : base(serial)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write((int)0); // version
                for (int x = 0; x < 12; x++)
                {
                    writer.Write(Provided[x]);
                }
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                int version = reader.ReadInt();

                House = typeof(SmallBrickHouseDeed);
                Name = "Small Brick House Recipe";
                Resources = new ResourceItem[] {
                    new BrickPanel(),
                    new WoodDoor(),
                    new BrickWall(),
                    new BrickWindow(),
                    new BrickFoundation(),
                    new StoneStair(),
                    new ShingleRoofing(),
                    new WoodFlooring(),
                    new MortarSupply(),
                    new JointSupply(),
                    new HingeSupply(),
                    new PaintSupply() };

                Quantities = new int[] { 4, 2, 11, 2, 20, 3, 400, 36, 50, 30, 6, 50 };
                Provided = new int[12];
                for (int x = 0; x < 12; x++)
                {
                    Provided[x] = reader.ReadInt();
                }
            }
        }

        public class WoodHouseRecipe : HouseRecipe
        {
            [Constructable]
            public WoodHouseRecipe()
                : base()
            {
                House = typeof(WoodHouseDeed);
                Name = "Wooden House Recipe";
                Resources = new ResourceItem[] {
                    new WoodPanel(),
                    new WoodDoor(),
                    new WoodWall(),
                    new WoodWindow(),
                    new BrickFoundation(),
                    new StoneStair(),
                    new ShingleRoofing(),
                    new WoodFlooring(),
                    new NailSupply(),
                    new JointSupply(),
                    new HingeSupply(),
                    new StainSupply() };

                Quantities = new int[] { 4, 2, 11, 2, 20, 3, 400, 36, 200, 30, 6, 50 };
                Provided = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }

            public WoodHouseRecipe(Serial serial)
                : base(serial)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write((int)0); // version
                for (int x = 0; x < 12; x++)
                {
                    writer.Write(Provided[x]);
                }
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                int version = reader.ReadInt();

                House = typeof(WoodHouseDeed);
                Name = "Wooden House Recipe";
                Resources = new ResourceItem[] {
                    new WoodPanel(),
                    new WoodDoor(),
                    new WoodWall(),
                    new WoodWindow(),
                    new BrickFoundation(),
                    new StoneStair(),
                    new ShingleRoofing(),
                    new WoodFlooring(),
                    new NailSupply(),
                    new JointSupply(),
                    new HingeSupply(),
                    new StainSupply() };

                Quantities = new int[] { 4, 2, 11, 2, 20, 3, 400, 36, 200, 30, 6, 50 };
                Provided = new int[12];
                for (int x = 0; x < 12; x++)
                {
                    Provided[x] = reader.ReadInt();
                }
            }
        }

        public class WoodPlasterHouseRecipe : HouseRecipe
        {
            [Constructable]
            public WoodPlasterHouseRecipe()
                : base()
            {
                House = typeof(WoodPlasterHouseDeed);
                Name = "Wood and Plaster House Recipe";
                Resources = new ResourceItem[] {
                    new WoodPanel(),
                    new WoodDoor(),
                    new WoodWall(),
                    new WoodWindow(),
                    new BrickFoundation(),
                    new StoneStair(),
                    new ShingleRoofing(),
                    new WoodFlooring(),
                    new NailSupply(),
                    new JointSupply(),
                    new HingeSupply(),
                    new PaintSupply() };

                Quantities = new int[] { 4, 2, 11, 2, 20, 3, 400, 36, 200, 30, 6, 50 };
                Provided = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }

            public WoodPlasterHouseRecipe(Serial serial)
                : base(serial)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write((int)0); // version
                for (int x = 0; x < 12; x++)
                {
                    writer.Write(Provided[x]);
                }
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                int version = reader.ReadInt();

                House = typeof(WoodPlasterHouseDeed);
                Name = "Wood and Plaster House Recipe";
                Resources = new ResourceItem[] {
                    new WoodPanel(),
                    new WoodDoor(),
                    new WoodWall(),
                    new WoodWindow(),
                    new BrickFoundation(),
                    new StoneStair(),
                    new ShingleRoofing(),
                    new WoodFlooring(),
                    new NailSupply(),
                    new JointSupply(),
                    new HingeSupply(),
                    new PaintSupply() };

                Quantities = new int[] { 4, 2, 11, 2, 20, 3, 400, 36, 200, 30, 6, 50 };
                Provided = new int[12];
                for (int x = 0; x < 12; x++)
                {
                    Provided[x] = reader.ReadInt();
                }
            }
        }

        public class ThatchedRoofCottageRecipe : HouseRecipe
        {
            [Constructable]
            public ThatchedRoofCottageRecipe()
                : base()
            {
                House = typeof(ThatchedRoofCottageDeed);
                Name = "Thatched Roof Cottage Recipe";
                Resources = new ResourceItem[] {
                    new WoodPanel(),
                    new WoodDoor(),
                    new WoodWall(),
                    new WoodWindow(),
                    new BrickFoundation(),
                    new StoneStair(),
                    new ThatchRoofing(),
                    new WoodFlooring(),
                    new NailSupply(),
                    new JointSupply(),
                    new HingeSupply(),
                    new StainSupply() };

                Quantities = new int[] { 4, 2, 11, 2, 20, 3, 400, 36, 200, 30, 6, 50 };
                Provided = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }

            public ThatchedRoofCottageRecipe(Serial serial)
                : base(serial)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write((int)0); // version
                for (int x = 0; x < 12; x++)
                {
                    writer.Write(Provided[x]);
                }
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                int version = reader.ReadInt();

                House = typeof(ThatchedRoofCottageDeed);
                Name = "Thatched Roof Cottage Recipe";
                Resources = new ResourceItem[] {
                    new WoodPanel(),
                    new WoodDoor(),
                    new WoodWall(),
                    new WoodWindow(),
                    new BrickFoundation(),
                    new StoneStair(),
                    new ThatchRoofing(),
                    new WoodFlooring(),
                    new NailSupply(),
                    new JointSupply(),
                    new HingeSupply(),
                    new StainSupply() };

                Quantities = new int[] { 4, 2, 11, 2, 20, 3, 400, 36, 200, 30, 6, 50 };
                Provided = new int[12];
                for (int x = 0; x < 12; x++)
                {
                    Provided[x] = reader.ReadInt();
                }
            }
        }
    }
}
