// Adjustments by Thagoras to work with BaseAnimal
// Additions by SpookyRobert
// Thanks to Lord_Greywolf, Vorspire, ABTOP, Thilgon, David, Mad Clown, and others.
/*
BaseAI.cs Edits:
	at the end of this
	public enum AIType
	{
	
	add:
		AI_WildAnimal
	
BaseCreature.cs Edits:
	at the end of the switch statement in this module
        public void ChangeAIType(AIType NewAI)
        {
            if (m_AI != null)
                m_AI.m_Timer.Stop();

            if (ForcedAI != null)
            {
                m_AI = ForcedAI;
                return;
            }

            m_AI = null;

            switch (NewAI)
            {
			
	add:
				case AIType.AI_WildAnimal:
					if (!(this is BaseAnimal)) 
					{
						m_AI = new AnimalAI(this);
						m_CurrentAI = AIType.AI_Animal;
					}
					else m_AI = new WildAnimalAI(this);
					break;
*/
using System;
using System.Collections;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	public class WildAnimalAI : BaseAI
	{
		public WildAnimalAI(BaseCreature m) : base(m)
		{
		}

		public virtual bool DamageCheck()
		{
			double hitPercent = (double)m_Mobile.Hits / m_Mobile.HitsMax;
			BaseAnimal ba = (BaseAnimal)m_Mobile;
			if ((ba.Eats == EatType.Herbivore) && (hitPercent < 0.80))
			{
				return true;
			}
			else if ((ba.Eats != EatType.Herbivore) && (hitPercent < 0.25))
			{
				return true;
			}
			else
				return false;
		}

		public virtual bool FleeCheck()
		{
			BaseAnimal ba = (BaseAnimal)m_Mobile;
			if (ba.Predator != null)
			{
				return true;
			}
			else
				return false;
		}
		
		public virtual bool IsIndependant()
		{
			if (m_Mobile.Summoned || m_Mobile.Controlled)
				return false;
			else return true;
		}
		
		public bool RunFrom(Mobile m)
		{
			if (m_Mobile.InRange(m, 1))
				return false;
			Run((m_Mobile.GetDirectionTo(m) - 4) & Direction.Mask);
			return true;
		}

		public bool RunTo(Mobile m, int range)
		{
			if (m_Mobile.InRange(m, range))
				return false;
			Run((m_Mobile.GetDirectionTo(m)) & Direction.Mask);
			return true;
		}

		public void Run(Direction d)
		{
			if ((m_Mobile.Spell != null && m_Mobile.Spell.IsCasting) || m_Mobile.Paralyzed || m_Mobile.Frozen || m_Mobile.DisallowAllMoves)
				return;

			//m_Mobile.CurrentSpeed = m_Mobile.ActiveSpeed;

			if (IsIndependant())
			{
				BaseAnimal ba = (BaseAnimal)m_Mobile;
				//Smaller critters should run a bit faster at first. And all animals will slow down when Stam gets low.
				// Don't know if 25 is a good size or not but I needed a number,
				//add I don't know if the speeds are good but it's a start.
				if (ba.Size < 25)
				{
					if (ba.Stam < (ba.StamMax / 4))
						ba.CurrentSpeed = ba.PassiveSpeed;
					else
						ba.CurrentSpeed = (ba.ActiveSpeed + .1);
				}
				else
				{
					if (ba.Stam < (ba.StamMax / 4))
						ba.CurrentSpeed = ba.PassiveSpeed;
					else
						ba.CurrentSpeed = ba.ActiveSpeed;
				}
			}
			else
				m_Mobile.CurrentSpeed = m_Mobile.ActiveSpeed;

			m_Mobile.Direction = d | Direction.Running;
			DoMove(m_Mobile.Direction, true);
		}
		
		public override bool DoActionWander()
		{
			BaseAnimal ba = (BaseAnimal)m_Mobile;
			if (IsIndependant())
			{
				if (FleeCheck() || DamageCheck())
				{
					Action = ActionType.Flee;
					return true;
				}
				else if (ba.Combatant != null && m_Mobile.Hits < m_Mobile.HitsMax)
				{
					m_Mobile.DebugSay("I am hurt or being attacked, I kill him");
					Action = ActionType.Combat;
				}
				else
				{
					base.DoActionWander();
					//m_Mobile.DebugSay("Wandering");
				}
				return true;
			}
			else if (ba.Combatant != null && m_Mobile.Hits < m_Mobile.HitsMax)
			{
				m_Mobile.DebugSay("I am hurt or being attacked, I kill him");
				Action = ActionType.Combat;
			}
			/*
			else if (AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				m_Mobile.DebugSay( "I have detected {0}, attacking", m_Mobile.FocusMob.Name );
				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;
			}
			*/
			else
			{
				base.DoActionWander();
				m_Mobile.DebugSay("Wandering");
			}
			return true;
		}

		public override bool DoActionCombat()
		{
			if (m_Mobile.FocusMob == null) m_Mobile.FocusMob = m_Mobile.Combatant;
			
			Mobile combatant = m_Mobile.Combatant;

			if (IsIndependant() && (FleeCheck() || DamageCheck()))
			{
				Action = ActionType.Flee;
				return true;
			}
			
			if (combatant == null || combatant.Deleted || combatant.Map != m_Mobile.Map)
			{
				m_Mobile.DebugSay("My combatant is gone..");
				Action = ActionType.Wander;
				return true;
			}

			if (WalkMobileRange(combatant, 1, true, m_Mobile.RangeFight, m_Mobile.RangeFight))
			{
				RunTo(combatant, 1);
				//m_Mobile.Direction = m_Mobile.GetDirectionTo( combatant );
			}
			else
			{
				if (m_Mobile.GetDistanceToSqrt(combatant) > m_Mobile.RangePerception + 1)
				{
					if ( m_Mobile.Debug ) m_Mobile.DebugSay("I cannot find {0}", combatant.Name);
					Action = ActionType.Wander;
					return true;
				}
				else
				{
					if ( m_Mobile.Debug ) m_Mobile.DebugSay("I should be closer to {0}", combatant.Name);
				}
			}
			return true;
		}

		public override bool DoActionBackoff()
		{
			if (FleeCheck() || DamageCheck())
			{
				Action = ActionType.Flee;
				return true;
			}
			else
			{
				m_Mobile.DebugSay("I have lost my focus, lets relax");
				Action = ActionType.Wander;
			}
			return true;
		}

		public override bool DoActionFlee()
		{
			BaseAnimal ba = (BaseAnimal)m_Mobile;
			Mobile from = ba.Predator;
			//Direction togo = Direction.South;
			if (from != null)
			{
				if (ba.Grouping != GroupingType.None)
				{
					if (ba.PackLeader != null && ba.PackLeader != m_Mobile)
					{
						BaseAnimal bapl = (BaseAnimal)ba.PackLeader;
						ba.DebugSay("checking pack leaders predator");
						if (bapl.Predator != null)
						{
							Run(bapl.Direction);
						}
						else
						{
							m_Mobile.FocusMob = from;
							m_Mobile.DebugSay("I am fleeing!");
							RunFrom(from);
						}
					}
					else if (ba.PackLeader == null && ba.Mate != null)
					{
						BaseAnimal mate = (BaseAnimal)ba.Mate;
						ba.DebugSay("checking mates predator");
						if (mate.Predator != null)
						{
							Run(mate.Direction);
						}
						else
						{
							m_Mobile.FocusMob = from;
							m_Mobile.DebugSay("I am fleeing!");
							RunFrom(from);
						}
					}
				}
				else
				{
					m_Mobile.FocusMob = from;
					m_Mobile.DebugSay("I am fleeing!");
					RunFrom(from);
					//return true;
				}

				if (from == null || from.Deleted || from.Map != m_Mobile.Map)
				{
					m_Mobile.DebugSay("I have lost him");
					Action = ActionType.Guard;
					return true;
				}
				if (!m_Mobile.InRange(from, 21))
				{
					m_Mobile.DebugSay("I have fled");
					ba.Predator = null;
					Action = ActionType.Guard;
					return true;
				}
				else
				{
					m_Mobile.DebugSay("I am fleeing!");
					RunFrom(from);
				}
			}
			return true;
		}
	}
}

