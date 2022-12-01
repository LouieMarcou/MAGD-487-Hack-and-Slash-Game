using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponUpgrade : MonoBehaviour
{
    public UpgradeData upgradeData;

    public abstract void ApplyEffects(WeaponBase weaponBase);
}
