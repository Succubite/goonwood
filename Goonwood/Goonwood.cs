using BepInEx;
using BepInEx.Logging;
using Goonwood.Patches;
using System.Reflection;
using System.Collections.Generic;
using MonoMod.RuntimeDetour;
using HarmonyLib;

namespace Goonwood;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Goonwood : BaseUnityPlugin
{
    public static Goonwood Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger { get; private set; } = null!;

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;

        Hook();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} (v{MyPluginInfo.PLUGIN_VERSION}) has loaded!");
    }

    internal static void Hook()
    {
        Logger.LogDebug("Hooking...");


        Logger.LogDebug("Finished Hooking!");
    }
}
