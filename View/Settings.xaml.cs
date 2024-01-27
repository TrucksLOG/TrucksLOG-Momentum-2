using ControlzEx.Standard;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using TrucksLOG.Utilities;
using WK.Libraries.BootMeUpNS;


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
            CLIENT_AUTORUN.IsOn = MyIni.Read("AUTORUN", "SETTINGS") == "1";
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
            if (sender is ToggleSwitch toggleSwitch)
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
            if (sender is ToggleSwitch toggleSwitch)
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

        private void CLIENT_AUTORUN_Toggled(object sender, System.Windows.RoutedEventArgs e)
        {
            var bootMeUp = new BootMeUp
            {
                UseAlternativeOnFail = true,
                BootArea = BootMeUp.BootAreas.Registry,
                TargetUser = BootMeUp.TargetUsers.CurrentUser
            };

            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn == true)
                {
                    MyIni.Write("AUTORUN", "1", "SETTINGS");
                    bootMeUp.Enabled = true;
                }
                else
                {
                    MyIni.Write("AUTORUN", "0", "SETTINGS");
                    bootMeUp.Enabled = false;
 
                }
            }
        }


        private async void SHOW_INFO_TOPMOST_Click(object sender, RoutedEventArgs e)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            await metroWindow.ShowMessageAsync("Information", "Bei dieser Option kannst du Einstellen, ob der Client immer im Vordergrund bleibt oder nicht. Wenn der Switch aktiviert ist, bleibt der Client, egal was du machst, immer im Vordergrund. Hilfreich wenn man nur einen Monitor hat, aber den Client immer sehen möchte.");

        }

        private async void SHOW_INFO_LOG_Click(object sender, RoutedEventArgs e)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            await metroWindow.ShowMessageAsync("Information", "Hier kannst du Auswählen, ob beim Starten die Log-Datei geleert wird. Wenn der Switch aktiviert ist, wird jedes mal beim Client Start zuerst die Log Datei geleert bevor der Client gestartet wird, andernfalls wird bis zum nächsten Tag alles nacheinander in die Log-Datei geschrieben. Dies ist beim Testen äusserst Hilfreich.");

        }

        private async void SHOW_INFO_AUTORUN_Click(object sender, RoutedEventArgs e)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            await metroWindow.ShowMessageAsync("Information", "Bei dieser Option kannst du Einstellen, ob der Client zusammen mit dem Betriebssystem gestartet wird. Damit musst du den Client beim Rechner start nicht nochmal extra einschalten.");

        }
    }
}
