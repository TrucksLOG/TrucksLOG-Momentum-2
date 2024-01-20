using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace TrucksLOG.Utilities
{
    public class Config
    {
        public static string LogRoot { get; internal set; }
        static readonly string Datum = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
        public static readonly string REST_PULL_USER = "https://support.truckslog.de/REST/USERDATEN/index.php";

        internal static string APP_Version()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }


        internal static string GET_DOKUMENT_ROOT()
        {
            if(!Directory.Exists(Environment.SpecialFolder.MyDocuments + @"\TrucksLOG"))
                Directory.CreateDirectory(Environment.SpecialFolder.MyDocuments + @"\TrucksLOG");

            return Environment.SpecialFolder.MyDocuments + @"\TrucksLOG";

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



    }
}