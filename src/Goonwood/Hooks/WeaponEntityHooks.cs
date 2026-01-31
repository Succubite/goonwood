using Gloomwood.Entity.Weapons;
using MonoDetour;
using MonoDetour.HookGen;

namespace Goonwood.Hooks;

[MonoDetourTargets(typeof(WeaponEntity))]
internal static class WeaponEntityHooks
{
    [MonoDetourHookInitialize]
    public static void Initialize()
    {
        Md.Gloomwood.Entity.Weapons.WeaponEntity.PayAmmoCost_System_Int32_Gloomwood_Entity_Weapons_WeaponAmmoUpdateFlags
            .Postfix(Postfix_PayAmmoCost);
    }

    private static void Postfix_PayAmmoCost(WeaponEntity self, ref int ammoCost,
        ref WeaponAmmoUpdateFlags ammoUpdateFlags)
    {
        if (ammoCost <= 0) return;
        Goonwood.Log.LogInfo($"Weapon fired with {ammoCost} bullets");
    }
}