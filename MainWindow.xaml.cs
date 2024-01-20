using AutoUpdaterDotNET;
using DiscordRPC;
using DiscordRPC.Logging;
using NLog;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using TrucksLOG.Utilities;

namespace TrucksLOG
{
    public partial class MainWindow : Window
    {
        public DiscordRpcClient client;
        public static readonly IniFile MyIni = new IniFile(@"Settings.ini");
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();

            if (!Directory.Exists(Environment.SpecialFolder.CommonDocuments + @"\TrucksLOG"))
                Directory.CreateDirectory(Environment.SpecialFolder.CommonDocuments + @"\TrucksLOG");


            var d = DateTime.Now;
            string FileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\TrucksLOG\Log_" + d.Day.ToString("00") + "_" + d.Month.ToString("00") + "_" + d.Year.ToString() + ".txt";
            File.WriteAllText(FileName, string.Empty);


            Logger.Info("App wurde gestartet!");

            MyIni.Write("DOK_ROOT", Config.GET_DOKUMENT_ROOT());

            client = new DiscordRpcClient("730374187025170472");
            DB db = new();

            client.Logger = new ConsoleLogger() { Level = DiscordRPC.Logging.LogLevel.Warning };

            client.OnReady += (sender, e) =>
            {
                Console.WriteLine("Received Ready from user {0}", e.User.Username);
            };

            client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("Received Update! {0}", e.Presence);
            };

            client.Initialize();
            client.SetPresence(new RichPresence()
            {
                Details = "TrucksLOG Momentum",
                State = Config.APP_Version() +  "Work in Progress...",
                Assets = new Assets()
                {
                    LargeImageKey = "",
                    LargeImageText = "",
                    SmallImageKey = ""
                }
            });


            var timer = new System.Timers.Timer(150);
            timer.Elapsed += (sender, args) => { client.Invoke(); };
            timer.Start();


        }


        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        if (e.ChangedButton == MouseButton.Left)
            this.DragMove();
           
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Update.CLIENT_UPDATE();

            if (MyIni.KeyExists("STEAM_ID", "USER") && ulong.Parse(MyIni.Read("STEAM_ID", "USER")) > 10)
            {
                REST.LOAD_USERDATA(ulong.Parse(MyIni.Read("STEAM_ID", "USER")));
            }
        }
    }
}
