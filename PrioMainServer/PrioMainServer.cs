using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;

namespace PrioScript
{
    internal class PrioMainServer : BaseScript
    {
        public PrioMainServer()
        {
            RegisterCommand("citypriop", new Action(CityPrioPause), false);
            RegisterCommand("countypriop", new Action(CountyPrioPause), false);

            RegisterCommand("citypcd10", new Action(CityPrioCd), false);
            RegisterCommand("countypcd10", new Action(CountyPrioCd), false);
        }

        public void CityPrioPause()
        {
            TriggerClientEvent("PrioPause", "city");
        }

        public void CountyPrioPause()
        {
            TriggerClientEvent("PrioPause", "county");
        }

        public void CityPrioCd()
        {
            TriggerClientEvent("PrioCd", "city", 3);
        }

        public void CountyPrioCd()
        {
            TriggerClientEvent("PrioCd", "county", 3);
        }
    }
}
