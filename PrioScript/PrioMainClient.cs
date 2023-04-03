using System;
using System.Security.Policy;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace PrioScript
{
    public class Main : BaseScript
    {
        public static bool playerSynced = false;

        public Main()
        {
            Tick += OnTick;
            EventHandlers["onClientResourceStart"] += new Action<string>(onClientResourceStart);

            RegisterCommand("citypcd10", new Action(CityPrioCd10), false);
            RegisterCommand("countypcd10", new Action(CountyPrioCd10), false);

            RegisterCommand("citypcd45", new Action(CityPrioCd45), false);
            RegisterCommand("countypcd45", new Action(CountyPrioCd45), false);

            RegisterCommand("citypcd60", new Action(CityPrioCd60), false);
            RegisterCommand("countypcd60", new Action(CountyPrioCd60), false);

            RegisterCommand("citypa", new Action(CityPrio), false);
            RegisterCommand("countypa", new Action(CountyPrio), false);
        }

        private async Task OnTick()
        {
            PrioHud.DrawText();
        }

        private void onClientResourceStart(string resources)
        {
            Player player = Game.Player;
            if (!playerSynced)
            {
                TriggerServerEvent("sync");
                playerSynced = true;
            }
        }

        [EventHandler("UpdateHud")]
        private void UpdateHud(string CityHud, string CountyHud, string cityStatus, string countyStatus)
        {
            PrioHud.UpdateHud(CityHud, CountyHud, cityStatus, countyStatus);
        }

        [EventHandler("PrioPause")]
        private void PrioPause(string zone)
        {
            PrioHud.PrioPause(zone);
        }

        private void CityPrioCd10()
        {
            if (PrioHud.CdCheck("city"))
                TriggerServerEvent("CityPrioCd", 10);
        }

        private void CityPrioCd45()
        {
            if (PrioHud.CdCheck("city"))
                TriggerServerEvent("CityPrioCd", 10);
        }

        private void CityPrioCd60()
        {
            if (PrioHud.CdCheck("city"))
                TriggerServerEvent("CityPrioCd", 10);
        }

        private void CountyPrioCd10()
        {
            if (PrioHud.CdCheck("county"))
                TriggerServerEvent("CountyPrioCd", 10);
        }

        private void CountyPrioCd45()
        {
            if (PrioHud.CdCheck("county"))
                TriggerServerEvent("CountyPrioCd", 10);
        }

        private void CountyPrioCd60()
        {
            if (PrioHud.CdCheck("county"))
                TriggerServerEvent("CountyPrioCd", 10);
        }

        private void CityPrio()
        {
            Player player = Game.Player;
            PrioHud.PrioActive(player, "city");
            TriggerServerEvent("LogToDiscord", "staff", $"{player.Name} activated priority in the city");
        }

        private void CountyPrio()
        {
            Player player = Game.Player;
            PrioHud.PrioActive(player, "county");
            TriggerServerEvent("LogToDiscord", "staff", $"{player.Name} activated priority in the county");
        }
    }
}
