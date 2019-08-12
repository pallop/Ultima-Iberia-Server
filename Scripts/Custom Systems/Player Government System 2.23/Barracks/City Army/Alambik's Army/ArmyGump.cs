using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Targets;

namespace Server.Gumps
{
    public class ArmyGump : Gump
    {
        private ArmyController m_Controller;

        public ArmyGump(ArmyController controller)
            : base(0, 0)
        {
            m_Controller = controller;
            int war = (controller.War) ? 1 : 0;
            int free = (controller.Free) ? 0 : 1;
            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;
            AddPage(0);
            AddBackground(5, 221, 158, 144, 9200);
            AddAlphaRegion(8, 224, 150, 137);
            AddPage(1);
            Direction d;
            if (controller.CurrentFormation == ArmyFormationEnum.Triangle)
                d = ((Direction)((7 + (8 - (int)(controller.ArmyFormationDirection))) % 8));
            else
                d = ((Direction)(((8 - (int)(controller.ArmyFormationDirection))) % 8));
            AddButton(47, 269, 11400 + ((d != Direction.Up) ? 11 : 0), 11412, (int)Buttons.UP, GumpButtonType.Reply, 0);
            AddButton(48, 338, 11400 + ((d != Direction.Down) ? 11 : 0), 11412, (int)Buttons.DOWN, GumpButtonType.Reply, 0);
            AddButton(15, 303, 11400 + ((d != Direction.Left) ? 11 : 0), 11412, (int)Buttons.LEFT, GumpButtonType.Reply, 0);
            AddButton(79, 303, 11400 + ((d != Direction.Right) ? 11 : 0), 11412, (int)Buttons.RIGHT, GumpButtonType.Reply, 0);
            AddButton(24, 278, 11400 + ((d != Direction.West) ? 11 : 0), 11412, (int)Buttons.WEST, GumpButtonType.Reply, 0);
            AddButton(70, 278, 11400 + ((d != Direction.North) ? 11 : 0), 11412, (int)Buttons.NORTH, GumpButtonType.Reply, 0);
            AddButton(25, 327, 11400 + ((d != Direction.South) ? 11 : 0), 11412, (int)Buttons.SOUTH, GumpButtonType.Reply, 0);
            AddButton(71, 327, 11400 + ((d != Direction.East) ? 11 : 0), 11412, (int)Buttons.EAST, GumpButtonType.Reply, 0);
            AddImageTiledButton(127, 229, 4020, 4014/*4021,4022*/, (int)Buttons.HELP, GumpButtonType.Reply, 0, 4020, 0, 10, 10, 0/*3006118*/);
            AddImageTiledButton(97, 229, 4017, 4018, (int)Buttons.ATTACK, GumpButtonType.Reply, 2, 4017, 0, 10, 10, 1011533);
            AddImageTiledButton(67, 230, 4026 + war, 4028, (int)Buttons.WAR_PEACE, GumpButtonType.Reply, 0, 4026 + war, 0, 10, 10, 1077813);
            if (free == 0) AddImageTiledButton(37, 230, 4008, 4010, (int)Buttons.FREE_FORMATION, GumpButtonType.Reply, 0, 4008, 0, 10, 10, 1063119);
            else AddImageTiledButton(37, 230, 4009, 4010, (int)Buttons.FREE_FORMATION, GumpButtonType.Reply, 0, 4009, 0, 10, 10, 1072367);
            AddImageTiledButton(7, 230, 4014, 4016, (int)Buttons.GOTO, GumpButtonType.Reply, 0, 4014, 0, 10, 10, 3005134);
            AddImageTiledButton(48, 304, 10741, 10742, (int)Buttons.SAY, GumpButtonType.Reply, 10741, 0, 0, 10, 10, 3002076);
            AddImageTiledButton(5, 349, 11400, 11400, (int)Buttons.SLEEP, GumpButtonType.Page, 2, 11400, 0, 10, 10, 0);
            AddLabel(20, 349, 0, @"Wake/Sleep");
            AddButton(102, 259, 11357, 11357, (int)Buttons.CARRE_PLEIN, GumpButtonType.Reply, 0);
            AddButton(128, 259, 11354, 11354, (int)Buttons.CARRE_VIDE, GumpButtonType.Reply, 0);
            AddButton(102, 294, 11352, 11352, (int)Buttons.LIGNE, GumpButtonType.Reply, 0);
            AddButton(128, 294, 11358, 11358, (int)Buttons.TRIANGLE, GumpButtonType.Reply, 0);
            AddButton(102, 329, 11350, 11350, (int)Buttons.OTHER, GumpButtonType.Reply, 0);
            AddButton(128, 329, 11356, 11356, (int)Buttons.BORDEL, GumpButtonType.Reply, 0);
            AddPage(2);
            AddLabel(14, 265, 0, @"Wake(Cancel) Sleep(OK)");
            AddButton(14, 296, 241, 242, (int)Buttons.CANCEL, GumpButtonType.Reply, 0);
            AddButton(81, 296, 247, 248, (int)Buttons.OK, GumpButtonType.Reply, 0);
        }

        public enum Buttons
        {
            NONE,
            SLEEP,
            ATTACK,
            UP,
            DOWN,
            LEFT,
            RIGHT,
            WEST,
            NORTH,
            SOUTH,
            EAST,
            WAR_PEACE,
            FREE_FORMATION,
            GOTO,
            SAY,
            HELP,
            WAKE,
            CARRE_PLEIN,
            CARRE_VIDE,
            LIGNE,
            TRIANGLE,
            OTHER,
            BORDEL,
            CANCEL,
            OK
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            if (from.AccessLevel < AccessLevel.GameMaster)
                if (from.Deleted || !from.Alive)
                    return;

            if (m_Controller == null)
                return;
            if (m_Controller.Deleted)
                return;

            switch (info.ButtonID)
            {
                case (int)Buttons.UP:
                    m_Controller.SetDirection(Direction.Up);
                    break;
                case (int)Buttons.DOWN:
                    m_Controller.SetDirection(Direction.Down);
                    break;
                case (int)Buttons.LEFT:
                    m_Controller.SetDirection(Direction.Left);
                    break;
                case (int)Buttons.RIGHT:
                    m_Controller.SetDirection(Direction.Right);
                    break;
                case (int)Buttons.WEST:
                    m_Controller.SetDirection(Direction.West);
                    break;
                case (int)Buttons.NORTH:
                    m_Controller.SetDirection(Direction.North);
                    break;
                case (int)Buttons.SOUTH:
                    m_Controller.SetDirection(Direction.South);
                    break;
                case (int)Buttons.EAST:
                    m_Controller.SetDirection(Direction.East);
                    break;
                case (int)Buttons.WAR_PEACE:
                    if (m_Controller.War)
                        m_Controller.SetPeace();
                    else
                        m_Controller.SetWar();
                    break;
                case (int)Buttons.FREE_FORMATION:
                    if (m_Controller.Free)
                        m_Controller.SetFormation(ArmyFormationEnum.Latest);
                    else
                        m_Controller.SetFree();
                    break;
                case (int)Buttons.GOTO:
                    m_Controller.TargetGoTo(from);
                    break;
                case (int)Buttons.SAY:
                    m_Controller.PromptSay(from);
                    break;
                case (int)Buttons.HELP:
                    from.SendGump(new ArmyHelpGump());
                    break;
                case (int)Buttons.ATTACK:
                    m_Controller.TargetAttack(from);
                    break;
                case (int)Buttons.CARRE_PLEIN:
                    m_Controller.SetFormation(ArmyFormationEnum.FullSquare);
                    break;
                case (int)Buttons.CARRE_VIDE:
                    m_Controller.SetFormation(ArmyFormationEnum.EmptySquare);
                    break;
                case (int)Buttons.LIGNE:
                    m_Controller.SetFormation(ArmyFormationEnum.Line);
                    break;
                case (int)Buttons.TRIANGLE:
                    m_Controller.SetFormation(ArmyFormationEnum.Triangle);
                    break;
                case (int)Buttons.OTHER:
                    m_Controller.SetFormation(ArmyFormationEnum.Other);
                    break;
                case (int)Buttons.BORDEL:
                    m_Controller.SetFormation(ArmyFormationEnum.Bordel);
                    break;
                case (int)Buttons.OK:
                    m_Controller.Sleeping();
                    break;
                case (int)Buttons.CANCEL:
                    m_Controller.Wakeup();
                    break;
                    //return; // CLOSE
                default:
                    return; //CLOSE
            }
            from.SendGump(new ArmyGump(m_Controller));
        }
    }
}
