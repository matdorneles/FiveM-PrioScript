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

        public static Player cityPrioPlayer;
        public static Player countyPrioPlayer;

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

        public static bool CdCheck(string zone)
        {
            Player player = Game.Player;
            if (zone == "city")
            {
                if (cityStatus == status["cd"])
                {
                    CommonFunctions.DrawWarning($"Priority in the {zone} is already in cooldown!!");
                    return false;
                }
                else if (cityPrioPlayer.Name != player.Name)
                {
                    CommonFunctions.DrawWarning("You ~r~can't ~w~finish another user's priority!!");
                    return false;
                }
                else if (!player.IsPlaying)
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }

            else if (zone == "county")
            {
                if (countyStatus == status["cd"])
                {
                    CommonFunctions.DrawWarning($"Priority in the {zone} is already in cooldown!!");
                    return false;
                }
                else if (countyPrioPlayer.Name != player.Name)
                {
                    CommonFunctions.DrawWarning("You ~r~can't ~w~finish another user's priority!!");
                    return false;
                }
                else if (!player.IsPlaying)
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public static bool PrioCheck(string zone)
        {
            if (zone == "city")
            {
                if (cityStatus == status["cd"])
                {
                    CommonFunctions.DrawWarning($"The ~y~{zone} priority ~w~is on cooldown!!");
                    return false;
                }
                else if (cityStatus == status["av"])
                {
                    CommonFunctions.DrawWarning($"You just ~r~activated ~w~the {zone} priority!!");
                    return true;
                }
                else
                {
                    CommonFunctions.DrawWarning($"You can't start a priority in the {zone}");
                    return false;
                }

                return false;
            }
            else if (zone == "county")
            {
                if (countyStatus == status["cd"])
                {
                    CommonFunctions.DrawWarning($"The ~y~{zone} priority ~w~is on cooldown!!");
                    return false;
                }
                else if (countyStatus == status["av"])
                {
                    CommonFunctions.DrawWarning($"You just ~r~activated ~w~the {zone} priority!!");
                    return true;
                }
                else
                {
                    CommonFunctions.DrawWarning($"You can't start a priority in the {zone}");
                    return false;
                }

                return false;
            }

            return false;
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
