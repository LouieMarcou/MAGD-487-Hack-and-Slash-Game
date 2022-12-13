using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponBase
{
	
	private bool hasLifesteal = false;
	private float lifestealAmountPerecent;
	
    // Start is called before the first frame update
    //void Start()
    //{
    //    playerController.GetAnimator().speed = weaponData.stats.speed;
    //    timer = new WaitForSeconds(weaponData.stats.timeToAttack / weaponData.stats.speed);
    //    //timer = new WaitForSeconds(weaponData.stats.timeToAttack);
    //    Debug.Log(weaponData.stats.timeToAttack + " / " + weaponData.stats.speed + " = " + weaponData.stats.timeToAttack / weaponData.stats.speed);
    //    playerController.GetAnimator().SetFloat("Speed", weaponData.stats.speed);
    //    canAttack = true;

    //}

    public override void Attack()
    {
        if (canAttack)
        {
            isAttacking = true;
            canAttack = false;
            //playerController.GetAnimator().SetBool("Sword Attack 1",true);//transitions to Sword attack 1 animation
            playerController.GetAnimator().SetTrigger("Attack");//transitions to Sword attack 1 animation
            StartCoroutine(AttackCooldown());
        }
        //else
        //    Debug.Log("can't attack");
    }

    public override IEnumerator AttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return timer;
        canAttack = true;
		//playerController.GetAnimator().SetBool("Sword Attack 1", false);
    }

    IEnumerator ResetAttackBool()
    {
        yield return timer;
        isAttacking = false;
    }
	
	public void SetHasLifesteal()
	{
		hasLifesteal = true;
	}
	
	public bool GetHasLifesteal()
	{
		return hasLifesteal;
	}
	
	public void SetLifestealAmount(float num)
	{
		lifestealAmountPerecent = num;
	}
	
	public float GetLifestealAmount()
	{
		return lifestealAmountPerecent;
	}

}
