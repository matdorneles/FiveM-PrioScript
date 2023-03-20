using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using System.Collections.Generic;
using System.Security.Policy;

namespace PrioScript
{
    public class PrioHud : BaseScript
    {
        public static Dictionary<string, string> status = new Dictionary<string, string>()
        {
            { "available", "~g~Available" },
            { "onHold", "~y~On Hold" },
            { "cd", "~y~min Cooldown" },
            { "active", "~r~Active" }
        };

        public static bool visible = false;

        private static readonly string cityPrioHud = $"~w~City Priority:";
        private static readonly string countyPrioHud = $"~w~County Priority:";

        private static string cityStatus = status["available"];
        private static string countyStatus = status["available"];

        private static string cityStatusHud = status["available"];
        private static string countyStatusHud = status["available"];

        private static bool cityIsPrioActive = false;
        private static Player cityActivePrioPlayer;
        private static bool countyIsPrioActive = false;
        private static Player countyActivePrioPlayer;

        public static void PrioPause(string zone)
        {
            if (zone == "city")
            {
                if (cityStatus == status["available"])
                {
                    cityStatus = status["onHold"];
                    cityStatusHud = cityStatus;
                }
                else
                {
                    cityStatus = status["available"];
                    cityStatusHud = cityStatus;
                }
            }

            if (zone == "county")
            {
                if (countyStatus == status["available"])
                {
                    countyStatus = status["onHold"];
                    countyStatusHud = countyStatus;
                }
                else
                {
                    countyStatus = status["available"];
                    countyStatusHud = countyStatus;
                }
            }
        }

        public static async void PrioCd(string zone, int minutes)
        {
            if (zone == "city")
            {
                for (int i = minutes; i > 0; i--)
                {
                    cityStatus = status["cd"];
                    cityStatusHud = $"~b~{i} {cityStatus}";
                    await Delay(60000);
                }
                cityStatus = status["available"];
                cityStatusHud = cityStatus;
            }

            if (zone == "county")
            {
                for (int x = minutes; x > 0; x--)
                {
                    countyStatus = status["cd"];
                    countyStatusHud = $"~b~{x} {countyStatus}";
                    await Delay(60000);
                }

                countyStatus = status["available"];
                countyStatusHud = countyStatus;
            }
        }

        public static void PrioActive(Player sourcePlayer, string zone)
        {
            if (zone == "city")
            {
                cityActivePrioPlayer = sourcePlayer;
                if (cityIsPrioActive == false)
                {
                    cityIsPrioActive = true;
                    cityStatus = status["active"];
                    cityStatusHud = $"{cityStatus} {cityActivePrioPlayer.Name}";
                }
                else if (cityIsPrioActive && cityActivePrioPlayer.Handle == Game.Player.Handle || cityActivePrioPlayer.IsPlaying == false)
                {
                    cityIsPrioActive = false;
                    cityStatus = status["available"];
                }
                else if (cityIsPrioActive && cityActivePrioPlayer.Handle != Game.Player.Handle)
                {
                    API.ClearPrints();
                    API.SetTextEntry_2("STRING");
                    API.AddTextComponentString($"There is already an ~r~active ~w~priority in the ~b~{zone}!");
                    API.DrawSubtitleTimed(3000, true);
                }
            }
        }

        public static void DrawText()
        {
            API.SetTextFont(0);
            API.SetTextScale(0.0f, 0.3f);
            API.SetTextColour(128, 128, 128, 255);
            API.SetTextDropshadow(0, 0, 0, 0, 255);
            API.SetTextDropShadow();
            API.SetTextOutline();
            API.SetTextEntry("STRING");
            API.AddTextComponentString($"{cityPrioHud} {cityStatusHud}");
            API.DrawText(0.168f, 0.8725f);

            API.SetTextFont(0);
            API.SetTextScale(0.0f, 0.3f);
            API.SetTextColour(128, 128, 128, 255);
            API.SetTextDropshadow(0, 0, 0, 0, 255);
            API.SetTextDropShadow();
            API.SetTextOutline();
            API.SetTextEntry("STRING");
            API.AddTextComponentString($"{countyPrioHud} {countyStatusHud}");
            API.DrawText(0.168f, 0.8520f);
        }
    }
}
