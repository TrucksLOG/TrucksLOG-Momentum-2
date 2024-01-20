using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using TrucksLOG.Utilities;

namespace TrucksLOG.View
{
    public partial class Login4 : UserControl
    {

        public static readonly IniFile MyIni = new IniFile(@"Settings.ini");
        public Login4()
        {
            InitializeComponent();

            if(REG.Loading_TMP_Path() != null)
            {
                inp_tmp_path.Text = REG.Loading_TMP_Path() + @"\TruckersMP-Launcher.exe";
                inp_tmp_path.Visibility = Visibility.Collapsed;
                LBL_TMP_PFAD.Content = "Der Pfad wurde gefunden in:" + Environment.NewLine + inp_tmp_path.Text;
                LBL_INFO.Content = "Wir haben den TruckersMP Client gefunden!";
                BrowseFileTMP.Visibility = Visibility.Collapsed;
                CHECK_ICON.Visibility = Visibility.Visible;
            } else
            {
                CHECK_ICON.Visibility = Visibility.Collapsed;
                LBL_INFO.Content = "Wir konnten den TruckersMP Client leider nicht automatisch finden!";
            }
        }

        public string FilePath { get; private set; }


        private void BrowseFileTMP_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            OpenFileDialog tmp = new()
            {
                Filter = "TruckersMP-Launcher (.exe)|TruckersMP-Launcher.exe|All Files (*.*)|*.*",
                InitialDirectory = @"C:\"
            };

            bool? result_tmp = tmp.ShowDialog();
            if (result_tmp == false) return;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Content = new Login3();
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            MyIni.Write("TMP_PATH", inp_tmp_path.Text, "GAMES");
            this.Content = new Login5();
        }
    }
}
