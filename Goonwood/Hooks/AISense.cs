using System;
using Gloomwood.AI;
using Gloomwood.Players;
using UnityEngine;

namespace Goonwood.Hooks;

public static class AISenseHooks
{
    public static void Initialize()
    {
        On.Gloomwood.AI.AISense.SetDetection += AISenseOnSetDetection;
    }

    private static void AISenseOnSetDetection(On.Gloomwood.AI.AISense.orig_SetDetection orig, AISense self,
        GameObject target, AIDetectInfo detection)
    {
        orig(self, target, detection);

        // Yeah i could've used an IL transpiler but i'm lazy.
        if (detection.level <= AIDetectLevel.None) return;

        var playerEntity = target.GetComponent<PlayerEntity>();
        if (playerEntity is null) return;
        
        // TODO: Make the vibration configurable
        // Maybe check for whether the game is paused?
        switch (detection.level)
        {
            case AIDetectLevel.None:
                Goonwood.Logger.LogInfo("Stopping vibration");
                Goonwood.DeviceManager.VibrateConnectedDevices(0.0);
                break;
            case AIDetectLevel.Low:
                Goonwood.Logger.LogInfo("Player was detected with a level of `Low`");
                Goonwood.DeviceManager.VibrateConnectedDevices(0.3);
                break;
            case AIDetectLevel.Moderate:
                Goonwood.Logger.LogInfo("Player was detected with a level of `Moderate`");
                Goonwood.DeviceManager.VibrateConnectedDevices(0.5);
                break;
            case AIDetectLevel.High:
                Goonwood.Logger.LogInfo("Player was detected with a level of `High`");
                Goonwood.DeviceManager.VibrateConnectedDevices(1.0);
                break;
        }
    }
}