/***************************************************************************
 *   Based off the RunUO Skills system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Targeting;

namespace Server.Commands
{
    public class LokaiSkillsCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("SetLokaiSkill", AccessLevel.GameMaster, new CommandEventHandler(SetLokaiSkill_OnCommand));
            CommandSystem.Register("GetLokaiSkill", AccessLevel.GameMaster, new CommandEventHandler(GetLokaiSkill_OnCommand));
            CommandSystem.Register("SetAllLokaiSkills", AccessLevel.GameMaster, new CommandEventHandler(SetAllLokaiSkills_OnCommand));
            CommandSystem.Register("LokaiSkills", AccessLevel.Counselor, new CommandEventHandler(LokaiSkills_OnCommand));

            CommandSystem.Register("SetAb", AccessLevel.GameMaster, new CommandEventHandler(SetLokaiSkill_OnCommand));
            CommandSystem.Register("GetAb", AccessLevel.GameMaster, new CommandEventHandler(GetLokaiSkill_OnCommand));
            CommandSystem.Register("SetAllAbs", AccessLevel.GameMaster, new CommandEventHandler(SetAllLokaiSkills_OnCommand));
            CommandSystem.Register("Abs", AccessLevel.Counselor, new CommandEventHandler(LokaiSkills_OnCommand));
        }

        private class LokaiSkillsTarget : Target
        {
            public LokaiSkillsTarget()
                : base(-1, true, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                    from.SendGump(new LokaiSkillsGump(from, (Mobile)o));
            }
        }

        [Usage("LokaiSkills")]
        [Aliases("Abs")]
        [Description("Opens a menu where you can view or edit Lokai Skills of a targeted mobile.")]
        public static void LokaiSkills_OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new LokaiSkillsTarget();
        }

        [Usage("SetLokaiSkill <name> <value>")]
        [Aliases("SetAb")]
        [Description("Sets a Lokai Skill value (by Lokai Skill name) of a targeted mobile.")]
        public static void SetLokaiSkill_OnCommand(CommandEventArgs arg)
        {
            if (arg.Length != 2)
            {
                arg.Mobile.SendMessage("SetLokaiSkill <Lokai Skill name> <value>");
            }
            else
            {
                LokaiSkillName lokaiSkill;
                try
                {
                    lokaiSkill = (LokaiSkillName)Enum.Parse(typeof(LokaiSkillName), arg.GetString(0), true);
                }
                catch
                {
                    arg.Mobile.SendMessage("You have specified an invalid Lokai Skill to set.");
                    return;
                }
                arg.Mobile.Target = new LokaiSkillTarget(lokaiSkill, arg.GetDouble(1));
            }
        }

        [Usage("SetAllLokaiSkills <name> <value>")]
        [Aliases("SetAllAbs")]
        [Description("Sets all Lokai Skill values of a targeted mobile.")]
        public static void SetAllLokaiSkills_OnCommand(CommandEventArgs arg)
        {
            if (arg.Length != 1)
            {
                arg.Mobile.SendMessage("SetAllLokaiSkills <value>");
            }
            else
            {
                arg.Mobile.Target = new AllLokaiSkillsTarget(arg.GetDouble(0));
            }
        }

        [Usage("GetLokaiSkill <name>")]
        [Aliases("GetAb")]
        [Description("Gets a Lokai Skill value (by Lokai Skill name) of a targeted mobile.")]
        public static void GetLokaiSkill_OnCommand(CommandEventArgs arg)
        {
            if (arg.Length != 1)
            {
                arg.Mobile.SendMessage("GetLokaiSkill <Lokai Skill name>");
            }
            else
            {
                LokaiSkillName lokaiSkill;
                try
                {
                    lokaiSkill = (LokaiSkillName)Enum.Parse(typeof(LokaiSkillName), arg.GetString(0), true);
                }
                catch
                {
                    arg.Mobile.SendMessage("You have specified an invalid Lokai Skill to set."); 
                    return;
                }

                arg.Mobile.Target = new LokaiSkillTarget(lokaiSkill);
            }
        }

        public class AllLokaiSkillsTarget : Target
        {
            private double m_Value;

            public AllLokaiSkillsTarget(double value)
                : base(-1, false, TargetFlags.None)
            {
                m_Value = value;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Mobile)
                {
                    Mobile targ = (Mobile)targeted;
                    LokaiSkills lokaiSkills = LokaiSkillUtilities.XMLGetSkills(targ);

                    for (int i = 0; i < lokaiSkills.Length; ++i)
                        lokaiSkills[i].Base = m_Value;

                    CommandLogging.LogChangeProperty(from, targ, "EveryLokaiSkill.Base", m_Value.ToString());
                }
                else
                {
                    from.SendMessage("That does not have Lokai Skills!");
                }
            }
        }

        public class LokaiSkillTarget : Target
        {
            private bool m_Set;
            private LokaiSkillName m_LokaiSkill;
            private double m_Value;

            public LokaiSkillTarget(LokaiSkillName lokaiSkill, double value)
                : base(-1, false, TargetFlags.None)
            {
                m_Set = true;
                m_LokaiSkill = lokaiSkill;
                m_Value = value;
            }

            public LokaiSkillTarget(LokaiSkillName lokaiSkill)
                : base(-1, false, TargetFlags.None)
            {
                m_Set = false;
                m_LokaiSkill = lokaiSkill;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Mobile)
                {
                    Mobile targ = (Mobile)targeted;
                    LokaiSkill lokaiSkill = LokaiSkillUtilities.XMLGetSkills(targ)[m_LokaiSkill];

                    if (lokaiSkill == null)
                        return;

                    if (m_Set)
                    {
                        lokaiSkill.Base = m_Value;
                        CommandLogging.LogChangeProperty(from, targ, String.Format("{0}.Base", m_LokaiSkill), m_Value.ToString());
                    }

                    from.SendMessage("{0} : {1} (Base: {2})", m_LokaiSkill, lokaiSkill.Value, lokaiSkill.Base);
                }
                else
                {
                    from.SendMessage("That does not have Lokai Skills!");
                }
            }
        }
    }
}