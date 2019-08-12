using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using Server.Commands;
using Server.Items;
using Server.Targeting;

namespace Server.Misc
{
    public class ForceServerCrash : Timer
    {
        public static bool Enabled = false; // is the script enabled?

        private static TimeSpan CrashTime = TimeSpan.FromHours(2.0);
        private static TimeSpan CrashDelay = TimeSpan.FromMinutes(5.0);

        private static TimeSpan WarningDelay = TimeSpan.FromMinutes(1.0);

        private static bool m_CrashServer;
        private static DateTime m_CrashTime;

        public static bool CrashServer
        {
            get { return m_CrashServer; }
        }

        public static void Initialize()
        {
            CommandSystem.Register("fsc", AccessLevel.Administrator, new CommandEventHandler(fsc_OnCommand));
            new ForceServerCrash().Start();
        }

        public static void fsc_OnCommand(CommandEventArgs e)
        {
            if (m_CrashServer)
            {
                e.Mobile.SendMessage("The server is in the process of crashing.");
            }
            else
            {
                e.Mobile.SendMessage("You have initiated a server crash.");
                Enabled = true;
                m_CrashTime = DateTime.Now;
            }
        }

        public ForceServerCrash()
            : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0))
        {
            Priority = TimerPriority.FiveSeconds;

            m_CrashTime = DateTime.Now.Date + CrashTime;

            if (m_CrashTime < DateTime.Now)
                m_CrashTime += TimeSpan.FromDays(1.0);
        }

        private void Warning_Callback()
        {
            World.Broadcast(0x22, true, "A server crash has been initiated, please find a safe place to log out.");
        }

        private void ForceServerCrash_Callback()
        {
            object obj = null; obj.ToString();
            Core.Process.Kill();
        }

        protected override void OnTick()
        {
            if (m_CrashServer || !Enabled)
                return;

            if (DateTime.Now < m_CrashTime)
                return;

            if (WarningDelay > TimeSpan.Zero)
            {
                Warning_Callback();
                Timer.DelayCall(WarningDelay, WarningDelay, new TimerCallback(Warning_Callback));
            }

            AutoSave.Save();
            m_CrashServer = true;

            Timer.DelayCall(CrashDelay, new TimerCallback(ForceServerCrash_Callback));
        }
    }
}