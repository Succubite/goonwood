using BepInEx;
using BepInEx.Logging;
// using Goonwood.Hooks;
using Goonwood.Buttplug;

namespace Goonwood;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Goonwood : BaseUnityPlugin
{
    public static Goonwood Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger { get; private set; } = null!;
    internal static DeviceManager DeviceManager { get; private set; } = null!;

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;

        // TODO: make `serverUri` configurable
        DeviceManager = new DeviceManager("ViralTremors", "ws://127.0.0.1:12345");
        DeviceManager.ConnectDevices();
        
        Hook();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} (v{MyPluginInfo.PLUGIN_VERSION}) has loaded!");
    }

    private static void Hook()
    {
        Logger.LogDebug("Hooking...");


        Logger.LogDebug("Finished Hooking!");
    }
}
