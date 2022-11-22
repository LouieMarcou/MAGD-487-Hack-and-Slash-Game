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
    public bool canAttack;

    void Awake()
    {
        timer = new WaitForSeconds(weaponData.stats.timeToAttack);
        
    }

    void Start()
    {
        playerController.GetAnimator().speed = weaponData.stats.speed;
        timer = new WaitForSeconds(weaponData.stats.timeToAttack / weaponData.stats.speed);
        //timer = new WaitForSeconds(weaponData.stats.timeToAttack);
        Debug.Log(weaponData.stats.timeToAttack + " / " + weaponData.stats.speed + " = " + weaponData.stats.timeToAttack / weaponData.stats.speed);
        playerController.GetAnimator().SetFloat("Speed", weaponData.stats.speed);
        canAttack = true;
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

    public WeaponStats GetOrginialStats()
    {
        return orginialWeaponStats;
    }

    public abstract IEnumerator AttackCooldown();

    public abstract void Attack();

    private void OnDisable()
    {
        if (orginialWeaponStats != null)
        {
            //Debug.Log(gameObject);
            //Debug.Log(weaponStats.damage);
            weaponData.stats = orginialWeaponStats;
            //Debug.Log(weaponStats.damage);


        }

    }
}
