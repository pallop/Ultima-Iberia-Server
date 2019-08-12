using System;

namespace Server.Items
{
	public class ElixirOfStrength : Item
	{
		private StatMod m_StatMod0; 

		[Constructable]
		public ElixirOfStrength() : base( 0xE2C )
		{
            Weight = 1.0;
            Hue = 0x65A;
            DefineMods();
            Name = "Elixir of Strength";
        }

		private void DefineMods()
		{
			m_StatMod0 = new StatMod( StatType.Str, "Elixir of Strength", 1, TimeSpan.Zero );
		}

		private void SetMods( Mobile m )
		{
			m.AddStatMod( m_StatMod0 );
		}

      		public ElixirOfStrength( Serial serial ) : base( serial ) 
      		{
			DefineMods();
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

      		public override void OnDoubleClick( Mobile from ) 
      		{ 
         		if ( !from.InRange( GetWorldLocation(), 1 ) ) 
         		{ 
            			from.SendLocalizedMessage( 500446 ); // That is too far away. 
         		} 
         		else 
			{
				if ( from.Mounted == true )
				{
					from.SendLocalizedMessage( 1042561 );
				}
				else
        			{
					SetMods( from );
					Delete();
				}
			}
		}
	}

	public class ElixirOfDexterity : Item
	{
		private StatMod m_StatMod0; 

		[Constructable]
		public ElixirOfDexterity() : base( 0xE2C )
		{
            Weight = 1.0;
            Hue = 0x164;
            DefineMods();
            Name = "Elixir of Dexterity";
        }

		private void DefineMods()
		{
			m_StatMod0 = new StatMod( StatType.Dex, "Elixir of Dexterity", 1, TimeSpan.Zero );
		}

		private void SetMods( Mobile m )
		{
			m.AddStatMod( m_StatMod0 );
		}

      		public ElixirOfDexterity( Serial serial ) : base( serial ) 
      		{
			DefineMods();
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

      		public override void OnDoubleClick( Mobile from ) 
      		{ 
         		if ( !from.InRange( GetWorldLocation(), 1 ) ) 
         		{ 
            			from.SendLocalizedMessage( 500446 ); // That is too far away. 
         		} 
         		else 
			{
				if ( from.Mounted == true )
				{
					from.SendLocalizedMessage( 1042561 );
				}
				else
        			{
					SetMods( from );
					Delete();
				}
			}
		}
	}

    public class ElixirOfIntelligence : Item
    {
        private StatMod m_StatMod0;

        [Constructable]
        public ElixirOfIntelligence()
            : base(0xE2C)
        {
            Weight = 1.0;
            Hue = 0x18C;
            DefineMods();
            Name = "Elixir of Intelligence";
        }

        private void DefineMods()
        {
            m_StatMod0 = new StatMod(StatType.Int, "Elixir of Intelligence", 1, TimeSpan.Zero);
        }

        private void SetMods(Mobile m)
        {
            m.AddStatMod(m_StatMod0);
        }

        public ElixirOfIntelligence(Serial serial)
            : base(serial)
        {
            DefineMods();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(GetWorldLocation(), 1))
            {
                from.SendLocalizedMessage(500446); // That is too far away. 
            }
            else
            {
                if (from.Mounted == true)
                {
                    from.SendLocalizedMessage(1042561);
                }
                else
                {
                    SetMods(from);
                    Delete();
                }
            }
        }
    }
}