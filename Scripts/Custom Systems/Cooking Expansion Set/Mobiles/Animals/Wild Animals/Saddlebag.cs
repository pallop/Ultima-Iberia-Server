using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using System.Collections;

namespace Server.Items
{
	public class Saddlebag : Item
	{
		[Constructable]
		public Saddlebag() : base( 0x9B2 )
		{
			Name = "a saddle bag";
			Weight = 5.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			bool CanUse = from.CheckSkill( SkillName.AnimalTaming, 30, 80 );
			if (CanUse)
			{
				if (this.IsChildOf( from.Backpack ))
				{
					from.SendMessage("Target Horse or Llama");
					from.Target = new BagTarget(this);
				}
				else
					from.SendMessage("That must be in your backpack to use");
			}
			else
			{
				from.SendMessage("You don't quite understand how to use it.");
			}
		}

		private class BagTarget : Target
		{
			private Saddlebag sb;

			public BagTarget(Saddlebag s) : base( 10, false, TargetFlags.None )
			{
				sb = s;
			}
			
			public virtual void ConvertAnimal(BaseCreature from, BaseCreature to)
			{
				to.Location = from.Location;
				to.Name = from.Name;
				to.Title = from.Title;
				to.Hits = from.HitsMax;
				to.DamageMin = from.DamageMin;
				to.DamageMax = from.DamageMax;
				to.Str = from.Str;
				to.Dex = from.Dex;
				to.Int = from.Int;							
				for ( int i = 0; i < from.Skills.Length; ++i )
				{
					to.Skills[i].Base = from.Skills[i].Base;
					to.Skills[i].Cap = from.Skills[i].Cap;
				}
				to.ControlOrder = from.ControlOrder;
				to.ControlTarget = from.ControlTarget;
				to.Controlled = from.Controlled;
				to.ControlMaster = from.ControlMaster;
				to.MoveToWorld(from.Location, from.Map);
				from.Delete();
			}
	
			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is BaseCreature)
				{
					BaseCreature bc = (BaseCreature) targ;
					if ( from.InRange( bc, 1 ) )
					{
						if (bc.ControlMaster != from)
							from.SendMessage("You can only put a pack on your own animal!");
						else
						{
							if (targ is WildHorse)
							{
								PackHorse ph = new PackHorse();
								ConvertAnimal(bc, ph);
								sb.Consume();
							}
							else if (targ is Llama)
							{
								PackLlama pl = new PackLlama();
								ConvertAnimal(bc, pl);
								sb.Consume();
							}
							else
								from.SendMessage("You can't put a pack on that.");
						}
					}
					else
						from.SendMessage("That's too far away");
				}
				else
					from.SendMessage("You can't put a pack on that.");
			}
		}

		public Saddlebag( Serial serial ) : base( serial )
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