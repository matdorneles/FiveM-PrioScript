using System;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace PrioScript
{
    public class Main : BaseScript
    {
        public Main()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            PrioHud.DrawText();
        }

        [EventHandler("TogglePrioHud")]
        private void Hud(string zone1, string zone2)
        {
            PrioHud.ToogleHud(zone1, zone2);
            Player player = Game.Player;
        }
    }
}
