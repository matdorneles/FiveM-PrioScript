using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;

namespace PrioScript
{
    internal class PrioMainServer : BaseScript
    {
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

            RegisterCommand("citypa", new Action(CityPrio), false);
            RegisterCommand("countypa", new Action(CountyPrio), false);
        }

        [EventHandler("playerIds")]
        private void GetPlayerIds([FromSource]Player player)
        {
            int playerIds = GetNumPlayerIdentifiers(player.Handle);
            for (int i = 0; i < playerIds; i++)
            {
                Debug.WriteLine(GetPlayerIdentifier(player.Handle, i));
            }
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
            for (int i = 10; i > -1; i--)
            {
                TriggerClientEvent("PrioCd", "city", i);
                await Delay(60000);
            }
        }

        public async void CityPrioCd45()
        {
            for (int i = 45; i > -1; i--)
            {
                TriggerClientEvent("PrioCd", "city", i);
                await Delay(60000);
            }
        }

        public async void CityPrioCd60()
        {
            for (int i = 60; i > -1; i--)
            {
                TriggerClientEvent("PrioCd", "city", i);
                await Delay(60000);
            }
        }

        public async void CountyPrioCd10()
        {
            for (int i = 10; i > -1; i--)
            {
                TriggerClientEvent("PrioCd", "county", i);
                await Delay(60000);
            }
        }

        public async void CountyPrioCd45()
        {
            for (int i = 45; i > -1; i--)
            {
                TriggerClientEvent("PrioCd", "county", i);
                await Delay(60000);
            }
        }

        public async void CountyPrioCd60()
        {
            for (int i = 60; i > -1; i--)
            {
                TriggerClientEvent("PrioCd", "county", i);
                await Delay(60000);
            }
        }

        public void CityPrio()
        {
            TriggerClientEvent("PrioActiveCity");
        }

        public void CountyPrio()
        {
            TriggerClientEvent("PrioActiveCounty");
        }
    }
}
