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
    public class Commerce
    {
        public static double TooDifficult = 1.99;
        public static double CriticalFailure = 1.66;
        public static double HazzardousFailure = 1.33;
        public static double Failure = 1.16;
        public static double PartialSuccess = 0.97;
        public static double Success = 0.90;
        public static double CompleteSuccess = 0.84;
        public static double ExceptionalSuccess = 0.79;
        public static double TooEasy = 0.75;

        public static void Initialize()
        {
            LokaiSkillInfo.Table[(int)LokaiSkillName.Commerce].Callback = new LokaiSkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile m)
        {
            if (LokaiSkillUtilities.CommerceEnabled)
            {
                m.SendMessage("Select a vendor with whom to practice your commerce skill.");
                m.Target = new InternalVendorTarget();
                return TimeSpan.FromSeconds(3.0);
            }
            else
            {
                m.SendMessage("Commerce is not enabled.");
                return TimeSpan.FromSeconds(1.0);
            }
        }

        private class InternalVendorTarget : Target
        {
            public InternalVendorTarget()
                : base(6, false, TargetFlags.None)
            {
            }

            protected override void OnTargetFinish(Mobile from)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is BaseVendor)
                {
                    from.SendMessage("Select an item to sell.");
                    from.Target = new InternalItemTarget(targeted as BaseVendor);
                }
                else
                {
                    from.SendMessage("You must select a vendor!");
                }
            }
        }

        private class InternalItemTarget : Target
        {
            private BaseVendor m_Vendor;

            public InternalItemTarget(BaseVendor vendor)
                : base(6, false, TargetFlags.None)
            {
                m_Vendor = vendor;
            }

            protected override void OnTargetFinish(Mobile from)
            {
                from.NextSkillTime = Core.TickCount;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                Container cont = from.Backpack;
                if (targeted is Item && cont != null && ((Item)targeted).IsChildOf(cont))
                {
                    IShopSellInfo[] info = m_Vendor.GetSellInfo();
                    Item item = targeted as Item;

                    int totalCost = 0;
                    string name = null;

                    foreach (IShopSellInfo ssi in info)
                    {
                        if (ssi.IsSellable(item))
                        {
                            totalCost = ssi.GetBuyPriceFor(item);
                            name = ssi.GetNameFor(item);
                            break;
                        }
                    }

                    if (name == null)
                    {
                        m_Vendor.SayTo(from, "I won't buy that.");
                    }
                    else if (totalCost == 0)
                    {
                        m_Vendor.SayTo(from, "I won't negotiate on free items.");
                    }
                    else
                    {
                        int commerceCost = totalCost;
                        string commerceSkill = "non-existent";
                        LokaiSkill lokaiSkill = (LokaiSkillUtilities.XMLGetSkills(from)).Commerce;

                        SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, lokaiSkill, 0.0, 100.0);
                        switch (rating)
                        {
                            case SuccessRating.CriticalFailure:
                                commerceCost = (int)(totalCost / CriticalFailure);
                                commerceSkill = "horrible";
                                break;
                            case SuccessRating.HazzardousFailure:
                                commerceCost = (int)(totalCost / HazzardousFailure);
                                commerceSkill = "terrible";
                                break;
                            case SuccessRating.Failure:
                                commerceCost = (int)(totalCost / Failure);
                                commerceSkill = "lousy";
                                break;
                            case SuccessRating.PartialSuccess:
                                commerceCost = (int)(totalCost / PartialSuccess);
                                commerceSkill = "mediocre";
                                break;
                            case SuccessRating.Success:
                                commerceCost = (int)(totalCost / Success);
                                commerceSkill = "good";
                                break;
                            case SuccessRating.CompleteSuccess:
                                commerceCost = (int)(totalCost / CompleteSuccess);
                                commerceSkill = "adept";
                                break;
                            case SuccessRating.ExceptionalSuccess:
                                commerceCost = (int)(totalCost / ExceptionalSuccess);
                                commerceSkill = "exceptional";
                                break;
                            case SuccessRating.TooEasy:
                                commerceCost = (int)(totalCost / TooEasy);
                                commerceSkill = "unquestionable";
                                break;
                            default:
                            case SuccessRating.TooDifficult:
                                commerceCost = (int)(totalCost / TooDifficult);
                                commerceSkill = "non-existent";
                                break;
                        }
                        m_Vendor.SayTo(from, "Normally, I would pay {0} for that (1), but due to your {2} commerce skill, I am paying you {3}.",
                            totalCost, name, commerceSkill, commerceCost);
                        totalCost = commerceCost;
                        item.Consume();

                        if (totalCost > 1000)
                            from.AddToBackpack(new BankCheck(totalCost));
                        else if (totalCost > 0)
                            from.AddToBackpack(new Gold(totalCost));

                    }
                }
                else
                    from.SendMessage("You can only sell items in your backpack.");
            }
        }
    }
}