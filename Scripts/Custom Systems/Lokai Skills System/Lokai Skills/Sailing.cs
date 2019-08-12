/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Multis;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Misc;

namespace Server.LokaiSkillHandlers
{
    public enum SailingRank
    {
        None        =   0,
        Landlubber  =   10,
        Novice      =   20,
        Amateur     =   30,
        Able        =   40,
        Adept       =   50,
        Journeyman  =   60,
        Veteran     =   75,
        Master      =   90
    }

    public class Sailing
    {
        private const double SAILSECONDS = 10.0;
        private const double FAILSECONDS = 1.0;
        private static bool SAILING_ACTIVE = true; //Set to 'false' to disable Sailing checks

        private static Dictionary<Mobile, DateTime> m_SailTime;

        public static void Initialize()
        {
            EventSink.Speech += new SpeechEventHandler(EventSink_Sailing);
            m_SailTime = new Dictionary<Mobile, DateTime>();
        }

        static void EventSink_Sailing(SpeechEventArgs e)
        {
            if (SAILING_ACTIVE)
                for (int i = 0; i < e.Keywords.Length; ++i)
                {
                    int keyword = e.Keywords[i];

                    if (keyword >= 0x42 && keyword <= 0x6B)
                    {
                        Point2D point2D = new Point2D(e.Mobile.X, e.Mobile.Y);
                        BaseBoat boat = BaseBoat.FindBoatAt(point2D, e.Mobile.Map);
                        if (e.Mobile is PlayerMobile && boat != null && boat.IsMoving)
                            SailBoat(e.Mobile, boat);
                    }
                }
        }

        public static void SailBoat(Mobile from, BaseBoat boat)
        {
            bool newsail = true;

            if (m_SailTime.ContainsKey(from))
            {
                if (m_SailTime[from] >= DateTime.Now)
                {
                    newsail = false;
                }
                else
                    m_SailTime.Remove(from);
            }

            if (newsail)
            {
                int MinLevel = 10;
                int MaxLevel = 100;
                //if (Core.Debug) from.SendMessage("TEST: SAILING IN A BOAT");

                Mobile sailor = from;

                LokaiSkills skils = LokaiSkillUtilities.XMLGetSkills(sailor);

                List<Mobile> crew = GetMobilesOn(boat);

                SuccessRating rating = SuccessRating.LokaiSkillNotEnabled;

                foreach (Mobile seaman in crew)
                {
                    skils = LokaiSkillUtilities.XMLGetSkills(sailor);

                    LokaiSkills seamanAbs = LokaiSkillUtilities.XMLGetSkills(seaman);
                    if (seamanAbs.Sailing.Value > skils.Sailing.Value)
                        sailor = seaman;
                    MinLevel--;
                    MaxLevel--;
                    if ((MinLevel == 5 && (boat is SmallBoat || boat is SmallDragonBoat)) ||
                        (MinLevel == 0 && (boat is MediumBoat || boat is MediumDragonBoat)) ||
                        (MinLevel == -5 && (boat is LargeBoat || boat is LargeDragonBoat)))
                    {
                        break;
                    }
                }

                int count = 0;
                foreach (Mobile seaman in crew)
                {
                    if ((count == 5 && (boat is SmallBoat || boat is SmallDragonBoat)) ||
                        (count == 10 && (boat is MediumBoat || boat is MediumDragonBoat)) ||
                        (count == 15 && (boat is LargeBoat || boat is LargeDragonBoat)))
                    { break; }
                    else
                    {
                        count++;
                        skils = LokaiSkillUtilities.XMLGetSkills(seaman);
                        if (seaman != sailor)
                        {
                            rating = LokaiSkillUtilities.CheckLokaiSkill(seaman, skils.Sailing, MinLevel, MaxLevel);
                            if (rating >= SuccessRating.PartialSuccess) seaman.SendMessage("You did your part.");
                            else seaman.SendMessage("You could have been more helpful.");
                        }
                    }
                }

                skils = LokaiSkillUtilities.XMLGetSkills(sailor);

                rating = LokaiSkillUtilities.CheckLokaiSkill(sailor, skils.Sailing, MinLevel, MaxLevel);
                if (rating <= SuccessRating.Failure)
                {
                    int severity = 25;
                    if (rating == SuccessRating.HazzardousFailure) severity += 4;
                    else if (rating == SuccessRating.CriticalFailure) severity += 8;

                    bool crash = false;

                    foreach (Mobile seaman in crew)
                    {
                        if (!m_SailTime.ContainsKey(seaman))
                            m_SailTime.Add(seaman, DateTime.Now.AddSeconds(FAILSECONDS));
                    }

                    switch (Utility.Random(severity))
                    {
                        case 0: case 1: case 2: boat.StartMove(Direction.Down, true); goto case 24;
                        case 3: case 4: case 5: boat.StartMove(Direction.East, true); goto case 24;
                        case 6: case 7: case 8: boat.StartMove(Direction.Left, true); goto case 24;
                        case 9: case 10: case 11: boat.StartMove(Direction.North, true); goto case 24;
                        case 12: case 13: case 14: boat.StartMove(Direction.Right, true); goto case 24;
                        case 15: case 16: case 17: boat.StartMove(Direction.South, true); goto case 24;
                        case 18: case 19: case 20: boat.StartMove(Direction.Up, true); goto case 24;
                        case 21: case 22: case 23: boat.StartMove(Direction.West, true); goto case 24;
                        case 24: boat.StartTurn(Utility.RandomList(2, -2, -4), false); goto case 99;
                        case 99:
                            {
                                foreach (Mobile mobile in crew) MightGetSick(mobile);
                                break;
                            }
                        default: crash = true; break;
                    }

                    if (crash)
                    {
                        boat.LowerAnchor(false);
                        List<Item> items = CheckForItems(boat);

                        BaseDockedBoat dboat = boat.DockedBoat;

                        foreach (Mobile seaman in crew)
                        {
                            seaman.SendMessage("The boat runs aground at some nearby land.");
                            boat.RemoveKeys(seaman);
                            if (seaman == boat.Owner)
                            {
                                if (dboat != null)
                                    seaman.AddToBackpack(dboat);
                            }
                        }

                        boat.Delete();

                        foreach (Mobile seaman in crew)
                        {
                            Strand(seaman);
                        }
                        if (items.Count > 0)
                        {
                            for (int v = 0; v < items.Count; v++)
                            {
                                int x = from.X + Utility.Random(7) - 3;
                                int y = from.Y + Utility.Random(7) - 3;
                                items[v].MoveToWorld(new Point3D(x, y, from.Z));
                            }
                        }
                    }
                    else
                        foreach (Mobile seaman in crew)
                            seaman.SendMessage("You go off course slightly.");
                }
                else
                {
                    //if (Core.Debug) from.SendMessage("TEST: SAILING SUCCESSFUL.");

                    foreach (Mobile seaman in crew)
                    {
                        seaman.SendMessage("You feel the gentle breeze of the open sea.");
                        if (!m_SailTime.ContainsKey(seaman))
                            m_SailTime.Add(seaman, DateTime.Now.AddSeconds(SAILSECONDS));
                    }
                }
            }
        }

        public static void MightGetSick(Mobile from)
        {
            if (0.28 > Utility.RandomDouble())
            {
                from.Animate(32, 5, 1, true, false, 0);
                from.PlaySound(from.Female ? 813 : 1087);
                from.SendMessage("You get sick.");
            }
        }

        public static List<Item> CheckForItems(BaseBoat boat)
        {
            List<Item> items = GetItemsOn(boat);

            if (boat.Hold != null && boat.Hold.Items.Count > 0)
            {
                foreach (Item item in boat.Hold.Items)
                {
                    items.Add(item);
                }
            }
            return items;
        }

        public static List<Item> GetItemsOn(BaseBoat boat)
        {
            List<Item> items = new List<Item>();

            MultiComponentList mcl = boat.Components;

            Map map = boat.Map;

            if (map == null || map == Map.Internal)
                return items;

            IPooledEnumerable eable = map.GetObjectsInBounds(new Rectangle2D(boat.X + mcl.Min.X, boat.Y + mcl.Min.Y, mcl.Width, mcl.Height));

            foreach (object o in eable)
            {
                if (o == boat || o == boat.Hold || o == boat.SPlank || o == boat.PPlank || o == boat.TillerMan)
                    continue;

                if (o is Item && boat.Contains((Item)o))
                {
                    items.Add(o as Item);
                }
            }

            eable.Free();
            return items;
        }

        public static List<Mobile> GetMobilesOn(BaseBoat boat)
        {
            List<Mobile> mobiles = new List<Mobile>();

            MultiComponentList mcl = boat.Components;

            Map map = boat.Map;

            if (map == null || map == Map.Internal)
                return mobiles;

            IPooledEnumerable eable = map.GetObjectsInBounds(new Rectangle2D(boat.X + mcl.Min.X, boat.Y + mcl.Min.Y, mcl.Width, mcl.Height));

            foreach (object o in eable)
            {
                if (o == boat || o == boat.Hold || o == boat.SPlank || o == boat.PPlank || o == boat.TillerMan)
                    continue;

                else if (o is Mobile && boat.Contains((Mobile)o))
                {
                    mobiles.Add(o as Mobile);
                }
            }

            eable.Free();
            return mobiles;
        }

        public static void Strand(Mobile from)
        {
            Strandedness.EventSink_Login(new LoginEventArgs(from));
            if (Utility.RandomBool())
            {
                from.Damage(Utility.Random(5 * Utility.Random(1, 3)));
                from.SendMessage("You took some damage!");
            }
            if (Utility.RandomBool())
            {
                int fame = -400 * Utility.Random(1, 2);
                Misc.Titles.AwardFame(from, fame, true);
            }
        }
    }
}
