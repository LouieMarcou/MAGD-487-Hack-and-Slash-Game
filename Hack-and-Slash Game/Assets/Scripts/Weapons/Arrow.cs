using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Bow weapon;

    private Vector3 shootPoint;
    private Transform BowParent;

    private Rigidbody rb;

    void Awake()
    {
        shootPoint = transform.localPosition;
        BowParent = transform.parent;
        weapon = BowParent.gameObject.GetComponent<Bow>();
        rb = GetComponent<Rigidbody>();
        //Debug.Log(weapon);
        //Debug.Log(GetComponent<CollisionDetection>().wp);
        
        //Debug.Log(GetComponent<CollisionDetection>().wp);
    }

    void Update()
    {
        //if(wp.isAttacking)
        //    SpinObject();
    }

    //void OnTriggerEnter(Collider collision)
    //{
    //    //Debug.Log(collision.name);
    //    if (collision.tag == "Enemy" && wp.isAttacking)
    //    {
    //        //Debug.Log(collision.name);
    //        collision.GetComponent<EnemyBase>().TakeDamage(wp.weaponData.stats.damage);
    //        gameObject.SetActive(false);
    //    }
    //    if(collision.tag == "Ground" && wp.isAttacking)
    //    {
    //        //gameObject.SetActive(false);
    //        //Debug.Log("hit ground");
    //    }
    //}

    public void Shoot()
    {
        rb.AddRelativeForce(Vector3.right * 1000f * weapon.weaponData.stats.speed, ForceMode.Force);
        rb.useGravity = true;
        //Debug.Log(wp.isAttacking);
    }

    public void Reset()
    {
        //Debug.Log("went into reset");
        //GetComponent<Rigidbody>().AddRelativeForce(Vector3.zero, ForceMode.VelocityChange);
        rb.velocity = Vector3.zero;
        transform.parent = BowParent;
        transform.localPosition = shootPoint;
        transform.rotation = BowParent.transform.rotation;
        rb.useGravity = false;
        gameObject.SetActive(true);

    }

    private void SpinObject()
    {
        float yVelocity = rb.velocity.y;
        float zVelocity = rb.velocity.z;
        float xVelocity = rb.velocity.x;
        float combinedVelocity = Mathf.Sqrt(xVelocity * xVelocity + zVelocity * zVelocity);
        float fallAngle = -1 * Mathf.Atan2(yVelocity, combinedVelocity) * 180 / Mathf.PI;

        transform.eulerAngles = new Vector3(fallAngle, transform.eulerAngles.y, transform.eulerAngles.x);
    }

    public void SetParent(Transform parent)
    {
        BowParent = parent;
    }

    public void SetBow(Bow bow)
    {
        weapon = bow;
        GetComponent<CollisionDetection>().wp = weapon;
    }
}
