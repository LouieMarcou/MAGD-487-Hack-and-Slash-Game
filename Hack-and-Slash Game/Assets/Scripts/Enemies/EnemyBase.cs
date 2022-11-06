using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	public EnemyData enemyData;
	
	public EnemyStats enemyStats;
	
	[SerializeField] private GameObject player;
	
	public float currentHealth;

    public EnemyObjectPool objectPool;

    private WaitForSeconds attackTime;
    private bool canAttack = true;

	void Awake()
	{
		player = GameObject.Find("Player");
        SetData(enemyData);
        objectPool = GameObject.Find(enemyData.Name + "ObjectPool").GetComponent<EnemyObjectPool>();
        attackTime = new WaitForSeconds(enemyData.stats.timeToAttack);
        //Debug.Log(GetObjectPool());
	}

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyData.stats.health;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemyData.stats.speed * Time.deltaTime);
        }
        else if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
	
    //Sets enemy stats
	public virtual void SetData(EnemyData ed)
    {
        enemyData = ed;
        //timeToAttack = enemyData.stats.timeToAttack;

        enemyStats = new EnemyStats(ed.stats.health, ed.stats.damage, ed.stats.speed, ed.stats.timeToAttack);
    }

    //Subtracts damage from player health
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
		//Debug.Log(currentHealth);
    }

    //Upon death it sets the health of the enemy so it can be used again
    public void ResetHealth()
    {
        currentHealth = enemyData.stats.health;
    }

    //Gets object pool that it is apart of
    public EnemyObjectPool GetObjectPool()
    {
        return objectPool;
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (canAttack)
            {
                player.GetComponent<PlayerController>().TakeDamage(enemyData.stats.damage, gameObject);
                canAttack = false;
                StartCoroutine(AttackDelay());
            }
        }
    }

    IEnumerator AttackDelay()
    {
        yield return attackTime;
        canAttack = true;
    }

}
