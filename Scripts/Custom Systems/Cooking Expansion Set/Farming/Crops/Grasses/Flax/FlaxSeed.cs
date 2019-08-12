using System;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
namespace Server.Items.Crops
{
    public class FlaxSeed : BaseCrop
    {
        public override bool CanGrowGarden { get { return true; } }

        [Constructable]
        public FlaxSeed() : this(1) { }

        [Constructable]
        public FlaxSeed(int amount)
            : base(0xF27)
        {
            Stackable = true;
            Weight = .1;
            Hue = 0x5E2;
            Movable = true;
            Amount = amount;
            Name = "Flax Seed";
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.Mounted && !CropHelper.CanWorkMounted) { from.SendMessage("You cannot plant a seed while mounted."); return; }
            Point3D m_pnt = from.Location;
            Map m_map = from.Map;
            if (!IsChildOf(from.Backpack)) { from.SendLocalizedMessage(1042010); return; }
            else if (!CropHelper.CheckCanGrow(this, m_map, m_pnt.X, m_pnt.Y)) { from.SendMessage("This seed will not grow here."); return; }
            ArrayList cropshere = CropHelper.CheckCrop(m_pnt, m_map, 0);
            if (cropshere.Count > 0) { from.SendMessage("There is already a crop growing here."); return; }
            ArrayList cropsnear = CropHelper.CheckCrop(m_pnt, m_map, 1);
            if ((cropsnear.Count > 2)) { from.SendMessage("There are too many crops nearby."); return; }
            if (this.BumpZ) ++m_pnt.Z;
            if (!from.Mounted) from.Animate(32, 5, 1, true, false, 0);
            from.SendMessage("You plant the seed.");
            this.Consume();
            Item item = new FlaxSeedling(from);
            item.Location = m_pnt;
            item.Map = m_map;
        }

        public FlaxSeed(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer) { base.Serialize(writer); writer.Write((int)0); }

        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
}