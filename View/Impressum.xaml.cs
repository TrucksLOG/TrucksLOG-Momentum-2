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

        private void AllClear_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Config.GOTO_URL("https://pixabay.com/de/users/allclear55-1703624/");
        }

        private void AHurricane_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Config.GOTO_URL("https://steamcommunity.com/profiles/76561197993488680");
        }

        private void MahApps_ICONS_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Config.GOTO_URL("https://github.com/MahApps/MahApps.Metro.IconPacks");
        }

        private void SETUP_STEAM_PIC_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Config.GOTO_URL("https://alphacoders.com/users/profile/95419/MrROBERTOX");
        }

        private void SETUP_CONFIG_PIC_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Config.GOTO_URL("https://pixabay.com/de/users/thomaswolter-92511/");
        }
    }
     
}
