using Gloomwood;
using On.Gloomwood.Players;

namespace Goonwood.Hooks;

public class PlayerEntityHooks
{
    public static void Initialize()
    {
        PlayerEntity.OnAttacked += PlayerEntityOnOnAttacked;
    }

    private static bool PlayerEntityOnOnAttacked(PlayerEntity.orig_OnAttacked orig, Gloomwood.Players.PlayerEntity self, AttackHitInfo hit)
    {
        Goonwood.Logger.LogInfo("haiiii :3");
        return orig(self, hit);
    }
}