using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
	public class MageGuardian : BaseGuardian 
	{ 

		[Constructable] 
		public MageGuardian() : base( AIType.AI_Mage, FightMode.Weakest, 10, 5, 0.1, 0.2 ) 
		{ 
			Title = "the Battle Mage, Valorian Militia"; 

			AddItem( new Boots() );
			AddItem( new Bandana(32) );
			AddItem( new Cloak(32) );
			AddItem( new LeatherGloves() );
			AddItem( new BodySash(32) );
			AddItem(new LeatherLegs());

			SetStr( 60, 66 );
			SetDex( 60, 66 );
			SetInt( 60, 66 );

			SetSkill( SkillName.MagicResist, 80.0,80.0 );
			SetSkill( SkillName.Magery, 80.0, 80.0 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 80.0,80.0 );

			if ( Female = Utility.RandomBool() ) 
			{ 
				Body = 401; 
				Name = NameList.RandomName( "female" );
				
				AddItem( new FemaleLeatherChest() );
				


			}
			else 
			{ 
				Body = 400; 			
				Name = NameList.RandomName( "male" ); 
				
				AddItem(new LeatherChest());
				AddItem(new LeatherArms());
				


			}

			Utility.AssignRandomHair( this );
		}


		public MageGuardian( Serial serial ) : base( serial ) 
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