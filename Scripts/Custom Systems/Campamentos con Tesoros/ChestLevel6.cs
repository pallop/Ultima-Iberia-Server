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
    public class ChestLevel6 : LockableContainer
    {

        private void SetChestAppearance()
        {
 
            bool UseFirstItemId = Utility.RandomBool();

            switch( Utility.RandomList( 0, 1 ) )
            {

                case 0:// Metal Chest
                    this.ItemID = ( UseFirstItemId ? 0x9ab : 0xe7c );
                    this.GumpID = 0x4A;
                    break;

                case 1:// Metal Golden Chest
                    this.ItemID = ( UseFirstItemId ? 0xe40 : 0xe41 );
                    this.GumpID = 0x42;
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
        public ChestLevel6() : base( 0xE41 )
        {
            this.SetChestAppearance();
        }

        public ChestLevel6( Serial serial ) : base( serial )
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