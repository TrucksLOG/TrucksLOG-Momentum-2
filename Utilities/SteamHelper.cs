using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TrucksLOG.Utilities
{
    class SteamHelper
    {
        public static async void GET_PLAYER_AVATAR()
        {
            ulong STEAMID = ulong.Parse(MainWindow.MyIni.Read("STEAM_ID", "USER"));

            var webInterfaceFactory = new SteamWebInterfaceFactory("D9F5A53DC726040E65E7298C4D5A1AC0");
            var steamInterface = webInterfaceFactory.CreateSteamWebInterface<SteamUser>(new HttpClient());
            var playerSummaryResponse = await steamInterface.GetPlayerSummaryAsync(STEAMID);
            var playerSummaryData = playerSummaryResponse.Data;
            MainWindow.MyIni.Write("PROFILBILD", playerSummaryData.AvatarFullUrl, "USER");
        }


    }
}
