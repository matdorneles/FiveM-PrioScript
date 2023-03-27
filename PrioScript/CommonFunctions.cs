

using CitizenFX.Core.Native;

namespace PrioScript
{
    internal class CommonFunctions
    {
        public static void DrawWarning(string msg)
        {
            API.SetNotificationTextEntry("STRING");
            API.AddTextComponentString(msg);
            API.DrawNotification(false, false);
        }
    }
}
