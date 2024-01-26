using GameFinder.RegistryUtils;
using GameFinder.StoreHandlers.Steam;
using Microsoft.Win32;
using NexusMods.Paths;
using TrucksLOG.Utilities;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;

namespace TrucksLOG.View
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login3 : UserControl
    {

        public static readonly IniFile MyIni = new(@"Settings.ini");
        public Login3()
        {
            InitializeComponent();
            if(SearchATS_Path() != null)
            {
                inp_ats_path.Text = SearchATS_Path();
                inp_ats_path.IsEnabled = false;
                LBL_ATS_PFAD.Content = "Der Pfad wurde gefunden in:" + Environment.NewLine + inp_ats_path.Text;
                LBL_INFO.Content = "Wir haben das Spiel gefunden!";
                BrowseFileATS.Visibility = Visibility.Collapsed;
                CHECK_ICON.Visibility = Visibility.Visible;
            } else
            {
                CHECK_ICON.Visibility = Visibility.Collapsed;
                LBL_INFO.Content = "Wir konnten das Spiel leider nicht automatisch finden!";
            }
        }

        public string FilePath { get; private set; }

        public static string SearchATS_Path()
        {
            var handler = new SteamHandler(FileSystem.Shared, OperatingSystem.IsWindows() ? WindowsRegistry.Shared : null);
            var steamGame = handler.FindOneGameById(GameFinder.StoreHandlers.Steam.Models.ValueTypes.AppId.From(270880), out _);
            if (steamGame != null)
            {
                return steamGame.Path.ToString() + @"\bin\win_x64\amtrucks.exe";
            }
            else
            {
                return null;
            }
        }

        private void BrowseFileATS_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string InitialDirectory, InitialDirectory2;

            string InstallPath = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", null);
            if (InstallPath != null)
            {
                InitialDirectory2 = InstallPath.Replace("/", "\\");
                InitialDirectory = Directory.Exists(InitialDirectory2 + @"\steamapps\common\American Truck Simulator") ? InitialDirectory2 + @"\steamapps\common\American Truck Simulator" : InitialDirectory2 + @"\steamapps\common";
            }
            else
            {
                InitialDirectory = @"C:\";
            }

            OpenFileDialog ats = new()
            {
                Filter = "American Truck Simulator (.exe)|amtrucks.exe|All Files (*.*)|*.*",
                InitialDirectory = InitialDirectory
            };

            bool? result_ats = ats.ShowDialog();
            if (result_ats == false) return;

            MessageBox.Show(ats.FileName);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Content = new Login2();
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            if(inp_ats_path != null)
            {
                INSERT_TELEMETRY(inp_ats_path.Text.Replace(@"\", @"/"));
                MyIni.Write("ATS_PATH", inp_ats_path.Text, "GAMES");
            }
            this.Content = new Login4();
        }

        private static bool INSERT_TELEMETRY(string PATH)
        {
            try
            {
                string firstPath = PATH.Replace("/amtrucks.exe", "");

                if (Directory.Exists(firstPath + @"/plugins"))
                {
                    MainWindow.Logger.Info("ATS Path " + firstPath + @"/plugins" + " exists!");
                    File.Copy(@"Assets/scs-telemetry.dll", firstPath + @"/plugins/scs-telemetry.dll", true);
                }
                else
                {
                    MainWindow.Logger.Info("ATS Path " + firstPath + @"/plugins" + " not exist! We create Directory: " + firstPath + @"/plugins");
                    Directory.CreateDirectory(firstPath + @"/plugins");
                    File.Copy("Assets/scs-telemetry.dll", firstPath + @"/plugins/scs-telemetry.dll", true);
                }
                return true;
            } catch (Exception ex)
            {
                MainWindow.Logger.Error("Telemetry ATS Error: " + ex.Message);
                return false;
            }
        }
    }
}
