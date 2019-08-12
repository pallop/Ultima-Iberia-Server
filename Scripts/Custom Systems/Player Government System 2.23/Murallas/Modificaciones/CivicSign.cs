using System; 
using Server;
using Server.Gumps;
using System.Collections;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{ 
	public class CivicSign : Item 
	{
		private ArrayList m_toDelete;
		private CityManagementStone m_Stone;
		private CivicSignType m_Type;
		private Mobile m_LandlordRemove;
		
		public Mobile LandlordRemove
		{
			get{ return m_LandlordRemove; }
			set{ m_LandlordRemove = value; }
		}

		public ArrayList toDelete
		{
			get{ return m_toDelete; }
			set{ m_toDelete = value; }
		}

		public CityManagementStone Stone
		{
			get{ return m_Stone; }
			set{ m_Stone = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public CivicSignType Type
		{
			get{ return m_Type; }
			set{ m_Type = value; }
		}

		public CivicSign() : base( 3023 ) 
		{ 
			Movable = false; 
			Name = "a civic building sign"; 
		} 

		public override void OnDoubleClick( Mobile from ) 
		{
			if ( from == m_Stone.Mayor && from.InRange( this.GetWorldLocation(), 5 ) )
				//from.SendGump( new DestoryCityStructureGump( this, from ) );
{
                            from.SendGump( new DestoryCityStructureGump( this, from ) );
                        }
                        else if ( from == m_Stone.AssistMayor && this.Type == CivicSignType.Barracks && from.InRange( this.GetWorldLocation(), 5 ) || from == m_Stone.General && this.Type == CivicSignType.Barracks && from.InRange( this.GetWorldLocation(), 5 ))
                        {
                            from.SendMessage( "Only the Mayor may remove buildings at this point ask them to remove it." );
                        }
			else
				from.SendMessage( "You cannot access that." );
		} 

		public CivicSign( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void OnDelete()
		{
			if ( toDelete != null ) // Delete all items needed
			{
				foreach( Item i in toDelete )
				{
					i.Delete();
				}
			}

			if ( Type == CivicSignType.Bank )
			{
				m_Stone.HasBank = false;
			}
			else if ( Type == CivicSignType.Stable )
			{
				m_Stone.HasStable = false;
			}
			else if ( Type == CivicSignType.Tavern )
			{
				m_Stone.HasTavern = false;
			}
			else if ( Type == CivicSignType.Healer )
			{
				m_Stone.HasHealer = false;
			}
			else if ( Type == CivicSignType.Moongate )
			{
				m_Stone.HasMoongate = false;
			}
			else if ( Type == CivicSignType.Garden )
			{
				if ( m_Stone.Gardens.Contains( this ) )
					m_Stone.Gardens.Remove( this );
			}
			else if ( Type == CivicSignType.Park )
			{
				if ( m_Stone.Parks.Contains( this ) )
					m_Stone.Parks.Remove( this );
			}
            else if ( Type == CivicSignType.Barracks )
			{
				if ( m_Stone.Barracks.Contains( this ) )
					m_Stone.Barracks.Remove( this );
			}
			else if ( Type == CivicSignType.Market )
			{
				m_Stone.HasMarket = false;
				m_LandlordRemove.Delete();
			}
//murallas
			else if ( Type == CivicSignType.EmpalizadaEscaleraNorte )
			{
				m_LandlordRemove.Delete();
			}

			else if ( Type == CivicSignType.EmpalizadaNorte )
			{
				m_LandlordRemove.Delete();
			}

			else if ( Type == CivicSignType.EmpalizadaPuertaN )
			{
				m_LandlordRemove.Delete();
			}

			else if ( Type == CivicSignType.EmpalizadaTorreEsquinera )
			{
				m_LandlordRemove.Delete();
			}

			else if ( Type == CivicSignType.EmpalizadaEscaleraWest )
			{
				m_LandlordRemove.Delete();
			}

			else if ( Type == CivicSignType.EmpalizadaWest )
			{
				m_LandlordRemove.Delete();
			}
			else if ( Type == CivicSignType.EmpalizadaEscaleraEst )
			{
				m_LandlordRemove.Delete();
			}
			else if ( Type == CivicSignType.EmpalizadaEst )
			{
				m_LandlordRemove.Delete();
			}
			else if ( Type == CivicSignType.EmpalizadaEscaleraSur )
			{
				m_LandlordRemove.Delete();
			}
			else if ( Type == CivicSignType.EmpalizadaSur )
			{
				m_LandlordRemove.Delete();
			}
			else if ( Type == CivicSignType.EmpalizadaPuertaEst )
			{
				m_LandlordRemove.Delete();
			}
			else if ( Type == CivicSignType.EmpalizadaPuertaWest )
			{
				m_LandlordRemove.Delete();
			}
			else if ( Type == CivicSignType.EmpalizadaPuertaSur )
			{
				m_LandlordRemove.Delete();
			}


			//Fin murallas

			base.OnDelete();
		}

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 1 ); // version 

			writer.Write( m_LandlordRemove );
			writer.WriteItemList( m_toDelete, true );

			writer.Write( m_Stone );
			writer.Write( (int) m_Type );
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
					{
						m_LandlordRemove = reader.ReadMobile();
						goto case 0;
					}
				
				
				case 0:
				{
					m_toDelete = reader.ReadItemList();
					m_Stone = (CityManagementStone)reader.ReadItem();
					m_Type = (CivicSignType)reader.ReadInt();

					break;
				}
			}
		} 
	} 
} 
