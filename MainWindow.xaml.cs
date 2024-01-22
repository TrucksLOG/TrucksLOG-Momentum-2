using AutoUpdaterDotNET;
using DiscordRPC;
using DiscordRPC.Logging;
using NLog;
using System;
using System.IO;
using System.Security.Policy;
using System.Threading;
using System.Windows;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;
using System.Windows.Shapes;
using TrucksLOG.Utilities;
using TrucksLOG.View;


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


            if(Config.AlreadyRunning() > 1)
            {
                MessageBox.Show("Der Client kann nur einmal gestartet werden!");
                Logger.Warn("Client wurde mehrfach gestartet!");
                Environment.Exit(0);
            }
               
          


            if (!Directory.Exists(Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ) + @"\TrucksLOG"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\TrucksLOG");

            if (MyIni.KeyExists("DEL_LOG", "SETTINGS") && MyIni.Read("DEL_LOG", "SETTINGS") == "1")
            {
                LogFile.CLEAR_LOG();
            }

            Logger.Info("App Version " + Config.APP_Version() + " wurde gestartet!");
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
          try
            {
               if (e.ChangedButton == MouseButton.Left)
                  this.DragMove();

            } catch { }
 
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            if (MyIni.KeyExists("STEAM_ID", "USER"))
            {
                Logger.Info("Update Client Result: " + Update.CLIENT_UPDATE());

                DB.LOAD_USERDATA(ulong.Parse(MyIni.Read("STEAM_ID", "USER")));
              

                Navigation_Panel.Visibility = Visibility.Visible;
                Image_Panel.Visibility = Visibility.Collapsed;

            } else
            {
                Navigation_Panel.Visibility = Visibility.Collapsed;
                Image_Panel.Visibility= Visibility.Visible;
            }
        }



    }
}
