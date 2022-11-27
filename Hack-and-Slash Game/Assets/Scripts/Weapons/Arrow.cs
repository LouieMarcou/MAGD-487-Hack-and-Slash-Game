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

    }

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
