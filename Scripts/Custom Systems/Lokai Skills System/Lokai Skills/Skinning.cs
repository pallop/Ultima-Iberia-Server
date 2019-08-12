/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Targeting;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Multis;

namespace Server.LokaiSkillHandlers
{
    public class Skinning
    {
        public static void Initialize()
        {
            LokaiSkillInfo.Table[(int)LokaiSkillName.Skinning].Callback = new LokaiSkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile m)
        {
            Item item = m.FindItemOnLayer(Layer.OneHanded);
            if (item != null && (item is SkinningKnife || item is Dagger))
            {
                m.Target = new InternalTarget();
            }
            else
            {
                Container pack = m.Backpack;
                if (pack != null
                    && (pack.FindItemByType(new Type[] { typeof(SkinningKnife), typeof(Dagger) }) != null))
                {
                    m.Target = new InternalTarget();
                }
                else
                    m.SendMessage("You need a Skinning knife or a Dagger to use this skill.");
            }
            return TimeSpan.FromSeconds(3.0);
        }

        public static void OnCarve(Mobile from, Corpse corpse, BaseCreature mob, LokaiSkill lokaiSkill)
        {
            if (corpse.Carved) return;

            int feathers = mob.Feathers;
            int wool = mob.Wool;
            int meat = mob.Meat;
            int hides = mob.Hides;
            int scales = mob.Scales;

            if ((feathers == 0 && wool == 0 && meat == 0 && hides == 0 && scales == 0) || mob.Summoned || mob.IsBonded)
            {
                from.SendLocalizedMessage(500485); // You see nothing useful to carve from the corpse.
            }
            else
            {
                if (Core.ML && from.Race == Race.Human)
                {
                    hides = (int)Math.Ceiling(hides * 1.1);	//10% Bonus Only applies to Hides, Ore & Logs
                }

                if (corpse.Map == Map.Felucca)
                {
                    feathers *= 2;
                    wool *= 2;
                    hides *= 2;
                }

                int ratingFactor = 10;
                int skinFactor = 10;

                SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, lokaiSkill, 0.0, 100.0);
                switch (rating)
                {
                    case SuccessRating.CriticalFailure: skinFactor = 3; ratingFactor = 0; break;
                    case SuccessRating.HazzardousFailure: skinFactor = 6; ratingFactor = 0; break;
                    case SuccessRating.Failure: skinFactor = 9; ratingFactor = 0; break;
                    case SuccessRating.PartialSuccess: skinFactor = 11; ratingFactor = 0; break;
                    case SuccessRating.Success: skinFactor = 14; ratingFactor = 3; break;
                    case SuccessRating.CompleteSuccess: skinFactor = 17; ratingFactor = 6; break;
                    case SuccessRating.ExceptionalSuccess:
                    case SuccessRating.TooEasy: skinFactor = 20; ratingFactor = 9; break;
                    default:
                    case SuccessRating.TooDifficult: skinFactor = 0; ratingFactor = 0; break;
                }

                feathers *= skinFactor;
                wool *= skinFactor;
                hides *= skinFactor;
                scales *= skinFactor;
                meat *= ratingFactor;

                feathers /= 10;
                wool /= 10;
                hides /= 10;
                scales /= 10;
                meat /= 10;

                new Blood(0x122D).MoveToWorld(corpse.Location, corpse.Map);

                if (feathers != 0)
                {
                    corpse.DropItem(new Feather(feathers));
                    from.SendLocalizedMessage(500479); // You pluck the bird. The feathers are now on the corpse.
                }

                if (wool != 0)
                {
                    corpse.DropItem(new Wool(wool));
                    from.SendLocalizedMessage(500483); // You shear it, and the wool is now on the corpse.
                }

                if (meat != 0)
                {
                    if (mob.MeatType == MeatType.Ribs)
                        corpse.DropItem(new RawRibs(meat));
                    else if (mob.MeatType == MeatType.Bird)
                        corpse.DropItem(new RawBird(meat));
                    else if (mob.MeatType == MeatType.LambLeg)
                        corpse.DropItem(new RawLambLeg(meat));

                    from.SendLocalizedMessage(500467); // You carve some meat, which remains on the corpse.
                }

                if (hides != 0)
                {
                    if (mob.HideType == HideType.Regular)
                        corpse.DropItem(new Hides(hides));
                    else if (mob.HideType == HideType.Spined)
                        corpse.DropItem(new SpinedHides(hides));
                    else if (mob.HideType == HideType.Horned)
                        corpse.DropItem(new HornedHides(hides));
                    else if (mob.HideType == HideType.Barbed)
                        corpse.DropItem(new BarbedHides(hides));

                    from.SendLocalizedMessage(500471); // You skin it, and the hides are now in the corpse.
                }

                if (scales != 0)
                {
                    ScaleType sc = mob.ScaleType;

                    switch (sc)
                    {
                        case ScaleType.Red: corpse.DropItem(new RedScales(scales)); break;
                        case ScaleType.Yellow: corpse.DropItem(new YellowScales(scales)); break;
                        case ScaleType.Black: corpse.DropItem(new BlackScales(scales)); break;
                        case ScaleType.Green: corpse.DropItem(new GreenScales(scales)); break;
                        case ScaleType.White: corpse.DropItem(new WhiteScales(scales)); break;
                        case ScaleType.Blue: corpse.DropItem(new BlueScales(scales)); break;
                        case ScaleType.All:
                            {
                                corpse.DropItem(new RedScales(scales));
                                corpse.DropItem(new YellowScales(scales));
                                corpse.DropItem(new BlackScales(scales));
                                corpse.DropItem(new GreenScales(scales));
                                corpse.DropItem(new WhiteScales(scales));
                                corpse.DropItem(new BlueScales(scales));
                                break;
                            }
                    }

                    from.SendMessage("You cut away some scales, but they remain on the corpse.");
                }

                corpse.Carved = true;

                if (corpse.IsCriminalAction(from))
                    from.CriminalAction(true);
            }
        }

        private class InternalTarget : Target
        {
            private bool m_SetSkillTime = true;

            public InternalTarget()
                : base(6, false, TargetFlags.None)
            {
            }

            protected override void OnTargetFinish(Mobile from)
            {
                if (m_SetSkillTime)
                    from.NextSkillTime = Core.TickCount;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                from.RevealingAction();
                if (targeted is Corpse)
                {
                    Corpse corpse = targeted as Corpse;
                    if (corpse.Owner != null && corpse.Owner is BaseCreature)
                    {
                        BaseCreature creature = (targeted as Corpse).Owner as BaseCreature;

                        LokaiSkill lokaiSkill = (LokaiSkillUtilities.XMLGetSkills(from)).Skinning;
                        OnCarve(from, corpse, creature, lokaiSkill);
                    }
                    else
                        from.SendMessage("You may not skin that type of corpse.");
                }
                else
                    from.SendMessage("You may not skin that!");
            }
        }
    }
}