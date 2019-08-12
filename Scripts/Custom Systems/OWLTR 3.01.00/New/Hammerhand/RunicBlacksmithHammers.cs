/*
 created by:
	Hammerhand
*/
using Server.Engines.Craft;

namespace Server.Items
{
    public class DullCopperRunicHammer : RunicHammer
    {
        public override CraftSystem CraftSystem { get { return DefBlacksmithy.CraftSystem; } }
        [Constructable]
        public DullCopperRunicHammer()
            : base(CraftResource.DullCopper)
        {
            Name = "DullCopper Runic Hammer";
        }
        public DullCopperRunicHammer(Serial serial) : base(serial) { }

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
    public class ShadowIronRunicHammer : RunicHammer
    {
        public override CraftSystem CraftSystem { get { return DefBlacksmithy.CraftSystem; } }
        [Constructable]
        public ShadowIronRunicHammer()
            : base(CraftResource.ShadowIron)
        {
            Name = "ShadowIron Runic Hammer";
        }
        public ShadowIronRunicHammer(Serial serial) : base(serial) { }

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
    public class CopperRunicHammer : RunicHammer
    {
        public override CraftSystem CraftSystem { get { return DefBlacksmithy.CraftSystem; } }
        [Constructable]
        public CopperRunicHammer()
            : base(CraftResource.Copper)
        {
            Name = "Copper Runic Hammer";
        }
        public CopperRunicHammer(Serial serial) : base(serial) { }

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
    public class BronzeRunicHammer : RunicHammer
    {
        public override CraftSystem CraftSystem { get { return DefBlacksmithy.CraftSystem; } }
        [Constructable]
        public BronzeRunicHammer()
            : base(CraftResource.Bronze)
        {
            Name = "Bronze Runic Hammer";
        }
        public BronzeRunicHammer(Serial serial) : base(serial) { }

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
    public class GoldRunicHammer : RunicHammer
    {
        public override CraftSystem CraftSystem { get { return DefBlacksmithy.CraftSystem; } }
        [Constructable]
        public GoldRunicHammer()
            : base(CraftResource.Gold)
        {
            Name = "Gold Runic Hammer";
        }
        public GoldRunicHammer(Serial serial) : base(serial) { }

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
    public class AgapiteRunicHammer : RunicHammer
    {
        public override CraftSystem CraftSystem { get { return DefBlacksmithy.CraftSystem; } }
        [Constructable]
        public AgapiteRunicHammer()
            : base(CraftResource.Agapite)
        {
            Name = "Agapite Runic Hammer";
        }
        public AgapiteRunicHammer(Serial serial) : base(serial) { }

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
    public class VeriteRunicHammer : RunicHammer
    {
        public override CraftSystem CraftSystem { get { return DefBlacksmithy.CraftSystem; } }
        [Constructable]
        public VeriteRunicHammer()
            : base(CraftResource.Verite)
        {
            Name = "Verite Runic Hammer";
        }
        public VeriteRunicHammer(Serial serial) : base(serial) { }

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
    public class ValoriteRunicHammer : RunicHammer
    {
        public override CraftSystem CraftSystem { get { return DefBlacksmithy.CraftSystem; } }
        [Constructable]
        public ValoriteRunicHammer()
            : base(CraftResource.Valorite)
        {
            Name = "Valorite Runic Hammer";
        }
        public ValoriteRunicHammer(Serial serial) : base(serial) { }

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
    public class BlazeRunicHammer : RunicHammer
    {
        public override CraftSystem CraftSystem { get { return DefBlacksmithy.CraftSystem; } }
        [Constructable]
        public BlazeRunicHammer()
            : base(CraftResource.Blaze)
        {
            Name = "Blaze Runic Hammer";
        }
        public BlazeRunicHammer(Serial serial) : base(serial) { }

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
    public class IceRunicHammer : RunicHammer
    {
        public override CraftSystem CraftSystem { get { return DefBlacksmithy.CraftSystem; } }
        [Constructable]
        public IceRunicHammer()
            : base(CraftResource.Ice)
        {
            Name = "Ice Runic Hammer";
        }
        public IceRunicHammer(Serial serial) : base(serial) { }

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
    public class ToxicRunicHammer : RunicHammer
    {
        public override CraftSystem CraftSystem { get { return DefBlacksmithy.CraftSystem; } }
        [Constructable]
        public ToxicRunicHammer()
            : base(CraftResource.Toxic)
        {
            Name = "Toxic Runic Hammer";
        }
        public ToxicRunicHammer(Serial serial) : base(serial) { }

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
    public class ElectrumRunicHammer : RunicHammer
    {
        public override CraftSystem CraftSystem { get { return DefBlacksmithy.CraftSystem; } }
        [Constructable]
        public ElectrumRunicHammer()
            : base(CraftResource.Electrum)
        {
            Name = "Electrum Runic Hammer";
        }
        public ElectrumRunicHammer(Serial serial) : base(serial) { }

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
    public class PlatinumRunicHammer : RunicHammer
    {
        public override CraftSystem CraftSystem { get { return DefBlacksmithy.CraftSystem; } }
        [Constructable]
        public PlatinumRunicHammer()
            : base(CraftResource.Platinum)
        {
            Name = "Platinum Runic Hammer";
        }
        public PlatinumRunicHammer(Serial serial) : base(serial) { }

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