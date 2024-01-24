using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrucksLOG.Utilities
{
    class GameHelper
    {

        internal class ProcessHelper
        {
            public static bool IsRunning(string name) => Process.GetProcessesByName(name).Length > 0;

            public static bool Check_MP()
            {
                Process G_ETS = Process.GetProcessesByName("eurotrucks2").FirstOrDefault<Process>();
                Process G_ATS = Process.GetProcessesByName("amtrucks").FirstOrDefault<Process>();

                if (G_ETS == null && G_ATS == null)
                    return false;

                if (G_ETS != null && G_ETS.MainWindowTitle.Contains("Multiplayer"))
                    return true;

                if (G_ATS != null && G_ATS.MainWindowTitle.Contains("Multiplayer"))
                    return true;

                return false;
            }
            public static bool IsDirectoryEmpty(string path)
            {
                return !Directory.EnumerateFileSystemEntries(path).Any();
            }

        }


        private void Checke_Telemetry_ETS(string ETS_PATH)
        {
            try
            {
                if (ProcessHelper.IsRunning("eurotrucks2.exe"))
                {
                    MainWindow.Logger.Warn("Telemetry nicht kopiert weil ETS2 läuft...");
                }
                else
                {
                    if (File.Exists(ETS_PATH))
                    {
                        var Ordner_ETS = ETS_PATH;
                        var Ordner2 = Ordner_ETS.Substring(0, Ordner_ETS.Length - 15);
                        if (!Directory.Exists(Ordner2 + @"plugins"))
                            Directory.CreateDirectory(Ordner2 + @"plugins");

                        File.Copy(@"Resources\scs-telemetry.dll", Ordner2 + "plugins\\scs-telemetry.dll", true);
                    }
                }
            }
            catch
            {
                MainWindow.Logger.Info("Telemetry ETS was not copied because the game is running");
            }
        }
    }
}
