using System;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
    public class WheatSheaf : Item
    {
        [Constructable]
        public WheatSheaf()
            : this(1)
        {
        }

        [Constructable]
        public WheatSheaf(int amount)
            : base(7869)
        {
            this.Weight = 1.0;
            this.Stackable = true;
            this.Amount = amount;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!this.Movable)
                return;

            from.BeginTarget(4, false, TargetFlags.None, new TargetCallback(OnTarget));
        }

        public virtual void OnTarget(Mobile from, object obj)
        {
            if (obj is AddonComponent)
                obj = (obj as AddonComponent).Addon;

            IFlourMill mill = obj as IFlourMill;

            if (mill != null)
            {
                int needs = mill.MaxFlour - mill.CurFlour;

                if (needs > this.Amount)
                    needs = this.Amount;

                mill.CurFlour += needs;
                this.Consume(needs);
            }
        }

        public WheatSheaf(Serial serial)
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