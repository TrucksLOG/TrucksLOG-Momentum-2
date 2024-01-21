using NLog;
using SteamUserInfo;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TrucksLOG.Utilities;

namespace TrucksLOG.View
{
    public partial class Login : UserControl
    {

        public static readonly IniFile MyIni = new IniFile(@"Settings.ini");
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        public Login()
        {
            InitializeComponent();

            SearchSTEAM_ID();
        }



        public async void SearchSTEAM_ID()
        {

            ISteamUserLoader loader = new SteamUserDefaultLoader();
            if (await loader.IsSteamInstalledAsync())
            {
                var allUsers = await loader.LoadUsersAsync();
                var activeUser = await loader.LoadActiveUserAsync();
                long userid = 0;

                foreach (var user in allUsers)
                {
                    bool isActive = activeUser == user;
                    userid = user.Id64;
                }
                inp_steam_id.Text = userid.ToString();
            } else
            {
                inp_steam_id.Text = null;
            }

        }

    

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Weiter_BTN.Visibility = Visibility.Collapsed;
            ulong STEAM = ulong.Parse(inp_steam_id.Text);

            if (STEAM > 10)
            {
                MyIni.Write("STEAM_ID", STEAM.ToString(), "USER");

                DB.LOAD_USERDATA(STEAM);

                this.Content = new Login2();

            } else
            {
                MessageBox.Show("Fehler beim Abfragen der STEAM-ID!\n\nBitte überprüfe deine Steam-ID auf der Webseite und bei der Eingabe.");
                return;
            }
            
        }

        private void inp_steam_id_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Wird noch eingebaut!");
        }

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           Config.GOTO_URL("https://pixabay.com/de/users/allclear55-1703624/");
        }
    }
}
