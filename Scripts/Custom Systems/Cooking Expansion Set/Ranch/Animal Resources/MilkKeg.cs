//Cows, Goats, and Sheep don't just make milk all the time (unlike chickens and eggs).
//They won't produce milk unless they've given birth.  Once they've started producing
//milk, they will continue to produce milk until the calf is weined.  Farmers have
//capitalized on this fact and continue to milk the cows, goats, sheep, long after the
//offspring is weined thus fooling their bodies into thinking they still need to produce milk.
//As such, the Cows, Sheep, and eventually Goat, have been written to only produce milk
//after they've given birth, and to continue only until their offspring turns to a Young
//animal (between Baby and Adult) AND they haven't been milked for (I think) 7 days straight.
using System;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Items;
using Server.Mobiles;
using Server.Multis;

namespace Server.Items
{
/*	public enum TypeOfMilk
	{
		None,
		Cow,
		Sheep,
		Goat,
		Mixed
	}*/
	
	[FlipableAttribute( 0x1AD6, 0x1AD7 )]
	public class MilkKeg : Item
	{
		private int m_Quantity;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Quantity
		{
			get { return m_Quantity; }
			set {	m_Quantity = value; }
		}
		
		private TypeOfMilk m_MilkType;
		[CommandProperty( AccessLevel.GameMaster )]
		public TypeOfMilk MilkType
		{
			get { return m_MilkType; }
			set {	m_MilkType = value; }
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
			Weight = 10 + m_Quantity;
			if (m_Quantity < 1) m_MilkType = TypeOfMilk.None;
			
			int perc = ( m_Quantity * 100 ) / 100;
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
			if (m_Quantity > 0) Name = "a keg of " + GetAgeDescription() + "milk [" + m_MilkType + "]";
			else Name = "a milk keg";
		}
		
		[Constructable]
		public MilkKeg() : base( 6870 )
		{
			Name = "a milk keg";
			Weight = 10;
			Hue = 2500;
		}

		public override void OnDoubleClick( Mobile from )
		{
			//if (this.IsChildOf(from.Backpack))
			//{
				from.SendMessage("Target container.");
				from.Target = new MilkTarget(this);
			//}
			//else from.SendMessage("That must be in your backpack to use.");
		}

		public MilkKeg( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( (int) m_Quantity);
			writer.Write( (int) m_MilkType);
			writer.Write(m_Age);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			m_Quantity = reader.ReadInt();
			m_MilkType = (TypeOfMilk) reader.ReadInt();
			m_Age = reader.ReadDateTime();
		}

		private class MilkTarget : Target
		{
			private MilkKeg t_mk;

			public MilkTarget(MilkKeg mk) : base( 1, false, TargetFlags.None )
			{
				t_mk = mk;
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is BaseBeverage)
				{
					BaseBeverage bev = (BaseBeverage) targ;
					if (bev.Content == BeverageType.Milk || bev.Quantity < 1)
					{
						bev.Content = BeverageType.Milk;
						if ( t_mk.Quantity > bev.Quantity)
						{
							t_mk.Quantity -= (bev.MaxQuantity - bev.Quantity);
							bev.Quantity = bev.MaxQuantity;
						}
						else
						{
							bev.Quantity += t_mk.Quantity;
							t_mk.Quantity = 0;
						}
						t_mk.InvalidateProperties();
						t_mk.UpdateName();
						from.PlaySound( 0x4E );
					}
					else from.SendMessage("You shouldn't mix beverages.");
				}
				else if (targ is MilkingBucket)
				{
					MilkingBucket bev = (MilkingBucket) targ;
					if(bev.Quantity < 10)
					{
						if (bev.MilkType == t_mk.MilkType || bev.Quantity < 1)
						{
							bev.MilkType = t_mk.MilkType;
							if (bev.Quantity < 1) bev.Age = t_mk.Age;
							if (bev.Age > t_mk.Age) bev.Age = t_mk.Age;
							if ( t_mk.Quantity > (10 - bev.Quantity))
							{
								t_mk.Quantity -= (10 - bev.Quantity);
								bev.Quantity = 10;
							}
							else
							{
								bev.Quantity += t_mk.Quantity;
								t_mk.Quantity = 0;
							}
							t_mk.InvalidateProperties();
							t_mk.UpdateName();
							bev.InvalidateProperties();
							bev.UpdateName();
							from.PlaySound( 0x4E );
							from.SendMessage("You pour the milk into the keg.");
						}
						else
						{
							bev.MilkType = TypeOfMilk.Mixed;
							if (bev.Quantity < 1) bev.Age = t_mk.Age;
							if (bev.Age > t_mk.Age) bev.Age = t_mk.Age;
							if ( t_mk.Quantity > (100 - bev.Quantity))
							{
								t_mk.Quantity -= (100 - bev.Quantity);
								bev.Quantity = 100;
							}
							else
							{
								bev.Quantity += t_mk.Quantity;
								t_mk.Quantity = 0;
							}
							t_mk.InvalidateProperties();
							t_mk.UpdateName();
							bev.InvalidateProperties();
							bev.UpdateName();
							from.PlaySound( 0x4E );
							from.SendMessage("You pour the milk into the keg.");
						}
					}
					else from.SendMessage("That is full!");
				}
				else if (targ is MilkKeg)
				{
					MilkKeg bev = (MilkKeg) targ;
					if(bev.Quantity < 100)
					{
						if (bev.MilkType == t_mk.MilkType || bev.Quantity < 1)
						{
							bev.MilkType = t_mk.MilkType;
							if (bev.Quantity < 1) bev.Age = t_mk.Age;
							if (bev.Age > t_mk.Age) bev.Age = t_mk.Age;
							if ( t_mk.Quantity > (100 - bev.Quantity))
							{
								t_mk.Quantity -= (100 - bev.Quantity);
								bev.Quantity = 100;
							}
							else
							{
								bev.Quantity += t_mk.Quantity;
								t_mk.Quantity = 0;
							}
							t_mk.InvalidateProperties();
							t_mk.UpdateName();
							bev.InvalidateProperties();
							bev.UpdateName();
							from.PlaySound( 0x4E );
							from.SendMessage("You pour the milk into the keg.");
						}
						else
						{
							bev.MilkType = TypeOfMilk.Mixed;
							if (bev.Quantity < 1) bev.Age = t_mk.Age;
							if (bev.Age > t_mk.Age) bev.Age = t_mk.Age;
							if ( t_mk.Quantity > (100 - bev.Quantity))
							{
								t_mk.Quantity -= (100 - bev.Quantity);
								bev.Quantity = 100;
							}
							else
							{
								bev.Quantity += t_mk.Quantity;
								t_mk.Quantity = 0;
							}
							t_mk.InvalidateProperties();
							t_mk.UpdateName();
							bev.InvalidateProperties();
							bev.UpdateName();
							from.PlaySound( 0x4E );
							from.SendMessage("You pour the milk into the keg.");
						}
					}
					else from.SendMessage("That is full!");
				}
				else if (targ is MeasuringCup)
				{
					if (t_mk.Quantity > 0)
					{
						from.AddToBackpack( new Milk());
						from.PlaySound( 0x240 );
						from.SendMessage("You measure out one cup of milk.");
						t_mk.Quantity -= 1;
						t_mk.InvalidateProperties();
						t_mk.UpdateName();
					}
					else from.SendMessage("The keg is empty!");
				}
				else
				{
					from.SendMessage ("You can't do that.");
				}
			}
		}
	}
}