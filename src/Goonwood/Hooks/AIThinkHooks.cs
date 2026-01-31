using System;
using Gloomwood.AI;
using MonoDetour;
using MonoDetour.HookGen;

namespace Goonwood.Hooks;

[MonoDetourTargets(typeof(AIThink))]
internal static class AIThinkHooks
{
    [MonoDetourHookInitialize]
    internal static void Initialize()
    {
        Md.Gloomwood.AI.AIThink.OnAlertLevel.Postfix(Postfix_OnAlertLevel);
    }
    
    private static void Postfix_OnAlertLevel(AIThink self, ref AIAlertLevel alertLevel)
    {
        Goonwood.Log.LogInfo($"Alert level changed to {alertLevel} for {self.self.name}");
        
        // All cases are met, shut up resharper.
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (alertLevel)
        {
            case AIAlertLevel.None:
                Goonwood.Log.LogDebug("Stopping vibration");
                Goonwood.DeviceManager.StopConnectedDevices();
                break;
            case AIAlertLevel.Low:
                Goonwood.Log.LogDebug("Player was detected with a level of `Low`");
                Goonwood.DeviceManager.VibrateConnectedDevices(0.1);
                break;
            case AIAlertLevel.Moderate:
                Goonwood.Log.LogDebug("Player was detected with a level of `Moderate`");
                Goonwood.DeviceManager.VibrateConnectedDevices(0.3);
                break;
            case AIAlertLevel.High:
                Goonwood.Log.LogDebug("Player was detected with a level of `High`");
                Goonwood.DeviceManager.VibrateConnectedDevices(0.4);
                break;
        }
    }
}