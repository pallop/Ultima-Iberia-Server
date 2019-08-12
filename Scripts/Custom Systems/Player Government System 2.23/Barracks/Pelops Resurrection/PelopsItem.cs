using System;
using Server;
using Server.Items;
using Server.Multis;
using System.Collections;
using Server.Ashlar;
using Server.Regions;
using Server.Network;
using Server.Engines.XmlSpawner2;

namespace Server.Ashlar
{
	public class PelopsItem : Item
	{
 
                private CityManagementStone m_Stone;

		[Constructable]
		public PelopsItem() : base( 0x3EE8 )
		{
                // add a siege attachment with 500 hits, 60% fire resistance, 20% physical resistance
                XmlAttach.AttachTo(this, new XmlSiege(250,50,50,1,1,1));
                //As you can see, this is truely just deco, nothing to upset the balance of a shard.
		}

		public PelopsItem( Serial serial ) : base( serial )
		{
		}
  
                public override void OnDelete()
		{
                       BaseHouse house = BaseHouse.FindHouseAt( (PelopsItem)this );
                       Point3D p = new Point3D( this.X, this.Y, this.Z );
                       Region r = Region.Find( p, this.Map );
                       PlayerCityRegion cityreg = r as PlayerCityRegion;

                     if ( r.IsPartOf( typeof( HouseRegion )))
                     {
                               Point3D v = new Point3D( this.X, this.Y, this.Z );
                               Region u = Region.Find( v, this.Map );
                               HouseRegion housereg = u as HouseRegion;
                               //house.SetLockdown( this, true );
                      }
                      else if ( r.IsPartOf( typeof( PlayerCityRegion )))
                      {
                                foreach ( Item i in World.Items.Values )
				{
                                        
					if ( i is CityManagementStone )
					{
						CityManagementStone s = (CityManagementStone)i;
                                                Point3D q = new Point3D( s.X, s.Y, s.Z );
                                                Region t = Region.Find( q, s.Map );
                                                PlayerCityRegion stonereg = t as PlayerCityRegion;
						if ( cityreg == stonereg )
                                                {
                                                           ArrayList decore = s.isLockedDown;
                                                           if ( decore == null )
                                                           {
					                      s.isLockedDown = new ArrayList();
					                      decore = s.isLockedDown;
				                           }
                                                   decore.Remove( this );
                                                   s.CurrentDecore -= 1;
                                                }

					}
				}
                      }
                      else
                      {
                      return;
                      }
			               base.OnDelete();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
