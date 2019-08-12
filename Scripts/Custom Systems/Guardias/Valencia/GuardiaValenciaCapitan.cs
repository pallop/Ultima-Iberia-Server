using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName("guard corpse")]
    public class GuardiaValenciaCapitan : BaseGuardiaValencia
    {
        [Constructable]
        public GuardiaValenciaCapitan()
            : base(AIType.AI_Melee)
        {
      SetStr(150, 250);
            SetDex(150, 250);
            SetInt(150, 250);

            SetHits(150, 250);
            SetMana(60);
 
            if (Utility.RandomBool())
            {
                new Horse().Rider = this;
            }

            SetResistance(ResistanceType.Physical, 90, 190);
            SetResistance(ResistanceType.Fire, 90, 190);
            SetResistance(ResistanceType.Cold, 90, 190);
            SetResistance(ResistanceType.Poison, 90, 190);

            SetSkill(SkillName.MagicResist, 100.0, 190.0);
            SetSkill(SkillName.Tactics, 100.0, 190.0);
            SetSkill(SkillName.Anatomy, 100.0, 190.0);
            SetSkill(SkillName.Healing, 100.0, 190.0);
            SetSkill(SkillName.Swords, 100.0, 190.0);
            SetSkill(SkillName.Parry, 100.0, 190.0);
            SetSkill(SkillName.Macing, 100.0, 190.0);
            SetSkill(SkillName.Fencing, 100.0, 190.0);

            Fame = 1000;
            Karma = 10000;

            VirtualArmor = 45;

            AddItem(new Boots(43));
            AddItem(new Cloak(43));
            PlateChest chest = new PlateChest();
            chest.Hue = 43;
            AddItem(chest);
			PlateGorget gorget = new PlateGorget();
            gorget.Hue = 43;
            AddItem(gorget);
            PlateLegs legs = new PlateLegs();
            legs.Hue = 43;
            AddItem(legs);
            PlateArms arms = new PlateArms();
            arms.Hue = 43;
            AddItem(arms);
			PlateGloves gloves = new PlateGloves();
            gloves.Hue = 43;
            AddItem(gloves);
            AddItem(new Bandage(20));

            switch (Utility.Random(4))
            {   //Swords
                case 0: AddItem(new Longsword());
                        AddItem(new MetalKiteShield());
                        SetDamage(15, 16);
                        SetResistance(ResistanceType.Physical, 50, 40);
                        break;
                case 1: AddItem(new Broadsword());
                        AddItem(new MetalKiteShield());
                        SetDamage(14, 15);
                        SetResistance(ResistanceType.Physical, 50, 40);
                        break;
                case 2: AddItem(new Halberd()); 
                        SetDamage(18, 22);
                        break;
                case 3: AddItem(new Bardiche());
                        SetDamage(17, 22); 
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

        public override Poison PoisonImmune { get { return Poison.Greater; } }

        public GuardiaValenciaCapitan(Serial serial)
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

            if (this.Hits < this.HitsMax / 3)
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