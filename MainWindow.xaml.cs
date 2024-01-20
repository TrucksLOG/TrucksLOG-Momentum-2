using AutoUpdaterDotNET;
using DiscordRPC;
using DiscordRPC.Logging;
using NLog;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TrucksLOG.Utilities;

namespace TrucksLOG
{
    public partial class MainWindow : Window
    {
        public DiscordRpcClient client;
        public static readonly IniFile MyIni = new IniFile(@"Settings.ini");
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();

            if (!Directory.Exists(Environment.SpecialFolder.CommonDocuments + @"\TrucksLOG"))
                Directory.CreateDirectory(Environment.SpecialFolder.CommonDocuments + @"\TrucksLOG");
           

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
                State =  Utilities.Config.APP_Version() +  "Work in Progress...",
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

        public static void GOTO_URL(string path)
        {
            new Process
            {
                StartInfo = new ProcessStartInfo(path)
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AutoUpdater.UpdateFormSize = new System.Drawing.Size(1000, 1200);
            AutoUpdater.Mandatory = true;
            AutoUpdater.ReportErrors = false;
            AutoUpdater.Icon = Properties.Resources.Thommy_64_64;
            AutoUpdater.UpdateMode = Mode.Normal;
            AutoUpdater.RunUpdateAsAdmin = true;
            AutoUpdater.Start("https://api.truckslog.de/MOMENTUM/update_version.xml");
        }
    }
}
