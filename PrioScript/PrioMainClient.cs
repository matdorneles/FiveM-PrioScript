using System;
using System.Security.Policy;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace PrioScript
{
    public class Main : BaseScript
    {
        public static bool playerSynced;

        public Main()
        {
            Tick += OnTick;
            EventHandlers["onClientResourceStart"] += new Action<string>(onClientResourceStart);


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
                playerSynced = false;
                TriggerServerEvent("sync");
                playerSynced = true;
                TriggerServerEvent("playerIds");
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

        [EventHandler("PrioCd")]
        private void PrioCd(string zone, int minutes)
        {
            PrioHud.PrioCd(zone, minutes);
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
