using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponBase
{
    void Update()
    {

    }

    //make animation play
    public override void Attack()
    {
        //if(!(playerController.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("Idle")))
        //{
        //    if(playerController.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("SwordAttack1"))
        //    {
        //        Debug.Log("Sword Attack 2");
        //        playerController.GetAnimator().SetBool("Sword Attack 2", true);
                
        //        attackWait = StartCoroutine(StartAttackWait("Sword Attack 2"));
                
        //    }
        //    //Debug.Log("Cannot attack yet");
        //    //return;
        //}
        if((playerController.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("Idle")))
        {
            playerController.GetAnimator().SetBool("Sword Attack 1", true);
        }

        //if attacking while sword attack 1 is playing, immediatly after it finishes, go to sword attack 2
        if (playerController.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("SwordAttack1"))
        {
            Debug.Log("Sword Attack 2");
            playerController.GetAnimator().SetBool("Sword Attack 2", true);

            attackWait = StartCoroutine(StartAttackWait("Sword Attack 2"));
            playerController.GetAnimator().SetBool("Sword Attack 1", false);

        }

    }
}
