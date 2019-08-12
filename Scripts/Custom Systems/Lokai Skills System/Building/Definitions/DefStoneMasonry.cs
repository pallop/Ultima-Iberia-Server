/***************************************************************************
 *   New LokaiSkill System script by Lokai. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Build
{
    public class DefStoneMasonry : BuildSystem
    {
        public override LokaiSkillName MainLokaiSkill
        {
            get { return LokaiSkillName.StoneMasonry; }
        }

        public override string GumpTitleString
        {
            get { return "Stone Masonry"; }
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
                    m_BuildSystem = new DefStoneMasonry();

                return m_BuildSystem;
            }
        }

        public override double GetChanceAtMin(BuildItem item)
        {
            return 0.0; // 0% 
        }

        private DefStoneMasonry()
            : base(1, 1, 1.25)// base( 1, 2, 1.7 ) 
        {
        }

        public override void InitBuildList()
        {
            int index = -1;

            // Panels

            //stone 1
            index = AddBuild(typeof(Stone1Est), "Panels", "Stone1 Este", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone1Sur), "Panels", "Stone1 Sur", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone1EsquinaGrande), "Panels", "Stone1 Esquina Grande", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone1EsquinaPeque), "Panels", "Stone1 Esquina Pequeña", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone1EstPeque), "Panels", "Stone1 Este Pequeña ", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone1SurPeque), "Panels", "Stone1 Sur Pequeña ", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
          
            //stoneslab
            index = AddBuild(typeof(StoneSlab), "Panels", "Stone Slab Sur", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneSlabEst), "Panels", "Stone Slab Este", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneSlabEsquinaGrande), "Panels", "Stone Slab Esquina Grande", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneSlabSurPeque), "Panels", "Stone Slab Sur Pequeña", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneSlabEstPeque), "Panels", "Stone Slab Este Pequeña", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneSlabEsquinaPeque), "Panels", "Stone Slab Esquina Pequeña", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneSlabRampa1Sur), "Panels", "Stone Slab Rampa1 Sur", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneSlabRampa1Est), "Panels", "Stone Slab Rampa1 Este", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneSlabRampa2Est), "Panels", "Stone Slab Rampa2 Este", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneSlabRampa2Sur), "Panels", "Stone Slab Rampa2 Sur", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneSlabEsquinaNormal), "Panels", "Stone Slab Esquina Normal", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneSlabEsquinaPincho), "Panels", "Stone Slab Esquina Pincho", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneSlabEsquinaPlana), "Panels", "Stone Slab Esquina Plana", 10.0, 50.0, typeof(StonePiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);

 //stone 3

            index = AddBuild(typeof(Stone3placa1), "Panels", "Stone3 Placa1", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3placa2), "Panels", "Stone3 Placa2", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3EsquinaNort), "Panels", "Stone3 Esquina Norte", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3EsquinaSur), "Panels", "Stone3 Esquina Sur", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3EsquinaSW), "Panels", "Stone3 Esquina SurOeste", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3EsquinaNE), "Panels", "Stone3 Esquina NorEste", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3EstGrandeFuera), "Panels", "Stone3 Este Grande Fuera", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3EstGrandeDentro), "Panels", "Stone3 Este Grande Dentro", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3SurGrandeFuera), "Panels", "Stone3 Sur Grande Fuera", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3SurGrandeDentro), "Panels", "Stone3 Sur Grande Dentro", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3SurPequeFuera), "Panels", "Stone3 Sur Pequeña Fuera", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3SurPequeDentro), "Panels", "Stone3 Sur Pequeña Dentro", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3EstPequeFuera), "Panels", "Stone3 Oeste Pequeña Fuera", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3EstPequeDentro), "Panels", "Stone3 Este Pequeña Dentro", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3RampaWest), "Panels", "Stone3 Rampa Oeste ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3RampaEst), "Panels", "Stone3 Rampa Este ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3RampaSur), "Panels", "Stone3 Rampa Sur ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3RampaNorte), "Panels", "Stone3 Rampa Norte  ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3EsquinaPuntaNE), "Panels", "Stone3 Esquina Punta NorEste ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3EsquinaPuntaSur), "Panels","Stone3 Esquina Punta Sur ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(Stone3EsquinaPuntaSW), "Panels", "Stone3 Esquina Punta SurOeste", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
          //Stone4

              index = AddBuild(typeof(Stone4Sur), "Panels", "Stone4 Sur ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(Stone4Est), "Panels", "Stone4 Este ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(Stone4Esquina), "Panels", "Stone4 Esquina ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(Stone4Pilon), "Panels", "Stone4 Pilon ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(Stone4PilonW), "Panels", "Stone4 Pilon Oeste ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(Stone4PilonNor), "Panels", "Stone4 Pilon Norte", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);

                //Stone5
             index = AddBuild(typeof(Stone5Sur), "Panels", "Stone5 Sur ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(Stone5Est), "Panels", "Stone5 Este ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(Stone5Esquina), "Panels", "Stone5 Esquina ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(Stone5Pilon), "Panels", "Stone5 Pilon ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(Stone5PilonW), "Panels", "Stone5 Pilon Oeste ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(Stone5PilonNor), "Panels", "Stone5 Pilon Norte", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);

            //Stone7
             index = AddBuild(typeof(Stone7Sur), "Panels", "Stone7 Sur ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(Stone7Est), "Panels", "Stone7 Este ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(Stone7Esquina), "Panels", "Stone7 Esquina ", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(Stone7SurDeco), "Panels", "Stone7 Sur Deco", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(Stone7EstDeco), "Panels", "Stone7 Este Deco", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
              index = AddBuild(typeof(Stone7EsquinaDeco), "Panels", "Stone7 Esquina Deco", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);

        //sandstone
            index = AddBuild(typeof(SandstonePanel), "Panels", "Sandstone Panel Sur", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstonePanelEst), "Panels", "Sandstone Panel Este", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstonePanelSurDeco), "Panels", "Sandstone Panel Sur Deco", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstonePanelEstDeco), "Panels", "Sandstone Panel Este Deco", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstonePanelEsquinaSurDeco), "Panels", "Sandstone Panel Esquina Sur Deco", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstonePanelEsquinaN), "Panels", "Sandstone Panel Esquina Norte", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstonePanelEsquinaS), "Panels", "Sandstone Panel Esquina Sur", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstonePanelEsquinaNE), "Panels","Sandstone Panel Esquina NorEste", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstonePanelEsquinaSW), "Panels", "Sandstone Panel Esquina SurOeste", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstonePanelRampaSur), "Panels","Sandstone Panel Rampa Sur", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstonePanelRampaEst), "Panels", "Sandstone Panel Rampa Este", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstonePanelEsquinaRampaSW), "Panels", "Sandstone Panel Esquina Rampa SurOeste", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstonePanelEsquinaRampaNE), "Panels", "Sandstone Panel Esquina Rampa NorEste", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstonePanelEsquinaRampaSur), "Panels", "Sandstone Panel Esquina Rampa Sur", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);

            //clay

            index = AddBuild(typeof(ClayPanelSur), "Panels", "Clay Panel Sur", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(ClayPanelEst), "Panels", "Clay Panel Este", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(ClayPanelEsquina), "Panels", "Clay Panel Esquina", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(ClayPanelSurPeque), "Panels", "Clay Panel Sur Pequeña", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(ClayPanelEstPeque), "Panels", "Clay Panel Este Pequeña", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(ClayPanelEsquinaPeque), "Panels", "Clay Panel Esquina Pequeña", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);


            //plaster
            index = AddBuild(typeof(PlasterPanelSur), "Panels", "Plaster Panel Sur", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(PlasterPanelEst), "Panels", "Plaster Panel Este", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);

            //marble
            index = AddBuild(typeof(MarbleSlab), "Panels","Marble Slab Sur", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(MarbleSlabEst), "Panels", "Marble Slab Este", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(MarbleSlabEsquinaGrande), "Panels", "Marble Slab Esquina Grande", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(MarbleSlabSurPeque), "Panels", "Marble Slab Sur Pequeña", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(MarbleSlabEstPeque), "Panels", "Marble Slab Este Pequeña", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(MarbleSlabEsquinaPeque), "Panels", "Marble Slab Esquina Pequeña", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);

            //Wrought
            index = AddBuild(typeof(WroughtIronPanel), "Panels", "Wrought Iron Panel Sur", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WroughtIronPanelEste), "Panels", "Wrought Iron Panel Este", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(WroughtIronPanelEsquina), "Panels", "Wrought Iron Panel Esquina", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 3, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
//////////////////////////////////////////////////
//doors

//Porticus
            index = AddBuild(typeof(PorticusDoorSur), "Doors", "Porticus Door Sur", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(PorticusDoorEst), "Doors", "Porticus Door Este", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);

//WroughtIronGate
            index = AddBuild(typeof(WroughtIronGate), "Doors", "Wrought Iron Gate Sur1", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(WroughtIronGateSur2), "Doors", "Wrought Iron Gate Sur2", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(WroughtIronGateEst1), "Doors", "Wrought Iron Gate Este1", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(WroughtIronGateEst2), "Doors", "Wrought Iron Gate Este2", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);

//barredmetaldoor
            index = AddBuild(typeof(BarredMetal_Door), "Doors", "Barred Metal Door Sur1", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(BarredMetal_DoorSur2), "Doors", "Barred Metal Door Sur2", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(BarredMetal_DoorEst1), "Doors", "Barred Metal Door Este1", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(BarredMetal_DoorEst2), "Doors", "Barred Metal Door Este2", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
//metaldoor
            index = AddBuild(typeof(Metal_Door), "Doors", "Metal Door Sur 1", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(Metal_DoorSur2), "Doors", "Metal Door Sur 2", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(Metal_DoorEst1), "Doors", "Metal Door Este 1", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(Metal_DoorEst2), "Doors", "Metal Door Este 2", 10.0, 100.0, typeof(RatanPanel), "Ratan Panels", 2, "You don't have enough ratan panels.");
            AddRes(index, typeof(NailSupply), "Nails", 20);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);


////////////////////////////////////
            // Flooring
            index = AddBuild(typeof(StoneFlooring), "Flooring", "Stone Flooring", 15.0, 60.0, typeof(StonePiece), "Stone Pieces", 2, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            index = AddBuild(typeof(MarbleFlooring), "Flooring", "Marble Flooring", 35.0, 80.0, typeof(MarblePiece), "Sandstone Pieces", 2, "You don't have enough marble panels.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);


///////////////////////////////////////////////////////////
            // Walls
//stone1
            index = AddBuild(typeof(StoneWall), "Walls", "Stone Wall Sur", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWallSur1), "Walls", "Stone Wall Sur1", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWallSur2), "Walls", "Stone Wall Sur2", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWallEst), "Walls", "Stone Wall Este", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWallEst1), "Walls", "Stone Wall Este1", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWallEst2), "Walls", "Stone Wall Este2", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWallEsquina), "Walls", "Stone Wall Esquina", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);

//Stone3
            index = AddBuild(typeof(StoneWall3), "Walls", "Stone Wall 3", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall3ArcoEst1), "Walls", "Stone Wall3 Arco Este 1", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall3ArcoEst2), "Walls", "Stone Wall3 Arco Este 2", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall3ArcoSur1), "Walls", "Stone Wall3  Arco Sur 1", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall3ArcoSur2), "Walls", "Stone Wall 3 Arco Sur 2", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
//Stone4
            index = AddBuild(typeof(StoneWall4Pilar), "Walls", "Stone Wall 4 Pilar", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall4Esquina), "Walls", "Stone Wall 4 Esquina", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall4Sur), "Walls", "Stone Wall 4 Sur ", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall4Est), "Walls", "Stone Wall 4 Este", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall4EsquinaArco), "Walls", "Stone Wall 4 Esquina Arco", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall4ArcoSur1), "Walls", "Stone Wall 4 Arco Sur 1", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall4ArcoSur2), "Walls", "Stone Wall 4 Arco Sur 2", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall4ArcoEst1), "Walls", "Stone Wall 4 Arco Este 1", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall4ArcoEst2), "Walls", "Stone Wall 4 Arco Este 2", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
//Stone5          
             
            index = AddBuild(typeof(StoneWall5Esquina), "Walls", "Stone Wall 5 Esquina", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall5Sur), "Walls", "Stone Wall 5 Sur ", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall5Est), "Walls", "Stone Wall 5 Este", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall5EsquinaArco), "Walls", "Stone Wall 5 Esquina Arco", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall5ArcoSur1), "Walls", "Stone Wall 5 Arco Sur 1", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall5ArcoSur2), "Walls", "Stone Wall 5 Arco Sur 2", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall5ArcoEst1), "Walls", "Stone Wall 5 Arco Este 1", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall5ArcoEst2), "Walls", "Stone Wall 5 Arco Este 2", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
//Stone7 
            index = AddBuild(typeof(StoneWall7Esquina), "Walls", "Stone Wall 7 Esquina", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall7Sur), "Walls", "Stone Wall 7 Sur ", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(StoneWall7Est), "Walls", "Stone Wall 7 Este", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
//Clay
            index = AddBuild(typeof(ClayWallEsquina), "Walls", "Clay Wall  Esquina", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(ClayWallSur), "Walls", "Clay Wall  Sur ", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(ClayWallEst), "Walls", "Clay Wall  Este", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);

//Plaster
            index = AddBuild(typeof(PlasterWallEsquina), "Walls", "Plaster Wall Esquina ", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(PlasterWallEsquina1), "Walls", "Plaster Wall Esquina 1 ", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(PlasterWallSur), "Walls", "Plaster Wall Sur", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(PlasterWallSur1), "Walls", "Plaster Wall Sur 1", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(PlasterWallEst), "Walls", "Plaster Wall Este", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(PlasterWallEst1), "Walls", "Plaster Wall Este 1", 20.0, 60.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);



//Sandstone Wall
           
            index = AddBuild(typeof(SandstoneWall), "Walls", "Sandstone Wall Sur", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstoneWallEst), "Walls", "Sandstone Wall Este", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstoneWallEsquina), "Walls", "Sandstone Wall Esquina", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstoneWallArcoEsquina), "Walls", "Sandstone Wall Arco Esquina", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstoneWallArcoSur1), "Walls", "Sandstone Wall Arco Sur1", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstoneWallArcoSur2), "Walls", "Sandstone Wall Arco Sur 2", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstoneWallArcoEst1), "Walls", "Sandstone Wall  Arco Este 1", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(SandstoneWallArcoEst2), "Walls", "Sandstone Wall Arco Este 2", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);


//Marble Wall

            index = AddBuild(typeof(MarbleWall), "Walls", "Marble Wall Sur", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(MarbleWallEst), "Walls", "Marble Wall Este", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(MarbleWallEsquina), "Walls", "Marble Wall Esquina", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(MarbleWallArcoEsquina), "Walls", "Marble Wall Arco Esquina", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(MarbleWallArcoSur1), "Walls", "Marble Wall Arco Sur1", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(MarbleWallArcoSur2), "Walls", "Marble Wall Arco Sur 2", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(MarbleWallArcoEst1), "Walls", "Marble Wall  Arco Este 1", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            index = AddBuild(typeof(MarbleWallArcoEst2), "Walls", "Marble Wall Arco Este 2", 20.0, 60.0, typeof(BrickPanel), "Brick Panels", 2, "You don't have enough brick panels.");
            AddRes(index, typeof(CementSupply), "Cement", 15);
            AddRes(index, typeof(JointSupply), "Joints", 4);





///////////////////////////////////////////////////////////////
            // Windows
//Stone1
            index = AddBuild(typeof(StoneWindow), "Windows", "Stone Window Sur", 30.0, 70.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(StoneWindowEst), "Windows", "Stone Window Este", 30.0, 70.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);

//Stone2
             index = AddBuild(typeof(StoneWindow2), "Windows", "Stone Window2 Sur", 30.0, 70.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(StoneWindow2Est), "Windows", "Stone Window2 Este", 30.0, 70.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
//stone3
             index = AddBuild(typeof(StoneWindow3), "Windows", "Stone Window3 Sur", 30.0, 70.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(StoneWindow3Est), "Windows", "Stone Window3 Este", 30.0, 70.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
//stone4
              index = AddBuild(typeof(StoneWindow4), "Windows", "Stone Window4 Sur", 30.0, 70.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(StoneWindow4Est), "Windows", "Stone Window4 Este", 30.0, 70.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
//stone5
            index = AddBuild(typeof(StoneWindow5), "Windows", "Stone Window5 Sur", 30.0, 70.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(StoneWindow5Est), "Windows", "Stone Window5 Este", 30.0, 70.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
//stone 9
            index = AddBuild(typeof(StoneWindow9), "Windows", "Stone Window9 Sur", 30.0, 70.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(StoneWindow9Est), "Windows", "Stone Window9 Este", 30.0, 70.0, typeof(StoneSlab), "Stone Slabs", 2, "You don't have enough stone slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);

//sandstone
            index = AddBuild(typeof(SandstoneWindow), "Windows", "Sandstone Window Sur", 40.0, 80.0, typeof(SandstonePanel), "Sandstone Panels", 2, "You don't have enough sandstone panels.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
            index = AddBuild(typeof(SandstoneWindowEst), "Windows", "Sandstone Window Este", 40.0, 80.0, typeof(SandstonePanel), "Sandstone Panels", 2, "You don't have enough sandstone panels.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);

//marble
            index = AddBuild(typeof(MarbleWindow), "Windows", "Marble Window Sur", 50.0, 90.0, typeof(MarbleSlab), "Marble Panels", 2, "You don't have enough marble slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
             index = AddBuild(typeof(MarbleWindowEst), "Windows", "Marble Window Este", 50.0, 90.0, typeof(MarbleSlab), "Marble Panels", 2, "You don't have enough marble slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);

//Plaster
            index = AddBuild(typeof(PlasterWindow), "Windows", "Plaster Window Sur", 50.0, 90.0, typeof(MarbleSlab), "Marble Panels", 2, "You don't have enough marble slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);
             index = AddBuild(typeof(PlasterWindowEst), "Windows", "Plaster Window Este", 50.0, 90.0, typeof(MarbleSlab), "Marble Panels", 2, "You don't have enough marble slabs.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            AddRes(index, typeof(JointSupply), "Joints", 4);
            AddRes(index, typeof(HingeSupply), "Hinges", 3);


//////////////////////////////////////////////////////////////////
// Stairs
            index = AddBuild(typeof(StoneStair), "Stairs", "Stone Stair Sur", 20.0, 60.0, typeof(StonePiece), "Stone Pieces", 10, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 12);
            index = AddBuild(typeof(StoneStairE), "Stairs", "Stone Stair Este", 20.0, 60.0, typeof(StonePiece), "Stone Pieces", 10, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 12);
            index = AddBuild(typeof(StoneStairN), "Stairs", "Stone Stair Norte", 20.0, 60.0, typeof(StonePiece), "Stone Pieces", 10, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 12);
            index = AddBuild(typeof(StoneStairw), "Stairs", "Stone Stair Oeste", 20.0, 60.0, typeof(StonePiece), "Stone Pieces", 10, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 12);

//sandstone
            index = AddBuild(typeof(SandstoneStair), "Stairs", "Sandstone Stair Sur", 40.0, 80.0, typeof(SandstonePiece), "Sandstone Pieces", 10, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 12);
             index = AddBuild(typeof(SandstoneStairE), "Stairs", "Sandstone Stair Este", 40.0, 80.0, typeof(SandstonePiece), "Sandstone Pieces", 10, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 12);
             index = AddBuild(typeof(SandstoneStairN), "Stairs", "Sandstone Stair Norte", 40.0, 80.0, typeof(SandstonePiece), "Sandstone Pieces", 10, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 12);
             index = AddBuild(typeof(SandstoneStairW), "Stairs", "Sandstone Stair Oeste", 40.0, 80.0, typeof(SandstonePiece), "Sandstone Pieces", 10, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 12);



//Marble

            index = AddBuild(typeof(MarbleStair), "Stairs", "Marble Stair Sur", 60.0, 100.0, typeof(MarblePiece), "Marble Pieces", 2, "You don't have enough marble pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            index = AddBuild(typeof(MarbleStairE), "Stairs", "Marble Stair Este", 60.0, 100.0, typeof(MarblePiece), "Marble Pieces", 2, "You don't have enough marble pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            index = AddBuild(typeof(MarbleStairN), "Stairs", "Marble Stair Norte", 60.0, 100.0, typeof(MarblePiece), "Marble Pieces", 2, "You don't have enough marble pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);
            index = AddBuild(typeof(MarbleStairW), "Stairs", "Marble Stair Oeste", 60.0, 100.0, typeof(MarblePiece), "Marble Pieces", 2, "You don't have enough marble pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);



////////////////////////////////////////////////////////////////////
            // Foundations
            index = AddBuild(typeof(StoneFoundation), "Foundations", "Stone Foundation", 10.0, 50.0, typeof(WoodPiece), "Stone Pieces", 3, "You don't have enough stone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 12);
            index = AddBuild(typeof(SandstoneFoundation), "Foundations", "Sandstone Foundation", 20.0, 60.0, typeof(SandstonePiece), "Sandstone Pieces", 10, "You don't have enough sandstone pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 12);
            index = AddBuild(typeof(MarbleFoundation), "Foundations", "Marble Foundation", 30.0, 70.0, typeof(MarblePiece), "Marble Pieces", 2, "You don't have enough marble pieces.");
            AddRes(index, typeof(MortarSupply), "Mortar", 10);

            MarkOption = false;
            Repair = false;

            SetSubRes(typeof(StonePiece), 1044525);
        }
    }
}