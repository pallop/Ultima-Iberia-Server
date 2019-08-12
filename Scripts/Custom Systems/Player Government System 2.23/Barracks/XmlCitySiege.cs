using System;
using Server;
using Server.Items;
using Server.Network;
using System.Collections;
using Server.Mobiles;
using Server.Regions;

namespace Server.Engines.XmlSpawner2
{
    public class XmlCitySiege : XmlSiege
    {

        public XmlCitySiege(ASerial serial): base(serial)
        {
        }

        [Attachable]
	public XmlCitySiege(int hitsmax, int resistfire, int resistphysical, int wood, int iron, int stone) : base(hitsmax, resistfire, resistphysical, wood, iron, stone)
	{
	}

        public override void OnDestroyed()
        {
                       //get the object that it is attached to
                       Item building = AttachedTo as Item; //<---Building
                       Point3D p = new Point3D( building.X, building.Y, building.Z );//<---location
                       Region r = Region.Find( p, building.Map );   //<---region
                       PlayerCityRegion cityreg = r as PlayerCityRegion;//<---region
                             
                     if ( r.IsPartOf( typeof( PlayerCityRegion )))  //<---building is in city region
                      {
                          ArrayList deletelist = new ArrayList();
                             foreach (Item i in World.Items.Values)
                             {
                                if (i is CivicSign)
                                {
                                   CivicSign z = (CivicSign)i;  //<--- line 44 from error
                                   if (z.toDelete != null && z.toDelete.Contains(building))//<---look for the Building/Addon in the arraylist
                                   {
                                      deletelist.Add(z);
                                   }
                                 }
                              }

                              foreach (Item d in deletelist)
                              {
                                d.Delete();
                              }
                              
                           //}
                           ArrayList deletelist1 = new ArrayList();
                           foreach (Item t in World.Items.Values)
                             {
                               if (t is CityManagementStone)
                               {
                                 CityManagementStone y = (CityManagementStone)t;
                                  if (y.toDelete != null && y.toDelete.Contains(building))
                                  {
                                     deletelist1.Add(y);
                                  }
                              }
                                 foreach (Item c in deletelist1)
                              {
                                c.Delete();
                                return;
                              }
                            }
                       }
            // add code to find all of the items associated with the building and delete them
            // and to remove the building from the city system

            // the base method will destroy the target building itself
            base.OnDestroyed();  //EDITED THIS OUT AS THERE WONT BE ANY ID TO CHANGE ANYMORE
        }

        public override void Serialize(GenericWriter writer)
        {

            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}

