using BepInEx.Configuration;

namespace Goonwood.Settings;

public class GoonwoodSettings(ConfigFile config)
{
    public ConfigEntry<bool> MySettingsBool =
        config.Bind<bool>("SectionName", "MySettingsBool", true, "This is an example boolean setting!");
}