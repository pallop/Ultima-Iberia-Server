/*
 * Created by SharpDevelop.
 * User: UOT
 * Date: 3/29/2005
 * Time: 9:54 PM
 * Version: 1.0.0
 */

using System;
using Server.Items;

namespace Server.Mobiles
{
	public class CannonGuardWest : BaseCannonGuard
	{		
		[Constructable]
		public CannonGuardWest() : base()
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the cannoneer";
			Hue = Utility.RandomSkinHue();
			this.CantWalk = true;
			
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
			Utility.AssignRandomHair( this );
			//AddItem( Server.Items.Hair.GetRandomHair( Female ) );
		}
		
		protected override void OnLocationChange(Point3D oldLoc)
		{
			if( Cannon == null )
			{
				Cannon = new CannonWest(this);
				Direction = Direction.West;
				Cannon.MoveToWorld( new Point3D(X - 3,Y,Z), Map);
			}
			else
			{
				Direction = Direction.West;
				Cannon.MoveToWorld( new Point3D(X - 3,Y,Z), Map);
			}
			base.OnLocationChange(oldLoc);
		}
		
		public CannonGuardWest( Serial serial ) : base( serial )
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

