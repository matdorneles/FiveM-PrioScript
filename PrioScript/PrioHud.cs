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
            { "av", "~g~Available" },
            { "oh", "~y~On Hold" },
            { "cd", "~b~Cooldown" },
            { "ac", "~r~Active" }
        };

        private static string cityStatus;
        private static string countyStatus;

        private static string cityHud;
        private static string countyHud;

        private static Player cityPrioPlayer;
        private static Player countyPrioPlayer;

        public static void UpdateHud(string NewCityHud, string NewCountyHud, string newCityStatus, string newCountyStatus)
        {
            cityHud = NewCityHud;
            countyHud = NewCountyHud;
            cityStatus = newCityStatus;
            countyStatus = newCountyStatus;
        }

        public static void PrioPause(string zone)
        {
            if (zone == "city")
            {
                if (cityStatus == status["av"])
                {
                    cityStatus = status["oh"];
                    cityHud = status["oh"];
                    TriggerServerEvent("UpdateHud", cityHud, countyHud, cityStatus, countyStatus);
                }
                else
                {
                    cityStatus = status["av"];
                    cityHud = status["av"];
                    TriggerServerEvent("UpdateHud", cityHud, countyHud, cityStatus, countyStatus);
                }
            }

            if (zone == "county")
            {
                if (countyStatus == status["av"])
                {
                    countyStatus = status["oh"];
                    countyHud = status["ah"];
                    TriggerServerEvent("UpdateHud", cityHud, countyHud, cityStatus, countyStatus);
                }
                else
                {
                    countyStatus = status["av"];
                    countyHud = status["av"];
                    TriggerServerEvent("UpdateHud", cityHud, countyHud, cityStatus, countyStatus);
                }
            }
        }

        public static async void PrioCd(string zone, int minutes)
        {
            if (zone == "city")
            {
                if (cityStatus == status["ac"] || cityStatus == status["cd"])
                {
                    if (minutes > 0)
                    {
                        cityStatus = status["cd"];
                        cityHud = $"{status["cd"]} ({minutes} minutes)";
                        TriggerServerEvent("UpdateHud", cityHud, countyHud, cityStatus, countyStatus);
                    }
                    else
                    {
                        cityStatus = status["av"];
                        cityHud = status["av"];
                        TriggerServerEvent("UpdateHud", cityHud, countyHud, cityStatus, countyStatus);
                    }
                }
                else
                {
                    CommonFunctions.DrawWarning($"~w~You ~r~can't activate ~b~cooldown ~w~in the {zone}");
                }
            }
            else if (zone == "county")
            {
                if (cityStatus == status["ac"] || cityStatus == status["cd"])
                {
                    if (minutes > 0)
                    {
                        countyStatus = status["cd"];
                        countyHud = $"{status["cd"]} ({minutes} minutes)";
                        TriggerServerEvent("UpdateHud", cityHud, countyHud, cityStatus, countyStatus);
                    }
                    else
                    {
                        countyStatus = status["av"];
                        countyHud = status["av"];
                        TriggerServerEvent("UpdateHud", cityHud, countyHud, cityStatus, countyStatus);
                    }
                }
                else
                {
                    CommonFunctions.DrawWarning($"~w~You ~r~can't activate ~b~cooldown ~w~in the {zone}");
                }
            }
        }

        public static void PrioActive(Player sourcePlayer, string zone)
        {
            if (zone == "city")
            {
                if (cityStatus == status["av"])
                {
                    cityPrioPlayer = sourcePlayer;
                    cityStatus = status["ac"];
                    cityHud = $"{status["ac"]} ({cityPrioPlayer.Name})";
                    TriggerServerEvent("UpdateHud", cityHud, countyHud, cityStatus, countyStatus);
                }
                else if (cityStatus == status["ac"])
                {
                    CommonFunctions.DrawWarning($"There is already an ~r~active ~w~priority in the ~b~{zone}!");
                }
                else
                {
                    CommonFunctions.DrawWarning("The ~r~priority ~w~in the ~bi~city ~w~is ~y~On Hold!");
                }
            }
            else if (zone == "county")
            {
                if (countyStatus == status["av"])
                {
                    countyPrioPlayer = sourcePlayer;
                    countyStatus = status["ac"];
                    countyHud = $"{status["ac"]} ({countyPrioPlayer.Name})";
                    TriggerServerEvent("UpdateHud", cityHud, countyHud, cityStatus, countyStatus);
                }
                else if (countyStatus == status["ac"])
                {
                    CommonFunctions.DrawWarning($"There is already an ~r~active ~w~priority in the ~b~{zone}!");
                }
                else
                {
                    CommonFunctions.DrawWarning("The ~r~priority ~w~in the ~bi~city ~w~is ~y~On Hold!");
                }
            }
        }

        public static void DrawText()
        {
            API.SetTextFont(0);
            API.SetTextScale(0.0f, 0.26f);
            API.SetTextColour(128, 128, 128, 255);
            API.SetTextDropshadow(0, 0, 0, 0, 255);
            API.SetTextDropShadow();
            API.SetTextOutline();
            API.SetTextEntry("STRING");
            API.AddTextComponentString($"~w~~h~County Priority: {countyHud}");
            API.DrawText(0.168f, 0.8799f);

            API.SetTextFont(0);
            API.SetTextScale(0.0f, 0.26f);
            API.SetTextColour(128, 128, 128, 255);
            API.SetTextDropshadow(0, 0, 0, 0, 255);
            API.SetTextDropShadow();
            API.SetTextOutline();
            API.SetTextEntry("STRING");
            API.AddTextComponentString($"~w~~h~City Priority: {cityHud}");
            API.DrawText(0.168f, 0.8970f);
        }
    }
}
