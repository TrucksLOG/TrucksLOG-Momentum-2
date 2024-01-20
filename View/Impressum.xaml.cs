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
    }
     
}
