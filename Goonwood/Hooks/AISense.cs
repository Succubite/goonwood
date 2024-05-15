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
        
        switch (detection.level)
        {
            case AIDetectLevel.Low:
                Goonwood.Logger.LogInfo("Player was detected with a level of `Low`");
                break;
            case AIDetectLevel.Moderate:
                Goonwood.Logger.LogInfo("Player was detected with a level of `Moderate`");
                break;
            case AIDetectLevel.High:
                Goonwood.Logger.LogInfo("Player was detected with a level of `High`");
                break;
        }
    }
}