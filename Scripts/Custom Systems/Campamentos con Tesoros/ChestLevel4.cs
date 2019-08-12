using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Multis;
using Server.Network;
using Server.ContextMenus;
using Server.Engines.PartySystem;

namespace Server.Items
{
    public class ChestLevel4 : LockableContainer
    {

        private void SetChestAppearance()
        {
 
            bool UseFirstItemId = Utility.RandomBool();

            switch( Utility.RandomList( 0, 1, 2 ) )
            {
                case 0:// Large Crate
                    this.ItemID = ( UseFirstItemId ? 0xe3c : 0xe3d );
                    this.GumpID = 0x44;
                    break;

                case 1:// Wooden Chest
                    this.ItemID = ( UseFirstItemId ? 0xe42 : 0xe43 );
                    this.GumpID = 0x49;
                    break;

                case 2:// Metal Chest
                    this.ItemID = ( UseFirstItemId ? 0x9ab : 0xe7c );
                    this.GumpID = 0x4A;
                    break;

            }
        }

        public override int DefaultGumpID{ get{ return 0x42; } }
        public override int DefaultDropSound{ get{ return 0x42; } }

        public override Rectangle2D Bounds
        {
            get{ return new Rectangle2D( 18, 105, 144, 73 ); }
        }

        [Constructable]
        public ChestLevel4() : base( 0xE41 )
        {
            this.SetChestAppearance();
        }

        public ChestLevel4( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int) 1 ); // version
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }
    }
}