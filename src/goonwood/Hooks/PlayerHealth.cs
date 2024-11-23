using Gloomwood;
using On.Gloomwood.Entity;
using On.Gloomwood.Players;

namespace Goonwood.Hooks;

public static class PlayerHealthHooks
{
    public static void Initialize()
    {
        PlayerHealth.OnPlayerDamage += PlayerHealthOnOnPlayerDamage;
        PlayerHealth.OnPlayerKilled += PlayerHealthOnOnPlayerKilled;
        EntityHealth.SetAmount += EntityHealthOnSetAmount;
    }

    private static void EntityHealthOnSetAmount(EntityHealth.orig_SetAmount orig, Gloomwood.Entity.EntityHealth self,
        int value)
    {
        orig(self, value);

        if (self is not Gloomwood.Players.PlayerHealth) return;
        Goonwood.Logger.LogInfo($"Player health set to {value}");
    }

    private static void PlayerHealthOnOnPlayerKilled(PlayerHealth.orig_OnPlayerKilled orig,
        Gloomwood.Players.PlayerHealth self, DamageEventInfo damageEventInfo)
    {
        Goonwood.Logger.LogInfo("Player died!");
        orig(self, damageEventInfo);
    }

    private static void PlayerHealthOnOnPlayerDamage(PlayerHealth.orig_OnPlayerDamage orig,
        Gloomwood.Players.PlayerHealth self, DamageEventInfo damageEventInfo)
    {
        Goonwood.Logger.LogInfo("Player damaged!");
        orig(self, damageEventInfo);
    }
}