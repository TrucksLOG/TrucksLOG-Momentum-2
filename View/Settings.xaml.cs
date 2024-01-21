using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using TrucksLOG.Utilities;

namespace TrucksLOG.View
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public static readonly IniFile MyIni = new IniFile(@"Settings.ini");

        public Settings()
        {
            InitializeComponent();

            if(MyIni.KeyExists("DEL_LOG", "SETTINGS") && MyIni.Read("DEL_LOG", "SETTINGS") == "1")
            {
                LOG_DEL_SWITCH.IsOn = true;
            } else
            {
                LOG_DEL_SWITCH.IsOn = false;
            }
        }


        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Action action = () => File.Delete("Settings.ini");
            action();
            Config.RestartApp();
        }

        private void ToggleSwitch_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                {
                    MyIni.Write("DEL_LOG", "1", "SETTINGS");
                }
                else
                {
                    MyIni.Write("DEL_LOG", "0", "SETTINGS");
                }
            }
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            Action action = () =>  Process.Start("notepad.exe", "Settings.ini");
            action();
        }
    }
}
