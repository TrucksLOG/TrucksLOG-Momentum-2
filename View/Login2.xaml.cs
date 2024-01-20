using GameFinder.RegistryUtils;
using GameFinder.StoreHandlers.Steam;
using Microsoft.Win32;
using NexusMods.Paths;
using TrucksLOG.Utilities;
using TrucksLOG.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace TrucksLOG.View
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login2 : UserControl
    {

        public static readonly IniFile MyIni = new IniFile(@"Settings.ini");

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
            var steamGame = handler.FindOneGameById(GameFinder.StoreHandlers.Steam.Models.ValueTypes.AppId.From(227300), out var errors);
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
                Filter = "American Truck Simulator (.exe)|amtrucks.exe|All Files (*.*)|*.*",
                InitialDirectory = Initial_Dir
            };

            bool? result_ats = ats.ShowDialog();
            if (result_ats == false) return;

        }


        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Content = new Login();
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            if(inp_ets_path != null) {
                MyIni.Write("ETS_PATH", inp_ets_path.Text, "GAMES");
                this.Content = new Login3();
            } else
            {
                MyIni.Write("ETS_PATH", "", "GAMES");
                INFO_STACK.Visibility = Visibility.Visible;
                LBL_INFO.Content = "Der Pfad zu Euro Truck Simulator 2 wird benötigt!";
                return;
            }
           
        }
    }
}
