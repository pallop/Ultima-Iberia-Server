using System;

namespace Server.Items
{
	public class TacticBerry : Item
	{
		private TimedSkillMod skillMod; 

		[Constructable]
		public TacticBerry() : base( 0x9D0 )
		{
            this.Weight = 1.0;
            this.DefineMods();
            this.Name = "Tactician's Berry";
            this.Stackable = false;
            this.Hue = 0x7;
            	}

		private void DefineMods()
		{
			DateTime duration = DateTime.UtcNow + TimeSpan.FromSeconds( 60.0 );

			skillMod = new TimedSkillMod( SkillName.Tactics, true, 10.0, duration );

			skillMod.ObeyCap = false;
		}

		private void SetMods( Mobile m )
		{
			m.AddSkillMod( skillMod );
		}

      		public TacticBerry( Serial serial ) : base( serial ) 
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
					from.PlaySound( 0x3A );
					Delete();
				}
			}
		}

	}
}