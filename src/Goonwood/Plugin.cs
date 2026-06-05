using BepInEx;
using BepInEx.Logging;
using Goonwood.Buttplug;
using Goonwood.Commands;
using Goonwood.Hooks;
using Goonwood.Settings;

namespace Goonwood;

[BepInAutoPlugin]
public partial class Goonwood : BaseUnityPlugin
{
    internal static ManualLogSource Log { get; private set; } = null!;
    internal static GoonwoodSettings Settings { get; private set; } = null!;
    internal static DeviceManager DeviceManager { get; private set; } = null!;

    private void Awake()
    {
        Log = Logger;
        Settings = new GoonwoodSettings(Config);

        DeviceManager = new DeviceManager("Goonwood", Settings.IntifaceURL.Value);
        DeviceManager.ConnectDevices();

        CommandInitializer.AddCommand(new ButtplugCommand());
        CommandInitializer.AddCommand(new AlertLevelCommand());

        CommandInitializer.Initialize();

        Hook();

        Log.LogInfo($"Plugin {Name} (v{Version}) has loaded!");
    }

    private static void Hook()
    {
        Log.LogDebug("Hooking...");

        WeaponEntityHooks.Initialize();
        AIManagerHooks.Initialize();

        Log.LogDebug("Finished Hooking!");
    }
}
