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

    }
}
