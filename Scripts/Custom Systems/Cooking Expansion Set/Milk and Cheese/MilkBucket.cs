
using System;
using Server;
using Server.Items;
using Server.Prompts;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items

{
	public class MilkBucket : Item
	{
		public int Laitage;
		public int Bestiole;

		[CommandProperty( AccessLevel.GameMaster )]
		public int m_laitage
		{
			get { return Laitage; }
			set { Laitage = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int m_bestiole
		{
			get { return Bestiole; }
			set { Bestiole = value; InvalidateProperties(); }
		}

		[Constructable]
		public MilkBucket() : base( 0x0FFA )
		{
			this.Weight = 10.0;
			this.Name = "Milk Bucket: (Empty)";
			this.Hue = 1001;
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );
			this.LabelTo( from,Laitage.ToString());
		}

		public override void OnDoubleClick (Mobile from )
		{
			if (! from.InRange( this.GetWorldLocation(), 1 ))
			{
				from.LocalOverheadMessage( MessageType.Regular, 906, 1019045 );
			}
			else
			{
				from.Target = new OnVaVoirLesVaches( this );
				from.SendMessage(0x96D, "What do you want to use that with?" );
			}
		}
		public MilkBucket( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (int) Laitage );
			writer.Write( (int) Bestiole);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
					{
						Laitage = reader.ReadInt();
						Bestiole = (int)reader.ReadInt();
						break;
					}
			}
		}
	}

	public class OnVaVoirLesVaches : Target
	{
		private MilkBucket m_varsaut;
		Mobile m_mobile = null;

		public OnVaVoirLesVaches( MilkBucket m_saut) : base( 2, false, TargetFlags.None )
		{
			m_varsaut = m_saut;
		}

		protected override void OnTarget( Mobile from, object o )
		{
			Container pack = from.Backpack;
			if( o is Mobile)
				m_mobile =(Mobile)o;

			if ((m_varsaut.m_laitage == 0) && (m_varsaut.m_bestiole == 0) && m_mobile != null)
			{
				if (m_mobile is Sheep)
				{
					m_varsaut.m_bestiole = 1;

					++m_varsaut.m_laitage;
					m_mobile.Stam = m_mobile.Stam - 3;
					from.PlaySound(0X4D1);
					m_varsaut.Name ="Milk Bucket: " + m_varsaut.m_laitage.ToString() + "/50 liters of sheep milk.";
					from.SendMessage(0x96D, "You obtain 1 liter of sheep milk." );
				}
				else if (m_mobile is Goat)
				{
					m_varsaut.m_bestiole = 2;

					++m_varsaut.m_laitage;
					m_mobile.Stam = m_mobile.Stam - 3;
					from.PlaySound(0X4D1);
					m_varsaut.Name ="Milk Bucket: " + m_varsaut.m_laitage.ToString() + "/50 liters of goat milk.";
					from.SendMessage(0x96D, "You obtain 1 liter of goat milk." );
				}
				else if (m_mobile is Cow)
				{
					m_varsaut.m_bestiole = 3;

					++m_varsaut.m_laitage;
					m_mobile.Stam = m_mobile.Stam - 3;
					from.PlaySound(0X4D1);
					m_varsaut.Name ="Milk Bucket: " + m_varsaut.m_laitage.ToString() + "/50 liters of cow milk.";
					from.SendMessage(0x96D, "You obtain 1 liter of cow milk." );
				}
				else
				{
					from.SendMessage (0x96D,"You can obtain milk only from sheep, goats or cows!");
					from.CloseGump( typeof( LaitageHelpGump ) );
					from.SendGump( new LaitageHelpGump ( from ) );
				}
			}
			else if ( m_mobile != null && (m_varsaut.m_laitage <= 49))
			{
				if (m_mobile.Stam > 3)
				{
					if ((m_mobile is Cow) && (m_varsaut.m_bestiole == 3 ))
					{
						++m_varsaut.m_laitage;
						m_mobile.Stam = m_mobile.Stam - 3;
						from.PlaySound(0X4D1);
						m_varsaut.Name ="Milk Bucket: " + m_varsaut.m_laitage.ToString() + "/50 liters of cow milk.";
						from.SendMessage(0x96D, "You obtain 1 liter of cow milk." );
					}
					else if ((m_mobile is Goat) && (m_varsaut.m_bestiole == 2 ))
					{
						++m_varsaut.m_laitage;
						m_mobile.Stam = m_mobile.Stam - 3;
						from.PlaySound(0X4D1);
						m_varsaut.Name ="Milk Bucket: " + m_varsaut.m_laitage.ToString() + "/50 liters of goat milk.";
						from.SendMessage(0x96D, "You obtain 1 liter of goat milk." );
					}
					else if ((m_mobile is Sheep) && (m_varsaut.m_bestiole == 1 ))
					{
						++m_varsaut.m_laitage;
						m_mobile.Stam = m_mobile.Stam - 3;
						from.PlaySound(0X4D1);
						m_varsaut.Name ="Milk Bucket: " + m_varsaut.m_laitage.ToString() + "/50 liters of sheep milk.";
						from.SendMessage(0x96D, "You obtain 1 liter of sheep milk." );
					}
					else
					{
						from.SendMessage(0x84B, "You can't obtain milk from that!" );
						from.CloseGump( typeof( LaitageHelpGump ) );
						from.SendGump( new LaitageHelpGump ( from ) );
					}
				}
				else
				{
					from.SendMessage(0x84B, "This animal is too tired to give more milk!" );
				}
			}
			else if ((o is Bottle ) && ( m_varsaut.m_laitage > 0 ) && pack.ConsumeTotal( typeof( Bottle ), 1 ))
			{
				if (m_varsaut.m_bestiole == 3)
				{
					m_varsaut.m_laitage = m_varsaut.m_laitage -1;
					from.SendMessage (0x96D,"You fill a bottle with cow milk.");
					m_varsaut.Name ="Milk Bucket: " + m_varsaut.m_laitage.ToString() + "/50 liters of cow milk.";
					from.PlaySound(0X240);
					from.AddToBackpack( new BottleCowMilk() );
				}
				else if (m_varsaut.m_bestiole == 2 )
				{
					m_varsaut.m_laitage = m_varsaut.m_laitage -1;
					from.SendMessage (0x96D,"You fill a bottle with goat milk.");
					from.PlaySound(0X240);
					m_varsaut.Name ="Milk Bucket: " + m_varsaut.m_laitage.ToString() + "/50 liters of goat milk.";
					from.AddToBackpack( new BottleGoatMilk() );
				}
				else if (m_varsaut.m_bestiole == 1 )
				{
					m_varsaut.m_laitage = m_varsaut.m_laitage -1;
					from.SendMessage (0x96D,"You fill a bottle with sheep milk.");
					m_varsaut.Name ="Milk Bucket: " + m_varsaut.m_laitage.ToString() + "/50 liters of sheep milk.";
					from.PlaySound(0X240);
					from.AddToBackpack( new BottleSheepMilk() );
				}
				else
				{
					from.SendMessage (0x84B,"This isn't a bottle or the milk bucket is empty.");
					from.CloseGump( typeof( LaitageHelpGump ) );
					from.SendGump( new LaitageHelpGump ( from ) );
				}

				if ( m_varsaut.m_laitage <= 0 )
				{
					m_varsaut.m_bestiole = 0;
					m_varsaut.Name = "Milk Bucket: (Empty)";
				}
			}
			else if ( ( o is BaseBeverage ) )
			{
				BaseBeverage p = (BaseBeverage)o;

				if  (( m_varsaut.m_laitage >= p.MaxQuantity ) && ( p.Quantity == 0 ) )
				{
					if (m_varsaut.m_bestiole == 3)
					{
						p.Content = BeverageType.Milk;
						p.Quantity = p.MaxQuantity;
						m_varsaut.m_laitage = m_varsaut.m_laitage - p.MaxQuantity;
						from.SendMessage (0x96D,"You fill the container with cow milk.");
						m_varsaut.Name ="Milk Bucket: " + m_varsaut.m_laitage.ToString() + "/50 liters of cow milk.";
						from.PlaySound(0X240);

					}
					else if (m_varsaut.m_bestiole == 2 )
					{
						p.Content = BeverageType.Milk;
						p.Quantity = p.MaxQuantity;
						m_varsaut.m_laitage = m_varsaut.m_laitage - p.MaxQuantity;
						from.SendMessage (0x96D,"You fill the container with goat milk.");
						from.PlaySound(0X240);
						m_varsaut.Name ="Milk Bucket: " + m_varsaut.m_laitage.ToString() + "/50 liters of goat milk.";
					}
					else if (m_varsaut.m_bestiole == 1 )
					{
						p.Content = BeverageType.Milk;
						p.Quantity = p.MaxQuantity;
						m_varsaut.m_laitage = m_varsaut.m_laitage - p.MaxQuantity;
						from.SendMessage (0x96D,"You fill the container with sheep milk.");
						m_varsaut.Name ="Milk Bucket: " + m_varsaut.m_laitage.ToString() + "/50 liters of sheep milk.";
						from.PlaySound(0X240);
					}
					else
					{
						from.SendMessage (0x84B,"This isn't a drink container, drink container has liquid in it already, or the milk bucket doesn't have enough milk left in it.");
						from.CloseGump( typeof( LaitageHelpGump ) );
						from.SendGump( new LaitageHelpGump ( from ) );
					}

				}

				if ( m_varsaut.m_laitage <= 0 )
				{
					m_varsaut.m_laitage = 0;
					m_varsaut.m_bestiole = 0;
					m_varsaut.Name = "Milk Bucket: (Empty)";
				}

			}

			else if (o is CheeseForm )
			{
				CheeseForm m_MouleVar =(CheeseForm)o;

				if (m_varsaut.m_laitage >= 15 && m_MouleVar.m_MoulePlein == false )
				{
					m_varsaut.m_laitage = m_varsaut.m_laitage -15;

					if (m_varsaut.m_bestiole == 1 )
					{
						m_varsaut.Name ="Milk Bucket: " + m_varsaut.m_laitage.ToString() + "/50 liters of sheep milk.";
						m_MouleVar.m_FromAfaire = 1;
						m_MouleVar.Name="Cheese form: Sheep cheese";
						m_MouleVar.m_MoulePlein = true;
					}
					else if (m_varsaut.m_bestiole == 2 )
					{
						m_varsaut.Name ="Milk Bucket: " + m_varsaut.m_laitage.ToString() + "/50 liters of goat milk.";
						m_MouleVar.m_FromAfaire = 2;
						m_MouleVar.Name="Cheese form: Goat cheese";
						m_MouleVar.m_MoulePlein = true;
					}
					else if (m_varsaut.m_bestiole == 3 )
					{
						m_varsaut.Name ="Milk Bucket: " + m_varsaut.m_laitage.ToString() + "/50 liters of cow milk.";
						m_MouleVar.m_FromAfaire = 3;
						m_MouleVar.Name="Cheese form: Cow cheese";
						m_MouleVar.m_MoulePlein = true;
					}
					else
					{
						from.SendMessage(0x84C, "This milk bucket is bad." );
						from.CloseGump( typeof( CheeseFormHelpGump ) );
						from.SendGump( new CheeseFormHelpGump ( from ) );

					}

					if ( m_varsaut.Laitage <= 0 )
					{
						m_varsaut.m_bestiole = 0;
						m_varsaut.Name = "Milk Bucket: (Empty)";
					}
				}
				else
				{
					from.SendMessage(0x84C, "The milk bucket didn't contain enough milk." );
					from.CloseGump( typeof( CheeseFormHelpGump ) );
					from.SendGump( new CheeseFormHelpGump ( from ) );

				}
			}

			else
			{
				from.CloseGump( typeof( LaitageHelpGump ) );
				from.SendGump( new LaitageHelpGump ( from ) );
			}
		}
	}
}

