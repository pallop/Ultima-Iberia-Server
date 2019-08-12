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
    public class EmptyDungBucket : Item
    {

        [Constructable]
        public EmptyDungBucket()
            : base(0x0E83)
        {
            Name = "an empty dung bucket";
        }

        public EmptyDungBucket(Serial serial)
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

    public class FullDungBucket : Item
    {

        [Constructable]
        public FullDungBucket()
            : base(0x0FAB)
        {
            Name = "a full dung bucket";
        }

        public FullDungBucket(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.SendMessage("What do you want to use this on?");
            from.Target = new InternalHayTarget(this);
        }

        private class InternalHayTarget : Target
        {
            FullDungBucket bucket;

            public InternalHayTarget(FullDungBucket dung)
                : base(3, true, TargetFlags.None)
            {
                bucket = dung;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                bool targetishay = false;

                if (targeted is SheafOfHay || targeted is DecoHay)
                {
                    targetishay = true;
                }
                else if (targeted is Static)
                {
                    Static target = (Static)targeted;
                    if (target.ItemID == 3892 || target.ItemID == 3894)
                        targetishay = true;
                }
                if (targetishay)
                {
                    try
                    {
                        object fertilizer = new BucketOfFertilizer();
                        BucketOfFertilizer buckettogive =
                            (BucketOfFertilizer) Activator.CreateInstance(fertilizer.GetType());
                        if (from.Backpack != null)
                            from.AddToBackpack(buckettogive);
                        else
                            buckettogive.DropToWorld(from, new Point3D(from.Location));
                        bucket.Delete();
                    }
                    catch
                    {
                        from.SendMessage("There was a problem turning your dung into fertilizer!");
                    }
                }
                else
                {
                    from.SendMessage("Use this on some hay.");
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
    }

    public class BucketOfFertilizer : Item
    {

        [Constructable]
        public BucketOfFertilizer()
            : base(0x0FAB)
        {
            Name = "a bucket of fertilizer";
            Hue = 142;
        }

        public BucketOfFertilizer(Serial serial)
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