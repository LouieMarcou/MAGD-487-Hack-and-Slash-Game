using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponBase
{
	private bool canAttack;
	
	
    // Start is called before the first frame update
    void Start()
    {
        playerController.GetAnimator().speed = speed;
        timer = new WaitForSeconds(timeToAttack/speed);
		canAttack = true;

    }
	
	void Update()
	{

	}


    public override void Attack()
    {
		
		if(canAttack)
		{
			canAttack = false;
			Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.identity, 7);
        	ApplyDamage(colliders);
			playerController.GetAnimator().SetBool("Sword Attack 1",true);//transitions to Sword attack 1 animation
			StartCoroutine(AttackDelay());
		}
    }

    public override IEnumerator AttackDelay()
    {
		
        yield return timer;
        canAttack = true;
		playerController.GetAnimator().SetBool("Sword Attack 1", false);
    }


    private void ApplyDamage(Collider[] colliders)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            //only registers if incredibly close. Need to fix
            Debug.Log(colliders[i].gameObject.name);
            if (colliders[i].GetComponent<EnemyBase>())
            {
                //totalDamage = Random.Range(0.7f, 1.3f) * weaponStats.damage * p.playerData.playerStats.DamageModifier;
                //totalDamage = Mathf.Ceil(totalDamage);
                //Debug.Log(totalDamage);
                //colliders[i].GetComponent<EnemyBase>().TakeDamage(totalDamage);
                colliders[i].GetComponent<EnemyBase>().TakeDamage(weaponData.stats.damage);
            }
        }
    }
}
