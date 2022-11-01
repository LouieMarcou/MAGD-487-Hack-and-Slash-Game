using UnityEngine;
using System;

[Serializable]
public class EnemyStats
{
	public int health;
	public float damage;
	public float speed;
	public float timeToAttack;
	
	public EnemyStats(int health, float damage, float speed, float timeToAttack)
	{
		this.health = health;
		this.damage = damage;
		this.speed = speed;
		this.timeToAttack = timeToAttack;
	}
}

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
	public string Name;
	//List of dropable items
	public EnemyStats stats;
	public GameObject EnemyBasePrefab;
}
