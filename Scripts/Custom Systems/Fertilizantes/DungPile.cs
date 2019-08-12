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
using Server.Engines.Plants;
using Server.HuePickers;

namespace Server.Items
{
    public class DungPile : Item
    {

        [Constructable]
        public DungPile()
            : base(0x913)
        {
            Hue = Utility.RandomList(2308, 2309, 2310, 2311, 2312, 2313, 2314, 2315, 2316, 2317, 2318, 42);
            Name = "a pile of dung";
            Timer deleteTimer = new DeleteTimer(this);
            deleteTimer.Start();
        }

        private class DeleteTimer : Timer
        {
            private readonly DungPile _mDungPile;

            public DeleteTimer(DungPile dung) : base(TimeSpan.FromMinutes(4.0))
            {
                _mDungPile = dung;
            }

            protected override void OnTick()
            {
                if (_mDungPile != null) _mDungPile.Delete();
            }
        }

        public override bool OnDragLift(Mobile from)
        {
            switch (Utility.Random(1, 4))
            {
                case 1:
                    from.SendMessage("You don't want to pick that up!");
                    break;
                case 2:
                    from.SendMessage("You'll want to wash that finger now!");
                    break;
                case 3:
                    from.SendMessage("Don't you have a dung shovel?");
                    break;
                default:
                    from.SendMessage("You don't want to touch that with your hands!");
                    break;
            }
            return false;
        }

        public override bool Decays
        {
            get { return true; }
        }

        public DungPile(Serial serial)
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
            Timer deleteTimer = new DeleteTimer(this);
            deleteTimer.Start();
        }
    }
}



