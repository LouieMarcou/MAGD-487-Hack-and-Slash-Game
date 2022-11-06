using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData weaponData;

    public WeaponStats weaponStats;
    private WeaponStats orginialWeaponStats;
    

    public PlayerController playerController;

    public Coroutine attackWait;
    public WaitForSeconds timer;

    public bool isAttacking = false;

    void Awake()
    {
        timer = new WaitForSeconds(weaponData.stats.timeToAttack);
        
    }

    public virtual void StoreOrginialData(WeaponData wd)
    {
        weaponData = wd;

        orginialWeaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack, wd.stats.speed);
    }

    public virtual void SetData(WeaponData wd)
    {
        weaponData = wd;

        weaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack, wd.stats.speed);
    }

    public abstract IEnumerator AttackCooldown();

    public abstract void Attack();
}
