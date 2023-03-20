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
    }
}
