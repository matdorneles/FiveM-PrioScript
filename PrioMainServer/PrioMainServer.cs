using CitizenFX.Core;
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

        string CityHud = "~y~On Hold";
        string CountyHud = "~g~Available";

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
                    CityHud = $"{status["cd"]} ({minutes} minutes)";
                    TriggerClientEvent("UpdateHud", CityHud, CountyHud, cityStatus, countyStatus);
                    await Delay(60000);
                }
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
                    CountyHud = $"{status["cd"]} ({minutes} minutes)";
                    TriggerClientEvent("UpdateHud", CityHud, CountyHud, cityStatus, countyStatus);
                    await Delay(60000);
                }
            }
        }
    }
}
