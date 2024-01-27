using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TrucksLOG.Utilities;

namespace TrucksLOG.View
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : UserControl
    {
        public static readonly IniFile MyIni = new(@"Settings.ini");

        public Customers()
        {
            InitializeComponent();

            
            NICKNAME.Text = MyIni.Read("NICKNAME", "USER");
            STEAMID.Text = MyIni.Read("STEAM_ID", "USER");
            SPEDITION.Text = MyIni.Read("SPEDITION", "USER");
            RANG.Text = GET_RANG(MyIni.Read("RANG", "USER"));
            FREIGABE_TXT.Text = GET_FREIGABE(MyIni.Read("FREIGABE", "USER"));
            SPONSOR.Text = GET_SPONSOR(MyIni.Read("PATREON", "USER"));
            REM_TEXT.Text = MyIni.Read("REM", "USER") == "1" ? "REM ist aktiviert" : "REM ist deaktiviert";
            REM_ICO.Foreground = MyIni.Read("REM", "USER") == "1" ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
            BETA_TXT.Text = MyIni.Read("BETA_TESTER", "USER") == "1" ? "Du bist Beta-Tester" : "Du bist kein Beta-Tester";
            CK_TEXT.Text = MyIni.Read("CLIENT_KEY", "USER")[..25] + "...";
            ACC_DATUM.Text = Config.Timestamp2DateTime(ulong.Parse(MyIni.Read("CREATED", "USER"))).ToString() + " Uhr";
            TMP_TXT.Text = MyIni.Read("TMP_ID", "USER");

            BitmapImage bitmap = new();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(MyIni.Read("PROFILBILD", "USER"), UriKind.Absolute);
            bitmap.EndInit();

            IMG_PROFILE.ImageSource = bitmap;



        }

        private static string GET_SPONSOR(string PAT)
        {
            string ausgabe = PAT switch
            {
                "0" => "Kein Sponsor-Rang",
                "1" => "Sponsor Level 1",
                "2" => "Sponsor Level 2",
                "3" => "Sponsor Level 3",
                "4" => "Sponsor Level 4",
                "5" => "Sponsor Level 5",
                "6" => "Sponsor Level 6",
                _ => "Kein Sponsor-Rang",
            };
            return ausgabe;
        }

        private static string GET_FREIGABE(string FG)
        {
            string ausgabe = FG switch
            {
                "0" => "Account wurde noch nicht freigeschaltet!",
                "1" => "Account ist freigeschaltet",
                "3" => "Account temporär Gesperrt",
                "6" => "Account permanent Gesperrt",
                _ => "Unbekannter Account-Status!",
            };
            return ausgabe;
        }

        private static string GET_RANG(string rang)
        {
            var ausgabe = "";

            switch (rang)
            {
                case "0":
                    ausgabe = "Trucker/in";
                    break;
                case "30":
                    ausgabe = "Sponsor-Tester/in";
                    break;
                case "40":
                    ausgabe = "Beta-Tester/in";
                    break;
                case "45":
                    ausgabe = "Event-Manager/in";
                    break;
                case "50":
                    ausgabe = "Probe-Supporter/in";
                    break;
                case "60":
                    ausgabe = "Supporter/in";
                    break;
                case "70":
                    ausgabe = "Teamleiter/in";
                    break;
                case "100":
                    ausgabe = "CMO (Chief Marketing Officer)";
                    break;
                case "105":
                    ausgabe = "CPO (Chief Personal Officer)";
                    break;
                case "997":
                    ausgabe = "CTO (Chief Technical Officer)";
                    break;
                case "998":
                    ausgabe = "COO (Chief Operating Officer)";
                    break;
                case "999":
                    ausgabe = "CEO (Chief Executive Officer)";
                    break;
            }
            return ausgabe;
        }

    }

}
