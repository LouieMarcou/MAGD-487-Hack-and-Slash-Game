using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuff : WeaponUpgrade
{
    public override void ApplyEffects(WeaponBase weaponBase)
    {
        weaponBase.weaponData.stats.damage *= (1 + upgradeData.uniqueNumber * 0.01f);
        weaponBase.weaponData.stats.speed *= (1 + upgradeData.uniqueNumber * 0.01f);

    }
}
