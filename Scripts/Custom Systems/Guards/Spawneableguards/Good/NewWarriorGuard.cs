using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName("guard corpse")]
    public class NewWarriorGuard : BaseGoodGuard
    {
        [Constructable]
        public NewWarriorGuard()
            : base(AIType.AI_Melee)
        {
            SetStr(95, 150);
            SetDex(60, 110);
            SetInt(26, 50);

            SetHits(80, 110);
            SetMana(40);
 
            if (Utility.RandomBool())
            {
                new Horse().Rider = this;
            }

            SetResistance(ResistanceType.Physical, 30, 20);
            SetResistance(ResistanceType.Fire, 20, 25);
            SetResistance(ResistanceType.Cold, 20, 20);
            SetResistance(ResistanceType.Poison, 20, 30);

            SetSkill(SkillName.MagicResist, 50.0, 80.0);
			SetSkill(SkillName.DetectHidden, 50.0, 100.0);
            SetSkill(SkillName.Tactics, 80.0, 120.0);
            SetSkill(SkillName.Anatomy, 80.0, 120.0);
            SetSkill(SkillName.Healing, 50.0, 65.0);
            SetSkill(SkillName.Swords, 80.0, 120.0);
            SetSkill(SkillName.Parry, 80.0, 120.0);
            SetSkill(SkillName.Macing, 80.0, 120.0);
            SetSkill(SkillName.Fencing, 80.0, 120.0);

            Fame = 1000;
            Karma = 10000;

            VirtualArmor = 25;

            AddItem(new Boots(0x2A));
            AddItem(new Cloak(0x2A));
            ChainChest chest = new ChainChest();
            chest.Hue = 0x2A;
            AddItem(chest);
            ChainLegs legs = new ChainLegs();
            legs.Hue = 0x2A;
            AddItem(legs);
            ChainHatsuburi head = new ChainHatsuburi();
            head.Hue = 0x2A;
            AddItem(head);
            AddItem(new Bandage(30));

            switch (Utility.Random(19))
            {   //Swords
                case 0: AddItem(new Longsword());
                        AddItem(new WoodenKiteShield());
                        SetDamage(5, 18);
                        SetResistance(ResistanceType.Physical, 50, 40);
                        break;
                case 1: AddItem(new Cutlass());
                        AddItem(new WoodenKiteShield());
                        SetDamage(5, 15);
                        SetResistance(ResistanceType.Physical, 50, 40);
                        break;
                case 2: AddItem(new Broadsword());
                        AddItem(new WoodenKiteShield());
                        SetDamage(8, 18);
                        SetResistance(ResistanceType.Physical, 50, 40);
                        break;
                case 3: AddItem(new Axe());
                        SetDamage(11, 19); 
                        break;
                case 4: AddItem(new Halberd()); 
                        SetDamage(13, 22);
                        break;
                case 5: AddItem(new Bardiche());
                        SetDamage(11, 22); 
                        break;
                case 6: AddItem(new TwoHandedAxe()); 
                        SetDamage(13, 23);
                        break;
                //Maces
                case 7: AddItem(new Mace());
                        AddItem(new WoodenKiteShield());
                        SetDamage(6, 18);
                        SetResistance(ResistanceType.Physical, 50, 40);
                        break;
                case 8: AddItem(new Club());
                        AddItem(new WoodenKiteShield()); 
                        SetDamage(5, 14);
                        SetResistance(ResistanceType.Physical, 50, 40);
                        break;
                case 9: AddItem(new Maul());
                        AddItem(new WoodenKiteShield());
                        SetDamage(9, 18);
                        SetResistance(ResistanceType.Physical, 50, 40);
                        break;
                case 10: AddItem(new HammerPick());
                         SetDamage(10, 22);
                         break;
                case 11: AddItem(new WarHammer()); 
                         SetDamage(13, 28); 
                         break;
                case 12: AddItem(new BlackStaff()); 
                         SetDamage(11, 17);
                         break;
                //Fencing
                case 13: AddItem(new Dagger());
                         AddItem(new WoodenKiteShield());
                         SetDamage(5, 13);
                         SetResistance(ResistanceType.Physical, 50, 40);
                         break;
                case 14: AddItem(new Kryss());
                         AddItem(new WoodenKiteShield());
                         SetDamage(6, 16);
                         SetResistance(ResistanceType.Physical, 50, 40);
                         break;
                case 15: AddItem(new WarFork());
                         AddItem(new WoodenKiteShield());
                         SetDamage(8, 18);
                         SetResistance(ResistanceType.Physical, 50, 40);
                         break;
                case 16: AddItem(new ShortSpear()); 
                         SetDamage(11, 23);
                         break;
                case 17: AddItem(new Spear()); 
                         SetDamage(13, 24);
                         break;
                case 18: AddItem(new Pike()); 
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

        public NewWarriorGuard(Serial serial)
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
                this.Say("Tus hechizos no te ayudaran.");
            }
            if (this.Hits < this.HitsMax / 2)
            {
                if (BandageContext.GetContext(this) == null)
                {
                    BandageContext.BeginHeal(this, this);
                    this.Say("Aplicandose un vendaje.");
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
