using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;

namespace CustomsFramework.Systems.ShrinkSystem
{
    public class ShrinkSystemCore : BaseCore
    {
        public const String SystemVersion = "1.0.0";

        private static ShrinkSystemCore m_Core;
        public static ShrinkSystemCore Core { get { return m_Core; } }

        public ShrinkSystemCore() : base()
        {
            this.Enabled = true;
            BaseCore.OnEnabledChanged += BaseCore_OnEnabledChanged;
        }

        public ShrinkSystemCore(CustomSerial serial) : base(serial)
        {
        	BaseCore.OnEnabledChanged += BaseCore_OnEnabledChanged;
        }
        
        private void BaseCore_OnEnabledChanged(BaseCoreEventArgs e)
        {
        	if ( ShrinkSystemCore.Core.Enabled)
			{	
			}
        }

        public override String Name { get { return "Shrink System Core"; } }
        public override String Description { get { return "Core that contains everything for the Shrink System."; } }
        public override String Version { get { return SystemVersion; } }
        public override AccessLevel EditLevel { get { return AccessLevel.Developer; } }
        public override Gump SettingsGump { get { return null; } }

        public static void Initialize()
        {
            ShrinkSystemCore core = World.GetCore(typeof(ShrinkSystemCore)) as ShrinkSystemCore;

            if (core == null)
            {
                core = new ShrinkSystemCore();

                core.Prep();
            }

            m_Core = core;
            
            CommandSystem.Register("SSToggle", AccessLevel.Developer, new CommandEventHandler(SSToggle_OnCommand));
        }

        // Called after all cores are loaded
        public override void Prep()
        {
        	return;
        }
        
        [Usage("SSToggle")]
        [Description("Toggles the shrink system on and off.")]
        public static void SSToggle_OnCommand(CommandEventArgs e)
        {
            if (ShrinkSystemCore.Core == null)
                return;

            if ( ShrinkSystemCore.Core.Enabled )
            {
            	ShrinkSystemCore.Core.Enabled = false;
            	e.Mobile.SendMessage( "You have disabled the shrink system." );
            }
            else
            {
            	ShrinkSystemCore.Core.Enabled = true;
            	e.Mobile.SendMessage( "you have enabled the shrink system." );
            }
            	
        }
        
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            Utilities.WriteVersion(writer, 0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            Int32 version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    break;
            }
        }
    }
}