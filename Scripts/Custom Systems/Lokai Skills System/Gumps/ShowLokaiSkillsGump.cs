/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
    public class NewSkillsCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("ShowLokaiSkills", AccessLevel.Player, new CommandEventHandler(ShowLokaiSkills_OnCommand));
            CommandSystem.Register("ShowSkills", AccessLevel.Player, new CommandEventHandler(ShowLokaiSkills_OnCommand));
        }

        [Usage("ShowLokaiSkills")]
        [Aliases("ShowSkills")]
        [Description("Shows a Player's LokaiSkills.")]
        private static void ShowLokaiSkills_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new ShowLokaiSkillsGump(e.Mobile));
        }
    }

    public class ShowLokaiSkillsGump : Gump
    {
        private Mobile m_Mobile;
        private bool m_Value;

        public ShowLokaiSkillsGump(Mobile mobile)
            : this(mobile, true)
        {
        }

        public string Color(string text, int color)
        {
            return String.Format("<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text);
        }

        public ShowLokaiSkillsGump(Mobile mobile, bool value)
            : base(0, 0)
        {
            m_Mobile = mobile;
            m_Value = value;
            bool GM = mobile.AccessLevel >= AccessLevel.GameMaster;
            LokaiSkills skils = LokaiSkillUtilities.XMLGetSkills(m_Mobile);
            m_Mobile.CloseGump(typeof(ShowLokaiSkillsGump));
            AddPage(0);
            AddBackground(564, 17, 220, 574, 9200);
            AddBackground(592, 21, 189, 537, 9300);

            AddButton(590, 560, value ? 0x25FF : 0x2602, value ? 0x2602 : 0x25FF, 19, GumpButtonType.Reply, 0);
            AddButton(680, 560, value ? 0x2602 : 0x25FF, value ? 0x25FF : 0x2602, 29, GumpButtonType.Reply, 0);
            AddHtml(621, 567, 55, 20, Color("BASE", value ? 0x20 : 0x777), false, false);
            AddHtml(711, 567, 55, 20, Color("VALUE", value ? 0x777 : 0x20), false, false);

            int y = 32;
            int hue = 0;
            for (int x = 0; x < LokaiSkillInfo.Table.Length; x++)
            {
                if (LokaiSkillUtilities.ShowLokaiSkill(x))
                {
                    if (LokaiSkillInfo.Table[x].ClickToUse || LokaiSkillInfo.Table[x].Callback != null)
                    {
                        AddButton(573, y + 3, 1210, 1209, x + 100, GumpButtonType.Reply, 0);
                        AddButton(555, y + 7, 2101, 2101, x + 200, GumpButtonType.Reply, 0);
                    }
                    if (GM) AddButton(761, y + 2, 5401, 5401, x + 300, GumpButtonType.Reply, 0);
                    hue = 0;
                }
                else
                {
                    if (GM) AddButton(761, y + 2, 5402, 5402, x + 300, GumpButtonType.Reply, 0);
                    hue = 1152;
                }
                string skill = value ? skils[x].Value.ToString("F1") : skils[x].Base.ToString("F1");
                if (LokaiSkillUtilities.ShowLokaiSkill(x) || GM) AddLabel(647, y, hue, LokaiSkillInfo.Table[x].Name);
                if (LokaiSkillUtilities.ShowLokaiSkill(x) || GM) AddLabel(602, y, hue, skill);
                y += 17;
            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile m = state.Mobile;
            if (info.ButtonID > 299)
            {
                int change = info.ButtonID - 300;
                LokaiSkillUtilities.ChangeShowLokaiSkill(change);
                m.SendGump(new ShowLokaiSkillsGump(m, m_Value));
            }
            else if (info.ButtonID > 199)
            {
                LokaiSkillName showAb = (LokaiSkillName)(info.ButtonID - 200);
                LokaiSkill lokaiSkill = LokaiSkillUtilities.XMLGetSkills(m)[showAb];
                int offset = ((int)showAb * 17) - 9;
                switch (showAb)
                {
                    case LokaiSkillName.Butchering: m.SendGump(new ShowButcheringGump(m, lokaiSkill, offset)); break;
                    case LokaiSkillName.Skinning: m.SendGump(new ShowSkinningGump(m, lokaiSkill, offset)); break;
                    case LokaiSkillName.DetectEvil: m.SendGump(new ShowDetectEvilGump(m, lokaiSkill, offset)); break;
                    case LokaiSkillName.CureDisease: m.SendGump(new ShowCureDiseaseGump(m, lokaiSkill, offset)); break;
                    case LokaiSkillName.PickPocket: m.SendGump(new ShowPickPocketGump(m, lokaiSkill, offset)); break;
                    case LokaiSkillName.Pilfering: m.SendGump(new ShowPilferingGump(m, lokaiSkill, offset)); break;
                    case LokaiSkillName.Ventriloquism: m.SendGump(new ShowVentriloquismGump(m, lokaiSkill, offset)); break;
                    case LokaiSkillName.Hypnotism: m.SendGump(new ShowHypnotismGump(m, lokaiSkill, offset)); break;
                    case LokaiSkillName.SpeakToAnimals: m.SendGump(new ShowSpeakToAnimalsGump(m, lokaiSkill, offset)); break;
                    case LokaiSkillName.Brewing: m.SendGump(new ShowBrewingGump(m, lokaiSkill, offset)); break;
                    case LokaiSkillName.Commerce: m.SendGump(new ShowCommerceGump(m, lokaiSkill, offset)); break;
                    case LokaiSkillName.Herblore: m.SendGump(new ShowHerbloreGump(m, lokaiSkill, offset)); break;
                }
                m.SendGump(new ShowLokaiSkillsGump(m, m_Value));         
            }
            else if (info.ButtonID > 99)
            {
                int useSkill = info.ButtonID - 100;
                if (m.Spell == null && !m.Meditating)
                {
                    if (LokaiSkillInfo.Table[useSkill].Callback != null)
                    {
                        if (m.NextSkillTime <= Core.TickCount || (LokaiSkillName)useSkill == LokaiSkillName.SpeakToAnimals)
                            m.NextSkillTime = Core.TickCount + (int)LokaiSkillInfo.Table[useSkill].Callback(m).TotalSeconds;
                        else
                            m.SendMessage("You must wait to use another skill.");
                    }
                    else
                        m.SendMessage("That skill is not yet active.");
                }
                else
                    m.SendMessage("You are too busy to use that skill now.");

                m.SendGump(new ShowLokaiSkillsGump(m, m_Value));
            }
            else if (info.ButtonID == 19)
            {
                m.SendGump(new ShowLokaiSkillsGump(m, false));
            }
            else if (info.ButtonID == 29)
            {
                m.SendGump(new ShowLokaiSkillsGump(m, true));
            }
        }
    }

    public class ShowButcheringGump : ShowLokaiSkillGump
    { public ShowButcheringGump(Mobile mobile, LokaiSkill lokaiSkill, int offset) : base(mobile, lokaiSkill, offset) { } }

    public class ShowSkinningGump : ShowLokaiSkillGump
    { public ShowSkinningGump(Mobile mobile, LokaiSkill lokaiSkill, int offset) : base(mobile, lokaiSkill, offset) { } }

    public class ShowDetectEvilGump : ShowLokaiSkillGump
    { public ShowDetectEvilGump(Mobile mobile, LokaiSkill lokaiSkill, int offset) : base(mobile, lokaiSkill, offset) { } }

    public class ShowCureDiseaseGump : ShowLokaiSkillGump
    { public ShowCureDiseaseGump(Mobile mobile, LokaiSkill lokaiSkill, int offset) : base(mobile, lokaiSkill, offset) { } }

    public class ShowPickPocketGump : ShowLokaiSkillGump
    { public ShowPickPocketGump(Mobile mobile, LokaiSkill lokaiSkill, int offset) : base(mobile, lokaiSkill, offset) { } }

    public class ShowPilferingGump : ShowLokaiSkillGump
    { public ShowPilferingGump(Mobile mobile, LokaiSkill lokaiSkill, int offset) : base(mobile, lokaiSkill, offset) { } }

    public class ShowVentriloquismGump : ShowLokaiSkillGump
    { public ShowVentriloquismGump(Mobile mobile, LokaiSkill lokaiSkill, int offset) : base(mobile, lokaiSkill, offset) { } }

    public class ShowHypnotismGump : ShowLokaiSkillGump
    { public ShowHypnotismGump(Mobile mobile, LokaiSkill lokaiSkill, int offset) : base(mobile, lokaiSkill, offset) { } }

    public class ShowSpeakToAnimalsGump : ShowLokaiSkillGump
    { public ShowSpeakToAnimalsGump(Mobile mobile, LokaiSkill lokaiSkill, int offset) : base(mobile, lokaiSkill, offset) { } }

    public class ShowCommerceGump : ShowLokaiSkillGump
    { public ShowCommerceGump(Mobile mobile, LokaiSkill lokaiSkill, int offset) : base(mobile, lokaiSkill, offset) { } }

    public class ShowBrewingGump : ShowLokaiSkillGump
    { public ShowBrewingGump(Mobile mobile, LokaiSkill lokaiSkill, int offset) : base(mobile, lokaiSkill, offset) { } }

    public class ShowHerbloreGump : ShowLokaiSkillGump
    { public ShowHerbloreGump(Mobile mobile, LokaiSkill lokaiSkill, int offset) : base(mobile, lokaiSkill, offset) { } }

    public class ShowLokaiSkillGump : Gump
    {
        private Mobile m_Mobile;
        private LokaiSkill m_LokaiSkill;

        public ShowLokaiSkillGump(Mobile mobile, LokaiSkill lokaiSkill, int offset)
            : base(0, 0)
        {
            m_Mobile = mobile;
            m_LokaiSkill = lokaiSkill;
            AddPage(0);
            AddBackground(420, 23 + offset, 128, 38, 9300);
            AddLabel(436, 31 + offset, 0, m_LokaiSkill.Name);
            AddButton(422, 36 + offset, 2104, 2103, m_LokaiSkill.LokaiSkillID + 100, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile m = state.Mobile;
            if (m.Spell == null && !m.Meditating)
            {
                int useSkill = info.ButtonID - 100;
                if (useSkill >= 0 && useSkill <= 29)
                {
                    if (LokaiSkillInfo.Table[useSkill].Callback != null)
                    {
                        m.NextSkillTime = Core.TickCount + (int)LokaiSkillInfo.Table[useSkill].Callback(m).TotalSeconds;
                        m.SendGump(new ShowLokaiSkillGump(m, m_LokaiSkill, this.Y));
                    }
                    else
                    {
                        m.SendMessage("That skill is not yet active.");
                        m.SendGump(new ShowLokaiSkillGump(m, m_LokaiSkill, this.Y));
                    }
                }
                //else closegump
            }
            else
            {
                m.SendMessage("You are too busy to use that skill now.");
                m.SendGump(new ShowLokaiSkillGump(m, m_LokaiSkill, this.Y));
            }

        }
    }
}