/*
 * Created by SharpDevelop.
 * User: UOT
 * Date: 3/29/2005
 * Time: 10:19 PM
 * Version: 1.0.0
 */

using System;
using Server.Items;

namespace Server.Mobiles
{
	public class WalkingCannonGuard : BaseCannonGuard
	{
		[Constructable]
		public WalkingCannonGuard() : base()
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the cannoneer";
			Hue = Utility.RandomSkinHue();
			Cannon = new CannonNorth(this);
			this.CantWalk = false;
			
			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				AddItem( new Skirt( Utility.RandomNeutralHue() ) );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				AddItem( new ShortPants( Utility.RandomNeutralHue() ) );
			}
			
			SetStr( 86, 100 );
			SetDex( 81, 95 );
			SetInt( 61, 75 );
			
			SetDamage( 10, 23 );
			
			SetSkill( SkillName.MagicResist, 25.0, 60.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 65.0, 90.5 );
			
			Fame = 1000;
			Karma = 1000;
			
			AddItem( new Boots( Utility.RandomNeutralHue() ) );
			AddItem( new FancyShirt());
			AddItem( new Bandana());
			
			//AddItem( Server.Items.Hair.GetRandomHair( Female ) );
                        Utility.AssignRandomHair( this );
		}
		
		protected override void OnLocationChange(Point3D oldLoc)
		{
			if( Cannon == null )
			{
				GetDirection();
			}
			else
				GetDirection();
			base.OnLocationChange(oldLoc);
		}
		
		public void GetDirection()
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
		
		public override void OnThink()
		{
			if( Combatant == null )
				return;
			Direction d = this.GetDirectionTo(Combatant);
			if( Direction != d )
				Direction = d;
			GetDirection();
			base.OnThink();
		}
		
		public WalkingCannonGuard( Serial serial ) : base( serial )
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

