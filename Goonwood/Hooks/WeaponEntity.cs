using Gloomwood.Entity.Weapons;
using WeaponEntity = On.Gloomwood.Entity.Weapons.WeaponEntity;

namespace Goonwood.Hooks;

public static class WeaponEntityHooks
{
    public static void Initialize()
    {
        WeaponEntity.PayAmmoCost_int_WeaponAmmoUpdateFlags += WeaponEntityOnPayAmmoCost_int_WeaponAmmoUpdateFlags;
    }

    private static void WeaponEntityOnPayAmmoCost_int_WeaponAmmoUpdateFlags(
        WeaponEntity.orig_PayAmmoCost_int_WeaponAmmoUpdateFlags orig, Gloomwood.Entity.Weapons.WeaponEntity self,
        int ammoCost, WeaponAmmoUpdateFlags ammoUpdateFlags)
    {
        orig(self, ammoCost, ammoUpdateFlags);

        if (ammoCost <= 0) return;
        Goonwood.Logger.LogInfo($"Weapon fired with {ammoCost} bullets");
    }
}