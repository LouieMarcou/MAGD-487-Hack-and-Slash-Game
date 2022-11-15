using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : WeaponBase
{
    private bool canAttack;
	
	void Start()
    {
        playerController.GetAnimator().speed = weaponData.stats.speed;
        timer = new WaitForSeconds(weaponData.stats.timeToAttack / weaponData.stats.speed);
        //timer = new WaitForSeconds(weaponData.stats.timeToAttack);
        //Debug.Log(weaponData.stats.timeToAttack / weaponData.stats.speed);
        //playerController.GetAnimator().SetFloat("Speed", weaponData.stats.speed);
        canAttack = true;

    }
	
	public override void Attack()
    {
        if (canAttack)
        {
            isAttacking = true;
            canAttack = false;
            playerController.GetAnimator().SetTrigger("Axe Attack");
            StartCoroutine(AttackCooldown());
        }
        else
            Debug.Log("can't attack");
    }

    public override IEnumerator AttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return timer;
        canAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return timer;
        isAttacking = false;
    }
}
