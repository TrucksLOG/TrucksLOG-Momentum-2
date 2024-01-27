using Microsoft.Win32;
using NLog;
using System;
using System.Windows.Media.Imaging;

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

        public static void Write(string KEY, string VALUE)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Valve\Properties");
            key.SetValue(KEY, VALUE);
            key.Close();
        }


        public static string Read(string KEY)
        {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Valve\Properties");
                var returning = "";
                if (key != null)
                {
                    returning = key.GetValue(KEY).ToString();
                    key.Close();
                }
                return returning;
        }

        public static void Delete_Key()
        {
            try
            {
                RegistryKey versie1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Valve", true);
                versie1.DeleteSubKeyTree("Properties");
                versie1.Close();
            } catch (Exception)
            {
                MainWindow.Logger.Error("Fehler beim Löschen des Keys!");
            }
        }

        public static bool CHECK_TOUR()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Properties", false))
            {
               return key == null ? false : true;
            }
        }

    }
}
