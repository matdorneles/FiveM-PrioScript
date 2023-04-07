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

            RegisterCommand("cityp10", new Action(CityPrioCd10), false);
            RegisterCommand("county10", new Action(CountyPrioCd10), false);

            RegisterCommand("city45", new Action(CityPrioCd45), false);
            RegisterCommand("county45", new Action(CountyPrioCd45), false);

            RegisterCommand("city60", new Action(CityPrioCd60), false);
            RegisterCommand("county60", new Action(CountyPrioCd60), false);

            RegisterCommand("cityps", new Action(CityPrio), false);
            RegisterCommand("countyps", new Action(CountyPrio), false);
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

        [EventHandler("UpdatePrioPlayer")]
        private void UpdatePrioPlayer(string zone, Player sourcePlayer)
        {
            if (zone == "city")
                PrioHud.cityPlayerName = sourcePlayer.Name;
            else if (zone == "county")
                PrioHud.countyPlayerName = sourcePlayer.Name;
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
                TriggerServerEvent("CityPrioCd", 45);
        }

        private void CityPrioCd60()
        {
            if (PrioHud.CdCheck("city"))
                TriggerServerEvent("CityPrioCd", 60);
        }

        private void CountyPrioCd10()
        {
            if (PrioHud.CdCheck("county"))
                TriggerServerEvent("CountyPrioCd", 10);
        }

        private void CountyPrioCd45()
        {
            if (PrioHud.CdCheck("county"))
                TriggerServerEvent("CountyPrioCd", 45);
        }

        private void CountyPrioCd60()
        {
            if (PrioHud.CdCheck("county"))
                TriggerServerEvent("CountyPrioCd", 60);
        }

        private void CityPrio()
        {
            if (PrioHud.PrioCheck("city"))
            {
                TriggerServerEvent("ActivatePrio", "city");
            }
        }

        private void CountyPrio()
        {
            if (PrioHud.PrioCheck("county"))
            {
                TriggerServerEvent("ActivatePrio", "county");
            }
        }
    }
}
