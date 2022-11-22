using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    public float damage;
    public float timeToAttack;
    public float speed;

    public WeaponStats(float damage, float timeToAttack, float speed)
    {
        this.damage = damage;
        this.timeToAttack = timeToAttack;
        this.speed = speed;
    }
}

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public string Name;
    public WeaponStats stats;
    public GameObject weaponBasePrefab;
    public List<UpgradeData> upgrades;
}
