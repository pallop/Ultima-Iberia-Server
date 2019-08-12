using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class ButterChurn : Item
	{
		private int m_Quantity;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Quantity
		{
			get { return m_Quantity; }
			set {	m_Quantity = value; }
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add(GetQuantityDescription());
		}

		public virtual void UpdateName()
		{
			Name = "a butter churn";
		}
		
		public virtual string GetQuantityDescription()
		{
			Weight = 5 + m_Quantity;
			
			int perc = ( m_Quantity * 100 ) / 10;
			if ( perc <= 0 )
				return "It's empty.";
			else if ( perc <= 33 )
				return "It's nearly empty.";
			else if ( perc <= 66 )
				return "It's half full.";
			else
				return "It's full.";
		}
		
		[Constructable]
		public ButterChurn() : base( 0x24D8 )
		{
			Name = "a butter churn";
			Weight = 5;
			//Hue = 2500;
		}

		public override void OnDoubleClick( Mobile from )
		{
			bool CanUse = from.CheckSkill( SkillName.Cooking, 5, 20 );
			if (CanUse && m_Quantity > 0)
			{
				from.SendMessage("You churn the cream into butter.");
				from.AddToBackpack(new Butter(m_Quantity));
				m_Quantity = 0;
				InvalidateProperties();
			}
			else from.SendMessage("You fail to churn the cream!");
		}

		public ButterChurn( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( (int) m_Quantity);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			switch (version)
			{
				case 1:
				{
					goto case 0;
				}
				case 0:
				{
					m_Quantity = reader.ReadInt();
					break;
				}
			}
		}
	}
}