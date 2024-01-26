using GameFinder.RegistryUtils;
using GameFinder.StoreHandlers.Steam;
using Microsoft.Win32;
using NexusMods.Paths;
using TrucksLOG.Utilities;
using TrucksLOG.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using GameFinder.Common;
using System.IO;
using System.Windows.Forms.VisualStyles;
using System.Text;

namespace TrucksLOG.View
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login2 : UserControl
    {

        public static readonly IniFile MyIni = new(@"Settings.ini");

        public Login2()
        {

            InitializeComponent();

            INFO_STACK.Visibility = Visibility.Collapsed;

            if ( SearchETS_Path() != null )
            {
                inp_ets_path.Text = SearchETS_Path();
                inp_ets_path.Visibility = Visibility.Collapsed;
                LBL_ETS_PATH.FontSize = 16;
                LBL_ETS_PATH.Content = "Wir haben den Pfad gefunden!" + Environment.NewLine + inp_ets_path.Text;
                BrowseFileETS.Visibility = Visibility.Collapsed;
                CHECK_ICON.Visibility = Visibility.Visible;
            } else
            {
                CHECK_ICON.Visibility = Visibility.Collapsed;
            }

        }

        public static string SearchETS_Path()
        {

            var handler = new SteamHandler(FileSystem.Shared, OperatingSystem.IsWindows() ? WindowsRegistry.Shared : null);
            var steamGame = handler.FindOneGameById(GameFinder.StoreHandlers.Steam.Models.ValueTypes.AppId.From(227300), out _);
            if (steamGame != null)
            {
                return steamGame.Path.ToString() + @"\bin\win_x64\eurotrucks2.exe";
            }
            else
            {
                return null;
            }
        }

        private void BrowseFileETS_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var Initial_Dir = @"C:\";

            var strSteamInstallPath = REG.Loading_STEAM_Path();
            if (strSteamInstallPath != null)
            {
                Initial_Dir = strSteamInstallPath.ToString().Replace("/", "\\") + @"\steamapps\common\";
            }

            OpenFileDialog ats = new()
            {
                Filter = "Euro Truck Simulator 2 (.exe)|eurotrucks2.exe|All Files (*.*)|*.*",
                InitialDirectory = Initial_Dir
            };

            bool? result_ats = ats.ShowDialog();
            if (result_ats == false) return;

        }


        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Content = new Login();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(inp_ets_path != null) {

                INSERT_TELEMETRY(inp_ets_path.Text.Replace(@"\", @"/"));
                MyIni.Write("ETS_PATH", inp_ets_path.Text, "GAMES");

                this.Content = new Login3();
            } else
            {
                INFO_STACK.Visibility = Visibility.Visible;
                LBL_INFO.Content = "Der Pfad zu Euro Truck Simulator 2 wird benötigt!";
                return;
            }
           
        }

        private static bool INSERT_TELEMETRY(string PATH)
        {
            try
            {
                string firstPath = PATH.Replace("/eurotrucks2.exe", "");

                if (Directory.Exists(firstPath + "/plugins"))
                {
                    MainWindow.Logger.Info("ETS2 Path: " + firstPath + @"/plugins" + " exists!");
                    File.Copy(@"Assets/scs-telemetry.dll", firstPath + @"/plugins/scs-telemetry.dll", true);
                }
                else
                {
                    MainWindow.Logger.Info("ETS2 Path " + firstPath + @"/plugins" + " not exist! We create Directory: " + firstPath + @"/plugins");
                    Directory.CreateDirectory(firstPath + @"/plugins");
                    File.Copy("Assets/scs-telemetry.dll", firstPath + @"/plugins/scs-telemetry.dll", true);
                }
                return true;
            }
            catch (Exception ex)
            {
                MainWindow.Logger.Error("Telemetry ETS2 Error: " + ex.Message);
                return false;
            }
        }
    }
}
