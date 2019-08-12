/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server;
using Server.Network;
using Server.Engines.Build;

namespace Server.Items
{
    public abstract class BaseBuildingTool : Item, IBuildable
    {
        private Mobile m_Builder;
        private ToolQuality m_Quality;
        private int m_UsesRemaining;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Builder
        {
            get { return m_Builder; }
            set { m_Builder = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public ToolQuality Quality
        {
            get { return m_Quality; }
            set { UnscaleUses(); m_Quality = value; InvalidateProperties(); ScaleUses(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int UsesRemaining
        {
            get { return m_UsesRemaining; }
            set { m_UsesRemaining = value; InvalidateProperties(); }
        }

        public void ScaleUses()
        {
            m_UsesRemaining = (m_UsesRemaining * GetUsesScalar()) / 100;
            InvalidateProperties();
        }

        public void UnscaleUses()
        {
            m_UsesRemaining = (m_UsesRemaining * 100) / GetUsesScalar();
        }

        public int GetUsesScalar()
        {
            if (m_Quality == ToolQuality.Exceptional)
                return 200;

            return 100;
        }

        public bool ShowUsesRemaining { get { return true; } set { } }

        public abstract BuildSystem BuildSystem { get; }

        public BaseBuildingTool(int itemID)
            : this(Utility.RandomMinMax(25, 75), itemID)
        {
        }

        public BaseBuildingTool(int uses, int itemID)
            : base(itemID)
        {
            m_UsesRemaining = uses;
            m_Quality = ToolQuality.Regular;
        }

        public BaseBuildingTool(Serial serial)
            : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (m_Quality == ToolQuality.Exceptional)
                list.Add(1060636); // exceptional

            list.Add(1060584, m_UsesRemaining.ToString()); // uses remaining: ~1_val~
        }

        public virtual void DisplayDurlokaiSkillTo(Mobile m)
        {
            LabelToAffix(m, 1017323, AffixType.Append, ": " + m_UsesRemaining.ToString()); // DurlokaiSkill
        }

        public static bool CheckAccessible(Item tool, Mobile m)
        {
            return (tool.IsChildOf(m) || tool.Parent == m);
        }

        public static bool CheckTool(Item tool, Mobile m)
        {
            Item check = m.FindItemOnLayer(Layer.OneHanded);

            if (check is BaseBuildingTool && check != tool)
                return false;

            check = m.FindItemOnLayer(Layer.TwoHanded);

            if (check is BaseBuildingTool && check != tool)
                return false;

            return true;
        }

        public override void OnSingleClick(Mobile from)
        {
            DisplayDurlokaiSkillTo(from);

            base.OnSingleClick(from);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack) || Parent == from)
            {
                BuildSystem system = this.BuildSystem;

                int num = system.CanBuild(from, this, null);

                if (num > 0)
                {
                    from.SendLocalizedMessage(num);
                }
                else
                {
                    BuildContext context = system.GetContext(from);

                    from.SendGump(new BuildGump(from, system, this, null));
                }
            }
            else
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version

            writer.Write((Mobile)m_Builder);
            writer.Write((int)m_Quality);

            writer.Write((int)m_UsesRemaining);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        m_Builder = reader.ReadMobile();
                        m_Quality = (ToolQuality)reader.ReadInt();
                        goto case 0;
                    }
                case 0:
                    {
                        m_UsesRemaining = reader.ReadInt();
                        break;
                    }
            }
        }
        #region IBuildable Members

        public int OnBuild(int quality, bool makersMark, Mobile from, BuildSystem buildSystem, Type typeRes, BaseBuildingTool tool, BuildItem buildItem, int resHue)
        {
            Quality = (ToolQuality)quality;

            if (makersMark)
                Builder = from;

            return quality;
        }

        #endregion
    }
}