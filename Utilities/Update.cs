using AutoUpdaterDotNET;
using NLog;
using System;

namespace TrucksLOG.Utilities
{
    public class Update
    {
        public static readonly IniFile MyIni = new IniFile(@"Settings.ini");
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void CLIENT_UPDATE()
        {
            if (MyIni.KeyExists("STEAM_ID", "USER"))
            {
                AutoUpdater.Mandatory = true;
                AutoUpdater.ReportErrors = false;
                AutoUpdater.Icon = Properties.Resources.Thommy_64_64;
                AutoUpdater.UpdateMode = Mode.Normal;
                AutoUpdater.RunUpdateAsAdmin = true;
                if (CHECK_BETA() != null)
                {
                    Logger.Info("UPDATE_PATH: " + CHECK_BETA());
                    AutoUpdater.Start(CHECK_BETA());
                }
            }
        }

        internal static string CHECK_BETA()
        {
            try
            {
                if (MyIni.KeyExists("BETA_TESTER", "USER"))
                {
                    return MyIni.Read("BETA_TESTER", "USER") == "1" ? "https://api.truckslog.de/MOMENTUM/CLIENT_BETA/update_version.xml" : "https://api.truckslog.de/MOMENTUM/CLIENT_USER/update_version.xml";
                }
                else
                {
                    Logger.Warn("No BETA Entry in Ini-File");
                    return null;
                }
            } catch (Exception e)
            {
                Logger.Error("Fehler in CHECK_BETA", e.Message);
                return null;
            }


        }



    }

  
}
