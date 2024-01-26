using ControlzEx.Standard;
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
        public static readonly IniFile MyIni = new(@"Settings.ini");

        public Settings()
        {
            InitializeComponent();

            CLIENT_TOPMOST.IsOn = MyIni.Read("TOPMOST", "SETTINGS") == "1";
            ARGUMENTS_ETS2.Text = MyIni.Read("ETS_ARGUMENTS", "GAMES");
            ARGUMENTS_ATS.Text = MyIni.Read("ATS_ARGUMENTS", "GAMES");

            if (MyIni.KeyExists("DEL_LOG", "SETTINGS") && MyIni.Read("DEL_LOG", "SETTINGS") == "1")
            {
                LOG_DEL_SWITCH.IsOn = true;
            } else
            {
                LOG_DEL_SWITCH.IsOn = false;
            }
        }


        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            static void action() => File.Delete("Settings.ini");
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
            static void action() => Process.Start("notepad.exe", "Settings.ini");
            action();
        }

        private void CLIENT_TOPMOST_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                {
                    MyIni.Write("TOPMOST", "1", "SETTINGS");
                }
                else
                {
                    MyIni.Write("TOPMOST", "0", "SETTINGS");
                }
            }
        }

        private void Button_Click_2(object sender, System.Windows.RoutedEventArgs e)
        {
            Config.GOTO_URL("https://gist.github.com/Bluscream/15d58187b357ed1f10ab1372110ac576");
        }

        private void Button_Click_3(object sender, System.Windows.RoutedEventArgs e)
        {
            Config.GOTO_URL("https://gist.github.com/Bluscream/15d58187b357ed1f10ab1372110ac576");
        }

        private void ARGUMENTS_ETS2_TextChanged(object sender, TextChangedEventArgs e)
        {
            MyIni.Write("ETS_ARGUMENTS", ARGUMENTS_ETS2.Text, "GAMES");
        }

        private void ARGUMENTS_ATS_TextChanged(object sender, TextChangedEventArgs e)
        {
            MyIni.Write("ATS_ARGUMENTS", ARGUMENTS_ATS.Text, "GAMES");
        }
    }
}
