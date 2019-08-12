/*Scripted by  _____         
*	  		   \_   \___ ___ 
*			    / /\/ __/ _ \
*		     /\/ /_| (_|  __/
*			 \____/ \___\___|
*v1.1	    (All Staff Levels)
*/
using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Targeting;

namespace Server.Gumps
{
   public class CityTool : Gump
    {
        private int i_SHN;
        public static void Initialize()
        {
                CommandSystem.Register("CityTool", AccessLevel.Player, new CommandEventHandler(CityTool_OnCommand));
        }


        private static void CityTool_OnCommand(CommandEventArgs e)
        {
	       
            Mobile from = e.Mobile;
            Console.WriteLine ("*[{1}] {0}  Arcquitectura deco tool**", from.Name, from.AccessLevel);
            PlayerMobile pm = (PlayerMobile)from;

           
            if ( pm.City != null )
            {
                 if ( pm.City.Mayor != from )
                {
                     from.CloseGump(typeof(CityTool));
                    from.SendMessage( "Debes de ser el alcalde para usar el comando" );
                }
                else if ( !PlayerGovernmentSystem.IsAtCity( from ) )
                {
                     from.CloseGump(typeof(CityTool));
                    from.SendMessage( "Debes estar dentro de la ciudad para usar el comando." );
                }
                
                else
                {
                     from.SendGump(new CityTool(from));
                }
            }
            else
            {
                 from.CloseGump(typeof(CityTool));
                from.SendMessage( "You must be the mayor of a city in order to use this." );
            }

        

            
        }
        public CityTool(Mobile from): base(0, 0)
        {

            Closable = false;
            Disposable = false;
            Dragable = true;
            Resizable = false;
            
        
            AddPage(1);	
	        AddImage(98, 71, 5010);
	        AddBackground(154, 84, 89, 21, 9200);
	        AddLabel(158, 85, 267, "UI ArquiDeco");
	        AddButton(191, 160, 22150, 22151, 0, GumpButtonType.Reply, 0);//CLOSE GUMP
			
			
        	
			if ( from.AccessLevel >= AccessLevel.Player)
			{

	            AddImage(174, 105, 4500);//NE
	            AddImage(200, 116, 4501);//N
	            AddImage(211, 142, 4502);//NW
	            AddImage(200, 169, 4503);//W
	            AddImage(174, 179, 4504);//SW
	            AddImage(146, 169, 4505);//S
	            AddImage(135, 142, 4506);//SE
	            AddImage(147, 115, 4507);//E
                
	            AddButton(191, 120, 11410, 11402, 8, GumpButtonType.Reply, 0);
	            AddButton(221, 129, 11410, 11402, 1, GumpButtonType.Reply, 0);
	            AddButton(232, 159, 11410, 11402, 2, GumpButtonType.Reply, 0);
	            AddButton(223, 188, 11410, 11402, 7, GumpButtonType.Reply, 0);
	            AddButton(191, 200, 11410, 11402, 4, GumpButtonType.Reply, 0);
	            AddButton(162, 188, 11410, 11402, 5, GumpButtonType.Reply, 0);
	            AddButton(151, 159, 11410, 11402, 6, GumpButtonType.Reply, 0);
	            AddButton(163, 131, 11410, 11402, 3, GumpButtonType.Reply, 0);

                

	          	AddButton(176, 247, 2262, 2282, 12, GumpButtonType.Reply, 0);//UNLOCK//lock
	            AddButton(285, 127, 22414, 22415, 9, GumpButtonType.Reply, 0);//Z UP
	            AddButton(284, 172, 22411, 22412, 10, GumpButtonType.Reply, 0);//Z DOWN
	          	
	         
        	}
        	
          

            AddPage(2);
            
            AddLabel(197, 248, 0, "Decreases the Z");
            AddLabel(201, 296, 0, "Increases the Z");
            AddLabel(213, 339, 0, "Fijar Objeto");
            AddLabel(192, 363, 0, "Closes GM Tool");
            

         
          
			
          

        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            string prefix = Server.Commands.CommandSystem.Prefix;
            TextRelay webadres = info.GetTextEntry(1);
            string website = (webadres == null ? "" : webadres.Text.Trim());
            switch (info.ButtonID)
            {
                case 1:
                    from.Target = new NT();
                    from.SendMessage(2125, "What do you wish to move North? (ESC to cancel)");
                    from.SendGump(new CityTool(from));
                    break;
                case 2:
                    from.Target = new NWT();
                    from.SendMessage(2125, "What do you wish to move North East? (ESC to cancel)");
                    from.SendGump(new CityTool(from));
                    break;
                case 3:
                    from.Target = new WT();
                    from.SendMessage(2125, "What do you wish to move West? (ESC to cancel)");
                    from.SendGump(new CityTool(from));
                    break;
                case 4:
                    from.Target = new SWT();
                    from.SendMessage(2125, "What do you wish to move South East? (ESC to cancel)");
                    from.SendGump(new CityTool(from));
                    break;
                case 5:
                    from.Target = new ST();
                    from.SendMessage(2125, "What do you wish to move South? (ESC to cancel)");
                    from.SendGump(new CityTool(from));
                    break;
                case 6:
                    from.Target = new SET();
                    from.SendMessage(2125, "What do you wish to move South West? (ESC to cancel)");
                    from.SendGump(new CityTool(from));
                    break;
                case 7:
                    from.Target = new ET();
                    from.SendMessage(2125, "What do you wish to move East? (ESC to cancel)");
                    from.SendGump(new CityTool(from));
                    break;
                case 8:
                    from.Target = new NET();
                    from.SendMessage(2125, "What do you wish to move North West? (ESC to cancel)");
                    from.SendGump(new CityTool(from));
                    break;
                case 9:
                    from.Target = new ZUT();
                    from.SendMessage(2125, "What do you wish to Raise? (ESC to cancel)");
                    from.SendGump(new CityTool(from));
                    break;
                case 10:
                    from.Target = new ZDT();
                    from.SendMessage(2125, "What do you wish to Lower? (ESC to cancel)");
                    from.SendGump(new CityTool(from));
                    break;
               
                case 11:
                        from.Target = new DT();
                    from.SendMessage(2125, "What do you wish to ? (ESC to cancel)");
                    from.SendGump(new CityTool(from));
                    break;
                case 12:
                    from.Target = new UT();
                    from.SendMessage(2125, "What do you wish to Lock/Unlock? (ESC to cancel)");
                    from.SendGump(new CityTool(from));
                    break;
               

            
        
            }
        }
        public class NT : Target //1
        {
            public NT()
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!BaseCommand.IsAccessible(from, targeted))
                {
                    from.SendMessage(38, "Invalid Target.");
                    from.Target = new NT();
                    return;
                }
                if (targeted is Item)
                {
                    Item item = (Item)targeted;
                    item.Y = (item.Y - 1);
                    from.Target = new NT();
                }
                else if (targeted is Mobile)
                {
                    Mobile m = (Mobile)targeted;
                    m.Y = (m.Y - 1);
                    from.Target = new NT();
                }
                else
                    from.Target = new NT();
            }
        }
        public class NWT : Target //2
        {
            public NWT()
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!BaseCommand.IsAccessible(from, targeted))
                {
                    from.SendMessage(38, "Invalid Target.");
                    from.Target = new NWT();
                    return;
                }
                if (targeted is Item)
                {
                    Item item = (Item)targeted;

                    item.X = (item.X + 1);
                    item.Y = (item.Y - 1);
                    from.Target = new NWT();
                }
                else if (targeted is Mobile)
                {
                    Mobile m = (Mobile)targeted;
                    m.X = (m.X + 1);
                    m.Y = (m.Y - 1);
                    from.Target = new NWT();
                }
                else
                    from.Target = new NWT();
            }
        }
        public class WT : Target//3
        {
            public WT()
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!BaseCommand.IsAccessible(from, targeted))
                {
                    from.SendMessage(38, "Invalid Target.");
                    from.Target = new WT();
                    return;
                }
                if (targeted is Item)
                {
                    Item item = (Item)targeted;
                    item.X = (item.X - 1);
                    from.Target = new WT();
                }
                else if (targeted is Mobile)
                {
                    Mobile m = (Mobile)targeted;
                    m.X = (m.X - 1);
                    from.Target = new WT();
                }
                else
                    from.Target = new WT();
            }
        }
        public class SWT : Target//4
        {
            public SWT()
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!BaseCommand.IsAccessible(from, targeted))
                {
                    from.SendMessage(38, "Invalid Target.");
                    from.Target = new SWT();
                    return;
                }
                if (targeted is Item)
                {
                    Item item = (Item)targeted;

                    item.X = (item.X + 1);
                    item.Y = (item.Y + 1);
                    from.Target = new SWT();
                }
                else if (targeted is Mobile)
                {
                    Mobile m = (Mobile)targeted;
                    m.X = (m.X + 1);
                    m.Y = (m.Y + 1);
                    from.Target = new SWT();
                }
                else
                    from.Target = new SWT();
            }
        }
        public class ST : Target//5
        {
            public ST()
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!BaseCommand.IsAccessible(from, targeted))
                {
                    from.SendMessage(38, "Invalid Target.");
                    from.Target = new ST();
                    return;
                }
                if (targeted is Item)
                {
                    Item item = (Item)targeted;

                    item.Y = (item.Y + 1);
                    from.Target = new ST();
                }
                else if (targeted is Mobile)
                {
                    Mobile m = (Mobile)targeted;
                    m.Y = (m.Y + 1);
                    from.Target = new ST();
                }
                else
                    from.Target = new ST();
            }
        }
        public class SET : Target//6
        {
            public SET()
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!BaseCommand.IsAccessible(from, targeted))
                {
                    from.SendMessage(38, "Invalid Target.");
                    from.Target = new SET();
                    return;
                }
                if (targeted is Item)
                {
                    Item item = (Item)targeted;

                    item.X = (item.X - 1);
                    item.Y = (item.Y + 1);
                    from.Target = new SET();
                }
                else if (targeted is Mobile)
                {
                    Mobile m = (Mobile)targeted;
                    m.X = (m.X - 1);
                    m.Y = (m.Y + 1);
                    from.Target = new SET();
                }
                else
                    from.Target = new SET();
            }
        }
        public class ET : Target//7
        {
            public ET()
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!BaseCommand.IsAccessible(from, targeted))
                {
                    from.SendMessage(38, "Invalid Target.");
                    from.Target = new ET();
                    return;
                }
                if (targeted is Item)
                {
                    Item item = (Item)targeted;

                    item.X = (item.X + 1);
                    from.Target = new ET();
                }
                else if (targeted is Mobile)
                {
                    Mobile m = (Mobile)targeted;
                    m.X = (m.X + 1);
                    from.Target = new ET();
                }
                else
                    from.Target = new ET();
            }
        }
        public class NET : Target//8
        {
            public NET()
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!BaseCommand.IsAccessible(from, targeted))
                {
                    from.SendMessage(38, "Invalid Target.");
                    from.Target = new NET();
                    return;
                }
                if (targeted is Item)
                {
                    Item item = (Item)targeted;

                    item.Y = (item.Y - 1);
                    item.X = (item.X - 1);
                    from.Target = new NET();
                }
                else if (targeted is Mobile)
                {
                    Mobile m = (Mobile)targeted;
                    m.Y = (m.Y - 1);
                    m.X = (m.X - 1);
                    from.Target = new NET();
                }
                else
                    from.Target = new NET();
            }
        }
        public class ZUT : Target//9
        {
            public ZUT()
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!BaseCommand.IsAccessible(from, targeted))
                {
                    from.SendMessage(38, "Invalid Target.");
                    from.Target = new ZUT();
                    return;
                }
                if (targeted is Item)
                {
                    Item item = (Item)targeted;

                    item.Z = (item.Z + 1);
                    from.Target = new ZUT();
                }
                else if (targeted is Mobile)
                {
                    Mobile m = (Mobile)targeted;
                    m.Z = (m.Z + 1);
                    from.Target = new ZUT();
                }
                else
                    from.Target = new ZUT();
            }
        }
        public class ZDT : Target//10
        {
            public ZDT()
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!BaseCommand.IsAccessible(from, targeted))
                {
                    from.SendMessage(38, "Invalid Target.");
                    from.Target = new ZDT();
                    return;
                }
                if (targeted is Item)
                {
                    Item item = (Item)targeted;

                    item.Z = (item.Z - 1);
                    from.Target = new ZDT();
                }
                else if (targeted is Mobile)
                {
                    Mobile m = (Mobile)targeted;
                    m.Z = (m.Z - 1);
                    from.Target = new ZDT();
                }
                else
                    from.Target = new ZDT();
            }
        }
        public class DT : Target//11
        {
            public DT()
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!BaseCommand.IsAccessible(from, targeted))
                {
                    from.SendMessage(38, "Invalid Target.");
                    from.Target = new DT();
                    return;
                }
                if (targeted is Item)
                {
                    Item item = (Item)targeted;
                    item.Delete();
                    if (item.Name == null)
                    {
                        from.SendMessage(2125, "Item Deleted.");
                        from.Target = new DT();
                            return;
                    }
                    else
                        from.SendMessage(2125, "{0} Deleted.", item.Name);
                    from.Target = new DT();
                }
                else if (targeted is BaseVendor || targeted is BaseCreature)
                {
                    Mobile m = (Mobile)targeted;
                    from.Target = new DT();
                    from.SendMessage(2125, "{0} Deleted.", m.Name);
                    m.Delete();
                }
                else
                    from.SendMessage(38, "Invalid Target.");
                from.Target = new DT();
            }
        }
        public class UT : Target//12
        {
            public UT()
                : base(-1, false, TargetFlags.None)
            {
                CheckLOS = false;
            }
            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!BaseCommand.IsAccessible(from, targeted))
                {
                    from.SendMessage(38, "Invalid Target.");
                    from.Target = new UT();
                    return;
                }
                if (targeted is Item)
                {
                    Item item = (Item)targeted;
                    if (item.Movable == true)
                    {
                        item.Movable = false;
                        if (item.Name == null)
                        {
                            from.SendMessage(2120, "Item frozen.");
                            from.Target = new UT();
                            return;
                        }
                        else
                            from.SendMessage(2120, "{0} frozen.", item.Name);
                        from.Target = new UT();
                    }
                    else if (item.Movable == false)
                    {
                        item.Movable = true;
                        if (item.Name == null)
                        {
                            from.SendMessage(2120, "Item UnFrozen.");
                            from.Target = new UT();
                            return;
                        }
                        else
                            from.SendMessage(2120, "{0} UnFrozen.", item.Name);
                        from.Target = new UT();
                    }
                }
                else if (targeted is Mobile)
                {
                    Mobile m = (Mobile)targeted;
                    if (m.Frozen == true)
                    {
                        m.Frozen = false;
                        from.SendMessage(2120, "{0} Unfrozen.", m.Name);
                        from.Target = new UT();
                       	return;
                    }
                    else if (m.Frozen == false)
                    {
                        m.Frozen = true;
                        from.SendMessage(2120, "{0} Frozen.", m.Name);
                        from.Target = new UT();
                        return;
                    }
                }
                else
                    from.SendMessage(38, "Invalid Target.");
                from.Target = new UT();
            }
        }

     
       
      
        
    }
   
       
    
}