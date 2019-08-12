using System;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class CreamPitcher : Item
	{
		private int m_Quantity;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Quantity
		{
			get { return m_Quantity; }
			set {	m_Quantity = value; }
		}
		
		private DateTime m_Age = DateTime.UtcNow;
		[ CommandProperty( AccessLevel.GameMaster ) ]
		public DateTime Age
		{
			get { return m_Age; }
			set { m_Age = value; }
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add(GetQuantityDescription());
		}

		public virtual string GetQuantityDescription()
		{
			Weight = 1 + m_Quantity;
			
			int perc = ( m_Quantity * 100 ) / 5;
			if ( perc <= 0 )
				return "It's empty.";
			else if ( perc <= 33 )
				return "It's nearly empty.";
			else if ( perc <= 66 )
				return "It's half full.";
			else
				return "It's full.";
		}
		
		public virtual string GetAgeDescription()
		{
			TimeSpan CheckDifference = DateTime.UtcNow - m_Age;
			int agecheck = (int)((CheckDifference.TotalMinutes / 60 / 24 )*12);
			if (agecheck < 1) return "new ";
			if (agecheck < 2) return "fresh ";
			//if (agecheck < 4) return "";
			if (agecheck > 14) return "sour ";
			if (agecheck > 10) return "old ";
			return "";
		}
		
		public virtual void UpdateName()
		{
			if (m_Quantity > 0) Name = "a pitcher of " + GetAgeDescription() + "cream";
			else Name = "a cream pitcher";
		}
		
		[Constructable]
		public CreamPitcher() : base( 0x9D6 )
		{
			Name = "a cream pitcher";
			Weight = 1;
			Hue = 1191;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if (this.IsChildOf(from.Backpack))
			{
				
				from.SendMessage("Target what?");
				from.Target = new CreamTarget(this);
			}
			else from.SendMessage("That must be in your backpack to use.");
		}

		public CreamPitcher( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write(m_Age);
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
					m_Age = reader.ReadDateTime();
					m_Quantity = reader.ReadInt();
					break;
				}
			}
		}

		private class CreamTarget : Target
		{
			private CreamPitcher t_cp;

			public CreamTarget(CreamPitcher cp) : base( 1, false, TargetFlags.None )
			{
				t_cp = cp;
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is MilkKeg)
				{
					if (t_cp.Quantity < 5)
					{
						bool CanUse = from.CheckSkill( SkillName.Cooking, 15, 30 );
						if (CanUse)
						{
							MilkKeg bev = (MilkKeg) targ;
							if(bev.Quantity > 10)
							{
								if (t_cp.Quantity < 1) t_cp.Age = bev.Age;
								if (t_cp.Age > bev.Age) t_cp.Age = bev.Age;
								bev.Quantity -= 1;
								t_cp.Quantity += 1;
								t_cp.InvalidateProperties();
								t_cp.UpdateName();
								bev.InvalidateProperties();
								bev.UpdateName();
								from.PlaySound( 0x4E );
								from.SendMessage("You skim off some cream.");
							}
							else from.SendMessage("There is no cream in that!");
						}
						else 
						{
							from.SendMessage("You fail to skim off the cream!");
							from.PlaySound( 0x4E );
						}
					}
					else from.SendMessage("The pitcher is full!");
				}
				else if (targ is ButterChurn)
				{
					ButterChurn bev = (ButterChurn) targ;
					if(bev.Quantity < 10)
					{
						if (t_cp.Quantity < 10 - bev.Quantity)
						{
							bev.Quantity += t_cp.Quantity;
							t_cp.Quantity = 0;
						}
						else
						{
							t_cp.Quantity -= (10 - bev.Quantity);
							bev.Quantity = 10;
						}	
						t_cp.InvalidateProperties();
						t_cp.UpdateName();
						bev.InvalidateProperties();
						bev.UpdateName();
						from.PlaySound( 0x4E );
						from.SendMessage("You pour the cream into the churn.");
					}
					else from.SendMessage("That is full!");
				}
				else if (targ is MeasuringCup)
				{
					if (t_cp.Quantity > 0)
					{
						from.AddToBackpack( new Cream());
						from.PlaySound( 0x240 );
						from.SendMessage("You measure out one cup of cream.");
						t_cp.Quantity -= 1;
						t_cp.InvalidateProperties();
						t_cp.UpdateName();
					}
					else from.SendMessage("The pitcher is empty!");
				}
				else
				{
					from.SendMessage ("You can't do that.");
				}
			}
		}
	}
}