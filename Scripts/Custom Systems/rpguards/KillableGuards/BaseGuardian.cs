using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles;


namespace Server.Mobiles 
{ 
	public class BaseGuardian : BaseCreature 
	{ 

		public BaseGuardian(AIType ai, FightMode fm, int PR, int FR, double AS, double PS) : base( ai, fm, PR, FR, AS, PS )
		{
			SpeechHue = Utility.RandomDyedHue(); 
			Hue = Utility.RandomSkinHue();
		}

		public override bool IsEnemy( Mobile m )
		{
            if (m is BaseGuardian || m is BaseVendor || m is PlayerVendor || m is TownCrier )

				return false;

			if ( m is PlayerMobile && !m.Criminal )

				return false;

			if (m is BaseCreature)
			{
				BaseCreature c = (BaseCreature)m;

				if( c.Controlled || c.FightMode == FightMode.Aggressor || c.FightMode == FightMode.None )

					return false;
			}	

			return true;
		}

		public BaseGuardian( Serial serial ) : base( serial ) 
		{ 
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
	} 
}   