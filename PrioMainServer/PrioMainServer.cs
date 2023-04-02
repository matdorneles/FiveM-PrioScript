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

            RegisterCommand("citypcd10", new Action(CityPrioCd10), false);
            RegisterCommand("countypcd10", new Action(CountyPrioCd10), false);

            RegisterCommand("citypcd45", new Action(CityPrioCd45), false);
            RegisterCommand("countypcd45", new Action(CountyPrioCd45), false);

            RegisterCommand("citypcd60", new Action(CityPrioCd60), false);
            RegisterCommand("countypcd60", new Action(CountyPrioCd60), false);

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

        public async void CityPrioCd10()
        {
            if (cityStatus == status["ac"] &&  cityStatus != status["cd"])
            {
                for (int i = 10; i > -1; i--)
                {
                    TriggerClientEvent("PrioCd", "city", i);
                    await Delay(60000);
                }
            }
        }

        public async void CityPrioCd45()
        {
            if (cityStatus == status["ac"] && cityStatus != status["cd"])
            {
                for (int i = 45; i > -1; i--)
                {
                    TriggerClientEvent("PrioCd", "city", i);
                    await Delay(60000);
                }
            }
        }

        public async void CityPrioCd60()
        {
            if (cityStatus == status["ac"] && cityStatus != status["cd"])
            {
                for (int i = 60; i > -1; i--)
                {
                    TriggerClientEvent("PrioCd", "city", i);
                    await Delay(60000);
                }
            }
        }

        public async void CountyPrioCd10()
        {
            if (countyStatus == status["ac"] &&  countyStatus != status["cd"])
            {
                for (int i = 10; i > -1; i--)
                {
                    TriggerClientEvent("PrioCd", "county", i);
                    await Delay(60000);
                }
            }
        }

        public async void CountyPrioCd45()
        {
            if (countyStatus == status["ac"] && countyStatus != status["cd"])
            {
                for (int i = 45; i > -1; i--)
                {
                    TriggerClientEvent("PrioCd", "county", i);
                    await Delay(60000);
                }
            }
        }

        public async void CountyPrioCd60()
        {
            if (countyStatus == status["ac"] && countyStatus != status["cd"])
            {
                for (int i = 60; i > -1; i--)
                {
                    TriggerClientEvent("PrioCd", "county", i);
                    await Delay(60000);
                }
            }
        }
    }
}
