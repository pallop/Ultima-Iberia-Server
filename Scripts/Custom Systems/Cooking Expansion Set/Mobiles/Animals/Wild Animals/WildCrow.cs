using System;
using Server.Items;
using Server.Mobiles;
using Server.Items.Crops;
using System.Collections;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "a crow corpse" )]
	public class WildCrow : BaseCreature
	{
		[Constructable]
		public WildCrow() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a Crow";
			Body = 5;
			BaseSoundID = 0x2EE;
			Hue = 1109;

			SetStr( 41, 53 );
			SetDex( 42, 63 );
			SetInt( 11, 25 );

			SetHits( 31, 47 );
			SetMana( 0 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Cold, 20, 25 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 17.8, 34.0 );
			SetSkill( SkillName.Tactics, 19.1, 38.0 );
			SetSkill( SkillName.Wrestling, 43.1, 57.0 );

			Fame = 300;
			Karma = 0;

			VirtualArmor = 22;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 17.1;

			PackGold( 25, 50 );
			PackItem( new BlackPearl( 4 ) );
		}

		private DateTime m_NextEat;

		public override void OnThink()
		{
			base.OnThink();
			if ( this.Alive == false ) return;
			if (this.Controlled == true) return;
			if ( DateTime.UtcNow < m_NextEat ) return;
			m_NextEat = DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( 15, 30 ) );
			ArrayList list = new ArrayList();
			foreach ( Item item in this.GetItemsInRange( 2 ) )
			{
                if (item is Item) list.Add(item);
			}
			if (list.Count == 0) return;
			foreach ( Item scare in this.GetItemsInRange( 12 ) )
			{
				//if ( scare is Scarecrow )
				if ( scare.ItemID == 7732 || scare.ItemID == 7733 )
				{
					this.X = this.X + Utility.Random(21) - 10;
					this.Y = this.Y + Utility.Random(21) - 10;
					this.Z = this.Z + 10;
					this.Say("Caw Caw aaakkkkkk Caw Caw Caw");
					return;
				}
			}
			
			int toEat = 0;
			for ( int i = 0; i < list.Count; ++i )
			{
				Item item = (Item)list[i];
				//BaseCrop plant = item as BaseCrop;
				//if ( plant == null) ; // do nothing
				//else
                if (item != null) //( plant != null)
				{
					if (item is AsparagusCrop ) 
                    {
                        AsparagusCrop ac = (AsparagusCrop)item;
                        ac.Yield = 0;
                        toEat += 1; 
                    }
					else if (item is BananaCrop ) 
                    {
                    
                        BananaCrop bc = (BananaCrop)item;
                        bc.Yield = 0;
                        toEat += 1; 
                    }
				}
				if (toEat > 0) this.Say("Caw Caw Caw mmmmm Caw Caw");
			}
		}

		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Feathers{ get{ return 36; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish | FoodType.FruitsAndVegies; } }

		public WildCrow(Serial serial) : base(serial){}
		public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int) 0); }
		public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
	}
}