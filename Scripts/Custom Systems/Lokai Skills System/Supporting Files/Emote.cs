/* 
 * Emote v2 by CMonkey123
 * Emote v3 by GM Jubal from Ebonspire
 * 
 * Emote v4 by Lokai
 * 	- Streamlined the code
 * 	- Shortened code using Arrays
 * 	- Added 3 Deaths and 3 Grunts
*/
/***************************************************************************
 *   File required for Hypnotism. This program is free software; you can 
 *   redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;
using System.IO;
using System.Text;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Commands;

namespace Server.Gumps
{
    public enum EmotePage { P1, P2, P3, P4, }

    public class Emote
    {
        public static void Initialize()
        {
            CommandSystem.Register("emote", AccessLevel.Player, new CommandEventHandler(Emote_OnCommand));
            CommandSystem.Register("e", AccessLevel.Player, new CommandEventHandler(Emote_OnCommand));
        }

        [Usage("emote <sound>")]
        [Description("Emote with sounds, words, and possibly an animation with one command!")]
        public static void Emote_OnCommand(CommandEventArgs e)
        {
            Mobile pm = e.Mobile;
            string em = e.ArgString.Trim().ToLower();
            int SoundInt = 0;
            for (int x = 0; x < Sounds.Length; x++)
                if (em == Sounds[x].ToLower()) { SoundInt = x; break; }
            if (SoundInt > 0) new ESound(pm, SoundInt);
            else pm.SendGump(new EmoteGump(pm, EmotePage.P1));
        }

        public static string[] Sounds = new string[] { "","Ah", "AhHa", "Applaud", "Blownose", "Bow", "BScough",
			"Burp", "ClearThroat", "Cough", "Cry", "Faint", "Fart", "Gasp", "Giggle", "Groan", "Growl",
			"Hey", "Hiccup", "Huh", "Kiss", "Laugh", "No", "Oh", "Oooh", "Oops", "Puke", "Punch", "Scream",
			"Shush", "Sigh", "Slap", "Sneeze", "Sniff", "Snore", "Spit", "StickOutTongue", "TapFoot", "Wistle",
			"Woohoo", "Yawn", "Yea", "Yell", "Die1", "Die2", "Die3", "Grunt1", "Grunt2", "Grunt3"
        };
    }

    public class EmoteGump : Gump
    {
        private Mobile m_From;
        private EmotePage m_Page;

        public void AddButtonLabeled(int x, int y, int buttonID, string text)
        {
            AddButton(x, y - 1, 4005, 4007, buttonID, GumpButtonType.Reply, 0);
            AddHtml(x + 35, y, 240, 20, Color(text, 0xFFFFFF), false, false);
        }

        public int GetButtonID(int type, int index)
        {
            return 1 + (index * 15) + type;
        }

        public string Color(string text, int color)
        {
            return String.Format("<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text);
        }

        public EmoteGump(Mobile from, EmotePage page)
            : base(600, 50)
        {
            from.CloseGump(typeof(EmoteGump));
            m_From = from;
            m_Page = page;
            Closable = true;
            Dragable = true;
            AddPage(0);
            AddBackground(0, 65, 130, 360, 5054);
            AddAlphaRegion(10, 70, 110, 350);
            AddImageTiled(10, 70, 110, 20, 9354);
            AddLabel(13, 70, 200, "Emote List");
            AddImage(100, 0, 10410);
            AddImage(100, 305, 10412);
            AddImage(100, 150, 10411);
            int y = 90;
            switch (page)
            {
                case EmotePage.P1:
                    {
                        for (int x = 1; x <= 12; x++)
                        {
                            AddButtonLabeled(10, y, GetButtonID(1, x), Emote.Sounds[x]);
                            y += 25;
                        }
                        AddButton(70, 380, 4502, 0504, GetButtonID(0, 2), GumpButtonType.Reply, 0);
                        break;
                    }
                case EmotePage.P2:
                    {
                        for (int x = 13; x <= 24; x++)
                        {
                            AddButtonLabeled(10, y, GetButtonID(1, x), Emote.Sounds[x]);
                            y += 25;
                        }
                        AddButton(10, 380, 4506, 4508, GetButtonID(0, 1), GumpButtonType.Reply, 0);
                        AddButton(70, 380, 4502, 0504, GetButtonID(0, 3), GumpButtonType.Reply, 0);
                        break;
                    }
                case EmotePage.P3:
                    {
                        for (int x = 25; x <= 36; x++)
                        {
                            AddButtonLabeled(10, y, GetButtonID(1, x), Emote.Sounds[x]);
                            y += 25;
                        }
                        AddButton(10, 380, 4506, 4508, GetButtonID(0, 2), GumpButtonType.Reply, 0);
                        AddButton(70, 380, 4502, 0504, GetButtonID(0, 4), GumpButtonType.Reply, 0);
                        break;
                    }
                case EmotePage.P4:
                    {
                        for (int x = 37; x <= 48; x++)
                        {
                            AddButtonLabeled(10, y, GetButtonID(1, x), Emote.Sounds[x]);
                            y += 25;
                        }
                        AddButton(10, 380, 4506, 4508, GetButtonID(0, 3), GumpButtonType.Reply, 0);
                        break;
                    }
            }
        }
        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            int val = info.ButtonID - 1;
            if (val < 0)
                return;

            Mobile from = m_From;
            int type = val % 15;
            int index = val / 15;

            switch (type)
            {
                case 0:
                    {
                        EmotePage page;
                        switch (index)
                        {
                            case 1: page = EmotePage.P1; break;
                            case 2: page = EmotePage.P2; break;
                            case 3: page = EmotePage.P3; break;
                            case 4: page = EmotePage.P4; break;
                            default: return;
                        }

                        from.SendGump(new EmoteGump(from, page));
                        break;
                    }
                case 1:
                    {
                        if (index >= 1 && index <= 12)
                        {
                            from.SendGump(new EmoteGump(from, EmotePage.P1));
                        }
                        else if (index >= 13 && index <= 24)
                        {
                            from.SendGump(new EmoteGump(from, EmotePage.P2));
                        }
                        else if (index >= 25 && index <= 36)
                        {
                            from.SendGump(new EmoteGump(from, EmotePage.P3));
                        }
                        else if (index >= 37 && index <= 48)
                        {
                            from.SendGump(new EmoteGump(from, EmotePage.P4));
                        }
                        new ESound(from, index);
                        break;
                    }
            }
        }
    }

    public class ItemRemovalTimer : Timer
    {
        private Item i_item;
        public ItemRemovalTimer(Item item)
            : base(TimeSpan.FromSeconds(10.0))
        {
            Priority = TimerPriority.OneSecond;
            i_item = item;
        }

        protected override void OnTick()
        {
            if ((i_item != null) && (!i_item.Deleted))
                i_item.Delete();
        }
    }

    public class Puke : Item
    {
        [Constructable]
        public Puke()
            : base(Utility.RandomList(0xf3b, 0xf3c))
        {
            Name = "A Pile of Puke";
            Hue = 0x557;
            Movable = false;

            ItemRemovalTimer thisTimer = new ItemRemovalTimer(this);
            thisTimer.Start();

        }

        public override void OnSingleClick(Mobile from)
        {
            this.LabelTo(from, this.Name);
        }

        public Puke(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            this.Delete(); // none when the world starts 
        }
    }

    public class ESound
    {
        private int[] FemSound = new int[] { 0, 778, 779, 780, 781, 0, 786, 782, 784, 785, 787, 791,
											792, 793, 794, 795, 796, 797, 798, 799, 800, 801,
											802, 803, 811, 812, 813, 0, 814, 815, 816, 0, 817, 818,
											819, 820, 0, 0, 821, 783, 822, 823, 824, 788, 789, 790,
                                            804, 807, 808
        };

        private int[] MaleSound = new int[] { 0, 1049, 1050, 1051, 1052, 0, 1057, 1053, 1055, 1056, 1058,
											1063, 1064, 1065, 1066, 1067, 1068, 1069, 1070, 1071, 1072,
											1073, 1074, 1075, 1085, 1086, 1087, 0, 1088, 1089, 1090, 0, 1091,
											1092, 1093, 1094, 0, 0, 1095, 1054, 1096, 1097, 1098, 1059, 1060, 1061,
                                            1076, 1079, 1080
        };

        private string[] Words = new string[] { "","*ah!*","*ah ha!*","*applauds*","*blows nose*","*bows*","*bs cough*","*burp!*",
									"*clears throat*","*cough!*","*cries*","*faints*","*farts*","*gasp!*","*giggles*",
									"*groans*","*growls*","*hey!*","*hiccup!*","*huh?*","*kisses*","*laughs*","*no!*",
									"*oh!*","*oooh*","*oops*","*pukes*","*punches*","*ahhhh!*","*shhh!*","*sigh*",
									"*slaps*","*ahh-choo!*","*sniff*","*snore*","*spits*","*sticks out tongue*",
									"*taps foot*","*wistles*","*woohoo!*","*yawns*","*yeah!*","*yeow!*", "*aurgh!*", "*urgh!*",
                                    "*auugh*", "*uhn*", "*urrha*", "*uaah!*"
        };

        public ESound(Mobile pm, int SoundMade)
        {
            switch (SoundMade)
            {
                case 4: if (!pm.Mounted) pm.Animate(34, 5, 1, true, false, 0); break;
                case 7:
                case 8:
                case 9: if (!pm.Mounted) pm.Animate(33, 5, 1, true, false, 0); break;
                case 11: if (!pm.Mounted) pm.Animate(21, 5, 1, true, false, 0); break;
                case 26: if (!pm.Mounted) pm.Animate(32, 5, 1, true, false, 0); MakePuke(pm); break;
                case 27: if (!pm.Mounted) pm.Animate(31, 5, 1, true, false, 0); break;
                case 31: if (!pm.Mounted) pm.Animate(11, 5, 1, true, false, 0); break;
                case 5:
                case 32: if (!pm.Mounted) pm.Animate(32, 5, 1, true, false, 0); break;
                case 33: if (!pm.Mounted) pm.Animate(34, 5, 1, true, false, 0); break;
                case 35: if (!pm.Mounted) pm.Animate(6, 5, 1, true, false, 0); break;
                case 37: if (!pm.Mounted) pm.Animate(38, 5, 1, true, false, 0); break;
                case 38: if (!pm.Mounted) pm.Animate(5, 5, 1, true, false, 0); break;
                case 40: if (!pm.Mounted) pm.Animate(17, 5, 1, true, false, 0); break;
                case 43: if (!pm.Mounted) pm.Animate(22, 5, 1, true, false, 0); break;
                case 44: if (!pm.Mounted) pm.Animate(21, 5, 1, true, false, 0); break;
                case 45: if (!pm.Mounted) pm.Animate(22, 5, 1, true, false, 0); break;
                default: break;
            }
            int sound = pm.Female ? FemSound[SoundMade] : MaleSound[SoundMade];
            if (sound > 0) pm.PlaySound(sound);
            pm.Say(Words[SoundMade]);
        }

        private void MakePuke(Mobile pm)
        {
            Point3D p = new Point3D(pm.Location);
            switch (pm.Direction)
            {
                case Direction.North: p.Y--; break;
                case Direction.South: p.Y++; break;
                case Direction.East: p.X++; break;
                case Direction.West: p.X--; break;
                case Direction.Right: p.X++; p.Y--; break;
                case Direction.Down: p.X++; p.Y++; break;
                case Direction.Left: p.X--; p.Y++; break;
                case Direction.Up: p.X--; p.Y--; break;
                default: break;
            }
            p.Z = pm.Map.GetAverageZ(p.X, p.Y);

            Puke puke = new Puke();
            puke.Map = pm.Map;
            puke.Location = p;
        }
    }
}