using System;

namespace Server.Items
{
	[FlipableAttribute( 0x170D, 0x170E )]
	public class SandalsOfAnimalLore : BaseShoesOfAnimalLore
	{
		[Constructable]
		public SandalsOfAnimalLore( int bonus ) : base( bonus, 0x170D )
		{
			Weight = 2;
			Name = "tamers sandals of animal lore";
			Hue = Utility.RandomMinMax( 1150, 1175);
		}

		public SandalsOfAnimalLore( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public abstract class BaseShoesOfAnimalLore : BaseShoes
	{
		private int m_Bonus;
		private SkillMod m_SkillMod;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Bonus
		{
			get
			{
				return m_Bonus;
			}
			set
			{
				m_Bonus = value;
				InvalidateProperties();

				if ( m_Bonus == 0 )
				{
					if ( m_SkillMod != null )
						m_SkillMod.Remove();

					m_SkillMod = null;
				}
				else if ( m_SkillMod == null && Parent is Mobile )
				{
					m_SkillMod = new DefaultSkillMod( SkillName.AnimalLore, true, m_Bonus );
					((Mobile)Parent).AddSkillMod( m_SkillMod );
				}
				else if ( m_SkillMod != null )
				{
					m_SkillMod.Value = m_Bonus;
				}
			}
		}

		public override void OnAdded( object parent )
		{
			base.OnAdded( parent );

			if ( m_Bonus != 0 && parent is Mobile )
			{
				if ( m_SkillMod != null )
					m_SkillMod.Remove();

				m_SkillMod = new DefaultSkillMod( SkillName.AnimalLore, true, m_Bonus );
				((Mobile)parent).AddSkillMod( m_SkillMod );
			}
		}

		public override void OnRemoved( object parent )
		{
			base.OnRemoved( parent );

			if ( m_SkillMod != null )
				m_SkillMod.Remove();

			m_SkillMod = null;
		}

		public BaseShoesOfAnimalLore( int bonus, int itemID ) : base( itemID )
		{
			m_Bonus = bonus;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Bonus != 0 )
				list.Add( 1060658, "animal lore bonus\t+{0}", m_Bonus.ToString() );
		}

		public BaseShoesOfAnimalLore( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Bonus );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Bonus = reader.ReadInt();
					break;
				}
			}

			if ( m_Bonus != 0 && Parent is Mobile )
			{
				if ( m_SkillMod != null )
					m_SkillMod.Remove();

				m_SkillMod = new DefaultSkillMod( SkillName.Veterinary, true, m_Bonus );
				((Mobile)Parent).AddSkillMod( m_SkillMod );
			}
		}
	}
}