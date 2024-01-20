using SteamUserInfo;
using System.Windows;
using System.Windows.Controls;
using TrucksLOG.Utilities;

namespace TrucksLOG.View
{
    public partial class Login : UserControl
    {

        public static readonly IniFile MyIni = new IniFile(@"Settings.ini");

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
            ulong STEAM = ulong.Parse(inp_steam_id.Text);

            if (STEAM > 10)
            {
                if(REST.Load_Nickname(ulong.Parse(inp_steam_id.Text)))
                {
                    MyIni.Write("STEAM_ID", STEAM.ToString(), "USER");
                    this.Content = new Login2();
                } else
                {
                    MessageBox.Show("Fehler beim Abfragen der STEAM-ID!\n\nBitte überprüfe deine Steam-ID auf der Webseite und bei der Eingabe.");
                }

            } else
            {
                MessageBox.Show("Fehler beim Abfragen der STEAM-ID!\n\nBitte überprüfe deine Steam-ID auf der Webseite und bei der Eingabe.");
                return;
            }
            
        }
    }
}
