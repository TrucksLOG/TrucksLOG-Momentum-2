using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using TrucksLOG.Utilities;

namespace TrucksLOG.View
{
    public partial class Login5 : System.Windows.Controls.UserControl
    {
        public static readonly IniFile MyIni = new(@"Settings.ini");

        public Login5()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new Login4();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(MyIni.KeyExists("LENKUNG", "USER"))
            {
                if(System.Windows.Forms.MessageBox.Show("Zum Abschluss der Einrichtung muss der Client neu gestartet werden...", "Confirm Restart", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    Config.RestartApp();
                }
            } else
            {
                System.Windows.Forms.MessageBox.Show("Du musst eine Auswahl treffen!");
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
