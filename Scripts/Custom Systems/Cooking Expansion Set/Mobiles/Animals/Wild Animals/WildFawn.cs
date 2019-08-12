using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a deer corpse")]
    public class WildFawn : BaseCreature
    {
        [Constructable]
        public WildFawn()
            : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a Fawn";
            Body = 0xED;

            SetStr(21, 51);
            SetDex(47, 77);
            SetInt(17, 47);

            SetHits(15, 29);
            SetMana(0);

            SetDamage(4);

            SetDamageType(ResistanceType.Physical, 50);

            SetResistance(ResistanceType.Physical, 2, 3);
            SetResistance(ResistanceType.Cold, 1);

            SetSkill(SkillName.MagicResist, 5.0);
            SetSkill(SkillName.Tactics, 10.0);
            SetSkill(SkillName.Wrestling, 10.0);

            Fame = 0;
            Karma = 0;

            VirtualArmor = 4;

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 23.1;
        }

        public WildFawn(Serial serial)
            : base(serial)
        {
        }

        public override int Meat
        {
            get
            {
                return 1;
            }
        }
        public override MeatType MeatType
        {
            get
            {
                return MeatType.Venison;
            }
        }
        public override int Hides
        {
            get
            {
                return 1;
            }
        }
        public override FoodType FavoriteFood
        {
            get
            {
                return FoodType.FruitsAndVegies | FoodType.GrainsAndHay;
            }
        }
        public override int GetAttackSound() 
        { 
            return 0x82; 
        }

        public override int GetHurtSound() 
        { 
            return 0x83; 
        }

        public override int GetDeathSound() 
        { 
            return 0x84; 
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