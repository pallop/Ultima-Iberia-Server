/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Multis;
using Server.Gumps;
using Server.Mobiles;

namespace Server.LokaiSkillHandlers
{
	public class Hypnotism
	{
		public static void Initialize()
		{
            LokaiSkillInfo.Table[(int)LokaiSkillName.Hypnotism].Callback = new LokaiSkillUseCallback(OnUse);
		}

		public static TimeSpan OnUse( Mobile m )
		{
			m.Target = new InternalTarget();
			m.SendMessage("Who do you wish to hypnotize?");
			return TimeSpan.FromSeconds( 9.0 );
		}

		private class InternalTarget : Target
		{
			private bool m_SetSkillTime = true;

			public InternalTarget() :  base ( 12, false, TargetFlags.None )
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

                if (targeted is Mobile)
                {
                    if (((Mobile)targeted) != from)
                    {
                        if (targeted is PlayerMobile)
                        {
                            PlayerMobile player = targeted as PlayerMobile;
                            from.Direction = from.GetDirectionTo(player);

                            from.LocalOverheadMessage(MessageType.Emote, 0x59, true, String.Format("You begin to hypnotize {0}", player.Name));
                            player.LocalOverheadMessage(MessageType.Emote, 0x59, true, String.Format("{0} begins to hypnotize you!", from.Name));
                            from.NonlocalOverheadMessage(MessageType.Emote, 0x59, true, String.Format("* begins hypnotizing {0} *", player.Name));

                            new InternalTimer(from, player, Utility.Random(3, 3)).Start();

                            m_SetSkillTime = false;
                        }
                        else if (targeted is BaseCreature)
                        {
                            BaseCreature creature = targeted as BaseCreature;
                            from.Direction = from.GetDirectionTo(creature);

                            from.LocalOverheadMessage(MessageType.Emote, 0x59, true, String.Format("You begin to hypnotize {0}", creature.Name));
                            from.NonlocalOverheadMessage(MessageType.Emote, 0x59, true, String.Format("* begins hypnotizing {0} *", creature.Name));

                            new InternalTimer(from, creature, Utility.Random(2, 3)).Start();

                            m_SetSkillTime = false;
                        }
                        else if (targeted is BaseEscortable)
                        {
                            BaseEscortable escortable = targeted as BaseEscortable;
                            from.Direction = from.GetDirectionTo(escortable);

                            from.LocalOverheadMessage(MessageType.Emote, 0x59, true, String.Format("You begin to hypnotize {0}", escortable.Name));
                            from.NonlocalOverheadMessage(MessageType.Emote, 0x59, true, String.Format("* begins hypnotizing {0} *", escortable.Name));

                            new InternalTimer(from, escortable, Utility.Random(3, 3)).Start();

                            m_SetSkillTime = false;
                        }
                        else
                        {
                            from.SendMessage("That being will not be hypnotized.");
                        }
                    }
                    else
                        from.SendMessage("You cannot hypnotize yourself!");
                }
                else
                {
                    from.SendMessage("You can not hypnotize that!");
                }

			}

			private class InternalTimer : Timer
			{
				private Mobile m_From;
				private Mobile m_Mobile;
				private BaseCreature m_Creature;
				private bool isCreature;
                private int m_FullCount;
                private int m_Count;

                public InternalTimer(Mobile from, BaseEscortable escortabletarget, int count)
                    : base(TimeSpan.FromSeconds(2.0), TimeSpan.FromSeconds(2.0), count)
                {
                    m_From = from;
                    m_Mobile = escortabletarget;
                    Priority = TimerPriority.TwoFiftyMS;
                    isCreature = false;
                    m_FullCount = count;
                }

                public InternalTimer(Mobile from, PlayerMobile mobiletarget, int count)
                    : base(TimeSpan.FromSeconds(2.0), TimeSpan.FromSeconds(2.0), count)
				{
					m_From = from;
					m_Mobile = mobiletarget;
					Priority = TimerPriority.TwoFiftyMS;
					isCreature = false;
                    m_FullCount = count;
				}

                public InternalTimer(Mobile from, BaseCreature creaturetarget, int count)
                    : base(TimeSpan.FromSeconds(2.0), TimeSpan.FromSeconds(2.0), count)
                {
                    m_From = from;
                    m_Creature = creaturetarget;
                    Priority = TimerPriority.TwoFiftyMS;
                    isCreature = true;
                    m_FullCount = count;
                }

				protected override void OnTick()
				{
                    m_Count++;

                    if (isCreature)
                    {
                        DamageEntry de = m_Creature.FindMostRecentDamageEntry(false);
                        bool alreadyOwned = m_Creature.Owners.Contains(m_From);

                        if (!m_From.InRange(m_Creature, 6))
                        {
                            m_From.NextSkillTime = Core.TickCount;
                            m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, "You are too far away to continue hypnotizing.", m_From.NetState);
                            Stop();
                        }
                        else if (!m_From.CheckAlive())
                        {
                            m_From.NextSkillTime = Core.TickCount;
                            m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, "You are dead, and cannot continue hypnotizing.", m_From.NetState);
                            Stop();
                        }
                        else if (!m_Creature.CheckAlive())
                        {
                            m_From.NextSkillTime = Core.TickCount;
                            m_From.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, "The creature is dead, so you cannot continue hypnotizing it.", m_From.NetState);
                            Stop();
                        }
                        else if (m_Creature.Controlled && m_Creature.ControlMaster == m_From)
                        {
                            m_From.NextSkillTime = Core.TickCount;
                            m_From.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, "The creature will already obey you.", m_From.NetState);
                            Stop();
                        }
                        else if (!m_From.CanSee(m_Creature) || !m_From.InLOS(m_Creature))
                        {
                            m_From.NextSkillTime = Core.TickCount;
                            m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, "You do not have a clear path to the creature you are hypnotizing, and must cease your attempt.", m_From.NetState);
                            Stop();
                        }
                        else if (m_Creature.Frozen && m_Creature.Paralyzed)
                        {
                            m_From.NextSkillTime = Core.TickCount;
                            m_From.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, "It appears to be already in a trance.", m_From.NetState);
                            Stop();
                        }
                        else if (m_Count < m_FullCount)
                        {
                            m_From.RevealingAction();

                            switch (Utility.Random(5))
                            {
                                case 0: m_From.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "You are getting sleepy....", false); break;
                                case 1: m_From.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "Look into my eyes....", false); break;
                                case 2: m_From.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "Look deep into my eyes....", false); break;
                                case 3: m_From.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "Your eyes are getting very heavy....", false); break;
                                case 4: m_From.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "Sleepy....very sleepy....", false); break;
                            }

                            if (!alreadyOwned) // Passively check animal lore for gain
                                m_From.CheckTargetSkill(SkillName.AnimalLore, m_Creature, 0.0, 120.0);
                        }
                        else
                        {
                            m_From.RevealingAction();
                            m_From.NextSkillTime = Core.TickCount;

                            double minSkill = (double)m_Creature.Int / 3.0;

                            if (minSkill < -10.0) minSkill = -10.0;
                            if (minSkill > 90.0) minSkill = 90.0;

                            minSkill += 8.3;

                            LokaiSkill lokaiSkill = LokaiSkillUtilities.XMLGetSkills(m_From).Hypnotism;
                            SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(m_From, lokaiSkill, minSkill, alreadyOwned ? minSkill : minSkill + 40);

                            if (rating >= SuccessRating.PartialSuccess)
                            {
                                if (rating == SuccessRating.TooEasy)
                                {
                                    m_From.SendLocalizedMessage(502797); // That wasn't even challenging.
                                }
                                else
                                {
                                    m_From.SendMessage("You succeed in hypnotizing the creature.");
                                }

                                double duration = 15.0;
                                switch (rating)
                                {
                                    case SuccessRating.PartialSuccess: break;
                                    case SuccessRating.Success: duration += 5.0; break;
                                    case SuccessRating.CompleteSuccess: duration += 15.0; break;
                                    case SuccessRating.ExceptionalSuccess: duration += 25.0; break;
                                    case SuccessRating.TooEasy: duration += 45; break;
                                }

                                m_Creature.Freeze(TimeSpan.FromSeconds(duration));
                                m_Creature.Paralyze(TimeSpan.FromSeconds(duration));
                                m_Creature.Pacify(m_From, DateTime.Now.AddSeconds(duration));
                            }
                            else
                            {
                                m_From.SendMessage("You fail to hypnotize the creature.");
                            }
                        }
                    }
                    else
                    {
                        DamageEntry de = m_Mobile.FindMostRecentDamageEntry(false);

                        if (!m_From.InRange(m_Mobile, 6))
                        {
                            m_From.NextSkillTime = Core.TickCount;
                            m_Mobile.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, "You are too far away to continue hypnotizing.", m_From.NetState);
                            Stop();
                        }
                        else if (!m_From.CheckAlive())
                        {
                            m_From.NextSkillTime = Core.TickCount;
                            m_Mobile.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, "You are dead, and cannot continue hypnotizing.", m_From.NetState);
                            Stop();
                        }
                        else if (!m_From.CanSee(m_Mobile) || !m_From.InLOS(m_Mobile))
                        {
                            m_From.NextSkillTime = Core.TickCount;
                            m_Mobile.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, "You do not have a clear path to the person you are hypnotizing, and must cease your attempt.", m_From.NetState);
                            Stop();
                        }
                        else if (!m_Mobile.CheckAlive())
                        {
                            m_From.NextSkillTime = Core.TickCount;
                            m_From.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, "The person is dead, so you cannot continue hypnotizing them.", m_From.NetState);
                            Stop();
                        }
                        else if (m_Mobile is BaseEscortable && ((BaseEscortable)m_Mobile).Controlled && ((BaseEscortable)m_Mobile).ControlMaster == m_From)
                        {
                            m_From.NextSkillTime = Core.TickCount;
                            m_From.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, "They will already obey you.", m_From.NetState);
                            Stop();
                        }
                        else if (m_Mobile.Frozen && m_Mobile.Paralyzed)
                        {
                            m_From.NextSkillTime = Core.TickCount;
                            m_From.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, "They appear to be already in a trance.", m_From.NetState);
                            Stop();
                        }
                        else if (m_Count < m_FullCount)
                        {
                            m_From.RevealingAction();

                            switch (Utility.Random(5))
                            {
                                case 0: m_From.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "You are getting sleepy....", false); break;
                                case 1: m_From.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "Look into my eyes....", false); break;
                                case 2: m_From.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "Look deep into my eyes....", false); break;
                                case 3: m_From.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "Your eyes are getting very heavy....", false); break;
                                case 4: m_From.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "Sleepy....very sleepy....", false); break;
                            }

                            m_From.CheckTargetSkill(SkillName.EvalInt, m_Mobile, 0.0, 120.0);
                        }
                        else
                        {
                            m_From.RevealingAction();
                            m_From.NextSkillTime = Core.TickCount;

                            m_From.CheckTargetSkill(SkillName.EvalInt, m_Mobile, 0.0, 120.0);

                            double minSkill = (double)m_Mobile.Int / 3.0;

                            if (minSkill < -10.0) minSkill = -10.0;
                            if (minSkill > 90.0) minSkill = 90.0;

                            minSkill += 10.0;

                            LokaiSkill lokaiSkill = LokaiSkillUtilities.XMLGetSkills(m_From).Hypnotism;
                            SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(m_From, lokaiSkill, minSkill, minSkill + 40);

                            if (rating >= SuccessRating.PartialSuccess)
                            {
                                double duration = 15.0;
                                switch (rating)
                                {
                                    case SuccessRating.PartialSuccess: break;
                                    case SuccessRating.Success: duration += 5.0; break;
                                    case SuccessRating.CompleteSuccess: duration += 15.0; break;
                                    case SuccessRating.ExceptionalSuccess: duration += 25.0; break;
                                    case SuccessRating.TooEasy: duration += 45; break;
                                }
                                m_From.SendMessage("You successfully put your subject in a trance.");

                                Mobile master = null;

                                if (m_Mobile is BaseEscortable)
                                {
                                    BaseEscortable target = (BaseEscortable)m_Mobile;
                                    if (((BaseEscortable)m_Mobile).Controlled)
                                    {
                                        master = target.ControlMaster;
                                    }
                                    target.Controlled = true;
                                    target.ControlMaster = m_From;
                                    m_From.SendMessage("The target will obey you for {0} seconds.", duration.ToString("F1"));
                                    new EscortableTimer(m_From, master, target, (int)duration).Start();
                                }
                                else
                                {
                                    m_Mobile.Freeze(TimeSpan.FromSeconds(duration));
                                    m_Mobile.Paralyze(TimeSpan.FromSeconds(duration));
                                    m_Mobile.SendMessage("You have been hypnotized!");
                                    m_From.SendGump(new HypnotismGump(m_From, m_Mobile, duration, 1));
                                }
                            }
                            else
                            {
                                m_From.SendMessage("You fail to hypnotize your subject.");
                            }
                        }
                    }

				}
			}

            private class EscortableTimer : Timer
            {
                private int maxcount;
                private int count;
                Mobile m_From;
                Mobile m_OldMaster;
                BaseEscortable m_Escortable;

                public EscortableTimer(Mobile from, Mobile oldMaster, BaseEscortable escortable, int duration)
                    : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0), duration)
                {
                    maxcount = duration;
                    m_From = from;
                    m_Escortable = escortable;
                    m_OldMaster = oldMaster;
                }

                protected override void OnTick()
                {
                    count++;
                    if (count >= maxcount)
                    {
                        if (m_OldMaster != null)
                        {
                            m_Escortable.ControlMaster = m_OldMaster;
                        }
                        else
                        {
                            m_Escortable.Controlled = false;
                        }
                    }
                }
            }
		}
    }

    public class HypnotismGump : Gump
    {
        private Mobile m_From;
        private Mobile m_Subject;
        private DateTime m_StopTime;
        private int m_Page;

        public HypnotismGump(Mobile from, Mobile subject, double duration, int page)
            : base(40, 82)
        {
            from.CloseGump(typeof(HypnotismGump));

            Dragable = true;
            Closable = false;
            Resizable = false;

            m_From = from;
            m_Subject = subject;
            m_StopTime = DateTime.Now.AddSeconds(duration);
            m_Page = page;

            AddPage(0);
            AddBackground(0, 65, 130, 360, 5054);
            AddAlphaRegion(10, 70, 110, 350);
            AddImageTiled(10, 70, 110, 20, 9354);
            AddLabel(13, 70, 200, string.Format("{0} seconds left.", duration.ToString("F1")));
            AddImage(100, 0, 10410);
            AddImage(100, 305, 10412);
            AddImage(100, 150, 10411);
            int y = 90;
            switch (page)
            {
                case 1:
                    {
                        for (int x = 1; x <= 12; x++)
                        {
                            AddButtonLabeled(10, y, GetButtonID(1, x), Emote.Sounds[x]);
                            y += 25;
                        }
                        AddButton(70, 380, 4502, 0504, GetButtonID(0, 2), GumpButtonType.Reply, 0);
                        break;
                    }
                case 2:
                    {
                        for (int x = 13; x <= 24; x++)
                        {
                            AddButtonLabeled(10, y, GetButtonID(1, x), Emote.Sounds[x]);
                            y += 25;
                        }
                        AddButton(10, 380, 4506, 4508, GetButtonID(0, 1), GumpButtonType.Reply, 0);
                        AddButton(70, 380, 4502, 0504, GetButtonID(0, 3), GumpButtonType.Reply, 0);
                        break;
                    }
                case 3:
                    {
                        for (int x = 25; x <= 36; x++)
                        {
                            AddButtonLabeled(10, y, GetButtonID(1, x), Emote.Sounds[x]);
                            y += 25;
                        }
                        AddButton(10, 380, 4506, 4508, GetButtonID(0, 2), GumpButtonType.Reply, 0);
                        AddButton(70, 380, 4502, 0504, GetButtonID(0, 4), GumpButtonType.Reply, 0);
                        break;
                    }
                case 4:
                    {
                        for (int x = 37; x <= 48; x++)
                        {
                            AddButtonLabeled(10, y, GetButtonID(1, x), Emote.Sounds[x]);
                            y += 25;
                        }
                        AddButton(10, 380, 4506, 4508, GetButtonID(0, 3), GumpButtonType.Reply, 0);
                        break;
                    }
            }
        }

        public void AddButtonLabeled(int x, int y, int buttonID, string text)
        {
            AddButton(x, y - 1, 4005, 4007, buttonID, GumpButtonType.Reply, 0);
            AddHtml(x + 35, y, 240, 20, Color(text, 0xFFFFFF), false, false);
        }
        public int GetButtonID(int type, int index)
        {
            return 1 + (index * 15) + type;
        }
        public string Color(string text, int color)
        {
            return String.Format("<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text);
        } 

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            int val = info.ButtonID - 1;
            if (val < 0)
                return;

            int type = val % 15;
            int index = val / 15;

            double timeleft = ((TimeSpan)(m_StopTime - DateTime.Now)).TotalSeconds;

            if (timeleft < 0.0)
            {
                m_From.CloseGump(typeof(HypnotismGump));
            }
            else
                switch (type)
                {
                    case 0:
                        {
                            if (index < 1 || index > 4)
                                m_From.CloseGump(typeof(HypnotismGump));
                            else
                                m_From.SendGump(new HypnotismGump(m_From, m_Subject, timeleft, index));
                            break;
                        }
                    case 1:
                        {
                            if (index >= 1 && index <= 12)
                            {
                                m_From.SendGump(new HypnotismGump(m_From, m_Subject, timeleft, 1));
                            }
                            else if (index >= 13 && index <= 24)
                            {
                                m_From.SendGump(new HypnotismGump(m_From, m_Subject, timeleft, 2));
                            }
                            else if (index >= 25 && index <= 36)
                            {
                                m_From.SendGump(new HypnotismGump(m_From, m_Subject, timeleft, 3));
                            }
                            else if (index >= 37 && index <= 48)
                            {
                                m_From.SendGump(new HypnotismGump(m_From, m_Subject, timeleft, 4));
                            }
                            new ESound(m_Subject, index);
                            break;
                        }
                }
        }
    }
}