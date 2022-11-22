using UnityEngine;

public class WeaponSpeedIncrease : WeaponUpgrade
{
    public override void ApplyEffects(WeaponBase weaponBase)//needs testing
    {
        weaponBase.weaponData.stats.speed *= 1f + (upgradeData.uniqueNumber * 0.01f);
        //weaponBase.weaponData.stats.timeToAttack *= 1f - (upgradeData.uniqueNumber * 0.01f);
        //weaponBase.timer = new WaitForSeconds(weaponBase.weaponData.stats.timeToAttack / weaponBase.weaponData.stats.speed);
        Debug.Log(weaponBase.weaponData.stats.timeToAttack / weaponBase.weaponData.stats.speed);
        weaponBase.playerController.GetAnimator().SetFloat("Speed", weaponBase.weaponData.stats.speed);
    }
}
