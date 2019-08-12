/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections.Generic;
using Server.Targeting;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Multis;

namespace Server.LokaiSkillHandlers
{
	public class PickPocket
	{
        //If true, vendors and monsters will NEVER lose the gold they give you.
        private static bool INFINITE_GOLD = true;

        //If true, players can try to pick pocket other players.
        private static bool ALLOW_PLAYER_THEFT = false;

		public static void Initialize()
		{
            LokaiSkillInfo.Table[(int)LokaiSkillName.PickPocket].Callback = new LokaiSkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile m)
        {
            m.Target = new InternalTarget();
            m.SendMessage("Whose pocket do you wish to pick?");
            return TimeSpan.FromSeconds(4.0);
        }

		private class InternalTarget : Target
		{
			private bool m_SetSkillTime = true;

			public InternalTarget() :  base ( 12, false, TargetFlags.Harmful )
			{
			}

			protected override void OnTargetFinish( Mobile from )
			{
				if ( m_SetSkillTime )
					from.NextSkillTime = Core.TickCount;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
                from.RevealingAction();
                LokaiSkill lokaiSkill = (LokaiSkillUtilities.XMLGetSkills(from)).PickPocket;

                if (targeted is Mobile)
                {
                    Mobile mobile = targeted as Mobile;
                    Container pack = mobile.Backpack;

                    if (!from.InRange(mobile.Location, 1))
                    {
                        from.NextSkillTime = Core.TickCount;
                        from.SendMessage("You are too far away to do that.");
                        return;
                    }
                    else if (pack == null)
                    {
                        from.NextSkillTime = Core.TickCount;
                        from.SendMessage("That target has no backpack.");
                        return;
                    }
                    else if (!from.CheckAlive())
                    {
                        from.NextSkillTime = Core.TickCount;
                        from.SendMessage("You cannot do that while you are dead.");
                        return;
                    }
                    else if (!mobile.CheckAlive())
                    {
                        from.NextSkillTime = Core.TickCount;
                        from.SendMessage("That is dead, so you cannot do that.");
                        return;
                    }
                    else if (mobile == from)
                    {
                        from.NextSkillTime = Core.TickCount;
                        from.SendMessage("You wish to pick your own pocket?");
                        return;
                    }
                    else
                    {
                        bool withoutNotice = true;
                        if (targeted is PlayerMobile)
                        {
                            if (ALLOW_PLAYER_THEFT)
                            {
                                SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, lokaiSkill, 40.0, 100.0);
                                withoutNotice = PickTry(from, mobile, rating, lokaiSkill, pack);
                            }
                            else
                                from.SendMessage("Pick-pocketing players is not allowed around here!");
                        }
                        else if (targeted is BaseVendor)
                        {
                            SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, lokaiSkill, 0.0, 60.0);
                            withoutNotice = PickTry(from, mobile, rating, lokaiSkill, pack);
                        }
                        else if (targeted is BaseCreature &&
                            (!((targeted as BaseCreature).Controlled && (targeted as BaseCreature).ControlMaster == from)))
                        {
                            SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, lokaiSkill, 20.0, 80.0);
                            withoutNotice = PickTry(from, mobile, rating, lokaiSkill, pack);
                        }
                        else
                        {
                            from.NextSkillTime = Core.TickCount;
                            from.SendMessage("You may not pick their pocket.");
                            return;
                        }

                        if (!withoutNotice)
                        {
                            if (mobile is PlayerMobile)
                            {
                                from.CriminalAction(true);
                                from.OnHarmfulAction(mobile, from.Criminal);
                            }
                            else if (mobile is BaseVendor)
                            {
                                mobile.Direction = mobile.GetDirectionTo(from);
                                from.Direction = from.GetDirectionTo(mobile);
                                mobile.Animate(31, 5, 1, true, false, 0);
                                mobile.Say(Utility.RandomList(1005560, 1013046, 1079127, 1013038, 1013039, 1010634));
                                from.Animate(20, 5, 1, true, false, 0);
                                from.Damage(Math.Max((Utility.Random(3) + 3), (int)(from.Hits / (Utility.Random(8) + 8))));
                            }
                            else if (mobile is BaseCreature)
                            {
                                (mobile as BaseCreature).AggressiveAction(from, from.Criminal);
                            }
                        }
                    }
                }
                else
                    from.SendMessage("That does not have a pocket you can pick.");
			}

            private bool PickTry(Mobile from, Mobile mobile, SuccessRating rating, LokaiSkill lokaiSkill, Container pack)
            {
                bool pickGold, pickItem;
                bool pickNone = false;
                Item[] goldItems = pack.FindItemsByType(typeof(Gold));
                int goldAmount = 0;
                foreach (Item item in goldItems) goldAmount += item.Amount;

                List<Item> pickables = new List<Item>();
                Search(pack, ref pickables);

                if (pickables.Count > 0) pickItem = true; else pickItem = false;
                if (goldAmount > 0) pickGold = true; else pickGold = false;

                if (!pickGold && !pickItem)
                {
                    pickNone = true;
                }
                else if (pickGold && pickItem)
                {
                    pickGold = Utility.RandomBool();
                    pickItem = !pickGold;
                }

                if (rating >= SuccessRating.PartialSuccess)
                {
                    if (pickNone)
                    {
                        from.NextSkillTime = Core.TickCount;
                        from.SendMessage("Their pockets are empty.");
                    }
                    else if (pickGold)
                    {
                        int pickAmount = Math.Min((int)(4 * lokaiSkill.Value), (int)(goldAmount / 1000));
                        if ((INFINITE_GOLD && !(mobile is PlayerMobile) || (goldAmount > 10 && pickAmount < 10)))
                            pickAmount = Utility.Random(6) + 5;
                        if (goldAmount > 100 && rating >= SuccessRating.CompleteSuccess && pickAmount < 100)
                            pickAmount = Utility.Random(61) + 40;
                        if (goldAmount > 500 && rating >= SuccessRating.ExceptionalSuccess && pickAmount < 500)
                            pickAmount = Utility.Random(301) + 200;
                        if (from.AddToBackpack(new Gold(pickAmount)))
                        {
                            if ((mobile is PlayerMobile && pack.ConsumeTotal(typeof(Gold), pickAmount) ||
                                (!INFINITE_GOLD && pack.ConsumeTotal(typeof(Gold), pickAmount))))
                                from.SendMessage("You were able to pick {0} gold from their pocket!", pickAmount.ToString());
                            else
                                from.SendMessage("You find {0} gold in their pocket!", pickAmount.ToString());
                        }
                    }
                    else
                    {
                        int random = Utility.Random(pickables.Count);
                        if (random >= pickables.Count) random = 0;
                        Item itemToPick = pickables[random];
                        if (pickables[random].Amount > 1 && from.AddToBackpack(itemToPick))
                        {
                            itemToPick.Amount = 1;
                            pickables[random].Amount--;
                        }
                        else
                        {
                            if (from.AddToBackpack(itemToPick)) pack.OnItemRemoved(pickables[random]);
                        }
                    }
                }
                else if (rating == SuccessRating.Failure)
                {
                    from.SendMessage("You fail.");
                }
                else if (rating == SuccessRating.HazzardousFailure || rating == SuccessRating.CriticalFailure)
                {
                    from.SendMessage("You fail utterly.");
                }
                else if (rating == SuccessRating.TooDifficult)
                {
                    from.SendMessage("You do not have the necessary lokaiSkill to attempt this.");
                }
                double caughtChance = 1.0;

                switch (rating)
                {
                    case SuccessRating.CriticalFailure: caughtChance = 0.95; break;
                    case SuccessRating.HazzardousFailure: caughtChance = 0.8; break;
                    case SuccessRating.Failure: caughtChance = 0.65; break;
                    case SuccessRating.PartialSuccess: caughtChance = 0.5; break;
                    case SuccessRating.Success: caughtChance = 0.3; break;
                    case SuccessRating.CompleteSuccess: caughtChance = 0.1; break;
                    case SuccessRating.ExceptionalSuccess: caughtChance = 0.05; break;
                    case SuccessRating.TooEasy: caughtChance = -0.01; break;
                    default: break;
                }
                return caughtChance < Utility.RandomDouble();
            }

            private void Search(Container pack, ref List<Item> pickables)
            {
                foreach (Item item in pack.Items)
                {
                    if (item is Container) Search((item as Container), ref pickables);
                    if ((item is BaseArmor || item is BaseBeverage || item is BaseClothing || item is BaseJewel
                        || item is BaseReagent || item is BaseTool || item is BaseWeapon) && !item.QuestItem
                        && item.LootType == LootType.Regular && !item.Insured && item.Movable)
                        pickables.Add(item);
                }
            }

			private class InternalTimer : Timer
			{
				private Mobile m_From, m_Target;

				public InternalTimer( Mobile from, Mobile target ) : base( TimeSpan.FromSeconds( 2.0 ) )
				{
					m_From = from;
					m_Target = target;
					Priority = TimerPriority.TwoFiftyMS;
				}

				protected override void OnTick()
				{
					m_From.NextSkillTime = Core.TickCount + (int)TimeSpan.FromSeconds( 10.0 ).TotalSeconds;
				}
			}
		}
	}
}