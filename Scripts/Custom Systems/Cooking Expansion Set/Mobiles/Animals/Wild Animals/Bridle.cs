using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using System.Collections;

namespace Server.Items
{
	public class Bridle : Item
	{
		[Constructable]
		public Bridle() : base( 0x1374 )
		{
			Name = "a bridal";
			Weight = 5.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			bool CanUse = from.CheckSkill( SkillName.AnimalTaming, 30, 80 );
			if (CanUse)
			{
				if (this.IsChildOf( from.Backpack ))
				{
					from.SendMessage("Target wild mount");
					from.Target = new BridleTarget(this);
				}
				else
					from.SendMessage("That must be in your backpack to use");
			}
			else
			{
				from.SendMessage("You don't quite understand how to use it.");
			}
		}

		private class BridleTarget : Target
		{
			private Bridle br;

			public BridleTarget(Bridle b) : base( 10, false, TargetFlags.None )
			{
				br = b;
			}
			
			public virtual void ConvertAnimal(BaseCreature from, BaseCreature to)
			{
				to.Location = from.Location;
				to.Name = from.Name;
				to.Title = from.Title;
				to.Hue = from.Hue;
				to.Body = from.Body;
				to.Hits = from.Hits;
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
							from.SendMessage("You can only put a bridle on your own animal!");
						else
						{
							if (targ is WildHorse)
							{
								Horse h = new Horse();
								h.Body = bc.Body;
								ConvertAnimal(bc, h);
								br.Consume();
							}
							else if (targ is Llama)
							{
								RidableLlama rl = new RidableLlama();
								ConvertAnimal(bc, rl);
								br.Consume();
							}
							else if (targ is WildDesertOstard)
							{
								DesertOstard dost = new DesertOstard();
								ConvertAnimal(bc, dost);
								br.Consume();
							}
							else if (targ is WildFireSteed)
							{
								FireSteed fs = new FireSteed();
								ConvertAnimal(bc, fs);
								br.Consume();
							}
							else if (targ is WildForestOstard)
							{
								ForestOstard fost = new ForestOstard();
								ConvertAnimal(bc, fost);
								br.Consume();
							}
							else if (targ is WildFrenziedOstard)
							{
								FrenziedOstard fzost = new FrenziedOstard();
								ConvertAnimal(bc, fzost);
								br.Consume();
							}
							else if (targ is WildHiryu)
							{
								Hiryu hi = new Hiryu();
								ConvertAnimal(bc, hi);
								br.Consume();
							}
							else if (targ is WildLesserHiryu)
							{
								LesserHiryu lh = new LesserHiryu();
								ConvertAnimal(bc, lh);
								br.Consume();
							}
							else if (targ is WildNightmare)
							{
								Nightmare nm = new Nightmare();
								ConvertAnimal(bc, nm);
								br.Consume();
							}
							else if (targ is WildRidgeback)
							{
								Ridgeback rb = new Ridgeback();
								ConvertAnimal(bc, rb);
								br.Consume();
							}
							else if (targ is WildSavageRidgeback)
							{
								SavageRidgeback sr = new SavageRidgeback();
								ConvertAnimal(bc, sr);
								br.Consume();
							}
							else if (targ is WildSilverSteed)
							{
								SilverSteed ss = new SilverSteed();
								ConvertAnimal(bc, ss);
								br.Consume();
							}
							else if (targ is WildSwampDragon)
							{
								SwampDragon sd = new SwampDragon();
								ConvertAnimal(bc, sd);
								br.Consume();
							}
							else
								from.SendMessage("You can't put a bridle on that.");
						}
					}
					else
						from.SendMessage("That's too far away");
				}
				else
					from.SendMessage("You can't put a bridle on that.");
			}
		}

		public Bridle( Serial serial ) : base( serial )
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