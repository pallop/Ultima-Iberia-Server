//
// ** Basic Trap Framework (BTF)
// ** Author: Lichbane
//
//  Extension to PlayerMobile.cs to track the number of active traps
//  a player has in the game.
//
//  Thanks to Soteric for helping come up with a more "DistroSafe" solution.
//
using System;
using System.Text;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    public partial class PlayerMobile : Mobile
    {
        private int m_TrapsActive = 0;

        [CommandProperty(AccessLevel.GameMaster)]
        public int TrapsActive
        {
            get { return m_TrapsActive; }
            set { m_TrapsActive = value; }
        }
    }
}