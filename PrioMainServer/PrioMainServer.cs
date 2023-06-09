﻿using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;
using System.Collections.Generic;

namespace PrioScript
{
    internal class PrioMainServer : BaseScript
    {
        public static Dictionary<string, string> status = new Dictionary<string, string>()
        {
            { "av", "~g~Available" },
            { "oh", "~y~On Hold" },
            { "cd", "~b~Cooldown" },
            { "ac", "~r~Active" }
        };

        private static string cityStatus = "~y~On Hold";
        private static string countyStatus = "~g~Available";

        private static Player CityPlayer;
        private static Player CountyPlayer;

        static string CityHud = "~y~On Hold";
        static string CountyHud = "~g~Available";

        public PrioMainServer()
        {
            RegisterCommand("citypriop", new Action(CityPrioPause), true);
            RegisterCommand("countypriop", new Action(CountyPrioPause), true);
        }

        [EventHandler("sync")]
        public void Sync()
        {
            TriggerClientEvent("UpdateHud", CityHud, CountyHud, cityStatus, countyStatus);
        }

        [EventHandler("UpdateHud")]
        public void UpdateHud(string newCityHud, string newCountyHud, string newCityStatus, string newCountyStatus)
        {
            CityHud = newCityHud;
            CountyHud = newCountyHud;
            cityStatus = newCityStatus;
            countyStatus = newCountyStatus;
            TriggerClientEvent("UpdateHud", CityHud, CountyHud, cityStatus, countyStatus);
        }

        public void CityPrioPause()
        {
            TriggerClientEvent("PrioPause", "city");
        }

        public void CountyPrioPause()
        {
            TriggerClientEvent("PrioPause", "county");
        }

        [EventHandler("CityPrioCd")]
        public async void CityPrioCd(int minutes)
        {
            if (cityStatus == status["ac"])
            {
                cityStatus = status["cd"];
                for (int i = minutes; i > -1; i--)
                {
                    if (cityStatus == status["av"])
                    {
                        break;
                    }
                    CityHud = $"{status["cd"]} ({i} minutes)";
                    TriggerClientEvent("UpdateHud", CityHud, CountyHud, cityStatus, countyStatus);
                    await Delay(60000);
                }
                cityStatus = status["av"];
                CityHud = status["av"];
                TriggerClientEvent("UpdateHud", CityHud, CountyHud, cityStatus, countyStatus);
            }
        }

        [EventHandler("CountyPrioCd")]
        public async void CountyPrioCd(int minutes)
        {
            if (countyStatus == status["ac"])
            {
                countyStatus = status["cd"];
                for (int i = minutes; i > -1; i--)
                {
                    if (countyStatus == status["av"])
                    {
                        break;
                    }
                    CountyHud = $"{status["cd"]} ({i} minutes)";
                    TriggerClientEvent("UpdateHud", CityHud, CountyHud, cityStatus, countyStatus);
                    await Delay(60000);
                }
                countyStatus = status["av"];
                CountyHud = status["av"];
                TriggerClientEvent("UpdateHud", CityHud, CountyHud, cityStatus, countyStatus);
            }
        }

        [EventHandler("ActivatePrio")]
        public static void PrioActive([FromSource] Player sourcePlayer, string zone)
        {
            if (zone == "city" && cityStatus == status["av"])
            {
                cityStatus = status["ac"];
                CityPlayer = sourcePlayer;
                CityHud = $"{status["ac"]} ({CityPlayer.Name})";
                TriggerClientEvent("UpdateHud", CityHud, CountyHud, cityStatus, countyStatus);
                TriggerClientEvent("UpdatePrioPlayer", "city", sourcePlayer.Name);
            }
            else if (zone == "county" && countyStatus == status["av"])
            {
                countyStatus = status["ac"];
                CountyPlayer = sourcePlayer;
                CountyHud = $"{status["ac"]} ({CountyPlayer.Name})";
                TriggerClientEvent("UpdateHud", CityHud, CountyHud, cityStatus, countyStatus);
                TriggerClientEvent("UpdatePrioPlayer", "county", sourcePlayer.Name);
            }
        }
    }
}
