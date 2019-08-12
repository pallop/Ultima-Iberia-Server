using System;
using Server;

namespace Server.Items
{
	public class FirePit : BaseLight
	{
		public override int LitItemID{ get { return 0xFAC; } }
		
		[Constructable]
		public FirePit() : base( 0xFAC )
		{
			Movable = false;
			Duration = TimeSpan.Zero; // Never burnt out
			Burning = true;
			Light = LightType.Circle225;
			Weight = 20.0;
		}

		public FirePit( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}