using Gloomwood;
using Gloomwood.AI;
using On.Gloomwood.Entity.AI;

namespace Goonwood.Hooks;

public static class AIHealthHooks
{
    public static void Initialize()
    {
        AIHealth.OnAttacked += AIHealthOnOnAttacked;
        AIHealth.Kill += AIHealthOnKill;
    }

    private static void AIHealthOnKill(AIHealth.orig_Kill orig, Gloomwood.Entity.AI.AIHealth self,
        DamageEventInfo damageEventInfo)
    {
        orig(self, damageEventInfo);

        Goonwood.Logger.LogInfo(
            $"{self.self.name} was killed with {damageEventInfo.amount} damage by {damageEventInfo.source}");
        
        var aiSense = self.self.Sense;
        var aiThink = self.self.Think;
        
        // Hopefully this should stop any detection.
        aiSense.ClearDetection();
        aiThink.OnAlertLevel(AIAlertLevel.None);
    }

    private static bool AIHealthOnOnAttacked(AIHealth.orig_OnAttacked orig, Gloomwood.Entity.AI.AIHealth self,
        AttackHitInfo hit)
    {
        var result = orig(self, hit);

        Goonwood.Logger.LogInfo($"{self.self.name} attacked with {hit.damage.amount} damage by {hit.source}");

        return result;
    }
}