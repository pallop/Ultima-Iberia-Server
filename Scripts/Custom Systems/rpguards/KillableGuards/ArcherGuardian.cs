using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
	public class ArcherGuardian : BaseGuardian
	{ 


		[Constructable] 
		public ArcherGuardian() : base( AIType.AI_Archer, FightMode.Weakest, 15, 5, 0.1, 0.2 ) 
		{ 
			Title = "the Soldier, Valorian Militia"; 

			AddItem( new Bow() );
			AddItem( new Boots() );
			AddItem( new Bandana(32) );
			AddItem( new Cloak(32) );
			AddItem( new LeatherGloves() );
			AddItem( new BodySash(32) );

			SetStr( 60, 66 );
			SetDex( 60, 66 );
			SetInt( 61, 67 );

			SetSkill( SkillName.Anatomy, 80.0, 80.0 );
			SetSkill( SkillName.Archery, 80.0, 80.0 );
			SetSkill( SkillName.Tactics, 80.0, 80.0 );
			SetSkill( SkillName.MagicResist, 80.0, 80.0 );

			if ( Female = Utility.RandomBool() ) 
			{ 
				Body = 401; 
				Name = NameList.RandomName( "female" );
				
				AddItem( new LeatherSkirt() );
				AddItem( new FemaleLeatherChest() );
				
			}
			else 
			{ 
				Body = 400; 			
				Name = NameList.RandomName( "male" );

				AddItem(new LeatherChest());
				AddItem(new LeatherLegs());
				AddItem(new LeatherArms());

			}
			
			Utility.AssignRandomHair( this );
		}

		public ArcherGuardian( Serial serial ) : base( serial ) 
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