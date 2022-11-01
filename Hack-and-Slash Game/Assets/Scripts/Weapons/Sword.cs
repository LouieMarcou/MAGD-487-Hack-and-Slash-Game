using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponBase
{
    public AnimationClip[] animationClips;

    private bool inAnimation;

    private int timesAttacked;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(animationClips[0].length);
        playerController.GetAnimator().speed = speed;
        timer = new WaitForSeconds(timeToAttack/speed);

    }

    //make animation play
    public override void Attack()
    {
        
        if(inAnimation == false)
        {
            
            if(playerController.GetAnimator().GetBool("Sword Attack 1") == false && timesAttacked == 0)
            {
                Debug.Log("attack 1");
                playerController.GetAnimator().SetBool("Sword Attack 1", true);//transitions to Sword attack 1 animation
                inAnimation = true;

                StartCoroutine(AttackDelay());
            }
            else if(playerController.GetAnimator().GetBool("Sword Attack 1") && timesAttacked == 1)
            {
                Debug.Log("attack 2");
                playerController.GetAnimator().SetBool("Sword Attack 2", true);//transitions to Sword attack 2 animation
                inAnimation = true;

                StartCoroutine(AttackDelay());
            }
            
        }

        else if(inAnimation)
        {
            Debug.Log("cannot attack");
        }
    }

    public override IEnumerator AttackDelay()
    {

        yield return timer;

        inAnimation = false;
        timesAttacked++;
        if(timesAttacked == 2)
        {
            SwordReturn();
        }
    }

    private void SwordReturn()
    {
        playerController.GetAnimator().SetBool("Sword Attack 1", false);
        playerController.GetAnimator().SetBool("Sword Attack 2", false);
        timesAttacked = 0;
    }
}
