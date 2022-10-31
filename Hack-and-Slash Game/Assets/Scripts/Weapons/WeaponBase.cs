using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData weaponData;

    public WeaponStats weaponStats;
    private WeaponStats orginialWeaponStats;

    public float timeToAttack = 1f;
    

    public PlayerController playerController;

    public Coroutine attackWait;
    private WaitForSeconds timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = new WaitForSeconds(timeToAttack/10);
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

    public IEnumerator StartAttackWait(string animationName)
    {
        //Debug.Log("Wait started");
        yield return timer;

        attackWait = null;
        playerController.GetAnimator().SetBool(animationName, false);
        //Debug.Log("Wait Over");
    }

    public abstract void Attack();
}
