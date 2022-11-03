using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponBase
{
    public AnimationClip[] animationClips;

    private bool inAnimation;

    private int timesAttacked;
	
	
	private bool canReturn;
	private float returnTimer = 0.1f;//variable needs testing
	private float t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerController.GetAnimator().speed = speed;
        timer = new WaitForSeconds(timeToAttack/speed);
		t = returnTimer;

    }
	
	//If canReturn is true the returnTimer will run until 0 and once it hits 0 the sword return animation will run and reset the returnTimer
	void Update()
	{
		if(canReturn)
		{
			returnTimer -= Time.deltaTime;
			if(returnTimer <= 0)
			{
				//Debug.Log("Able to return to starting position");
				SwordReturn();
				returnTimer = t;
				canReturn = false;
			}
		}
	}

    //make animation play, animation will not play if a animation is already playing
	//if the bool that transitions to the first attack animation is not playing and the player has attacked 0 times, 
	//it will set the transition bool and inAnimation bool = true. Then a coroutine will run whichs prevents the player from attacking again
	//if the transition bool used in the first clip is true and you have attacked already the transition bool for the second animaiton and inAnimation will be set to true and the coroutine will run
    public override void Attack()
    {
        //Problem:Spamming attacks stops the return animation
        if(inAnimation == false)
        {
            Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity, 7);
            ApplyDamage(colliders);
            if(playerController.GetAnimator().GetBool("Sword Attack 1") == false && timesAttacked == 0)
            {
                //Debug.Log("attack 1");
                playerController.GetAnimator().SetBool("Sword Attack 1", true);//transitions to Sword attack 1 animation
                inAnimation = true;
                
                StartCoroutine(AttackDelay());
            }
            else if(playerController.GetAnimator().GetBool("Sword Attack 1") && timesAttacked == 1)
            {
                //Debug.Log("attack 2");
                playerController.GetAnimator().SetBool("Sword Attack 2", true);//transitions to Sword attack 2 animation
                inAnimation = true;
                StartCoroutine(AttackDelay());
            }
            
        }
		
        else if(inAnimation)
        {
            //Debug.Log("cannot attack");
        }
    }

	//Coroutine that prevents the player from attacking before an animation is over
	//If the player does not attack in the time window canReturn will be set true so the sword will return to the original position
	//timesAttacked will increase AFTER the time has run so that player can't spam attacks
	//if the player has reached the max amount on consecutive attakcs it will reset the animations
    public override IEnumerator AttackDelay()
    {

        yield return timer;
		canReturn = true;
        inAnimation = false;
        timesAttacked++;
        if(timesAttacked == 2)
        {
            SwordReturn();
        }
    }

	//Resets sword attack animations and timesAttacked to run the sword Return animation
    private void SwordReturn()
    {
        playerController.GetAnimator().SetBool("Sword Attack 1", false);
        playerController.GetAnimator().SetBool("Sword Attack 2", false);
        timesAttacked = 0;
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
