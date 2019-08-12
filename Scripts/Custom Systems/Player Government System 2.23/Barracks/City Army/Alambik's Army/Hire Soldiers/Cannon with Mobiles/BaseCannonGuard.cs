using System;
using Server.Items;

namespace Server.Mobiles
{
	public class BaseCannonGuard : BaseSoldier
	{
		public BaseCannon m_Cannon;
		private DateTime NextFire, NextThink;
		
		public BaseCannon Cannon{ get{ return m_Cannon;} set{ m_Cannon = value; } }
		
		public BaseCannonGuard(/*AIType AI*/) //: base( /*AIType.AI_Archer*/AI, FightMode.Evil, 15, 1, 0.2, 0.4 )
		{
                        ControlSlots = 3;
                        this.AI = AIType.AI_Archer;
			//Title = "the cannoneer";
			//this.CantWalk = true;
		}
				
		public override void OnCombatantChange()
		{
			if( Combatant != null && m_Cannon != null)
			{
				FireCanon(Combatant);
			}
		}
		
		public override void OnDelete()
		{
			if( m_Cannon != null )
				m_Cannon.Delete();
			base.OnDelete();
		}
		
		public override void OnDeath(Container c)
		{
			m_Cannon.Delete();
			base.OnDeath(c);
		}
  
		/*public override bool HandlesOnSpeech( Mobile from )
		{
			//if ( from.AccessLevel >= AccessLevel.GameMaster )
				return true;

			return base.HandlesOnSpeech( from );
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			base.OnSpeech( e );

			if (this.ControlMaster == e.Mobile)
			{
				if (e.Speech == "deploy")
				{
                                   //if( m_Cannon == null )
			          // {
                                    this.Say("As you command.");
				    m_Cannon = new CannonNorth(this);
				    Direction = Direction.North;
				    Cannon.MoveToWorld( new Point3D(X,Y - 3,Z), Map);
			//}
					//m_Timer.Stop();
					//m_Timer.Delay = TimeSpan.FromSeconds( Utility.Random(1, 5) );
					//m_Timer.Start();
				}
			}
		}*/
		public override void OnThink()
		{
			base.OnThink();
			if( NextThink > DateTime.Now )
				return;
			if( m_Cannon == null )
			{
                                 this.Say("My weapon is deployed.");
				m_Cannon = new CannonNorth(this);
				Direction = Direction.North;
				Cannon.MoveToWorld( new Point3D(X,Y - 3,Z), Map);
			}
			
			if( m_Cannon.Deleted )
				Delete();
			
			if( Combatant == null )
				return;
			
			NextThink = DateTime.Now + TimeSpan.FromSeconds(2.5);
			FireCanon(Combatant);
		}
		
		public virtual void FireCanon(Mobile target)
		{
                        if( m_Cannon != null )
			{
			if( NextFire > DateTime.Now )
				return;
			NextFire = DateTime.Now + TimeSpan.FromSeconds(10);
			m_Cannon.CCom.CheckFiringAngle(target.Location,this);
                        }
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}
		
		public BaseCannonGuard( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
			writer.Write( m_Cannon );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			m_Cannon = (BaseCannon)reader.ReadItem();
		}
	}
}
