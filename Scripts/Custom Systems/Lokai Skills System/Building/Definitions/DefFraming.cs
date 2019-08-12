/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Build
{
    public class DefFraming : BuildSystem
    {
        public override LokaiSkillName MainLokaiSkill
        {
            get { return LokaiSkillName.Framing; }
        }

        public override string  GumpTitleString
        {
            get { return "Framing"; }
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
                    m_BuildSystem = new DefFraming();

                return m_BuildSystem;
            }
        }

        public override double GetChanceAtMin(BuildItem item)
        {
            return 0.0; // 0% 
        }

        private DefFraming()
            : base(1, 1, 1.25)// base( 1, 2, 1.7 ) 
        {
        }

        public override void InitBuildList()
        {
            int index = -1;

            // Panels
            index = AddBuild(typeof(WoodPanel), "Panels", "Wood Panel1 Sur", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
               index = AddBuild(typeof(WoodPanel1Est), "Panels", "Wood Panel1 Este", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
               index = AddBuild(typeof(WoodPanel1EsquinaGrande), "Panels", "Wood Panel1 Esquina", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
               index = AddBuild(typeof(WoodPanel1EsquinaPeque), "Panels", "Wood Panel Esquina Pequeña", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
               index = AddBuild(typeof(WoodPanel1EstPeque), "Panels", "Wood Panel1 Este Peque", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
               index = AddBuild(typeof(WoodPanel1SuPeque), "Panels", "Wood Panel1 Sur Peque", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
				///woodpanel2
             index = AddBuild(typeof(WoodPanel2Sur), "Panels", "Wood Panel2 Sur", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel2Est), "Panels", "Wood Panel2 Este", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel2EsquinaPeque), "Panels", "Wood Panel2 Esquina Pequeña", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel2EstPeque), "Panels", "Wood Panel2 Este Peque", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel2SuPeque), "Panels", "Wood Panel2 Sur Peque", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            	//WoodPanel3
             index = AddBuild(typeof(WoodPanel3PinchosFueraEste), "Panels",  "Wood Panel3 Pinchos Fuera Este", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel3PinchosFueraSur), "Panels", "Wood Panel3 Pinchos Fuera Sur", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel3PinchosDentroEst), "Panels", "Wood Panel3 Pinchos Dentro Este", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel3PinchosDentroSur), "Panels", "Wood Panel3 Pinchos Dentro Sur", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel3EstPeque), "Panels", "Wood Panel3 Este Peque", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel3SuPeque), "Panels", "Wood Panel3 Sur Peque", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             //wood panel4
             index = AddBuild(typeof(WoodPanel4Sur), "Panels", "Wood Panel4 Sur", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel4Est), "Panels", "Wood Panel4 Este", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel4EsquinaGrande), "Panels", "Wood Panel4 Esquina Grande", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel4EsquinaPeque), "Panels", "Wood Panel4 Esquina Pequeña", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel4EstPeque), "Panels", "Wood Panel4 Este Peque", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel4SuPeque), "Panels", "Wood Panel4 Sur Peque", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            //wood panel 5
            index = AddBuild(typeof(WoodPanel5Sur), "Panels", "Wood Panel5 Sur", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel5Est), "Panels", "Wood Panel5 Este", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel5EsquinaGrande), "Panels", "Wood Panel5 Esquina Grande", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel5EsquinaPeque), "Panels", "Wood Panel5 Esquina Pequeña", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel5EstPeque), "Panels", "Wood Panel5 Este Peque", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodPanel5SuPeque), "Panels", "Wood Panel5 Sur Peque", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);


//log
            index = AddBuild(typeof(LogPanel), "Panels", "Log Panel Sur", 10.0, 100.0, typeof(LogPiece), "Log Pieces", 3, "You don't have enough log pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(LogPanelEst), "Panels", "Log Panel Este", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(LogPanelEsquina), "Panels", "Log Panel Esquina", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(LogPanelTocon), "Panels", "Log Panel Tocon", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(LogPanelSurTocon), "Panels", "Log Panel Sur Tocon", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(LogPanelEstTocon), "Panels", "Log Panel Este Tocon", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(LogPanelSurPeque), "Panels", "Log Panel Sur Pequeño", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(LogPanelEstPeque), "Panels", "Log Panel Este Pequeño", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);

 //ratan

            index = AddBuild(typeof(RatanPanel), "Panels", "Ratan Panel Sur", 10.0, 100.0, typeof(RatanPiece), "Ratan Pieces", 3, "You don't have enough ratan pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(RatanPanelEst), "Panels", "Ratan Panel Este", 10.0, 100.0, typeof(RatanPiece), "Ratan Pieces", 3, "You don't have enough ratan pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(RatanPanelEstPeque), "Panels", "Ratan Panel Este Pequeña", 10.0, 100.0, typeof(RatanPiece), "Ratan Pieces", 3, "You don't have enough ratan pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(RatanPanelSurPeque), "Panels", "Ratan Panel Sur Pequeña", 10.0, 100.0, typeof(RatanPiece), "Ratan Pieces", 3, "You don't have enough ratan pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(RatanPanelEsquina), "Panels", "Ratan Panel Esquina", 10.0, 100.0, typeof(RatanPiece), "Ratan Pieces", 3, "You don't have enough ratan pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);


//Hide
             index = AddBuild(typeof(HidePanel), "Panels", "Hide Panel Sur", 10.0, 100.0, typeof(HidePiece), "Hide Pieces", 3, "You don't have enough hide pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(HidePanelEst), "Panels", "Hide Panel Est", 10.0, 100.0, typeof(HidePiece), "Hide Pieces", 3, "You don't have enough hide pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(HidePanelSurPeque), "Panels", "Hide Panel Sur Pequeña", 10.0, 100.0, typeof(HidePiece), "Hide Pieces", 3, "You don't have enough hide pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(HidePanelEstPeque), "Panels", "Hide Panel Este Pequeña", 10.0, 100.0, typeof(HidePiece), "Hide Pieces", 3, "You don't have enough hide pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);

 //bamboo

            index = AddBuild(typeof(BambooPanel), "Panels", "Bamboo Panel 1 ", 10.0, 100.0, typeof(BambooPiece), "Bamboo Pieces", 3, "You don't have enough bamboo pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BambooPanel2), "Panels", "Bamboo Panel 2", 10.0, 100.0, typeof(BambooPiece), "Bamboo Pieces", 3, "You don't have enough bamboo pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BambooPanel3), "Panels", "Bamboo Panel 3", 10.0, 100.0, typeof(BambooPiece), "Bamboo Pieces", 3, "You don't have enough bamboo pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BambooPanel4), "Panels", "Bamboo Panel 4", 10.0, 100.0, typeof(BambooPiece), "Bamboo Pieces", 3, "You don't have enough bamboo pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BambooPanel5), "Panels", "Bamboo Panel 5 ", 10.0, 100.0, typeof(BambooPiece), "Bamboo Pieces", 3, "You don't have enough bamboo pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BambooPanel6), "Panels", "Bamboo Panel 6", 10.0, 100.0, typeof(BambooPiece), "Bamboo Pieces", 3, "You don't have enough bamboo pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BambooPanel7), "Panels", "Bamboo Panel 7", 10.0, 100.0, typeof(BambooPiece), "Bamboo Pieces", 3, "You don't have enough bamboo pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BambooPanel8), "Panels", "Bamboo Panel 8", 10.0, 100.0, typeof(BambooPiece), "Bamboo Pieces", 3, "You don't have enough bamboo pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BambooPanel9), "Panels", "Bamboo Panel 9", 10.0, 100.0, typeof(BambooPiece), "Bamboo Pieces", 3, "You don't have enough bamboo pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BambooPanel10), "Panels", "Bamboo Panel 10", 10.0, 100.0, typeof(BambooPiece), "Bamboo Pieces", 3, "You don't have enough bamboo pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BambooPanel11), "Panels", "Bamboo Panel 11", 10.0, 100.0, typeof(BambooPiece), "Bamboo Pieces", 3, "You don't have enough bamboo pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BambooPanel12), "Panels", "Bamboo Panel 12", 10.0, 100.0, typeof(BambooPiece), "Bamboo Pieces", 3, "You don't have enough bamboo pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);




/////////////////////////////////////////////////////////

            // Flooring
            index = AddBuild(typeof(WoodFlooring), "Flooring", "Wood Flooring", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 2, "You don't have enough wood pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);

//////////////////////////////////////////////////////
            // Walls
            //woodwall1
            index = AddBuild(typeof(WoodWall), "Walls", "Wood Wall Sur", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWallEst), "Walls", "Wood Wall Este", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWallEst1), "Walls", "Wood Wall Este1", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWallEst2), "Walls", "Wood Wall Este2", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWallSur1), "Walls", "Wood Wall Sur1", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWallSur2), "Walls", "Wood Wall Sur2", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodWallEsquina), "Walls", "Wood Wall Esquina", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            //woodwall2
            index = AddBuild(typeof(WoodWall2Sur), "Walls", "Wood Wall2 Sur", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWall2Sur2), "Walls", "Wood Wall2 Sur2", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWall2Est), "Walls", "Wood Wall2 Este", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWall2Est2), "Walls", "Wood Wall2 Este2", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWall2Esquina), "Walls", "Wood Wall2 Esquina", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            //woodwall3
            index = AddBuild(typeof(WoodWall3Sur), "Walls", "Wood Wall3 Sur", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWall3Sur2), "Walls", "Wood Wall3 Sur2", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWall3Est), "Walls", "Wood Wall3 Este", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWall3Est2), "Walls", "Wood Wall3 Este2", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWall3Esquina), "Walls", "Wood Wall3 Esquina ", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(WoodWall3Esquina2), "Walls", "Wood Wall3 Esquina2", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
//woodwall4
            index = AddBuild(typeof(WoodWall4Sur), "Walls", "Wood Wall4 Sur", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWall4Esquina), "Walls", "Wood Wall4 Esquina ", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWall4Est), "Walls", "Wood Wall4 Este", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);


//woodwall5
                index = AddBuild(typeof(WoodWall5Sur), "Walls", "Wood Wall5 Sur", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWall5Esquina), "Walls", "Wood Wall5 Esquina ", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWall5Est), "Walls", "Wood Wall5 Este", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);


//logwall

            index = AddBuild(typeof(LogWall), "Walls", "Log Wall", 10.0, 100.0, typeof(LogPanel), "Log Panels", 2, "You don't have enough log panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);


//RatanWall
            index = AddBuild(typeof(RatanWall), "Walls", "Ratan Wall Sur", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(RatanWallSur2), "Walls", "Ratan Wall Sur2", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(RatanWallEst), "Walls", "Ratan Wall Este", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(RatanWallEst2), "Walls", "Ratan Wall Este2", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(RatanWallEsquina), "Walls",  "Ratan Wall Esquina", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);

//HideWall
            index = AddBuild(typeof(HideWall), "Walls", "Hide Wall Sur" , 10.0, 100.0, typeof(HidePanel), "Hide Panels", 2, "You don't have enough hide panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(HideWallEst), "Walls", "Hide Wall Este", 10.0, 100.0, typeof(HidePanel), "Hide Panels", 2, "You don't have enough hide panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(HideWallEsquina), "Walls", "Hide Wall Esquina", 10.0, 100.0, typeof(HidePanel), "Hide Panels", 2, "You don't have enough hide panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);


//BambooWall
            index = AddBuild(typeof(BambooWall), "Walls", "Bamboo Wall", 10.0, 100.0, typeof(BambooPanel), "Bamboo Panels", 2, "You don't have enough bamboo panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);

////////////////////////////////////////////////////////////////

            // Windows
//wood1
            index = AddBuild(typeof(WoodWindow), "Windows", "Wood Window Sur", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWindowEst), "Windows", "Wood Window Este", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
//wood2
            index = AddBuild(typeof(WoodWindow2), "Windows", "Wood Window2 Sur", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWindow2Est), "Windows", "Wood Window2 Este", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);

//wood4

            index = AddBuild(typeof(WoodWindow4), "Windows", "Wood Window4 Sur", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WoodWindow4Est), "Windows", "Wood Window4 Este", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);

//Log
            index = AddBuild(typeof(LogWindow), "Windows", "Log Window Sur", 10.0, 100.0, typeof(LogPanel), "Log Panels", 2, "You don't have enough log panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(LogWindowEst), "Windows", "Log Window Este", 10.0, 100.0, typeof(LogPanel), "Log Panels", 2, "You don't have enough log panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);


//Ratan
            index = AddBuild(typeof(RatanWindow), "Windows", "Ratan Window Sur", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(RatanWindowEst), "Windows", "Ratan Window Este", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);

//Hide
            index = AddBuild(typeof(HideWindow), "Windows", "Hide Window Sur", 10.0, 100.0, typeof(HidePanel), "Hide Panels", 2, "You don't have enough hide panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(HideWindowEst), "Windows", "Hide Window Este", 10.0, 100.0, typeof(HidePanel), "Hide Panels", 2, "You don't have enough hide panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);

//Bamboo
            index = AddBuild(typeof(BambooWindow), "Windows", "Bamboo Window Sur", 10.0, 100.0, typeof(BambooPanel), "Bamboo Panels", 2, "You don't have enough bamboo panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(BambooWindowEst), "Windows", "Bamboo Window Este", 10.0, 100.0, typeof(BambooPanel), "Bamboo Panels", 2, "You don't have enough bamboo panels.");
            AddRes(index, typeof(NailSupply), "Nails", 30);
            AddRes(index, typeof(JointSupply), "Joints", 4);





////////////////////////////////////////////////////////////////
// wood doors
            index = AddBuild(typeof(WoodDoor), "Doors", "Wood Door Est1", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
             index = AddBuild(typeof(WoodDoorEst2), "Doors", "Wood Door Est2", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
             index = AddBuild(typeof(WoodDoorSur1), "Doors", "Wood Door Sur1", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
             index = AddBuild(typeof(WoodDoorSur2), "Doors", "Wood Door Sur2", 10.0, 100.0, typeof(WoodPanel), "Wood Panels", 2, "You don't have enough wood panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);



//doors
            index = AddBuild(typeof(LogDoor), "Doors", "Log Door", 10.0, 100.0, typeof(LogPanel), "Log Panels", 2, "You don't have enough log panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);

//fence
             index = AddBuild(typeof(FenceDoorEst1), "Doors", "Fence Door Est1", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(FenceDoorEst2), "Doors", "Fence Door Est2", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(FenceDoorSur1), "Doors", "Fence Door Sur1", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
             index = AddBuild(typeof(FenceDoorSur2), "Doors", "Fence Door Sur2", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);

          

//ratan

            index = AddBuild(typeof(RatanDoor), "Doors", "Ratan Door Sur1", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
             index = AddBuild(typeof(RatanDoorSur2), "Doors", "Ratan Door Sur2", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
             index = AddBuild(typeof(RatanDoorEst1), "Doors", "Ratan Door Este1", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
             index = AddBuild(typeof(RatanDoorEst2), "Doors", "Ratan Door Este2", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            
//Hide
            index = AddBuild(typeof(HideDoor), "Doors", "Hide Door", 10.0, 100.0, typeof(HidePanel), "Hide Panels", 2, "You don't have enough hide panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
//bamboo
            index = AddBuild(typeof(BambooDoor), "Doors", "Bamboo Door", 10.0, 100.0, typeof(BambooPanel), "Bamboo Panels", 2, "You don't have enough bamboo panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
///////////////////////////////////////////////////////////////////////////////////
            // Stairs
            index = AddBuild(typeof(WoodStair), "Stairs", "Wood Stair Sur", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 10, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 25);
            index = AddBuild(typeof(WoodStairE), "Stairs", "Wood Stair Este", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 10, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 25);
            index = AddBuild(typeof(WoodStairN), "Stairs", "Wood Stair Norte", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 10, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 25);
            index = AddBuild(typeof(WoodStairW), "Stairs", "Wood Stair Oeste", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 10, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 25);


/////////////////////////////////////////////////////////////////////////
            // Foundations
            index = AddBuild(typeof(WoodFoundation), "Foundations", "Wood Foundation", 10.0, 100.0, typeof(WoodPiece), "Wood Pieces", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(NailSupply), "Nails", 25);

            MarkOption = false;
            Repair = false;

            SetSubRes(typeof(WoodPiece), 1044525);
        }
    }
}