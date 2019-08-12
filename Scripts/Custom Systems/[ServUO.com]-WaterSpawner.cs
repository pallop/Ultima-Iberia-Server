using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Commands;
using System.Collections;
using System.Collections.Generic;

namespace Server
{
  public class WaterSpawnGenerator
  {
    private static Rectangle2D[] m_BritRegions = new Rectangle2D[]
      {
        new Rectangle2D( new Point2D( 0, 0 ), new Point2D( 6142, 4094 ) ),
      };
      
    private static XmlSpawner.SpawnObject spawnWater01;
    private static XmlSpawner.SpawnObject spawnWater02;
    private static XmlSpawner.SpawnObject spawnWater03;
    private static XmlSpawner.SpawnObject spawnWater04;
    private static XmlSpawner.SpawnObject spawnWater05;
    private static XmlSpawner.SpawnObject spawnWater06;
    private static XmlSpawner.SpawnObject spawnWater07;
    private static XmlSpawner.SpawnObject spawnWater08;
    private static XmlSpawner.SpawnObject spawnWater09;
    private static XmlSpawner.SpawnObject spawnWater10;
    private static XmlSpawner.SpawnObject spawnWater11;
    private static XmlSpawner.SpawnObject spawnWater12;
    private static XmlSpawner.SpawnObject[] spawnOres = new XmlSpawner.SpawnObject[12];

    private static int[] m_Criterion = new int[]
      {

        //Trees
        0xC9E, 0xCA8, 0xCAA, 0xCCA, 0xCCB, 0xCCC, 0xCCD, 0xCD0, 0xCD3, 0xCD6, 0xCD8, 0xCDA, 0xCDD, 0xCE0, 0xCE3,
        0xCE6, 0xCF8, 0xCFB, 0xCFE, 0xD01, 0xD43, 0xD59, 0xD70, 0xD85, 0xD94, 0xD98, 0xD9C, 0xDA0, 0xDA4, 0xDA8,
        
        //Rocks
        4943, 4944, 4945, 4946, 4947, 4948, 4949, 4950, 4951, 4951, 4953, 4954, 4955, 4956, 4957, 4958, 4959,
        4960, 4961, 4962, 4963, 4964, 4965, 4966, 4967, 4968, 4969, 4970, 4971, 4972, 4973, 6001, 6002, 6003,
        6004, 6005, 6006, 6007, 6008, 6009, 6010, 6011, 6012, 13121, 13122, 13123, 13124, 13125, 13126, 13127,
        13128, 13129, 13130, 13131, 13132, 13133, 13134, 13135, 13136, 13137, 13354, 13355, 13361, 13362, 13363,
        13364, 13365, 13366, 13367, 13368, 13369, 13625, 13626, 13627, 13628,
        //0x12B9,

        //grass
        0x177D, 0x177E, 0x177E, 0x177F, 0x1780, 0x1781,
        //dirt
        0x31F4, 0x31F5, 0x31F6, 0x31F7, 0x31F8, 0x31F9, 0x31FA, 0x31FB,
        //swamp
        0x3209, 0x320A, 0x320D, 0x3209E, 0x320F, 0x3210, 0x3211, 0x3213, 0x3214, 0x3215, 0x3216, 0x3217, 0x3218, 0x3219, 0x321A,
        0x321B, 0x321C, 0x321D, 0x321E, 0x3220, 0x3221, 0x3222, 0x3223, 0x3224, 0x3226, 0x3227, 0x3228, 0x3229, 0x322A,
        0x3236, 

        //lava
        0x12EE, 0x12EF, 0x12F0, 0x12F1, 0x12F2, 0x12F4, 0x12F5, 0x12F6, 0x12F7, 0x12F8, 0x12F9, 0x12FA, 0x12FB, 0x12FC, 0x12FD, 0x12FE,
        0x1300, 0x1301, 0x1302, 0x1303, 0x1304, 0x1306, 0x1307, 0x1308, 0x1309, 0x130A, 0x130C, 0x130D, 0x130E, 0x130F, 0x1310, 0x1312, 
        0x1313, 0x1314, 0x1315, 0x1316, 0x1318, 0x1310, 0x131A, 0x131B, 0x131C, 0x136E, 0x137E, 0x1380, 0x1382,


        
      };

    public static void Initialize()
    {
      CommandSystem.Register( "WaterSpawnGen", AccessLevel.Administrator, new CommandEventHandler( WaterSpawnGen_OnCommand ) );
      CommandSystem.Register( "WaterSpawnRem", AccessLevel.Administrator, new CommandEventHandler( WaterSpawnRem_OnCommand ) );

      //spawnOreVain = new XmlSpawner.SpawnObject("RandomOreVain", 4);
      //spawnOreVain.SpawnsPerTick = 1;
      //spawnOres[0] = spawnOreVain;
      
    }

    [Usage( "WaterSpawnGen" )]
    [Description( "Generates Ore Spot spawners by analyzing the map. Slow." )]
    public static void WaterSpawnGen_OnCommand( CommandEventArgs e )
    {
      Generate();
    }
    
    [Usage( "WaterSpawnRem" )]
    [Description( "Removes Ore Spot spawners by analyzing the map. Slow." )]
    public static void WaterSpawnRem_OnCommand( CommandEventArgs e )
    {
      Remove();
    }

    private static Map m_Map;
    private static int m_Count;

    public static void Generate()
    {
      World.Broadcast( 0x35, true, "Generating Water spawners, please wait." );

      Network.NetState.FlushAll();
      Network.NetState.Pause();

      m_Map = Map.Felucca;
      m_Count = 0;

      for ( int i = 0; i < m_BritRegions.Length; ++i )
        Generate( m_BritRegions[i] );

      int FeluccaCount = m_Count;

      Network.NetState.Resume();

      World.Broadcast( 0x35, true, "Water spawner generation complete. {0}", FeluccaCount );
    }

    public static bool IsRock( int id )
    {
      if ( id > m_Criterion[m_Criterion.Length - 1] )
        return false;

      for ( int i = 0; i < m_Criterion.Length; ++i )
      {
        int delta = id - m_Criterion[i];

        if ( delta < 0 )
          return false;
        else if ( delta == 0 )
          return true;
      }

      return false;
    }


     public static bool IsTree( int id )
    {
      if ( id > m_Criterion[m_Criterion.Length - 1] )
        return false;

      for ( int i = 0; i < m_Criterion.Length; ++i )
      {
        int delta = id - m_Criterion[i];

        if ( delta < 0 )
          return false;
        else if ( delta == 0 )
          return true;
      }

      return false;
    }

     public static bool IsSwamp( int id )
    {
      if ( id > m_Criterion[m_Criterion.Length - 1] )
        return false;

      for ( int i = 0; i < m_Criterion.Length; ++i )
      {
        int delta = id - m_Criterion[i];

        if ( delta < 0 )
          return false;
        else if ( delta == 0 )
          return true;
      }

      return false;
    }

     public static bool IsDirt( int id )
    {
      if ( id > m_Criterion[m_Criterion.Length - 1] )
        return false;

      for ( int i = 0; i < m_Criterion.Length; ++i )
      {
        int delta = id - m_Criterion[i];

        if ( delta < 0 )
          return false;
        else if ( delta == 0 )
          return true;
      }

      return false;
    }

     public static bool IsGrass( int id )
    {
      if ( id > m_Criterion[m_Criterion.Length - 1] )
        return false;

      for ( int i = 0; i < m_Criterion.Length; ++i )
      {
        int delta = id - m_Criterion[i];

        if ( delta < 0 )
          return false;
        else if ( delta == 0 )
          return true;
      }

      return false;
    }


    public static bool IsLava( int id )
    {
      if ( id > m_Criterion[m_Criterion.Length - 1] )
        return false;

      for ( int i = 0; i < m_Criterion.Length; ++i )
      {
        int delta = id - m_Criterion[i];

        if ( delta < 0 )
          return false;
        else if ( delta == 0 )
          return true;
      }

      return false;
    }





    private static void AddWaterSpawner ( int x, int y, int z )
    {
        if ( Utility.RandomDouble() < 0.9750 )
            return;
        
        Map map = Map.Felucca;
        IPooledEnumerable eable = map.GetItemsInRange(new Point3D(x, y, z), 20);
        
        foreach ( Item item in eable )
        {
            if ( item is XmlSpawner ) {
                if ( item.Hue == 346 ) {
                    return;
                }
            }
        }
      
        if ( !m_Map.CanFit( x, y, z, 1, false, false ) )
          return;
        
        //XmlSpawner(int amount, int minDelay, int maxDelay, int team, int homeRange, int spawnrange, string creatureName)
        XmlSpawner spawner = null;
        int MaxSpawn = 0;
        int Counter = 0;
        switch( Utility.Random(6) ) {
            case 0:
                spawner = new XmlSpawner( 10, 1, 3, 0, 0, Utility.RandomMinMax(1,6), "SeaSerpent" );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("DeepSeaSerpent", 3) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Kraken", 2) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("WaterElemental", 2) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 9) );
                MaxSpawn = 12;
                Counter = 3;
                break;
            case 1:
                spawner = new XmlSpawner( 4, 2, 5, 0, 0, Utility.RandomMinMax(1,5), "PirateCaptain" );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 4) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                MaxSpawn = 6;
                Counter = 3;
                break;
            case 2:
                spawner = new XmlSpawner( 4, 1, 3, 0, 0, Utility.RandomMinMax(1,2), "PirateCaptain" );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("DeepSeaSerpent", 4) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Kraken", 2) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                MaxSpawn = 10;
                Counter = 6;
                break;
            case 3:
                spawner = new XmlSpawner( 2, 1, 3, 0, 0, Utility.RandomMinMax(1,3), "PirateCaptain" );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("WaterElemental", 4) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 2) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                MaxSpawn = 14;
                Counter = 8;
                break;
            case 4:
                 spawner = new XmlSpawner( 10, 1, 3, 0, 0, Utility.RandomMinMax(1,6), "SeaSerpent" );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("DeepSeaSerpent", 3) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Kraken", 2) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("WaterElemental", 2) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 9) );
                MaxSpawn = 6;
                Counter = 4;
                break;
            case 5:
                spawner = new XmlSpawner( 6, 3, 7, 0, 0, Utility.RandomMinMax(8,10), "Dolphin" );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Dolphin", 1) );
                MaxSpawn = 6;
                Counter = 4;
                break;
        }
        
        //Spawn Orcs ( 30% )
        if ( spawner != null ) {
            switch( Utility.Random(10) ) {
                case 0:
                case 1: // 20%
                    spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("DeepSeaSerpent", 2) );
                    break;
                case 2: // 10%
                    spawner.m_SpawnObjects.Add( new XmlSpawner.SpawnObject("Kraken", 2) );
                    break;
                default:
                    break;
            }
        }
        
        if ( spawner != null ) {
            for ( int i = 0; i < Counter; i++ ) {
                spawner.SpawnObjects[i].SpawnsPerTick = 1;
            }
            
            spawner.Name = "Water Spawner #" + m_Count;
            spawner.MoveToWorld( new Point3D( x, y, z), Map.Felucca);
            spawner.MaxCount = MaxSpawn;
            spawner.Hue = 346;
        }
  
        ++m_Count;
    }

    public static void Generate( Rectangle2D region )
    {
      int OakTree = 0x12B9;
      
      for ( int rx = 0; rx < region.Width; ++rx )
      {
        for ( int ry = 0; ry < region.Height; ++ry )
        {
          int vx = rx + region.X;
          int vy = ry + region.Y;

          StaticTile[] tiles = m_Map.Tiles.GetStaticTiles( vx, vy );

          for ( int i = 0; i < tiles.Length; ++i )
          {
            StaticTile tile = tiles[i];

            int id = tile.ID;
            id &= 0x3FFF;
            int z = tile.Z;

            if ( IsRock( id ) )
            {
              AddWaterSpawner( vx + 1, vy, z );
            }

             if ( IsTree( id ) )
            {
              AddWaterSpawner( vx + 1, vy, z );
            }

             if ( IsGrass( id ) )
            {
              AddWaterSpawner( vx + 1, vy, z );
            }

             if ( IsDirt( id ) )
            {
              AddWaterSpawner( vx + 1, vy, z );
            }

             if ( IsSwamp( id ) )
            {
              AddWaterSpawner( vx + 1, vy, z );
            }

            if ( IsLava( id ) )
            {
              AddWaterSpawner( vx + 1, vy, z );
            }



            }
        }
      }
    }
    
    public static void Remove() {
        List<Item> itemList = new List<Item>( World.Items.Values );
        
        List<Item> WaterSpawner = new List<Item>();
        
        foreach ( Item item in itemList )
        {
            if ( item is XmlSpawner ) {
                XmlSpawner spawner = item as XmlSpawner;
                if ( spawner.SpawnObjects.Length > 0 ) {
                    if ( spawner.SpawnObjects[0].TypeName.ToUpper() == "SeaSerpent" ) {
                        WaterSpawner.Add(item);
                    }
                    else if ( spawner.SpawnObjects[0].TypeName.ToUpper() == "PirateCaptain" ) {
                        WaterSpawner.Add(item);
                    }
                    else if ( spawner.SpawnObjects[0].TypeName.ToUpper() == "DeepSeaSerpent" ) {
                        WaterSpawner.Add(item);
                    }
                    else if ( spawner.SpawnObjects[0].TypeName.ToUpper() == "Dolphin" ) {
                        WaterSpawner.Add(item);
                    }
                    else if ( spawner.SpawnObjects[0].TypeName.ToUpper() == "WaterElemental" ) {
                        WaterSpawner.Add(item);
                    }
                    else if ( spawner.SpawnObjects[0].TypeName.ToUpper() == "Kraken" ) {
                        WaterSpawner.Add(item);
                    }
                    else if ( spawner.SpawnObjects[0].TypeName.ToUpper() == "WaterElemental" ) {
                        WaterSpawner.Add(item);
                    }
                    else if ( spawner.SpawnObjects[0].TypeName.ToUpper() == "Dolphin" ) {
                        WaterSpawner.Add(item);
                    }
                    else if ( spawner.SpawnObjects[0].TypeName.ToUpper() == "Kraken" ) {
                        WaterSpawner.Add(item);
                    }
                    else if ( spawner.SpawnObjects[0].TypeName.ToUpper() == "DeepSeaSerpent" ) {
                        WaterSpawner.Add(item);
                    }
                    else if ( spawner.SpawnObjects[0].TypeName.ToUpper() == "PirateCaptain" ) {
                        WaterSpawner.Add(item);
                    }
                }
            }
        }
        
        Console.WriteLine("Total Counted Spawner : {0}", WaterSpawner.Count);
        
        foreach (Item item in WaterSpawner)
            item.Delete();
    }
  }
}