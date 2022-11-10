using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponBase
{
	[SerializeField] private GameObject arrow;
	private GameObject arrowClone;
	private Vector3 arrowSpeed;
	private bool canAttack;	
	
    // Start is called before the first frame update
    void Start()
    {
        arrowClone = Instantiate(arrow, transform);
		canAttack = true;
		arrowSpeed = new Vector3(0,0,10);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public override void Attack()
	{
		if(canAttack)
		{
			Debug.Log("attack");
			arrowClone.transform.parent = null;
			Debug.Log(arrowClone.transform.parent);
			//isAttacking = true;
            canAttack = false;
			
			arrowClone.GetComponent<Rigidbody>().AddForce(arrowSpeed * 200f, ForceMode.Force);
			arrowClone.GetComponent<Rigidbody>().useGravity = true;
			//arrowClone.GetComponent<Rigidbody>().velocity = new Vector3(0,0,1);
			
            StartCoroutine(AttackCooldown());
		}
	}
	
	public override IEnumerator AttackCooldown()
	{
		
		yield return timer;
		ResetArrow();
		canAttack = true;
	}
	
	public void ResetArrow()
	{
		arrowClone.transform.parent = transform;
		arrowClone.transform.position = transform.position;
		arrowClone.GetComponent<Rigidbody>().useGravity = false;
		arrowClone.GetComponent<Rigidbody>().AddForce(Vector3.zero, ForceMode.VelocityChange);
	}
}
