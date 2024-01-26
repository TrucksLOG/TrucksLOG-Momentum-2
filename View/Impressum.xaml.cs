using System.Diagnostics;
using System.Windows.Controls;
using TrucksLOG.Utilities;

namespace TrucksLOG.View
{
    public partial class Impressum : UserControl
    {
        public Impressum()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Config.GOTO_URL("https://truckslog.de/?s=SYSTEM/impressum");
        }

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Config.GOTO_URL("https://www.1001freefonts.com/de/good-times.font");
        }

        private void TextBlock_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Config.GOTO_URL("https://www.dafont.com/nikodecs.font");

        }

        private void TextBlock_MouseDown_2(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Config.GOTO_URL("https://www.1001freefonts.com/de/rubik.font");
        }
    }
     
}
