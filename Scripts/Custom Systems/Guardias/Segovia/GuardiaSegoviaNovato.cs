using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName("guard corpse")]
    public class GuardiaSegoviaNovato : BaseGuardiaSegovia
    {
        [Constructable]
        public GuardiaSegoviaNovato()
            : base(AIType.AI_Melee)
        {
             SetStr(100, 150);
            SetDex(80, 100);
            SetInt(20, 40);

            SetHits(80, 110);
            SetMana(40);
 
            if (Utility.RandomBool())
            {
                new Horse().Rider = this;
            }

            SetResistance(ResistanceType.Physical, 30, 50);
            SetResistance(ResistanceType.Fire, 30, 50);
            SetResistance(ResistanceType.Cold, 30, 50);
            SetResistance(ResistanceType.Poison, 30, 50);

            SetSkill(SkillName.MagicResist, 100.0, 120.0);
            SetSkill(SkillName.Tactics, 60.0, 80.0);
            SetSkill(SkillName.Anatomy, 60.0, 80.0);
            SetSkill(SkillName.Healing, 40.0, 55.0);
            SetSkill(SkillName.Swords, 60.0, 80.0);
            SetSkill(SkillName.Parry, 60.0, 90.0);
            SetSkill(SkillName.Macing, 60.0, 90.0);
            SetSkill(SkillName.Fencing, 60.0, 90.0);

            Fame = 1000;
            Karma = 10000;

            VirtualArmor = 25;

            AddItem(new Boots(202));
            AddItem(new Cloak(202));
            PlateChest chest = new PlateChest();
            chest.Hue = 202;
            AddItem(chest);
			PlateGorget gorget = new PlateGorget();
            gorget.Hue = 202;
            AddItem(gorget);
            PlateLegs legs = new PlateLegs();
            legs.Hue = 202;
            AddItem(legs);
            PlateArms arms = new PlateArms();
            arms.Hue = 202;
            AddItem(arms);
			PlateGloves gloves = new PlateGloves();
            gloves.Hue = 202;
            AddItem(gloves);
            AddItem(new Bandage(20));

            switch (Utility.Random(7))
            {   //Swords
                case 0: AddItem(new Longsword());
                        AddItem(new WoodenKiteShield());
                        SetDamage(5, 18);
                        SetResistance(ResistanceType.Physical, 50, 40);
                        break;
                case 1: AddItem(new Broadsword());
                        AddItem(new WoodenKiteShield());
                        SetDamage(8, 18);
                        SetResistance(ResistanceType.Physical, 50, 40);
                        break;
                case 2: AddItem(new Axe());
                        SetDamage(11, 19); 
                        break;
                case 3: AddItem(new TwoHandedAxe()); 
                        SetDamage(13, 23);
                        break;
                //Maces
                case 4: AddItem(new Mace());
                        AddItem(new WoodenKiteShield());
                        SetDamage(6, 18);
                        SetResistance(ResistanceType.Physical, 50, 40);
                        break;
                case 5: AddItem(new HammerPick());
                         SetDamage(10, 22);
                         break;
                case 6: AddItem(new WarHammer()); 
                         SetDamage(13, 28); 
                         break;
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
        }

	public override bool OnBeforeDeath()
	{
	    IMount mount = this.Mount;

            if ( mount != null )
	    mount.Rider = null;

	    if ( mount is Mobile )
	    ((Mobile)mount).Kill();

	    return base.OnBeforeDeath();
	}

        public override Poison PoisonImmune { get { return Poison.Lesser; } }

        public GuardiaSegoviaNovato(Serial serial)
            : base(serial)
        {
        }

        public override void OnGaveMeleeAttack(Mobile d)
        {
           if (0.2 >= Utility.RandomDouble())
               this.Say(500131);
           if (d is BaseCreature)
           {
               if ((d as BaseCreature).IsDispellable)
               {
                   d.Hits -= Utility.RandomMinMax(20, 50);
               }
           }
        }

        public override void OnGotMeleeAttack(Mobile d)
        {
            if (0.2 >= Utility.RandomDouble())
            {
                this.Say(500131);
            }

            if (this.Hits < this.HitsMax / 2)
            {
                if (BandageContext.GetContext(this) == null)
                {
                    BandageContext.BeginHeal(this, this);
                    this.Say("*Aplicandose vendajes*");
                }
            }

            if (d is BaseCreature)
            {
                BaseCreature t = (BaseCreature)d;
                if (t.Controlled && !(t as BaseCreature).IsDispellable)
                {
                    if (0.5 >= Utility.RandomDouble())
                    {
                        t.Combatant = null;
                        t.Warmode = false;
                        t.Pacify(this, DateTime.Now + TimeSpan.FromSeconds(20.0));
                    }
                    if (this.Combatant == t)
                    {
                        this.Provoke(this, t.ControlMaster, true);
                    }
                }
            }
        }

        public override void OnDamagedBySpell(Mobile d)
        {
            if (0.3 >= Utility.RandomDouble())
            {
                this.Say("Hereje, tus brujerias no podran salvarte");
            }
            if (this.Hits < this.HitsMax / 2)
            {
                if (BandageContext.GetContext(this) == null)
                {
                    BandageContext.BeginHeal(this, this);
                    this.Say("Se aplica un vendaje.");
                }
            }

            if (d is BaseCreature)
            {
                BaseCreature t = (BaseCreature)d;
                if (t.Controlled && !(t as BaseCreature).IsDispellable)
                {
                    if (0.5 >= Utility.RandomDouble())
                    {
                        t.Combatant = null;
                        t.Warmode = false;
                        t.Pacify(this, DateTime.Now + TimeSpan.FromSeconds(20.0));

                        if (this.Combatant == t)
                        {
                            this.Provoke(this, t.ControlMaster, true);
                        }
                    }
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}