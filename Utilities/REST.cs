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
        public static readonly IniFile MyIni = new IniFile(@"Settings.ini");
        public static string URL = @"https://support.truckslog.de/REST/USERDATEN/";
        private string _connectionString = string.Format("Server={0}; database={1}; User ID={2}; password={3};", "85.214.100.220", "k103238web_truckslog_29_01_21", "k103238web_ThomT", "Nj[Z4uU]@&5yrFH8X?Y19HUT(726aR{E0^A");

        public static string GetAccessToken()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            IConfiguration configuration = configurationBuilder.AddUserSecrets<Userdaten>().Build();
            return configuration.GetSection("github")["accessToken"];
        }

        public static bool LOAD_USERDATA(ulong STEAM_ID)
        {
            try
            {
 
                var client = new RestClient();
                var request = new RestRequest(@"https://support.truckslog.de/REST/USERDATEN/index.php", Method.Post);
                request.AddParameter("SECRET", GetAccessToken());
                request.AddParameter("STEAM_ID", STEAM_ID);
                var response = client.Execute(request);
                if (response != null)
                {
                    Logger.Info("RESPONSE: " + response.Content + " A " + GetAccessToken());

                    dynamic json = JsonConvert.DeserializeObject(response.Content);
                    MyIni.Write("NICKNAME", json.nickname.ToString(), "USER");
                    MyIni.Write("SPEDITION", json.in_spedition.ToString(), "USER");
                    MyIni.Write("FREIGABE", json.freigabe.ToString(), "USER");
                    MyIni.Write("BETA_TESTER", json.beta_tester.ToString(), "USER");
                  
                    Logger.Info("Found Data in REST!  Freigabe: " + json.freigabe.ToString() + ", Nickname: " + json.nickname.ToString() + ", VTC: " + json.in_spedition.ToString() + ", BETA: " + json.beta_tester.ToString());
                    return true;
                } else
                {
                    return false;
                }
            } catch (Exception ex)
            {
                Logger.Error("ERROR @LOAD_USERDATEN: " + ex.Message + ex.StackTrace + URL);
                return false;
            }
          
        }



        public void LOAD_USERDATA_2(ulong STEAM_ID)
        {
    
           
           
  
  
        }


        class Userdaten
        {

        }
    }

}
