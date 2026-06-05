using System.Linq;
using Gloomwood.AI;
using Gloomwood.Entity.AI;
using MonoDetour;
using MonoDetour.HookGen;

namespace Goonwood.Hooks;

[MonoDetourTargets(typeof(AIManager))]
internal static class AIManagerHooks
{
    private static bool NeedsReset { get; set; } = true;
    private static AIAlertLevel LastAlertLevel { get; set; } = AIAlertLevel.None;

    [MonoDetourHookInitialize]
    public static void Initialize()
    {
        Md.Gloomwood.AI.AIManager.OnUpdate.Postfix(Postfix_OnUpdate);
    }

    private static void Postfix_OnUpdate(AIManager self, ref float deltaTime)
    {
        var activeAI = self.GetActiveAI();
        if (activeAI?.Count is 0 or null) return;

        var sensingEntities = activeAI.Where(entity =>
            entity.HasComponent(AIComponentFlags.Sense) &&
            (!entity.HasComponent(AIComponentFlags.Health) || entity.Health.IsAlive));

        // Find the highest alert level among all entities
        var highestAlertLevel = AIAlertLevel.None;
        foreach (var entity in sensingEntities)
        {
            var currentLevel = entity.Sense.AlertLevel;
            if (currentLevel > highestAlertLevel)
            {
                highestAlertLevel = currentLevel;
            }
        }

        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (highestAlertLevel)
        {
            case AIAlertLevel.None when NeedsReset:
                Goonwood.Log.LogDebug("All entities calm, stopping connected devices");
                Goonwood.DeviceManager.StopConnectedDevices();
                NeedsReset = false;
                break;

            case AIAlertLevel.Low:
            case AIAlertLevel.Moderate:
            case AIAlertLevel.High:
                // Only vibrate if alert level changed, or we need to reset from calm state
                if (highestAlertLevel != LastAlertLevel || !NeedsReset)
                {
                    var intensity = GetIntensityForAlertLevel(highestAlertLevel);
                    Goonwood.Log.LogDebug($"Alert level: {highestAlertLevel}, vibrating at intensity: {intensity}");
                    Goonwood.DeviceManager.VibrateConnectedDevices(intensity);
                    NeedsReset = true;
                }

                break;
        }

        LastAlertLevel = highestAlertLevel;
    }

    private static float GetIntensityForAlertLevel(AIAlertLevel level)
    {
        return level switch
        {
            AIAlertLevel.Low => 0.1f,
            AIAlertLevel.Moderate => 0.3f,
            AIAlertLevel.High => 0.4f,
            _ => 0.0f
        };
    }
}
