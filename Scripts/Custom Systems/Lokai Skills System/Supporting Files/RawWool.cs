/***************************************************************************
 *   Based off RunUO items. This program is free software; you can 
 *   redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class RawWool : Item, IDyable
	{
		[Constructable]
		public RawWool() : this( 1 )
		{
		}

		[Constructable]
		public RawWool( int amount ) : base( 0xDF8 )
		{
			Stackable = true;
			Weight = 4.0;
			Amount = amount;
			Name = "Raw Wool";
		}

		public RawWool( Serial serial ) : base( serial )
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
		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
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
			Item item = new GreyYarn( 3 );
			item.Hue = hue;

			from.AddToBackpack( item );
			from.SendLocalizedMessage( 1010576 ); // You put the balls of yarn in your backpack.
		}

		private class PickWheelTarget : Target
		{
			private RawWool m_RawWool;

			public PickWheelTarget( RawWool RawWool ) : base( 3, false, TargetFlags.None )
			{
				m_RawWool = RawWool;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_RawWool.Deleted )
					return;

				ISpinningWheel wheel = targeted as ISpinningWheel;

				if ( wheel == null && targeted is AddonComponent )
					wheel = ((AddonComponent)targeted).Addon as ISpinningWheel;

				if ( wheel is Item )
				{
					Item item = (Item)wheel;

					if ( !m_RawWool.IsChildOf( from.Backpack ) )
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
                        LokaiSkill lokaiSkill = skills[LokaiSkillName.Spinning];
                        SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, lokaiSkill, 0.0, 100.0);

                        if (rating >= SuccessRating.PartialSuccess)
                        {
                            m_RawWool.Consume();
                            wheel.BeginSpin(new SpinCallback(RawWool.OnSpun), from, m_RawWool.Hue);
                        }
                        else if (rating == SuccessRating.Failure)
                        {
                            from.SendMessage("You fail, but manage to save your Raw Wool.");
                        }
                        else if (rating == SuccessRating.HazzardousFailure || rating == SuccessRating.CriticalFailure)
                        {
                            m_RawWool.Consume();
                            from.SendMessage("You fail, and some Raw Wool is lost.");
                        }
                        else if (rating == SuccessRating.TooDifficult)
                        {
                            m_RawWool.Consume();
                            from.SendMessage("You have no idea how to use this thing.");
                            from.SendMessage("You fail utterly, some Raw Wool is lost.");
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