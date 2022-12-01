using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : WeaponBase
{	
	private bool canCharge = false;
	private bool onChargeRelease = false;
    private float chargeAttackDamageModifier = 0f;
	
	//void Start()
 //   {
 //       playerController.GetAnimator().speed = weaponData.stats.speed;
 //       timer = new WaitForSeconds(weaponData.stats.timeToAttack / weaponData.stats.speed);
 //       //timer = new WaitForSeconds(weaponData.stats.timeToAttack);
 //       //Debug.Log(weaponData.stats.timeToAttack / weaponData.stats.speed);
 //       //playerController.GetAnimator().SetFloat("Speed", weaponData.stats.speed);
 //       canAttack = true;

 //   }
	
	public void ChargeAttack()
	{
		//Debug.Log("charging");
        if(canAttack)
        {
            canAttack = false;
            playerController.GetAnimator().SetTrigger("Charge Attack");
        }
		
	}

    public void ReleaseCharge()
    {
        //Debug.Log("releasing");
        isAttacking = true;
        onChargeRelease = true;
        playerController.GetAnimator().SetBool("Charge Release",true);
        StartCoroutine(AttackCooldown());
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
        {
            //Debug.Log("can't attack");
        }
           
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
	
	public bool GetCanCharge()
	{
		return canCharge;
	}
	
	public void SetCanCharge()
	{
		canCharge = true;
	}

    public bool GetChargeRelease()
    {
        return onChargeRelease;
    }

    public void SetAttackModifier(float mod)
    {
        chargeAttackDamageModifier = mod;
    }

    public float GetAttackModifier()
    {
        return chargeAttackDamageModifier;
    }
}
