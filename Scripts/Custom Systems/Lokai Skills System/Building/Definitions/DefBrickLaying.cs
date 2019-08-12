/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Build
{
    public class DefBrickLaying : BuildSystem
    {
        public override LokaiSkillName MainLokaiSkill
        {
            get { return LokaiSkillName.BrickLaying; }
        }

        public override string GumpTitleString
        {
            get { return "Brick Laying"; }
        }

        public override void PlayBuildEffect(Mobile from) { }
        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken,
            int quality, bool makersMark, BuildItem item) { return 0; }

        public override int CanBuild(Mobile from, BaseBuildingTool tool, Type itemType) { return 0; }

        private static BuildSystem m_BuildSystem;

        public static BuildSystem BuildSystem
        {
            get
            {
                if (m_BuildSystem == null)
                    m_BuildSystem = new DefBrickLaying();

                return m_BuildSystem;
            }
        }

        public override double GetChanceAtMin(BuildItem item)
        {
            return 0.0; // 0% 
        }

        private DefBrickLaying()
            : base(1, 1, 1.25)// base( 1, 2, 1.7 ) 
        {
        }

        public override void InitBuildList()
        {
            int index = -1;

            // Panels
            index = AddBuild(typeof(BrickPanel), "Panels", "Brick Panel Pequeño Sur", 10.0, 50.0, typeof(BrickPiece), "Brick Pieces", 3, "You don't have enough brick pieces.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BrickPanelEstPequeño), "Panels", "Brick Panel Pequeño Este", 10.0, 50.0, typeof(BrickPiece), "Brick Pieces", 3, "You don't have enough brick pieces.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BrickPanelEsquinaPequeña), "Panels", "Brick Panel Pequeño Esquina", 10.0, 50.0, typeof(BrickPiece), "Brick Pieces", 3, "You don't have enough brick pieces.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BrickPanelSurGrande), "Panels", "Brick Panel Grande Sur", 10.0, 50.0, typeof(BrickPiece), "Brick Pieces", 3, "You don't have enough brick pieces.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BrickPanelEstGrande), "Panels", "Brick Panel Grande Este", 10.0, 50.0, typeof(BrickPiece), "Brick Pieces", 3, "You don't have enough brick pieces.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BrickPanelEsquinaGrande), "Panels", "Brick Panel Grande Esquina", 10.0, 50.0, typeof(BrickPiece), "Brick Pieces", 3, "You don't have enough brick pieces.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);


            // Flooring
            index = AddBuild(typeof(BrickFlooring), "Flooring", "Brick Flooring", 15.0, 60.0, typeof(BrickPiece), "Brick Pieces", 2, "You don't have enough brick pieces.");
            AddRes(index, typeof(CementSupply), "Cement", 15);

            // Walls
            index = AddBuild(typeof(BrickWall), "Walls", "Brick Wall Sur", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BrickWallEst), "Walls", "Brick Wall Este", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BrickWallEsquina), "Walls", "Brick Wall Esquina", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BrickWallArcoEsquina), "Walls", "Brick Wall Arco Esquina", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BrickWallArcoSur1), "Walls", "Brick Wall Arco Sur1", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BrickWallArcoSur2), "Walls", "Brick Wall Arco Sur 2", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BrickWallArcoEst1), "Walls", "Brick Wall  Arco Este 1", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BrickWallArcoEst2), "Walls", "Brick Wall Arco Este 2", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);

            // Windows
            index = AddBuild(typeof(BrickWindow), "Windows", "Brick Window Sur", 30.0, 70.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
             index = AddBuild(typeof(BrickWindowEst), "Windows", "Brick Window Este", 30.0, 70.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);

            //STAIR
            index = AddBuild(typeof(BrickStair), "Stair", "Brick Stair Sur", 10.0, 50.0, typeof(BrickPiece), "Brick Pieces", 3, "You don't have enough brick pieces.");
            AddRes(index, typeof(CementSupply), "Cement", 12);
            index = AddBuild(typeof(BrickStairE), "Stair", "Brick Stair Este", 10.0, 50.0, typeof(BrickPiece), "Brick Pieces", 3, "You don't have enough brick pieces.");
            AddRes(index, typeof(CementSupply), "Cement", 12);
            index = AddBuild(typeof(BrickStairN), "Stair", "Brick Stair Norte", 10.0, 50.0, typeof(BrickPiece), "Brick Pieces", 3, "You don't have enough brick pieces.");
            AddRes(index, typeof(CementSupply), "Cement", 12);
            index = AddBuild(typeof(BrickStairW), "Stair", "Brick Stair Oeste", 10.0, 50.0, typeof(BrickPiece), "Brick Pieces", 3, "You don't have enough brick pieces.");
            AddRes(index, typeof(CementSupply), "Cement", 12);

            // Foundations
            index = AddBuild(typeof(BrickFoundation), "Foundations", "Brick Foundation", 10.0, 50.0, typeof(BrickPiece), "Brick Pieces", 3, "You don't have enough brick pieces.");
            AddRes(index, typeof(CementSupply), "Cement", 12);

            MarkOption = false;
            Repair = false;

            SetSubRes(typeof(BrickPiece), 1044525);
        }
    }
}