using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server;
using Server.Targeting;
using Server.Gumps;
using Server.ContextMenus;
using Server.Commands;


namespace Arya.Misc
{
    /// <summary>
    /// Manipulates tiles and items during a session with the copier script
    /// </summary>
    public class StaticsWorkshop
    {
        #region [CopyStatics Command

        public static void Initialize()
        {
            CommandSystem.Register("CopyStatics", AccessLevel.Administrator, new CommandEventHandler(OnCopyStatics));
        }

        [Usage("CopyStatics"), Description("Allows to copy statics to static items. The command brings up a gump that guides the process")]
        public static void OnCopyStatics(CommandEventArgs e)
        {
            e.Mobile.SendGump(new InternalGump(new StaticsWorkshop(e.Mobile)));
        }

        #endregion



        /// <summary>
        /// Contains the selected tiles. Point2Ds are keys.
        /// </summary>
        private Hashtable m_Tiles;
        /// <summary>
        /// Contains the items built.
        /// </summary>
        private List<Item> m_Building;
        /// <summary>
        /// The user
        /// </summary>
        private Mobile m_User;

        private bool m_ZRange;
        private sbyte m_MinZ = sbyte.MinValue;
        private sbyte m_MaxZ = sbyte.MaxValue;
        private int m_Nudge = 1;
        private int m_Hue = 0;

        /// <summary>
        /// States whether mass selection of tiles should be limited to a set of Z values
        /// </summary>
        public bool ZRange
        {
            get { return m_ZRange; }
            set { m_ZRange = value; }
        }

        /// <summary>
        /// Gets or sets the minumum Z value in the range
        /// </summary>
        public sbyte MinZ
        {
            get { return m_MinZ; }
            set { m_MinZ = value; }
        }

        /// <summary>
        /// Gets or sets the maximum Z value in the range
        /// </summary>
        public sbyte MaxZ
        {
            get { return m_MaxZ; }
            set { m_MaxZ = value; }
        }

        /// <summary>
        /// Gets or sets the nudge amount
        /// </summary>
        public int NudgeAmount
        {
            get { return m_Nudge; }
            set { m_Nudge = value; }
        }

        /// <summary>
        /// Gets or sets the hue for the hue operation
        /// </summary>
        public int HueValue
        {
            get { return m_Hue; }
            set
            {
                if (value < 0 || value > 3000)
                    return;

                m_Hue = value;
            }
        }


        /// <summary>
        /// Gets the number of tiles in the current selection
        /// </summary>
        public int TilesCount
        {
            get
            {
                int count = 0;

                foreach (List<Server.StaticTile> tile in m_Tiles.Values)
                {
                    count += tile.Count;
                }

                return count;
            }
        }

        public StaticsWorkshop(Mobile m)
        {
            m_User = m;
            m_Tiles = new Hashtable();
            m_Building = new List<Item>();
        }

        /// <summary>
        /// Resends the gump to the user
        /// </summary>
        private void ResendGump()
        {
            m_User.SendGump(new InternalGump(this));
        }

        /// <summary>
        /// Removes all the selected tiles
        /// </summary>
        public void Clear()
        {
            m_Tiles.Clear();
            ResendGump();
        }

        /// <summary>
        /// Requests to add a range of statics to the selected tiles
        /// </summary>
        public void AddRange()
        {
            BoundingBoxPicker.Begin(m_User, new BoundingBoxCallback(AddRangeCallback), null);
        }

        /// <summary>
        /// Callback for the bounding box picker
        /// </summary>
        private void AddRangeCallback(Mobile m, Map map, Point3D start, Point3D end, object state)
        {
            for (int x = Math.Min(start.X, end.X); x <= Math.Max(start.X, end.X); x++)
            {
                for (int y = Math.Min(start.Y, end.Y); y <= Math.Max(start.Y, end.Y); y++)
                {

                    Point2D p = new Point2D(x, y);

                    StaticTile[] tiles = map.Tiles.GetStaticTiles( x, y, true );
                    List<Server.StaticTile> list = new List<Server.StaticTile>();
                    foreach (StaticTile t in tiles)
                    	list.Add(t);
                    
                    #region Z Range Filter
                    if (m_ZRange)
                    {
                        List<Server.StaticTile> remove = new List<Server.StaticTile>();

                        foreach (StaticTile t in list)
                        {
                            if (t.Z < m_MinZ || t.Z > m_MaxZ)
                                remove.Add(t);
                        }

                        foreach (StaticTile t in remove)
                        {
                            list.Remove(t);
                        }
                    }
                    #endregion
					if (list != null && list.Count > 0)
                        {
                        	m_Tiles[new Point2D(x, y)] = list;
                       	}
                }
            }

            ResendGump();
        }

        /// <summary>
        /// Requests to add a single tile to the selected tiles
        /// </summary>
        public void AddSingle()
        {
            m_User.SendMessage(0x40, "Please select the tile you wish to add");
            m_User.Target = new InternalTarget(this, false);
        }

        private void AddSingleCallback(StaticTarget st)
        {
            StaticTile tile = new StaticTile((ushort)(st.ItemID), (sbyte)st.Z);
            Point2D p = new Point2D(st.X, st.Y);

            List<Server.StaticTile> current = m_Tiles[p] as List<Server.StaticTile>;

            if (current != null)
            {
                if (!current.Contains(tile))
                {
                    current.Add(tile);
                }
            }
            else
            {
                current = new List<Server.StaticTile>();
                current.Add(tile);
                m_Tiles[p] = current;
            }

            ResendGump();
        }

        #region InternalTarget

        private class InternalTarget : Server.Targeting.Target
        {
            private StaticsWorkshop m_Workshop;
            private bool m_Build;

            public InternalTarget(StaticsWorkshop workshop, bool build)
                : base(-1, build, Server.Targeting.TargetFlags.None)
            {
                m_Workshop = workshop;
                m_Build = build;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Build)
                {
                    // Build structure
                    IPoint3D p = targeted as IPoint3D;

                    if (p != null)
                    {
                        m_Workshop.BuildCallback(new Point3D(p));
                    }
                }
                else
                {
                    // Add static
                    StaticTarget st = targeted as StaticTarget;

                    if (st != null)
                    {
                        m_Workshop.AddSingleCallback(st);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Builds the structure (brings up the target)
        /// </summary>
        public void Build()
        {
            if (m_Tiles.Keys.Count == 0)
            {
                m_User.SendMessage(0x40, "Nothing to build...");
                return;
            }

            m_User.SendMessage(0x40, "Please select where you wish to place the structure...");
            m_User.Target = new InternalTarget(this, true);
        }

        /// <summary>
        /// Builds the structure
        /// </summary>
        /// <param name="p">The point at which to build</param>
        private void BuildCallback(Point3D p)
        {
            if (m_User.Map == null || m_User.Map == Map.Internal)
                return;

            m_Building.Clear();

            // Calculate bounds
            Point2D start = new Point2D(int.MaxValue, int.MaxValue);
            Point2D end = Point2D.Zero;
            int z = int.MaxValue;

            foreach (Point2D point in m_Tiles.Keys)
            {
                start.X = Math.Min(start.X, point.X);
                start.Y = Math.Min(start.Y, point.Y);
                end.X = Math.Max(end.X, point.X);
                end.Y = Math.Max(end.Y, point.Y);

                List<Server.StaticTile> tiles = m_Tiles[ point ] as List<Server.StaticTile>;
                foreach (StaticTile t in tiles)
                {
                    z = Math.Min(z, t.Z);
                }
            }

            Point2D center = Point2D.Zero;
            center.X = start.X + ((end.X - start.X) / 2);
            center.Y = start.Y + ((end.Y - start.Y) / 2);

            foreach (Point2D point in m_Tiles.Keys)
            {
                int xOffset = point.X - center.X;
                int yOffset = point.Y - center.Y;

                List<Server.StaticTile> tiles = m_Tiles[ point ] as List<Server.StaticTile>;

                if (tiles == null || tiles.Count == 0)
                    continue;

                foreach (StaticTile t in tiles)
                {
                    int zOffset = t.Z - p.Z;

                    Server.Items.Static item = new Server.Items.Static(t.ID);
                    item.MoveToWorld(new Point3D(p.X + xOffset, p.Y + yOffset, p.Z + zOffset), m_User.Map);
                    m_Building.Add(item);
                }
            }
            ResendGump();
        }

        /// <summary>
        /// Deletes the building last placed
        /// </summary>
        public void DeleteBuilding()
        {
            foreach (Item item in m_Building)
            {
                if (!item.Deleted)
                    item.Delete();
            }

            m_Building.Clear();
            ResendGump();
        }

        /// <summary>
        /// Nudges the built structure
        /// </summary>
        /// <param name="up">Specifies whether to nudge up (alternative is doh down)</param>
        public void Nudge(bool up)
        {
            int amount = up ? m_Nudge : -m_Nudge;

            foreach (Item item in m_Building)
            {
                if (!item.Deleted)
                    item.Z += amount;
            }

            ResendGump();
        }

        /// <summary>
        /// Sets a hue on all the structure
        /// </summary>
        public void Hue()
        {
            foreach (Item item in m_Building)
            {
                if (!item.Deleted)
                    item.Hue = m_Hue;
            }

            ResendGump();
        }

        #region InternalGump

        private class InternalGump : Gump
        {
            private StaticsWorkshop m_Workshop;
            private const int LabelHue = 0x480;
            private const int GreenHue = 0x40;
            private const int RedHue = 0x20;

            public InternalGump(StaticsWorkshop workshop)
                : base(50, 50)
            {
                m_Workshop = workshop;
                m_Workshop.m_User.CloseGump(typeof(InternalGump));
                MakeGump();
            }

            private void MakeGump()
            {
                this.Closable = false;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;
                this.AddPage(0);
                this.AddBackground(0, 0, 235, 335, 9270);
                this.AddAlphaRegion(10, 10, 215, 315);
                this.AddLabel(70, 15, RedHue, @"Statics Copier");
                this.AddLabel(105, 40, GreenHue, string.Format("{0} Selected", m_Workshop.TilesCount));

                // Add range: B1
                this.AddButton(20, 40, 4005, 4006, 1, GumpButtonType.Reply, 0);
                this.AddLabel(60, 40, LabelHue, @"Add");

                // Range check: 0
                this.AddCheck(20, 70, 2510, 2511, false, 0);
                this.AddLabel(40, 70, LabelHue, @"Z Range:");

                // Min Z: Text 0
                this.AddTextEntry(105, 65, 40, 20, RedHue, 0, m_Workshop.MinZ.ToString());
                this.AddImageTiled(105, 85, 40, 1, 9304);
                this.AddLabel(155, 70, LabelHue, @"to");

                // Max Z: Text 1
                this.AddTextEntry(180, 65, 40, 20, RedHue, 1, m_Workshop.MaxZ.ToString());
                this.AddImageTiled(180, 85, 40, 1, 9304);

                // Add single tile: B2
                this.AddButton(20, 95, 4005, 4006, 2, GumpButtonType.Reply, 0);
                this.AddLabel(60, 95, LabelHue, @"Add a single tile");

                // Clear: B3
                this.AddButton(20, 125, 4005, 4006, 3, GumpButtonType.Reply, 0);
                this.AddLabel(60, 125, LabelHue, @"Clear");
                this.AddLabel(20, 155, GreenHue, @"Placement Control");

                // Place: B4
                this.AddButton(20, 180, 4005, 4006, 4, GumpButtonType.Reply, 0);
                this.AddLabel(60, 180, LabelHue, @"Place");

                // Delete: B5
                this.AddButton(20, 210, 4020, 4021, 5, GumpButtonType.Reply, 0);
                this.AddLabel(60, 210, LabelHue, @"Delete");
                this.AddLabel(25, 240, LabelHue, @"Nudge");
                this.AddImageTiled(95, 260, 40, 1, 9354);

                // Nudge: Text 2
                this.AddTextEntry(95, 240, 40, 20, RedHue, 2, m_Workshop.NudgeAmount.ToString());

                // Nudge Down: B6
                this.AddButton(75, 242, 5602, 5606, 6, GumpButtonType.Reply, 0);

                // Nudge Up: B7
                this.AddButton(140, 242, 5600, 5604, 7, GumpButtonType.Reply, 0);
                this.AddLabel(25, 265, LabelHue, @"Set hue");
                this.AddImageTiled(95, 285, 40, 1, 9354);

                // Hue : Text 3
                this.AddTextEntry(95, 265, 40, 20, RedHue, 3, m_Workshop.HueValue.ToString());

                // Hue: B8
                this.AddButton(145, 265, 4014, 4015, 8, GumpButtonType.Reply, 0);

                // Close: B0
                this.AddButton(20, 295, 4017, 4018, 0, GumpButtonType.Reply, 0);
                this.AddLabel(60, 295, LabelHue, @"Close");
            }

            public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
            {
                #region Collect Data

                m_Workshop.ZRange = info.Switches.Length == 1;

                foreach (TextRelay text in info.TextEntries)
                {
                    switch (text.EntryID)
                    {
                        case 0: // Min Z

                            try { m_Workshop.MinZ = sbyte.Parse(text.Text); }
                            catch { }

                            break;

                        case 1: // Max Z

                            try { m_Workshop.MaxZ = sbyte.Parse(text.Text); }
                            catch { }

                            break;

                        case 2: // Nudge Amount

                            try { m_Workshop.NudgeAmount = byte.Parse(text.Text); }
                            catch { }

                            break;

                        case 3: // Hue

                            try { m_Workshop.HueValue = int.Parse(text.Text); }
                            catch { }

                            break;
                    }
                }

                #endregion

                switch (info.ButtonID)
                {
                    case 1: // Add Range

                        m_Workshop.AddRange();

                        break;

                    case 2: // Add Single

                        m_Workshop.AddSingle();

                        break;

                    case 3: // Clear

                        m_Workshop.Clear();

                        break;

                    case 4: // Build

                        m_Workshop.Build();

                        break;

                    case 5: // Delete

                        m_Workshop.DeleteBuilding();

                        break;

                    case 6: // Nudge Down

                        m_Workshop.Nudge(false);

                        break;

                    case 7: // Nudge Up

                        m_Workshop.Nudge(true);

                        break;

                    case 8: // Hue

                        m_Workshop.Hue();

                        break;
                }

                if (info.ButtonID != 0)
                {
                    m_Workshop.m_User.SendGump(new InternalGump(m_Workshop));
                }
            }

        }

        #endregion
    }
}