using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using System.Collections;

namespace Server.Items
{
	[Flipable( 0x14F0, 0x14EF )]
	public class RanchExtensionDeed : Item
	{
		[Constructable]
		public RanchExtensionDeed() : base( 0x14F0)
		{
			Name = "a ranch extension deed";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if (this.IsChildOf( from.Backpack ))
			{
				from.SendMessage("Target ranch stone.");
				from.Target = new RanchTarget(this);
			}
			else from.SendMessage("That must be in your backpack to use");
		}

		private class RanchTarget : Target
		{
			private RanchExtensionDeed t_re;

			public RanchTarget(RanchExtensionDeed d) : base( 10, false, TargetFlags.None )
			{
				t_re = d;
			}
	
			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is RanchStone)
				{
					RanchStone rs = (RanchStone) targ;
					if ( from.InRange( rs.GetWorldLocation(), 2 ) )
					{
						if (rs.Owner == from)
						{
							if ((rs.Size + 5) < 50) 
							{
								t_re.Delete();
								rs.Size += 5;
								rs.InvalidateProperties();
								from.SendMessage("Your ranch has been enlarged.");
							}
							else from.SendMessage("Your ranch is already at maximum size!");
						}
						else from.SendMessage("You don't own that ranchstone!");
					}
					else
						from.SendMessage("That's too far away!");
				}
			}
		}

		public RanchExtensionDeed( Serial serial ) : base( serial )
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