using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    public UpgradeData upgradeData; 

    public void ReflectDamage(EnemyBase enemy)
    {
        enemy.TakeDamage(enemy.enemyData.stats.damage * (upgradeData.uniqueNumber * 0.01f));
    }
}
