using System;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
    [Flipable(0x153d, 0x153e)]
    public class ChefsFullApron : BaseMiddleTorso
    {
        public override SetItem SetID { get { return SetItem.Chefs; } }
        public override int Pieces { get { return 5; } }

        [Constructable]
        public ChefsFullApron()
            : this(0)
        {
        }

        [Constructable]
        public ChefsFullApron(int hue)
            : base(0x153d, hue)
        {
            Name = "Chef's Full Apron";
            Weight = 4.0;
            SetSkillBonuses.SetValues(0, SkillName.Cooking, 25);
            SetHue = 0x47E;
        }

        public ChefsFullApron(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute(0x1539, 0x153a)]
    public class ChefsLongPants : BasePants
    {
        public override SetItem SetID { get { return SetItem.Chefs; } }
        public override int Pieces { get { return 5; } }

        [Constructable]
        public ChefsLongPants()
            : this(0)
        {
        }

        [Constructable]
        public ChefsLongPants(int hue)
            : base(0x1539, hue)
        {
            Name = "Chef's Long Pants";
            Weight = 2.0;
            SetSkillBonuses.SetValues(0, SkillName.Cooking, 25);
            SetHue = 0x47E;
        }

        public ChefsLongPants(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute(0x1efd, 0x1efe)]
    public class ChefsFancyShirt : BaseShirt
    {

        public override SetItem SetID { get { return SetItem.Chefs; } }
        public override int Pieces { get { return 5; } }

        [Constructable]
        public ChefsFancyShirt()
            : this(0)
        {
        }

        [Constructable]
        public ChefsFancyShirt(int hue)
            : base(0x1EFD, hue)
        {
            Name = "Chef's Fancy Shirt";
            Weight = 2.0;
            SetSkillBonuses.SetValues(0, SkillName.Cooking, 25);
            SetHue = 0x47E;
        }

        public ChefsFancyShirt(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute(0x170f, 0x1710)]
    public class ChefsShoes : BaseShoes
    {
        public override SetItem SetID { get { return SetItem.Chefs; } }
        public override int Pieces { get { return 5; } }

        public override CraftResource DefaultResource
        {
            get
            {
                return CraftResource.RegularLeather;
            }
        }

        [Constructable]
        public ChefsShoes()
            : this(0)
        {
        }

        [Constructable]
        public ChefsShoes(int hue)
            : base(0x170F, hue)
        {
            Name = "Chef's Shoes";
            Weight = 2.0;
            SetSkillBonuses.SetValues(0, SkillName.Cooking, 25);
            SetHue = 0x47E;
        }

        public ChefsShoes(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class ChefsHat : BaseHat
    {
        public override SetItem SetID { get { return SetItem.Chefs; } }
        public override int Pieces { get { return 5; } }

        public override int BasePhysicalResistance
        {
            get
            {
                return 0;
            }
        }
        public override int BaseFireResistance
        {
            get
            {
                return 5;
            }
        }
        public override int BaseColdResistance
        {
            get
            {
                return 9;
            }
        }
        public override int BasePoisonResistance
        {
            get
            {
                return 5;
            }
        }
        public override int BaseEnergyResistance
        {
            get
            {
                return 5;
            }
        }

        public override int InitMinHits
        {
            get
            {
                return 20;
            }
        }
        public override int InitMaxHits
        {
            get
            {
                return 30;
            }
        }

        [Constructable]
        public ChefsHat()
            : this(0)
        {
        }

        [Constructable]
        public ChefsHat(int hue)
            : base(0x1715, hue)
        {
            Name = "Chef's Hat";
            Weight = 1.0;
            SetSkillBonuses.SetValues(0, SkillName.Cooking, 25);
            SetHue = 0x47E;
        }

        public ChefsHat(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
