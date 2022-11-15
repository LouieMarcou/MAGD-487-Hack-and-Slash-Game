using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatAttackIncrease : WeaponUpgrade
{
    public override void ApplyEffects(WeaponBase weaponBase)
    {
        weaponBase.weaponData.stats.damage += upgradeData.uniqueNumber;

    }
}
