using System.Windows;
using System.Windows.Controls;
using TrucksLOG.Utilities;

namespace TrucksLOG.View
{
    public partial class Login5 : UserControl
    {
        public static readonly IniFile MyIni = new IniFile(@"Settings.ini");

        public Login5()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(MyIni.KeyExists("LENKUNG", "USER"))
            {
                this.Content = new Home();
            }
        }

        private void TASTATUR_Checked(object sender, RoutedEventArgs e)
        {
            MyIni.Write("LENKUNG", "TASTATUR", "USER");
        }

        private void LENKRAD_Checked(object sender, RoutedEventArgs e)
        {
            MyIni.Write("LENKUNG", "LENKRAD", "USER");
        }
    }
}
