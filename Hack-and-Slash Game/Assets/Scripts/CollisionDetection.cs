using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponBase wp;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Enemy" && wp.isAttacking)
        {
            //Debug.Log(collision.name);
            collision.GetComponent<EnemyBase>().TakeDamage(wp.weaponData.stats.damage);
        }
    }
}
