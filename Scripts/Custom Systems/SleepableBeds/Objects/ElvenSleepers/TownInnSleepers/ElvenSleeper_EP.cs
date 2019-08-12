#region Acknowledgements
/// <summary>
/// Script Package: Sleepable Beds | Version: 1.0 | Author: Oak
/// Scripted For RunUO 2.0                          Author: Zardoz
///
/// Written for RunUO 1.0 shard, Sylvan Dreams,  in February 2005.
/// Based largely on work by David on his Sleepable NPCs scripts.
/// Modified for RunUO 2.0, removed shard specific customizations 
/// (wing layers, etc.)
/// </summary>
#endregion Edited By: A.A.R

using System;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using Server.Multis;
using Server.Engines.PartySystem;
using Server.Gumps;
using Server.Misc;
using Server.Guilds;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;

namespace Server.Items
{
    public class ElvenSleeper_EP : BaseAddon, IChopable
    {
        private ElvenSleeper_EP m_Sleeper;
        private Mobile m_Owner;
        private SleepingBodies m_SleepingBodies;
        private Mobile m_Player;
        private Point3D m_Location;
        private Point3D m_PlayerLocation;

        private bool m_Active = false;
        private bool m_Debug = false;
        private bool m_Sleeping = false;

        private int m_wakeup;

        #region CommandProperties

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D Bed { get { return m_Location; } set { m_Location = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Debug { get { return m_Debug; } set { m_Debug = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Active { get { return m_Active; } set { m_Active = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Asleep { get { return m_Sleeping; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Mobile
        {
            get { return m_Player; }
            set
            {
                if (value == null)
                    m_Active = false;
                else m_Active = true; m_Player = value; InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public ElvenSleeper_EP Sleeper { get { return m_Sleeper; } set { } }

        #endregion Edited By: A.A.R

        private void Sleep()
        {
            if (m_Sleeping) return;
        }

        [Constructable]
        public ElvenSleeper_EP()
        {
            Visible = true;
            Name = "Elven Sleeper";

            #region Bed Component Item ID's

            AddComponent(new ElvenSleeper_EPPiece(this, 0x304D), 0, 0, 0);
            AddComponent(new ElvenSleeper_EPPiece(this, 0x304C), 1, 0, 0);
            
            #endregion
        }

        #region DoubleClick Method

        public void DoubleClick(Mobile from)
        {
            Mobile m_Player = from as PlayerMobile;

            if (!m_Player.CanSee(this) || !m_Player.InLOS(this))
            {
                m_Player.LocalOverheadMessage(MessageType.Regular, 0x33, true, "You are too far from the bed!");
                return;
            }

            if (m_Player.CantWalk && !m_Sleeping)
            {
                m_Player.LocalOverheadMessage(MessageType.Regular, 0x33, true, "You are already sleeping somewhere!");
            }
            else
            {
                if (!m_Sleeping)
                {
                    m_Owner = m_Player;

                    m_Player.Hidden = true;
                    m_Player.Squelched = true;
                    m_Player.Frozen = true;
                    m_wakeup = 0;
                    m_Player.CantWalk = true;
                    m_Sleeping = true;
                    m_Player.Blessed = true;

                    m_SleepingBodies = new SleepingBodies(m_Player, false, false);

                    #region Sleeping Body Position

                    Point3D m_Location = new Point3D(this.Location.X + 2, this.Location.Y, this.Location.Z + 9);
                    
                    #endregion

                    m_SleepingBodies.Direction = Direction.East;
                    m_SleepingBodies.MoveToWorld(m_Location, this.Map);
                }
                else
                {

                    if (m_Owner == m_Player)
                    {
                        m_Player.Hidden = false;
                        m_Player.Squelched = false;
                        m_Player.Frozen = false;
                        m_wakeup = 0;
                        m_Player.CantWalk = false;
                        m_Sleeping = false;
                        m_Player.Blessed = false;

                        if (m_SleepingBodies != null)
                            m_SleepingBodies.Delete();
                        m_SleepingBodies = null;

                        switch (Utility.RandomMinMax(1, 3))
                        {
                            case 1:
                                m_Player.LocalOverheadMessage(MessageType.Regular, 0x33, true, "You wake up and feel strong and well rested.");
                                break;

                            case 2:
                                m_Player.LocalOverheadMessage(MessageType.Regular, 0x33, true, "You spring out of bed, ready for another day!");
                                break;

                            case 3:
                                m_Player.LocalOverheadMessage(MessageType.Regular, 0x33, true, "You fall out of bed and crack your knee on the wooden bedframe!");
                                m_Player.Hits = m_Player.Hits - 25;
                                break;
                        }
                    }
                    else
                    {
                        switch (m_wakeup)
                        {
                            case 0:
                                m_Player.LocalOverheadMessage(MessageType.Regular, 0x33, true, "Shhh, don't wake them up. They really need their beauty rest!");
                                m_wakeup = m_wakeup + 1;
                                break;

                            case 1:
                                m_Player.LocalOverheadMessage(MessageType.Regular, 0x33, true, "You really should NOT bother someone that is sleeping. Bad things might happen.");
                                m_wakeup = m_wakeup + 1;
                                break;

                            case 2:
                                m_Player.LocalOverheadMessage(MessageType.Regular, 0x33, true, "You were warned!! Now leave them alone.");
                                m_Player.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.Head);
                                m_Player.PlaySound(0x208);
                                m_Player.Hits = m_Player.Hits - 40;
                                break;
                        }
                    }
                }
            }
        }

        #endregion Edited By: A.A.R

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            string tmp = String.Format("{0}: {1}", this.Name, (m_Player != null ? m_Player.Name : "unassigned"));
            list.Add(tmp);

            if (m_Active)
                list.Add(1060742); // active
            else
                list.Add(1060743); // inactive
        }

        public ElvenSleeper_EP(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);

            writer.Write((Item)m_SleepingBodies);
            writer.Write((Mobile)m_Player);

            writer.Write(m_Active);
            writer.Write(m_Location);
            writer.Write(m_Sleeping);

            writer.Write((Mobile)m_Owner);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            m_SleepingBodies = (SleepingBodies)reader.ReadItem();
            m_Player = reader.ReadMobile();

            m_Active = reader.ReadBool();
            m_Location = reader.ReadPoint3D();
            m_Sleeping = reader.ReadBool();
            m_Owner = reader.ReadMobile();

            m_Debug = false;
        }
    }

    public class ElvenSleeper_EPPiece : AddonComponent
    {
        private ElvenSleeper_EP m_Sleeper;

        public ElvenSleeper_EPPiece(ElvenSleeper_EP sleeper, int itemid)
            : base(itemid)
        {
            m_Sleeper = sleeper;
        }

        public override void OnDoubleClick(Mobile from)
        {
            m_Sleeper.DoubleClick(from);
        }

        public ElvenSleeper_EPPiece(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
            writer.Write((Item)m_Sleeper);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_Sleeper = (ElvenSleeper_EP)reader.ReadItem();
        }
    }
}