using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData weaponData;

    public WeaponStats weaponStats;
    private WeaponStats orginialWeaponStats;

    public float timeToAttack = 1f;
    public float speed;
    

    public PlayerController playerController;

    public Coroutine attackWait;
    public WaitForSeconds timer;

    void Awake()
    {
        timer = new WaitForSeconds(timeToAttack);
        
    }

    public virtual void StoreOrginialData(WeaponData wd)
    {
        weaponData = wd;
        timeToAttack = weaponData.stats.timeToAttack;
        //timeToAttack = timeToAttack * playerData.playerStats.CooldownModifier;

        orginialWeaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack);
    }

    public virtual void SetData(WeaponData wd)
    {
        weaponData = wd;
        timeToAttack = weaponData.stats.timeToAttack;
        //timeToAttack = timeToAttack * playerData.playerStats.CooldownModifier;

        weaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack);
    }

    public IEnumerator StartAttackWait(string animationName)//may not need this anymore
    {
        //Debug.Log("Wait started");
        
        yield return timer;

        //attackWait = null;

        playerController.GetAnimator().SetBool(animationName, false);
        //Debug.Log("Wait Over");
    }

    public abstract IEnumerator AttackDelay();

    public abstract void Attack();
}
