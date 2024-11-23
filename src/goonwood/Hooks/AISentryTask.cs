using Gloomwood.AI;
using On.Gloomwood.AI.Tasks;

namespace Goonwood.Hooks;

public static class AISentryTaskHooks
{
    public static void Initialize()
    {
        AISentryTask.Exit += AISentryTaskOnExit;
        AISentryTask.UpdateSentryState += AISentryTaskOnUpdateSentryState;
    }

    private static bool AISentryTaskOnUpdateSentryState(AISentryTask.orig_UpdateSentryState orig, Gloomwood.AI.Tasks.AISentryTask self)
    {
        var result = orig(self);

        var detectionInfo = self.detection;
        if (detectionInfo is null) return result;
        
        // NOTE: For some reason, AIDetectLevel.None is never set but the sentry does stop detecting when the task is
        //       exited.
        switch (detectionInfo.level)
        {
            case AIDetectLevel.None:
                Goonwood.Logger.LogInfo("Stopping vibration");
                Goonwood.DeviceManager.StopConnectedDevices();
                break;
            case AIDetectLevel.Low:
                Goonwood.Logger.LogInfo("Player was detected with a level of `Low` by sentry");
                Goonwood.DeviceManager.VibrateConnectedDevices(0.1);
                break;
            case AIDetectLevel.Moderate:
                Goonwood.Logger.LogInfo("Player was detected with a level of `Moderate` by sentry");
                Goonwood.DeviceManager.VibrateConnectedDevices(0.3);
                break;
            case AIDetectLevel.High:
                Goonwood.Logger.LogInfo("Player was detected with a level of `High` by sentry");
                Goonwood.DeviceManager.VibrateConnectedDevices(0.4);
                break;
            default:
                return result;
        }

        return result;
    }
    
    private static void AISentryTaskOnExit(AISentryTask.orig_Exit orig, Gloomwood.AI.Tasks.AISentryTask self)
    {
        orig(self);
        
        Goonwood.DeviceManager.StopConnectedDevices();
    }
}