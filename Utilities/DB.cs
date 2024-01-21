using GameFinder.StoreHandlers.Steam.Models;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using NLog;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Windows;
using TrucksLOG.Classes;

namespace TrucksLOG.Utilities
{
    internal class DB
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static readonly IniFile MyIni = new IniFile(@"Settings.ini");
        private static Random random = new Random();

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

        public static string RandomString(int length = 20)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
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
    
        public static int INSERT_TOUR(JobData job)
        {
            try
            {
                int ReturnType = 0;

                string strQuery = "INSERT INTO c_tourtable_ALT (steamid, tour_id, startort, startfirma, zielort, zielfirma) " +
                    "VALUES (@steamid, @tour_id, @startort, @startfirma, @zielort, @zielfirma)";

                Logger.Warn("JOB_DATA INCOMING: " + job.STARTORT + ", " + job.ZIELORT);

                var conn = new MySqlConnection(@GetAccessString());
                conn.Open();
                using var command = new MySqlCommand(strQuery, conn);
                command.Parameters.AddWithValue("steamid", MyIni.Read("STEAM_ID", "USER"));
                command.Parameters.AddWithValue("tour_id", job.INTERNE_ID);
                command.Parameters.AddWithValue("startort", job.STARTORT);
                command.Parameters.AddWithValue("startfirma", job.STARTFIRMA);
                command.Parameters.AddWithValue("zielort", job.ZIELORT);
                command.Parameters.AddWithValue("zielfirma", job.ZIELFIRMA);
                ReturnType = command.ExecuteNonQuery();
                conn.Close();
                return ReturnType;
            }
            catch (Exception ex)
            {
                Logger.Error("Fehler in INSERT_TOUR: " + ex.Message);
                return 0;
            }
        }
    }



    class Secrets()
    {
       
    }
}
