using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
	public float health;
	public float stamina;
	public float speed;
	
	public PlayerStats(float health, float stamina, float speed)
	{
		this.health = health;
		this.stamina = stamina;
		this.speed = speed;
	}
}


[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
	public string Name;
	public PlayerStats stats;
	public GameObject playerBasePrefab;
}
