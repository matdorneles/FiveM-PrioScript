using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace PrioScript
{
    public class Main : BaseScript
    {
        public static bool playerSynced;

        public Main()
        {
            Tick += OnTick;
            EventHandlers["onClientResourceStart"] += new Action<string>(onClientResourceStart);
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

        [EventHandler("PrioActive")]
        private void PrioActive([FromSource] Player player, string zone)
        {
            PrioHud.PrioActive(player, zone);
        }
    }
}
