/***************************************************************************
 *   Based off RunUO items. This program is free software; you can 
 *   redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class RawFlax : Item
	{
		[Constructable]
		public RawFlax() : this( 1 )
		{
		}

		[Constructable]
		public RawFlax( int amount ) : base( 0x1A9C )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
			Name = "Raw Flax";
		}

		public RawFlax( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 502655 ); // What spinning wheel do you wish to spin this on?
				from.Target = new PickWheelTarget( this );
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		public static void OnSpun( ISpinningWheel wheel, Mobile from, int hue )
		{
			Item item = new ThreadSpool( 6 );
			item.Hue = hue;

			from.AddToBackpack( item );
			from.SendLocalizedMessage( 1010577 ); // You put the spools of Thread in your backpack.
		}

		private class PickWheelTarget : Target
		{
			private RawFlax m_RawFlax;

			public PickWheelTarget( RawFlax RawFlax ) : base( 3, false, TargetFlags.None )
			{
				m_RawFlax = RawFlax;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_RawFlax.Deleted )
					return;

				ISpinningWheel wheel = targeted as ISpinningWheel;

				if ( wheel == null && targeted is AddonComponent )
					wheel = ((AddonComponent)targeted).Addon as ISpinningWheel;

				if ( wheel is Item )
				{
					Item item = (Item)wheel;

					if ( !m_RawFlax.IsChildOf( from.Backpack ) )
					{
						from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
					}
					else if ( wheel.Spinning )
					{
						from.SendLocalizedMessage( 502656 ); // That spinning wheel is being used.
					}
					else
					{
                        LokaiSkills skills = LokaiSkillUtilities.XMLGetSkills(from);
                        LokaiSkill lokaiSkill = (LokaiSkillUtilities.XMLGetSkills(from)).Spinning;
                        SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, lokaiSkill, 0.0, 100.0);

                        if (rating >= SuccessRating.PartialSuccess)
                        {
                            m_RawFlax.Consume();
                            wheel.BeginSpin(new SpinCallback(RawFlax.OnSpun), from, m_RawFlax.Hue);
                        }
                        else if (rating == SuccessRating.Failure)
                        {
                            from.SendMessage("You fail, but manage to save your Raw Flax.");
                        }
                        else if (rating == SuccessRating.HazzardousFailure || rating == SuccessRating.CriticalFailure)
                        {
                            m_RawFlax.Consume();
                            from.SendMessage("You fail, and some Raw Flax is lost.");
                        }
                        else if (rating == SuccessRating.TooDifficult)
                        {
                            m_RawFlax.Consume();
                            from.SendMessage("You have no idea how to use this thing.");
                            from.SendMessage("You fail utterly, some Raw Flax is lost.");
                        }
					}
				}
				else
				{
					from.SendLocalizedMessage( 502658 ); // Use that on a spinning wheel.
				}
			}
		}
	}
}