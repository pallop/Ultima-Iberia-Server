/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Text;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Targeting;
using Server.Engines.Harvest;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
    public class TreeHarvestTool : BaseHarvestTool
    {
        public override HarvestSystem HarvestSystem { get { return TreeHarvest.System; } }

        private Type m_Resource;

        private TreeResourceType m_ResourceType;
        public TreeResourceType ResourceType { get { return m_ResourceType; } set { m_ResourceType = value; } }

        public override string DefaultName { get { return "tree harvest tool"; } }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            list.Add(new ResourceEntry(from, this));
        }

        private class ResourceEntry : ContextMenuEntry
        {
            TreeHarvestTool m_Tool;
            Mobile m_From;

            public ResourceEntry(Mobile from, TreeHarvestTool tool)
                : base(5055)
            {
                m_From = from;
                m_Tool = tool;
            }

            public override void OnClick()
            {
                m_From.SendGump(new ResourceGump(m_From, m_Tool));
            }
        }

        [Constructable]
        public TreeHarvestTool()
            : this(500)
        {
        }

        [Constructable]
        public TreeHarvestTool(int uses)
            : base(uses, 0x2561)
        {
            Weight = 3.0;
        }

        public TreeHarvestTool(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((int)m_ResourceType);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_ResourceType = (TreeResourceType)reader.ReadInt();
        }
    }

    public class ResourceGump : Gump
    {
        TreeHarvestTool m_Tool;
        Mobile m_From;

        public ResourceGump(Mobile from, TreeHarvestTool tool)
            : base(0, 0)
        {

            m_From = from;
            m_Tool = tool;
            LokaiSkills skills = LokaiSkillUtilities.XMLGetSkills(from);
            LokaiSkillName skil = LokaiSkillName.TreePicking;
            string lokaiSkill = "Current Tree";
            switch (m_Tool.ResourceType)
            {
                case TreeResourceType.BarkSkin: lokaiSkill += " Carving Skill: "; skil = LokaiSkillName.TreeCarving; break;
                case TreeResourceType.FruitNut: lokaiSkill += " Picking Skill: "; skil = LokaiSkillName.TreePicking; break;
                case TreeResourceType.LeafSpine: lokaiSkill += " Picking Skill: "; skil = LokaiSkillName.TreePicking; break;
                case TreeResourceType.RootBranch: lokaiSkill += " Digging Skill: "; skil = LokaiSkillName.TreeDigging; break;
                case TreeResourceType.SapJuice: lokaiSkill += " Sapping Skill: "; skil = LokaiSkillName.TreeSapping; break;
            }
            lokaiSkill += skills[skil].Value.ToString("F1");

            m_From.CloseGump(typeof(ResourceGump));

            Closable = true;
            Dragable = true;
            Resizable = false;
            int bark = m_Tool.ResourceType == TreeResourceType.BarkSkin ? 2361 : 2360;
            int fruit = m_Tool.ResourceType == TreeResourceType.FruitNut ? 2361 : 2360;
            int leaf = m_Tool.ResourceType == TreeResourceType.LeafSpine ? 2361 : 2360;
            int root = m_Tool.ResourceType == TreeResourceType.RootBranch ? 2361 : 2360;
            int sap = m_Tool.ResourceType == TreeResourceType.SapJuice ? 2361 : 2360;
            AddPage(0);
            AddBackground(40, 40, 404, 445, 9250);
            AddImage(40, 40, from.Female ? 13 : 12, 146);
            AddImage(40, 40, 50981);
            AddImage(40, 40, 60479);
            AddImage(40, 40, 60517);
            AddImage(40, 40, 50617);
            AddImage(40, -30, 50650);
            AddImage(-3, 178, 50497);
            AddImage(95, 81, 11374);
            AddImage(51, 52, 11374);
            AddImage(55, 99, 11374);
            AddImage(56, 135, 11374);
            AddImage(61, 233, 11374);
            AddLabel(57, 57, 1378, @"A");
            AddLabel(99, 84, 1378, @"B");
            AddLabel(60, 102, 1378, @"C");
            AddLabel(61, 137, 1378, @"D");
            AddLabel(66, 236, 1378, @"E");
            AddLabel(70, 300, 1378, @"A");
            AddLabel(70, 330, 1378, @"B");
            AddLabel(70, 360, 1378, @"C");
            AddLabel(70, 390, 1378, @"D");
            AddLabel(70, 420, 1378, @"E");
            AddLabel(170, 54, 0, @"Tree Harvest Tool");
            AddLabel(88, 300, 0, @"Fruit Picking Tool: for harvesting fruit and nuts");
            AddLabel(88, 330, 0, @"Leaf Picking Tool: for harvesting leaves and spines");
            AddLabel(88, 360, 0, @"Carving Tool: for harvesting bark and skin");
            AddLabel(88, 390, 0, @"Sapping Tool: for harvesting sap and juice");
            AddLabel(88, 420, 0, @"Digging Tool: for harvesting roots and buried branches");
            AddLabel(55, 274, 0, lokaiSkill);
            AddButton(55, 304, fruit, 2362, (int)Buttons.FruitNut, GumpButtonType.Reply, 0);
            AddButton(55, 394, sap, 2362, (int)Buttons.SapJuice, GumpButtonType.Reply, 0);
            AddButton(55, 364, bark, 2362, (int)Buttons.BarkSkin, GumpButtonType.Reply, 0);
            AddButton(55, 334, leaf, 2362, (int)Buttons.LeafSpine, GumpButtonType.Reply, 0);
            AddButton(55, 424, root, 2362, (int)Buttons.RootBranch, GumpButtonType.Reply, 0);
            AddItem(300, 65, 0x0C96);
            AddItem(167, 201, 0x194F);
            AddItem(207, 211, 0x0993);
            AddItem(220, 75, 0x0D94);
            AddItem(220, 75, 0x0D96);
            AddItem(270, 145, 0x0CDA);
            AddItem(270, 145, 0x0CDC);
            AddButton(133, 447, 9904, 9905, (int)Buttons.Investigate, GumpButtonType.Reply, 0);
            AddLabel(159, 448, 0, @"Investigate Tree");

        }

        private enum Buttons { FruitNut = 10, SapJuice, BarkSkin, LeafSpine, RootBranch, Investigate }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Buttons choice = (Buttons)info.ButtonID;
            switch (choice)
            {
                case Buttons.BarkSkin: m_Tool.ResourceType = TreeResourceType.BarkSkin;
                    m_From.SendGump(new ResourceGump(m_From, m_Tool)); break;
                case Buttons.FruitNut: m_Tool.ResourceType = TreeResourceType.FruitNut;
                    m_From.SendGump(new ResourceGump(m_From, m_Tool)); break;
                case Buttons.LeafSpine: m_Tool.ResourceType = TreeResourceType.LeafSpine;
                    m_From.SendGump(new ResourceGump(m_From, m_Tool)); break;
                case Buttons.RootBranch: m_Tool.ResourceType = TreeResourceType.RootBranch;
                    m_From.SendGump(new ResourceGump(m_From, m_Tool)); break;
                case Buttons.SapJuice: m_Tool.ResourceType = TreeResourceType.SapJuice;
                    m_From.SendGump(new ResourceGump(m_From, m_Tool)); break;
                case Buttons.Investigate: m_From.Target = new InternalTarget();
                    m_From.SendGump(new ResourceGump(m_From, m_Tool)); break;
                default: break;
            }
        }

        private class InternalTarget : Target
        {
            public InternalTarget()
                : base(6, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                bool none = true;
                bool validtarget = false;
                int tileID = 0;

                if (targeted is Static && !((Static)targeted).Movable)
                {
                    Static obj = (Static)targeted;
                    tileID = (obj.ItemID & 0x3FFF) | 0x4000;
                    validtarget = true;
                }
                else if (targeted is StaticTarget)
                {
                    StaticTarget obj = (StaticTarget)targeted;
                    tileID = (obj.ItemID & 0x3FFF) | 0x4000;
                    validtarget = true;
                }

                if (validtarget)
                {                    
                    TreeResource[] resources = TreeHarvest.GetResources(tileID);
                    if (resources.Length > 0)
                    {
                        none = false;
                        StringBuilder sb = new StringBuilder("That contains the following resources: ");
                        sb.Append(resources[0].ToString());
                        if (resources.Length > 1)
                        {
                            for (int x = 1; x < resources.Length; x++)
                            {
                                sb.Append(", ");
                                sb.Append(resources[x].ToString());
                            }
                        }
                        from.SendMessage(sb.ToString());
                    }
                }

                if (none)
                {
                    from.SendMessage("There are no tree resources there.");
                }
            }
        }
    }
}