 /*Created on SharpDevelop.
 * Build By : Lucas Henrique Pena de Araújo Abreu (TacurumiN)
 * Date: 31/01/2014
 * Hour: 20:51*/
 
using System;
using Server.Targeting;
using Server.Items;

namespace Server.Items
{
    public class FishOil : Item
    {
		//This item has just one Charge, be sure to use it correctly.
		
        [Constructable]
        public FishOil()
            : base(0x1C18)
        {
            this.Weight = 1.0;
        }

        public FishOil(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber
        {
            get
            {
                return 1150863;
            }
        }// Fish Oil Flask
		
		public override void OnDoubleClick(Mobile from)
		{
			if ( !IsChildOf (from.Backpack)) // If the object is not on your backpack the doubleclick will not work.
			{
				//from.SendMessage( "Este objeto precisa estar em sua mochila para que possa utiliza-lo!" );
				from.SendLocalizedMessage(1042010); // You must have the object in your backpack to use it.
			}
			else
			{
				from.SendLocalizedMessage(1154219); // Where do you wish to use this?
				//from.BeginTarget(2, false, TargetFlags.None, new TargetCallback( OnTargetOracle )); // Begins the targeting.
			}
		}
		
		// OnTarget method that is used for the targeting purpose.
		/*private void OnTargetOracle(Mobile from, object targeted)
		{
			// if the target is the Oracle of the seas, does the magic :
			if(targeted is OracleOfTheSea)
			{ 
				// The Oracle uses goes back to 5.
				((OracleOfTheSea)targeted).UsesRemaining = 5;
			 
				from.SendMessage("Your have fully changed your Oracle of The Sea !"); // Confirmation message.
				//Delete the Fish Oil Flask
				this.Delete();
			}
			else if(targeted is Lantern)
			{
				// If the oil is used on a lantern it will make them stay lit for a very long time.
				((Lantern)targeted).Duration = TimeSpan.FromMinutes(60);
				((Lantern)targeted).Burning = true;	
				((Lantern)targeted).BurntOut = false;
				
				from.SendMessage("Your have fully changed your Lantern !"); // Confirmation message.
				//Delete the Fish Oil Flask
				this.Delete();
			}
			else
			{
				// If the targeted item is not the Oracle Of The Seas or a Lantern, the oil does nothing.
				from.SendMessage("This item does not have any effect on this.");
			}
		}*/
		
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}