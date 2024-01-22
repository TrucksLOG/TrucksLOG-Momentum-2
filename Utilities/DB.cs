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
            return GetAccessString();
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

                var conn = new MySqlConnection(Get_ConnectionString());
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using var command = new MySqlCommand(strQuery, conn);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MainWindow.MyIni.Write("STEAM_ID", STEAM_ID.ToString(), "USER");
                    MainWindow.MyIni.Write("NICKNAME", reader["nickname"].ToString(), "USER");
                    MainWindow.MyIni.Write("FREIGABE", reader["freigabe"].ToString(), "USER");
                    MainWindow.MyIni.Write("SPEDITION", reader["in_spedition"].ToString(), "USER");
                    MainWindow.MyIni.Write("BETA_TESTER", reader["beta_tester"].ToString(), "USER");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Fehler in LOAD_USERDATA: " + ex.Message);
            }
        }

        public static long CHECK_TOUR(JobData job)
        {
            try
            {
                string strQuery = "SELECT id FROM c_tourtable_ALT WHERE steamid = @steamid AND startort = @startort AND startfirma = @startfirma AND zielort = @zielort AND zielfirma = @zielfirma AND einkommen = @einkommen AND ladung = @ladung";


                using (var conn = new MySqlConnection(Get_ConnectionString()))
                {
                    conn.Open();
                    using (var command = new MySqlCommand(strQuery, conn))
                    {
                        command.Parameters.AddWithValue("steamid", MyIni.Read("STEAM_ID", "USER"));
                        command.Parameters.AddWithValue("startort", job.STARTORT);
                        command.Parameters.AddWithValue("startfirma", job.STARTFIRMA);
                        command.Parameters.AddWithValue("zielort", job.ZIELORT);
                        command.Parameters.AddWithValue("zielfirma", job.ZIELFIRMA);
                        command.Parameters.AddWithValue("einkommen", job.EINKOMMEN);
                        command.Parameters.AddWithValue("ladung", job.LADUNG);
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error("Fehler in CHECK_TOUR: " + ex.Message);
                return 0;
            }
        }


        public static long INSERT_TOUR(JobData job)
        {
            try
            {
                string strQuery = "INSERT INTO c_tourtable_ALT (steamid, tour_id, startort, startfirma, zielort, zielfirma, einkommen, ladung, gewicht) VALUES (@steamid, @tour_id, @startort, @startfirma, @zielort, @zielfirma, @einkommen, @ladung, @gewicht)";

                var conn = new MySqlConnection(Get_ConnectionString());
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using var command = new MySqlCommand(strQuery, conn);
                command.Parameters.AddWithValue("steamid", MyIni.Read("STEAM_ID", "USER"));
                command.Parameters.AddWithValue("tour_id", RandomString());
                command.Parameters.AddWithValue("startort", job.STARTORT);
                command.Parameters.AddWithValue("startfirma", job.STARTFIRMA);
                command.Parameters.AddWithValue("zielort", job.ZIELORT);
                command.Parameters.AddWithValue("zielfirma", job.ZIELFIRMA);
                command.Parameters.AddWithValue("einkommen", job.EINKOMMEN);
                command.Parameters.AddWithValue("ladung", job.LADUNG);
                command.Parameters.AddWithValue("gewicht", job.GEWICHT);
                command.ExecuteNonQuery();
                var returnID = command.LastInsertedId; 
                conn.Close();
                return returnID;
            }
            catch (Exception ex)
            {
                Logger.Error("Fehler in INSERT_TOUR: " + ex.Message);
                return 0;
            }
        }


        public static int END_TOUR(long TOUR_ID)
        {
            try
            {
                string strQuery = "UPDATE c_tourtable_ALT SET status = 'Abgeschlossen' WHERE tour_id = @tourid AND steamid = @steamid";

                var conn = new MySqlConnection(Get_ConnectionString());
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                using var command = new MySqlCommand(strQuery, conn);
                command.Parameters.AddWithValue("tourid", TOUR_ID);
                command.Parameters.AddWithValue("steamid", MyIni.Read("STEAM_ID", "USER"));
                var returnId = command.ExecuteNonQuery();
                conn.Close();
                return returnId;
            }
            catch (Exception ex)
            {
                Logger.Error("Fehler in END_TOUR: " + ex.Message);
                return 0;
            }
        }


        public static int ABORT_TOUR(long TOUR_ID)
        {
            try
            {
                string strQuery = "UPDATE c_tourtable_ALT SET status = 'Abgebrochen' WHERE tour_id = @tourid AND steamid = @steamid";

                var conn = new MySqlConnection(Get_ConnectionString());
                if(conn.State == ConnectionState.Closed)
                    conn.Open();

                using var command = new MySqlCommand(strQuery, conn);
                command.Parameters.AddWithValue("tourid", TOUR_ID);
                command.Parameters.AddWithValue("steamid", MyIni.Read("STEAM_ID", "USER"));
                var returnId = command.ExecuteNonQuery();
                conn.Close();
                return returnId;
            }
            catch (Exception ex)
            {
                Logger.Error("Fehler in END_TOUR: " + ex.Message);
                return 0;
            }
        }


    }



    class Secrets()
    {
       
    }
}
