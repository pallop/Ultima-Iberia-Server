/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Multis;
using Server.Mobiles;
using System.Collections.Generic;

namespace Server.LokaiSkillHandlers
{
    public enum RidingRank
    {
        None            =   0,
        Pedestrian      =   10,
        Novice          =   20,
        Amateur         =   30,
        Intermediate    =   40,
        Able            =   50,
        Advanced        =   60,
        Equestrian      =   75,
        Master          =   90
    }

    public class Riding
    {
        private const double RIDESECONDS = 10.0;
        private const double FAILSECONDS = 6.0;
        private static bool RIDING_ACTIVE = true; //Set to 'false' to disable Riding checks

        private static Dictionary<Mobile, DateTime> m_RideTime;

        public static void Initialize()
        {
            EventSink.Movement += new MovementEventHandler(EventSink_RidingMovement);
            m_RideTime = new Dictionary<Mobile, DateTime>();
        }

        static void EventSink_RidingMovement(MovementEventArgs e)
        {
            if (RIDING_ACTIVE)
                if (e.Mobile is PlayerMobile && e.Mobile.Mounted && DesignContext.Find(e.Mobile) == null)
                {
                    if (e.Mobile.Mount is Item) RideEthereal(e.Mobile, (Item)e.Mobile.Mount);
                    if (e.Mobile.Mount is BaseCreature) RideMount(e.Mobile, (BaseCreature)e.Mobile.Mount);
                }
        }

        public static void RideEthereal(Mobile from, Item ethereal)
        {
            bool newride = true;

            if (m_RideTime.ContainsKey(from))
            {
                if (m_RideTime[from] >= DateTime.Now)
                {
                    newride = false;
                }
                else
                    m_RideTime.Remove(from);
            }


            LokaiSkills skills = LokaiSkillUtilities.XMLGetSkills(from);

            if (newride)
            {
                int MinLevel = 20;
                int MaxLevel = 100;
                int fame = 0;
                if (Core.Debug) from.SendMessage("TEST: RIDING AN ETHEREAL MOUNT");

                SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, skills.AnimalRiding, MinLevel, MaxLevel);
                if (rating <= SuccessRating.Failure)
                {
                    int scalar = from.TotalWeight / (from.Dex + from.Str);
                    double penaltyTime = 0.0;

                    if (Core.Debug) from.SendMessage("TEST: RIDING FAILED CHECK.");
                    IMount mount = ethereal as IMount;

                    if (mount != null)
                    {
                        mount.Rider = null;
                        from.SendMessage("You were dismounted.");
                    }
                    switch (rating)
                    {
                        case SuccessRating.HazzardousFailure:
                            {
                                if (Utility.RandomBool())
                                {
                                    from.Damage(Utility.Random(5 * scalar));
                                    from.SendMessage("You took some damage!");
                                    fame = -200 * Utility.Random(1, 2);
                                    Misc.Titles.AwardFame(from, fame, true);
                                    penaltyTime = 4.0;
                                }
                                break;
                            }
                        case SuccessRating.CriticalFailure:
                            {
                                if (Utility.RandomBool())
                                {
                                    from.Damage(Utility.Random(15 * scalar));
                                    from.SendMessage("You took serious damage!");
                                    fame = -400 * Utility.Random(1, 2);
                                    Misc.Titles.AwardFame(from, fame, true);
                                    penaltyTime = 10.0;
                                }
                                break;
                            }
                        case SuccessRating.TooDifficult:
                            {
                                from.SendMessage("This mount is too difficult for you!");
                                break;
                            }
                        default: break;
                    }
                    m_RideTime.Add(from, DateTime.Now.AddSeconds(FAILSECONDS + penaltyTime));
                }
                else
                {
                    double bonusTime = 0.0;
                    if (Core.Debug) from.SendMessage("TEST: RIDING SUCCEEDED CHECK.");
                    switch (rating)
                    {
                        case SuccessRating.ExceptionalSuccess:
                            {
                                if (Utility.RandomBool())
                                {
                                    fame = 400 * Utility.Random(1, 2);
                                    Misc.Titles.AwardFame(from, fame, true);
                                    bonusTime = 20.0;
                                }
                                break;
                            }
                        case SuccessRating.CompleteSuccess:
                            {
                                if (Utility.RandomBool())
                                {
                                    fame = 200 * Utility.Random(1, 2);
                                    Misc.Titles.AwardFame(from, fame, true);
                                    bonusTime = 10.0;
                                }
                                break;
                            }
                        case SuccessRating.Success:
                            {
                                if (Utility.RandomBool())
                                {
                                    fame = 100 * Utility.Random(1, 2);
                                    Misc.Titles.AwardFame(from, fame, true);
                                    bonusTime = 4.0;
                                }
                                break;
                            }
                        case SuccessRating.TooEasy:
                            {
                                from.SendMessage("This mount does not provide enough of a challenge for you.");
                                bonusTime = 120.0;
                                break;
                            }
                        default: break;
                    }
                    m_RideTime.Add(from, DateTime.Now.AddSeconds(RIDESECONDS + bonusTime));
                }
                skills.LastLokaiSkillCheck[(int)LokaiSkillName.AnimalRiding] = rating;
            }
            else
            {
                SuccessRating rating = skills.LastCheck(LokaiSkillName.AnimalRiding);
                if (rating <= SuccessRating.Failure)
                {
                    double wait = ((TimeSpan)(m_RideTime[from] - DateTime.Now)).TotalSeconds;
                    if (Core.Debug) from.SendMessage("TEST: FAILED LAST RIDING CHECK.");
                    IMount mount = ethereal as IMount;

                    if (mount != null)
                    {
                        mount.Rider = null;
                        if (rating > SuccessRating.TooDifficult)
                            from.SendMessage("You must wait {0} seconds before you attempt this again.", wait.ToString("F1"));
                        else
                            from.SendMessage("This mount is too difficult for you!");
                    }
                }
            }
        }

        public static void RideMount(Mobile from, BaseCreature mount)
        {
            bool newride = true;

            if (m_RideTime.ContainsKey(from))
            {
                if (m_RideTime[from] >= DateTime.Now)
                {
                    newride = false;
                }
                else
                    m_RideTime.Remove(from);
            }
            LokaiSkills skills = LokaiSkillUtilities.XMLGetSkills(from);

            if (newride)
            {
                int MinLevel = 20;
                int MaxLevel = 70;
                int fame = 0;
                if (Core.Debug) from.SendMessage("TEST: RIDING A LIVE MOUNT");

                if (mount.Summoned)
                {
                    MinLevel -= 10;
                    MaxLevel -= 20;
                }
                if (mount.Dex < 30) { MaxLevel -= 10; MinLevel -= 10; }
                else if (mount.Dex < 60) { MaxLevel += 0; MinLevel += 0; }
                else if (mount.Dex < 90) { MaxLevel += 10; MinLevel += 10; }
                else if (mount.Dex < 140) { MaxLevel += 20; MinLevel += 20; }
                else { MaxLevel += 30; MinLevel += 30; }
                SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, skills.AnimalRiding, MinLevel, MaxLevel);
                if (rating <= SuccessRating.Failure)
                {
                    int scalar = from.TotalWeight / (from.Dex + from.Str);
                    double penaltyTime = 0.0;

                    if (Core.Debug) from.SendMessage("TEST: RIDING FAILED CHECK.");
                    IMount imount = mount as IMount;

                    if (imount != null)
                    {
                        imount.Rider = null;
                        from.SendMessage("You were dismounted.");
                    }
                    switch (rating)
                    {
                        case SuccessRating.HazzardousFailure:
                            {
                                if (Utility.RandomBool())
                                {
                                    from.Damage(Utility.Random(5 * scalar));
                                    from.SendMessage("You took some damage!");
                                    fame = -200 * Utility.Random(1, 2);
                                    Misc.Titles.AwardFame(from, fame, true);
                                    penaltyTime = 4.0;
                                }
                                break;
                            }
                        case SuccessRating.CriticalFailure:
                            {
                                if (Utility.RandomBool())
                                {
                                    from.Damage(Utility.Random(15 * scalar));
                                    from.SendMessage("You took serious damage!");
                                    fame = -400 * Utility.Random(1, 2);
                                    Misc.Titles.AwardFame(from, fame, true);
                                    penaltyTime = 10.0;
                                }
                                break;
                            }
                        case SuccessRating.TooDifficult:
                            {
                                from.SendMessage("This mount is too difficult for you!");
                                break;
                            }
                        default: break;
                    }
                    m_RideTime.Add(from, DateTime.Now.AddSeconds(FAILSECONDS + penaltyTime));
                }
                else
                {
                    double bonusTime = 0.0;
                    if (Core.Debug) from.SendMessage("TEST: RIDING SUCCEEDED CHECK.");
                    switch (rating)
                    {
                        case SuccessRating.ExceptionalSuccess:
                            {
                                if (Utility.RandomBool())
                                {
                                    fame = 400 * Utility.Random(1, 2);
                                    Misc.Titles.AwardFame(from, fame, true);
                                }
                                bonusTime = 20.0;
                                break;
                            }
                        case SuccessRating.CompleteSuccess:
                            {
                                if (Utility.RandomBool())
                                {
                                    fame = 200 * Utility.Random(1, 2);
                                    Misc.Titles.AwardFame(from, fame, true);
                                }
                                bonusTime = 10.0;
                                break;
                            }
                        case SuccessRating.Success:
                            {
                                if (Utility.RandomBool())
                                {
                                    fame = 100 * Utility.Random(1, 2);
                                    Misc.Titles.AwardFame(from, fame, true);
                                }
                                bonusTime = 4.0;
                                break;
                            }
                        case SuccessRating.TooEasy:
                            {
                                from.SendMessage("This mount does not provide enough of a challenge for you.");
                                bonusTime = 120.0;
                                break;
                            }
                        default: break;
                    }
                    m_RideTime.Add(from, DateTime.Now.AddSeconds(RIDESECONDS + bonusTime));
                }
                skills.LastLokaiSkillCheck[(int)LokaiSkillName.AnimalRiding] = rating;
            }
            else
            {
                SuccessRating rating = skills.LastCheck(LokaiSkillName.AnimalRiding);
                if (rating <= SuccessRating.Failure)
                {
                    double wait = ((TimeSpan)(m_RideTime[from] - DateTime.Now)).TotalSeconds;
                    if (Core.Debug) from.SendMessage("TEST: FAILED LAST RIDING CHECK.");
                    IMount imount = mount as IMount;

                    if (imount != null)
                    {
                        imount.Rider = null;
                        if (rating > SuccessRating.TooDifficult)
                            from.SendMessage("You must wait {0} seconds before you attempt this again.", wait.ToString("F1"));
                        else
                            from.SendMessage("This mount is too difficult for you!");
                    }
                }
            }
        }
    }
}
