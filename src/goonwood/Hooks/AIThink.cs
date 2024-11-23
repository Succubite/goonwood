using Gloomwood.AI;
using AIThink = On.Gloomwood.AI.AIThink;

namespace Goonwood.Hooks;

public static class AIThinkHooks
{
    public static void Initialize()
    {
        AIThink.OnAlertLevel += AIThinkOnOnAlertLevel;
    }

    private static void AIThinkOnOnAlertLevel(AIThink.orig_OnAlertLevel orig, Gloomwood.AI.AIThink self, AIAlertLevel alertLevel)
    {
        orig(self, alertLevel);

        Goonwood.Logger.LogInfo($"Alert level changed to {alertLevel} for {self.self.name}");
        
        // TODO: Make the vibration configurable
        // Maybe check for whether the game is paused?
        switch (alertLevel)
        {
            case AIAlertLevel.None:
                Goonwood.Logger.LogDebug("Stopping vibration");
                Goonwood.DeviceManager.StopConnectedDevices();
                break;
            case AIAlertLevel.Low:
                Goonwood.Logger.LogDebug("Player was detected with a level of `Low`");
                Goonwood.DeviceManager.VibrateConnectedDevices(0.1);
                break;
            case AIAlertLevel.Moderate:
                Goonwood.Logger.LogDebug("Player was detected with a level of `Moderate`");
                Goonwood.DeviceManager.VibrateConnectedDevices(0.3);
                break;
            case AIAlertLevel.High:
                Goonwood.Logger.LogDebug("Player was detected with a level of `High`");
                Goonwood.DeviceManager.VibrateConnectedDevices(0.4);
                break;
        }
    }
}