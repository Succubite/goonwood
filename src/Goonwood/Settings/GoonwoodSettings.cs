using BepInEx.Configuration;

namespace Goonwood.Settings;

public class GoonwoodSettings(ConfigFile config)
{
    public ConfigEntry<string> IntifaceURL =
        config.Bind("Buttplug", "IntifaceURL", "ws://127.0.0.1:12345", "The websocket URL for intiface");

    public ConfigEntry<float> LowAlertLevelIntensity =
        config.Bind("Buttplug", "LowAlertLevelIntensity", 0.1f, "The intensity of the low alert level");

    public ConfigEntry<float> ModerateAlertLevelIntensity =
        config.Bind("Buttplug", "ModerateAlertLevelIntensity", 0.3f, "The intensity of the moderate alert level");

    public ConfigEntry<float> HighAlertLevelIntensity =
        config.Bind("Buttplug", "HighAlertLevelIntensity", 0.4f, "The intensity of the high alert level");
}
