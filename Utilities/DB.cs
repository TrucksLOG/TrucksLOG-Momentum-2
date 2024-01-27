using GameFinder.StoreHandlers.Steam.Models;
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
using TrucksLOG.Classes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace TrucksLOG.Utilities
{
    internal class DB
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static readonly IniFile MyIni = new(@"Settings.ini");
        private static readonly Random random = new();

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
                var client = new RestClient(Config.GET_USERDATA_URL);
                client.AddDefaultQueryParameter("STEAM", STEAM_ID.ToString());
                var response = client.Execute(new RestRequest(), Method.Post);
                if(String.IsNullOrEmpty(response.Content))
                {
                    return "Keine Daten gefunden!";
                }

                var json = JsonConvert.DeserializeObject<Userdaten>(response.Content);
                MyIni.Write("STEAM_ID", STEAM_ID.ToString(), "USER");
                MyIni.Write("NICKNAME", json.Nickname, "USER");
                MyIni.Write("SPEDITION", json.In_spedition, "USER");
                MyIni.Write("RANG", json.Rang.ToString(), "USER");
                MyIni.Write("PATREON", json.Patreon.ToString(), "USER");
                MyIni.Write("FREIGABE", json.Freigabe.ToString(), "USER");
                MyIni.Write("PROFILBILD", json.Profilbild, "USER");
                MyIni.Write("REM", json.REM.ToString(), "USER");
                MyIni.Write("BETA_TESTER", json.Beta_tester.ToString(), "USER");
                MyIni.Write("CLIENT_KEY", json.Client_key.ToString(), "USER");
                MyIni.Write("CREATED", json.Created.ToString(), "USER");
                MyIni.Write("TMP_ID", json.Truckers_mp.ToString(), "USER");
                return "OK";

            }
            catch (Exception ex)
            {
                Logger.Error("ERROR @LOAD_USERDATEN: " + ex.Message + ex.StackTrace);
                return ex.Message;
            }

        }


        public static string CHECK_TOUR(string startort, string startfirma, string zielort, string zielfirma, string ladung, float gewicht)
        {
            try
            {
                var client = new RestClient(@"https://api.truckslog.de/MOMENTUM/REST/TOUR/Check_Tour.php");
                client.AddDefaultQueryParameter("STEAM", MyIni.Read("STEAM_ID", "USER").ToString());
                client.AddDefaultQueryParameter("STARTORT",startort);
                client.AddDefaultQueryParameter("STARTFIRMA", startfirma);
                client.AddDefaultQueryParameter("ZIELORT", zielort);
                client.AddDefaultQueryParameter("ZIELFIRMA", zielfirma);
                client.AddDefaultQueryParameter("LADUNG", ladung);
                client.AddDefaultQueryParameter("GEWICHT", gewicht.ToString());
                var resp = client.Execute(new RestRequest(), Method.Post);
                Logger.Info(resp.Content);
                return resp.Content;

            } catch
            {
                return "FALSE";
            }
        }

        internal static void TOUR_INSERT(JobData job)
        {
            var client = new RestClient(@"https://api.truckslog.de/MOMENTUM/REST/TOUR/Check_Tour.php");
            client.AddDefaultQueryParameter("STEAM", MyIni.Read("STEAM_ID", "USER").ToString());
            client.AddDefaultQueryParameter("STARTORT", job.STARTORT);
            client.AddDefaultQueryParameter("STARTFIRMA", job.STARTFIRMA);
            client.AddDefaultQueryParameter("ZIELORT", job.ZIELORT);
            client.AddDefaultQueryParameter("ZIELFIRMA", job.ZIELFIRMA);
            client.AddDefaultQueryParameter("LADUNG", job.LADUNG);
            client.AddDefaultQueryParameter("GEWICHT", job.GEWICHT.ToString());


            var resp = client.Execute(new RestRequest(), Method.Post);
            Logger.Info("INSERT TOR: " + resp.Content);

        }
    }



}
