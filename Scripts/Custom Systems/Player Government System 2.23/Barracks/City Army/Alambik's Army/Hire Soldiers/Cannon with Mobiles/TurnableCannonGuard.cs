/*
 * Created by SharpDevelop.
 * User: UOT
 * Date: 3/29/2005
 * Time: 9:29 PM
 * Version: 1.0.0
 */

using System;
using Server.Items;

namespace Server.Mobiles
{
	public class TurnableCannonGuard : BaseCannonGuard
	{		
		[Constructable]
		public TurnableCannonGuard() : base()
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the cannoneer";
			Hue = Utility.RandomSkinHue();
			//this.CantWalk = true;
			this.Age = SoldierAge.Teenage;
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
			SetStr( 25, 35 );
                        SetDex( 20, 30 );
                        SetInt( 15, 20 );

                        SetDamage( 2, 10 );
			 SetSkill( SkillName.Cooking, 15, 20 );
            SetSkill( SkillName.Healing, 15, 20 );
            SetSkill( SkillName.MagicResist, 15, 20 );
			SetSkill( SkillName.Tactics, 15, 20 );
                        SetSkill( SkillName.Anatomy, 15, 20 );
		        SetSkill( SkillName.Archery, 22, 22 );
		        //SetSkill( SkillName.Swords, 15, 20 );
		        //SetSkill( SkillName.Parry, 15, 20 );
		        //SetSkill( SkillName.Macing, 36, 67 );
		        SetSkill( SkillName.Focus, 36, 67 );
		        SetSkill( SkillName.Wrestling, 25, 47 );
			
			Fame = 100;
			Karma = 100;
			
			//AddItem( new Boots( Utility.RandomNeutralHue() ) );
			//AddItem( new FancyShirt());
			//AddItem( new Bandana());
			
			//AddItem( Server.Items.Hair.GetRandomHair( Female ) );
                        Utility.AssignRandomHair( this );
                        Container pack = Backpack;
            if ( pack != null )
            pack.Delete();
            pack = new StrongBackpack();
            pack.Movable = false;
            AddItem( pack );
            PackItem( new TentDeed() );
                        //PackItem( new CannonNorthDeed() );
		}
		public override bool HandlesOnSpeech( Mobile from )
		{
			//if ( from.AccessLevel >= AccessLevel.GameMaster )
				return true;

			return base.HandlesOnSpeech( from );
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			base.OnSpeech( e );

			if (this.ControlMaster == e.Mobile)
			{
				if (e.Speech == "deploy")
				{
                                   //if( m_Cannon == null )
			          // {
                                    CannonNorthDeed deed = this.Backpack.FindItemByType( typeof(CannonNorthDeed) ) as CannonNorthDeed;
                                      if( deed != null )
			              {
                                         this.Say("As you command.");
                                         this.CantWalk = true;
				         m_Cannon = new CannonNorth(this);
				         Direction = Direction.North;
				         Cannon.MoveToWorld( new Point3D(X,Y - 3,Z), Map);
                                         deed.Delete();
                                      }
                                      else
                                      {
                                          if( m_Cannon == null )
			                  {
                                           this.Say("My weapon is deployed.");
                                          }
                                          else
                                          {
                                            this.Say("My weapon has been destroyed or lost.");
                                          }
                                      }
			           // }
				}
                                else if (e.Speech == "crate")
				{
                                   if( m_Cannon != null )
			           {
                                    this.Say("As you command.");
                                    this.CantWalk = false;
                                    m_Cannon.Delete();
                                    CannonNorthDeed deed = new CannonNorthDeed();
                                    this.AddToBackpack (deed);
			           }
                                   else
                                   {
                                    this.Say("My weapon must have been destroyed.");
                                   }
				}
			}
		}
		protected override void OnLocationChange(Point3D oldLoc)
		{
			if( Cannon == null )
			{
				//Cannon = new CannonNorth(this);
				//Direction = Direction.North;
				//Cannon.MoveToWorld( new Point3D(X,Y - 3,Z), Map);
			}
			else
			{
                               if( Cannon != null )
			       {
				  Direction = Direction.North;
				  Cannon.MoveToWorld( new Point3D(X,Y - 3,Z), Map);
                               }
			}
			base.OnLocationChange(oldLoc);
		}
		
		public override void OnThink()
		{
			if( Combatant == null )
				return;
			Direction d = this.GetDirectionTo(Combatant);
			if( Direction != d )
				Direction = d;
                        if( Cannon != null )
	                {
			if( Cannon.Direction != Direction )
			{
				switch (Direction)
				{
					case Direction.Up:
					case Direction.North:
						{
							Cannon.Delete();
							Cannon = new CannonNorth(this);
							Cannon.MoveToWorld( new Point3D(X,Y - 3,Z), Map);
							Cannon.Direction = Direction;
							break;
						}
					case Direction.Down:
					case Direction.South:
						{
							Cannon.Delete();
							Cannon = new CannonSouth(this);
							Cannon.MoveToWorld( new Point3D(X,Y + 1,Z), Map);
							Cannon.Direction = Direction;
							break;
						}
					case Direction.Right:
					case Direction.East:
						{
							Cannon.Delete();
							Cannon = new CannonEast(this);
							Cannon.MoveToWorld( new Point3D(X + 1,Y,Z), Map);
							Cannon.Direction = Direction;
							break;
						}
					case Direction.Left:
					case Direction.West:
						{
							Cannon.Delete();
							Cannon = new CannonWest(this);
							Cannon.MoveToWorld( new Point3D(X - 3,Y,Z), Map);
							Cannon.Direction = Direction;
							break;
						}
				}
			}
                       }
			base.OnThink();
		}
		
		public TurnableCannonGuard( Serial serial ) : base( serial )
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
