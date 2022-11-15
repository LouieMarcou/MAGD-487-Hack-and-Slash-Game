using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowDoubleShot : WeaponUpgrade
{
    public override void ApplyEffects(WeaponBase weaponBase)
    {
        weaponBase.GetComponent<Bow>().AddNumberOfShots(upgradeData.uniqueNumber);

    }
}
