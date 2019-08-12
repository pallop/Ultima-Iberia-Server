/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Prompts;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Multis;

namespace Server.LokaiSkillHandlers
{
	public class Ventriloquism
	{
		public static void Initialize()
		{
            LokaiSkillInfo.Table[(int)LokaiSkillName.Ventriloquism].Callback = new LokaiSkillUseCallback(OnUse);
		}

		public static TimeSpan OnUse( Mobile m )
		{
            m.Target = new InternalTarget();
            m.SendMessage("Select your target.");
            return TimeSpan.FromSeconds(12.0);
		}

		private class InternalTarget : Target
		{
			private bool m_SetSkillTime = true;

			public InternalTarget() :  base ( 8, false, TargetFlags.None )
			{
			}

			protected override void OnTargetFinish( Mobile from )
			{
				if ( m_SetSkillTime )
					from.NextSkillTime = Core.TickCount;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
                if (targeted is Mobile || targeted is Item || targeted is StaticTarget)
                {
                    from.SendMessage("What do you wish to say.");
                    from.Prompt = new InternalPrompt(targeted);
                }
                else
                {
                    from.SendMessage("You can't throw your voice there.");
                }
			}

            private class InternalPrompt : Prompt
            {
                private object targeted;

                public InternalPrompt(object target)
                {
                    targeted = target;
                }

                public override void OnResponse(Mobile from, string text)
                {
                    if (targeted is Item && ((Item)targeted).Parent != null)
                    {
                        targeted = ((Item)targeted).Parent;
                        OnResponse(from, text);
                    }
                    LokaiSkills skills = LokaiSkillUtilities.XMLGetSkills(from);
                    LokaiSkill lokaiSkill = (LokaiSkillUtilities.XMLGetSkills(from)).Ventriloquism;

                    if (targeted is Mobile)
                    {
                        Mobile mob = (Mobile)targeted;
                        if (mob == from)
                        {
                            from.SendMessage("That's not much of a trick.");
                        }
                        else if (!mob.Alive || mob.Deleted || mob.Hidden)
                        {
                            from.SendMessage("I don't know who or what you mean.");
                        }
                        else if (!from.InRange(mob.Location, 5))
                        {
                            from.SendMessage("They are too far away.");
                        }
                        else if (!from.CanSee(mob))
                        {
                            from.SendMessage("You can't see them well enough to make them talk.");
                        }
                        else
                        {
                            double distance = Math.Sqrt((double)((mob.X - from.X) * (mob.X - from.X) +
                                (double)((mob.Y - from.Y) * (mob.Y - from.Y))));
                            double minLokaiSkill = 10.0 + (distance * 5.0);
                            double maxLokaiSkill = 75.0 + (distance * 5.0);
                            SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, lokaiSkill, minLokaiSkill, maxLokaiSkill);
                            if (rating >= SuccessRating.PartialSuccess)
                            {
                                mob.PublicOverheadMessage(MessageType.Emote, 0, false, text);
                            }
                            else
                            {
                                from.Say(text);
                                from.SendMessage("Your attempt at ventriloquism fails.");
                            }
                        }
                    }
                    else if (targeted is Item)
                    {
                        Item item = (Item)targeted;
                        if (!from.InRange(item.Location, 5))
                        {
                            from.SendMessage("That is too far away.");
                        }
                        else if (!from.CanSee(item))
                        {
                            from.SendMessage("You can't see that well enough to make it talk.");
                        }
                        else
                        {
                            double distance = Math.Sqrt((double)((item.X - from.X) * (item.X - from.X) +
                                (double)((item.Y - from.Y) * (item.Y - from.Y))));
                            double minLokaiSkill = 0.0 + (distance * 5.0);
                            double maxLokaiSkill = 65.0 + (distance * 5.0);
                            SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, lokaiSkill, minLokaiSkill, maxLokaiSkill);
                            if (rating >= SuccessRating.PartialSuccess)
                            {
                                item.PublicOverheadMessage(MessageType.Emote, 0, false, text);
                            }
                            else
                            {
                                from.Say(text);
                                from.SendMessage("Your attempt at ventriloquism fails.");
                            }
                        }
                    }
                    else if (targeted is StaticTarget)
                    {
                        StaticTarget stat = (StaticTarget)targeted;
                        if (!from.InRange(stat.Location, 5))
                        {
                            from.SendMessage("That is too far away.");
                        }
                        else
                        {
                            double distance = Math.Sqrt((double)((stat.X - from.X) * (stat.X - from.X) +
                                (double)((stat.Y - from.Y) * (stat.Y - from.Y))));
                            double minLokaiSkill = -10.0 + (distance * 5.0);
                            double maxLokaiSkill = 55.0 + (distance * 5.0);
                            SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, lokaiSkill, minLokaiSkill, maxLokaiSkill);
                            if (rating >= SuccessRating.PartialSuccess)
                            {
                                IPoint3D p = targeted as IPoint3D;
                                Item item = new Item(stat.ItemID);
                                if (p == null)
                                {
                                    from.SendMessage("Unable to target that.");
                                    return;
                                }

                                if (p is Item)
                                {
                                    p = ((Item)p).GetWorldTop();
                                    if (Core.Debug) from.SendMessage("TEMP: Set Point3D to ((Item)p).GetWorldTop().");
                                }
                                else
                                {
                                    p = new Point3D(stat.X, stat.Y, stat.Z - item.ItemData.CalcHeight);
                                    if (Core.Debug) from.SendMessage("TEMP: IPoint3D was not an Item.");
                                }
                                item.MoveToWorld(new Point3D(p), from.Map);
                                item.PublicOverheadMessage(MessageType.Emote, 0, false, text);
                                new InternalTimer(from, item).Start();
                            }
                            else
                            {
                                from.Say(text);
                                from.SendMessage("Your attempt at ventriloquism fails.");
                            }
                        }
                    }
                }
            }

			private class InternalTimer : Timer
			{
				private Mobile m_From;
                private Item m_Target;

				public InternalTimer( Mobile from, Item target ) : base( TimeSpan.FromSeconds( 2.0 ) )
				{
					m_From = from;
                    m_Target = target;
                    Priority = TimerPriority.TwoFiftyMS;
				}

				protected override void OnTick()
				{
                    m_Target.Delete();
				}
			}
		}
	}
}