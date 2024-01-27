using GameFinder.StoreHandlers.Steam.Models;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using System;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using TrucksLOG.Classes;
using static SCSSdkClient.Object.SCSTelemetry;
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
                Logger.Info("TOUR_CHECK Response: " + resp.Content);
                return resp.Content;

            } catch
            {
                return "FALSE";
            }
        }

        internal static void TOUR_INSERT(JobData job)
        {
            var client = new RestClient(@"https://api.truckslog.de/MOMENTUM/REST/TOUR/Start_Tour.php");
            client.AddDefaultQueryParameter("STEAM", MyIni.Read("STEAM_ID", "USER").ToString());
            client.AddDefaultQueryParameter("SPEDITION", MyIni.Read("SPEDITION", "USER").ToString());
            client.AddDefaultQueryParameter("NICKNAME", MyIni.Read("NICKNAME", "USER").ToString());
            client.AddDefaultQueryParameter("CK", MyIni.Read("CLIENT_KEY", "USER").ToString());
            client.AddDefaultQueryParameter("STARTORT", job.STARTORT);
            client.AddDefaultQueryParameter("STARTFIRMA", job.STARTFIRMA);
            client.AddDefaultQueryParameter("ZIELORT", job.ZIELORT);
            client.AddDefaultQueryParameter("ZIELFIRMA", job.ZIELFIRMA);
            client.AddDefaultQueryParameter("LADUNG", job.LADUNG);
            client.AddDefaultQueryParameter("GEWICHT", job.GEWICHT.ToString());
            client.AddDefaultQueryParameter("STRECKE", job.GESAMT_STRECKE.ToString());
            client.AddDefaultQueryParameter("FRACHTMARKT", job.FRACHTMARKT.ToString());
            client.AddDefaultQueryParameter("SPIEL", job.SPIEL.ToString());
            client.AddDefaultQueryParameter("ODO_START", job.ODO_START.ToString());
            client.AddDefaultQueryParameter("STATUS", "Auf Fahrt");
            client.AddDefaultQueryParameter("EINKOMMEN", job.EINKOMMEN.ToString());
            var resp = client.Execute(new RestRequest(), Method.Post);

            Logger.Info("STARTING TOUR: " + resp.Content);
            REG.Write("TOUR_ID", resp.Content.ToString());
            REG.Write("Starting_City", job.STARTORT);
            REG.Write("Destination_City", job.ZIELORT);
            REG.Write("Source_Company", job.STARTFIRMA);
            REG.Write("Destination_Company", job.ZIELFIRMA);
            REG.Write("Cargo", job.GEWICHT.ToString());
            REG.Write("Route", job.GESAMT_STRECKE.ToString());
           
        }


        internal static string TOUR_END(JobData job)
        {
            var client = new RestClient(@"https://api.truckslog.de/MOMENTUM/REST/TOUR/End_Tour.php");
            client.AddDefaultQueryParameter("TOUR_ID", MyIni.Read("TOUR_ID", "AKTUELLE_TOUR").ToString());
            client.AddDefaultQueryParameter("STEAM", MyIni.Read("STEAM_ID", "USER").ToString());
            client.AddDefaultQueryParameter("FRACHTSCHADEN", job.FRACHTSCHADEN.ToString());
            client.AddDefaultQueryParameter("EINNAHMEN", job.EINNAHMEN.ToString());
            client.AddDefaultQueryParameter("GEF_STRECKE", job.JOB_GEF_STRECKE.ToString());
            client.AddDefaultQueryParameter("REST_KM", job.REST_STRECKE.ToString());
            client.AddDefaultQueryParameter("ODO_END", job.ODO_ENDE.ToString());

            var resp = client.Execute(new RestRequest(), Method.Post);
            Logger.Info("Ending TOUR: " + resp.Content);
            return resp.Content;
        }


        internal static void CANCEL_TOUR()
        {
            var client = new RestClient(@"https://api.truckslog.de/MOMENTUM/REST/TOUR/Cancel_Tour.php");
            client.AddDefaultQueryParameter("TOUR_ID", REG.Read("TOUR_ID"));
            client.AddDefaultQueryParameter("STEAM", MyIni.Read("STEAM_ID", "USER").ToString());

            var resp = client.Execute(new RestRequest(), Method.Post);
            Logger.Info("Cancel TOUR: " + resp.Content);
        }
    }



}
