/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Build
{
    public class DefRoofing : BuildSystem
    {
        public override LokaiSkillName MainLokaiSkill
        {
            get { return LokaiSkillName.Roofing; }
        }

        public override string GumpTitleString
        {
            get { return "Roofing"; }
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
                    m_BuildSystem = new DefRoofing();

                return m_BuildSystem;
            }
        }

        public override double GetChanceAtMin(BuildItem item)
        {
            return 0.0; // 0% 
        }

        private DefRoofing()
            : base(1, 1, 1.25)// base( 1, 2, 1.7 ) 
        {
        }

        public override void InitBuildList()
        {
            int index = -1;

            /*
            // Base Pieces
            index = AddBuild(typeof(BrickPiece), "Base Pieces", "Brick Piece", 0.0, 40.0, typeof(Clay), "Clay", 5, "You don't have enough clay.");

            // Base Pieces
            index = AddBuild(typeof(StonePiece), "Base Pieces", "Stone Piece", 0.0, 40.0, typeof(BaseGranite), "Granite", 5, "You don't have enough granite.");
            index = AddBuild(typeof(SandstonePiece), "Base Pieces", "Sandstone Piece", 10.0, 50.0, typeof(BaseGranite), "Granite", 5, "You don't have enough granite.");
            index = AddBuild(typeof(MarblePiece), "Base Pieces", "Marble Piece", 20.0, 60.0, typeof(BaseGranite), "Granite", 5, "You don't have enough granite.");

            // Base Pieces
            index = AddBuild(typeof(TilePiece), "Base Pieces", "Tile Piece", 20.0, 60.0, typeof(Clay), "Granite", 5, "You don't have enough granite.");
            index = AddBuild(typeof(LogPiece), "Base Pieces", "Log Piece", 10.0, 50.0, typeof(Log), "Granite", 5, "You don't have enough granite.");
            index = AddBuild(typeof(WoodPiece), "Base Pieces", "Wood Piece", 15.0, 55.0, typeof(Board), "Granite", 5, "You don't have enough granite.");
            index = AddBuild(typeof(ThatchPiece), "Base Pieces", "Thatch Piece", 0.0, 40.0, typeof(BaseGranite), "Granite", 5, "You don't have enough granite.");
            index = AddBuild(typeof(PalmPiece), "Base Pieces", "Palm Piece", 5.0, 45.0, typeof(TreeResourceItem), "Granite", 5, "You don't have enough granite.", TreeResource.PalmHusks);
            index = AddBuild(typeof(SlatePiece), "Base Pieces", "Slate Piece", 30.0, 70.0, typeof(Clay), "Granite", 5, "You don't have enough granite.");
            */

            // Roofing
//Tile
            index = AddBuild(typeof(TileRoofing), "Roofing", "Tile Roofing", 40.0, 85.0, typeof(TilePiece), "Tile Piece", 3, "You don't have enough tile pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(TileRoofing2), "Roofing", "Tile Roofing2", 40.0, 85.0, typeof(TilePiece), "Tile Piece", 3, "You don't have enough tile pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2); 
            index = AddBuild(typeof(TileRoofingS), "Roofing", "Tile Roofing Sur", 40.0, 85.0, typeof(TilePiece), "Tile Piece", 3, "You don't have enough tile pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(TileRoofingE), "Roofing", "Tile Roofing Este", 40.0, 85.0, typeof(TilePiece), "Tile Piece", 3, "You don't have enough tile pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(TileRoofingN), "Roofing", "Tile Roofing Norte", 40.0, 85.0, typeof(TilePiece), "Tile Piece", 3, "You don't have enough tile pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(TileRoofingW), "Roofing", "Tile Roofing Oeste", 40.0, 85.0, typeof(TilePiece), "Tile Piece", 3, "You don't have enough tile pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(TileRoofingEsquinaS), "Roofing", "Tile Roofing Esquina Sur", 40.0, 85.0, typeof(TilePiece), "Tile Piece", 3, "You don't have enough tile pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(TileRoofingEsquinaE), "Roofing", "Tile Roofing Esquina Este", 40.0, 85.0, typeof(TilePiece), "Tile Piece", 3, "You don't have enough tile pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(TileRoofingEsquinaN), "Roofing", "Tile Roofing Esquina Norte", 40.0, 85.0, typeof(TilePiece), "Tile Piece", 3, "You don't have enough tile pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(TileRoofingEsquinaW), "Roofing", "Tile Roofing Esquina Oeste", 40.0, 85.0, typeof(TilePiece), "Tile Piece", 3, "You don't have enough tile pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);


//Log
            index = AddBuild(typeof(LogRoofing), "Roofing", "Log Roofing", 20.0, 60.0, typeof(LogPiece), "Log Piece", 3, "You don't have enough clay pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(LogRoofing2), "Roofing", "Log Roofing2", 20.0, 60.0, typeof(LogPiece), "Log Piece", 3, "You don't have enough clay pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(LogRoofingS), "Roofing", "Log Roofing Sur", 20.0, 60.0, typeof(LogPiece), "Log Piece", 3, "You don't have enough clay pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(LogRoofingE), "Roofing", "Log Roofing Este", 20.0, 60.0, typeof(LogPiece), "Log Piece", 3, "You don't have enough clay pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(LogRoofingN), "Roofing", "Log Roofing Norte", 20.0, 60.0, typeof(LogPiece), "Log Piece", 3, "You don't have enough clay pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(LogRoofingW), "Roofing", "Log Roofing Oeste", 20.0, 60.0, typeof(LogPiece), "Log Piece", 3, "You don't have enough clay pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(LogRoofingEsquinaS), "Roofing", "Log Roofing Esquina Sur", 20.0, 60.0, typeof(LogPiece), "Log Piece", 3, "You don't have enough clay pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(LogRoofingEsquinaE), "Roofing", "Log Roofing Esquina Este", 20.0, 60.0, typeof(LogPiece), "Log Piece", 3, "You don't have enough clay pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(LogRoofingEsquinaN), "Roofing", "Log Roofing Esquina Norte", 20.0, 60.0, typeof(LogPiece), "Log Piece", 3, "You don't have enough clay pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(LogRoofingEsquinaW), "Roofing", "Log Roofing Esquina Oeste", 20.0, 60.0, typeof(LogPiece), "Log Piece", 3, "You don't have enough clay pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);




//shingleRoofing
            index = AddBuild(typeof(ShingleRoofing), "Roofing", "Shingle Roofing", 25.0, 65.0, typeof(WoodPiece), "Wood Piece", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ShingleRoofing2), "Roofing", "Shingle Roofing2", 25.0, 65.0, typeof(WoodPiece), "Wood Piece", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ShingleRoofingS), "Roofing", "Shingle Roofing Sur", 25.0, 65.0, typeof(WoodPiece), "Wood Piece", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ShingleRoofingE), "Roofing", "Shingle Roofing Este", 25.0, 65.0, typeof(WoodPiece), "Wood Piece", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ShingleRoofingN), "Roofing", "Shingle Roofing Norte", 25.0, 65.0, typeof(WoodPiece), "Wood Piece", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ShingleRoofingW), "Roofing", "Shingle Roofing Oeste", 25.0, 65.0, typeof(WoodPiece), "Wood Piece", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ShingleRoofingEsquinaS), "Roofing", "Shingle Roofing Esquina Sur", 25.0, 65.0, typeof(WoodPiece), "Wood Piece", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ShingleRoofingEsquinaE), "Roofing", "Shingle Roofing Esquina Este", 25.0, 65.0, typeof(WoodPiece), "Wood Piece", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ShingleRoofingEsquinaN), "Roofing", "Shingle Roofing Esquina Norte", 25.0, 65.0, typeof(WoodPiece), "Wood Piece", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ShingleRoofingEsquinaW), "Roofing", "Shingle Roofing Esquina Oeste", 25.0, 65.0, typeof(WoodPiece), "Wood Piece", 3, "You don't have enough wood pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);


//Thatch
            index = AddBuild(typeof(ThatchRoofing), "Roofing", "Thatch Roofing", 10.0, 50.0, typeof(ThatchPiece), "Thatch Piece", 3, "You don't have enough thatch pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ThatchRoofing2), "Roofing", "Thatch Roofing2", 10.0, 50.0, typeof(ThatchPiece), "Thatch Piece", 3, "You don't have enough thatch pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ThatchRoofingS), "Roofing", "Thatch Roofing Sur", 10.0, 50.0, typeof(ThatchPiece), "Thatch Piece", 3, "You don't have enough thatch pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ThatchRoofingE), "Roofing", "Thatch Roofing Este", 10.0, 50.0, typeof(ThatchPiece), "Thatch Piece", 3, "You don't have enough thatch pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ThatchRoofingN), "Roofing", "Thatch Roofing Norte", 10.0, 50.0, typeof(ThatchPiece), "Thatch Piece", 3, "You don't have enough thatch pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ThatchRoofingW), "Roofing", "Thatch Roofing Oeste", 10.0, 50.0, typeof(ThatchPiece), "Thatch Piece", 3, "You don't have enough thatch pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ThatchRoofingEsquinaS), "Roofing", "Thatch Roofing Esquina Sur", 10.0, 50.0, typeof(ThatchPiece), "Thatch Piece", 3, "You don't have enough thatch pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ThatchRoofingEsquinaE), "Roofing", "Thatch Roofing Esquina Este", 10.0, 50.0, typeof(ThatchPiece), "Thatch Piece", 3, "You don't have enough thatch pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ThatchRoofingEsquinaN), "Roofing", "Thatch Roofing Esquina Norte", 10.0, 50.0, typeof(ThatchPiece), "Thatch Piece", 3, "You don't have enough thatch pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(ThatchRoofingEsquinaW), "Roofing", "Thatch Roofing Esquina Oeste", 10.0, 50.0, typeof(ThatchPiece), "Thatch Piece", 3, "You don't have enough thatch pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);





//Palm
            index = AddBuild(typeof(PalmRoofing), "Roofing", "Palm Roofing", 15.0, 55.0, typeof(PalmPiece), "Palm Piece", 3, "You don't have enough palm pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(PalmRoofing2), "Roofing", "Palm Roofing2", 15.0, 55.0, typeof(PalmPiece), "Palm Piece", 3, "You don't have enough palm pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(PalmRoofingS), "Roofing", "Palm Roofing Sur", 15.0, 55.0, typeof(PalmPiece), "Palm Piece", 3, "You don't have enough palm pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(PalmRoofingE), "Roofing", "Palm Roofing Este", 15.0, 55.0, typeof(PalmPiece), "Palm Piece", 3, "You don't have enough palm pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(PalmRoofingN), "Roofing", "Palm Roofing Norte", 15.0, 55.0, typeof(PalmPiece), "Palm Piece", 3, "You don't have enough palm pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(PalmRoofingW), "Roofing", "Palm Roofing Oeste", 15.0, 55.0, typeof(PalmPiece), "Palm Piece", 3, "You don't have enough palm pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(PalmRoofingEsquinaS), "Roofing", "Palm Roofing Esquina Sur", 15.0, 55.0, typeof(PalmPiece), "Palm Piece", 3, "You don't have enough palm pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(PalmRoofingEsquinaE), "Roofing", "Palm Roofing Esquina Este", 15.0, 55.0, typeof(PalmPiece), "Palm Piece", 3, "You don't have enough palm pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(PalmRoofingEsquinaN), "Roofing", "Palm Roofing Esquina Norte", 15.0, 55.0, typeof(PalmPiece), "Palm Piece", 3, "You don't have enough palm pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(PalmRoofingEsquinaW), "Roofing", "Palm Roofing Esquina Oeste", 15.0, 55.0, typeof(PalmPiece), "Palm Piece", 3, "You don't have enough palm pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);

//Slate
            index = AddBuild(typeof(SlateRoofing), "Roofing", "Slate Roofing", 50.0, 100.0, typeof(SlatePiece), "Slate Piece", 3, "You don't have enough slate pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(SlateRoofing2), "Roofing", "Slate Roofing2", 50.0, 100.0, typeof(SlatePiece), "Slate Piece", 3, "You don't have enough slate pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(SlateRoofingS), "Roofing", "Slate Roofing Sur", 50.0, 100.0, typeof(SlatePiece), "Slate Piece", 3, "You don't have enough slate pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(SlateRoofingE), "Roofing", "Slate Roofing Este", 50.0, 100.0, typeof(SlatePiece), "Slate Piece", 3, "You don't have enough slate pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(SlateRoofingN), "Roofing", "Slate Roofing Norte", 50.0, 100.0, typeof(SlatePiece), "Slate Piece", 3, "You don't have enough slate pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(SlateRoofingW), "Roofing", "Slate Roofing Oeste", 50.0, 100.0, typeof(SlatePiece), "Slate Piece", 3, "You don't have enough slate pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(SlateRoofingEsquinaS), "Roofing", "Slate Roofing Esquina Sur" , 50.0, 100.0, typeof(SlatePiece), "Slate Piece", 3, "You don't have enough slate pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(SlateRoofingEsquinaE), "Roofing", "Slate Roofing Esquina Este", 50.0, 100.0, typeof(SlatePiece), "Slate Piece", 3, "You don't have enough slate pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(SlateRoofingEsquinaN), "Roofing", "Slate Roofing Esquina Norte", 50.0, 100.0, typeof(SlatePiece), "Slate Piece", 3, "You don't have enough slate pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);
            index = AddBuild(typeof(SlateRoofingEsquinaW), "Roofing", "Slate Roofing Esquina Oeste", 50.0, 100.0, typeof(SlatePiece), "Slate Piece", 3, "You don't have enough slate pieces.");
            AddRes(index, typeof(PitchSupply), "Pitch", 4);
            AddRes(index, typeof(TarSupply), "Tar", 2);

        }
    }
}