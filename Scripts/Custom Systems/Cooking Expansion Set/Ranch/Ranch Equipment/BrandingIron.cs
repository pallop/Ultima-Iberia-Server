using System;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Items;
using Server.Mobiles;
using Server.Regions;
using Server.Prompts;
using Server.Multis;

namespace Server.Items
{
	[FlipableAttribute( 0x143A, 0x143B )]
	public class BrandingIron : Item
	{
		private bool m_Hot = true;
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Hot
		{
			get { return m_Hot; }
			set {	m_Hot = value; }
		}
		
		private RanchStone m_ranchstone;
		[CommandProperty( AccessLevel.GameMaster )]
		public RanchStone ranchstone
		{
			get { return m_ranchstone; }
			set {	m_ranchstone = value; InvalidateProperties(); }
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			if (m_ranchstone != null) list.Add(m_ranchstone.Ranch);
		}

		[Constructable]
		public BrandingIron() : base( 0x143A )
		{
			Name = "a branding iron";
			Hue = 1257;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if (m_ranchstone.Deleted || m_ranchstone == null) 
			{
				this.Delete();
				return;
			}
			
			if (this.IsChildOf(from.Backpack))
			{
				if (m_Hot)
				{
					from.SendMessage("Target the animal to brand.");
					from.Target = new BrandingTarget(this);
				}
				else from.SendMessage("The iron has grown cold.");
			}
			else from.SendMessage("That must be in your backpack to use.");
		}

		public BrandingIron( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( (bool) m_Hot);
			
			writer.Write(m_ranchstone);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			m_Hot = reader.ReadBool();
			m_ranchstone = (RanchStone)reader.ReadItem();
		}

		private class BrandingTarget : Target
		{
			private BrandingIron t_bi;

			public BrandingTarget(BrandingIron bi) : base( 1, false, TargetFlags.None )
			{
				t_bi = bi;
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is BaseAnimal)
				{
					BaseAnimal ba = (BaseAnimal) targ;
					if ((ba.ControlMaster == from && ba.Owner == null) || ba.Owner == from)
					{
						ba.Owner = t_bi.ranchstone.Owner;
						ba.Brand = t_bi.ranchstone.Ranch;
						from.PlaySound( 0x3B5 );
						ba.PlaySound(ba.GetHurtSound());
						from.SendMessage("Animal branded!");
						ba.InvalidateProperties();
						t_bi.Hot = Utility.RandomBool(); //false;
						if (!t_bi.Hot) from.SendMessage("The branding iron cools.");
					}
					else from.SendMessage("You don't own that animal.");
				}
				else
				{
					from.SendMessage ("You can't brand that.");
				}
			}
		}
	}
}