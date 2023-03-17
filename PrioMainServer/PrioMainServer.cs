using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;

namespace PrioScript
{
    internal class PrioMainServer : BaseScript
    {
        public PrioMainServer() {
            RegisterCommand("priohud", new Action(Hud), false);
        }

        private void Hud()
        {
            TriggerClientEvent("TogglePrioHud", PrioConfig.config["zone1"], PrioConfig.config["zone2"]);
        }
    }
}
