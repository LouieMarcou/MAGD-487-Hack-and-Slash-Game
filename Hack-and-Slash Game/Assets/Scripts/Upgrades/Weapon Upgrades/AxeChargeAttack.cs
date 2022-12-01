using UnityEngine;

public class AxeChargeAttack : WeaponUpgrade
{
    public override void ApplyEffects(WeaponBase weaponBase)
    {
        weaponBase.GetComponent<Axe>().SetCanCharge();
        weaponBase.GetComponent<Axe>().SetAttackModifier(upgradeData.uniqueNumber);
    }
}
