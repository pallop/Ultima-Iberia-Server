using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
    public class GrayBrickFireplaceSouthAddon : BaseAddon
    {
        public override BaseAddonDeed Deed
        {
            get
            {
                return new GrayBrickFireplaceSouthDeed();
            }
        }

        [Constructable]
        public GrayBrickFireplaceSouthAddon()
        {
            AddonComponent ac = null;
            ac = new

            AddonComponent(0x8D4);
            ac.Name = "Gray Brick Fireplace";
            AddComponent(ac, 0, 0, 0);

            ac = new AddonComponent(0x94B);
            ac.Name = "Gray Brick Fireplace";
            AddComponent(ac, -1, 0, 0);
             
        }

        public GrayBrickFireplaceSouthAddon(Serial serial)
            : base(serial)
        {
        }

        public override void OnComponentUsed(AddonComponent ac, Mobile from)
        {
            if (!from.InRange(GetWorldLocation(), 2))
                from.SendMessage("You are too far away to use that!");
            else
            {
                if (ac.ItemID == 0x945)
                {
                    ac.ItemID = 0x8D4;
                    Effects.PlaySound(from.Location, from.Map, 0x4B9);
                    from.SendMessage("You put out the fireplace!");
                }
                else if (ac.ItemID == 0x8D4)
                {
                    Container pack = from.Backpack;

                    if (pack == null)
                        return;

                    int res = pack.ConsumeTotal(
                    new Type[]
				    {
					   typeof( Log )
				    },
                    new int[]
				    {
                        3
                    });
                    switch (res)
                    {
                        case 0:
                            {
                                from.SendMessage("You must have 3 logs to put in fireplace");
                                break;
                            }
                            default:
                            {
                                Effects.PlaySound(from.Location, from.Map, 0x137);
                                from.SendMessage("You put the logs in fireplace!");
                                ac.ItemID = 0x943;
                                break;
                            }
                    }
                }
                else if (ac.ItemID == 0x943)
                {
                    Item matchlight = from.Backpack.FindItemByType(typeof(MatchLight));

                    if (matchlight != null)
                    {
                        matchlight.Delete();
                        ac.ItemID = 0x945;
                        ac.Light = LightType.Circle225;
                        Effects.PlaySound(from.Location, from.Map, 0x4BA);
                        from.SendMessage("You light the fireplace!");
                    }
                    else
                    {
                        if (matchlight == null)
                        {
                            from.SendMessage("You must have a match to light the fireplace");
                        }
                    }
                }
                else
                    return;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

    }

    public class GrayBrickFireplaceSouthDeed : BaseAddonDeed
    {
        public override BaseAddon Addon
        {
            get
            {
                return new GrayBrickFireplaceSouthAddon();
            }
        }

        public override int LabelNumber
        {
            get
            {
                return 1061847;
            }
        }// grey brick fireplace (South)

        [Constructable]
        public GrayBrickFireplaceSouthDeed()
        {
        }

        public GrayBrickFireplaceSouthDeed(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}