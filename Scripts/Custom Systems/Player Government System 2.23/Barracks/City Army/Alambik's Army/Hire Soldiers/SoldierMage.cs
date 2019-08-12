using System; 
using System.Collections; 
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{ 
    public class SoldierMage : BaseSoldier
    { 
        [Constructable] 
        public SoldierMage()
        { 
            SpeechHue = Utility.RandomDyedHue(); 
            this.AI = AIType.AI_Mage;
            ControlSlots = 3;
            Title = "the mage";
            if ( this.Female = Utility.RandomBool() )
            {
                 //if ( this.Race.Elf = Utility.RandomBool() )
                 //{
                     // Body = 0x25e;
                      //Name = NameList.RandomName( "female" );
                 //}
                 //else
                 //{
                     //Race = Human;
                     Body = 0x191;
                     Name = NameList.RandomName( "female" );
                 //}
            }
            else
            {
                 //if ( this.Race.Elf = Utility.RandomBool() )
                 //{
                      //Body = 0x25d;
                     // Name = NameList.RandomName( "male" );
                 //}
                 //else
                 //{
                     //Race = Human;
                     Body = 0x190;
                     Name = NameList.RandomName( "male" );
                 //}
            }
            Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) ); 
            hair.Hue = Utility.RandomNeutralHue(); 
            hair.Layer = Layer.Hair; 
            hair.Movable = false; 
            AddItem( hair ); 
            this.Age = SoldierAge.Teenage;
            if( Utility.RandomBool() && !this.Female )
            {
                Item beard = new Item( Utility.RandomList( 0x203E, 0x203F, 0x2040, 0x2041, 0x204B, 0x204C, 0x204D ) );

                beard.Hue = hair.Hue;
                beard.Layer = Layer.FacialHair;
                beard.Movable = false;

                AddItem( beard );
            }
Personality = (SoldierPersonality)(Utility.RandomMinMax(0,7));
            Motivation = (SoldierMotivation)(Utility.RandomMinMax(0,4));
            SetStr( 25, 35 );
            SetDex( 20, 30 );
            SetInt( 15, 20 );

            SetDamage( 2, 10 );
 SetSkill( SkillName.Cooking, 15, 20 );
            SetSkill( SkillName.Healing, 15, 20 );
            SetSkill( SkillName.MagicResist, 15, 20 );
		SetSkill( SkillName.Tactics, 15, 20 );
                SetSkill( SkillName.Anatomy, 15, 20 );
		SetSkill( SkillName.Magery, 22, 22 );
                SetSkill( SkillName.EvalInt, 22, 22 );
		//SetSkill( SkillName.Swords, 15, 20 );
		//SetSkill( SkillName.Parry, 15, 20 );
		//SetSkill( SkillName.Macing, 36, 67 );
		SetSkill( SkillName.Focus, 36, 67 );
		SetSkill( SkillName.Wrestling, 25, 47 );
            

            Fame = 100; 
            Karma = 100; 

            //AddItem( new Shoes( Utility.RandomNeutralHue() ) );
            //AddItem( new Shirt());

            // Pick a random sword
            /*switch ( Utility.Random( 5 ))
            { 
                case 0: AddItem( new Longsword() ); break; 
                case 1: AddItem( new Broadsword() ); break; 
                case 2: AddItem( new VikingSword() ); break; 
		case 3: AddItem( new Dagger() ); break;
		case 4: AddItem( new Katana() ); break;
            } 

            // Pick a random shield
            switch ( Utility.Random( 6 ))
            { 
                case 0: AddItem( new BronzeShield() ); break; 
                case 1: AddItem( new HeaterShield() ); break; 
                case 2: AddItem( new MetalKiteShield() ); break; 
                case 3: AddItem( new MetalShield() ); break; 
                case 4: AddItem( new WoodenKiteShield() ); break; 
                case 5: AddItem( new WoodenShield() ); break; 
		//case 6: AddItem( new OrderShield() ); break;
		//case 7: AddItem( new ChaosShield() ); break;
            } 
          
		switch( Utility.Random( 5 ) )
		{
			case 0: break;
			case 1: AddItem( new Bascinet() ); break;
			case 2: AddItem( new CloseHelm() ); break;
			case 3: AddItem( new NorseHelm() ); break;
			case 4: AddItem( new Helmet() ); break;

		}
            // Pick some armour
            switch( Utility.Random( 4 ) )
            {
                case 0: // Leather
                    AddItem( new LeatherChest() );
                    AddItem( new LeatherArms() );
                    AddItem( new LeatherGloves() );
                    AddItem( new LeatherGorget() );
                    AddItem( new LeatherLegs() );
                    break;

                case 1: // Studded Leather
                    AddItem( new StuddedChest() );
                    AddItem( new StuddedArms() );
                    AddItem( new StuddedGloves() );
                    AddItem( new StuddedGorget() );
                    AddItem( new StuddedLegs() );
                    break;

                case 2: // Ringmail
                    AddItem( new RingmailChest() );
                    AddItem( new RingmailArms() );
                    AddItem( new RingmailGloves() );
                    AddItem( new RingmailLegs() );
                    break;

                case 3: // Chain
                    AddItem( new ChainChest() );
                    AddItem( new ChainCoif() );
                    AddItem( new ChainLegs() );
                    break;
            }*/
            Container pack = Backpack;
            if ( pack != null )
            pack.Delete();
            pack = new StrongBackpack();
            pack.Movable = false;
            AddItem( pack );
            //PackItem( new TentDeed() );
        } 
        
        public SoldierMage( Serial serial ) : base( serial )
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
