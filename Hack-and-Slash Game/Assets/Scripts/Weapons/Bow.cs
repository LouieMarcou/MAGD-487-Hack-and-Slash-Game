using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponBase
{
	[SerializeField] private GameObject arrowPrefab;
	[SerializeField] private List<GameObject> arrows;
	private ObjectPool arrowObjectPool;

	private GameObject arrowClone;
	private GameObject currentArrow;
	private float numShots = 1;

	private float secondShotDelayFloat = 0.2f;
	private WaitForSeconds secondShotDelayTime;

	//public Camera camera;

	// Start is called before the first frame update
	void Start()
    {
  //      arrowClone = Instantiate(arrowPrefab, transform.position, transform.rotation, transform);
		//arrowClone.GetComponent<Arrow>().SetParent(transform);
		canAttack = true;
		arrowObjectPool = GameObject.Find("ArrowObjectPool").GetComponent<ObjectPool>();

		for (int i = 0; i < numShots; i++)
        {
			arrows.Add(arrowObjectPool.pooledObjects[i]);
			arrows[i].transform.position = transform.position;
			arrows[i].transform.rotation = transform.rotation;
			arrows[i].transform.parent = transform;
			arrows[i].GetComponent<Arrow>().SetBow(GetComponent<Bow>());
			arrows[i].GetComponent<Arrow>().SetParent(transform);
			//Debug.Log(i);
		}

		currentArrow = arrows[0];
		currentArrow.SetActive(true);
		

		secondShotDelayTime = new WaitForSeconds(secondShotDelayFloat);
    }
	
	public override void Attack()
	{
		if(canAttack)
		{
			currentArrow.transform.parent = null;
			isAttacking = true;
            canAttack = false;
			//Debug.Log(arrows.Count);

			if (arrows.Count == 1)
			{
				arrows[0].GetComponent<Arrow>().Shoot();
				StartCoroutine(AttackCooldown());
			}
			else if (arrows.Count == 2)
            {
				arrows[0].GetComponent<Arrow>().Shoot();
				StartCoroutine(TimeBetweenShots());
			}

			
		}
	}
	
	public override IEnumerator AttackCooldown()
	{
		
		yield return timer;
		//currentArrow.GetComponent<Arrow>().Reset();
		for (int i = 0; i < arrows.Count; i++)
		{
			arrows[i].GetComponent<Arrow>().Reset();
		}
		canAttack = true;
		isAttacking = false;
	}
	
	public void AddNumberOfShots(float num)
	{
		int val = (int)numShots;
		numShots += num;
		arrows.Add(arrowObjectPool.pooledObjects[val]);
        arrows[val].transform.position = transform.position;
        arrows[val].transform.rotation = transform.rotation;
        arrows[val].transform.parent = transform;
        arrows[val].GetComponent<Arrow>().SetBow(GetComponent<Bow>());
        arrows[val].GetComponent<Arrow>().SetParent(transform);
		//Debug.Log(arrows[val].transform.parent);
    }

	private IEnumerator TimeBetweenShots()
    {
		//Debug.Log("waiting for next shot");
		yield return secondShotDelayTime;
		//Debug.Log("next shot");
		arrows[1].GetComponent<Arrow>().Shoot();
		StartCoroutine(AttackCooldown());
	}

	void OnDisable()
    {
		numShots = 1;
		if (GetOrginialStats() != null)
		{
			weaponData.stats = GetOrginialStats();
		}
	}
}
