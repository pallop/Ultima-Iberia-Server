/***************************************************************************
 *   Based off RunUO items. This program is free software; you can 
 *   redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public abstract class NewBaseClothMaterial : Item, IDyable
	{
		public NewBaseClothMaterial( int itemID ) : this( itemID, 1 )
		{
		}

		public NewBaseClothMaterial( int itemID, int amount ) : base( itemID )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

		public NewBaseClothMaterial( Serial serial ) : base( serial )
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
				from.SendLocalizedMessage( 500366 ); // Select a loom to use that on.
				from.Target = new PickLoomTarget( this );
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		private class PickLoomTarget : Target
		{
			private NewBaseClothMaterial m_Material;

			public PickLoomTarget( NewBaseClothMaterial material ) : base( 3, false, TargetFlags.None )
			{
				m_Material = material;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Material.Deleted )
					return;

				ILoom loom = targeted as ILoom;

				if ( loom == null && targeted is AddonComponent )
					loom = ((AddonComponent)targeted).Addon as ILoom;

				if ( loom != null )
				{
                    if (!m_Material.IsChildOf(from.Backpack))
                    {
                        from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
                    }
                    else
                    {
                        LokaiSkills skills = LokaiSkillUtilities.XMLGetSkills(from);
                        LokaiSkill lokaiSkill = skills[LokaiSkillName.Weaving];
                        SuccessRating rating = LokaiSkillUtilities.CheckLokaiSkill(from, lokaiSkill, 0.0, 100.0);

                        if (rating >= SuccessRating.PartialSuccess)
                        {
                            if (loom.Phase < 4)
                            {
                                if (targeted is Item)
                                    ((Item)targeted).SendLocalizedMessageTo(from, 1010001 + loom.Phase++);
                                m_Material.Consume();
                            }
                            else
                            {
                                Item create = new BoltOfCloth();
                                create.Hue = m_Material.Hue;
                                loom.Phase = 0;
                                from.SendLocalizedMessage(500368); // You create some cloth and put it in your backpack.
                                from.AddToBackpack(create);
                                m_Material.Consume();
                            }
                        }
                        else if (rating == SuccessRating.Failure)
                        {
                            from.SendMessage("You fail, but manage to save your material.");
                        }
                        else if (rating == SuccessRating.HazzardousFailure)
                        {
                            m_Material.Consume();
                            from.SendMessage("You fail, and some material is lost.");
                        }
                        else if (rating == SuccessRating.CriticalFailure)
                        {
                            m_Material.Consume();
                            loom.Phase = 0;
                            from.SendMessage("You fail utterly, some material is lost, and you need to start over.");
                        }
                        else if (rating == SuccessRating.TooDifficult)
                        {
                            m_Material.Consume();
                            loom.Phase = 0;
                            from.SendMessage("You have no idea how to work this thing.");
                            from.SendMessage("You fail utterly, some material is lost, and you need to start over.");
                        }
                    }
				}
				else
				{
					from.SendLocalizedMessage( 500367 ); // Try using that on a loom.
				}
			}
		}
	}

	public class GreyYarn : NewBaseClothMaterial
	{
		[Constructable]
		public GreyYarn() : this( 1 )
		{
		}

		[Constructable]
		public GreyYarn( int amount ) : base( 0xE1D, amount )
		{
			Name = "Grey Yarn";
		}

		public GreyYarn( Serial serial ) : base( serial )
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
	}

	public class WhiteYarn : NewBaseClothMaterial
	{
		[Constructable]
		public WhiteYarn() : this( 1 )
		{
		}

		[Constructable]
		public WhiteYarn( int amount ) : base( 0xE1E, amount )
		{
			Name = "White Yarn";
		}

		public WhiteYarn( Serial serial ) : base( serial )
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
	}

	public class WhiteYarnUnraveled : NewBaseClothMaterial
	{
		[Constructable]
		public WhiteYarnUnraveled() : this( 1 )
		{
		}

		[Constructable]
		public WhiteYarnUnraveled( int amount ) : base( 0xE1F, amount )
		{
			Name = "White Yarn Unraveled";
		}

		public WhiteYarnUnraveled( Serial serial ) : base( serial )
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
	}

	public class ThreadSpool : NewBaseClothMaterial
	{
		[Constructable]
		public ThreadSpool() : this( 1 )
		{
		}

		[Constructable]
		public ThreadSpool( int amount ) : base( 0xFA0, amount )
		{
			Name = "ThreadSpool";
		}

		public ThreadSpool( Serial serial ) : base( serial )
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
	}
}