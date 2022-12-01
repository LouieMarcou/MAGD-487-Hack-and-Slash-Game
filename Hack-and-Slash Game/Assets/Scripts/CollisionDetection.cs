using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponBase wp;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy" && wp.isAttacking)
        {
            //Debug.Log(collision.name);
            float damage = wp.weaponData.stats.damage;

            if (wp.GetComponentInParent<PlayerController>().GetHasCrit())
            {
                float chance = Random.Range(0f, 100f);
                //Debug.Log(chance);
                if (chance <= wp.GetComponentInParent<PlayerController>().GetCritChance())
                {
                    // Debug.Log("crit");
                    damage *= wp.GetComponentInParent<PlayerController>().GetCritMultiplier();
                }
            }

            float modifiedDamage = damage * Random.Range(0.7f, 1.2f);
            //Debug.Log(modifiedDamage);
            if (wp.weaponData.Name.Contains("Axe") && wp.gameObject.GetComponent<Axe>().GetChargeRelease())
            {
                collision.GetComponent<EnemyBase>().TakeDamage(modifiedDamage * wp.gameObject.GetComponent<Axe>().GetAttackModifier());
            }
            else
            {
                collision.GetComponent<EnemyBase>().TakeDamage(modifiedDamage);
                if (GetComponent<Arrow>())
                {
                    gameObject.SetActive(false);
                }
            }



            if (wp.weaponData.Name.Contains("Sword") && wp.gameObject.GetComponent<Sword>().GetHasLifesteal())
            {
                //Debug.Log(wp.weaponData.stats.damage);
                //Debug.Log(wp.gameObject.GetComponent<Sword>().GetLifestealAmount());
                wp.playerController.AddHealth(damage * wp.gameObject.GetComponent<Sword>().GetLifestealAmount());
            }
            wp.HitMarkerCoroutine();
        }


        else if (collision.tag == "Ground" && wp.isAttacking && GetComponent<Arrow>())
        {
            gameObject.SetActive(false);
        }
    }
}
