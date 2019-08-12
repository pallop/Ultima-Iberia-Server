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
    public class ChestLevel1 : LockableContainer
    {

        private void SetChestAppearance()
        {
           bool UseFirstItemId = Utility.RandomBool();

            switch( Utility.RandomList( 0, 1, 2, 3 ) )
            {
                case 0:// Large Crate
                    this.ItemID = ( UseFirstItemId ? 0xe3c : 0xe3d );
                    this.GumpID = 0x44;
                    break;

                case 1:// Medium Crate
                    this.ItemID = ( UseFirstItemId ? 0xe3e : 0xe3f );
                    this.GumpID = 0x44;
                    break;

                case 2:// Small Crate
                    this.ItemID = ( UseFirstItemId ? 0x9a9 : 0xe7e );
                    this.GumpID = 0x44;
                    break;

                case 3:// Barrel
                    this.ItemID = ( UseFirstItemId ? 0xe77 : 0xe77 );
                    this.GumpID = 0x3e;
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
        public ChestLevel1() : base( 0xE41 )
        {
            this.SetChestAppearance();
        }

        public ChestLevel1( Serial serial ) : base( serial )
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