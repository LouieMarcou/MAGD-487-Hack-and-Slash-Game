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
			if(wp.weaponData.Name.Contains("Sword") && wp.gameObject.GetComponent<Sword>().GetHasLifesteal())
			{
				//Debug.Log(wp.weaponData.stats.damage);
				//Debug.Log(wp.gameObject.GetComponent<Sword>().GetLifestealAmount());
				wp.playerController.AddHealth(wp.weaponData.stats.damage * wp.gameObject.GetComponent<Sword>().GetLifestealAmount());
			}
        }
    }
}
