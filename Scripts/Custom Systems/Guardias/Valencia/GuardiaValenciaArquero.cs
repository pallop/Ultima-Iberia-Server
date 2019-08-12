using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName("guard corpse")]
    public class GuardiaValenciaArquero : BaseGuardiaValencia
    {
    private int currentweapon;

        [Constructable]
        public GuardiaValenciaArquero()
            : base(AIType.AI_Archer)
        {
            SetStr(100, 150);
            SetDex(100, 150);
            SetInt(100, 150);

            SetHits(80, 180);
            SetMana(60);

            if (Utility.RandomBool())
            {
                new Horse().Rider = this;
            }

            SetResistance(ResistanceType.Physical, 50, 120);
            SetResistance(ResistanceType.Fire, 50, 120);
            SetResistance(ResistanceType.Cold, 50, 120);
            SetResistance(ResistanceType.Poison, 50, 120);

            SetSkill(SkillName.MagicResist, 80.0, 120.0);
            SetSkill(SkillName.Tactics, 80.0, 120.0);
            SetSkill(SkillName.Anatomy, 80.0, 120.0);
            SetSkill(SkillName.Healing, 80.0, 120.0);
            SetSkill(SkillName.Archery, 80.0, 120.0);
            SetSkill(SkillName.Fencing, 80.0, 120.0);

            Fame = 1000;
            Karma = 10000;

            VirtualArmor = 16;

			AddItem(new Boots(43));
            AddItem(new Cloak(43));
			PackItem(new Dagger());
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


            switch (Utility.Random(2))
            {  
                case 0: AddItem(new CompositeBow());
                        AddItem(new Arrow(50));
                        SetDamage(5, 20);
                        currentweapon=2;
                        break;
                case 1: AddItem(new HeavyCrossbow());
                        AddItem(new Bolt(50));
                        SetDamage(18, 30);
                        currentweapon=4;
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

        public GuardiaValenciaArquero(Serial serial)
            : base(serial)
        {
        }

        public override void OnGaveMeleeAttack(Mobile d)
        {
           if (0.2 >= Utility.RandomDouble())
           {
               this.Say(500131);
           }

           if (d is BaseCreature)
           {
               if ((d as BaseCreature).IsDispellable)
               {
                   d.Hits -= Utility.RandomMinMax(20, 50);
               }
           }

           if (currentweapon==4)
           {
            if( this.FindItemOnLayer(Layer.TwoHanded) != null )
            {
              if (0.2 >= Utility.RandomDouble())
              {
         	    IMount mount = d.Mount;
 
                    if ( mount != null )
                    {
	                  mount.Rider = null;

	                  if ( d is PlayerMobile )
                          {
                              d.SendLocalizedMessage(1040023); // You have been knocked off of your mount!
                          }
                          else
                          {
	                  ((Mobile)mount).Kill();
                          }
                    }
              }
            }
           }

        }

        public override void OnGotMeleeAttack(Mobile d)
        {
                Item item = this.FindItemOnLayer( Layer.TwoHanded );
                if (item != null)
                {
                    Item dagger = this.Backpack.FindItemByType(typeof(Dagger));
                    this.Backpack.DropItem(item);
                    EquipItem(dagger);
                    SetDamage(10, 12);
                    InvalidateProperties();
                }

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

                            if (this.Combatant == t)
                            {
                                this.Provoke(this, t.ControlMaster, true);
                            }
                        }
                    }
                }
        }

        public override void OnActionCombat()
        {
            Item item = this.FindItemOnLayer(Layer.OneHanded);
            if (item != null)
            {
                if (!InRange(this.Combatant, 1))
                {
                    this.Backpack.DropItem(item);

                    switch(currentweapon)
                    {
                    case 0: Item rangedcbow = this.Backpack.FindItemByType(typeof(CompositeBow));
                            EquipItem(rangedcbow);
                            SetDamage(5, 20);
                            break;
                    
                    case 1: Item rangedhcross = this.Backpack.FindItemByType(typeof(HeavyCrossbow));
                            EquipItem(rangedhcross);
                            SetDamage(18, 30);
                            break;
                    }
                    InvalidateProperties();
                    this.Warmode = true;
                }
            }
        }
        public override void OnDamagedBySpell(Mobile d)
        {
            if (0.3 >= Utility.RandomDouble())
            {
                this.Say("Tus brujerias no te ayudaran.");
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
            writer.Write( currentweapon );
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            currentweapon = reader.ReadInt();
        }
    }
}