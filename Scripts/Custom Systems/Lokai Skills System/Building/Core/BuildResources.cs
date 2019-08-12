/***************************************************************************
 *   Based off the RunUO Craft system. This program is free software; you 
 *   can redistribute it and/or modify it under the terms of the GNU GPL. 
 ***************************************************************************/
using System;
using System.Collections;

namespace Server.Items
{
    public enum BuildResource
    {
        None = 0,
        WoodPiece = 1, StonePiece, BrickPiece, LogPiece, WroughtIronPiece, SandstonePiece, MarblePiece,
            TilePiece, MetalPiece, ThatchPiece, PalmPiece, SlatePiece, RugPiece, RatanPiece, HidePiece, BambooPiece,
        CementSupply = 101, MortarSupply, PitchSupply, TarSupply, NailSupply, JointSupply, HingeSupply, PaintSupply, StainSupply,
        TileRoofing = 201, LogRoofing, ThatchRoofing, ShingleRoofing, PalmRoofing, SlateRoofing,
        BrickFlooring = 301, TileFlooring, MarbleFlooring, StoneFlooring, SandStoneFlooring, FlagStoneFlooring, CobbleStoneFlooring, WoodFlooring, RugFlooring,
        WoodPanel = 401, StoneSlab, BrickPanel, LogPanel, WroughtIronPanel, SandstonePanel, MarbleSlab, RatanPanel, HidePanel, BambooPanel,
        WoodWall = 501, StoneWall, BrickWall, LogWall, WroughtIronFence, SandstoneWall, MarbleWall, RatanWall, HideWall, BambooWall,
        WoodDoor = 601, Metal_Door, BarredMetal_Door, LogDoor, WroughtIronGate, RatanDoor, HideDoor, BambooDoor,
        WoodWindow = 701, StoneWindow, BrickWindow, LogWindow, SandstoneWindow, MarbleWindow, RatanWindow, HideWindow, BambooWindow,
        WoodStair = 801, StoneStair, SandstoneStair, MarbleStair,
        WoodFoundation = 901, StoneFoundation, BrickFoundation, SandstoneFoundation, MarbleFoundation
    }

    public enum BuildResourceType
    {
        None,
        BasePieces,
        Supplies,
        Roofing,
        Flooring,
        Panels,
        Walls,
        Doors,
        Windows,
        Stairs,
        Foundations
    }

    public class BuildAttributeInfo
    {
        public static readonly BuildAttributeInfo Blank;
        public BuildAttributeInfo() { }
        static BuildAttributeInfo()
        {
            Blank = new BuildAttributeInfo();
        }
    }

    public class BuildResourceInfo
    {
        private int m_Hue;
        private int m_Number;
        private string m_Name;
        private BuildAttributeInfo m_AttributeInfo;
        private BuildResource m_Resource;
        private Type[] m_ResourceTypes;

        public int Hue { get { return m_Hue; } }
        public int Number { get { return m_Number; } }
        public string Name { get { return m_Name; } }
        public BuildAttributeInfo AttributeInfo { get { return m_AttributeInfo; } }
        public BuildResource Resource { get { return m_Resource; } }
        public Type[] ResourceTypes { get { return m_ResourceTypes; } }

        public BuildResourceInfo(string name, BuildResource resource, params Type[] resourceTypes)
            : this(0x000, 0, name, BuildAttributeInfo.Blank, resource, resourceTypes)
        {
        }

        public BuildResourceInfo(int hue, int number, string name, BuildAttributeInfo attributeInfo, BuildResource resource, params Type[] resourceTypes)
        {
            m_Hue = hue;
            m_Number = number;
            m_Name = name;
            m_AttributeInfo = attributeInfo;
            m_Resource = resource;
            m_ResourceTypes = resourceTypes;

            for (int i = 0; i < resourceTypes.Length; ++i)
                BuildResources.RegisterType(resourceTypes[i], resource);
        }
    }

    public class BuildResources
    {
        private static BuildResourceInfo[] m_BaseInfo = new BuildResourceInfo[]
			{
				new BuildResourceInfo("WoodPiece", BuildResource.WoodPiece, typeof(WoodPiece)),
				new BuildResourceInfo("StonePiece", BuildResource.StonePiece, typeof(StonePiece)),
				new BuildResourceInfo("BrickPiece", BuildResource.BrickPiece, typeof(BrickPiece)),
				new BuildResourceInfo("LogPiece", BuildResource.LogPiece, typeof(LogPiece)),
				new BuildResourceInfo("WroughtIronPiece", BuildResource.WroughtIronPiece, typeof(WroughtIronPiece)),
				new BuildResourceInfo("SandstonePiece", BuildResource.SandstonePiece, typeof(SandstonePiece)),
				new BuildResourceInfo("MarblePiece", BuildResource.MarblePiece, typeof(MarblePiece)),
				new BuildResourceInfo("TilePiece", BuildResource.TilePiece, typeof(TilePiece)),
				new BuildResourceInfo("MetalPiece", BuildResource.MetalPiece, typeof(MetalPiece)),
				new BuildResourceInfo("ThatchPiece", BuildResource.ThatchPiece, typeof(ThatchPiece)),
				new BuildResourceInfo("PalmPiece", BuildResource.PalmPiece, typeof(PalmPiece)),
				new BuildResourceInfo("SlatePiece", BuildResource.SlatePiece, typeof(SlatePiece)),
				new BuildResourceInfo("RugPiece", BuildResource.RugPiece, typeof(RugPiece)),
				new BuildResourceInfo("RatanPiece", BuildResource.RatanPiece, typeof(RatanPiece)),
				new BuildResourceInfo("HidePiece", BuildResource.HidePiece, typeof(HidePiece)),
				new BuildResourceInfo("BambooPiece", BuildResource.BambooPiece, typeof(BambooPiece)),
			};

        private static BuildResourceInfo[] m_SuppliesInfo = new BuildResourceInfo[]
			{
				new BuildResourceInfo("CementSupply", BuildResource.CementSupply, typeof(CementSupply)),
				new BuildResourceInfo("MortarSupply", BuildResource.MortarSupply, typeof(MortarSupply)),
				new BuildResourceInfo("PitchSupply", BuildResource.PitchSupply, typeof(PitchSupply)),
				new BuildResourceInfo("TarSupply", BuildResource.TarSupply, typeof(TarSupply)),
				new BuildResourceInfo("NailSupply", BuildResource.NailSupply, typeof(NailSupply)),
				new BuildResourceInfo("JointSupply", BuildResource.JointSupply, typeof(JointSupply)),
				new BuildResourceInfo("HingeSupply", BuildResource.HingeSupply, typeof(HingeSupply)),
				new BuildResourceInfo("PaintSupply", BuildResource.PaintSupply, typeof(PaintSupply)),
				new BuildResourceInfo("StainSupply", BuildResource.StainSupply, typeof(StainSupply)),
			};

        private static BuildResourceInfo[] m_RoofingInfo= new BuildResourceInfo[]
			{
				new BuildResourceInfo("TileRoofing", BuildResource.TileRoofing, typeof(TileRoofing)),
				new BuildResourceInfo("LogRoofing", BuildResource.LogRoofing, typeof(LogRoofing)),
				new BuildResourceInfo("ThatchRoofing", BuildResource.ThatchRoofing, typeof(ThatchRoofing)),
				new BuildResourceInfo("ShingleRoofing", BuildResource.ShingleRoofing, typeof(ShingleRoofing)),
				new BuildResourceInfo("PalmRoofing", BuildResource.PalmRoofing, typeof(PalmRoofing)),
				new BuildResourceInfo("SlateRoofing", BuildResource.SlateRoofing, typeof(SlateRoofing)),
			};

        private static BuildResourceInfo[] m_FlooringInfo = new BuildResourceInfo[]
			{
				new BuildResourceInfo("BrickFlooring", BuildResource.BrickFlooring, typeof(BrickFlooring)),
				new BuildResourceInfo("TileFlooring", BuildResource.TileFlooring, typeof(TileFlooring)),
				new BuildResourceInfo("MarbleFlooring", BuildResource.MarbleFlooring, typeof(MarbleFlooring)),
				new BuildResourceInfo("StoneFlooring", BuildResource.StoneFlooring, typeof(StoneFlooring)),
				new BuildResourceInfo("SandStoneFlooring", BuildResource.SandStoneFlooring, typeof(SandStoneFlooring)),
				new BuildResourceInfo("FlagStoneFlooring", BuildResource.FlagStoneFlooring, typeof(FlagStoneFlooring)),
				new BuildResourceInfo("CobbleStoneFlooring", BuildResource.CobbleStoneFlooring, typeof(CobbleStoneFlooring)),
				new BuildResourceInfo("WoodFlooring", BuildResource.WoodFlooring, typeof(WoodFlooring)),
				new BuildResourceInfo("RugFlooring", BuildResource.RugFlooring, typeof(RugFlooring)),
			};

        private static BuildResourceInfo[] m_PanelInfo = new BuildResourceInfo[]
			{
				new BuildResourceInfo("WoodPanel", BuildResource.WoodPanel, typeof(WoodPanel)),
				new BuildResourceInfo("StoneSlab", BuildResource.StoneSlab, typeof(StoneSlab)),
				new BuildResourceInfo("BrickPanel", BuildResource.BrickPanel, typeof(BrickPanel)),
				new BuildResourceInfo("LogPanel", BuildResource.LogPanel, typeof(LogPanel)),
				new BuildResourceInfo("WroughtIronPanel", BuildResource.WroughtIronPanel, typeof(WroughtIronPanel)),
				new BuildResourceInfo("SandstonePanel", BuildResource.SandstonePanel, typeof(SandstonePanel)),
				new BuildResourceInfo("MarbleSlab", BuildResource.MarbleSlab, typeof(MarbleSlab)),
				new BuildResourceInfo("RatanPanel", BuildResource.RatanPanel, typeof(RatanPanel)),
				new BuildResourceInfo("HidePanel", BuildResource.HidePanel, typeof(HidePanel)),
				new BuildResourceInfo("BambooPanel", BuildResource.BambooPanel, typeof(BambooPanel)),
			};

        private static BuildResourceInfo[] m_WallsInfo = new BuildResourceInfo[]
			{
				new BuildResourceInfo("WoodWall", BuildResource.WoodWall, typeof(WoodWall)),
				new BuildResourceInfo("StoneWall", BuildResource.StoneWall, typeof(StoneWall)),
				new BuildResourceInfo("BrickWall", BuildResource.BrickWall, typeof(BrickWall)),
				new BuildResourceInfo("LogWall", BuildResource.LogWall, typeof(LogWall)),
				new BuildResourceInfo("WroughtIronFence", BuildResource.WroughtIronFence, typeof(WroughtIronFence)),
				new BuildResourceInfo("SandstoneWall", BuildResource.SandstoneWall, typeof(SandstoneWall)),
				new BuildResourceInfo("MarbleWall", BuildResource.MarbleWall, typeof(MarbleWall)),
				new BuildResourceInfo("RatanWall", BuildResource.RatanWall, typeof(RatanWall)),
				new BuildResourceInfo("HideWall", BuildResource.HideWall, typeof(HideWall)),
				new BuildResourceInfo("BambooWall", BuildResource.BambooWall, typeof(BambooWall)),
			};

        private static BuildResourceInfo[] m_DoorsInfo = new BuildResourceInfo[]
			{
				new BuildResourceInfo("WoodDoor", BuildResource.WoodDoor, typeof(WoodDoor)),
				new BuildResourceInfo("Metal_Door", BuildResource.Metal_Door, typeof(Metal_Door)),
				new BuildResourceInfo("BarredMetal_Door", BuildResource.BarredMetal_Door, typeof(BarredMetal_Door)),
				new BuildResourceInfo("LogDoor", BuildResource.LogDoor, typeof(LogDoor)),
				new BuildResourceInfo("WroughtIronGate", BuildResource.WroughtIronGate, typeof(WroughtIronGate)),
				new BuildResourceInfo("RatanDoor", BuildResource.RatanDoor, typeof(RatanDoor)),
				new BuildResourceInfo("HideDoor", BuildResource.HideDoor, typeof(HideDoor)),
				new BuildResourceInfo("BambooDoor", BuildResource.BambooDoor, typeof(BambooDoor)),
			};

        private static BuildResourceInfo[] m_WindowsInfo = new BuildResourceInfo[]
			{
				new BuildResourceInfo("WoodWindow", BuildResource.WoodWindow, typeof(WoodWindow)),
				new BuildResourceInfo("StoneWindow", BuildResource.StoneWindow, typeof(StoneWindow)),
				new BuildResourceInfo("BrickWindow", BuildResource.BrickWindow, typeof(BrickWindow)),
				new BuildResourceInfo("LogWindow", BuildResource.LogWindow, typeof(LogWindow)),
				new BuildResourceInfo("SandstoneWindow", BuildResource.SandstoneWindow, typeof(SandstoneWindow)),
				new BuildResourceInfo("MarbleWindow", BuildResource.MarbleWindow, typeof(MarbleWindow)),
				new BuildResourceInfo("RatanWindow", BuildResource.RatanWindow, typeof(RatanWindow)),
				new BuildResourceInfo("HideWindow", BuildResource.HideWindow, typeof(HideWindow)),
				new BuildResourceInfo("BambooWindow", BuildResource.BambooWindow, typeof(BambooWindow)),
			};

        private static BuildResourceInfo[] m_StairsInfo = new BuildResourceInfo[]
			{
				new BuildResourceInfo("WoodStair", BuildResource.WoodStair, typeof(WoodStair)),
				new BuildResourceInfo("StoneStair", BuildResource.StoneStair, typeof(StoneStair)),
				new BuildResourceInfo("SandstoneStair", BuildResource.SandstoneStair, typeof(SandstoneStair)),
				new BuildResourceInfo("MarbleStair", BuildResource.MarbleStair, typeof(MarbleStair)),
			};

        private static BuildResourceInfo[] m_FoundationsInfo = new BuildResourceInfo[]
			{
				new BuildResourceInfo("WoodFoundation", BuildResource.WoodFoundation, typeof(WoodFoundation)),
				new BuildResourceInfo("StoneFoundation", BuildResource.StoneFoundation, typeof(StoneFoundation)),
				new BuildResourceInfo("BrickFoundation", BuildResource.BrickFoundation, typeof(BrickFoundation)),
				new BuildResourceInfo("SandstoneFoundation", BuildResource.SandstoneFoundation, typeof(SandstoneFoundation)),
				new BuildResourceInfo("MarbleFoundation", BuildResource.MarbleFoundation, typeof(MarbleFoundation)),
			};

        /// <summary>
        /// Returns true if '<paramref name="resource"/>' is None. False if otherwise.
        /// </summary>
        public static bool IsStandard(BuildResource resource)
        {
            return (resource == BuildResource.None);
        }

        private static Hashtable m_TypeTable;

        /// <summary>
        /// Registers that '<paramref name="resourceType"/>' uses '<paramref name="resource"/>' so that it can later be queried by <see cref="BuildResources.GetFromType"/>
        /// </summary>
        public static void RegisterType(Type resourceType, BuildResource resource)
        {
            if (m_TypeTable == null)
                m_TypeTable = new Hashtable();

            m_TypeTable[resourceType] = resource;
        }

        /// <summary>
        /// Returns the <see cref="BuildResource"/> value for which '<paramref name="resourceType"/>' uses -or- BuildResource.None if an unregistered type was specified.
        /// </summary>
        public static BuildResource GetFromType(Type resourceType)
        {
            if (m_TypeTable == null)
                return BuildResource.None;

            object obj = m_TypeTable[resourceType];

            if (!(obj is BuildResource))
                return BuildResource.None;

            return (BuildResource)obj;
        }

        /// <summary>
        /// Returns a <see cref="BuildResourceInfo"/> instance describing '<paramref name="resource"/>' -or- null if an invalid resource was specified.
        /// </summary>
        public static BuildResourceInfo GetInfo(BuildResource resource)
        {
            BuildResourceInfo[] list = null;

            switch (GetType(resource))
            {
                case BuildResourceType.BasePieces: list = m_BaseInfo; break;
                case BuildResourceType.Supplies: list = m_SuppliesInfo; break;
                case BuildResourceType.Flooring: list = m_FlooringInfo; break;
                case BuildResourceType.Roofing: list = m_RoofingInfo; break;
                case BuildResourceType.Panels: list = m_PanelInfo; break;
                case BuildResourceType.Walls: list = m_WallsInfo; break;
                case BuildResourceType.Doors: list = m_DoorsInfo; break;
                case BuildResourceType.Windows: list = m_WindowsInfo; break;
                case BuildResourceType.Stairs: list = m_StairsInfo; break;
                case BuildResourceType.Foundations: list = m_FoundationsInfo; break;
            }

            if (list != null)
            {
                int index = GetIndex(resource);

                if (index >= 0 && index < list.Length)
                    return list[index];
            }

            return null;
        }

        /// <summary>
        /// Returns a <see cref="BuildResourceType"/> value indiciating the type of '<paramref name="resource"/>'.
        /// </summary>
        public static BuildResourceType GetType(BuildResource resource)
        {
            if (resource >= BuildResource.WoodPiece && resource <= BuildResource.BambooPiece)
                return BuildResourceType.BasePieces;

            if (resource >= BuildResource.CementSupply && resource <= BuildResource.StainSupply)
                return BuildResourceType.Supplies;

            if (resource >= BuildResource.TileRoofing && resource <= BuildResource.SlateRoofing)
                return BuildResourceType.Roofing;

            if (resource >= BuildResource.BrickFlooring && resource <= BuildResource.RugFlooring)
                return BuildResourceType.Flooring;

            if (resource >= BuildResource.WoodPanel && resource <= BuildResource.BambooPanel)
                return BuildResourceType.Panels;

            if (resource >= BuildResource.WoodWall && resource <= BuildResource.BambooWall)
                return BuildResourceType.Walls;

            if (resource >= BuildResource.WoodDoor && resource <= BuildResource.BambooDoor)
                return BuildResourceType.Doors;

            if (resource >= BuildResource.WoodWindow && resource <= BuildResource.BambooWindow)
                return BuildResourceType.Windows;

            if (resource >= BuildResource.WoodStair && resource <= BuildResource.MarbleStair)
                return BuildResourceType.Stairs;

            if (resource >= BuildResource.WoodFoundation && resource <= BuildResource.MarbleFoundation)
                return BuildResourceType.Foundations;

            return BuildResourceType.None;
        }

        /// <summary>
        /// Returns the first <see cref="BuildResource"/> in the series of resources for which '<paramref name="resource"/>' belongs.
        /// </summary>
        public static BuildResource GetStart(BuildResource resource)
        {
            switch (GetType(resource))
            {
                case BuildResourceType.BasePieces: return BuildResource.WoodPiece;
                case BuildResourceType.Supplies: return BuildResource.CementSupply;
                case BuildResourceType.Roofing: return BuildResource.TileRoofing;
                case BuildResourceType.Flooring: return BuildResource.BrickFlooring;
                case BuildResourceType.Panels: return BuildResource.WoodPanel;
                case BuildResourceType.Walls: return BuildResource.WoodWall;
                case BuildResourceType.Doors: return BuildResource.WoodDoor;
                case BuildResourceType.Windows: return BuildResource.WoodWindow;
                case BuildResourceType.Stairs: return BuildResource.WoodStair;
                case BuildResourceType.Foundations: return BuildResource.WoodFoundation;
            }

            return BuildResource.None;
        }

        /// <summary>
        /// Returns the index of '<paramref name="resource"/>' in the seriest of resources for which it belongs.
        /// </summary>
        public static int GetIndex(BuildResource resource)
        {
            BuildResource start = GetStart(resource);

            if (start == BuildResource.None)
                return 0;

            return (int)(resource - start);
        }

        /// <summary>
        /// Returns the <see cref="BuildResourceInfo.Number"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
        /// </summary>
        public static int GetLocalizationNumber(BuildResource resource)
        {
            BuildResourceInfo info = GetInfo(resource);

            return (info == null ? 0 : info.Number);
        }

        /// <summary>
        /// Returns the <see cref="BuildResourceInfo.Hue"/> property of '<paramref name="resource"/>' -or- 0 if an invalid resource was specified.
        /// </summary>
        public static int GetHue(BuildResource resource)
        {
            BuildResourceInfo info = GetInfo(resource);

            return (info == null ? 0 : info.Hue);
        }

        /// <summary>
        /// Returns the <see cref="BuildResourceInfo.Name"/> property of '<paramref name="resource"/>' -or- an empty string if the resource specified was invalid.
        /// </summary>
        public static string GetName(BuildResource resource)
        {
            BuildResourceInfo info = GetInfo(resource);

            return (info == null ? String.Empty : info.Name);
        }
    }
}