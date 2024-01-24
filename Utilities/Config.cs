using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace TrucksLOG.Utilities
{
    public class Config
    {
        public static string LogRoot { get; internal set; }
        static readonly string Datum = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
        public static readonly string REST_PULL_USER = "https://support.truckslog.de/REST/USERDATEN/index.php";
        public static string GET_USERDATA_URL = "https://api.truckslog.de/MOMENTUM/REST/USERDATEN/GetUserData.php";



            internal static string APP_Version()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }


        internal static string GET_DOKUMENT_ROOT()
        {
            if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\TrucksLOG"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\TrucksLOG");

            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\TrucksLOG";

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

        internal static void RestartApp()
        {
            ProcessStartInfo Info = new ProcessStartInfo();
            Info.Arguments = "/C choice /C Y /N /D Y /T 1 & START \"\" \"" + Assembly.GetEntryAssembly().Location + "\"";
            Info.WindowStyle = ProcessWindowStyle.Hidden;
            Info.CreateNoWindow = true;
            Info.FileName = "TrucksLOG Momentum.exe";
            Process.Start(Info);
            Process.GetCurrentProcess().Kill();
        }

        public static int AlreadyRunning()
        {
            Process[] processCollection = Process.GetProcesses();
            var anz = 0;
            foreach (Process p in processCollection)
            {
                if (p.ProcessName == "TrucksLOG Momentum")
                    anz++;
            }
            return anz;

        }
        

        public static ulong TIMESTAMP()
        {
           return (ulong)(DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }
    }
}