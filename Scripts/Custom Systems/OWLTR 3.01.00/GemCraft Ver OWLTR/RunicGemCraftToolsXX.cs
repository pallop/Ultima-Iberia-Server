/*
 created by:
	Hammerhand
*/
using Server.Engines.Craft;

namespace Server.Items
{
    public class BDRunicGemCraftTool : RunicGemCraftTool
    {
        public override CraftSystem CraftSystem { get { return DefGemCraft.CraftSystem; } }
        [Constructable]
        public BDRunicGemCraftTool()
            : base(CraftResource.BlueDiamond, 15)
        {
            Name = "BlueDiamond Runic GemcraftTool";
        }
        public BDRunicGemCraftTool(Serial serial) : base(serial) { }

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
    public class BARunicGemCraftTool : RunicGemCraftTool
    {
        public override CraftSystem CraftSystem { get { return DefGemCraft.CraftSystem; } }
        [Constructable]
        public BARunicGemCraftTool()
            : base(CraftResource.BrilliantAmber, 15)
        {
            Name = "BrilliantAmber Runic GemcraftTool";
        }
        public BARunicGemCraftTool(Serial serial) : base(serial) { }

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
    public class DSRunicGemCraftTool : RunicGemCraftTool
    {
        public override CraftSystem CraftSystem { get { return DefGemCraft.CraftSystem; } }
        [Constructable]
        public DSRunicGemCraftTool()
            : base(CraftResource.DarkSapphire, 50)
        {
            Name = "DarkSapphire Runic GemcraftTool";
        }
        public DSRunicGemCraftTool(Serial serial) : base(serial) { }

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
    public class ECRunicGemCraftTool : RunicGemCraftTool
    {
        public override CraftSystem CraftSystem { get { return DefGemCraft.CraftSystem; } }
        [Constructable]
        public ECRunicGemCraftTool()
            : base(CraftResource.EcruCitrine, 15)
        {
            Name = "EcruCitrine Runic GemcraftTool";
        }
        public ECRunicGemCraftTool(Serial serial) : base(serial) { }

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
    public class FRRunicGemCraftTool : RunicGemCraftTool
    {
        public override CraftSystem CraftSystem { get { return DefGemCraft.CraftSystem; } }
        [Constructable]
        public FRRunicGemCraftTool()
            : base(CraftResource.FireRuby, 15)
        {
            Name = "FireRuby Runic GemcraftTool";
        }
        public FRRunicGemCraftTool(Serial serial) : base(serial) { }

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
    public class PERunicGemCraftTool : RunicGemCraftTool
    {
        public override CraftSystem CraftSystem { get { return DefGemCraft.CraftSystem; } }
        [Constructable]
        public PERunicGemCraftTool()
            : base(CraftResource.PerfectEmerald, 15)
        {
            Name = "PerfectEmerald Runic GemcraftTool";
        }
        public PERunicGemCraftTool(Serial serial) : base(serial) { }

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
    public class TRunicGemCraftTool : RunicGemCraftTool
    {
        public override CraftSystem CraftSystem { get { return DefGemCraft.CraftSystem; } }
        [Constructable]
        public TRunicGemCraftTool()
            : base(CraftResource.Turquoise, 15)
        {
            Name = "Turquoise Runic GemcraftTool";
        }
        public TRunicGemCraftTool(Serial serial) : base(serial) { }

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
    public class WPRunicGemCraftTool : RunicGemCraftTool
    {
        public override CraftSystem CraftSystem { get { return DefGemCraft.CraftSystem; } }
        [Constructable]
        public WPRunicGemCraftTool()
            : base(CraftResource.WhitePearl, 15)
        {
            Name = "WhitePearl Runic GemcraftTool";
        }
        public WPRunicGemCraftTool(Serial serial) : base(serial) { }

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