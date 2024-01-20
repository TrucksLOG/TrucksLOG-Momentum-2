using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using System;


namespace TrucksLOG.Utilities
{
    public class REST
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static string URL = "https://support.truckslog.de/REST";
        public static readonly IniFile MyIni = new IniFile(@"Settings.ini");


        private static string GetAccessToken()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            IConfiguration configuration = configurationBuilder.AddUserSecrets<Userdaten>().Build();
            return configuration.GetSection("github")["accessToken"];
        }

        public static bool LOAD_USERDATA(ulong STEAM_ID)
        {
            try
            {
                if (STEAM_ID > 10)
                {
                    var client = new RestClient(URL);
                    var request = new RestRequest("USERDATEN/", Method.Post);
                    request.AddParameter("SECRET", GetAccessToken());
                    request.AddParameter("STEAM_ID", STEAM_ID);
                    var response = client.Execute(request);
                    if (response != null)
                    {
                        dynamic json = JsonConvert.DeserializeObject(response.Content);
                        MyIni.Write("NICKNAME", json.nickname.ToString(), "USER");
                        MyIni.Write("SPEDITION", json.in_spedition.ToString(), "USER");
                        MyIni.Write("FREIGABE", json.freigabe.ToString(), "USER");
                        MyIni.Write("BETA_TESTER", json.beta_tester.ToString(), "USER");

                        Logger.Info("Found Data in REST!  Freigabe: " + json.freigabe + ", Nickname: " + json.nickname + ", VTC: " + json.in_spedition + ", BETA: " + json.beta_tester);
                        return true;
                    }
                    else
                    {
                        Logger.Info("Keine Daten aus Load_Nickname erhalten!");
                        return false;
                    }
                } else
                {
                    Logger.Info("STEAM-ID ist kleiner als 10, demnach muss Sie falsch sein!");
                    return false;
                }
            } catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
          
        }

        class Userdaten
        {
            private string nickname { get; set; }
            private string eMail { get; set; }
            private string SteamID { get; set; }

        }
    }

}
