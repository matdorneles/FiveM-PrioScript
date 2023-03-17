using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace PrioScript
{
    public class PrioHud : BaseScript
    {
        public static bool visible = false;
        private static string zone1;
        private static string zone2;

        public static void ToogleHud(string setZone1, string setZone2)
        {
            if (visible == false)
                visible = true;
            else
                visible = false;
            zone1 = setZone1;
            zone2 = setZone2;
        }

        public static void DrawText()
        {
            if(visible)
            {
                API.SetTextFont(0);
                API.SetTextScale(0.0f, 0.3f);
                API.SetTextColour(128, 128, 128, 255);
                API.SetTextDropshadow(0, 0, 0, 0, 255);
                API.SetTextDropShadow();
                API.SetTextOutline();
                API.SetTextEntry("STRING");
                API.AddTextComponentString("~y~OBRP Priority Control");
                API.DrawText(0.168f, 0.8315f);

                API.SetTextFont(0);
                API.SetTextScale(0.0f, 0.3f);
                API.SetTextColour(128, 128, 128, 255);
                API.SetTextDropshadow(0, 0, 0, 0, 255);
                API.SetTextDropShadow();
                API.SetTextOutline();
                API.SetTextEntry("STRING");
                API.AddTextComponentString($"~w~{zone1} Priority: ~g~Available");
                API.DrawText(0.168f, 0.8725f);

                API.SetTextFont(0);
                API.SetTextScale(0.0f, 0.3f);
                API.SetTextColour(128, 128, 128, 255);
                API.SetTextDropshadow(0, 0, 0, 0, 255);
                API.SetTextDropShadow();
                API.SetTextOutline();
                API.SetTextEntry("STRING");
                API.AddTextComponentString($"~w~{zone2} Priority: ~g~Available");
                API.DrawText(0.168f, 0.8520f);
            }
        }
    }
}
