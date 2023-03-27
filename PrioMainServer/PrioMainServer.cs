using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;
using NDCore.Functions;

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
            RegisterCommand("citypriop", new Action(CityPrioPause), false);
            RegisterCommand("countypriop", new Action(CountyPrioPause), false);

            RegisterCommand("citypcd10", new Action(CityPrioCd10), false);
            RegisterCommand("countypcd10", new Action(CountyPrioCd), false);

            RegisterCommand("citypa", new Action(CityPrio), false);
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

        public void CountyPrioCd()
        {
            TriggerClientEvent("PrioCd", "county", 3);
        }

        public void CityPrio()
        {
            TriggerClientEvent("PrioActive", "city");
        }
    }
}
