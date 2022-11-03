using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
	public EnemyData enemyData;
	
	public EnemyStats enemyStats;
	
	[SerializeField] private GameObject player;
	
	private float timeToAttack;
	
	private float currentHealth;
	
	void Awake()
	{
		player = GameObject.Find("Player");
	}
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyData.stats.health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime);
    }
	
	public virtual void SetData(EnemyData ed)
    {
        enemyData = ed;
        timeToAttack = enemyData.stats.timeToAttack;

        enemyStats = new EnemyStats(ed.stats.health, ed.stats.damage, ed.stats.speed, ed.stats.timeToAttack);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
		Debug.Log(currentHealth);
    }
}
