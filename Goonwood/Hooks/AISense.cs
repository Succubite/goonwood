using Gloomwood.AI;
using UnityEngine;

namespace Goonwood.Hooks;

public static class AISenseHooks
{
    public static void Initialize()
    {
        On.Gloomwood.AI.AISense.SetDetection += AISenseOnSetDetection;
    }

    private static void AISenseOnSetDetection(On.Gloomwood.AI.AISense.orig_SetDetection orig, Gloomwood.AI.AISense self, GameObject target, AIDetectInfo detection)
    {
        orig(self, target, detection);

        // Yeah i could've used an IL transpiler but i'm lazy.
        if (detection.level > AIDetectLevel.None)
        {
            Goonwood.Logger.LogInfo($"{target.name} was detected as {detection.level}");
        }
    }
}