using BepInEx.Configuration;

namespace Goonwood.Settings;

public class GoonwoodSettings(ConfigFile config)
{
    public ConfigEntry<string> IntifaceURL =
        config.Bind("Buttplug", "IntifaceURL", "ws://127.0.0.1:12345", "The websocket URL for intiface");
}
