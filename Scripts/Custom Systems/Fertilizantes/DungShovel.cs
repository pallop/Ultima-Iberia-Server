// Dung and Fertilizer System
// Written for Free Ultima Online Emulation Shards
// by Lokai (c) 2015
/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
using System;
using Server.Targeting;

namespace Server.Items
{
    public class DungShovel : BaseTool
    {
        [Constructable]
        public DungShovel()
            : this(50)
        {
        }

        [Constructable]
        public DungShovel(int uses)
            : base(uses, 0xF39)
        {
            this.Weight = 5.0;
            Name = "a dung shovel";
        }

        public DungShovel(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.SendMessage("What do you want to use this on?");
            from.Target = new InternalDungTarget(this);
        }

        private class InternalDungTarget : Target
        {
            DungShovel shovel;

            public InternalDungTarget(DungShovel tool)
                : base(4, true, TargetFlags.None)
            {
                shovel = tool;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is DungPile)
                {
                    from.SendMessage("What dung bucket do you want to place this in?");
                    from.Target = new InternalBucketTarget(shovel, (DungPile)targeted);
                }
                else
                {
                    from.SendMessage("Use this on a pile of dung.");
                }
            }
        }

        private class InternalBucketTarget : Target
        {
            DungShovel shovel;
            DungPile dung;

            public InternalBucketTarget(DungShovel tool, DungPile resource)
                : base(3, true, TargetFlags.None)
            {
                shovel = tool;
                dung = resource;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is EmptyDungBucket)
                {
                    EmptyDungBucket bucket = (EmptyDungBucket)targeted;
                    if (bucket.Parent == from.Backpack)
                    {
                        try
                        {
                            object fullbucket = new FullDungBucket();
                            FullDungBucket full = (FullDungBucket) Activator.CreateInstance(fullbucket.GetType());
                            full.Hue = dung.Hue;
                            if (from.Backpack != null)
                                from.AddToBackpack(full);
                            else
                                full.DropToWorld(from, new Point3D(from.Location));
                            bucket.Delete();
                            dung.Delete();
                            shovel.UsesRemaining -= 1;
                            if (shovel.UsesRemaining <= 0)
                            {
                                shovel.Delete();
                                from.SendMessage("You have worn out your shovel!");
                            }
                        }
                        catch
                        {
                            from.SendMessage("There was a problem filling your bucket!");
                        }
                    }
                    else
                    {
                        from.SendMessage("I know it stinks, but you must have the bucket in your pack to fill it.");
                    }
                }
                else
                {
                    from.SendMessage("That is not a dung bucket.");
                }
            }
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

        public override Engines.Craft.CraftSystem CraftSystem
        {
            get { return null; }
        }
    }
}