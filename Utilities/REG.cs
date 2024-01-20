using Microsoft.Win32;
using System;

namespace TrucksLOG.Utilities
{
    class REG
    {
        public static string Loading_STEAM_Path()
        {
            try
            {
                using var myKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Valve\Steam");
                return myKey.GetValue("SteamPath").ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string Loading_TMP_Path()
        {
            try
            {
                string MyKey = null;
                RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\TruckersMP");
                if(myKey != null)
                    MyKey = (string)myKey.GetValue("InstallLocation");

                return MyKey?? null;
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}
