using UnityEngine;
using System;

[Serializable]
public class EnemyStats
{
	public float health;
	public float damage;
	public float speed;
	public float timeToAttack;
	public float armor;

	public EnemyStats(float health, float damage, float speed, float timeToAttack)
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
