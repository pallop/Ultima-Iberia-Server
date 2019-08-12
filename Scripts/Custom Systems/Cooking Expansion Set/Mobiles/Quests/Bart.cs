using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;

namespace Server.Engines.Quests
{
    public class HoneyCombs : BaseQuest
    {

        public HoneyCombs()
            : base()
        {
            this.AddObjective(new ObtainObjective(typeof(HoneyComb), "Honey Combs", 10, 0x1762));
            this.AddReward(new BaseReward(typeof(HoneycombProcessingKettle), "Honeycomb Processing Kettle"));
        }

        public override object Title
        {
            get
            {
                return "Honeycomb Hunting";
            }
        }

        public override object Description
        {
            get
            {
                return "Bart takes a good look at you. Hey, you there! You look like someone who's willing and able to help me! I am a candle crafter. Well, I used to be. I'm too old to roam the woods and collect honeycombs out of beehives. Get some for me! You find honeycombs in beehives, of course, but the bears that live in the close area around the hives tend to steal them and have honeycombs, too! You wont regret helping me! Get me five honeycombs and I'll give you one of these kettles. You can use them to sepparate honey and wax out of a honeycomb, so you can process it further!";
            }
        }
        /* I understand.  I certainly don’t want you to do something you don’t want to do. */
        public override object Refuse
        {
            get
            {
                return 1113501;
            }
        }

        public override object Uncomplete
        {
            get
            {
                return "Still haven't got the honeycombs I asked for!";
            }
        }
        /* Oh, thank you!  Here is your reward as promised.  I will get right back to work in a few minutes. */
        public override object Complete
        {
            get
            {
                return 1113503;
            }
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class Bart : MondainQuester
    {
        [Constructable]
        public Bart()
            : base("Bart")
        {
        }

        public Bart(Serial serial)
            : base(serial)
        {
        }

        public override Type[] Quests
        {
            get
            {
                return new Type[] 
                {
                    typeof(HoneyCombs)
                };
            }
        }

        public override void InitBody()
        {
            InitStats(100, 100, 25);

            Name = "Bart";
            Title = "the candle crafter";

            Female = false;
            Race = Race.Human;

            CantWalk = true;
            Hue = Utility.RandomSkinHue();
        }

        public override void InitOutfit()
        {
            AddItem(new Backpack());
            AddItem(new Shoes(0x737));
            AddItem(new LongPants(0x1BB));
            AddItem(new FancyShirt(0x535));
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