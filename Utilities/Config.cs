using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Windows.Web.Http;

namespace TrucksLOG.Utilities
{
    public class Config
    {
        public static string LogRoot { get; internal set; }
        public static readonly string REST_PULL_USER = "https://support.truckslog.de/REST/USERDATEN/index.php";
        public static readonly string GET_USERDATA_URL = "https://api.truckslog.de/MOMENTUM/REST/USERDATEN/GetUserData.php";


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
            ProcessStartInfo Info = new()
            {
                Arguments = "/C choice /C Y /N /D Y /T 1 & START \"\" \"" + Assembly.GetEntryAssembly().Location + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "TrucksLOG Momentum.exe"
            };
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


        public static string Timestamp2DateTime(double unixTimeStamp)
        {
            DateTime dtDateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            try
            {
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
                return dtDateTime.ToString("dd.MM.yyyy H:mm");

            }
            catch (Exception ex)
            {
                MainWindow.Logger.Error("Fehler in Timestamp2Datetime " + ex.Message + ex.Source);
                return "";
            }
        }

    }
}