using Microsoft.Extensions.Configuration;
using MySqlConnector;
using NLog;
using System;
using System.Data;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Windows;

namespace TrucksLOG.Utilities
{
    internal class DB
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        internal static string Get_ConnectionString()
        {
            return @"Server=85.214.100.220;User ID=k103238web_ThomT;Password=Nj[Z4uU]@&5yrFH8X?Y19HUT(726aR{{E0^A;Database=k103238web_truckslog_29_01_21";
        }

        public static string GetAccessString()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            IConfiguration configuration = configurationBuilder.AddUserSecrets<Secrets>().Build();
            return configuration.GetSection("validate")["accessString"];
        }


        public static void LOAD_USERDATA(ulong STEAM_ID)
        {
                try
                {
                string strQuery = "SELECT nickname, beta_tester, in_spedition, freigabe FROM user WHERE steamid = " + STEAM_ID;

                var conn = new MySqlConnection(@GetAccessString());
                conn.Open();
                    using var command = new MySqlCommand(strQuery, conn);
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                    MainWindow.MyIni.Write("STEAM_ID", STEAM_ID.ToString(), "USER");
                    MainWindow.MyIni.Write("NICKNAME", reader["nickname"].ToString(), "USER");
                    MainWindow.MyIni.Write("FREIGABE", reader["freigabe"].ToString(), "USER");
                    MainWindow.MyIni.Write("SPEDITION", reader["in_spedition"].ToString(), "USER");
                    MainWindow.MyIni.Write("BETA", reader["beta_tester"].ToString(), "USER");
                    }
                conn.Close();
                }
                catch (Exception ex)
                {
                   Logger.Error("Fehler in Lade_NUM_TEXTE: " + ex.Message);
                }
            }
          
        }

    class Secrets()
    {
       
    }
}
