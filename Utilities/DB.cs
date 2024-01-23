using Newtonsoft.Json;
using NLog;
using RestSharp;
using System;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace TrucksLOG.Utilities
{
    internal class DB
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static readonly IniFile MyIni = new IniFile(@"Settings.ini");
        private static Random random = new Random();

        public static string RandomString(int length = 20)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public static string LOAD_USERDATA(ulong STEAM_ID)
        {
            try
            {
                var client = new RestClient(@"https://api.truckslog.de/MOMENTUM/REST/USERDATEN/GetUserData.php");
                client.AddDefaultQueryParameter("STEAM", STEAM_ID.ToString());
                var response = client.Execute(new RestRequest(), Method.Post);
                if(String.IsNullOrEmpty(response.Content))
                {
                    return "Keine Daten gefunden!";
                }

                var json = JsonConvert.DeserializeObject<Userdaten>(response.Content);
                MyIni.Write("NICKNAME", json.nickname, "USER");
                MyIni.Write("SPEDITION", json.in_spedition, "USER");
                MyIni.Write("RANG", json.rang.ToString(), "USER");
                MyIni.Write("PATREON", json.patreon.ToString(), "USER");
                MyIni.Write("FREIGABE", json.freigabe.ToString(), "USER");
                MyIni.Write("PROFILBILD", json.profilbild, "USER");
                MyIni.Write("REM", json.REM.ToString(), "USER");
                MyIni.Write("BETA_TESTER", json.beta_tester.ToString(), "USER");
                return "OK";

            }
            catch (Exception ex)
            {
                Logger.Error("ERROR @LOAD_USERDATEN: " + ex.Message + ex.StackTrace);
                return ex.Message;
            }

        }




    }



}
