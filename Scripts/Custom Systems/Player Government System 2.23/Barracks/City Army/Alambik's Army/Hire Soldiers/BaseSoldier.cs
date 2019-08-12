using System;
using Server; 
using System.Reflection;
using System.Collections; 
using System.Collections.Generic;
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Mobiles; 
using Server.Network; 
using Server.Spells;
using Server.Spells.First;
using Server.Spells.Fourth;
using Server.Targeting;
using Server.Targets;
using Server.Gumps;
using Server.Regions;

namespace Server.Mobiles 
{ 
    public class BaseSoldier : BaseCreature
    { 
    //public override bool AllowEquipFrom( Mobile from ){ return m_Owner == from; }
    //public override bool CheckNonlocalLift( Mobile from, Item item ){ return m_Owner == from; }
    public override bool CanPaperdollBeOpenedBy(Mobile from){ return true; }
        private PrevAI m_PreviousAI;
        private bool m_IsHired; 
        public Point3D HouseLocation;
        public Map HouseMap;
        public SoldierAge age;
        public SoldierPersonality attitude;
        public SoldierMotivation motive;
        private int Recall_C;
        private Point3D m_Location;
        private Direction m_Direction = Direction.South;
        public CityRecruitStone m_CityRecruitStone;
        
        public enum SoldierAge
        {
           Teenage,
           YoungAdult,
           MiddleAge,
           Old,
           Elder
        }
        
    public enum SoldierPersonality
    {
        Funny,
        Noble,
        Subserviant,
        Asshole,
        Defiant,
        PassiveAgressive,
        Arrogant,
        Cowardly
    }
    
    public enum PrevAI
    {
        AI_Mage,
        AI_Melee,
        AI_Archer
    }
    
    public enum SoldierMotivation
    {
        Gold,
        Sex,
        Fame,
        Servitude,
        Honor,
        Quest1,
        Quest2,
        Quest3
    }
    [CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
    public PrevAI PreviousAI
    {
        get{return m_PreviousAI;}
        set{m_PreviousAI = value;}
    }
        [CommandProperty( AccessLevel.GameMaster )]
        public SoldierAge Age
        {
    	   get{return age;}
    	   set{age = value;}
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
    public SoldierPersonality Personality
    {
        get{return attitude;}
        set{attitude = value;}
    }
    
    [CommandProperty( AccessLevel.GameMaster )]
    public SoldierMotivation Motivation
    {
        get{return motive;}
        set{motive = value;}
    }
    
        [CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
        public Point3D HomeLocation
        {
           get
           {
            return HouseLocation;
           }
           set
           {
            HouseLocation = value;
            InvalidateProperties();
           }
        }

    [CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
    public Map HomeMap
    {
        get
        {
            return HouseMap;
        }
        set
        {
            HouseMap = value;
            InvalidateProperties();
        }
    }
    
    [CommandProperty( AccessLevel.Counselor, AccessLevel.GameMaster )]
    public int RecallCharges
    {
        get
        {
            return Recall_C;
        }
        set
        {
            Recall_C = value;
            InvalidateProperties();
        }
    }
        
        public BaseSoldier( AIType AI ): base( AI, FightMode.Aggressor, 10, 1, 0.1, 4.0 )
        { 
            this.Personality = (SoldierPersonality)(Utility.RandomMinMax(0,7));
            this.Motivation = (SoldierMotivation)(Utility.RandomMinMax(0,4));
            
            this.Title = "the Soldier";

        } 
        
        public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies; } }

        public BaseSoldier(): base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.1, 4.0 )
        {
        }

        public BaseSoldier( Serial serial ) : base( serial )
        { 
        } 
        
        public override void OnSingleClick( Mobile from )
        {
        }
        public override void GetProperties( ObjectPropertyList list )
        {
           base.GetProperties( list );
           list.Add( 1060658, "recall charges\t{0}", this.RecallCharges );
           if (this.ControlMaster != null)
           list.Add( 1060659, "commander\t{0}", this.ControlMaster.Name );
        }
        
        public override void Serialize( GenericWriter writer ) 
        { 
            base.Serialize( writer ); 

            writer.Write( (int) 1 ); // version
            writer.Write( (int) PreviousAI );
            writer.Write( m_CityRecruitStone);
            writer.Write( (int) attitude );
            writer.Write( (int) motive );
            writer.Write( (int) age );
            writer.Write( (int) Recall_C );
            writer.Write( (bool)m_IsHired ); 
            writer.Write( (Point3D) HouseLocation );
            writer.Write( (Map) HouseMap );
        } 

        public override void Deserialize( GenericReader reader ) 
        { 
            base.Deserialize( reader ); 

            int version = reader.ReadInt();
             switch ( version )
            {
            case 1:
            {
            PreviousAI = (PrevAI)reader.ReadInt();
            goto case 0;
            }
            case 0:
            {
            m_CityRecruitStone = (CityRecruitStone)reader.ReadItem();
            motive = (SoldierMotivation)reader.ReadInt();
                attitude = (SoldierPersonality)reader.ReadInt();
                age = (SoldierAge)reader.ReadInt();
                Recall_C = reader.ReadInt();
            m_IsHired = reader.ReadBool(); 
            HouseLocation = reader.ReadPoint3D();
            HouseMap = reader.ReadMap();
            break;
            }
          }
        } 
////////////////////////////////////////////////////////////////////////////////////////////////
//  Melee Attack
////////////////////////////////////////////////////////////////////////////////////////////////
    public override void OnGaveMeleeAttack( Mobile defender )
    {
///////////////////////////////////////////////////////////////////////////////////////////////
//  Soldier Fame (10% chance of adding 10% Target fame to yours.
///////////////////////////////////////////////////////////////////////////////////////////////
		if (this.AI == AIType.AI_Army && Utility.RandomMinMax( 1, 100 ) < 5)
		    {
		    if (this.ControlMaster.Fame < 10000 && this.Combatant.Hits < this.DamageMin )
	   		{
		    	this.ControlMaster.Fame = this.ControlMaster.Fame + /*(int)(Math.Floor*/(defender.Fame / 10) ;
		    	this.Say("You have gained fame.");
		    	}
		    }
        // Get Fame
        if ( 0.1 >= Utility.RandomDouble())
           this.Fame = this.Fame + (this.Combatant.RawStr / 10 );
        // Melee Attack Skill Gain
        //if ( this.AI == AIType.AI_Melee )
        //{
            if ( 0.2 >= Utility.RandomDouble() && this.Skills[SkillName.Swords].Base < 35 && this.Title == "the fighter" && (this.FindItemOnLayer( Layer.TwoHanded ) is BaseSword || this.FindItemOnLayer( Layer.OneHanded ) is BaseSword))
            {
                this.Skills[SkillName.Swords].Base = this.Skills[SkillName.Swords].Base + 0.1;
                this.Age = SoldierAge.Teenage;
                this.ControlSlots = 3;
            }
            else if ( 0.15 >= Utility.RandomDouble() && this.Skills[SkillName.Swords].Base < 50 && this.Title == "the fighter" && (this.FindItemOnLayer( Layer.TwoHanded ) is BaseSword || this.FindItemOnLayer( Layer.OneHanded ) is BaseSword))
            {
                this.Skills[SkillName.Swords].Base = this.Skills[SkillName.Swords].Base + 0.1;
                this.Age = SoldierAge.YoungAdult;
                this.ControlSlots = 2;
            }
            else if ( 0.10 >= Utility.RandomDouble() && this.Skills[SkillName.Swords].Base < 100 && this.Title == "the fighter" && (this.FindItemOnLayer( Layer.TwoHanded ) is BaseSword || this.FindItemOnLayer( Layer.OneHanded ) is BaseSword))
            {
                this.Skills[SkillName.Swords].Base = this.Skills[SkillName.Swords].Base + 0.05;
                this.Age = SoldierAge.MiddleAge;
                this.ControlSlots = 1;
            }
            else if ( 0.05 >= Utility.RandomDouble() && this.Skills[SkillName.Swords].Base < 125 && this.Title == "the fighter" && (this.FindItemOnLayer( Layer.TwoHanded ) is BaseSword || this.FindItemOnLayer( Layer.OneHanded ) is BaseSword))
            {
                this.Skills[SkillName.Swords].Base = this.Skills[SkillName.Swords].Base + 0.05;
                this.Age = SoldierAge.Old;
                this.ControlSlots = 0;
            }
            else if ( 0.01 >= Utility.RandomDouble() && this.Skills[SkillName.Swords].Base < 150 && this.Title == "the fighter" && (this.FindItemOnLayer( Layer.TwoHanded ) is BaseSword || this.FindItemOnLayer( Layer.OneHanded ) is BaseSword))
            {
                this.Skills[SkillName.Swords].Base = this.Skills[SkillName.Swords].Base + 0.01;
                this.Age = SoldierAge.Elder;
                this.ControlSlots = 0;
            }
            if ( 0.2 >= Utility.RandomDouble() && this.Skills[SkillName.Macing].Base < 35 && this.Title == "the macer" && (this.FindItemOnLayer( Layer.TwoHanded ) is BaseBashing || this.FindItemOnLayer( Layer.OneHanded ) is BaseBashing))
            {
                this.Skills[SkillName.Swords].Base = this.Skills[SkillName.Macing].Base + 0.1;
                this.Age = SoldierAge.Teenage;
                this.ControlSlots = 3;
            }
            else if ( 0.15 >= Utility.RandomDouble() && this.Skills[SkillName.Macing].Base < 50 && this.Title == "the macer" && (this.FindItemOnLayer( Layer.TwoHanded ) is BaseBashing || this.FindItemOnLayer( Layer.OneHanded ) is BaseBashing))
            {
                this.Skills[SkillName.Swords].Base = this.Skills[SkillName.Macing].Base + 0.1;
                this.Age = SoldierAge.YoungAdult;
                this.ControlSlots = 2;
            }
            else if ( 0.10 >= Utility.RandomDouble() && this.Skills[SkillName.Macing].Base < 100 && this.Title == "the macer" && (this.FindItemOnLayer( Layer.TwoHanded ) is BaseBashing || this.FindItemOnLayer( Layer.OneHanded ) is BaseBashing))
            {
                this.Skills[SkillName.Swords].Base = this.Skills[SkillName.Macing].Base + 0.05;
                this.Age = SoldierAge.MiddleAge;
                this.ControlSlots = 1;
            }
            else if ( 0.05 >= Utility.RandomDouble() && this.Skills[SkillName.Macing].Base < 125 && this.Title == "the macer" && (this.FindItemOnLayer( Layer.TwoHanded ) is BaseBashing || this.FindItemOnLayer( Layer.OneHanded ) is BaseBashing))
            {
                this.Skills[SkillName.Swords].Base = this.Skills[SkillName.Macing].Base + 0.05;
                this.Age = SoldierAge.Old;
                this.ControlSlots = 0;
            }
            else if ( 0.01 >= Utility.RandomDouble() && this.Skills[SkillName.Macing].Base < 150 && this.Title == "the macer" && (this.FindItemOnLayer( Layer.TwoHanded ) is BaseBashing || this.FindItemOnLayer( Layer.OneHanded ) is BaseBashing))
            {
                this.Skills[SkillName.Swords].Base = this.Skills[SkillName.Macing].Base + 0.01;
                this.Age = SoldierAge.Elder;
                this.ControlSlots = 0;
            }
            if ( 0.2 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 35 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.1;
            }
            else if ( 0.15 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 50 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.1;
            }
            else if ( 0.10 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 100 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.05;
            }
            else if ( 0.05 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 125 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.05;
            }
            else if ( 0.01 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 150 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.01;
            }
            if ( 0.2 >= Utility.RandomDouble() && this.Skills[SkillName.Anatomy].Base < 35 )
            {
                this.Skills[SkillName.Anatomy].Base = this.Skills[SkillName.Anatomy].Base + 0.1;
            }
            else if ( 0.15 >= Utility.RandomDouble() && this.Skills[SkillName.Anatomy].Base < 50 )
            {
                this.Skills[SkillName.Anatomy].Base = this.Skills[SkillName.Anatomy].Base + 0.1;
            }
            else if ( 0.10 >= Utility.RandomDouble() && this.Skills[SkillName.Anatomy].Base < 100 )
            {
                this.Skills[SkillName.Anatomy].Base = this.Skills[SkillName.Anatomy].Base + 0.05;
            }
            else if ( 0.05 >= Utility.RandomDouble() && this.Skills[SkillName.Anatomy].Base < 125 )
            {
                this.Skills[SkillName.Anatomy].Base = this.Skills[SkillName.Anatomy].Base + 0.05;
            }
            else if ( 0.01 >= Utility.RandomDouble() && this.Skills[SkillName.Anatomy].Base < 150 )
            {
                this.Skills[SkillName.Anatomy].Base = this.Skills[SkillName.Anatomy].Base + 0.01;
            }
        //}
        //Mage Melee Skill Gain
        //else if (this.AI == AIType.AI_Mage)
        //{
            /*if ( 0.2 >= Utility.RandomDouble() && this.Skills[SkillName.Wrestling].Base < 35 )
            {
                this.Skills[SkillName.Wrestling].Base = this.Skills[SkillName.Wrestling].Base + 0.1;
            }
            else if ( 0.15 >= Utility.RandomDouble() && this.Skills[SkillName.Wrestling].Base < 50 )
            {
                this.Skills[SkillName.Wrestling].Base = this.Skills[SkillName.Wrestling].Base + 0.1;
            }
            else if ( 0.10 >= Utility.RandomDouble() && this.Skills[SkillName.Wrestling].Base < 100 )
            {
                this.Skills[SkillName.Wrestling].Base = this.Skills[SkillName.Wrestling].Base + 0.1;
            }
            else if ( 0.05 >= Utility.RandomDouble() && this.Skills[SkillName.Wrestling].Base < 125 )
            {
                this.Skills[SkillName.Wrestling].Base = this.Skills[SkillName.Wrestling].Base + 0.05;
            }
            else if ( 0.01 >= Utility.RandomDouble() && this.Skills[SkillName.Wrestling].Base < 150 )
            {
                this.Skills[SkillName.Wrestling].Base = this.Skills[SkillName.Wrestling].Base + 0.01;
            }
            if ( 0.2 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 35 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.1;
            }
            else if ( 0.15 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 50 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.1;
            }
            else if ( 0.1 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 100 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.05;
            }
            else if ( 0.05 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 125 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.05;
            }
            else if ( 0.01 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 150 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.01;
            }
            if ( 0.2 >= Utility.RandomDouble() && this.Skills[SkillName.Anatomy].Base < 35 )
            {
                this.Skills[SkillName.Anatomy].Base = this.Skills[SkillName.Anatomy].Base + 0.1;
            }
            else if ( 0.15 >= Utility.RandomDouble() && this.Skills[SkillName.Anatomy].Base < 50 )
            {
                this.Skills[SkillName.Anatomy].Base = this.Skills[SkillName.Anatomy].Base + 0.1;
            }
            else if ( 0.10 >= Utility.RandomDouble() && this.Skills[SkillName.Anatomy].Base < 100 )
            {
                this.Skills[SkillName.Anatomy].Base = this.Skills[SkillName.Anatomy].Base + 0.05;
            }
            else if ( 0.05 >= Utility.RandomDouble() && this.Skills[SkillName.Anatomy].Base < 125 )
            {
                this.Skills[SkillName.Anatomy].Base = this.Skills[SkillName.Anatomy].Base + 0.05;
            }
            else if ( 0.01 >= Utility.RandomDouble() && this.Skills[SkillName.Anatomy].Base < 150 )
            {
                this.Skills[SkillName.Anatomy].Base = this.Skills[SkillName.Anatomy].Base + 0.01;
            }*/
            if ( 0.2 >= Utility.RandomDouble() && this.Skills[SkillName.EvalInt].Base < 35 && this.Title == "the mage")
            {
                this.Skills[SkillName.EvalInt].Base = this.Skills[SkillName.EvalInt].Base + 0.1;
            }
            else if ( 0.15 >= Utility.RandomDouble() && this.Skills[SkillName.EvalInt].Base < 50 && this.Title == "the mage")
            {
                this.Skills[SkillName.EvalInt].Base = this.Skills[SkillName.EvalInt].Base + 0.1;
            }
            else if ( 0.1 >= Utility.RandomDouble() && this.Skills[SkillName.EvalInt].Base < 100 && this.Title == "the mage")
            {
                this.Skills[SkillName.EvalInt].Base = this.Skills[SkillName.EvalInt].Base + 0.05;
            }
            else if ( 0.05 >= Utility.RandomDouble() && this.Skills[SkillName.EvalInt].Base < 125 && this.Title == "the mage")
            {
                this.Skills[SkillName.EvalInt].Base = this.Skills[SkillName.EvalInt].Base + 0.05;
            }
            else if ( 0.01 >= Utility.RandomDouble() && this.Skills[SkillName.EvalInt].Base < 150 && this.Title == "the mage")
            {
                this.Skills[SkillName.EvalInt].Base = this.Skills[SkillName.EvalInt].Base + 0.01;
            }
            if ( 0.2 >= Utility.RandomDouble() && this.Skills[SkillName.Magery].Base < 35 && this.Title == "the mage")
            {
                this.Skills[SkillName.Magery].Base = this.Skills[SkillName.Magery].Base + 0.1;
                this.Age = SoldierAge.Teenage;
                this.ControlSlots = 3;
            }
            else if ( 0.15 >= Utility.RandomDouble() && this.Skills[SkillName.Magery].Base < 50 && this.Title == "the mage")
            {
                this.Skills[SkillName.Magery].Base = this.Skills[SkillName.Magery].Base + 0.1;
                this.Age = SoldierAge.YoungAdult;
                this.ControlSlots = 2;
            }
            else if ( 0.1 >= Utility.RandomDouble() && this.Skills[SkillName.Magery].Base < 100 && this.Title == "the mage")
            {
                this.Skills[SkillName.Magery].Base = this.Skills[SkillName.Magery].Base + 0.05;
                this.Age = SoldierAge.MiddleAge;
                this.ControlSlots = 1;
            }
            else if ( 0.05 >= Utility.RandomDouble() && this.Skills[SkillName.Magery].Base < 125 && this.Title == "the mage")
            {
                this.Skills[SkillName.Magery].Base = this.Skills[SkillName.Magery].Base + 0.05;
                this.Age = SoldierAge.Old;
                this.ControlSlots = 0;
            }
            else if ( 0.01 >= Utility.RandomDouble() && this.Skills[SkillName.Magery].Base < 150 && this.Title == "the mage")
            {
                this.Skills[SkillName.Magery].Base = this.Skills[SkillName.Magery].Base + 0.01;
                this.Age = SoldierAge.Elder;
                this.ControlSlots = 0;
            }
        //}
        //Archer Melee Skill Gain
        //else if (this.AI == AIType.AI_Archer)
        //{
            if ( 0.2 >= Utility.RandomDouble() && this.Skills[SkillName.Archery].Base < 35 && this.FindItemOnLayer( Layer.TwoHanded ) is BaseRanged && this.Title == "the archer")
            {
                this.Skills[SkillName.Archery].Base = this.Skills[SkillName.Archery].Base + 0.1;
                this.Age = SoldierAge.Teenage;
                this.ControlSlots = 3;
            }
            else if ( 0.15 >= Utility.RandomDouble() && this.Skills[SkillName.Archery].Base < 50 && this.FindItemOnLayer( Layer.TwoHanded ) is BaseRanged && this.Title == "the archer")
            {
                this.Skills[SkillName.Archery].Base = this.Skills[SkillName.Archery].Base + 0.1;
                this.Age = SoldierAge.YoungAdult;
                this.ControlSlots = 2;
            }
            else if ( 0.10 >= Utility.RandomDouble() && this.Skills[SkillName.Archery].Base < 100 && this.FindItemOnLayer( Layer.TwoHanded ) is BaseRanged && this.Title == "the archer")
            {
                this.Skills[SkillName.Archery].Base = this.Skills[SkillName.Archery].Base + 0.1;
                this.Age = SoldierAge.MiddleAge;
                this.ControlSlots = 1;
            }
            else if ( 0.05 >= Utility.RandomDouble() && this.Skills[SkillName.Archery].Base < 125 && this.FindItemOnLayer( Layer.TwoHanded ) is BaseRanged && this.Title == "the archer")
            {
                this.Skills[SkillName.Archery].Base = this.Skills[SkillName.Archery].Base + 0.05;
                this.Age = SoldierAge.Old;
                this.ControlSlots = 0;
            }
            else if ( 0.01 >= Utility.RandomDouble() && this.Skills[SkillName.Archery].Base < 150 && this.FindItemOnLayer( Layer.TwoHanded ) is BaseRanged && this.Title == "the archer")
            {
                this.Skills[SkillName.Archery].Base = this.Skills[SkillName.Archery].Base + 0.01;
                this.Age = SoldierAge.Elder;
                this.ControlSlots = 0;
            }
            if ( 0.2 >= Utility.RandomDouble() && this.Skills[SkillName.Anatomy].Base < 35 )
            {
                this.Skills[SkillName.Anatomy].Base = this.Skills[SkillName.Anatomy].Base + 0.1;
            }
            else if ( 0.15 >= Utility.RandomDouble() && this.Skills[SkillName.Anatomy].Base < 50 )
            {
                this.Skills[SkillName.Anatomy].Base = this.Skills[SkillName.Anatomy].Base + 0.1;
            }
            else if ( 0.10 >= Utility.RandomDouble() && this.Skills[SkillName.Anatomy].Base < 100 )
            {
                this.Skills[SkillName.Anatomy].Base = this.Skills[SkillName.Anatomy].Base + 0.05;
            }
            else if ( 0.05 >= Utility.RandomDouble() && this.Skills[SkillName.Anatomy].Base < 125 )
            {
                this.Skills[SkillName.Anatomy].Base = this.Skills[SkillName.Anatomy].Base + 0.05;
            }
            else if ( 0.01 >= Utility.RandomDouble() && this.Skills[SkillName.Anatomy].Base < 150 )
            {
                this.Skills[SkillName.Anatomy].Base = this.Skills[SkillName.Anatomy].Base + 0.01;
            }
            if ( 0.2 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 35 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.1;
            }
            else if ( 0.15 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 50 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.1;
            }
            else if ( 0.10 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 100 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.05;
            }
            else if ( 0.05 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 125 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.05;
            }
            else if ( 0.01 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 150 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.01;
            }
        //}
       /* else if (this.AI == AIType.AI_Thief)
        {
            if ( 0.1 >= Utility.RandomDouble() && this.Skills[SkillName.Swords].Base < 150 )
            {
                this.Skills[SkillName.Swords].Base = this.Skills[SkillName.Swords].Base + 0.01;
                this.Say( "My swords skill has increased to, " );
                this.Say( this.Skills[SkillName.Fencing].Base.ToString() );
            }
            if ( 0.1 >= Utility.RandomDouble() && this.Skills[SkillName.Tactics].Base < 150 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Tactics].Base + 0.01;
                this.Say( "My Tactical combat ability increased to, " + this.Skills[SkillName.Tactics].Base.ToString() );
                //this.Say( this.Skills[SkillName.Tactics].Base.ToString() );
            }

        }*/

////////////////////////////////////////////////////////////////////////////////////////////////
//  Disarm Attacker (Theif)
////////////////////////////////////////////////////////////////////////////////////////////////

       /* if (this.AI == AIType.AI_Thief && 0.2 >= Utility.RandomDouble())
        {
            Container pack = defender.Backpack;
            ArrayList dropitems = new ArrayList(defender.Items);
            foreach (Item item in dropitems)
            {
                if (item.Movable != false)
                {
                    if (item.Layer == Layer.OneHanded || item.Layer == Layer.TwoHanded)
                    {
                        pack.DropItem( item );
                        //defender.Stam = defender.Stam - /*(int)(Math.Floor( defender.Stam / 2 ));
                        this.Say("I have Disarmed my opponent.");
                    }
                }
            }
        }*/

    }
////////////////////////////////////////////////////////////////////////////////////////////////
//  Magic Defend
////////////////////////////////////////////////////////////////////////////////////////////////
     public override void OnDamagedBySpell( Mobile from )
     {
        if ( 0.2 >= Utility.RandomDouble() && this.Skills[SkillName.MagicResist].Base < 25 )
        {
            this.Skills[SkillName.MagicResist].Base = this.Skills[SkillName.MagicResist].Base + 0.1;
        }
        else if ( 0.15 >= Utility.RandomDouble() && this.Skills[SkillName.MagicResist].Base < 40 )
        {
            this.Skills[SkillName.MagicResist].Base = this.Skills[SkillName.MagicResist].Base + 0.1;
        }
        else if ( 0.10 >= Utility.RandomDouble() && this.Skills[SkillName.MagicResist].Base < 55 )
        {
            this.Skills[SkillName.MagicResist].Base = this.Skills[SkillName.MagicResist].Base + 0.05;
        }
        else if ( 0.05 >= Utility.RandomDouble() && this.Skills[SkillName.MagicResist].Base < 75 )
        {
            this.Skills[SkillName.MagicResist].Base = this.Skills[SkillName.MagicResist].Base + 0.05;
        }
        else if ( 0.01 >= Utility.RandomDouble() && this.Skills[SkillName.MagicResist].Base < 100 )
        {
            this.Skills[SkillName.MagicResist].Base = this.Skills[SkillName.MagicResist].Base + 0.01;
        }
 		  base.OnDamagedBySpell( from );
     }
////////////////////////////////////////////////////////////////////////////////////////////////
//  Melee Defend
////////////////////////////////////////////////////////////////////////////////////////////////
    public override void OnGotMeleeAttack( Mobile attacker )
    {
    //if ( this.AI == AIType.AI_Melee )
        //{
        if ( 0.2 >= Utility.RandomDouble() && this.Skills[SkillName.Parry].Base < 35  && this.Title == "the fighter" && this.FindItemOnLayer( Layer.TwoHanded ) is BaseShield )
        {
            this.Skills[SkillName.Parry].Base = this.Skills[SkillName.Parry].Base + 0.1;
        }
        else if ( 0.15 >= Utility.RandomDouble() && this.Skills[SkillName.Parry].Base < 50  && this.Title == "the fighter" && this.FindItemOnLayer( Layer.TwoHanded ) is BaseShield )
        {
            this.Skills[SkillName.Parry].Base = this.Skills[SkillName.Parry].Base + 0.1;
        }
        else if ( 0.10 >= Utility.RandomDouble() && this.Skills[SkillName.Parry].Base < 100  && this.Title == "the fighter" && this.FindItemOnLayer( Layer.TwoHanded ) is BaseShield )
        {
            this.Skills[SkillName.Parry].Base = this.Skills[SkillName.Parry].Base + 0.05;
        }
        else if ( 0.05 >= Utility.RandomDouble() && this.Skills[SkillName.Parry].Base < 125  && this.Title == "the fighter" && this.FindItemOnLayer( Layer.TwoHanded ) is BaseShield )
        {
            this.Skills[SkillName.Parry].Base = this.Skills[SkillName.Parry].Base + 0.05;
        }
        else if ( 0.01 >= Utility.RandomDouble() && this.Skills[SkillName.Parry].Base < 150  && this.Title == "the fighter" && this.FindItemOnLayer( Layer.TwoHanded ) is BaseShield )
        {
            this.Skills[SkillName.Parry].Base = this.Skills[SkillName.Parry].Base + 0.01;
        }
    //}
    }

        public override bool KeepsItemsOnDeath{ get{ return true; } }

        [CommandProperty( AccessLevel.Administrator )] 
        public bool IsHired 
        { 
            get 
            { 
                return m_IsHired; 
            } 
            set 
            { 
                if ( m_IsHired== value ) 
                    return; 

                m_IsHired= value; 
                Delta( MobileDelta.Noto ); 
                InvalidateProperties(); 
            } 
        } 

        #region [ GetOwner ] 
        public virtual Mobile GetOwner() 
        { 
            if( !Controlled )
                return null; 
            Mobile Owner = ControlMaster; 
          
            m_IsHired = true; 
          
            if( Owner == null ) 
                return null; 

            if( Owner.Deleted )
            {
                Say( 1005653 ); // Hmmm.  I seem to have lost my master.
                SetControlMaster( null );
                return null;
            }
            
            if( Owner.Map != this.Map || !Owner.InRange( Location, 30 ) )
            { 
                Say( 1005653 ); // Hmmm.  I seem to have lost my master. 
                this.Location = this.HomeLocation;
                this.Map = this.HomeMap;
                this.ControlOrder = OrderType.Stay;
                this.Say("I am Home.");
                return null; 
            } 

            return Owner; 
        } 
        #endregion 

        #region [ AddHire ] 
        public virtual bool AddHire( Mobile m ) 
        { 
            Mobile owner = GetOwner(); 

            if( owner != null ) 
            { 
                m.SendLocalizedMessage( 1043283, owner.Name ); // I am following ~1_NAME~. 
                return false; 
            } 

            if( SetControlMaster( m ) ) 
            { 
                m_IsHired = true; 
                return true; 
            } 
          
            return false; 
        } 
        #endregion 

        #region [ OnDragDrop ] 
        public override bool OnDragDrop( Mobile from, Item item ) 
        { 
            Container pack = this.Backpack;
            if( this.ControlMaster != null && this.ControlMaster == from)
            {
               if ( item is RecallScroll )
               {
                  int amount = item.Amount;
                  if ( amount > (100 - Recall_C) )
                  {
                    item.Consume( 100 - Recall_C );
                    Recall_C = 100;
                  }
                  else
                  {
                    Recall_C += amount;
                    item.Delete();

                    return true;
                  }
              }
              
            if ( item is BaseClothing && item.Layer == Layer.Pants && this.FindItemOnLayer( Layer.Pants ) == null || item.Layer == Layer.Shirt && this.FindItemOnLayer( Layer.Shirt ) == null || item.Layer == Layer.Helm && this.FindItemOnLayer( Layer.Helm ) == null || item.Layer == Layer.Gloves && this.FindItemOnLayer( Layer.Gloves ) == null || item.Layer == Layer.Neck && this.FindItemOnLayer( Layer.Neck ) == null || item.Layer == Layer.Arms && this.FindItemOnLayer( Layer.Arms ) == null || item.Layer == Layer.InnerTorso && this.FindItemOnLayer( Layer.InnerTorso ) == null || item.Layer == Layer.Shoes && this.FindItemOnLayer( Layer.Shoes ) == null || item.Layer == Layer.Cloak && this.FindItemOnLayer( Layer.Cloak ) == null || item.Layer == Layer.OuterTorso && this.FindItemOnLayer( Layer.OuterTorso ) == null )
			{
			this.Emote( "Thank you, tis wonderful" );
			pack.DropItem( item );
			this.AddItem( item );
				return true;
			}

			if ( item is BaseArmor && item.Layer == Layer.Pants && this.FindItemOnLayer( Layer.Pants ) == null || item.Layer == Layer.Shirt && this.FindItemOnLayer( Layer.Shirt ) == null || item.Layer == Layer.Helm && this.FindItemOnLayer( Layer.Helm ) == null || item.Layer == Layer.Gloves && this.FindItemOnLayer( Layer.Gloves ) == null || item.Layer == Layer.Neck && this.FindItemOnLayer( Layer.Neck ) == null || item.Layer == Layer.Arms && this.FindItemOnLayer( Layer.Arms ) == null || item.Layer == Layer.InnerTorso && this.FindItemOnLayer( Layer.InnerTorso ) == null || item.Layer == Layer.Shoes && this.FindItemOnLayer( Layer.Shoes ) == null )
			{
			this.Emote( "I shall put this to good use." );
			pack.DropItem( item );
			this.AddItem( item );
				return true;
			}

			if ( item is BaseShield && item.Layer == Layer.TwoHanded && this.FindItemOnLayer( Layer.TwoHanded ) == null )
			{
			this.Emote( "I shall put this to good use." );
			pack.DropItem( item );
			this.AddItem( item );
				return true;
			}
   
                        if ( item is BaseWeapon && item.Layer == Layer.TwoHanded && this.FindItemOnLayer( Layer.TwoHanded ) == null )
			{
			this.Emote( "I shall put this to good use." );
			pack.DropItem( item );
			this.AddItem( item );
				return true;
			}
   
                        if ( item is BaseRanged && item.Layer == Layer.TwoHanded && this.FindItemOnLayer( Layer.TwoHanded ) == null )
			{
			this.Emote( "I shall put this to good use." );
			pack.DropItem( item );
			this.AddItem( item );
				return true;
			}
   
                        if ( item is BaseWeapon && item.Layer == Layer.OneHanded && this.FindItemOnLayer( Layer.OneHanded ) == null )
			{
			this.Emote( "I shall put this to good use." );
			pack.DropItem( item );
			this.AddItem( item );
				return true;
			}

                        if ( item is BaseJewel && item.Layer == Layer.Bracelet && this.FindItemOnLayer( Layer.Bracelet ) == null || item.Layer == Layer.Ring && this.FindItemOnLayer( Layer.Ring ) == null || item.Layer == Layer.Earrings && this.FindItemOnLayer( Layer.Earrings ) == null || item.Layer == Layer.Neck && this.FindItemOnLayer( Layer.Neck ) == null)
			{
			this.Emote( "I shall put this to good use." );
			pack.DropItem( item );
			this.AddItem( item );
				return true;
			}
   
			if ( PackAnimal.CheckAccess( this, from ) )
			{
				AddToBackpack( item );
				return true;
			}
                    }

            return base.OnDragDrop( from, item ); 
        } 
        #endregion 
        
        private int bandages = 0;
        private int healpotions = 0;
        private int healscrolls = 0;
        
        private void HealSelf()
		{
			if ( BandageContext.GetContext( this ) == null )
			{
				BandageContext.BeginHeal( this, this );
			}

			return;
		}
  
        #region [ OnDamage ]
                public override void OnDamage( int amount, Mobile from, bool willKill )
 	        {
                  ArrayList t_items = new ArrayList( this.Items );
                     for (int i=0; i < t_items.Count; i++)
		    {
		      Item item = (Item)t_items[i];
                      if (item is Bandage)
			bandages += item.Amount;
		      else if (item is BaseHealPotion)
			healpotions += item.Amount;
		      else if (item is HealScroll)
			healscrolls += item.Amount;
                    }
                  
		  if ( (!willKill))
                      {
		    if ( (this.Hits < (int)(this.HitsMax / 1.5)) && (((healpotions + healscrolls) > 0) || (bandages > 0)) )
                      {
                      this.Say("Healing myself!");
		      this.DebugSay("Healing myself!");
 		      if ( !new GreaterHealSpell( this, null ).Cast() )
                       {
 			new HealSpell( this, null ).Cast();
 			if (healscrolls > 0)
 			  healscrolls -= 2;
 			else if (healpotions > 0)
 			  healpotions -= 2;
 		      }
 		      else {
 			if (healscrolls > 0)
 			  healscrolls--;
 			else if (healpotions > 0)
 			  healpotions--;
 		      }
                      
		      if (bandages > 0)
                      {
			BandageContext.BeginHeal( this, this );
			bandages--;
            if ( 0.2 >= Utility.RandomDouble() && this.Skills[SkillName.Healing].Base < 35 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Healing].Base + 0.1;
            }
            else if ( 0.15 >= Utility.RandomDouble() && this.Skills[SkillName.Healing].Base < 50 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Healing].Base + 0.1;
            }
            else if ( 0.10 >= Utility.RandomDouble() && this.Skills[SkillName.Healing].Base < 100 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Healing].Base + 0.05;
            }
            else if ( 0.05 >= Utility.RandomDouble() && this.Skills[SkillName.Healing].Base < 125 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Healing].Base + 0.05;
            }
            else if ( 0.01 >= Utility.RandomDouble() && this.Skills[SkillName.Healing].Base < 150 )
            {
                this.Skills[SkillName.Tactics].Base = this.Skills[SkillName.Healing].Base + 0.01;
            }
		      }
		    }
		  }
 		  base.OnDamage( amount, from, willKill );
 		}
        #endregion

        #region [ OnSpeech ] 
        public override void OnSpeech( SpeechEventArgs e ) 
        {    
             Mobile from = e.Mobile;
             string message;
            if( !e.Handled && e.Mobile.InRange( this, 6 ) ) 
            {

            if (this.ControlMaster != null)
            {
            string addressmeas;
            string mastername;
            addressmeas = "Friend";
            mastername = "No One";
            if ( this.ControlMaster.Female == false && this.ControlMaster != null )
            {
                addressmeas = "Lord";
                mastername = this.ControlMaster.Name;
            }
            else if ( this.ControlMaster.Female == true && this.ControlMaster != null )
            {
                addressmeas = "Lady";
                mastername = this.ControlMaster.Name;
            }
            else if ( this.ControlMaster == null )
            {
                addressmeas = "Friend";
                mastername = "No One";
            }
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Who Is Master Command
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (e.Speech.ToLower() == this.Name.ToLower() + " who's your master" || e.Speech.ToLower() == this.Name.ToLower() + " who's your daddy" || e.Speech.ToLower() == this.Name.ToLower() + " who's your leader" || e.Speech.ToLower() == this.Name.ToLower() + " who's your commander")
                    {
                        message = "I am a desciple of " + addressmeas + " " + mastername;
                        this.Say( message );
                    }
            if( this.ControlMaster != null && this.ControlMaster == e.Mobile)
            {
             message = " ";
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Reply to player
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    if ( e.Speech.ToLower() == this.Name.ToLower() )
                    { message = " ";
                        if( this.Motivation == SoldierMotivation.Gold )
                        {
                            if( this.Personality == SoldierPersonality.Asshole)
                                message = "What do you want, and how much does the job pay?";
                            else if( this.Personality == SoldierPersonality.Subserviant)
                                message = "How might I serve thee, My " + addressmeas + "?";
                            else if( this.Personality == SoldierPersonality.Noble)
                                message = "For gold and glory, I am at your command, My " + addressmeas + "?";
                            else if( this.Personality == SoldierPersonality.PassiveAgressive)
                                message = "How might I help, " + addressmeas + "?";
                            else if( this.Personality == SoldierPersonality.Defiant)
                                message = addressmeas + " " + mastername + ", I have yet to see sufficant compensation for my service. Why should I answer you now?";
                            else if( this.Personality == SoldierPersonality.Arrogant)
                                message = "What great deed dost thou wish of me, and at what price?";
                            else if( this.Personality == SoldierPersonality.Cowardly)
                                message = "Yes, My " + addressmeas + "?";
                            else if( this.Personality == SoldierPersonality.Funny)
                                message = "Put some coins in my purse and I'll happily hear you out my friend.";
                            else
                                message = "Yes?";
                        }
                        else if( this.Motivation == SoldierMotivation.Sex )
                        {
                            if( this.Personality == SoldierPersonality.Asshole)
                                if (this.Female == false)
                                {
                                    if ( this.ControlMaster.Female == false )
                                        message = "What do you want now?";
                                    else
                                        message = "I'll serve you in battle when you serve me on your back or knees, My Lady.";
                                }
                                else
                                {
                                    if ( this.ControlMaster.Female == false )
                                        message = "You'd have me ride a smelly mount all day, when I'd sooner prefer to ride you My Lord.";
                                    else
                                        message = "What now? Shall I stable the horses for you while you whore with all the good men and leave me with the stable boy again?";
                                }
                            else if( this.Personality == SoldierPersonality.Subserviant)
                                if (this.Female == false)
                                {
                                    if ( this.ControlMaster.Female == false )
                                        message = "Yes, My Lord.";
                                    else
                                        message = "How might I honor your beauty, My Lady";
                                }
                                else
                                {
                                    if ( this.ControlMaster.Female == false )
                                        message = "Anything for you, My Lord.";
                                    else
                                        message = "Yes, My Lady";
                                }
                            else if( this.Personality == SoldierPersonality.Noble)
                                if (this.Female == false)
                                {
                                    if ( this.ControlMaster.Female == false )
                                        message = "How might I honor thee?";
                                    else
                                        message = "What can I do to win your favor, my Lady?";
                                }
                                else
                                {
                                    if ( this.ControlMaster.Female == false )
                                        message = "I serve only you, my Lord";
                                    else
                                        message = "How might I honor thee, my Lady?";
                                }
                            else if( this.Personality == SoldierPersonality.PassiveAgressive)
                                if (this.Female == false)
                                {
                                    if ( this.ControlMaster.Female == false )
                                        message = "Your command is my purpose, Lord.";
                                    else
                                        message = "What ever My Lady wishes, I will see done.";
                                }
                                else
                                {
                                    if ( this.ControlMaster.Female == false )
                                        message = "Your command is my purpose, My Lord";
                                    else
                                        message = "What ever My Lord wishes, I will see done.";
                                }
                            else if( this.Personality == SoldierPersonality.Defiant)
                                if (this.Female == false)
                                {
                                    if ( this.ControlMaster.Female == false )
                                        message = "Lord, women love a working man. I'd hate to win them all by doing everything for you.";
                                    else
                                        message = "Wench, why should I follow orders from a woman.";
                                }
                                else
                                {
                                    if ( this.ControlMaster.Female == false )
                                        message = "I work all day for you my Lord, let a tired girl rest a moment.";
                                    else
                                        message = "Your servant I may be, my lady.  But at least I have larger breasts than thee.";
                                }
                            else if( this.Personality == SoldierPersonality.Arrogant)
                                if (this.Female == false)
                                {
                                    if ( this.ControlMaster.Female == false )
                                        message = "What?! Oh, you want to know how I attract all the ladies, don't you my Lord?";
                                    else
                                        message = "I'll serve you well in battle, and better in bed, my Lady.";
                                }
                                else
                                {
                                    if ( this.ControlMaster.Female == false )
                                        message = "My Lord, I noticed you looking up my dress when I mounted my Horse.  Nice, isn't it?";
                                    else
                                        message = "Ok, one last order, then we can pick up a few knights at the pub and turn in early.";
                                }
                            else if( this.Personality == SoldierPersonality.Cowardly)
                                if (this.Female == false)
                                {
                                    if ( this.ControlMaster.Female == false )
                                        message = "My Lord, I know it wrong for a man to say this to another man, but... I love thee, in the most sinful way.";
                                    else
                                        message = "My Lady?";
                                }
                                else
                                {
                                    if ( this.ControlMaster.Female == false )
                                        message = "You're so strong My lord.  Protect me, and I'll serve your every need.";
                                    else
                                        message = "Yes, my Lady?";
                                }
                            else if( this.Personality == SoldierPersonality.Funny)
                                if (this.Female == false)
                                {
                                    if ( this.ControlMaster.Female == false )
                                        switch(Utility.Random(4)){
                                        case 0:{message = "Let's hurry up and be done with the work. There's a shepard's daughter in Yew I'd like to get back to.";};break;
                                        case 1:{message = "Sorry Lord, I was just day dreaming about that shepard's daughter again.";};break;
                                        case 2:{message = "Let's go find a damsel in distress.  I could use some good Hero Sex";};break;
                                        case 3:{message = "...that shepard's daughter. If she were a horse, I'd stick my wank in her flank and ride like the wind.";};break;}
                                    else
                                        switch(Utility.Random(4)){
                                        case 0:{message = "My Lady, I try to repress my feelings for you, but you make it hard... Literally.";};break;
                                        case 1:{message = "My Lady, If I should die in combat, I pray I can come back as your horse's saddle in the next life.";};break;
                                        case 2:{message = "Good morning My Lady, Your breasts look lovely today.";};break;
                                        case 3:{message = "Trust me not, My Lady. I'll defend your chastity with my long sword, all the while plotting to take it with my 'other sword'";};break;}
                                }
                                else
                                {
                                    if ( this.ControlMaster.Female == false )
                                        switch(Utility.Random(4)){
                                        case 0:{message = "I wrote you a poem.  Hail to my lord, with his giant sword. Parry and thrust to vanquish my lust...";};break;
                                        case 1:{message = "Lord, would you care for a backrub?";};break;
                                        case 2:{message = "Dost my Lord wish to fondle me again?";};break;
                                        case 3:{message = "My mount is tired.  Perhaps we could let it rest, and I'll ride your face instead, My Lord.";};break;}


                                    else
                                        switch(Utility.Random(4)){
                                        case 0:{message = "My Lady, we need to find some men.  It's been so long, even the livestock seems to tempt me.";};break;
                                        case 1:{message = "My lady, could we stop for a bath soon?  I don't know if that smell is comming from me or the horses.";};break;
                                        case 2:{message = "Remember that Knight we met in Serpant's Hold.  He could Lance me anytime.";};break;
                                        case 3:{message = "Let's join the savage camp, My lady.  Riding around with nothing but body paint on sounds exciting.";};break;}
                                }
                            else
                                message = "Yes?";
                        }
                        else
                        {
                            if( this.Personality == SoldierPersonality.Asshole)
                                message = "What the hell do you want now?";
                            else if( this.Personality == SoldierPersonality.Subserviant)
                                message = "How might I serve thee, My " + addressmeas + "?";
                            else if( this.Personality == SoldierPersonality.Noble)
                                message = "What noble task might I aid thee in, My " + addressmeas + "?";
                            else if( this.Personality == SoldierPersonality.PassiveAgressive)
                                message = "How might I help, " + addressmeas + "?";
                            else if( this.Personality == SoldierPersonality.Defiant)
                                message = addressmeas + " " + mastername + ", Why dost thou assume I care to hear thy words?";
                            else if( this.Personality == SoldierPersonality.Arrogant)
                                message = "What great deed dost thou wish of me?";
                            else if( this.Personality == SoldierPersonality.Cowardly)
                                message = "Yes, My " + addressmeas + "?";
                            else if( this.Personality == SoldierPersonality.Funny)
                                message = "Excuse me " + addressmeas + ", but my name is " + this.Name + ". Not Slave or Servant or Squire, but " + this.Name + ".";
                            else
                                message = "Yes?";
                        }

                        this.Say( message );
                        if (this.Female == true )
                            this.PlaySound( 799 );
                        else
                            this.PlaySound( 1071 );
                    }
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Sleep Command
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                 /*   else if (e.Speech.ToLower() == "sleep" || e.Speech.ToLower() == "bedtime" ))
                    {
                                //if (this.HouseLocation != null)
                                //{
                                this.Direction = m_Direction;
                                message = "As you command.";
                                this.Say( message );
			        this.Direction = m_Direction;
			        Point3D p = ( m_Location == Point3D.Zero ? this.HouseLocation : m_Location );
                                SleepingBody body = new SleepingBody( this, false, false );
			        body.MoveToWorld( p, this.Map );
                                this.Map = Map.Internal;
                               // }
                                
                    }*/
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Make Camp Command
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        else if (e.Speech.ToLower() == this.Name.ToLower() + " new fire")
			{
                               IPooledEnumerable eable = this.Map.GetItemsInRange ( this.Location,  5 );
			        foreach ( Item item in eable )
			        {
                                     if ( item is BedRoll1 )
				        {
                                                Point3D fireLocation = GetFireLocation( this.ControlMaster );
                                                new Campfire().MoveToWorld( fireLocation, from.Map );
                                                message = "I am securing thy camp now.";
			                        this.Say( message );
						eable.Free();
						return;
					}
				}

			}
			else if (e.Speech.ToLower() == this.Name.ToLower() + " make camp" || e.Speech.ToLower() == this.Name.ToLower() + " setup camp")
			{
                               if ( Validate(from) == false )
                               {
                               message = "Excuse me M'Lord but we can't set up here.";
			        this.Say( message );
                                return;
                                }

                          if ( this.Backpack.ConsumeTotal( typeof( TentDeed ), 1 ))
		           {
                                 Point3D fireLocation = GetFireLocation( this.ControlMaster );
                                 new Campfire().MoveToWorld( fireLocation, from.Map );

               TentWalls v = new TentWalls();
               v.Location = from.Location;
               v.Map = from.Map;

               TentRoof w = new TentRoof();
               w.Location = from.Location;
               w.Map = from.Map;

               TentFloor y = new TentFloor();
               y.Location = from.Location;
               y.Map = from.Map;

               TentTrim z = new TentTrim();
               z.Location = from.Location;
               z.Map = from.Map;

               TentVerifier tentverifier = new TentVerifier();
               from.AddToBackpack (tentverifier);

               SecureTent chest = new SecureTent((PlayerMobile)from,v,w,y,z);
               chest.Location = new Point3D( from.X -1, from.Y-1, from.Z );
               chest.Map = from.Map;
               
              //this.HouseLocation = new Point3D( from.X, from.Y+1, from.Z );//this.ControlMaster.Location;
              //this.HouseMap = from.Map;
              //this.Say("I shall remember this location as my home.");
                        
               BedRoll1 x = new BedRoll1(v,w,y,z,(PlayerMobile)from, (SecureTent) chest,(TentVerifier) tentverifier);
               x.Location = new Point3D( from.X, from.Y+1, from.Z );
               x.Map = from.Map;
               
				message = "I am securing thy camp now.";
			        this.Say( message );
                }
                    else
		   {
		   from.SendMessage( "You must have the bedroll or its already set up somewhere." );
		   }
			}
                      else if (e.Speech.ToLower() == this.Name.ToLower() + " remove camp" || e.Speech.ToLower() == this.Name.ToLower() + " roll out" || e.Speech.ToLower() == "soldiers break camp" || e.Speech.ToLower() == "soldier break camp")
			{
			foreach ( Item item in this.GetItemsInRange( 5 ) )
			{
                                if ( item is BedRoll1 )

	        {
         BedRoll1 BedRoll1 = (BedRoll1)item;
	   from.SendGump (new TentDGump(BedRoll1,from));
                }
         	}
            }
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Remember Home
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    else if (e.Speech.ToLower() == "remember home")
                    {
                        this.HouseLocation = this.ControlMaster.Location;
                        this.HouseMap = this.ControlMaster.Map;
                        this.Say("I shall remember this location.");
                    }
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Return Home
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    else if (e.Speech.ToLower() == this.Name.ToLower() +" return home")
                    {
                        if (this.RecallCharges > 0)
                        {
                            this.Location = this.HomeLocation;
                            this.Map = this.HomeMap;
                            this.ControlOrder = OrderType.Stay;
                            this.Say("I am Home.");
                            this.RecallCharges -= 1;

                        }
                        else
                            this.Say("I need recall scrolls to do that.");
                    }
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Hide/Show Command
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        else if (e.Speech.ToLower() == this.Name.ToLower() +" show" )
			{
        			message = "I am here Master.";
                                this.Say( message );
        			this.Hidden = false;
			}
                        else if (e.Speech.ToLower() == this.Name.ToLower() +" hide")
			{
        			this.Hidden = true;
			}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Fight Mode Command
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        else if (e.Speech.ToLower() == "soldiers attention" || e.Speech.ToLower() == "soldier attention")
			{
        			message = "As you command.";
                                this.Say( message );
                                ((BaseCreature)this).AI = AIType.AI_Army;
                                this.FightMode = FightMode.Aggressor;
			}
                        else if (e.Speech.ToLower() == "soldiers standdown" || e.Speech.ToLower() == "soldier standdown")
			{
        			message = "As you command.";
                                this.Say( message );
                                if(this.Title == "the fighter" || this.Title == "the macer")
                                {
                                ((BaseCreature)this).AI = AIType.AI_Melee;
        			this.FightMode = FightMode.Aggressor;
                                this.FocusMob = null;
                                }
                                else if(this.Title == "the mage")
                                {
                                ((BaseCreature)this).AI = AIType.AI_Mage;
        			this.FightMode = FightMode.Aggressor;
                                this.FocusMob = null;
                                }
                                else if(this.Title == "the archer" || this.Title == "the cannoneer")
                                {
                                ((BaseCreature)this).AI = AIType.AI_Archer;
        			this.FightMode = FightMode.Aggressor;
                                this.FocusMob = null;
                                }
			}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Dismount Command
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    else if (e.Speech.ToLower() == this.Name.ToLower() +" dismount" )
                    {
                        if (this.Mount != null)
                        {
                            message = "As you command.";
                            IMount mount = this.Mount;
                            if ( mount != null )
                                mount.Rider = null;

                            if ( mount is Mobile )
                                ((Mobile)mount).Delete();
                            this.Say( message );
                        }
                        else
                        {
                            message = "But I'm not on a mount.";
                            this.Say( message );
                        }
                    }
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Remount Command
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//Horse
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" remount horse")
                    {
                        if (this.Mount != null)
                        {
                            message = "I am already mounted.";
                            this.Say( message );
                            return;
                        }
                        if ( this.ControlMaster.Backpack != null && this.ControlMaster.Backpack.ConsumeTotal( typeof( Gold ), 700 ) || this.Backpack != null && this.Backpack.ConsumeTotal( typeof( Gold ), 900 ) )
                        {
                            new Horse().Rider = this;
                            message = "As you command.";
                            this.Say( message );
                        }
                        else
                        {
                            message = "But we haven't the 900 Gold to buy this mount.";
                            this.Say( message );
                        }
                    }
//Ostard
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" remount ostard")
                    {
                        if (this.Mount != null)
                        {
                            message = "I am already mounted.";
                            this.Say( message );
                            return;
                        }
                        if ( this.ControlMaster.Backpack != null && this.ControlMaster.Backpack.ConsumeTotal( typeof( Gold ), 800 ) )
                        {
                            new ForestOstard().Rider = this;
                            message = "As you command.";
                            this.Say( message );
                        }
                        else
                        {
                            message = "But we haven't the 1,000 Gold to buy this mount.";
                            this.Say( message );
                        }
                    }
//Swampdragon
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" remount swamp dragon")
                    {
                        if (this.Mount != null)
                        {
                            message = "I am already mounted.";
                            this.Say( message );
                            return;
                        }
                        if ( this.Fame >= 50000 && this.ControlMaster.Backpack.ConsumeTotal( typeof( Gold ), 900 ))
                        {
                            new SwampDragon().Rider = this;
                            message = "As you command.";
                            this.Say( message );
                        }
                        else
                        {
                            message = "I need more fame to ride such a creature.";
                            this.Say( message );
                        }
                    }


//Swampdragon (barded)
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" remount barded swampdragon")
                    {
                        if (this.Mount != null)
                        {
                            message = "I am already mounted.";
                            this.Say( message );
                            return;
                        }
                        if ( this.Fame >= 10000 && this.ControlMaster.Backpack.ConsumeTotal( typeof( Gold ), 1000 ))
                        {
                            new ScaledSwampDragon().Rider = this;
                            message = "As you command.";
                            this.Say( message );
                        }
                        else
                        {
                            message = "I need more fame to ride such a creature.";
                            this.Say( message );
                        }
                    }
//Llama
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" remount llama")
                    {
                        if (this.Mount != null)
                        {
                            message = "I am already mounted.";
                            this.Say( message );
                            return;
                        }
                        if ( this.ControlMaster.Backpack != null && this.ControlMaster.Backpack.ConsumeTotal( typeof( Gold ), 2000 ) )
                        {
                            new RidableLlama().Rider = this;
                            message = "As you command.";
                            this.Say( message );
                        }
                        else
                        {
                            message = "But we haven't the 2,000 Gold to buy this mount.";
                            this.Say( message );
                        }
                    }
//Ridgeback
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" remount ridgeback")
                    {
                        if (this.Mount != null)
                        {
                            message = "I am already mounted.";
                            this.Say( message );
                            return;
                        }
                        if ( this.Fame >= 1000 && this.ControlMaster.Backpack.ConsumeTotal( typeof( Gold ), 2500 ))
                        {
                            new Ridgeback().Rider = this;
                            message = "As you command.";
                            this.Say( message );
                        }
                        else
                        {
                            message = "I need more fame or gold to ride such a creature.";
                            this.Say( message );
                        }
                    }
//Skeletal Mount
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" remount skeletal horse")
                    {
                        if (this.Mount != null)
                        {
                            message = "I am already mounted.";
                            this.Say( message );
                            return;
                        }
                        if (0 >= this.ControlMaster.Karma)
                        {
                            if ( this.Fame >= 10000 && this.ControlMaster.Backpack.ConsumeTotal( typeof( Gold ), 3000 ))
                            {
                                new SkeletalMount().Rider = this;
                                message = "As you command.";
                                this.Say( message );
                            }
                            else
                            {
                                message = "I need more fame or gold to ride such a creature.";
                                this.Say( message );
                            }
                        }
                        else
                        {
                            message = addressmeas + ", I don't think virtous people should ride these beasts.";
                            this.Say( message );
                        }
                    }
//FireSteed
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" remount fire steed")
                    {
                        if (this.Mount != null)
                        {
                            message = "I am already mounted.";
                            this.Say( message );
                            return;
                        }
                        if ( this.Fame >= 10000 && this.ControlMaster.Backpack.ConsumeTotal( typeof( Gold ), 3000 ))
                        {
                            new FireSteed().Rider = this;
                            message = "As you command.";
                            this.Say( message );
                        }
                        else
                        {
                            message = "I need more fame or gold to ride such a creature.";
                            this.Say( message );
                        }
                    }
//Unicorn
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" remount unicorn")
                    {
                        if (this.Mount != null)
                        {
                            message = "I am already mounted.";
                            this.Say( message );
                            return;
                        }
                        if ( this.Female == true )
                        {
                            if ( this.Fame >= 10000 && this.ControlMaster.Backpack.ConsumeTotal( typeof( Gold ), 3000 ))
                            {
                                new Unicorn().Rider = this;
                                message = "As you command.";
                                this.Say( message );
                            }
                            else
                            {
                                message = "I need more fame or gold to ride such a creature.";
                                this.Say( message );
                            }
                        }
                        else
                        {
                            message = "Sire, I don't believe Unicorns will allow a man to ride them.";
                            this.Say( message );
                        }
                    }
//Kirin
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" remount unicorn")
                    {
                        if (this.Mount != null)
                        {
                            message = "I am already mounted.";
                            this.Say( message );
                            return;
                        }
                        if ( this.Female == false )
                        {
                            if ( this.Fame >= 10000 && this.ControlMaster.Backpack.ConsumeTotal( typeof( Gold ), 3000 ))
                            {
                                new Kirin().Rider = this;
                                message = "As you command.";
                                this.Say( message );
                            }
                            else
                            {
                                message = "I need more fame or gold to ride such a creature.";
                                this.Say( message );
                            }
                        }
                        else
                        {
                            message = "Sire, I don't believe Kirins will allow a woman to ride them.";
                            this.Say( message );
                        }
                    }
//NightMare
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" remount nightmare")
                    {
                        if (this.Mount != null)
                        {
                            message = "I am already mounted.";
                            this.Say( message );
                            return;
                        }
                        if (0 >= this.ControlMaster.Karma)
                        {
                            if ( this.Fame >= 10000 && this.ControlMaster.Backpack.ConsumeTotal( typeof( Gold ), 3000 ))
                            {
                                new Nightmare().Rider = this;
                                message = "As you command.";
                                this.Say( message );
                            }
                            else
                            {
                                message = "I need more fame or gold to ride such a creature.";
                                this.Say( message );
                            }
                        }
                        else
                        {
                            message = addressmeas + ", I don't think virtous people should ride these beasts.";
                            this.Say( message );
                        }
                    }
////////////////////////////////////////////////////////////////////////////////////////////////
//  Food Preperation Command
////////////////////////////////////////////////////////////////////////////////////////////////


                    else if (e.Speech.ToLower() == this.Name.ToLower() +" food" || e.Speech.ToLower() == this.Name.ToLower() +" cook" || e.Speech.ToLower() == this.Name.ToLower() +" dinner")
                    {
                        if ((this.Skills[SkillName.Cooking].Base / 100) >= Utility.RandomDouble() )
                        {
                            int c = 0;
//Ribs
                            if ( this.ControlMaster.Backpack != null && this.ControlMaster.Backpack.ConsumeTotal( typeof( RawRibs ), 1 ) )
                            {
                                do
                                {
                                    this.ControlMaster.AddToBackpack( new Ribs( 1 ));
                                    c = ( c + 1 );

                                }
                                while( this.ControlMaster.Backpack.ConsumeTotal( typeof( RawRibs ), 1 ));

                                message = "Not even Kings have tasted Ribs as tender as these.";
                                this.Say( message );

                                if ( 0.5 >= Utility.RandomDouble() && this.Skills[SkillName.Cooking].Base < 100 )
                                {
                                    this.Skills[SkillName.Cooking].Base = this.Skills[SkillName.Cooking].Base + 1;
                                    //message = "My Skill as a chef has improved.";
                                    //this.Say( message );
                                }
                                c= 0;
                            }
//Lamb
                            else if ( this.ControlMaster.Backpack != null && this.ControlMaster.Backpack.ConsumeTotal( typeof( RawLambLeg ), 1 ) )
                            {
                                do
                                {
                                    this.ControlMaster.AddToBackpack( new LambLeg( 1 ));
                                    c = ( c + 1 );

                                }
                                while( this.ControlMaster.Backpack.ConsumeTotal( typeof( RawLambLeg ), 1 ));

                                message = "Smoked Lamb, this dish is my Specialty.  Enjoy Sir.";
                                this.Say( message );

                                if ( 0.5 >= Utility.RandomDouble() && this.Skills[SkillName.Cooking].Base < 100 )
                                {
                                    this.Skills[SkillName.Cooking].Base = this.Skills[SkillName.Cooking].Base + 1;
                                    //message = "My Skill as a chef has improved.";
                                    //this.Say( message );
                                }
                                c= 0;
                            }
//Fish
                            else if ( this.ControlMaster.Backpack != null && this.ControlMaster.Backpack.ConsumeTotal( typeof( RawFishSteak ), 1 ) )
                            {
                                do
                                {
                                    this.ControlMaster.AddToBackpack( new FishSteak( 1 ));
                                    c = ( c + 1 );

                                }
                                while( this.ControlMaster.Backpack.ConsumeTotal( typeof( RawFishSteak ), 1 ));

                                message = "I detest the stench of fish.  Next time cook it your self.";
                                this.Say( message );

                                if ( 0.5 >= Utility.RandomDouble() && this.Skills[SkillName.Cooking].Base < 100 )
                                {
                                    this.Skills[SkillName.Cooking].Base = this.Skills[SkillName.Cooking].Base + 1;
                                    //message = "My Skill as a chef has improved.";
                                    //this.Say( message );
                                }
                                c= 0;
                            }
//Poultry
                            else if ( this.ControlMaster.Backpack != null && this.ControlMaster.Backpack.ConsumeTotal( typeof( RawBird ), 1 ) )
                            {
                                do
                                {
                                    this.ControlMaster.AddToBackpack( new CookedBird( 1 ));
                                    c = ( c + 1 );

                                }
                                while( this.ControlMaster.Backpack.ConsumeTotal( typeof( RawBird ), 1 ));

                                message = "Why is it that I do all the cooking, and you do all the eating. I'm Hungry too.";
                                this.Say( message );

                                if ( 0.5 >= Utility.RandomDouble() && this.Skills[SkillName.Cooking].Base < 100 )
                                {
                                    this.Skills[SkillName.Cooking].Base = this.Skills[SkillName.Cooking].Base + 1;
                                    //message = "My Skill as a chef has improved.";
                                    //this.Say( message );
                                }
                                c= 0;
                            }
                            else
                            {
                                message = "But we have no meat to cook, Lord.";
                                this.Say( message );
                            }
                        }
                        else
                        {
                            message = "I am sorry, My " + addressmeas + ". But I'm afraid the food has burnt.";
                            this.Say( message );
                            if (this.ControlMaster.Backpack != null && this.ControlMaster.Backpack.ConsumeTotal( typeof( RawBird ), 10 )){}
                            if (this.ControlMaster.Backpack != null && this.ControlMaster.Backpack.ConsumeTotal( typeof( RawFishSteak ), 10 )){}
                            if (this.ControlMaster.Backpack != null && this.ControlMaster.Backpack.ConsumeTotal( typeof( RawLambLeg ), 10 )){}
                            if (this.ControlMaster.Backpack != null && this.ControlMaster.Backpack.ConsumeTotal( typeof( RawRibs ), 10 )){}
                        }
                    }
////////////////////////////////////////////////////////////////////////////////////////////////
//  Smelt Ore
////////////////////////////////////////////////////////////////////////////////////////////////

			else if (e.Speech.ToLower() == this.Name.ToLower() +" smelt")
			{

			if ((this.Skills[SkillName.Mining].Base / 100) >= Utility.RandomDouble() )
			{
				int i = 0;
//Iron
				if ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( IronOre ), 1 ) )
					{
						do
						{
							from.AddToBackpack( new IronIngot( 2 ));
							i = ( i + 1 );
						}
						while( from.Backpack.ConsumeTotal( typeof( IronOre ), 1 ));
					}
				i = 0;
//DullCopper
				if ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( DullCopperOre ), 1 ) )
					{
						do
						{
							from.AddToBackpack( new DullCopperIngot( 2 ));
							i = ( i + 1 );
						}
						while( from.Backpack.ConsumeTotal( typeof( DullCopperOre ), 1 ));
					}
				i = 0;
//ShadowIron
				if ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( ShadowIronOre ), 1 ) )
					{
						do
						{
							from.AddToBackpack( new ShadowIronIngot( 2 ));
							i = ( i + 1 );
						}
						while( from.Backpack.ConsumeTotal( typeof( ShadowIronOre ), 1 ));
					}
				i = 0;
//Copper
				if ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( CopperOre ), 1 ) )
					{
						do
						{
							from.AddToBackpack( new CopperIngot( 2 ));
							i = ( i + 1 );
						}
						while( from.Backpack.ConsumeTotal( typeof( CopperOre ), 1 ));
					}
				i = 0;
//Bronze
				if ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( BronzeOre ), 1 ) )
					{
						do
						{
							from.AddToBackpack( new BronzeIngot( 2 ));
							i = ( i + 1 );
						}
						while( from.Backpack.ConsumeTotal( typeof( BronzeOre ), 1 ));
					}
				i = 0;
//Gold
				if ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( GoldOre ), 1 ) )
					{
						do
						{
							from.AddToBackpack( new GoldIngot( 2 ));
							i = ( i + 1 );
						}
						while( from.Backpack.ConsumeTotal( typeof( GoldOre ), 1 ));
					}
				i = 0;
//Agapite
				if ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( AgapiteOre ), 1 ) )
					{
						do
						{
							from.AddToBackpack( new AgapiteIngot( 2 ));
							i = ( i + 1 );
						}
						while( from.Backpack.ConsumeTotal( typeof( AgapiteOre ), 1 ));
					}
				i = 0;
//Verite
				if ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( VeriteOre ), 1 ) )
					{
						do
						{
							from.AddToBackpack( new VeriteIngot( 2 ));
							i = ( i + 1 );
						}
						while( from.Backpack.ConsumeTotal( typeof( VeriteOre ), 1 ));
					}
				i = 0;
//Valorite
				if ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( ValoriteOre ), 1 ) )
					{
						do
						{
							from.AddToBackpack( new ValoriteIngot( 2 ));
							i = ( i + 1 );
						}
						while( from.Backpack.ConsumeTotal( typeof( ValoriteOre ), 1 ));
					}
				i = 0;

				message = "I have smelted your ore, My Lord.";
				this.Say( message );
				if ( 0.5 >= Utility.RandomDouble() && this.Skills[SkillName.Mining].Base < 100 )
				{
				this.Skills[SkillName.Mining].Base = this.Skills[SkillName.Mining].Base + 1;
				message = "I Have Gained Skill In Mining.";
				this.Say( message );
				}

				if ( 0.2 >= Utility.RandomDouble() && this.RawStr < 175 )
				{
				this.RawStr = this.RawStr + 1;
				message = "I am Stronger Now!";
				this.Say( message );
				}


				// To add custom ore types just copy the above ^^^
			}
			else
			{
				message = "I Applolgize my Lord, but I was unable to smelt the ore.";
				this.Say( message );
				if (from.Backpack != null && from.Backpack.ConsumeTotal( typeof( ValoriteOre ), 10 )){}
				if (from.Backpack != null && from.Backpack.ConsumeTotal( typeof( VeriteOre ), 10 )){}
				if (from.Backpack != null && from.Backpack.ConsumeTotal( typeof( AgapiteOre ), 10 )){}
				if (from.Backpack != null && from.Backpack.ConsumeTotal( typeof( BronzeOre ), 10 )){}
				if (from.Backpack != null && from.Backpack.ConsumeTotal( typeof( CopperOre ), 10 )){}
				if (from.Backpack != null && from.Backpack.ConsumeTotal( typeof( DullCopperOre ), 10 )){}
				if (from.Backpack != null && from.Backpack.ConsumeTotal( typeof( ShadowIronOre ), 10 )){}
				if (from.Backpack != null && from.Backpack.ConsumeTotal( typeof( GoldOre ), 10 )){}
				if (from.Backpack != null && from.Backpack.ConsumeTotal( typeof( IronOre ), 10 )){}
			}
		}

		//}
	//}
////////////////////////////////////////////////////////////////////////////////////////////////
//  Post Skill Levels
////////////////////////////////////////////////////////////////////////////////////////////////

                    //Swords
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" sword skill")
                    {
                        message = this.Skills[SkillName.Swords].Base.ToString();
                        this.Say( "My Present Swordsmanship Skill Is, " );
                        this.Say( message );
                    }
                    //Healing
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" healing skill")
                    {
                        message = this.Skills[SkillName.Healing].Base.ToString();
                        this.Say( "My Present Healing Skill Is, " );
                        this.Say( message );
                    }
                    //Cooking
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" cooking skill")
                    {
                        message = this.Skills[SkillName.Cooking].Base.ToString();
                        this.Say( "My Present Cooking Skill Is, " );
                        this.Say( message );
                    }
                    //Tactics
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" tactic skill")
                    {
                        message = this.Skills[SkillName.Tactics].Base.ToString();
                        this.Say( "My Present Tactics Skill Is, " );
                        this.Say( message );
                    }
                    //Parry
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" parry skill")
                    {
                        message = this.Skills[SkillName.Parry].Base.ToString();
                        this.Say( "My Present Parrying Skill Is, " );
                        this.Say( message );
                    }
                    //Mining
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" mining skill")
                    {
                        message = this.Skills[SkillName.Mining].Base.ToString();
                        this.Say( "My Present Mining Skill Is, " );
                        this.Say( message );
                    }
                    //Magery
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" mage skill")
                    {
                        message = this.Skills[SkillName.Magery].Base.ToString();
                        this.Say( "My Present Magery Skill Is, " );
                        this.Say( message );
                    }
                    //Meditation
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" meditation skill")
                    {
                        message = this.Skills[SkillName.Meditation].Base.ToString();
                        this.Say( "My Present Meditation Skill Is, " );
                        this.Say( message );
                    }
                    //Stealing
                    /*else if (e.Speech.ToLower() == "stealing skill")
                    {
                        message = this.Skills[SkillName.Stealing].Base.ToString();
                        this.Say( "My Present Stealing Skill Is, " );
                        this.Say( message );
                    }
                    //Fencing
                    else if (e.Speech.ToLower() == "fencing skill")
                    {
                        message = this.Skills[SkillName.Fencing].Base.ToString();
                        this.Say( "My Present Fencing Skill Is, " );
                        this.Say( message );
                    }*/
                    //Anatomy
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" anatomy skill")
                    {
                        message = this.Skills[SkillName.Anatomy].Base.ToString();
                        this.Say( "My Present Anatomy Skill Is, " );
                        this.Say( message );
                    }
                    //Focus
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" focus skill")
                    {
                        message = this.Skills[SkillName.Focus].Base.ToString();
                        this.Say( "My Present Focus Skill Is, " );
                        this.Say( message );
                    }
                    //Wrestling
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" wrestling skill")
                    {
                        message = this.Skills[SkillName.Wrestling].Base.ToString();
                        this.Say( "My Present Wrestling Skill Is, " );
                        this.Say( message );
                    }
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Disrobe Command
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    else if (e.Speech.ToLower() == this.Name.ToLower() +" disrobe" )
                    {
                        if ( this.Female && !this.ControlMaster.Female )
                            message = "Sir, I am your loyal soldier, not a whore.  I'll do as you command, but HANDS OFF.";
                        else if ( !this.Female && !this.ControlMaster.Female)
                            message = "Turn around my Lord. It's not right for men to do this in front of one another.";
                        else if ( this.Female && this.ControlMaster.Female)
                            message = "But my Lady, this is quite undignified.";
                        else if ( !this.Female && this.ControlMaster.Female)
                            message = "You show me your's, and I'll show you mine, M'Lady.";
                        else
                            message = " ";

                        Container pack = this.Backpack;
                        ArrayList equipitems = new ArrayList(this.Items);
                        foreach (Item item in equipitems)
                        {
                            if (item.Movable != false)
                            {
                                if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair) && (item.Layer != Layer.Mount) && (item.Layer != Layer.OneHanded))
                                {
                                    pack.DropItem( item );
                                }
                            }
                        }
                        this.Say( message );
                    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Disarm Command
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    else if (e.Speech.ToLower() == this.Name.ToLower() +" disarm" )
                    {
                        message = "Yes, My " + addressmeas;

                        Container pack = this.Backpack;
                        ArrayList equipitems = new ArrayList(this.Items);
                        foreach (Item item in equipitems)
                        {
                            if (item.Movable != false)
                            {
                                if (item.Layer == Layer.OneHanded || item.Layer == Layer.TwoHanded)
                                {
                                    pack.DropItem( item );
                                }
                            }
                        }
                        this.Say( message );
                    }
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Heal
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    else if (e.Speech.ToLower() == this.Name.ToLower() +" heal me" )
                    {
                        if (this.Backpack != null && this.Backpack.ConsumeTotal( typeof( Bandage ), 1 ) || this.ControlMaster.Backpack != null && this.ControlMaster.Backpack.ConsumeTotal( typeof( Bandage ), 1 ))
                        {

                            if ((this.Skills[SkillName.Healing].Base / 100 ) >= Utility.RandomDouble())
                            {
                            
                                if (this.ControlMaster.Hits < 70)    //need a healing delay here   check HealTimer     speed = (int)Math.Floor( speed * (bonus + 100.0) / 100.0 ); //(int)(Math.Floor((this.ControlMaster.RawStr / 2) + 50) * (this.Skills[SkillName.Healing].Base / 100)) >= this.ControlMaster.Hits)
                                    this.ControlMaster.Hits = this.ControlMaster.Hits + 20;   //Different amounts for different skill level                         //(int)(Math.Floor((this.ControlMaster.RawStr / 2) + 50) * (this.Skills[SkillName.Healing].Base / 100));
                                else
                                    this.Say( "Stop being such a wimp.  You only have a few minor scratchs.");
                                if ( 0.1 >= Utility.RandomDouble() && this.Skills[SkillName.Healing].Base < 100 )
                                {
                                    this.Skills[SkillName.Healing].Base = this.Skills[SkillName.Healing].Base + 0.1;
                                    //this.Say( "My Medical Knowledge has increased to, " );
                                    //this.Say( this.Skills[SkillName.Healing].Base.ToString() );
                                }
                            }
                            else
                            {
                                if (this.Backpack != null && this.Backpack.ConsumeTotal( typeof( Bandage ), 1 ) || this.ControlMaster.Backpack != null && this.ControlMaster.Backpack.ConsumeTotal( typeof( Bandage ), 1 ))
                                {
                                    message = "I am afraid my skill was insufficiant to heal your wounds, My " + addressmeas;
                                    this.Say( message );
                                }
                            }
                        }
                        else
                            this.Say( "We have no more Bandages!" );
                    }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Resurrect
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    /*else if (e.Speech.ToLower() == "res" || e.Speech.ToLower() == "resurrect")
                    {

                        if ( !this.ControlMaster.Alive )
                        {
                            if (this.Skills[SkillName.Healing].Base >= 80.0)
                            {
                                Resurrect( this.ControlMaster );
                                this.Say("I shall summon thee back from the abyss, my " + addressmeas);
                            }
                            else
                                this.Say("Be gone you dreadful spirit. I have not the skill to bring thee back.");
                        }
                        else
                            this.Say(addressmeas + ", You are very much alive already.");
                    }*/
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Herald
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //string said = e.Speech.ToLower();
                    else if (e.Speech.ToLower() == (this.Name.ToLower() + " herald my coming") )//e.Speech.ToLower().IndexOf( this.Name.ToLower() + "herald my coming" ) >= 0)
                    {
                        string phrase = ((PlayerMobile)from).CityTitle.ToLower();
                        this.PlaySound( 747 );
                        this.Say( "Ladies and Gentlemen, Boys and Girls, Soldiers, Slaves, and Citizens from all Regions. Please gather around, it is my privelage NAE it is my honor to introduce my leige " + addressmeas + " " + mastername + ".");
                        if ( ((PlayerMobile)from).CityTitle != null)
                        this.Say( addressmeas + " " + mastername + " is a " + phrase + " from the Region of " + ((PlayerMobile)from).City.CityName.ToString() + ".");
                    }
            }
            base.OnSpeech( e ); 
        } 
        }
        }
        #endregion
        
        public bool Validate(Mobile from)
	  {
	         if(from.Region.Name != "Cove" && from.Region.Name != "Britain" &&//towns
                   from.Region.Name != "Jhelom" && from.Region.Name != "Minoc" &&//towns
                   from.Region.Name != "Haven" && from.Region.Name != "Trinsic" &&//towns
                   from.Region.Name != "Vesper" && from.Region.Name != "Yew" &&//towns
                   from.Region.Name != "Wind" && from.Region.Name != "Serpent's Hold" &&//towns
                   from.Region.Name != "Skara Brae" && from.Region.Name != "Nujel'm" &&//towns
                   from.Region.Name != "Moonglow" && from.Region.Name != "Magincia" &&//towns
                   from.Region.Name != "Delucia" && from.Region.Name != "Papua" &&//towns
                   from.Region.Name != "Buccaneer's Den" && from.Region.Name != "Ocllo" &&//towns
                   from.Region.Name != "Gargoyle City" && from.Region.Name != "Mistas" &&//towns
                   from.Region.Name != "Montor" && from.Region.Name != "Alexandretta's Bowl" &&//towns
                   from.Region.Name != "Lenmir Anfinmotas" && from.Region.Name != "Reg Volon" &&//towns
                   from.Region.Name != "Bet-Lem Reg" && from.Region.Name != "Lake Shire" &&//towns
                   from.Region.Name != "Ancient Citadel" && from.Region.Name != "Luna" &&//towns
                   from.Region.Name != "Umbra" && //towns
                   from.Region.Name != "Moongates" &&
                   from.Region.Name != "Covetous" && from.Region.Name != "Deceit" &&//dungeons
                   from.Region.Name != "Despise" && from.Region.Name != "Destard" &&//dungeons
                   from.Region.Name != "Hythloth" && from.Region.Name != "Shame" &&//dungeons
                   from.Region.Name != "Wrong" && from.Region.Name != "Terathan Keep" &&//dungeons
                   from.Region.Name != "Fire" && from.Region.Name != "Ice" &&//dungeons
	  	   from.Region.Name != "Rock Dungeon" && from.Region.Name != "Spider Cave" &&//dungeons
	  	   from.Region.Name != "Spectre Dungeon" && from.Region.Name != "Blood Dungeon" &&//dungeons
	  	   from.Region.Name != "Wisp Dungeon" && from.Region.Name != "Ankh Dungeon" &&//dungeons
	  	   from.Region.Name != "Exodus Dungeon" && from.Region.Name != "Sorcerer's Dungeon" &&//dungeons
	  	   from.Region.Name != "Ancient Lair" && from.Region.Name != "Doom" &&//dungeons
	  	   from.Region.Name != "Britain Graveyard" && from.Region.Name != "Wrong Entrance" &&
	  	   from.Region.Name != "Covetous Entrance" && from.Region.Name != "Despise Entrance" &&
	  	   from.Region.Name != "Despise Passage" && from.Region.Name != "Jhelom Islands" &&
	  	   from.Region.Name != "Haven Island" && from.Region.Name != "Crystal Cave Entrance" &&
	  	   from.Region.Name != "Protected Island" && from.Region.Name != "Jail")
	  			   {
	  			   	return true;
	  			   }
	  			   else
	  			   {
	  			   	return false;
	  			   }
	  }
   private Point3D GetFireLocation( Mobile from )
		{
			if ( from.Region is DungeonRegion )
				return Point3D.Zero;

			if ( this.ControlMaster == null )
				return this.Location;

			ArrayList list = new ArrayList( 4 );

			AddOffsetLocation( from,  0, -1, list );
			AddOffsetLocation( from, -1,  0, list );
			AddOffsetLocation( from,  0,  1, list );
			AddOffsetLocation( from,  1,  0, list );

			if ( list.Count == 0 )
				return Point3D.Zero;

			int idx = Utility.Random( list.Count );
			return (Point3D) list[idx];
		}

		private void AddOffsetLocation( Mobile from, int offsetX, int offsetY, ArrayList list )
		{
			Map map = from.Map;

			int x = from.X + offsetX;
			int y = from.Y + offsetY;

			Point3D loc = new Point3D( x, y, from.Z );

			if ( map.CanFit( loc, 1 ) && from.InLOS( loc ) )
			{
				list.Add( loc );
			}
			else
			{
				loc = new Point3D( x, y, map.GetAverageZ( x, y ) );

				if ( map.CanFit( loc, 1 ) && from.InLOS( loc ) )
					list.Add( loc );
			}
		}
////////////////////////////////////////////////////////////////////////////////////////////////
//  Pack Access
////////////////////////////////////////////////////////////////////////////////////////////////
    public override bool IsSnoop( Mobile from )
    {
        if ( PackAnimal.CheckAccess( this, from ) )
            return false;

        return base.IsSnoop( from );
    }
    
    public override bool CheckNonlocalDrop( Mobile from, Item item, Item target )
    {
          return PackAnimal.CheckAccess( this, from );
    }

    public override bool CheckNonlocalLift( Mobile from, Item item )
    {
          return PackAnimal.CheckAccess( this, from );
    }
////////////////////////////////////////////////////////////////////////////////////////////////
//  Double Click
////////////////////////////////////////////////////////////////////////////////////////////////

    public override void OnDoubleClick( Mobile from )
    {
        PackAnimal.TryPackOpen( this, from );
    }
        #region [ OnThink ]
////////////////////////////////////////////////////////////////////////////////////////////////
//  Item Pickup
////////////////////////////////////////////////////////////////////////////////////////////////
    private DateTime m_NextPickup;
    public override void OnThink()
    {
        base.OnThink();

        if ( DateTime.Now < m_NextPickup )
            return;

        m_NextPickup = DateTime.Now + TimeSpan.FromSeconds( 10 + (6.0 * Utility.RandomDouble()) ); //10 was 2.5

        Container pack = this.Backpack;

        if ( pack == null || !this.Alive)
            return;

        ArrayList list = new ArrayList();

        foreach ( Item item in this.GetItemsInRange( 2 ) )
        {
            if ( item.Movable )
                list.Add( item );
        }

        for ( int i = 0; i < list.Count; ++i )
        {
            Item item = (Item)list[i];

            if ( !pack.CheckHold( this, item, false, true ) )
                return;

            bool rejected;
            LRReason reject;

            NextActionTime = Core.TickCount;

            Lift( item, item.Amount, out rejected, out reject );

            if ( rejected )
                continue;

            Drop( this, Point3D.Zero );
        }
        }
        #endregion
        
        public override bool OnBeforeDeath()
        {
                          if ( this.Mount is Mobile )
                             ((Mobile)this.Mount).Delete();
                 if ( !base.OnBeforeDeath() )
                     return false;
                     
                     /*foreach ( Item i in World.Items.Values )
                     {
                        if ( i is CityRecruitStone)
                        {
                          if ( ((CityRecruitStone)i).Leader == ((BaseCreature)this).ControlMaster && ((BaseCreature)this).IsBonded == false)
                          ((CityRecruitStone)i).Recruits -= 1;
                          ((BaseCreature)this).ControlMaster.SendMessage("You lost a soldier.");
                         }
                     }
                     foreach ( Item i in World.Items.Values )
                     {
                        if ( i is ArmyController)
                        {
                          if ( ((ArmyController)i).Soldiers.Contains( this ) && ((BaseCreature)this).IsBonded == false)
                          ((ArmyController)i).Soldiers.Remove( this );
                          ((BaseCreature)this).ControlMaster.SendMessage("You lost a soldier.");
                         }
                     }*/
                     return true;
	}
        /*#region [ GetContextMenuEntries ]
        public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
        { 
            Mobile Owner = GetOwner(); 
          
            if( Owner == null ) 
            { 
                base.GetContextMenuEntries( from, list ); 
                list.Add( new HireEntry( from, this ) ); 
            } 
            else 
                base.GetContextMenuEntries( from, list ); 
        } 
        #endregion 
        
        #region [ Class PayTimer ]
        private class PayTimer : Timer 
        { 
            private BaseSoldier m_Hire;
          
            public PayTimer( BaseSoldier vend ) : base( TimeSpan.FromMinutes( 30.0 ), TimeSpan.FromMinutes( 30.0 ) )
            { 
                m_Hire = vend; 
                Priority = TimerPriority.OneMinute; 
            } 
          
            protected override void OnTick() 
            { 
                int m_Pay = m_Hire.m_Pay; 
                if( m_Hire.m_HoldGold <= m_Pay ) 
                { 
                    // Get the current owner, if any (updates HireTable) 
                    Mobile owner = m_Hire.GetOwner(); 

                    m_Hire.Say( 503235 ); // I regret nothing!postal 
                    m_Hire.Delete(); 
                } 
                else 
                { 
                    m_Hire.m_HoldGold -= m_Pay; 
                } 
            } 
        } 
        #endregion 

        #region [ Class HireEntry ] 
        public class HireEntry : ContextMenuEntry 
        { 
            private Mobile m_Mobile; 
            private BaseSoldier m_Hire;

            public HireEntry( Mobile from, BaseSoldier hire ) : base( 6120, 3 )
            { 
                m_Hire = hire; 
                m_Mobile = from; 
            } 
          
            public override void OnClick()    
            {    
                m_Hire.Payday(m_Hire);
                m_Hire.SayHireCost(); 
            } 
        } 
        #endregion */
    }    
} 
