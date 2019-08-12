
/*
 *Created by Ashlar, beloved of Morrigan
*/
 
/* 
 * This is by far the most complex script i have yet done.  
 * I have learned alot in the making of this and have a much greater respect for programmers now.
 * 
 * There are Thanks for specific things scattered thru the code, but this thanks goes here at the top:
 * The RunUO Development Team -- 
 *      You have created a wonderful learning tool and have inspired the creativity of thousands.  
 *      Your work has changed the world, by itself and by the effects it has had on the lives of 
 *      all the people who have used RunUO.     Thank you for the effects RunUO has had on me.
*/
 
                                                                                          //All comments other than block comments are offsetso you can see the code.
using System;
using Server;
using Server.Gumps;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Multis;
using Server.Mobiles;
using System.Collections;
using Server.Regions;
using Server.Ashlar;

namespace Server.Ashlar
{
    public class PelopsResurrection : Item
    {
    
        #region Declare Variables
        
        public bool HasCost;

        private int m_FocusTile;                                                        //Which pic in the collection to view in the gump
        public int FocusTile { get { return m_FocusTile; } set { m_FocusTile = value; InvalidateProperties(); } }//Allows us to change the number in m_FocusTile

        private int m_HueNumber;                                                        //Stores the hue number that the player likes from the gump
        public int HueNumber { get { return m_HueNumber; } set { m_HueNumber = value; InvalidateProperties(); } }//Allows us to change the number
        
        private string m_Label;                                                         //Stores the string Label that the player likes from the gump
        public string Label { get { return m_Label; } set { m_Label = value; InvalidateProperties(); } }//Allows us to change the label

        private ArrayList m_StoredTiles;                                                //This is where the itemID's will be stored
        public ArrayList StoredTiles { get { return m_StoredTiles; } }                  //Allows us to add new itemID's
        #endregion

        [Constructable]
        public PelopsResurrection() : base( 0x20EE )
        {                                                                               //The pic is a bird, but it is the closest i could find to a shoulder bone. (see the story of Pelops)
            Name = "Super Deco Tool";
            Weight = 1.0;
            Hue = 1153;
            LootType = LootType.Blessed;                                                //It would suck to lose a collection with thousands of id's!!! 
            m_StoredTiles = new ArrayList();                                            //Initalize the arraylist
            m_StoredTiles.Add( this.ItemID );                                           //The Arraylist can not be empty!  Will cause a crash if it is!
            FocusTile = 0;                                                              //Which pic in the collection to show in the gump. This is here in case they get the item and immediately open the gump.
            HasCost = false;
        }

        public override void OnDoubleClick( Mobile from )
        {
            /*
             * Before i got serialization to work, having a null refrence on the arraylist 
             * was really messing me up...so, there are currently 3 places that initialize 
             * the arraylist besides in serialization...who knows, somoeone might want to 
             * modify this script, so this adds some safety for them.
            */
            if ( m_StoredTiles == null )                                                
            {
                m_StoredTiles = new ArrayList();
                m_StoredTiles.Add( 0x20EE );                                            //The Arraylist can not be empty!  Will cause a crash if it is!
            }

            if ( from.InRange( GetWorldLocation(), 2 ) )                                //should i have this set to check from.Backpack instead?
            {
                from.SendMessage( "Target the Super Deco Tool to open the gump, anything else to add to your collection" );//Let them know what to do...
                from.Target = new AddIDTarget( this );                                  //The player shouldn't have to have the gump open to add id's.
            }
            else
            {
                from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 );       // I can't reach that. //Out of Range message.
            }
        }

        #region Converters
                                                                                        //Thanks for posting ConvertHexToDecimal on RunUO, Ravatar!!
        public static int ConvertHexToDecimal( string hexNumber )
        {                                                                               //called from the OnResponse in the gump
            hexNumber = hexNumber.Trim().ToLower();

            int intStart = hexNumber.IndexOf( "0x" );

            if ( intStart > -1 )
            {
                intStart += 2;
                hexNumber = hexNumber.Substring( intStart, hexNumber.Length - intStart );
            }

            return Int32.Parse( hexNumber, System.Globalization.NumberStyles.AllowHexSpecifier );
        }
                                                                                        //I found ConvertStringToInt at: http://www.eggheadcafe.com/forums/ForumPost.asp?ID=33242&INTID=1 Thank you for posting this, Bharath Reddy 
        public Int32 ConvertStringToInt( string numberstring, Mobile from )
        {                                                                               //called from GetFocusTile and the OnResponse in the gump
            try { return Utility.ToInt32( numberstring ); }
            catch
            {
                from.Say( "Numbers only in Hue and GoTo #" );
                from.SendGump( new PelopsResurrectionGump( from, this ) );
                return 0;
            }
        }
        #endregion

        #region Rares
        public static int[] m_RaresList = new int[]
			{                                                                           //Simply add tileid's to this list and the player will not be able to add them to their collection.
            0x2006, 0x12A5, 0x13D9, 0x13D8, 0x106B,
            0x1858, 0x1E88, 0x1E91, 0xF38, 0xC2C, 0x1363,
            0x13CA, 0xB24, 0x428, 0x10D9, 0xC16, 0x10DA,
            0xE31, 0xE28, 0x1E21, 0x1E24, 0x1E25, 0xE23, 0x9DC,
            0x9DD, 0x9DE, 0x9DF, 0x9E6, 0x9E7, 0x9E8, 0x1027,
            0x1026, 0xFBF, 0xFC1, 0x166E, 0x14F8, 0x1876, 0x1877,
            0x1878, 0x1879, 0xC3C, 0xC3E, 0xC40, 0xC42, 0xFB6, 0xFB8  //when it is empty or commented out, no restrictions are in place on rares
            };
        public bool CheckInRareList( int tileID )
        {
            bool contains = false;

            for ( int i = 0; !contains && i < m_RaresList.Length; ++i )
                contains = ( tileID == m_RaresList[ i ] );

            return contains;
        }
        #endregion

        public void AddIdToPelops( Mobile from, int tileID )
        {                                                                               //this is called from AddIDTarget
            if ( ( m_StoredTiles == null || !m_StoredTiles.Contains( tileID ) ) && m_RaresList.Length >= 0 )           //if the targeted tile is already in your collection...
            {
                if ( m_RaresList.Length >= 0 && CheckInRareList( tileID ) )
                {
                    from.SendMessage( "That is a rare!  You cannot add it to your collection." );
                    return;
                }
                else if ( m_StoredTiles == null )                                            //yes, i know we checked for a null arraylist in doubleclick, so this isn't needed, but let's be sure...null reference's suck!
                {
                    m_StoredTiles = new ArrayList();
                    m_StoredTiles.Add( tileID );                                        //Adds the int tileID to the m_StoredTiles arraylist (automatic upcast to type object)
                    OnStoredTilesAdd( from );                                           //Example of a logical insert point for further functions.  Ok, i didn't know if i wanted to do something else here or not :p
                    m_StoredTiles.Sort();                                               //This puts the tiles in the same order that is found in InsideUO, so the savy player can figure out which tiles they are missing in their collection
                }
                else
                {
                    m_StoredTiles.Add( tileID );                                        //Adds the int tileID to the m_StoredTiles arraylist (automatic upcast to type object)
                    OnStoredTilesAdd( from );                                           //Example of a logical insert point for further functions.  Ok, i didn't know if i wanted to do something else here or not :p
                    m_StoredTiles.Sort();                                               //This puts the tiles in the same order that is found in InsideUO, so the savy player can figure out which tiles they are missing in their collection
                }
            }
            else if ( m_StoredTiles.Contains( tileID ) )                                //We don't want to add multiple tilesto the arraylist!!  There are more than enough already!
            {
                from.SendMessage( "That is already in your collection." );
            }
        }
        public virtual void OnStoredTilesAdd( Mobile from )
        {
            switch ( Utility.Random( 3 ) )
            {
                default:
                case 0: from.SendMessage( "Added to your collection." ); break;                           //I added this as a seperate function to allow easier modifying of this script..logical place to do a new action. 
                case 1: from.SendMessage( "Collection is stored in the same order as in InsideUO" ); break;
                case 2: from.SendMessage( "Gotta catch em all!" ); break;
            }
        }

        public Int32 GetFocusTile( Mobile from )
        {                                                                               //GetFocusTile sets the tile to view in the gump
            if (  m_FocusTile >= m_StoredTiles.Count )                                  //the gump shows how many id's you have in your collection, but the actual picking of the ids start at 0, not 1.
            {
                from.SendMessage( "Your collection list starts at 0, not 1.  The number you chose does not exist." );
                FocusTile = 0;                                                          //sets the FocusTile to match the pic shown in the gump
                return ConvertStringToInt( m_StoredTiles[ 0 ].ToString(), from );       //sends the lowest tile in the collection to the player
            }
            else
                return ConvertStringToInt( m_StoredTiles[ m_FocusTile ].ToString(), from );
            /*
             *Since i am using an ArrayList, and everything in an arraylist is an object, not a int,  
             *to display the pic in the gump, we have to convert the stored object into a string, 
             *and then from the string into an int. blah!  We can then use the int to show the picture.
             *I had read on MSDN that doing this isn't supposed to be possible???  Boggle!!!
            */
        }

        public bool IsStaff( Mobile from )
        {                                                                               //Called from OnResponse in the gump, IsValidUse, and in several targets (code reduction)
            return ( from.AccessLevel >= AccessLevel.GameMaster );                      //If you are not a player
        }
        public bool IsValidUse ( Mobile from )
        {
            
                                                          //Called from a couple targets (code reduction)
            BaseHouse house = BaseHouse.FindHouseAt( from );                            //gets info about the house where you are at
            bool NotInHouse = ( house == null || !house.IsCoOwner( from ));            //If there is no house at your location or if you are not co-owner of the house you are at..you are NotInHouse
            return ( !NotInHouse || IsStaff( from ) );
        }

        #region HasCost functions
        public Type PayType()
        {
            return typeof( Gold );
        }
        public string PayTypeName()
        {
            return "gold.";
        }
        public Int32 NewCost()
        {
            return 300;
        }
        public Int32 ResetIDCost()
        {
            return 150;
        }
        public Int32 LabelCost()
        {
            return 100;
        }
        public Int32 HueCost()
        {
            return 50;
        }
        public void AlertPlayerOfCost( Mobile from, string function, int cost )
        {
            if ( from.AccessLevel < AccessLevel.GameMaster )
            {
                from.SendMessage( "Hey! " + from.Name + "! " + function + " will cost you " + cost + " " + PayTypeName() + " to perform." );
                from.SendMessage( "Hit escape to exit this transaction." );
            }
        }

        public bool TakePayment( Mobile from, int cost )
        {
            if ( this.HasCost )
            {
                Container bp = from.Backpack;
                if ( from.AccessLevel < AccessLevel.GameMaster && bp != null )
                {
                    if ( bp.ConsumeTotal( PayType(), cost ) )
                    {
                        from.PlaySound( 0x32 );
                        return true;
                    }
                    else
                    {
                        from.SendMessage( "Begging thy pardon, but thou can not afford that." );
                        return false;
                    }
                }
                else
                    return true;
            }
            else
                return true;
        }
        #endregion

        public PelopsResurrection( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.WriteEncodedInt( ( int )0 ); // version

            writer.Write( ( bool )HasCost );
            writer.Write( ( int )m_FocusTile );
            writer.Write( ( int )m_HueNumber );
            writer.Write( ( string )m_Label );
                                                                                        //I sweated bullets on the arraylist serialization, gave up a few times and then figured it out :)   
            writer.Write( ( int )m_StoredTiles.Count );                                 //write the number of things in the arraylist first so that the reader knows how many to read
            for ( int i = 0; i < m_StoredTiles.Count; ++i )                             //a loop for every id in the collection
            {
                int it = ( int )m_StoredTiles[ i ];                                     //the int it stands for the value that we are working with on this loop
                writer.Write( ( int )it );                                              //Write that value
            }
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadEncodedInt();

            HasCost = reader.ReadBool();
            m_FocusTile = reader.ReadInt();
            m_HueNumber = reader.ReadInt();
            m_Label = reader.ReadString();
                                                                                        //I can't tell you how many times it serialized and then didn't read successfully.  I gave up serveral times then looked at how serialization works in the first place!
            int count = reader.ReadInt();                                               //read the number of things in the arraylist
            m_StoredTiles = new ArrayList();                                            //create a new arraylist to hold our values
            for ( int i = 0; i < count; ++i )                                           //a loop for every id that is stored
            {
                m_StoredTiles.Add( reader.ReadInt() );                                  //add this loop's stored value to the arraylist
            }
        }
    }
}
namespace Server.Ashlar
{
    public class PelopsResurrectionGump : Gump
    {
        private PelopsResurrection m_Pelops;                                                                    //Declare a variable for our item
        
        public PelopsResurrectionGump( Mobile from, PelopsResurrection pelops) : base( 0, 0 )
        {                                                                                                       //Thanks for the Gump Tutorial Al_i_n  It came in handy for my rusty spots!
            m_Pelops = pelops;                                                                                  //Assign the item the player used to our variable
            this.Closable = true;                                                                               //Go away! go away!  (on rightclick that is)
            this.Disposable = false;                                                                            //Nope, i have no idea what this does either!
            this.Dragable = true;                                                                               //The gump is the size of the paperdoll, but the player might want it elsewhere
            this.Resizable = false;                                                                             //The gump is not a scroll, nope!

            this.AddPage( 0 );                                                                                  //Gotta put our stuff somewhere!

            this.AddBackground( 44, 2, 246, 589, 83 );                                                          //The brown background with the silver frame
            this.AddBackground( 55, 15, 224, 567, 9300 );                                                       //The white parchment background

            this.AddLabel( 90, 20, 0, @"Super Deco Tool." );                                               //Gumptext

            this.AddLabel( 70, 50, 0, @"GoTo" );                                                                //Gumptext
            this.AddBackground( 120, 50, 52, 20, 9350 );                                                        //Input area background
            this.AddTextEntry( 125, 50, 50, 20, 0, 3, "" + m_Pelops.FocusTile );                                //Goto int TextEntry
            this.AddLabel( 170, 50, 0, @" of " + m_Pelops.StoredTiles.Count );                                  //How many in Collecion Gumptext
            this.AddButton( 100, 52, 2224, 2224, ( int )Buttons.Goto, GumpButtonType.Reply, 0 );                //Button sets FocusTile to be the int TextEntry

            this.AddButton( 255, 52, 2224, 2224, ( int )Buttons.Next, GumpButtonType.Reply, 0 );                //Changes pic to previous tile, wraps around 0 no problem :D
            this.AddButton( 235, 52, 2223, 2223, ( int )Buttons.Back, GumpButtonType.Reply, 0 );                //Changes pic to next tile, wraps around end of arraylist no problem ;P

            this.AddLabel( 180, 225, 0, @"West - North" );                                                      //Gumptext
            this.AddButton( 219, 240, 4501, 4501, ( int )Buttons.North, GumpButtonType.Reply, 0 );              //(targeted) Moves a PelopsItem North
            this.AddButton( 173, 240, 4507, 4507, ( int )Buttons.West, GumpButtonType.Reply, 0 );               //(targeted) Moves a PelopsItem West
            this.AddLabel( 180, 285, 0, @"South - East" );                                                      //Gumptext
            this.AddButton( 173, 301, 4505, 4505, ( int )Buttons.South, GumpButtonType.Reply, 0 );              //(targeted) Moves a PelopsItem South
            this.AddButton( 219, 301, 4503, 4503, ( int )Buttons.East, GumpButtonType.Reply, 0 );               //(targeted) Moves a PelopsItem East
            this.AddLabel( 178, 365, 0, @"Raise / Lower" );                                                     //Gumptext
            this.AddButton( 173, 383, 4500, 4500, ( int )Buttons.IncZ, GumpButtonType.Reply, 0 );               //(targeted) Moves a PelopsItem Up
            this.AddButton( 219, 383, 4504, 4504, ( int )Buttons.DecZ, GumpButtonType.Reply, 0 );               //(targeted) Moves a PelopsItem Down

            this.AddButton( 64, 465, 5534, 5534, ( int )Buttons.New, GumpButtonType.Reply, 0 );                 //(targeted)Creates a new pelops item with hue and label if they are checked.
            this.AddButton( 134, 466, 5531, 5531, ( int )Buttons.Delete, GumpButtonType.Reply, 0 );             //(targeted) Deletes a PelopsItem
            this.AddButton( 205, 465, 5526, 5526, ( int )Buttons.Help, GumpButtonType.Reply, 0 );               //Sends the PelopsResurrectionHelpGump

            this.AddLabel( 130, 498, 0, @"Reset ID" );                                                          //Gumptext
            this.AddButton( 190, 498, 2331, 2331, ( int )Buttons.ResetIDButton, GumpButtonType.Reply, 0 );      //Changes the tileID of an existing PelopsItem

            this.AddLabel( 130, 524, 0, @"Hue" );                                                               //Gumptext
            this.AddBackground( 155, 524, 45, 20, 9350 );                                                       //Input area background
            this.AddTextEntry( 160, 524, 35, 20, 0, 4, "" + m_Pelops.HueNumber );                               //Hue int TextEntry
            this.AddCheck( 110, 524, 2510, 2511, true, 1 );                                                    //New item is hued the Hue int TextEntry if checked
            this.AddButton( 205, 524, 2331, 2331, ( int )Buttons.HueTargetButton, GumpButtonType.Reply, 0 );    //(targeted) set Hue to be the Hue int TextEntry.

            this.AddLabel( 85, 552, 0, @"Label" );                                                              //GumpText
            this.AddBackground( 122, 552, 119, 20, 9350 );                                                      //Input area background
            this.AddTextEntry( 127, 552, 117, 20, 0, 5, "" + m_Pelops.Label );                                  //Label string TextEntry
            this.AddCheck( 63, 552, 2510, 2511, false, 2 );                                                     //New item is Named the Label string TextEntry?
            this.AddButton( 253, 552, 2331, 2331, ( int )Buttons.LabelTargetButton, GumpButtonType.Reply, 0 );  //(targeted) set Name to be the Label string TextEntry.

            AddPage( 1 );                                                                                       //Ah, no real reason, The pic took the most work to get right, so it gets a page of it's own :p
            this.AddItem( 70, 75, m_Pelops.GetFocusTile( from ), m_Pelops.HueNumber );                                              //Picture of a tile from StoredTiles, FocusTile acts as indexof
        }

        public enum Buttons
        {                                                                                                       //If you use a string name for the button instead of an int you have to declare the names of the buttons in the gump here.
            Exit,                                                                                               //Not used in the gump or in the OnResponse, so it seems to be required for you to rightclick close.
            Help,
            North,
            South,
            East,
            West,
            IncZ,
            DecZ,
            Goto,
            Next,
            Back,
            New,
            Delete,
            ResetIDButton,
            HueTargetButton,
            LabelTargetButton,
        }
        public override void OnResponse( NetState state, RelayInfo info )
        {
            Mobile from = state.Mobile;                                                                 //The mobile who clicked the gump

            bool HueNew = info.IsSwitched( 1 );                                                         //assigns true or false about if checkbox 1 appears to be checked on the gump
            bool LabelNew = info.IsSwitched( 2 );                                                       //assigns true or false about if checkbox 2 appears to be checked on the gump

            TextRelay entry1 = info.GetTextEntry( 3 );                                                  //gets the GoTo textbox input
            int GoTo = m_Pelops.ConvertStringToInt( entry1 == null ? "" : entry1.Text.Trim(), from );   //assigns the int variable GoTo to be = string value of the GoTo inputbox converted into int
            m_Pelops.FocusTile = GoTo;                                                                  //assigns the int variable Offset on the PelopsResurrection item to be the same as the converted int from the inputbox
            if ( m_Pelops.FocusTile >= m_Pelops.StoredTiles.Count )                                     //checks for arraylist out of range - another layer of protection against crashes!
            {
                m_Pelops.FocusTile = 0;                                                                 //They wanted to go go out of the collection, send them the first tile
                from.CloseGump( typeof( PelopsResurrectionGump ) );                                     //invalidates the gump they wanted to get because it has a bad arraylist index
            }

            TextRelay entry2 = info.GetTextEntry( 4 );                                                  //gets the hueNumber textbox input
            int hueNumber = m_Pelops.ConvertStringToInt( entry2 == null ? "" : entry2.Text.Trim(), from );//assigns the int variable hueNumber to be = string value of the hueNumber inputbox converted into int
            m_Pelops.HueNumber = hueNumber;                                                             //Changes (stores) the huenumber on the PelopsResurrection item so that the gump can use it when it re-opens

            TextRelay entry3 = info.GetTextEntry( 5 );                                                  //gets the tileLabel textbox input
            string tileLabel = ( entry3 == null ? "" : entry3.Text.Trim() );                            //assigns the string variable tileLabel to be = string value of the tileLabel inputbox
            m_Pelops.Label = tileLabel;                                                                 //Changes (stores) the tileLabel on the PelopsResurrection item so that the gump can use it when it re-opens
                
            switch ( info.ButtonID )                                                                    //anytime a button is pressed on a gump, the gump goes away and does whatever it's button was assigned to do...in here!
            {   //This list is collapsed to conserve space.  Just hit enter on each code segment to expand it.
                case ( int )Buttons.Next:               if ( m_Pelops.FocusTile + 1 > m_Pelops.StoredTiles.Count - 1 ) { m_Pelops.FocusTile = 0; } else m_Pelops.FocusTile = m_Pelops.FocusTile + 1; from.SendGump( new PelopsResurrectionGump( from, m_Pelops) ); break;
                case ( int )Buttons.Back:               if ( m_Pelops.FocusTile - 1 < 0 ) { m_Pelops.FocusTile = m_Pelops.StoredTiles.Count - 1; } else m_Pelops.FocusTile = m_Pelops.FocusTile - 1; from.SendGump( new PelopsResurrectionGump( from, m_Pelops) ); break;
                case ( int )Buttons.Goto:               from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) ); break;
                case ( int )Buttons.LabelTargetButton:  if ( m_Pelops.HasCost ) { m_Pelops.AlertPlayerOfCost( from, "Apply Label", m_Pelops.LabelCost() ); }  from.Target = new LabelTarget( m_Pelops, tileLabel ); break;
                case ( int )Buttons.HueTargetButton:    if ( m_Pelops.HasCost ) { m_Pelops.AlertPlayerOfCost( from, "Apply Label", m_Pelops.HueCost() ) ; } from.Target = new HueTarget( m_Pelops, hueNumber ); break;
                case ( int )Buttons.ResetIDButton:      if ( m_Pelops.HasCost ) { m_Pelops.AlertPlayerOfCost( from, "Reset ID", m_Pelops.ResetIDCost() ); } from.Target = new ResetIDTarget( m_Pelops, HueNew, LabelNew, hueNumber, tileLabel, m_Pelops.GetFocusTile( from ) ); break;
                case ( int )Buttons.New:                if ( m_Pelops.HasCost ) { m_Pelops.AlertPlayerOfCost( from, "Create New", m_Pelops.NewCost() ); } from.Target = new NewTarget( m_Pelops, HueNew, LabelNew, hueNumber, tileLabel, m_Pelops.GetFocusTile( from ) ); break;
                case ( int )Buttons.Help:               from.SendGump( new PelopsResurrectionHelpGump( from ) ); if ( m_Pelops.HasCost) { from.SendGump( new PelopsResurrectionCostGump( from, m_Pelops) ); } from.SendGump( new PelopsResurrectionGump( from, m_Pelops) ); break;
                case ( int )Buttons.Delete:             from.Target = new DeleteTarget( m_Pelops, m_Pelops.GetFocusTile( from ) ); break;
                case ( int )Buttons.East:               from.Target = new EastTarget( m_Pelops ); break;
                case ( int )Buttons.South:              from.Target = new SouthTarget( m_Pelops ); break;
                case ( int )Buttons.North:              from.Target = new NorthTarget( m_Pelops ); break;
                case ( int )Buttons.West:               from.Target = new WestTarget( m_Pelops ); break;
                case ( int )Buttons.DecZ:               from.Target = new DecZTarget( m_Pelops ); break;
                case ( int )Buttons.IncZ:               from.Target = new IncZTarget( m_Pelops ); break;
            }
        }
    }
}
namespace Server.Regions//Ashlar
{
    //Comments in NewTarget
    public class LabelTarget : Target
    {
        Item pi = null;
        private PelopsResurrection m_Pelops;
        string m_tileLabel;

        public LabelTarget( PelopsResurrection pelops, string tileLabel )
            : base( 4, false, TargetFlags.None )
        {
            m_Pelops = pelops;
            m_tileLabel = tileLabel;
        }

        protected override void OnTarget( Mobile from, object targeted )
        {
            PlayerMobile pm = (PlayerMobile)from;                                                              //declare a variable for the map to create the item. --currently set for from.Map, this is just added for versitility, modifications to this script might want to have offsets or other maps and such.
            //if (!m_Pelops.IsValidUse( from ) && !(pm.City != null && pm.City.Mayor == pm && PlayerGovernmentSystem.IsAtCity( from ) || pm.City != null && pm.City.AssistMayor == pm && PlayerGovernmentSystem.IsAtCity( from )))
            if ( !m_Pelops.IsValidUse( from ) )                                             //needed to avoid the player starting the action in their home, and then running to somewhere else to do the action.
            {
                from.SendMessage( "You must be in your house or the city your mayor or assistant mayor of." );
                return;                                                                     //It was not a valid use, so don't do anything else in the this target system
            }
            else if ( targeted is PelopsItem )
            {
                if ( m_Pelops.HasCost )
                {
                    if ( !m_Pelops.TakePayment( from, m_Pelops.LabelCost() ) )
                    {
                        from.CloseGump( typeof( PelopsResurrectionGump ) );
                        from.SendGump( new PelopsResurrectionGump( from, m_Pelops) );
                        return;
                    }
                }
                pi = ( PelopsItem )targeted;
                pi.Name = m_tileLabel;
                from.SendMessage( "Renamed." );
                from.Target = new LabelTarget( m_Pelops, m_tileLabel );
            }
            else if ( targeted is PelopsResurrection )
            {
                if ( m_Pelops.HasCost )
                {
                    m_Pelops.TakePayment( from, m_Pelops.LabelCost() );
                }
                pi = ( PelopsResurrection )targeted;
                pi.Name = m_tileLabel;
                from.SendMessage( "Renamed." );
                from.Target = new LabelTarget( m_Pelops, m_tileLabel );
            }
            else
            {
                from.SendMessage( "You may only rename items that were created by the Super Deco Tool." );
            }
            from.CloseGump( typeof( PelopsResurrectionGump ) );
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
        }
    }
    public class HueTarget : Target
    {
        Item pi = null;
        private PelopsResurrection m_Pelops;
        int m_hueNumber;

        public HueTarget( PelopsResurrection pelops, int hueNumber )
            : base( 4, false, TargetFlags.None )
        {
            m_Pelops = pelops;
            m_hueNumber = hueNumber;
        }

        protected override void OnTarget( Mobile from, object targeted )
        {
            PlayerMobile pm = (PlayerMobile)from;                                                              //declare a variable for the map to create the item. --currently set for from.Map, this is just added for versitility, modifications to this script might want to have offsets or other maps and such.
            if (!m_Pelops.IsValidUse( from ) && !(pm.City != null && pm.City.Mayor == pm && PlayerGovernmentSystem.IsAtCity( from ) || pm.City != null && pm.City.AssistMayor == pm && PlayerGovernmentSystem.IsAtCity( from )))
            //if ( !m_Pelops.IsValidUse( from ) )                                             //needed to avoid the player starting the action in their home, and then running to somewhere else to do the action.
            {
                from.SendMessage( "You must be in your house or the city your mayor or assistant mayor of." );
                return;                                                                     //It was not a valid use, so don't do anything else in the this target system
            }
            else if ( targeted is PelopsItem )
            {
                if ( m_Pelops.HasCost )
                {
                    if ( !m_Pelops.TakePayment( from, m_Pelops.HueCost() ) )
                    {
                        from.CloseGump( typeof( PelopsResurrectionGump ) );
                        from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
                        return;
                    }
                }
                pi = ( PelopsItem )targeted;
                pi.Hue = m_hueNumber;
                from.SendMessage( "Re-hue another?." );
                from.Target = new HueTarget( m_Pelops, m_hueNumber );
            }
            else if ( targeted is PelopsResurrection )
            {
                if ( m_Pelops.HasCost )
                {
                    m_Pelops.TakePayment( from, m_Pelops.HueCost() );
                }
                pi = ( PelopsResurrection )targeted;
                pi.Hue = m_hueNumber;
                from.SendMessage( "Re-hue another?." );
                from.Target = new HueTarget( m_Pelops, m_hueNumber );
            }
            else
            {
                from.SendMessage( "You may only re-hue items that were created by Pelop's Resurrection." );
            }
            from.CloseGump( typeof( PelopsResurrectionGump ) );
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
        }
    }
    public class ResetIDTarget : Target
    {
        private PelopsResurrection m_Pelops;
        bool m_HueNew = false;
        bool m_LabelNew = false;
        int m_hueNumber;
        string m_tileLabel;
        int m_ft;

        public ResetIDTarget( PelopsResurrection pelops, bool hueNew, bool labelNew, int hueNumber, string tileLabel, int ft )
            : base( 4, false, TargetFlags.None )
        {
            m_Pelops = pelops;
            m_HueNew = hueNew;
            m_LabelNew = labelNew;
            m_hueNumber = hueNumber;
            m_tileLabel = tileLabel;
            m_ft = ft;
        }

        protected override void OnTargetOutOfRange( Mobile from, object targeted )
        {
            base.OnTargetOutOfRange( from, targeted );
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
        }

        protected override void OnTarget( Mobile from, object targeted )
        {
            PlayerMobile pm = (PlayerMobile)from;                                                              //declare a variable for the map to create the item. --currently set for from.Map, this is just added for versitility, modifications to this script might want to have offsets or other maps and such.
            if (!m_Pelops.IsValidUse( from ) && !(pm.City != null && pm.City.Mayor == pm && PlayerGovernmentSystem.IsAtCity( from ) || pm.City != null && pm.City.AssistMayor == pm && PlayerGovernmentSystem.IsAtCity( from )))
            //if ( !m_Pelops.IsValidUse( from ) )                                             //needed to avoid the player starting the action in their home, and then running to somewhere else to do the action.
            {
                from.SendMessage( "You must be in your house or the city your mayor or assistant mayor of." );
                return;                                                                     //It was not a valid use, so don't do anything else in the this target system
            }
            else if ( targeted is PelopsItem )
            {
                if ( m_Pelops.HasCost )
                {
                    if ( !m_Pelops.TakePayment( from, m_Pelops.ResetIDCost() ) )
                    {
                        from.CloseGump( typeof( PelopsResurrectionGump ) );
                        from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
                        return;
                    }
                }
                PelopsItem pi = ( PelopsItem )targeted;
                pi.ItemID = m_ft;
                if ( m_HueNew )
                    pi.Hue = m_hueNumber;
                if ( m_LabelNew )
                    pi.Name = m_tileLabel;
                from.SendMessage( "Changed." );
            }
            else
            {
                from.SendMessage( "Changed." );
            }
            from.CloseGump( typeof( PelopsResurrectionGump ) );
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
        }    
    }
    public class NewTarget : Target
    {                                                                                       //Declare the variables we are importing
        private PelopsResurrection m_Pelops;
        bool m_HueNew = false;
        bool m_LabelNew = false;
        int m_hueNumber;
        string m_tileLabel;
        int m_ft;

        public NewTarget( PelopsResurrection pelops, bool hueNew, bool labelNew, int hueNumber, string tileLabel, int ft )
            : base( 4, false, TargetFlags.None )
        {
            m_Pelops = pelops;                                                              //The item they targeted after the doubleclick that gave them the gump
            m_HueNew = hueNew;                                                              //A bool set in OnResponse if they checked the hue new box
            m_LabelNew = labelNew;                                                          //A bool set in OnResponse if they checked the label new box
            m_hueNumber = hueNumber;                                                        //The hue number in the gump at the time they pressed a button
            m_tileLabel = tileLabel;                                                        //the label textbox contents at the time they pressed a button
            m_ft = ft;
        }

        protected override void OnTargetOutOfRange( Mobile from, object targeted )
        {
            base.OnTargetOutOfRange( from, targeted );                                      //The range for most target functions in pelops is 4 tiles, so the player can't mess up their neighbors houses, but we want to give them a new gump it they go out of range.
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
        }
         
        protected override void OnTarget( Mobile from, object targeted )
        {
            PelopsItem pi = new PelopsItem();                                               //all items created are a PelopsItem (PelopsItem is just a placeholder item - it does nothing.)
            pi.ItemID = m_ft;                                                               //set the itemid to be the same as the focustile
            if ( m_HueNew )                                                                 //if the checkbox for hue new is selected
                pi.Hue = m_hueNumber;                                                       //set the hue to be what was chosen in the gump
            if ( m_LabelNew )                                                               //if the checkbox for label new is selected
                pi.Name = m_tileLabel;                                                      //set the Name of the item to be what was chosen in the gump

            Point3D loc;                                                                    //declare variable for the point3d where we will create the item
            Map map;
            PlayerMobile pm = (PlayerMobile)from;
            CityManagementStone city = pm.City;
            Region cityreg = Region.Find( from.Location, from.Map );
                                                                  //declare a variable for the map to create the item. --currently set for from.Map, this is just added for versitility, modifications to this script might want to have offsets or other maps and such.
            if (!m_Pelops.IsValidUse( from ) && !(pm.City != null && pm.City.Mayor == pm && PlayerGovernmentSystem.IsAtCity( from ) || pm.City != null && pm.City.AssistMayor == pm && PlayerGovernmentSystem.IsAtCity( from )))
            //if ( !m_Pelops.IsValidUse( from ) )                                             //needed to avoid the player starting the action in their home, and then running to somewhere else to do the action.
            {
                from.SendMessage( "You must be in your house or the city your mayor or assistant mayor of." );
                return;                                                                     //It was not a valid use, so don't do anything else in the this target system
            }

            else if ( targeted is LandTarget )                                              //I could have it create the item where ever the target appears, but this allows us to modify what happens if we want to....ie, a mobile might scream or something
            {
                if ( m_Pelops.HasCost )                                                     //optional pay-per-use system...currency sink
                {
                    if ( !m_Pelops.TakePayment( from, m_Pelops.NewCost() ) )                //This acts as both taking their currency and a bool check if they had the funds needed
                    {
                        from.CloseGump( typeof( PelopsResurrectionGump ) );                 //They didnt have the funds, need to send a gump, first close one if there is one 
                        from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );      //send a new gump
                        return;                                                             //the player did not have the required funds, so don't do anything else in the this target system
                    }
                }
                loc = ( ( LandTarget )targeted ).Location;                                  //the location that they targeted
                map = from.Map;                                                             //the map we want to add the pelops item to

                BaseHouse house = BaseHouse.FindHouseAt( from );                              //grabs the house they are at-if any
                if ( house != null && from.AccessLevel < AccessLevel.GameMaster )                            //only needed because a staff would succeed the check above, but cause problems here
                {
                    pi.MoveToWorld( loc, map );
                    house.LockDown( from, pi );
                    pi.Movable = false;                                      //move the new pelopsitem with whatever settings chosen to the location and map specified
                         from.SendMessage( "Create another?." );                                         //tell them why they are getting a new target option.
            from.Target = new NewTarget( m_Pelops, m_HueNew, m_LabelNew, m_hueNumber, m_tileLabel, m_ft );//give the player a new target to add the same thing in another place if they want to.
            from.CloseGump( typeof( PelopsResurrectionGump ) );                             //going to re-send the gump, but we want to close it first in case they already have one open.
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );                                       //a player has to be in their house or they would not make the check above, so this locks the item down. Can be released with "i wish to release this"
                }
                else if ( cityreg != null && city != null )
                {
                        ArrayList decore = pm.City.isLockedDown;
                        
                        if ( pm.City.CurrentDecore == pm.City.MaxDecore )
                        {
                             from.SendMessage( "You cannot secure anymore items in this city." );
                             return;
                        }
                        else
                        {
                                        if ( decore == null )
					{
						pm.City.isLockedDown = new ArrayList();
						decore = pm.City.isLockedDown;
					}

                                        pi.MoveToWorld( loc, map );                                              //move the new pelopsitem with whatever settings chosen to the location and map specified
					pi.Movable = false;
                                        decore.Add( pi );
					pm.City.CurrentDecore += 1;
					from.SendMessage( "You secure the item." );
     from.SendMessage( "Create another?." );                                         //tell them why they are getting a new target option.
            from.Target = new NewTarget( m_Pelops, m_HueNew, m_LabelNew, m_hueNumber, m_tileLabel, m_ft );//give the player a new target to add the same thing in another place if they want to.
            from.CloseGump( typeof( PelopsResurrectionGump ) );                             //going to re-send the gump, but we want to close it first in case they already have one open.
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
			}
				    	//return;
                }
                else
                    pi.MoveToWorld( loc, map );                                              //move the new pelopsitem with whatever settings chosen to the location and map specified
                    pi.Movable = false;                                                      //the player must be staff, so make the item not decay by setting movable = false.

            }

            else if ( targeted is StaticTarget )
            {                                                                               //Same as above
                if ( m_Pelops.HasCost )
                {
                    if ( !m_Pelops.TakePayment( from, m_Pelops.NewCost() ) )
                        return;
                }
                loc = ( ( StaticTarget )targeted ).Location;
                map = from.Map;
                
                BaseHouse house = BaseHouse.FindHouseAt( from );
                if ( house != null && from.AccessLevel < AccessLevel.GameMaster )
                {
                    pi.MoveToWorld( loc, map );
                    house.LockDown( from, pi );
                    pi.Movable = false;                                      //move the new pelopsitem with whatever settings chosen to the location and map specified
                     from.SendMessage( "Create another?" );                                         //tell them why they are getting a new target option.
            from.Target = new NewTarget( m_Pelops, m_HueNew, m_LabelNew, m_hueNumber, m_tileLabel, m_ft );//give the player a new target to add the same thing in another place if they want to.
            from.CloseGump( typeof( PelopsResurrectionGump ) );                             //going to re-send the gump, but we want to close it first in case they already have one open.
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
                }
                else if ( cityreg != null && city != null )
                {
                        ArrayList decore = pm.City.isLockedDown;

                        if ( pm.City.CurrentDecore == pm.City.MaxDecore )
                        {
                             from.SendMessage( "You cannot secure anymore items in this city." );
                             return;
                        }
                        else
                        {
                                        if ( decore == null )
					{
						pm.City.isLockedDown = new ArrayList();
						decore = pm.City.isLockedDown;
					}

                                        pi.MoveToWorld( loc, map );                                              //move the new pelopsitem with whatever settings chosen to the location and map specified
					pi.Movable = false;
                                        decore.Add( pi );
					pm.City.CurrentDecore += 1;
					from.SendMessage( "You secure the item." );
     from.SendMessage( "Create another?" );                                         //tell them why they are getting a new target option.
            from.Target = new NewTarget( m_Pelops, m_HueNew, m_LabelNew, m_hueNumber, m_tileLabel, m_ft );//give the player a new target to add the same thing in another place if they want to.
            from.CloseGump( typeof( PelopsResurrectionGump ) );                             //going to re-send the gump, but we want to close it first in case they already have one open.
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
			}
				    	//return;
                }
                else
                    pi.MoveToWorld( loc, map );
                    pi.Movable = false;
            }

            else if ( targeted is Mobile )
            {                                                                               //Same as above
                if ( m_Pelops.HasCost )
                {
                    if ( !m_Pelops.TakePayment( from, m_Pelops.NewCost() ) )
                        return;
                }
                
                loc = ( ( Mobile )targeted ).Location;
                map = ( ( Mobile )targeted ).Map;
                                
                BaseHouse house = BaseHouse.FindHouseAt( from );
                if ( house != null && from.AccessLevel < AccessLevel.GameMaster )
                {
                    pi.MoveToWorld( loc, map );
                    house.LockDown( from, pi );
                    pi.Movable = false;                      //move the new pelopsitem with whatever settings chosen to the location and map specified
                    from.SendMessage( "Create another?" );                                         //tell them why they are getting a new target option.
            from.Target = new NewTarget( m_Pelops, m_HueNew, m_LabelNew, m_hueNumber, m_tileLabel, m_ft );//give the player a new target to add the same thing in another place if they want to.
            from.CloseGump( typeof( PelopsResurrectionGump ) );                             //going to re-send the gump, but we want to close it first in case they already have one open.
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
                }
                else if ( cityreg != null && city != null )
                {

                        ArrayList decore = pm.City.isLockedDown;
                        
                        if ( pm.City.CurrentDecore == pm.City.MaxDecore )
                        {
                             from.SendMessage( "You cannot secure anymore items in this city." );
                             return;
                        }
                        else
                        {
                                        if ( decore == null )
					{
						pm.City.isLockedDown = new ArrayList();
						decore = pm.City.isLockedDown;
					}

                                        pi.MoveToWorld( loc, map );                                              //move the new pelopsitem with whatever settings chosen to the location and map specified
					pi.Movable = false;
                                        decore.Add( pi );
					pm.City.CurrentDecore += 1;
					from.SendMessage( "You secure the item." );
     from.SendMessage( "Create another?" );                                         //tell them why they are getting a new target option.
            from.Target = new NewTarget( m_Pelops, m_HueNew, m_LabelNew, m_hueNumber, m_tileLabel, m_ft );//give the player a new target to add the same thing in another place if they want to.
            from.CloseGump( typeof( PelopsResurrectionGump ) );                             //going to re-send the gump, but we want to close it first in case they already have one open.
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
			}
				    	//return;
                }
                else
                    pi.MoveToWorld( loc, map );
                    pi.Movable = false;
            }

            else if ( targeted is PelopsResurrection )
            {
                from.SendMessage( "Certain arts do not fare well in a backpack...sorry!" );//some tiles disappear if they are in a players backpack...Check in-game the tiles you allow. (comon sense, but you know players) 
                return;
            }

            else if ( targeted is Item )
            {                                                                               //Same as above
                if ( m_Pelops.HasCost )
                {
                    if ( !m_Pelops.TakePayment( from, m_Pelops.NewCost() ) )
                        return;
                }

                Item item = ( Item )targeted;                                               //better to define which item we mean for the GetWorldLocation() on the next line...
                loc = item.GetWorldLocation();
                map = item.Map;
                //pi.MoveToWorld( loc, map );

                int height = item.ItemData.Height;
                if ( pi.Z + height + 1 > from.Z + 20 || pi.Z > 90 )
                {
                    from.SendMessage( "You cannot reach that high." );
                    pi.Delete();
                }
                else
                    pi.Location = new Point3D( pi.Location, pi.Z + height + 1 );

                BaseHouse house = BaseHouse.FindHouseAt( from );
                if ( house != null && from.AccessLevel < AccessLevel.GameMaster )
                {
                   pi.MoveToWorld( loc, map );
                   house.LockDown( from, pi );
                   pi.Movable = false;                                      //move the new pelopsitem with whatever settings chosen to the location and map specified
                   from.SendMessage( "Create another?" );                                         //tell them why they are getting a new target option.
                   from.Target = new NewTarget( m_Pelops, m_HueNew, m_LabelNew, m_hueNumber, m_tileLabel, m_ft );//give the player a new target to add the same thing in another place if they want to.
                   from.CloseGump( typeof( PelopsResurrectionGump ) );                             //going to re-send the gump, but we want to close it first in case they already have one open.
                   from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
                }
                else if ( cityreg != null && city != null )
                {

                        ArrayList decore = pm.City.isLockedDown;

                        if ( pm.City.CurrentDecore == pm.City.MaxDecore )
                        {
                             from.SendMessage( "You cannot secure anymore items in this city." );
                             return;
                        }
                        else
                        {
                                        if ( decore == null )
					{
						pm.City.isLockedDown = new ArrayList();
						decore = pm.City.isLockedDown;
					}

                                        pi.MoveToWorld( loc, map );                                              //move the new pelopsitem with whatever settings chosen to the location and map specified
					pi.Movable = false;
                                        decore.Add( pi );
					pm.City.CurrentDecore += 1;
					from.SendMessage( "You secure the item." );
            from.SendMessage( "Create another?" );                                         //tell them why they are getting a new target option.
            from.Target = new NewTarget( m_Pelops, m_HueNew, m_LabelNew, m_hueNumber, m_tileLabel, m_ft );//give the player a new target to add the same thing in another place if they want to.
            from.CloseGump( typeof( PelopsResurrectionGump ) );                             //going to re-send the gump, but we want to close it first in case they already have one open.
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
			}
				    	//return;
                }
                else
                    pi.MoveToWorld( loc, map );
                    pi.Movable = false;
            }
            else
                return;                                                                     //I think i covered the possibilitys, but just in case i missed somthing

            from.SendMessage( "Create another?." );                                         //tell them why they are getting a new target option.
            from.Target = new NewTarget( m_Pelops, m_HueNew, m_LabelNew, m_hueNumber, m_tileLabel, m_ft );//give the player a new target to add the same thing in another place if they want to.
            from.CloseGump( typeof( PelopsResurrectionGump ) );                             //going to re-send the gump, but we want to close it first in case they already have one open.
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );                  //re-send the gump
        }
    }
    public class DeleteTarget : Target
    {    
        Item pi = null;
        Item pr = null;
        private PelopsResurrection m_Pelops;
        int m_ft;

        public DeleteTarget( PelopsResurrection pelops, int ft )
            : base( 4, false, TargetFlags.None )
        {
            m_Pelops = pelops;
            m_ft = ft;
        }

        protected override void OnTargetOutOfRange( Mobile from, object targeted )
        {
            base.OnTargetOutOfRange( from, targeted );
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
        }

        protected override void OnTarget( Mobile from, object targeted )
        {
            PlayerMobile pm = (PlayerMobile)from;
            CityManagementStone city = pm.City;
            Region cityreg = Region.Find( from.Location, from.Map );   //declare a variable for the map to create the item. --currently set for from.Map, this is just added for versitility, modifications to this script might want to have offsets or other maps and such.
            if (!m_Pelops.IsValidUse( from ) && !(pm.City != null && pm.City.Mayor == pm && PlayerGovernmentSystem.IsAtCity( from ) || pm.City != null && pm.City.AssistMayor == pm && PlayerGovernmentSystem.IsAtCity( from )))
            //if ( !m_Pelops.IsValidUse( from ) )                                             //needed to avoid the player starting the action in their home, and then running to somewhere else to do the action.
            {
                from.SendMessage( "You must be in your house or the city your mayor or assistant mayor of." );
                return;                                                                     //It was not a valid use, so don't do anything else in the this target system
            }
            else if ( targeted is PelopsItem )
            {
                        pi = ( PelopsItem )targeted;
                        pi.Delete();
                        from.SendMessage( "Remove another?." );
                        from.Target = new DeleteTarget( m_Pelops, m_ft );
            }
            else if ( targeted is PelopsResurrection )
            {
                if ( m_Pelops.StoredTiles.Count > 1 )
                {
                    if ( m_Pelops.FocusTile == 0 )
                    {
                        pr = ( PelopsResurrection )targeted;
                        m_Pelops.StoredTiles.Remove( m_Pelops.GetFocusTile( from ) );
                        m_Pelops.StoredTiles.Sort();
                    }
                    else
                    {
                        pr = ( PelopsResurrection )targeted;
                        m_Pelops.StoredTiles.Remove( m_Pelops.GetFocusTile( from ) );
                        m_Pelops.StoredTiles.Sort();
                        m_Pelops.FocusTile = m_Pelops.FocusTile - 1;
                    }
                }
                else
                {
                    from.SendMessage( "You can not remove the last tile from your collection." );
                }
            }
            else
            {
                from.SendMessage( "You may only delete items that were created by Pelop's Resurrection." );
            }
            from.CloseGump( typeof( PelopsResurrectionGump ) );
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
        }
    }
    public class EastTarget : Target
    {
        Item pi = null;
        Point3D loc;
        private PelopsResurrection m_Pelops;

        public EastTarget( PelopsResurrection pelops )
            : base( 4, false, TargetFlags.None )
        {
            m_Pelops = pelops;
        }

        protected override void OnTarget( Mobile from, object targeted )
        {
            PlayerMobile pm = (PlayerMobile)from;                                                              //declare a variable for the map to create the item. --currently set for from.Map, this is just added for versitility, modifications to this script might want to have offsets or other maps and such.
            if (!m_Pelops.IsValidUse( from ) && !(pm.City != null && pm.City.Mayor == pm && PlayerGovernmentSystem.IsAtCity( from ) || pm.City != null && pm.City.AssistMayor == pm && PlayerGovernmentSystem.IsAtCity( from )))
            //if ( !m_Pelops.IsValidUse( from ) )                                             //needed to avoid the player starting the action in their home, and then running to somewhere else to do the action.
            {
                from.SendMessage( "You must be in your house or the city your mayor or assistant mayor of." );
                return;                                                                     //It was not a valid use, so don't do anything else in the this target system
            }
            else if ( targeted is PelopsItem )
            {
                pi = ( PelopsItem )targeted;
                loc = new Point3D( pi.Location, pi.X + 2 );
                BaseHouse house = BaseHouse.FindHouseAt( loc, from.Map, pi.ItemData.Height );
                if ( house == null )
                {
                    from.SendMessage( "That would not still be in your house." );
                }
                else
                {
                    pi.X = ( pi.X + 1 );
                    from.SendMessage( "Moved east.  Another?" );
                    from.Target = new EastTarget( m_Pelops );
                }
            }
            else
            {
                from.SendMessage( "You may only move items that were created by the Deco Tool." );
            }
            from.CloseGump( typeof( PelopsResurrectionGump ) );
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
        }
    }
    public class SouthTarget : Target
    {
        Item pi = null;
        Point3D loc;
        private PelopsResurrection m_Pelops;

        public SouthTarget( PelopsResurrection pelops )
            : base( 4, false, TargetFlags.None )
        {
            m_Pelops = pelops;
        }

        protected override void OnTarget( Mobile from, object targeted )
        {
            PlayerMobile pm = (PlayerMobile)from;                                                              //declare a variable for the map to create the item. --currently set for from.Map, this is just added for versitility, modifications to this script might want to have offsets or other maps and such.
            if (!m_Pelops.IsValidUse( from ) && !(pm.City != null && pm.City.Mayor == pm && PlayerGovernmentSystem.IsAtCity( from ) || pm.City != null && pm.City.AssistMayor == pm && PlayerGovernmentSystem.IsAtCity( from )))
            //if ( !m_Pelops.IsValidUse( from ) )                                             //needed to avoid the player starting the action in their home, and then running to somewhere else to do the action.
            {
                from.SendMessage( "You must be in your house or the city your mayor or assistant mayor of." );
                return;                                                                     //It was not a valid use, so don't do anything else in the this target system
            }
            else if ( targeted is PelopsItem )
            {
                pi = ( PelopsItem )targeted;
                loc = new Point3D( pi.Location, pi.Y + 2 );
                BaseHouse house = BaseHouse.FindHouseAt( loc, from.Map, pi.ItemData.Height );
                if ( house == null )
                {
                    from.SendMessage( "That would not still be in your house." );
                }
                else
                {
                    pi.Y = ( pi.Y + 1 );
                    from.SendMessage( "Moved south.  Another?" );
                    from.Target = new SouthTarget( m_Pelops );
                }
            }
            else
            {
                from.SendMessage( "You may only move items that were created by Pelop's Resurrection." );
            }
            from.CloseGump( typeof( PelopsResurrectionGump ) );
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
        }
    }
    public class NorthTarget : Target
    {
        Item pi = null;
        Point3D loc;
        private PelopsResurrection m_Pelops;

        public NorthTarget( PelopsResurrection pelops )
            : base( 4, false, TargetFlags.None )
        {
            m_Pelops = pelops;
        }

        protected override void OnTarget( Mobile from, object targeted )
        {
            PlayerMobile pm = (PlayerMobile)from;                                                              //declare a variable for the map to create the item. --currently set for from.Map, this is just added for versitility, modifications to this script might want to have offsets or other maps and such.
            if (!m_Pelops.IsValidUse( from ) && !(pm.City != null && pm.City.Mayor == pm && PlayerGovernmentSystem.IsAtCity( from ) || pm.City != null && pm.City.AssistMayor == pm && PlayerGovernmentSystem.IsAtCity( from )))
            //if ( !m_Pelops.IsValidUse( from ) )                                             //needed to avoid the player starting the action in their home, and then running to somewhere else to do the action.
            {
                from.SendMessage( "You must be in your house or the city your mayor or assistant mayor of." );
                return;                                                                     //It was not a valid use, so don't do anything else in the this target system
            }
            else if ( targeted is PelopsItem )
            {
                pi = ( PelopsItem )targeted;
                loc = new Point3D( pi.Location, pi.Y - 2 );
                BaseHouse house = BaseHouse.FindHouseAt( loc, from.Map, pi.ItemData.Height );
                if ( house == null )
                {
                    from.SendMessage( "That would not still be in your house." );
                }
                else
                {
                    pi.Y = ( pi.Y - 1 );
                    from.SendMessage( "Moved north.  Another?" );
                    from.Target = new NorthTarget( m_Pelops );
                }
            }
            else
            {
                from.SendMessage( "You may only move items that were created by Pelop's Resurrection." );
            }
            from.CloseGump( typeof( PelopsResurrectionGump ) );
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
        }
    }
    public class WestTarget : Target
    {
        Item pi = null;
        Point3D loc;
        private PelopsResurrection m_Pelops;

        public WestTarget( PelopsResurrection pelops )
            : base( 4, false, TargetFlags.None )
        {
            m_Pelops = pelops;
        }

        protected override void OnTarget( Mobile from, object targeted )
        {
            PlayerMobile pm = (PlayerMobile)from;                                                              //declare a variable for the map to create the item. --currently set for from.Map, this is just added for versitility, modifications to this script might want to have offsets or other maps and such.
            if (!m_Pelops.IsValidUse( from ) && !(pm.City != null && pm.City.Mayor == pm && PlayerGovernmentSystem.IsAtCity( from ) || pm.City != null && pm.City.AssistMayor == pm && PlayerGovernmentSystem.IsAtCity( from )))
            //if ( !m_Pelops.IsValidUse( from ) )                                             //needed to avoid the player starting the action in their home, and then running to somewhere else to do the action.
            {
                from.SendMessage( "You must be in your house or the city your mayor or assistant mayor of." );
                return;                                                                     //It was not a valid use, so don't do anything else in the this target system
            }
            else if ( targeted is PelopsItem )
            {
                pi = ( PelopsItem )targeted;
                loc = new Point3D( pi.Location, pi.X - 2 );
                BaseHouse house = BaseHouse.FindHouseAt( loc, from.Map, pi.ItemData.Height );
                if ( house == null )
                {
                    from.SendMessage( "That would not still be in your house." );
                }
                else
                {
                    pi.X = ( pi.X - 1 );
                    from.SendMessage( "Moved west.  Another?" );
                    from.Target = new WestTarget( m_Pelops );
                }
            }
            else
            {
                from.SendMessage( "You may only move items that were created by Pelop's Resurrection." );
            }
            from.CloseGump( typeof( PelopsResurrectionGump ) );
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
        }
    }
    public class DecZTarget : Target
    {    
        Item pi = null;
        private PelopsResurrection m_Pelops;

        public DecZTarget( PelopsResurrection pelops )
            : base( 4, false, TargetFlags.None )
        {
            m_Pelops = pelops;
        }

        private static int GetFloorZ( Item item )
        {                                                                       //Taken from Interior Decorator...couldnt refrence it..t'was private
            Map map = item.Map;

            if ( map == null )
                return int.MinValue;

            StaticTile[] tiles = map.Tiles.GetStaticTiles( item.X, item.Y, true );

            int z = int.MinValue;

            for ( int i = 0; i < tiles.Length; ++i )
            {
                StaticTile tile = tiles[ i ];
                ItemData id = TileData.ItemTable[ tile.ID & 0x3FFF ];

                int top = tile.Z; 

                if ( id.Surface && !id.Impassable && top > z && top <= item.Z )
                    z = top;
            }

            return z;
        }

        protected override void OnTarget( Mobile from, object targeted )
        {
            PlayerMobile pm = (PlayerMobile)from;                                                              //declare a variable for the map to create the item. --currently set for from.Map, this is just added for versitility, modifications to this script might want to have offsets or other maps and such.
            if (!m_Pelops.IsValidUse( from ) && !(pm.City != null && pm.City.Mayor == pm && PlayerGovernmentSystem.IsAtCity( from ) || pm.City != null && pm.City.AssistMayor == pm && PlayerGovernmentSystem.IsAtCity( from )))
            //if ( !m_Pelops.IsValidUse( from ) )                                             //needed to avoid the player starting the action in their home, and then running to somewhere else to do the action.
            {
                from.SendMessage( "You must be in your house or the city your mayor or assistant mayor of." );
                return;                                                                     //It was not a valid use, so don't do anything else in the this target system
            }
            else if ( targeted is PelopsItem )
            {
                pi = ( PelopsItem )targeted;
                //int floorZ = GetFloorZ( pi );
                //if ( floorZ > int.MinValue && pi.Z > GetFloorZ( pi ) )
                //{
                    pi.Location = new Point3D( pi.Location, pi.Z - 1 ); ;
                    from.SendMessage( "Lowered.  Again?" );
                    from.Target = new DecZTarget( m_Pelops );
                //}
                //else
                //{
                    //from.SendLocalizedMessage( 1042275 ); // You cannot lower it down any further.
                //}
            }
            else
            {
                from.SendMessage( "You may only move items that were created by Pelop's Resurrection." );
            }
            from.CloseGump( typeof( PelopsResurrectionGump ) );
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
        }
    }
    public class IncZTarget : Target
    {
        Item pi = null;
        private PelopsResurrection m_Pelops;

        public IncZTarget( PelopsResurrection pelops )
            : base( 4, false, TargetFlags.None )
        {
            m_Pelops = pelops;
        }

        private static int GetFloorZ( Item item )
        {                                                                       //Taken from Interior Decorator...couldnt refrence it..t'was private
            Map map = item.Map;

            if ( map == null )
                return int.MinValue;

            StaticTile[] tiles = map.Tiles.GetStaticTiles( item.X, item.Y, true );

            int z = int.MinValue;

            for ( int i = 0; i < tiles.Length; ++i )
            {
                StaticTile tile = tiles[ i ];
                ItemData id = TileData.ItemTable[ tile.ID & 0x3FFF ];

                int top = tile.Z;

                if ( id.Surface && !id.Impassable && top > z && top <= item.Z )
                    z = top;
            }

            return z;
        }

        protected override void OnTarget( Mobile from, object targeted )
        {
            PlayerMobile pm = (PlayerMobile)from;                                                              //declare a variable for the map to create the item. --currently set for from.Map, this is just added for versitility, modifications to this script might want to have offsets or other maps and such.
            if (!m_Pelops.IsValidUse( from ) && !(pm.City != null && pm.City.Mayor == pm && PlayerGovernmentSystem.IsAtCity( from ) || pm.City != null && pm.City.AssistMayor == pm && PlayerGovernmentSystem.IsAtCity( from )))
            //if ( !m_Pelops.IsValidUse( from ) )                                             //needed to avoid the player starting the action in their home, and then running to somewhere else to do the action.
            {
                from.SendMessage( "You must be in your house or the city your mayor or assistant mayor of." );
                return;                                                                     //It was not a valid use, so don't do anything else in the this target system
            }
            else if ( targeted is PelopsItem )
            {
                pi = ( PelopsItem )targeted;
                //int floorZ = GetFloorZ( pi );

                //if ( floorZ > int.MinValue && pi.Z < ( floorZ + 20 ) ) // Confirmed : no height checks here
                //{
                    pi.Location = new Point3D( pi.Location, pi.Z + 1 );
                    from.SendMessage( "Raised.  Again?" );
                    from.Target = new IncZTarget( m_Pelops );
                //}
                //else
                //{
                   // from.SendLocalizedMessage( 1042274 ); // You cannot raise it up any higher.
                //}
            }
            else
            {
                from.SendMessage( "You may only move items that were created by Pelop's Resurrection." );
            }
            from.CloseGump( typeof( PelopsResurrectionGump ) );
            from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
        }
    }
    public class AddIDTarget : Target
    {
        Mobile mob = null;
        Item it = null;
        int tileID = 0;
        private PelopsResurrection m_Pelops;

        public AddIDTarget( PelopsResurrection pelops )
            : base( 18, false, TargetFlags.None )
        {
            m_Pelops = pelops;
        }

        protected override void OnTarget( Mobile from, object targeted )
        {
            if ( targeted is Mobile )
            {
                mob = ( Mobile )targeted;

                if ( !mob.InRange( from, 8 ) )
                {
                    from.SendMessage( 0x35, "You must be closer to see!" );
                    from.Target = new AddIDTarget( m_Pelops );
                    return;
                }
                if ( mob is PlayerMobile )
                {
                    Mobile pm = ( PlayerMobile )targeted;
                    from.SendMessage( 0x35, pm.Name +" can not be added to your collection!" );
                    from.Target = new AddIDTarget( m_Pelops );
                    return;
                }
                if ( mob is BaseVendor )
                {
                    Mobile vend = ( BaseVendor )targeted;
                    from.SendMessage( 0x35, vend.Name + " can not be added to your collection!" );
                    from.Target = new AddIDTarget( m_Pelops );
                    return;
                }
                else if ( mob is BaseCreature )
                {
                    tileID = ShrinkTable.Lookup( mob );
                    m_Pelops.AddIdToPelops( from, tileID );
                    from.Target = new AddIDTarget( m_Pelops );
                    return;
                }
            }
            else if ( targeted is StaticTarget )
            {
                tileID = ( ( StaticTarget )targeted ).ItemID;
                m_Pelops.AddIdToPelops( from, tileID );
                from.Target = new AddIDTarget( m_Pelops );
                return;
            }
            else if ( targeted is Item )
            {
                it = ( Item )targeted;
                if ( it is PelopsResurrection )
                {
                    from.CloseGump( typeof( PelopsResurrectionGump ) );
                    from.SendGump( new PelopsResurrectionGump( from, m_Pelops ) );
                }
                else
                {
                    tileID = it.ItemID;
                    m_Pelops.AddIdToPelops( from, tileID );
                    from.Target = new AddIDTarget( m_Pelops );
                    return;
                }
            }
        }
    }
}
